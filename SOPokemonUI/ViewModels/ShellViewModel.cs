using Caliburn.Micro;
using SOPokemonUI.EventModels;
using System.Threading;
using System.Threading.Tasks;
using SOPokemonUI.Helpers;

namespace SOPokemonUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {

        #region Fields

        private readonly IEventAggregator _events;

        private string _language;

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        #endregion



        #region Methods

        public ShellViewModel(IEventAggregator events)
        {
            _events = events;

            _events.SubscribeOnUIThread(this);

            // Startup Screen
            ActivateItemAsync(IoC.Get<LogOnViewModel>(), new CancellationToken());
            //Start();
        }

        // Start App without LogOn Screen
        private async void Start()
        {
            await HandleAsync(new LogOnEvent(), CancellationToken.None);
        }

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            Language = message.Language;

            await ActivateItemAsync(new PokemonListViewModel(Language), cancellationToken);
        }

        public async Task SelectLanguage()
        {
            await ActivateItemAsync(IoC.Get<LogOnViewModel>(), new CancellationToken());
        }

        // End Application
        public void Exit()
        {
            TryCloseAsync();
        }

        #endregion
    }
}
