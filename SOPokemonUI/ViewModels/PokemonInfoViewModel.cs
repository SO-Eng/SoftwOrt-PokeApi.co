using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using PokeApiNet;
using SOPokemonUI.Helpers;
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
        readonly PokeApiClient pokeClient = new PokeApiClient();

        private LoadPokemonPic getPokemonPic;
        private readonly string _language;

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


        private string _pokemonWeightLanguage;

        public string PokemonWeightLanguage
        {
            get { return _pokemonWeightLanguage; }
            set { _pokemonWeightLanguage = value; }
        }

        private string _pokemonWeight;

        public string PokemonWeight
        {
            get { return _pokemonWeight; }
            set { _pokemonWeight = value; }
        }

        private string _pokemonHeightLanguage;

        public string PokemonHeightLanguage
        {
            get { return _pokemonHeightLanguage; }
            set { _pokemonHeightLanguage = value; }
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

        public bool IsTypeDeclared
        {
            get
            {
                bool output = true;

                if (TypeOne?.Length > 0)
                {
                    output = false;
                }

                return output;
            }
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

        private Brush _typeTwoBrBrush;

        public Brush TypeTwoBgBrush
        {
            get { return _typeTwoBrBrush; }
            set { _typeTwoBrBrush = value; }
        }

        private string _typeThree;

        public string TypeThree
        {
            get { return _typeThree; }
            set
            {
                _typeThree = value;

            }
        }

        private Brush _typeThreeBgBrush;

        public Brush TypeThreeBgBrush
        {
            get { return _typeThreeBgBrush; }
            set { _typeThreeBgBrush = value; }
        }

        private List<AbilityModel> _pokemonAbilityList = new List<AbilityModel>();

        public List<AbilityModel> PokemonAbilityList
        {
            get { return _pokemonAbilityList; }
            set { _pokemonAbilityList = value; }
        }


        private string _pokemonAbilityLanguage;

        public string PokemonAbilityLanguage
        {
            get { return _pokemonAbilityLanguage; }
            set { _pokemonAbilityLanguage = value; }
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

        public PokemonInfoViewModel(string language,PokemonModel selectedPokemon)
        {
            _language = language;
            SelectedPokemon = selectedPokemon;
            LoadPokemonInfo();
        }

        public async void LoadPokemonInfo()
        {
            PokemonSpecies pokemonNameLang = await pokeClient.GetResourceAsync<PokemonSpecies>(SelectedPokemon.Id);
            Pokemon pokemonInfo = await pokeClient.GetResourceAsync<Pokemon>(SelectedPokemon.PokeNameOriginal);

            // Banner on top: Name of Pokemon in selected language
            for (int i = 0; i < pokemonNameLang.Names.Count; i++)
            {
                if (pokemonNameLang.Names[i].Language.Name == _language)
                {
                    PokemonName = pokemonNameLang.Names[i].Name;
                }
            }

            await LoadPokemonImage(pokemonInfo);

            LoadPokemonType(pokemonInfo);

            LoadPokemonStatLanguage(pokemonInfo);

            LoadPokemonAbility(pokemonInfo);

            NotifyOfPropertyChange(() => PokemonName);

            if (SelectedPokemon != null)
            {
                pokeClient.ClearResourceCache();
                pokeClient.ClearCache();
            }
        }

        private void LoadPokemonStatLanguage(Pokemon pokemonInfo)
        {
            PokemonWeightLanguage = AbilitiesLanguage.GetWeightLanguage(_language);
            PokemonHeightLanguage = AbilitiesLanguage.GetHeightLanguage(_language);
            PokemonAbilityLanguage = AbilitiesLanguage.GetAbilityLanguage(_language);
            PokemonWeight = (pokemonInfo.Weight / 10).ToString("#0.## 'Kg'");
            PokemonHeight = (pokemonInfo.Height / 10).ToString("#0.### 'm'");

            NotifyOfPropertyChange(() => PokemonWeightLanguage);
            NotifyOfPropertyChange(() => PokemonHeightLanguage);
            NotifyOfPropertyChange(() => PokemonAbilityLanguage);
            NotifyOfPropertyChange(() => PokemonWeight);
            NotifyOfPropertyChange(() => PokemonHeight);
        }

        // Method to load image of selected pokemon
        private async Task LoadPokemonImage(Pokemon pokemonInfo)
        {
            getPokemonPic = new LoadPokemonPic(pokemonInfo.Sprites.FrontDefault);
            PokemonImageModel pIM = await getPokemonPic.LoadPokemonImage();
            PokeImage = pIM.PokemonImage;

            NotifyOfPropertyChange(() => PokeImage);
        }

        private void LoadPokemonType(Pokemon pokemonInfo)
        {
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

            switch (PokemonTypeList.Count)
            {
                case 1:
                    TypeTextboxOne();
                    break;
                case 2:
                    TypeTextboxTwo();
                    break;
                default:
                    TypeTextboxThree();
                    break;
            }

            p = 0;
        }

        private async void TypeTextboxOne()
        {
            Type type;
            try
            {
                for (int i = 0; i < PokemonTypeList.Count; i++)
                {
                    type = await pokeClient.GetResourceAsync<Type>(PokemonTypeList[i].TypeId);
                    if (TypeOne == null)
                    {
                        for (int j = 0; j < type.Names.Count; j++)
                        {
                            if (type.Names[j].Language.Name == _language)
                            {
                                TypeThree = type.Names[j].Name;
                                TypeThreeBgBrush = SetTypeColor(type);
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                //
            }

            NotifyOfPropertyChange(() => IsTypeDeclared);
            NotifyOfPropertyChange(() => TypeThree);
            NotifyOfPropertyChange(() => TypeThreeBgBrush);
            PokemonTypeList.Clear();
        }

        private async void TypeTextboxTwo()
        {
            Type type;

            try
            {
                for (int i = 0; i < PokemonTypeList.Count; i++)
                {
                    type = await pokeClient.GetResourceAsync<Type>(PokemonTypeList[i].TypeId);
                    if (TypeOne == null)
                    {
                        for (int j = 0; j < type.Names.Count; j++)
                        {
                            if (type.Names[j].Language.Name == _language)
                            {
                                TypeOne = type.Names[j].Name;
                                TypeOneBgBrush = SetTypeColor(type);
                                break;
                            }
                        }
                    }
                    if (TypeTwo == null && i == 1)
                    {
                        for (int j = 0; j < type.Names.Count; j++)
                        {
                            if (type.Names[j].Language.Name == _language)
                            {
                                TypeTwo = type.Names[j].Name;
                                TypeTwoBgBrush = SetTypeColor(type);
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                //
            }

            NotifyOfPropertyChange(() => IsTypeDeclared);
            NotifyOfPropertyChange(() => TypeOne);
            NotifyOfPropertyChange(() => TypeOneBgBrush);
            NotifyOfPropertyChange(() => TypeTwo);
            NotifyOfPropertyChange(() => TypeTwoBgBrush);
            PokemonTypeList.Clear();
        }

        private async void TypeTextboxThree()
        {
            Type type;

            try
            {
                for (int i = 0; i < PokemonTypeList.Count; i++)
                {
                    type = await pokeClient.GetResourceAsync<Type>(PokemonTypeList[i].TypeId);
                    if (TypeOne == null)
                    {
                        for (int j = 0; j < type.Names.Count; j++)
                        {
                            if (type.Names[j].Language.Name == _language)
                            {
                                TypeOne = type.Names[j].Name;
                                TypeOneBgBrush = SetTypeColor(type);
                                break;
                            }
                        }
                    }
                    if (TypeTwo == null && i == 1)
                    {
                        for (int j = 0; j < type.Names.Count; j++)
                        {
                            if (type.Names[j].Language.Name == _language)
                            {
                                TypeTwo = type.Names[j].Name;
                                TypeTwoBgBrush = SetTypeColor(type);
                                break;
                            }
                        }
                    }
                    if (TypeThree == null && i == 2)
                    {
                        for (int j = 0; j < type.Names.Count; j++)
                        {
                            if (type.Names[j].Language.Name == _language)
                            {
                                TypeThree = type.Names[j].Name;
                                TypeThreeBgBrush = SetTypeColor(type);
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                //
            }

            NotifyOfPropertyChange(() => IsTypeDeclared);
            NotifyOfPropertyChange(() => TypeOne);
            NotifyOfPropertyChange(() => TypeOneBgBrush);
            NotifyOfPropertyChange(() => TypeTwo);
            NotifyOfPropertyChange(() => TypeTwoBgBrush);
            NotifyOfPropertyChange(() => TypeThree);
            NotifyOfPropertyChange(() => TypeThreeBgBrush);
            PokemonTypeList.Clear();
        }

        private Brush SetTypeColor(Type type)
        {
            return ColorDeclaration.FillTypeColorTextBoxes(type.Id);
        }

        private async void LoadPokemonAbility(Pokemon pokemonInfo)
        {
            Ability ability;

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
                for (int i = 0; i < PokemonAbilityList.Count; i++)
                {
                    ability = await pokeClient.GetResourceAsync<Ability>(PokemonAbilityList[i].AbilityId);
                    if (PokemonAbilities == null)
                    {
                        for (int j = 0; j < ability.Names.Count; j++)
                        {
                            if (ability.Names[j].Language.Name == _language)
                            {
                                PokemonAbilities = ability.Names[j].Name;
                                break;
                            }
                        }
                    }

                    if (PokemonAbilities2 == null && i == 1)
                    {
                        for (int j = 0; j < ability.Names.Count; j++)
                        {
                            if (ability.Names[j].Language.Name == _language)
                            {
                                PokemonAbilities2 = ability.Names[j].Name;
                                break;
                            }
                        }
                    }

                    if (PokemonAbilities3 == null && i == 2)
                    {
                        for (int j = 0; j < ability.Names.Count; j++)
                        {
                            if (ability.Names[j].Language.Name == _language)
                            {
                                PokemonAbilities3 = ability.Names[j].Name;
                            }
                        }
                    }
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
