using Caliburn.Micro;
using SOPokemonUI.EventModels;
using System.Threading;
using System.Threading.Tasks;
using SOPokemonUI.Helpers;

namespace SOPokemonUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvents>
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
        }

        public async Task HandleAsync(LogOnEvents message, CancellationToken cancellationToken)
        {
            Language = message.Language;

            await ActivateItemAsync(IoC.Get<PokemonListViewModel>(), cancellationToken);
        }

        // End Application
        public void Exit()
        {
            TryCloseAsync();
        }

        #endregion
    }
}
