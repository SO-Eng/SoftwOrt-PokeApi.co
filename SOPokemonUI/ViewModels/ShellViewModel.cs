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

        private int _language = 5;

        public int Language
        {
            get { return _language; }
            set { _language = value; }
        }


        #endregion


        #region Methods

        public ShellViewModel()
        {
            LoadPokemonPage();
        }


        public async void LoadPokemonPage()
        {
            NamedApiResourceList<Pokemon> allPokemons = await pokeClient.GetNamedResourcePageAsync<Pokemon>(80,0);

            for (int i = 0; i < allPokemons.Results.Count; i++)
            {
                PokemonSpecies pokemonNameLang = await pokeClient.GetResourceAsync<PokemonSpecies>(allPokemons.Results[i].Name);
                PokeList.Add(new PokemonModel { Id = i + 1, PokeNameOriginal = allPokemons.Results[i].Name, PokeName = pokemonNameLang.Names[Language].Name, PokeUrl = allPokemons.Results[i].Url});
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
                PokemonSpecies pokemonNameLang = await pokeClient.GetResourceAsync<PokemonSpecies>(SelectedPokemon.PokeNameOriginal);
                Pokemon pokemonInfo = await pokeClient.GetResourceAsync<Pokemon>(SelectedPokemon.PokeNameOriginal);
                Ability ability = await pokeClient.GetResourceAsync<Ability>(SelectedPokemon.Id);

                //PokeImage = LoadPokemonImage(pokemonInfo);
                LoadPokemonImage(pokemonInfo);

                //PokemonName = SelectedPokemon.PokeName;
                PokemonName = pokemonNameLang.Names[Language].Name;
                PokemonWeight = (pokemonInfo.Weight / 10).ToString("##.## 'Kg'");
                PokemonHeight = (pokemonInfo.Height / 10).ToString("#0.### 'm'");

                PokemonAbilities2 = "";
                PokemonAbilities3 = "";

                for (int i = 0; i < pokemonInfo.Abilities.Count; i++)
                {
                    PokemonAbilityList.Add(new AbilityModel { AbilitiesUrl = pokemonInfo.Abilities[i].Ability.Url, 
                        AbilityId = Int32.Parse(pokemonInfo.Abilities[i].Ability.Url.Where(Char.IsDigit).ToArray()) });
                    if (PokemonAbilityList[i].AbilityId < 30)
                    {
                        PokemonAbilityList[i].AbilityId -= 20;
                    }
                    else if (PokemonAbilityList[i].AbilityId < 999)
                    {
                        PokemonAbilityList[i].AbilityId -= 200;
                    }
                    else
                    {
                        PokemonAbilityList[i].AbilityId -= 2000;
                    }
                }

                try
                {
                    ability = await pokeClient.GetResourceAsync<Ability>(PokemonAbilityList[0].AbilityId);
                    PokemonAbilities = ability.Names[Language].Name;

                    if (PokemonAbilityList.Count > 1)
                    {
                        ability = await pokeClient.GetResourceAsync<Ability>(PokemonAbilityList[1].AbilityId);
                        PokemonAbilities2 = ability.Names[Language].Name;
                    }

                    if (PokemonAbilityList.Count > 2)
                    {
                        ability = await pokeClient.GetResourceAsync<Ability>(PokemonAbilityList[2].AbilityId);
                        PokemonAbilities3 = ability.Names[Language].Name;
                    }
                }
                catch
                {
                    //Console.WriteLine("Page not found: 404");
                }

                NotifyOfPropertyChange(() => PokemonName);
                //NotifyOfPropertyChange(() => PokeImage);
                NotifyOfPropertyChange(() => PokemonWeight);
                NotifyOfPropertyChange(() => PokemonHeight);
                NotifyOfPropertyChange(() => PokemonAbilityList);
                NotifyOfPropertyChange(() => PokemonAbilities);
                NotifyOfPropertyChange(() => PokemonAbilities2);
                NotifyOfPropertyChange(() => PokemonAbilities3);
                PokemonAbilityList.Clear();
            }
        }

        private void LoadPokemonImage(Pokemon picture)
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

            PokeImage = imageTemp;
            NotifyOfPropertyChange(() => PokeImage);

            //return imageTemp;
        }

        // End Application
        public void Exit()
        {
            TryCloseAsync();
        }

        #endregion
    }
}
