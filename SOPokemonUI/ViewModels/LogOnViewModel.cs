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

        private bool start = false;

        private readonly IEventAggregator _events;

        private bool selected = false;

        private string _language;
        private List<string> headerLanguage = new List<string>();
        private System.Windows.Forms.Timer timerHeader = new System.Windows.Forms.Timer();
        private int headerCounter = 1;

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
                if (SelJapanese)
                {
                    Language = "ja-Hrkt";
                    NotifyOfPropertyChange(() => Language);
                    GetLoadingDescription();
                    NotifyOfPropertyChange(() => CanLogOn);
                }
                else
                {
                    NotifyOfPropertyChange(() => LoadingDescription);
                }
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
                if (SelFrance)
                {
                    Language = "fr";
                    NotifyOfPropertyChange(() => Language);
                    GetLoadingDescription();
                    NotifyOfPropertyChange(() => CanLogOn);
                }
                else
                {
                    NotifyOfPropertyChange(() => LoadingDescription);
                }
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
                if (SelSpanish)
                {
                    Language = "es";
                    NotifyOfPropertyChange(() => Language);
                    GetLoadingDescription();
                    NotifyOfPropertyChange(() => CanLogOn);
                }
                else
                {
                    NotifyOfPropertyChange(() => LoadingDescription);
                }
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
                if (SelEnglish)
                {
                    Language = "en";
                    NotifyOfPropertyChange(() => Language);
                    GetLoadingDescription();
                    NotifyOfPropertyChange(() => CanLogOn);
                }
                else
                {
                    NotifyOfPropertyChange(() => LoadingDescription);
                }
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
                if (SelKorean)
                {
                    Language = "ko";
                    NotifyOfPropertyChange(() => Language);
                    GetLoadingDescription();
                    NotifyOfPropertyChange(() => CanLogOn);
                }
                else
                {
                    NotifyOfPropertyChange(() => LoadingDescription);
                }
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
                if (SelGerman)
                {
                    Language = "de";
                    NotifyOfPropertyChange(() => Language);
                    GetLoadingDescription();
                    NotifyOfPropertyChange(() => CanLogOn);
                }
                else
                {
                    NotifyOfPropertyChange(() => LoadingDescription);
                }
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
                if (SelItalian)
                {
                    Language = "it";
                    NotifyOfPropertyChange(() => Language);
                    GetLoadingDescription();
                    NotifyOfPropertyChange(() => CanLogOn);
                }
                else
                {
                    NotifyOfPropertyChange(() => LoadingDescription);
                }

            }
        }

        private string _loadingDescription;

        public string LoadingDescription
        {
            get { return _loadingDescription; }
            set
            {
                _loadingDescription = value;
                if (!start)
                {
                    NotifyOfPropertyChange(() => LoadingDescription);
                    start = true;
                }
            }
        }

        private string _selectLanguageHeader;

        public string SelectLanguageHeader
        {
            get { return _selectLanguageHeader; }
            set
            {
                _selectLanguageHeader = value;
                NotifyOfPropertyChange(() => SelectLanguageHeader);
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
            FillList();
            SelectLanguageHeader = headerLanguage[0];
            StartTimer();
        }

        // Load LanguageHeader in a List
        private void FillList()
        {
            var countLanguages = LanguageTotal.GetLanguagesCount();

            for (int i = 0; i < countLanguages; i++)
            {
                headerLanguage.Add(LogOnLanguage.SelectLanguageHeader(i));
            }
        }

        // Load LanguageHeader in a List
        private void StartTimer()
        {
            timerHeader.Tick += new EventHandler(TimerEventProcessor);
            timerHeader.Interval = 3000;
            timerHeader.Start();
        }

        // Timer to change LanguageHeader in intervall
        private void TimerEventProcessor(object sender, EventArgs e)
        {
            if (headerCounter == LanguageTotal.GetLanguagesCount())
            {
                headerCounter = 0;
            }

            // Go through the List to change HeaderLanguage
            SelectLanguageHeader = headerLanguage[headerCounter];

            headerCounter += 1;
        }

        // Switch to ShellViewModel => PokemonListViewModel with selected language
        public async Task LogOn()
        {
            timerHeader.Stop();

            await _events.PublishOnUIThreadAsync(new LogOnEvent { Language = Language }, new CancellationToken());
        }

        // Method to change loading description when language is selected
        public void GetLoadingDescription()
        {
            LoadingDescription = LogOnLanguage.GetLoadingText(Language);
        }
        #endregion
    }
}
