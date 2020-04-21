using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using PokeApiNet;
using SOPokemonUI.Models;
using Type = PokeApiNet.Type;

namespace SOPokemonUI.ViewModels
{
    public class PokemonInfoViewModel : Screen
    {
        #region Fields
        private int p = 0;
        private int q = 0;

        // Initiate client
        PokeApiClient pokeClient = new PokeApiClient();

        private readonly int _language;
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
                //NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }

        private string _pokemonName;

        public string PokemonName
        {
            get { return _pokemonName; }
            set
            {
                _pokemonName = value;
                //NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }


        private string _pokemonWeight;

        public string PokemonWeight
        {
            get { return _pokemonWeight; }
            set
            {
                _pokemonWeight = value;
                //NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }

        private string _pokemonHeight;

        public string PokemonHeight
        {
            get { return _pokemonHeight; }
            set
            {
                _pokemonHeight = value;
                //NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }

        private List<TypeModel> _pokemonTypeList = new List<TypeModel>();

        public List<TypeModel> PokemonTypeList
        {
            get { return _pokemonTypeList; }
            set { _pokemonTypeList = value; }
        }

        private string _typeOne;

        public string TypeOne
        {
            get { return _typeOne; }
            set { _typeOne = value; }
        }

        private string _typeTwo;

        public string TypeTwo
        {
            get { return _typeTwo; }
            set { _typeTwo = value; }
        }

        private string _typeThree;

        public string TypeThree
        {
            get { return _typeThree; }
            set { _typeThree = value; }
        }


        private List<AbilityModel> _pokemonAbilityList = new List<AbilityModel>();

        public List<AbilityModel> PokemonAbilityList
        {
            get { return _pokemonAbilityList; }
            set
            {
                _pokemonAbilityList = value;
                //NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }

        private string _pokemonAbilities;

        public string PokemonAbilities
        {
            get { return _pokemonAbilities; }
            set
            {
                _pokemonAbilities = value;
                //NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }

        private string _pokemonAbilities2;

        public string PokemonAbilities2
        {
            get { return _pokemonAbilities2; }
            set
            {
                _pokemonAbilities2 = value;
                //NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }

        private string _pokemonAbilities3;

        public string PokemonAbilities3
        {
            get { return _pokemonAbilities3; }
            set
            {
                _pokemonAbilities3 = value;
                //NotifyOfPropertyChange(() => SelectedPokemon);
            }
        }

        #endregion




        #region Methods

        public PokemonInfoViewModel()
        {
            
        }

        public PokemonInfoViewModel(int language,PokemonModel selectedPokemon)
        {
            _language = language;
            SelectedPokemon = selectedPokemon;
            //_pokeClient = pokeClient;
            LoadPokemonInfo();
        }

        public async void LoadPokemonInfo()
        {
            PokemonSpecies pokemonNameLang = await pokeClient.GetResourceAsync<PokemonSpecies>(SelectedPokemon.PokeNameOriginal);
            Pokemon pokemonInfo = await pokeClient.GetResourceAsync<Pokemon>(SelectedPokemon.PokeNameOriginal);
            Ability ability;

            LoadPokemonImage(pokemonInfo);

            LoadPokemonType(pokemonInfo); 

            PokemonName = pokemonNameLang.Names[_language].Name;
            PokemonWeight = (pokemonInfo.Weight / 10).ToString("##.## 'Kg'");
            PokemonHeight = (pokemonInfo.Height / 10).ToString("#0.### 'm'");

            PokemonAbilities2 = "";
            PokemonAbilities3 = "";


            for (int i = 0; i < pokemonInfo.Abilities.Count; i++)
            {
                if (p > 0)
                {
                    break;
                }

                PokemonAbilityList.Add(new AbilityModel
                {
                    AbilitiesUrl = pokemonInfo.Abilities[i].Ability.Url,
                    AbilityId = Int32.Parse(pokemonInfo.Abilities[i].Ability.Url.Where(Char.IsDigit).ToArray())
                });
                if (PokemonAbilityList[i].AbilityId < 30 && PokemonAbilityList[i].AbilityId > 0)
                {
                    PokemonAbilityList[i].AbilityId -= 20;
                }
                else if (PokemonAbilityList[i].AbilityId < 999 && PokemonAbilityList[i].AbilityId > 30)
                {
                    PokemonAbilityList[i].AbilityId -= 200;
                }
                else
                {
                    PokemonAbilityList[i].AbilityId -= 2000;
                }
            }

            p += 1;

            try
            {
                ability = await pokeClient.GetResourceAsync<Ability>(PokemonAbilityList[0].AbilityId);
                PokemonAbilities = ability.Names[_language].Name;

                if (PokemonAbilityList.Count > 1)
                {
                    ability = await pokeClient.GetResourceAsync<Ability>(PokemonAbilityList[1].AbilityId);
                    PokemonAbilities2 = ability.Names[_language].Name;
                }

                if (PokemonAbilityList.Count > 2)
                {
                    ability = await pokeClient.GetResourceAsync<Ability>(PokemonAbilityList[2].AbilityId);
                    PokemonAbilities3 = ability.Names[_language].Name;
                }
            }
            catch 
            {
                //
            }

            NotifyOfPropertyChange(() => PokemonName);
            NotifyOfPropertyChange(() => PokemonWeight);
            NotifyOfPropertyChange(() => PokemonHeight);
            NotifyOfPropertyChange(() => PokemonAbilities);
            NotifyOfPropertyChange(() => PokemonAbilities2);
            NotifyOfPropertyChange(() => PokemonAbilities3);
            PokemonAbilityList.Clear();
            p = 0;
        }

        private void LoadPokemonImage(Pokemon pokemonPic)
        {
            BitmapImage imageTemp;
            try
            {
                imageTemp = new BitmapImage(new Uri(pokemonPic.Sprites.FrontDefault, UriKind.Absolute));

            }
            catch
            {
                imageTemp = new BitmapImage(new Uri("https://www.softwort-engineering.com/downloads/pokemon/PicNA_Pokemon.png", UriKind.Absolute));
            }

            PokeImage = imageTemp;
            NotifyOfPropertyChange(() => PokeImage);
        }

        private async void LoadPokemonType(Pokemon pokemonInfo)
        {
            Type type = await pokeClient.GetResourceAsync<Type>(SelectedPokemon.Id);

            TypeOne = "";
            TypeTwo = "";
            TypeThree = "";

            for (int i = 0; i < pokemonInfo.Types.Count; i++)
            {
                if (q > 0)
                {
                    break;
                }
                PokemonTypeList.Add( new TypeModel
                {
                    TypeUrl = pokemonInfo.Types[i].Type.Url,
                    TypeId = Int32.Parse(pokemonInfo.Types[i].Type.Url.Where(Char.IsDigit).ToArray())
                });
                if (PokemonTypeList[i].TypeId < 30)
                {
                    PokemonTypeList[i].TypeId -= 20;
                }
                else if (PokemonTypeList[i].TypeId < 999)
                {
                    PokemonTypeList[i].TypeId -= 200;
                }
                else
                {
                    PokemonTypeList[i].TypeId -= 2000;
                }
            }

            q = 1;

            try
            {
                type = await pokeClient.GetResourceAsync<Type>(PokemonTypeList[0].TypeId);
                TypeOne = type.Names[3].Name;

                if (PokemonTypeList.Count > 1)
                {
                    type = await pokeClient.GetResourceAsync<Type>(PokemonTypeList[1].TypeId);
                    TypeTwo = type.Names[3].Name;
                }

                if (PokemonTypeList.Count > 2)
                {
                    type = await pokeClient.GetResourceAsync<Type>(PokemonTypeList[2].TypeId);
                    TypeThree = type.Names[3].Name;
                }
            }
            catch
            {
                //Console.WriteLine("Page not found: 404");
            }

            //NotifyOfPropertyChange(() => PokemonAbilityList);
            NotifyOfPropertyChange(() => TypeOne);
            NotifyOfPropertyChange(() => TypeTwo);
            NotifyOfPropertyChange(() => TypeThree);
            PokemonTypeList.Clear();
            q = 0;
        }

        #endregion
    }
}
