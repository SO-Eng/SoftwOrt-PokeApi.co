using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using PokeApiNet;
using SOPokemonUI.Models;

namespace SOPokemonUI.ViewModels
{
    public class PokemonDescrViewModel : Screen
    {

        #region Fields
        private readonly string _language;
        private int q = 0;

        // Initiate client
        PokeApiClient pokeClient = new PokeApiClient();

        private PokemonModel _selectedPokemon;

        public PokemonModel SelectedPokemon
        {
            get { return _selectedPokemon; }
            set { _selectedPokemon = value; }
        }

        public string HpLanguage
        {
            get { return _hpLanguage; }
            set { _hpLanguage = value; }
        }
        private int _apValue;

        private int _hpValue;

        public int HpValue
        {
            get { return _hpValue; }
            set { _hpValue = value; }
        }
        private string _hpLanguage;

        private string _apLanguage;

        public string ApLanguage
        {
            get { return _apLanguage; }
            set { _apLanguage = value; }
        }

        public int ApValue
        {
            get { return _apValue; }
            set { _apValue = value; }
        }


        private string _defenseLanguage;

        public string DefenseLanguage
        {
            get { return _defenseLanguage; }
            set { _defenseLanguage = value; }
        }
        private int _defenseValue;

        public int DefenseValue
        {
            get { return _defenseValue; }
            set { _defenseValue = value; }
        }


        private string _apSpecialLanguage;

        public string ApSpecialLanguage
        {
            get { return _apSpecialLanguage; }
            set { _apSpecialLanguage = value; }
        }

        private int _apSpecialValue;

        public int ApSpecialValue
        {
            get { return _apSpecialValue; }
            set { _apSpecialValue = value; }
        }


        private string _defenseSpecialLanguage;

        public string DefenseSpecialLanguage
        {
            get { return _defenseSpecialLanguage; }
            set { _defenseSpecialLanguage = value; }
        }

        private int _defenseSpecialValue;

        public int DefenseSpecialValue
        {
            get { return _defenseSpecialValue; }
            set { _defenseSpecialValue = value; }
        }


        private string _speedLanguage;

        public string SpeedLanguage
        {
            get { return _speedLanguage; }
            set { _speedLanguage = value; }
        }

        private int _speedValue;

        public int SpeedValue
        {
            get { return _speedValue; }
            set { _speedValue = value; }
        }


        private string _pokemonDescription;
        
        public string PokemonDescription
        {
            get { return _pokemonDescription; }
            set { _pokemonDescription = value; }
        }

        private string _abilityFlavorOne;

        public string AbilityFlavorOne
        {
            get { return _abilityFlavorOne; }
            set { _abilityFlavorOne = value; }
        }

        private string _abilityFlavorTwo;

        public string AbilityFlavorTwo
        {
            get { return _abilityFlavorTwo; }
            set { _abilityFlavorTwo = value; }
        }

        private string _abilityFlavorThree;

        public string AbilityFlavorThree
        {
            get { return _abilityFlavorThree; }
            set { _abilityFlavorThree = value; }
        }


        private List<AbilityModel> _pokemonAbilityList = new List<AbilityModel>();
        public List<AbilityModel> PokemonAbilityList
        {
            get { return _pokemonAbilityList; }
            set { _pokemonAbilityList = value; }
        }

        #endregion



        #region Methods

        public PokemonDescrViewModel(string language, PokemonModel selectedPokemon)
        {
            _language = language;
            SelectedPokemon = selectedPokemon;
            LoadPokemonDescription();
        }

        private async  void LoadPokemonDescription()
        {
            Pokemon pokemonInfo = await pokeClient.GetResourceAsync<Pokemon>(SelectedPokemon.PokeNameOriginal);

            LoadPokemonFlavorText();

            LoadStatLanguages();

            LoadStatValues(pokemonInfo);

            LoadAbilityFlavorText(pokemonInfo);

        }

        private async void LoadAbilityFlavorText(Pokemon pokemonInfo)
        {
            Ability abilityFlavorText;

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
                    abilityFlavorText = await pokeClient.GetResourceAsync<Ability>(PokemonAbilityList[i].AbilityId);
                    if (AbilityFlavorOne == null)
                    {
                        for (int j = 0; i < abilityFlavorText.FlavorTextEntries.Count; j++)
                        {
                            if (abilityFlavorText.FlavorTextEntries[j].Language.Name == _language)
                            {
                                AbilityFlavorOne = abilityFlavorText.FlavorTextEntries[j].FlavorText;
                                AbilityFlavorOne = String.Join("", AbilityFlavorOne.Where(c => c != '\n' && c != '\r' && c != '\t'));
                                break;
                            }
                        }
                    }

                    if (AbilityFlavorTwo == null && i == 1)
                    {
                        for (int k = 0; k < abilityFlavorText.FlavorTextEntries.Count; k++)
                        {
                            if (abilityFlavorText.FlavorTextEntries[k].Language.Name == _language)
                            {
                                AbilityFlavorTwo = abilityFlavorText.FlavorTextEntries[k].FlavorText;
                                AbilityFlavorTwo = String.Join("", AbilityFlavorTwo.Where(c => c != '\n' && c != '\r' && c != '\t'));
                                break;
                            }
                        }
                    }

                    if (AbilityFlavorThree == null && i == 2)
                    {
                        for (int l = 0; l < abilityFlavorText.FlavorTextEntries.Count; l++)
                        {
                            if (abilityFlavorText.FlavorTextEntries[l].Language.Name == _language)
                            {
                                AbilityFlavorThree = abilityFlavorText.FlavorTextEntries[l].FlavorText;
                                AbilityFlavorThree = String.Join("", AbilityFlavorThree.Where(c => c != '\n' && c != '\r' && c != '\t'));
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

            NotifyOfPropertyChange(() => AbilityFlavorOne);
            NotifyOfPropertyChange(() => AbilityFlavorTwo);
            NotifyOfPropertyChange(() => AbilityFlavorThree);
            PokemonAbilityList.Clear();
            q = 0;
        }

        private async void LoadPokemonFlavorText()
        {
            PokemonSpecies flavorText = await pokeClient.GetResourceAsync<PokemonSpecies>(SelectedPokemon.Id);

            try
            {
                for (int i = 0; i < flavorText.FlavorTextEntries.Count; i++)
                {
                    if (flavorText.FlavorTextEntries[i].Language.Name == _language)
                    {
                        PokemonDescription = flavorText.FlavorTextEntries[i].FlavorText;
                        PokemonDescription = String.Join("", PokemonDescription.Where(c => c != '\n' && c != '\r' && c != '\t'));
                        break;
                    }
                }
            }
            catch
            {
                //
            }
            
            NotifyOfPropertyChange(() => PokemonDescription);
        }

        private async void LoadStatLanguages()
        {
            Stat stat;

            try
            {
                stat = await pokeClient.GetResourceAsync<Stat>(1);
                for (int i = 0; i < stat.Names.Count; i++)
                {
                    if (stat.Names[i].Language.Name == _language)
                    {
                        HpLanguage = stat.Names[i].Name;
                        break;
                    }
                    HpLanguage = stat.Names[5].Name;
                }

                stat = await pokeClient.GetResourceAsync<Stat>(2);
                for (int i = 0; i < stat.Names.Count; i++)
                {
                    if (stat.Names[i].Language.Name == _language)
                    {
                        ApLanguage = stat.Names[i].Name;
                        break;
                    }
                    ApLanguage = stat.Names[5].Name;
                }

                stat = await pokeClient.GetResourceAsync<Stat>(3);
                for (int i = 0; i < stat.Names.Count; i++)
                {
                    if (stat.Names[i].Language.Name == _language)
                    {
                        DefenseLanguage = stat.Names[i].Name;
                        break;
                    }
                    DefenseLanguage = stat.Names[5].Name;
                }

                stat = await pokeClient.GetResourceAsync<Stat>(4);
                for (int i = 0; i < stat.Names.Count; i++)
                {
                    if (stat.Names[i].Language.Name == _language)
                    {
                        ApSpecialLanguage = stat.Names[i].Name;
                        break;
                    }
                    ApSpecialLanguage = stat.Names[5].Name;
                }

                stat = await pokeClient.GetResourceAsync<Stat>(5);
                for (int i = 0; i < stat.Names.Count; i++)
                {
                    if (stat.Names[i].Language.Name == _language)
                    {
                        DefenseSpecialLanguage = stat.Names[i].Name;
                        break;
                    }
                    DefenseSpecialLanguage = stat.Names[5].Name;
                }

                stat = await pokeClient.GetResourceAsync<Stat>(6);
                for (int i = 0; i < stat.Names.Count; i++)
                {
                    if (stat.Names[i].Language.Name == _language)
                    {
                        SpeedLanguage = stat.Names[i].Name;
                        break;
                    }
                    SpeedLanguage = stat.Names[5].Name;
                }
            }
            catch
            {
                //
            }

            NotifyOfPropertyChange(() => HpLanguage);
            NotifyOfPropertyChange(() => ApLanguage);
            NotifyOfPropertyChange(() => DefenseLanguage);
            NotifyOfPropertyChange(() => ApSpecialLanguage);
            NotifyOfPropertyChange(() => DefenseSpecialLanguage);
            NotifyOfPropertyChange(() => SpeedLanguage);
        }

        private void LoadStatValues(Pokemon pokemonInfo)
        {
            try
            {
                HpValue = pokemonInfo.Stats[5].BaseStat;
                ApValue = pokemonInfo.Stats[4].BaseStat;
                DefenseValue = pokemonInfo.Stats[3].BaseStat;
                ApSpecialValue = pokemonInfo.Stats[2].BaseStat;
                DefenseSpecialValue = pokemonInfo.Stats[1].BaseStat;
                SpeedValue = pokemonInfo.Stats[0].BaseStat;
            }
            catch
            {
                //
            }

            NotifyOfPropertyChange(() => HpValue);
            NotifyOfPropertyChange(() => ApValue);
            NotifyOfPropertyChange(() => DefenseValue);
            NotifyOfPropertyChange(() => ApSpecialValue);
            NotifyOfPropertyChange(() => DefenseSpecialValue);
            NotifyOfPropertyChange(() => SpeedValue);
        }

        #endregion
    }
}
