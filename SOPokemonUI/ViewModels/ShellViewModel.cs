using System;
using System.Collections.Generic;
using Caliburn.Micro;
using SOPokemonUI.EventModels;
using System.Threading;
using System.Threading.Tasks;
using SOPokemonUI.Helpers;
using SOPokemonUI.Models;

namespace SOPokemonUI.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive, IHandle<LogOnEvent>, IHandle<LoadingBarEvent>
    {

        #region Fields

        private readonly IEventAggregator _events;
        private bool logOn = false;

        private string _language;
        
        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        private string _file = "_File";

        public string File
        {
            get { return _file; }
            set
            {
                _file = value;
                NotifyOfPropertyChange(() => File);
            }
        }

        private string _close = "_Close";

        public string Close
        {
            get { return _close; }
            set
            {
                _close = value;
                NotifyOfPropertyChange(() => Close);
            }
        }

        private string _settings = "_Settings";

        public string Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                NotifyOfPropertyChange(() => Settings);
            }
        }

        private string _languageMenu = "_Language";

        public string LanguageMenu
        {
            get { return _languageMenu; }
            set
            {
                _languageMenu = value;
                NotifyOfPropertyChange(() => LanguageMenu);
            }
        }

        public bool CanSelectLanguage
        {
            get
            {
                bool output = false;

                if (logOn)
                {
                    output = true;
                }

                return output;
            }
        }

        private int _loadingValue;

        public int LoadingValue
        {
            get { return _loadingValue; }
            set
            {
                _loadingValue = value;
                NotifyOfPropertyChange(() => IsBarVisible);
            }
        }

        private string _loadingText;

        public string LoadingText
        {
            get { return _loadingText; }
            set { _loadingText = value; }
        }

        public bool IsBarVisible
        {
            get
            {
                bool output = false;

                if (LoadingValue > 0 && LoadingValue < 249)
                {
                    output = true;
                }

                return output;
            }
        }

        #endregion



        #region Methods

        public ShellViewModel(IEventAggregator events)
        {
            _events = events;
            _events.SubscribeOnUIThread(this);

            LoadingText = "Loading:";

            // Startup Screen
            ActivateItemAsync(IoC.Get<LogOnViewModel>(), new CancellationToken());
            //Start();
        }

        private void SetMenuLanguage()
        {
            File = MenuLanguage.MenuFile(Language);
            Close = MenuLanguage.MenuClose(Language);
            Settings = MenuLanguage.MenuSettings(Language);
            LanguageMenu = MenuLanguage.MenuLanguageSelect(Language);
        }

        // Start App without LogOn Screen
        private async void Start()
        {
            await HandleAsync(new LogOnEvent(), CancellationToken.None);
        }

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            Language = message.Language;

            SetMenuLanguage();
            logOn = true;
            NotifyOfPropertyChange(() => CanSelectLanguage);

            await ActivateItemAsync(new PokemonListViewModel(Language, _events), CancellationToken.None);
        }

        public async Task SelectLanguage()
        {
            logOn = false;
            NotifyOfPropertyChange(() => CanSelectLanguage);

            await ActivateItemAsync(IoC.Get<LogOnViewModel>(), new CancellationToken());
        }

        // End Application
        public void Exit()
        {
            TryCloseAsync();
        }

        public async Task HandleAsync(LoadingBarEvent message, CancellationToken cancellationToken)
        {
            LoadingValue = message.LoadingCount;

            NotifyOfPropertyChange(() => LoadingValue);
        }

        //public async Task HandleAsync(EvoPokemonEvent message, CancellationToken cancellationToken)
        //{
        //    await ActivateItemAsync(new PokemonDescrViewModel(Language, message.SelectedEvo));
        //    await ActivateItemAsync(new PokemonEvoViewModel(Language, message.SelectedEvo, message.EvoPokeList, _events cancellationToken);
        //}

        #endregion
    }
}
