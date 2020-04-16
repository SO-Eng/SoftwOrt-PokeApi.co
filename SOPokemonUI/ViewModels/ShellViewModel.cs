using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using Caliburn.Micro;
using PokeApiNet;
using SOPokemonUI.Models;

namespace SOPokemonUI.ViewModels
{
    public class ShellViewModel : Screen
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
                NotifyOfPropertyChange(() => SelectedPokemon);
                LoadPokemonInfo();
            }
        }

        private BitmapImage _pokeImage;
        public BitmapImage PokeImage
        {
            get { return _pokeImage; }
            set
            {
                _pokeImage = value;
                NotifyOfPropertyChange(() => SelectedPokemon);

            }
        }

        private int _pokemonWeight;

        public int PokemonWeight
        {
            get { return _pokemonWeight; }
            set
            {
                _pokemonWeight = value;
                NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }



        #endregion


        #region Methods

        public ShellViewModel()
        {
            LoadPokemonPage();
        }


        public async void LoadPokemonPage()
        {
            NamedApiResourceList<Pokemon> firstPokemon = await pokeClient.GetNamedResourcePageAsync<Pokemon>(60,0);

            for (int i = 0; i < firstPokemon.Results.Count; i++)
            {
                PokeList.Add(new PokemonModel{ PokeName = firstPokemon.Results[i].Name, PokeUrl = firstPokemon.Results[i].Url});
            }
        }

        public async void LoadPokemonInfo()
        {
            if (SelectedPokemon == null)
            {
                return;
            }
            else
            {
                Pokemon pokemonInfo = await pokeClient.GetResourceAsync<Pokemon>(SelectedPokemon.PokeName);

                PokeImage = new BitmapImage(new Uri(pokemonInfo.Sprites.FrontDefault, UriKind.Absolute));
                PokemonWeight = pokemonInfo.Weight;

                NotifyOfPropertyChange(() => PokeImage);
                NotifyOfPropertyChange(() => PokemonWeight);
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
