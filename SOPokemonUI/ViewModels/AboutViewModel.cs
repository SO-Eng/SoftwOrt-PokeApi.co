using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using Caliburn.Micro;

namespace SOPokemonUI.ViewModels
{
    public class AboutViewModel : Screen
    {
        #region Fields

        public string Language { get; set; }

        private BitmapImage _soLogo;

        public BitmapImage SoLogo
        {
            get { return _soLogo; }
            set
            {
                _soLogo = value;
                NotifyOfPropertyChange(() => SoLogo);
            }
        }

        private BitmapImage _pokeLogo;

        public BitmapImage PokeLogo
        {
            get { return _pokeLogo; }
            set
            {
                _pokeLogo = value;
                NotifyOfPropertyChange(() => PokeLogo);
            }
        }

        #endregion



        #region Methods

        public AboutViewModel()
        {
            SoLogo = new BitmapImage(new Uri(@"../Logo/SO-Logo.png", UriKind.Relative));
            PokeLogo = new BitmapImage(new Uri(@"../Logo/SOPokedex.ico", UriKind.Relative));
        }

        public void GetLanguage(string language)
        {
            Language = language;
        }

        #endregion
    }
}
