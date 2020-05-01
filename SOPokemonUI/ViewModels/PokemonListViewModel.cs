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
using SOPokemonUI.LanguagePack;
using SOPokemonUI.Models;

namespace SOPokemonUI.ViewModels
{
    class PokemonListViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<EvoPokemonEvent>
    {
        #region Fields

        private readonly IEventAggregator _events;
        private readonly string _csvPath;
        int q = 0;
        private char cSearch;

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
        public PokemonListViewModel(string language, IEventAggregator events, string csvPath)
        {
            _events = events;
            _csvPath = csvPath;
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
            var loadPokemonsCsv = FileProcessor.FileProcessor.LoadFromCsvFile<PokemonModel>(_csvPath);
            if (loadPokemonsCsv != null)
            {
                if (loadPokemonsCsv.Count < 807)
                {
                    await LoadFromApi();
                    // Save to CSV file
                    FileProcessor.FileProcessor.SaveToCsvFile(PokeList, _csvPath);
                }
                else
                {
                    await LoadFromDisk(loadPokemonsCsv);
                }
            }
            else
            {
                await LoadFromApi();
                // Save to CSV file
                FileProcessor.FileProcessor.SaveToCsvFile(PokeList, _csvPath);
            }
        }

        private async Task LoadFromDisk(BindableCollection<PokemonModel> loadPokemonsCsv)
        {
            foreach (var pokemon in loadPokemonsCsv)
            {
                PokeList.Add(new PokemonModel
                {
                    Id = pokemon.Id,
                    PokeNameOriginal = pokemon.PokeNameOriginal,
                    PokeName = pokemon.PokeName,
                    PokeUrl = pokemon.PokeUrl
                });
                SearchPokeList.Add(new PokemonModel
                {
                    Id = pokemon.Id,
                    PokeNameOriginal = pokemon.PokeNameOriginal,
                    PokeName = pokemon.PokeName,
                    PokeUrl = pokemon.PokeUrl
                });
            }
            await _events.PublishOnUIThreadAsync(new LoadingBarEvent { LoadingCount = loadPokemonsCsv.Count }, CancellationToken.None);
        }

        private async Task LoadFromApi()
        {
            // MAX limit currently: 807
            int maxPokemons = 807;

            NamedApiResourceList<Pokemon> allPokemons = await pokeClient.GetNamedResourcePageAsync<Pokemon>(maxPokemons, 0);

            for (int i = 1; i <= allPokemons.Results.Count; i++)
            {
                PokemonSpecies pokemonNameLang = await pokeClient.GetResourceAsync<PokemonSpecies>(i);
                for (int j = 0; j < pokemonNameLang.Names.Count; j++)
                {
                    if (pokemonNameLang.Names[j].Language.Name == Language)
                    {
                        PokeList.Add(new PokemonModel
                        {
                            Id = i,
                            PokeNameOriginal = allPokemons.Results[i - 1].Name,
                            PokeName = pokemonNameLang.Names[j].Name,
                            PokeUrl = allPokemons.Results[i - 1].Url
                        });
                        SearchPokeList.Add(new PokemonModel
                        {
                            Id = i,
                            PokeNameOriginal = allPokemons.Results[i - 1].Name,
                            PokeName = pokemonNameLang.Names[j].Name,
                            PokeUrl = allPokemons.Results[i - 1].Url
                        });
                    }
                }
                // Brings loading status to front-end in ProgressBar
                await _events.PublishOnUIThreadAsync(new LoadingBarEvent { LoadingCount = i }, CancellationToken.None);
            }
        }


        // Dynamic search in UI ListView
        private void SearchInCollection()
        {
            SearchPokeList.Clear();
            var tempSearchString = PokeList.Where(x => x.PokeName.ToLower().Contains(SearchBox.ToLower()));
            var tempSearchId = PokeList.Where(x => x.Id.ToString().Contains(SearchBox));

            // Try to search for numbers if Searchbox contains digits
            try
            {
                cSearch = SearchBox.ToCharArray()[0];
            }
            catch
            {
                //
            }

            if (cSearch >= '0' && cSearch <= '9')
            {
                foreach (var pokemon in tempSearchId)
                {
                    SearchPokeList.Add(new PokemonModel { Id = pokemon.Id, PokeName = pokemon.PokeName, PokeNameOriginal = pokemon.PokeNameOriginal, PokeUrl = pokemon.PokeUrl });
                }
            }
            else // Search for string
            {
                foreach (var pokemon in tempSearchString)
                {
                    SearchPokeList.Add(new PokemonModel { Id = pokemon.Id, PokeName = pokemon.PokeName, PokeNameOriginal = pokemon.PokeNameOriginal, PokeUrl = pokemon.PokeUrl });
                }
            }

            NotifyOfPropertyChange(() => SearchPokeList);
        }


        // Load PokemonInfoView, -DescrView and -EvoView to ShellView
        public async Task HandleAsync(EvoPokemonEvent message, CancellationToken cancellationToken)
        {
            if (message.SelectedEvo != null)
            {
                int pokeId = message.SelectedEvo.Id;
                message.SelectedEvo = null;
                FireEvent(pokeId);
                return;
            }
            if (SelectedPokemon == null)
            {
                return;
            }


            PokemonInfoView = new PokemonInfoViewModel(Language, SelectedPokemon);
            await ActivateItemAsync(PokemonInfoView, CancellationToken.None);

            PokemonDescrView = new PokemonDescrViewModel(Language, SelectedPokemon);
            await ActivateItemAsync(PokemonDescrView, CancellationToken.None);

            PokemonEvoView = new PokemonEvoViewModel(Language, SelectedPokemon, PokeList, _events);
            await ActivateItemAsync(PokemonDescrView, CancellationToken.None);

            if (SearchBox?.Length > 0)
            {
                RestructureList();
            }
        }

        // Clear SearchBox and Select Pokemon in ListView (jump to item)
        private void RestructureList()
        {
            SearchBox = "";

            for (int i = 0; i <= SearchPokeList.Count; i++)
            {
                if (SearchPokeList[i].Id == SelectedPokemon.Id)
                {
                    SelectedPokemon = SearchPokeList[i];
                    break;
                }
            }
        }

        // Jump to item in ListView and clear SearchBox before
        public void FireEvent(int pokeId)
        {
            SearchBox = "";

            for (int i = 0; i <= SearchPokeList.Count; i++)
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
