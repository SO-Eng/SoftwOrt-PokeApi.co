using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
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
            NamedApiResourceList<Pokemon> allPokemons = await pokeClient.GetNamedResourcePageAsync<Pokemon>(808,0);

            string tempPokeName;
            string buildNameNew;

            for (int i = 0; i < allPokemons.Results.Count; i++)
            {
                tempPokeName = allPokemons.Results[i].Name;
                buildNameNew = char.ToUpper(tempPokeName[0]) + tempPokeName.Substring(1).ToLower();
                PokeList.Add(new PokemonModel{ PokeName = buildNameNew, PokeUrl = allPokemons.Results[i].Url});
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
                try
                {
                    Pokemon pokemonInfo = await pokeClient.GetResourceAsync<Pokemon>(SelectedPokemon.PokeName);

                    PokeImage = new BitmapImage(new Uri(pokemonInfo.Sprites.FrontDefault, UriKind.Absolute));
                    PokemonWeight = pokemonInfo.Weight;

                    NotifyOfPropertyChange(() => PokeImage);
                    NotifyOfPropertyChange(() => PokemonWeight);
                }
                catch
                {
                    MessageBox.Show($"Leider gibt es noch kein Bild zu { SelectedPokemon.PokeName }, \nbitte probiere es ein anderes mal wieder.",
                        "Fehler beim laden...", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
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
