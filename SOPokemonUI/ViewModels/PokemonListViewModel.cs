using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using PokeApiNet;
using SOPokemonUI.EventModels;
using SOPokemonUI.Helpers;
using SOPokemonUI.Models;

namespace SOPokemonUI.ViewModels
{
    class PokemonListViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<EvoPokemonEvent>
    {
        #region Fields

        private readonly IEventAggregator _events;

        // Initiate client
        PokeApiClient pokeClient = new PokeApiClient();

        private BindableCollection<PokemonModel> _pokeList = new BindableCollection<PokemonModel>();

        public BindableCollection<PokemonModel> PokeList
        {
            get { return _pokeList; }
            set { _pokeList = value; }
        }

        private BindableCollection<PokemonModel> _searchPokeList = new BindableCollection<PokemonModel>();

        public BindableCollection<PokemonModel> SearchPokeList
        {
            get { return _searchPokeList; }
            set
            {
                _searchPokeList = value;
            }
        }

        private PokemonModel _selectedPokemon;

        public PokemonModel SelectedPokemon
        {
            get { return _selectedPokemon; }
            set
            {
                _selectedPokemon = value;
                //PokemonInfo();
                //PokemonDescription();
                //PokemonEvolutions();
                HandleAsync(new EvoPokemonEvent(), CancellationToken.None);
                NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }

        private ListView _pokemonList;

        public ListView PokemonList
        {
            get { return _pokemonList; }
            set
            {
                _pokemonList = value;
                NotifyOfPropertyChange(() => PokemonList);
            }
        }

        private string _language;

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        private string _searchBox;

        public string SearchBox
        {
            get { return _searchBox; }
            set
            {
                _searchBox = value;
                SearchInCollection();
                NotifyOfPropertyChange(() => SearchBox);
            }
        }

        private string _searchHeader;

        public string SearchHeader
        {
            get { return _searchHeader; }
            set
            {
                _searchHeader = value;
                NotifyOfPropertyChange(() => SearchHeader);
            }
        }


        private IScreen _pokemonInfoView;
        public IScreen PokemonInfoView
        {
            get { return _pokemonInfoView; }
            private set
            {
                _pokemonInfoView = value;
                NotifyOfPropertyChange(() => PokemonInfoView);
            }
        }

        private IScreen _pokemonDescrView;
        public IScreen PokemonDescrView
        {
            get { return _pokemonDescrView; }
            private set
            {
                _pokemonDescrView = value;
                NotifyOfPropertyChange(() => PokemonDescrView);
            }
        }

        private IScreen _pokemonEvoView;

        public IScreen PokemonEvoView
        {
            get { return _pokemonEvoView; }
            set
            {
                _pokemonEvoView = value;
                NotifyOfPropertyChange(() => PokemonEvoView);
            }
        }

        #endregion


        #region Methods
        public PokemonListViewModel(string language, IEventAggregator events)
        {
            _events = events;
            _events.SubscribeOnUIThread(this);
            Language = language;
            SetSearchLanguage();
            LoadPokemonList();
        }

        private void SetSearchLanguage()
        {
            SearchHeader = SearchTextLanguage.GetSearchLanguage(Language);
        }

        // Fill ListView with all Pokemons in selected language and save them in PokemonModel
        public async void LoadPokemonList()
        {
            NamedApiResourceList<Pokemon> allPokemons = await pokeClient.GetNamedResourcePageAsync<Pokemon>(200, 0); // MAX limit: 807

            for (int i = 1; i <= allPokemons.Results.Count; i++)
            {
                PokemonSpecies pokemonNameLang = await pokeClient.GetResourceAsync<PokemonSpecies>(i);
                for (int j = 0; j < pokemonNameLang.Names.Count; j++)
                {
                    if (pokemonNameLang.Names[j].Language.Name == Language)
                    {
                        PokeList.Add(new PokemonModel { Id = i, PokeNameOriginal = allPokemons.Results[i - 1].Name, PokeName = pokemonNameLang.Names[j].Name, PokeUrl = allPokemons.Results[i - 1].Url });
                        SearchPokeList.Add(new PokemonModel { Id = i, PokeNameOriginal = allPokemons.Results[i - 1].Name, PokeName = pokemonNameLang.Names[j].Name, PokeUrl = allPokemons.Results[i - 1].Url });
                    }
                }
            }
        }

        // Dynamic search in UI ListView
        private void SearchInCollection()
        {
            SearchPokeList.Clear();
            var tempSearch = PokeList.Where(x => x.PokeName.ToLower().Contains(SearchBox.ToLower()));

            foreach (var pokemon in tempSearch)
            {
                SearchPokeList.Add(new PokemonModel { Id = pokemon.Id, PokeName = pokemon.PokeName, PokeNameOriginal = pokemon.PokeNameOriginal, PokeUrl = pokemon.PokeUrl });
            }
            NotifyOfPropertyChange(() => SearchPokeList);
        }


        // Load PokemonInfoView, -DescrView and -EvoView to ShellView
        public async Task HandleAsync(EvoPokemonEvent message, CancellationToken cancellationToken)
        {
            if (SelectedPokemon == null)
            {
                return;
            }
            if (message.SelectedEvo != null)
            {
                int pokeId = message.SelectedEvo.Id;
                message.SelectedEvo = null;
                FireEvent(pokeId);
                return;
            }

            PokemonInfoView = new PokemonInfoViewModel(Language, SelectedPokemon);
            await ActivateItemAsync(PokemonInfoView, CancellationToken.None);

            PokemonDescrView = new PokemonDescrViewModel(Language, SelectedPokemon);
            await ActivateItemAsync(PokemonDescrView, CancellationToken.None);

            PokemonEvoView = new PokemonEvoViewModel(Language, SelectedPokemon, PokeList, _events);
            await ActivateItemAsync(PokemonDescrView, CancellationToken.None);
        }

        public void FireEvent(int pokeId)
        {
            for (int i = 0; i <= PokeList.Count; i++)
            {
                if (SearchPokeList[i].Id == pokeId)
                {
                    SelectedPokemon = SearchPokeList[i];
                    break;
                }
            }

            NotifyOfPropertyChange(() => SelectedPokemon);
        }
        #endregion
    }
}
