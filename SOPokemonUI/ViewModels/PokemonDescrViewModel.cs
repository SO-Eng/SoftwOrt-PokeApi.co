using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using PokeApiNet;
using SOPokemonUI.Models;

namespace SOPokemonUI.ViewModels
{
    public class PokemonDescrViewModel : Screen
    {
        private readonly int _language;

        #region Fields

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

        public string DefensLanguage
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

        private List<DescriptionModel> _pokemonDescrList = new List<DescriptionModel>();

        public List<DescriptionModel> PokemonDescrList
        {
            get { return _pokemonDescrList; }
            set { _pokemonDescrList = value; }
        }

        #endregion



        #region Methods

        public PokemonDescrViewModel(int language, PokemonModel selectedPokemon)
        {
            _language = language;
            SelectedPokemon = selectedPokemon;
            LoadPokemonDescription();
        }

        private async  void LoadPokemonDescription()
        {
            Pokemon pokemonInfo = await pokeClient.GetResourceAsync<Pokemon>(SelectedPokemon.PokeNameOriginal);

            LoadStatValues(pokemonInfo);

            LoadStatLanguages(pokemonInfo);
        }

        private async void LoadStatLanguages(Pokemon pokemonInfo)
        {
            for (int i = 0; i < pokemonInfo.Stats.Count; i++)
            {
                PokemonDescrList.Add(new DescriptionModel { StatId = i + 1 } );
            }

            for (int i = pokemonInfo.Stats.Count; i < 0; i--)
            {
                PokemonDescrList.Add(new DescriptionModel{ StatUrl = pokemonInfo.Stats[i].Stat.Url });
            }

            Stat stat = await pokeClient.GetResourceAsync<Stat>(1);

            HpLanguage = stat.Names[2].Name;

            NotifyOfPropertyChange(() => HpLanguage);
            PokemonDescrList.Clear();
        }

        private void LoadStatValues(Pokemon pokemonInfo)
        {
            HpValue = pokemonInfo.Stats[5].BaseStat;
            ApValue = pokemonInfo.Stats[4].BaseStat;
            DefenseValue = pokemonInfo.Stats[3].BaseStat;
            ApSpecialValue = pokemonInfo.Stats[2].BaseStat;
            DefenseSpecialValue = pokemonInfo.Stats[1].BaseStat;
            SpeedValue = pokemonInfo.Stats[0].BaseStat;

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
