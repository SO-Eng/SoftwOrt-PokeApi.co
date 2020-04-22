using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using PokeApiNet;
using SOPokemonUI.Models;
//using Type = PokeApiNet.Type;

namespace SOPokemonUI.ViewModels
{
    public class PokemonInfoViewModel : Screen
    {
        #region Fields
        private int p = 0;
        private int q = 0;

        // Initiate client
        readonly PokeApiClient pokeClient = new PokeApiClient();

        private readonly int _language;

        private PokemonModel _selectedPokemon;

        public PokemonModel SelectedPokemon
        {
            get { return _selectedPokemon; }
            set { _selectedPokemon = value; }
        }

        private BitmapImage _pokeImage;
        public BitmapImage PokeImage
        {
            get { return _pokeImage; }
            set { _pokeImage = value; }
        }

        private string _pokemonName;

        public string PokemonName
        {
            get { return _pokemonName; }
            set { _pokemonName = value; }
        }


        private string _pokemonWeight;

        public string PokemonWeight
        {
            get { return _pokemonWeight; }
            set { _pokemonWeight = value; }
        }

        private string _pokemonHeight;

        public string PokemonHeight
        {
            get { return _pokemonHeight; }
            set { _pokemonHeight = value; }
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
        private Brush _typeOneBgBrush;

        public Brush TypeOneBgBrush
        {
            get { return _typeOneBgBrush; }
            set { _typeOneBgBrush = value; }
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
            set { _pokemonAbilityList = value; }
        }

        private string _pokemonAbilities;

        public string PokemonAbilities
        {
            get { return _pokemonAbilities; }
            set { _pokemonAbilities = value; }
        }

        private string _pokemonAbilities2;

        public string PokemonAbilities2
        {
            get { return _pokemonAbilities2; }
            set { _pokemonAbilities2 = value; }
        }

        private string _pokemonAbilities3;

        public string PokemonAbilities3
        {
            get { return _pokemonAbilities3; }
            set { _pokemonAbilities3 = value; }
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

            PokemonName = pokemonNameLang.Names[_language].Name;
            PokemonWeight = (pokemonInfo.Weight / 10).ToString("##.## 'Kg'");
            PokemonHeight = (pokemonInfo.Height / 10).ToString("#0.### 'm'");

            PokeImage = LoadPokemonImage(pokemonInfo);

            LoadPokemonType(pokemonInfo);

            LoadPokemonAbility(pokemonInfo);

            NotifyOfPropertyChange(() => PokeImage);

            NotifyOfPropertyChange(() => PokemonName);
            NotifyOfPropertyChange(() => PokemonWeight);
            NotifyOfPropertyChange(() => PokemonHeight);

            if (SelectedPokemon != null)
            {
                pokeClient.ClearResourceCache();
                pokeClient.ClearCache();
            }
        }

        private BitmapImage LoadPokemonImage(Pokemon pokemonInfo)
        {

            BitmapImage imageTemp = new BitmapImage();
            try
            {
                Uri source = new Uri(pokemonInfo.Sprites.FrontDefault, UriKind.Absolute);
                imageTemp.BeginInit();
                imageTemp.CacheOption = BitmapCacheOption.None;
                imageTemp.UriSource = source;
                imageTemp.EndInit();
            }
            catch
            {
                imageTemp = new BitmapImage(new Uri("https://www.softwort-engineering.com/downloads/pokemon/PicNA_Pokemon.png", UriKind.Absolute));
            }

            return imageTemp;
        }

        private async void LoadPokemonType(Pokemon pokemonInfo)
        {
            PokeApiNet.Type type;

            TypeOne = "";
            TypeOneBgBrush = Brushes.White;
            TypeTwo = "";
            TypeThree = "";

            for (int i = 0; i < pokemonInfo.Types.Count; i++)
            {
                if (p > 0)
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

            p = 1;

            try
            {
                type = await pokeClient.GetResourceAsync<PokeApiNet.Type>(PokemonTypeList[0].TypeId);
                TypeOne = type.Names[3].Name;
                TypeOneBgBrush = Brushes.DarkRed;

                if (PokemonTypeList.Count > 1)
                {
                    type = await pokeClient.GetResourceAsync<PokeApiNet.Type>(PokemonTypeList[1].TypeId);
                    TypeTwo = type.Names[3].Name;
                }

                if (PokemonTypeList.Count > 2)
                {
                    type = await pokeClient.GetResourceAsync<PokeApiNet.Type>(PokemonTypeList[2].TypeId);
                    TypeThree = type.Names[3].Name;
                }
            }
            catch
            {
                //Console.WriteLine("Page not found: 404");
            }

            NotifyOfPropertyChange(() => TypeOne);
            NotifyOfPropertyChange(() => TypeOneBgBrush);
            NotifyOfPropertyChange(() => TypeTwo);
            NotifyOfPropertyChange(() => TypeThree);
            PokemonTypeList.Clear();
            p = 0;
        }

        private async void LoadPokemonAbility(Pokemon pokemonInfo)
        {
            Ability ability;

            PokemonAbilities2 = "";
            PokemonAbilities3 = "";


            for (int i = 0; i < pokemonInfo.Abilities.Count; i++)
            {
                if (q > 0)
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

            q += 1;

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

            NotifyOfPropertyChange(() => PokemonAbilities);
            NotifyOfPropertyChange(() => PokemonAbilities2);
            NotifyOfPropertyChange(() => PokemonAbilities3);
            PokemonAbilityList.Clear();
            q = 0;
        }

        #endregion
    }
}
