using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using PokeApiNet;
using SOPokemonUI.EventModels;
using SOPokemonUI.Helpers;
using SOPokemonUI.LanguagePack;
using SOPokemonUI.Models;

namespace SOPokemonUI.ViewModels
{
    public class PokemonEvoViewModel : Screen
    {

        #region Fields

        // Initiate client
        PokeApiClient pokeClient = new PokeApiClient();

        private LoadPokemonPic getPokemonPic;
        private readonly string _language;
        private BindableCollection<PokemonModel> _pokeList;
        private readonly IEventAggregator _events;
        private int evoId = 1;
        private int previous = 1;
        private int next = 0;

        private List<EvolutionModel> _evoOneList = new List<EvolutionModel>();

        public List<EvolutionModel> EvoOneList
        {
            get { return _evoOneList; }
            set
            {
                _evoOneList = value;
            }
        }


        public PokemonModel SelectedPokemon { get; set; }

        private string _evolutionHeader;

        public string EvolutionHeader
        {
            get { return _evolutionHeader; }
            set { _evolutionHeader = value; }
        }

        private string _basisHeader;

        public string BasisHeader
        {
            get { return _basisHeader; }
            set { _basisHeader = value; }
        }

        private string _pokemonBasisName;

        public string PokemonBasisName
        {
            get { return _pokemonBasisName; }
            set { _pokemonBasisName = value; }
        }

        private string _evoOneHeader;

        public string EvoOneHeader
        {
            get { return _evoOneHeader; }
            set { _evoOneHeader = value; }
        }

        private string _pokemonEvoOneName;

        public string PokemonEvoOneName
        {
            get { return _pokemonEvoOneName; }
            set { _pokemonEvoOneName = value; }
        }

        private string _evoTwoHeader;

        public string EvoTwoHeader
        {
            get { return _evoTwoHeader; }
            set { _evoTwoHeader = value; }
        }

        private string _pokemonEvoTwoName;

        public string PokemonEvoTwoName
        {
            get { return _pokemonEvoTwoName; }
            set { _pokemonEvoTwoName = value; }
        }

        // Pictures of evolutionable Pokemons
        private BitmapImage _pokeImageBasis;

        public BitmapImage PokeImageBasis
        {
            get { return _pokeImageBasis; }
            set { _pokeImageBasis = value; }
        }

        private BitmapImage _pokeImageEvoOne;

        public BitmapImage PokeImageEvoOne
        {
            get { return _pokeImageEvoOne; }
            set { _pokeImageEvoOne = value; }
        }

        private BitmapImage _pokeImageEvoTwo;

        public BitmapImage PokeImageEvoTwo
        {
            get { return _pokeImageEvoTwo; }
            set { _pokeImageEvoTwo = value; }
        }

        // Clickable Stackpanel to select Pokemon
        private StackPanel _evoStackPanelOne;

        public StackPanel EvoStackPanelOne
        {
            get { return _evoStackPanelOne; }
            set { _evoStackPanelOne = value; }
        }

        private StackPanel _evoStackPanelTwo;

        public StackPanel EvoStackPanelTwo
        {
            get { return _evoStackPanelTwo; }
            set { _evoStackPanelTwo = value; }
        }

        private StackPanel _evoStackPanelThree;

        public StackPanel EvoStackPanelThree
        {
            get { return _evoStackPanelThree; }
            set { _evoStackPanelThree = value; }
        }


        // Backgroundcolor for same Pokemon in Evolutions as Selected one
        private RadialGradientBrush _stackPanelEvoOneBg = new RadialGradientBrush();

        public RadialGradientBrush StackPanelEvoOneBg
        {
            get { return _stackPanelEvoOneBg; }
            set { _stackPanelEvoOneBg = value; }
        }

        private RadialGradientBrush _stackPanelEvoTwoBg = new RadialGradientBrush();

        public RadialGradientBrush StackPanelEvoTwoBg
        {
            get { return _stackPanelEvoTwoBg; }
            set { _stackPanelEvoTwoBg = value; }
        }

        private RadialGradientBrush _stackPanelEvoThreeBg = new RadialGradientBrush();

        public RadialGradientBrush StackPanelEvoThreeBg
        {
            get { return _stackPanelEvoThreeBg; }
            set { _stackPanelEvoThreeBg = value; }
        }

        // Make Previous- and NextButton visible
        public bool IsPreviousVisible
        {
            get
            {
                bool output = false;

                if (EvoOneList?.Count > 1)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool IsNextVisible
        {
            get
            {
                bool output = false;

                if (EvoOneList?.Count > 1)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanPreviousButton
        {
            get
            {
                bool output = false;

                if (next != 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanNextButton
        {
            get
            {
                bool output = false;

                if (next <= EvoOneList?.Count - 2)
                {
                    output = true;
                }

                return output;
            }
        }


        #endregion


        #region Methods


        public PokemonEvoViewModel(string language, PokemonModel selectedPokemon, BindableCollection<PokemonModel> pokeList, IEventAggregator events)
        {
            _language = language;
            _pokeList = pokeList;
            _events = events;
            SelectedPokemon = selectedPokemon;

            EvolutionHeader = EvolutionLanguage.GetEvolutionHeader(_language);

            LoadPokemonEvolutions();
        }

        private async void LoadPokemonEvolutions()
        {
            PokemonSpecies pokemonSpecies = await pokeClient.GetResourceAsync<PokemonSpecies>(SelectedPokemon.Id);

            SetEvolutionId(pokemonSpecies);
            LoadPokemonEvoInfos();
        }

        private void SetEvolutionId(PokemonSpecies pokemonSpecies)
        {
            evoId = Int32.Parse(pokemonSpecies.EvolutionChain.Url.Where(Char.IsDigit).ToArray());

            if (evoId < 30)
            {
                evoId -= 20;
            }
            else if (evoId < 999)
            {
                evoId -= 200;
            }
            else
            {
                evoId -= 2000;
            }
        }

        private async void LoadPokemonEvoInfos()
        {
            Pokemon pokemonInfo;
            EvolutionChain evoChain = await pokeClient.GetResourceAsync<EvolutionChain>(evoId);
            EvoOneList.Clear();

            PokeImageBasis = null;
            PokeImageEvoOne = null;
            PokeImageEvoTwo = null;

            try
            {
                BasisHeader = EvolutionLanguage.GetBasisHeader(_language);
                PokemonSpecies pokemonBasis = await pokeClient.GetResourceAsync<PokemonSpecies>(evoChain.Chain.Species.Name);
                var tempBasis = _pokeList.First(x => x.PokeNameOriginal == pokemonBasis.Name).PokeName;
                PokemonBasisName = tempBasis;
                pokemonInfo = await pokeClient.GetResourceAsync<Pokemon>(pokemonBasis.Id);
                PokeImageBasis = await LoadPokemonEvoPic(pokemonInfo);
            }
            catch
            {
                PokemonBasisName = "";
            }

            try
            {
                // Some Pokemons have more then one first evolution!
                // So we load allways in a List
                for (int i = 0; i < evoChain.Chain.EvolvesTo.Count; i++)
                {
                    PokemonSpecies pokemonEvoOne = await pokeClient.GetResourceAsync<PokemonSpecies>(evoChain.Chain.EvolvesTo[i].Species.Name);
                    var tempEvoOne = _pokeList.First(x => x.PokeNameOriginal == pokemonEvoOne.Name).PokeName;
                    PokemonEvoOneName = tempEvoOne;
                    pokemonInfo = await pokeClient.GetResourceAsync<Pokemon>(pokemonEvoOne.Id);
                    PokeImageEvoOne = await LoadPokemonEvoPic(pokemonInfo);

                    EvoOneList.Add(new EvolutionModel
                        {
                            Name = PokemonEvoOneName,
                            EvoImage = PokeImageEvoOne
                        }
                    );
                }

                if (EvoOneList.Count > 0)
                {
                    EvoOneHeader = EvolutionLanguage.GetEvoOneHeader(_language);
                }
            }
            catch
            {
                PokemonEvoOneName = "";
            }

            try
            {
                PokemonSpecies pokemonEvoTwo = await pokeClient.GetResourceAsync<PokemonSpecies>(evoChain.Chain.EvolvesTo[0].EvolvesTo[0].Species.Name);
                var tempEvoTwo = _pokeList.First(x => x.PokeNameOriginal == pokemonEvoTwo.Name).PokeName;
                EvoTwoHeader = EvolutionLanguage.GetEvoTwoHeader(_language);
                PokemonEvoTwoName = tempEvoTwo;
                pokemonInfo = await pokeClient.GetResourceAsync<Pokemon>(pokemonEvoTwo.Id);
                PokeImageEvoTwo = await LoadPokemonEvoPic(pokemonInfo);
            }
            catch
            {
                PokemonEvoTwoName = "";
            }

            NotifyOfPropertyChange(() => BasisHeader);
            NotifyOfPropertyChange(() => PokemonBasisName);
            NotifyOfPropertyChange(() => PokeImageBasis);

            NotifyOfPropertyChange(() => EvoOneHeader);

            NotifyOfPropertyChange(() => EvoTwoHeader);
            NotifyOfPropertyChange(() => PokemonEvoTwoName);
            NotifyOfPropertyChange(() => PokeImageEvoTwo);

            ShowEvoOne();

            CompareSelectedPokemon();
        }

        private void ShowEvoOne()
        {
            try
            {
                PokemonEvoOneName = EvoOneList[0].Name;
                PokeImageEvoOne = EvoOneList[0].EvoImage;

                if (EvoOneList.Count > 1)
                {
                    NotifyOfPropertyChange(() => IsPreviousVisible);
                    NotifyOfPropertyChange(() => IsNextVisible);
                    NotifyOfPropertyChange(() => CanPreviousButton);
                    NotifyOfPropertyChange(() => CanNextButton);
                }

            }
            catch
            {
                PokemonEvoOneName = "";
            }

            NotifyOfPropertyChange(() => PokemonEvoOneName);
            NotifyOfPropertyChange(() => PokeImageEvoOne);
        }


        public async Task NextButton()
        {
            next += 1;

            if (next >= EvoOneList.Count)
            {
                next = EvoOneList.Count - 1;
                return;
            }

            PokemonEvoOneName = EvoOneList[next].Name;
            PokeImageEvoOne = EvoOneList[next].EvoImage;

            CompareSelectedPokemon();

            NotifyOfPropertyChange(() => PokemonEvoOneName);
            NotifyOfPropertyChange(() => PokeImageEvoOne);
            NotifyOfPropertyChange(() => CanPreviousButton);
            NotifyOfPropertyChange(() => CanNextButton);
        }

        public async Task PreviousButton()
        {
            next -= 1;

            if (next < 0)
            {
                next = 0;
                return;
            }


            PokemonEvoOneName = EvoOneList[next].Name;
            PokeImageEvoOne = EvoOneList[next].EvoImage;

            CompareSelectedPokemon();

            NotifyOfPropertyChange(() => PokemonEvoOneName);
            NotifyOfPropertyChange(() => PokeImageEvoOne);
            NotifyOfPropertyChange(() => CanPreviousButton);
            NotifyOfPropertyChange(() => CanNextButton);
        }


        private async Task<BitmapImage> LoadPokemonEvoPic(Pokemon pokemonInfo)
        {
            getPokemonPic = new LoadPokemonPic(pokemonInfo.Sprites.FrontDefault, _language);
            PokemonImageModel pIM = await getPokemonPic.LoadPokemonImage();

            return pIM.PokemonImage;
        }


        private void CompareSelectedPokemon()
        {
            StackPanelEvoOneBg = null;
            StackPanelEvoTwoBg = null;
            StackPanelEvoThreeBg = null;

            if (SelectedPokemon.PokeName == PokemonBasisName)
            {
                StackPanelEvoOneBg = GenerateRadialColorShape();
            }
            else if (SelectedPokemon.PokeName == PokemonEvoOneName)
            {
                StackPanelEvoTwoBg = GenerateRadialColorShape();
            }
            else if (SelectedPokemon.PokeName == PokemonEvoTwoName)
            {
                StackPanelEvoThreeBg = GenerateRadialColorShape();
            }

            NotifyOfPropertyChange(() => StackPanelEvoOneBg);
            NotifyOfPropertyChange(() => StackPanelEvoTwoBg);
            NotifyOfPropertyChange(() => StackPanelEvoThreeBg);
        }

        private RadialGradientBrush GenerateRadialColorShape()
        {
            RadialGradientBrush selectedStackPanel = new RadialGradientBrush();

            selectedStackPanel.GradientOrigin = new Point(0.5, 0.5);
            selectedStackPanel.Center = new Point(0.5, 0.5);
            selectedStackPanel.RadiusX = 0.5;
            selectedStackPanel.RadiusY = 0.5;

            selectedStackPanel.GradientStops.Add(new GradientStop(Colors.LightCoral, 0.0));
            selectedStackPanel.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 231), 0.4));
            selectedStackPanel.GradientStops.Add(new GradientStop(Colors.LightCoral, 0.7));

            selectedStackPanel.Freeze();

            return selectedStackPanel;
        }

        public async Task SelectBasisPokemon()
        {
            if (SelectedPokemon.PokeName != PokemonBasisName)
            {
                for (int i = 1; i < _pokeList.Count; i++)
                {
                    if (_pokeList[i].PokeName == PokemonBasisName)
                    {
                        SelectedPokemon = _pokeList[i];
                        break;
                    }
                }

                await _events.PublishOnUIThreadAsync(new EvoPokemonEvent { SelectedEvo = SelectedPokemon }, CancellationToken.None);
            }
        }

        public async Task SelectEvoOnePokemon()
        {
            if (SelectedPokemon.PokeName != PokemonEvoOneName)
            {
                for (int i = 1; i < _pokeList.Count; i++)
                {
                    if (_pokeList[i].PokeName == PokemonEvoOneName)
                    {
                        SelectedPokemon = _pokeList[i];
                        break;
                    }
                }

                await _events.PublishOnUIThreadAsync(new EvoPokemonEvent { SelectedEvo = SelectedPokemon }, CancellationToken.None);
            }
        }

        public async Task SelectEvoTwoPokemon()
        {
            if (SelectedPokemon.PokeName != PokemonEvoTwoName && PokemonEvoTwoName != "")
            {
                for (int i = 1; i < _pokeList.Count; i++)
                {
                    if (_pokeList[i].PokeName == PokemonEvoTwoName)
                    {
                        SelectedPokemon = _pokeList[i];
                        break;
                    }
                }

                await _events.PublishOnUIThreadAsync(new EvoPokemonEvent { SelectedEvo = SelectedPokemon }, CancellationToken.None);
            }
        }

        #endregion
    }
}
