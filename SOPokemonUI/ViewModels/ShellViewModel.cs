using System;
using System.Dynamic;
using Caliburn.Micro;
using SOPokemonUI.EventModels;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using SOPokemonUI.LanguagePack;
using Microsoft.Win32;

namespace SOPokemonUI.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive, IHandle<LogOnEvent>, IHandle<LoadingBarEvent>
    {

        #region Fields

        private readonly IEventAggregator _events;
        private readonly IWindowManager _window;
        private readonly InfoViewModel _info;
        private bool logOn = false;

        private string _language;
        public string regPath = @"Software\SoftwOrt\SO-PokeApi";
        private string subFolder;
        private string csvPath;

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

        private string _helpMenu = "_Help";

        public string HelpMenu
        {
            get { return _helpMenu; }
            set
            {
                _helpMenu = value;
                NotifyOfPropertyChange(() => HelpMenu);
            }
        }

        private string _infoMenu;

        public string InfoMenu
        {
            get { return _infoMenu; }
            set
            {
                _infoMenu = value;
                NotifyOfPropertyChange(() => InfoMenu);
            }
        }

        public bool CanSelectLanguage
        {
            get
            {
                bool output = false;

                if (logOn && LoadingValue >= 807)
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
            set
            {
                _loadingText = value;
                NotifyOfPropertyChange(() => LoadingText);
            }
        }

        public bool IsBarVisible
        {
            get
            {
                bool output = false;

                if (LoadingValue > 0 && LoadingValue < 807)
                {
                    output = true;
                }

                return output;
            }
        }

        #endregion



        #region Methods

        public ShellViewModel(IEventAggregator events, IWindowManager window, InfoViewModel info)
        {
            _events = events;
            _window = window;
            _info = info;
            _events.SubscribeOnUIThread(this);

            StartUpCheck();
        }

        private async Task StartUpCheck()
        {
            using (RegistryKey regkey = Registry.CurrentUser.OpenSubKey(regPath))
            {
                if (regkey != null)
                {
                    // Get last language selection
                    Language = regkey.GetValue("Language").ToString();
                    await HandleAsync(new LogOnEvent(), CancellationToken.None);
                }
                else
                {
                    await ActivateItemAsync(IoC.Get<LogOnViewModel>(), new CancellationToken());
                }
            }
        }

        private void SetMenuLanguage()
        {
            File = MenuLanguage.MenuFile(Language);
            Close = MenuLanguage.MenuClose(Language);
            Settings = MenuLanguage.MenuSettings(Language);
            LanguageMenu = MenuLanguage.MenuLanguageSelect(Language);
            LoadingText = MenuLanguage.LoadingBarSelect(Language);
            HelpMenu = MenuLanguage.MenuHelp(Language);
            InfoMenu = MenuLanguage.MenuInfo(Language);
        }

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            if (message.Language != null)
            {
                Language = message.Language;

                // Save to registry for next startup
                using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey(regPath))
                {
                    regKey.SetValue("Language", Language);
                }
            }

            // Create saveFile depending on selected Language in PokemonListViewModel.cs
            subFolder = $@"PokemonList\List_{ Language }.csv";
            csvPath = AppDomain.CurrentDomain.BaseDirectory + subFolder;

            LoadingValue = 0;
            NotifyOfPropertyChange(() => LoadingValue);
            SetMenuLanguage();
            logOn = true;
            NotifyOfPropertyChange(() => CanSelectLanguage);

            await ActivateItemAsync(new PokemonListViewModel(Language, _events, csvPath), CancellationToken.None);
        }

        public async Task HandleAsync(LoadingBarEvent message, CancellationToken cancellationToken)
        {
            LoadingValue = message.LoadingCount;

            NotifyOfPropertyChange(() => LoadingValue);
            NotifyOfPropertyChange(() => CanSelectLanguage);
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
            //SaveSettings();
            TryCloseAsync();
        }

        public async Task About()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Title = MenuLanguage.MenuInfo(Language);

            _info.GetLanguage(Language);
            await _window.ShowDialogAsync(_info, null, settings);
        }

        #endregion
    }
}
