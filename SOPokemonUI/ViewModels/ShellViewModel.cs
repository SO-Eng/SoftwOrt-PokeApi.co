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

        private string _language = "de";

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
            ActivateItemAsync(IoC.Get<PokemonListViewModel>(), new CancellationToken());
        }

        public Task HandleAsync(LogOnEvents message, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        // End Application
        public void Exit()
        {
            TryCloseAsync();
        }

        #endregion
    }
}
