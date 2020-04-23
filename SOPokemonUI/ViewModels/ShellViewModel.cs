using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using Caliburn.Micro;
using PokeApiNet;
using SOPokemonUI.Models;

namespace SOPokemonUI.ViewModels
{
    public class ShellViewModel : Conductor<Screen>.Collection.AllActive
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
            }
        }


        private string _language = "de";

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        private Screen _pokemonInfoView;
        public Screen PokemonInfoView
        {
            get { return _pokemonInfoView; }
            private set
            {
                _pokemonInfoView = value;
                NotifyOfPropertyChange(() => PokemonInfoView);
            }
        }

        private Screen _pokemonDescrView;
        public Screen PokemonDescrView
        {
            get { return _pokemonDescrView; }
            private set
            {
                _pokemonDescrView = value;
                NotifyOfPropertyChange(() => PokemonDescrView);
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
            NamedApiResourceList<Pokemon> allPokemons = await pokeClient.GetNamedResourcePageAsync<Pokemon>(30,0); // MAX limit: 807

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
            if (SelectedPokemon != null)
            {
                PokemonInfoView = new PokemonInfoViewModel(Language, SelectedPokemon);
                Items.Add(PokemonInfoView);
            }
        }

        // Load PokemonDescrView to ShellView
        public void PokemonDescription()
        {
            if (SelectedPokemon != null)
            {
                PokemonDescrView = new PokemonDescrViewModel(Language, SelectedPokemon);
                Items.Add(PokemonDescrView);
            }
        }

        // End Application
        public void Exit()
        {
            TryCloseAsync();
        }

        #endregion
    }
}
