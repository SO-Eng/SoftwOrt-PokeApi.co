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
                NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }


        private int _language = 5;

        public int Language
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


        public async void LoadPokemonList()
        {
            NamedApiResourceList<Pokemon> allPokemons = await pokeClient.GetNamedResourcePageAsync<Pokemon>(30,0);

            for (int i = 0; i < allPokemons.Results.Count; i++)
            {
                PokemonSpecies pokemonNameLang = await pokeClient.GetResourceAsync<PokemonSpecies>(allPokemons.Results[i].Name);
                PokeList.Add(new PokemonModel { Id = i + 1, PokeNameOriginal = allPokemons.Results[i].Name, PokeName = pokemonNameLang.Names[Language].Name, PokeUrl = allPokemons.Results[i].Url});
            }
        }

        public void PokemonInfo()
        {
            if (SelectedPokemon != null)
            {
                PokemonInfoView = new PokemonInfoViewModel(Language, SelectedPokemon);
                Items.Add(new PokemonInfoViewModel(Language, SelectedPokemon));
            }
        }

        public void PokemonDescription()
        {
            if (SelectedPokemon != null)
            {
                PokemonDescrView = new PokemonDescrViewModel();
                Items.Add(new PokemonDescrViewModel());
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
