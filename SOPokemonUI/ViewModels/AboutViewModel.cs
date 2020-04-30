using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using SOPokemonUI.Helpers;
using SOPokemonUI.LanguagePack;
using SOPokemonUI.Models;

namespace SOPokemonUI.ViewModels
{
    public class AboutViewModel : Screen
    {
        #region Fields

        public string Language { get; set; }
        private LoadPokemonPic getAboutPics;

        private BitmapImage _soLogo;

        public BitmapImage SoLogo
        {
            get { return _soLogo; }
            set { _soLogo = value; }
        }

        private BitmapImage _pokeLogo;

        public BitmapImage PokeLogo
        {
            get { return _pokeLogo; }
            set { _pokeLogo = value; }
        }


        private string _textOne;

        public string TextOne
        {
            get { return _textOne; }
            set
            {
                _textOne = value;
                NotifyOfPropertyChange(() => TextOne);
            }
        }

        private string _textTwo;

        public string TextTwo
        {
            get { return _textTwo; }
            set
            {
                _textTwo = value;
                NotifyOfPropertyChange(() => TextTwo);
            }
        }

        private string _textThree;

        public string TextThree
        {
            get { return _textThree; }
            set
            {
                _textThree = value;
                NotifyOfPropertyChange(() => TextThree);
            }
        }

        private string _textFore;

        public string TextFour
        {
            get { return _textFore; }
            set
            {
                _textFore = value;
                NotifyOfPropertyChange(() => TextFour);
            }
        }

        private string _textFive;

        public string TextFive
        {
            get { return _textFive; }
            set
            {
                _textFive = value;
                NotifyOfPropertyChange(() => TextFive);
            }
        }

        private string _textSix;

        public string TextSix
        {
            get { return _textSix; }
            set
            {
                _textSix = value;
                NotifyOfPropertyChange(() => TextSix);
            }
        }

        private string _textSeven;

        public string TextSeven
        {
            get { return _textSeven; }
            set
            {
                _textSeven = value;
                NotifyOfPropertyChange(() => TextSeven);
            }
        }

        #endregion



        #region Methods

        public AboutViewModel()
        {
            LoadPictures();
        }

        public void GetLanguage(string language)
        {
            Language = language;
            LoadTextes();
        }

        // Load pictures in header
        private async Task LoadPictures()
        {
            string soLogo = "https://raw.githubusercontent.com/SO-Eng/SoftwOrt-PokeApi.co/master/SOPokemonUI/Logo/SO-Logo.png";
            string pokeLogo = "https://raw.githubusercontent.com/SO-Eng/SoftwOrt-PokeApi.co/master/SOPokemonUI/Logo/SOPokedex.ico";

            getAboutPics = new LoadPokemonPic(soLogo, Language);
            PokemonImageModel pIMsO = await getAboutPics.LoadPokemonImage();
            SoLogo = pIMsO.PokemonImage;

            getAboutPics = new LoadPokemonPic(pokeLogo, Language);
            PokemonImageModel pIMpO = await getAboutPics.LoadPokemonImage();
            PokeLogo = pIMpO.PokemonImage;

            NotifyOfPropertyChange(() => SoLogo);
            NotifyOfPropertyChange(() => PokeLogo);
        }

        // Load Textes depending to selected language
        private void LoadTextes()
        {
            TextOne = AboutLanguages.TextOne(Language);
            TextTwo = AboutLanguages.TextTwo(Language);
            TextThree = AboutLanguages.TextThree(Language);
            TextFour = AboutLanguages.TextFour(Language);
            TextFive = AboutLanguages.TextFive(Language);
            TextSix = AboutLanguages.TextSix(Language);
            TextSeven = AboutLanguages.TextSeven(Language);
        }

        // Methods for Hyperlinks in .Net Core 3
        public void PokeApiLink()
        {
            try
            {
                string url = "https://pokeapi.co/";
                var psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch
            {
                //
            }
        }

        public void CaliburnMicroLink()
        {
            try
            {
                string url = "https://caliburnmicro.com/";
                var psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch
            {
                //
            }
        }

        public void PoroCYonLink()
        {
            try
            {
                string url = "https://gitlab.com/PoroCYon/PokeApi.NET";
                var psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch
            {
                //
            }
        }

        public void SoPokeApiLink()
        {
            try
            {
                string url = "https://github.com/SO-Eng/SoftwOrt-PokeApi.co";
                var psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch
            {
                //
            }
        }
        #endregion
    }
}
