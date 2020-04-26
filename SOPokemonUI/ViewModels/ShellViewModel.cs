using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using PokeApiNet;
using SOPokemonUI.Helpers;
using SOPokemonUI.Models;

namespace SOPokemonUI.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        #region Fields

        // Initiate client
        PokeApiClient pokeClient = new PokeApiClient();

        private BindableCollection<PokemonModel> _pokeList = new BindableCollection<PokemonModel>();

        public BindableCollection<PokemonModel> PokeList
        {
            get { return _pokeList; }
            set { _pokeList = value; }
        }
        
        private PokemonModel _selectedPokemon;

        public PokemonModel SelectedPokemon
        {
            get { return _selectedPokemon; }
            set
            {
                _selectedPokemon = value;
                PokemonInfo();
                PokemonDescription();
                PokemonEvolutions();
            }
        }


        private string _language = "en";

        public string Language
        {
            get { return _language; }
            set { _language = value; }
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

        public ShellViewModel()
        {
            LoadPokemonList();
        }

        // Fill ListView with all Pokemons in selected language and save them in PokemonModel
        public async void LoadPokemonList()
        {
            NamedApiResourceList<Pokemon> allPokemons = await pokeClient.GetNamedResourcePageAsync<Pokemon>(34,0); // MAX limit: 807

            for (int i = 1; i <= allPokemons.Results.Count; i++)
            {
                PokemonSpecies pokemonNameLang = await pokeClient.GetResourceAsync<PokemonSpecies>(i);
                for (int j = 0; j < pokemonNameLang.Names.Count; j++)
                {
                    if (pokemonNameLang.Names[j].Language.Name == _language)
                    {
                        PokeList.Add(new PokemonModel { Id = i, PokeNameOriginal = allPokemons.Results[i - 1].Name, PokeName = pokemonNameLang.Names[j].Name, PokeUrl = allPokemons.Results[i - 1].Url });
                    }
                }
            }
        }

        // Load PokemonInfoView to ShellView
        public void PokemonInfo()
        {
            PokemonInfoView = null;
            if (SelectedPokemon != null)
            {
                PokemonInfoView = new PokemonInfoViewModel(Language, SelectedPokemon);
                ActivateItemAsync(PokemonInfoView, CancellationToken.None);
            }
        }

        // Load PokemonDescrView to ShellView
        public void PokemonDescription()
        {
            PokemonDescrView = null;
            if (SelectedPokemon != null)
            {
                PokemonDescrView = new PokemonDescrViewModel(Language, SelectedPokemon);
                ActivateItemAsync(PokemonDescrView, CancellationToken.None);
            }
        }

        public void PokemonEvolutions()
        {
            PokemonEvoView = null;
            if (SelectedPokemon != null)
            {
                PokemonEvoView = new PokemonEvoViewModel(Language, SelectedPokemon, PokeList);
                ActivateItemAsync(PokemonDescrView, CancellationToken.None);
            }
        }

        public void SelectNewPokemon(int pokeId)
        {
            for (int i = 1; i <= PokeList.Count; i++)
            {
                if (PokeList[i].Id == pokeId)
                {
                    SelectedPokemon = PokeList[i];
                    break;
                }
            }

            NotifyOfPropertyChange(() => SelectedPokemon);
        }

        // End Application
        public void Exit()
        {
            TryCloseAsync();
        }

        #endregion
    }
}
