using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using SOPokemonUI.EventModels;
using SOPokemonUI.Helpers;

namespace SOPokemonUI.ViewModels
{
    public class LogOnViewModel : Screen
    {

        #region Fields

        private readonly IEventAggregator _events;

        private bool selected = false;

        private string _language;

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }


        private bool _selJapanese;

        public bool SelJapanese
        {
            get { return _selJapanese; }
            set
            {
                _selJapanese = value;
                selected = true;
                Language = "ja-Hrkt";
                GetLoadingDescription();
                NotifyOfPropertyChange(() => Language);
                NotifyOfPropertyChange(() => LoadingDescription);
                NotifyOfPropertyChange(() => CanLogOn);
            }
        }

        private bool _selFrance;

        public bool SelFrance
        {
            get { return _selFrance; }
            set
            {
                _selFrance = value;
                selected = true;
                Language = "fr";
                GetLoadingDescription();
                NotifyOfPropertyChange(() => Language);
                NotifyOfPropertyChange(() => LoadingDescription);
                NotifyOfPropertyChange(() => CanLogOn);
            }
        }

        private bool _selSpanish;

        public bool SelSpanish
        {
            get { return _selSpanish; }
            set
            {
                _selSpanish = value;
                selected = true;
                Language = "es";
                GetLoadingDescription();
                NotifyOfPropertyChange(() => Language);
                NotifyOfPropertyChange(() => LoadingDescription);
                NotifyOfPropertyChange(() => CanLogOn);
            }
        }

        private bool _selEnglish;

        public bool SelEnglish
        {
            get { return _selEnglish; }
            set
            {
                _selEnglish = value;
                selected = true;
                Language = "en";
                GetLoadingDescription();
                NotifyOfPropertyChange(() => Language);
                NotifyOfPropertyChange(() => LoadingDescription);
                NotifyOfPropertyChange(() => CanLogOn);
            }
        }

        private bool _selKorean;

        public bool SelKorean
        {
            get { return _selKorean; }
            set
            {
                _selKorean = value;
                selected = true;
                Language = "ko";
                GetLoadingDescription();
                NotifyOfPropertyChange(() => Language);
                NotifyOfPropertyChange(() => LoadingDescription);
                NotifyOfPropertyChange(() => CanLogOn);
            }
        }

        private bool _selGerman;

        public bool SelGerman
        {
            get { return _selGerman; }
            set
            {
                _selGerman = value;
                selected = true;
                Language = "de";
                GetLoadingDescription();
                NotifyOfPropertyChange(() => Language);
                NotifyOfPropertyChange(() => LoadingDescription);
                NotifyOfPropertyChange(() => CanLogOn);
            }
        }

        private bool _selItalian;

        public bool SelItalian
        {
            get { return _selItalian; }
            set
            {
                _selItalian = value;
                selected = true;
                Language = "it";
                GetLoadingDescription();
                NotifyOfPropertyChange(() => Language);
                NotifyOfPropertyChange(() => LoadingDescription);
                NotifyOfPropertyChange(() => CanLogOn);
            }
        }

        private string _loadingDescription;

        public string LoadingDescription
        {
            get { return _loadingDescription; }
            set
            {
                _loadingDescription = value;
                //NotifyOfPropertyChange(() => LoadingDescription);
            }
        }


        public bool CanLogOn
        {
            get
            {
                bool output = false;

                if (selected)
                {
                    output = true;
                }

                return output;
            }
        }

        #endregion



        #region Methods

        public LogOnViewModel(IEventAggregator events)
        {
            _events = events;
        }


        public async Task LogOn()
        {
            await _events.PublishOnUIThreadAsync(new LogOnEvent { Language = Language }, new CancellationToken());
        }

        public void GetLoadingDescription()
        {
            LoadingDescription = LogOnLanguage.GetLoadingText(Language);
        }
        #endregion
    }
}
