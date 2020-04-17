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

        private string _pokemonName;

        public string PokemonName
        {
            get { return _pokemonName; }
            set
            {
                _pokemonName = value;
                NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }


        private string _pokemonWeight;

        public string PokemonWeight
        {
            get { return _pokemonWeight; }
            set
            {
                _pokemonWeight = value;
                NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }

        private string _pokemonHeight;

        public string PokemonHeight
        {
            get { return _pokemonHeight; }
            set
            {
                _pokemonHeight = value;
                NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }

        private List<AbilityModel> _pokemonAbilityList = new List<AbilityModel>();

        public List<AbilityModel> PokemonAbilityList
        {
            get { return _pokemonAbilityList; }
            set
            {
                _pokemonAbilityList = value;
                NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }

        private string _pokemonAbilities;

        public string PokemonAbilities
        {
            get { return _pokemonAbilities; }
            set
            {
                _pokemonAbilities = value;
                NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }

        private string _pokemonAbilities2;

        public string PokemonAbilities2
        {
            get { return _pokemonAbilities2; }
            set
            {
                _pokemonAbilities2 = value;
                NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }

        private string _pokemonAbilities3;

        public string PokemonAbilities3
        {
            get { return _pokemonAbilities3; }
            set
            {
                _pokemonAbilities3 = value;
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
                Pokemon pokemonInfo = await pokeClient.GetResourceAsync<Pokemon>(SelectedPokemon.PokeName);

                PokeImage = LoadPokemonImage(pokemonInfo);

                PokemonName = SelectedPokemon.PokeName;
                PokemonWeight = (pokemonInfo.Weight / 10).ToString("##.## 'Kg'");
                PokemonHeight = (pokemonInfo.Height / 10).ToString("#0.### 'm'");

                PokemonAbilities = "";
                PokemonAbilities2 = "";
                PokemonAbilities3 = "";

                for (int i = 0; i < pokemonInfo.Abilities.Count; i++)
                {
                    string tempAbility = char.ToUpper(pokemonInfo.Abilities[i].Ability.Name[0]) +
                                         pokemonInfo.Abilities[i].Ability.Name.Substring(1).ToLower();
                    PokemonAbilityList.Add(new AbilityModel { Ability = tempAbility });
                }

                PokemonAbilities = PokemonAbilityList.First().Ability;

                if (PokemonAbilityList.Count > 1)
                {
                    PokemonAbilities2 = PokemonAbilityList[1].Ability;
                }

                if (PokemonAbilityList.Count > 2)
                {
                    PokemonAbilities3 = PokemonAbilityList[2].Ability;
                }

                NotifyOfPropertyChange(() => PokemonName);
                NotifyOfPropertyChange(() => PokeImage);
                NotifyOfPropertyChange(() => PokemonWeight);
                NotifyOfPropertyChange(() => PokemonHeight);
                NotifyOfPropertyChange(() => PokemonAbilityList);
                NotifyOfPropertyChange(() => PokemonAbilities);
                NotifyOfPropertyChange(() => PokemonAbilities2);
                NotifyOfPropertyChange(() => PokemonAbilities3);
                PokemonAbilityList.Clear();
            }
        }

        private BitmapImage LoadPokemonImage(Pokemon picture)
        {
            BitmapImage imageTemp;
            try
            {
                imageTemp = new BitmapImage(new Uri(picture.Sprites.FrontDefault, UriKind.Absolute));

            }
            catch
            {
                imageTemp = new BitmapImage(new Uri("https://www.softwort-engineering.com/downloads/pokemon/PicNA_Pokemon.png", UriKind.Absolute));
            }
            return imageTemp;
        }

        // End Application
        public void Exit()
        {
            TryCloseAsync();
        }

        #endregion
    }
}
