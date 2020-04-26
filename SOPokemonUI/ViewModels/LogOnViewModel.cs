using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using SOPokemonUI.EventModels;

namespace SOPokemonUI.ViewModels
{
    public class LogOnViewModel : Screen
    {

        #region Fields

        private readonly IEventAggregator _events;

        private bool selected = false;

        private string _language = "de";

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }


        private int _selGerman;

        public int SelGerman
        {
            get { return _selGerman; }
            set
            {
                _selGerman = value;
                selected = true;
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool CanLogIn
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


        public async Task LogIn()
        {
            await _events.PublishOnUIThreadAsync(new LogOnEvents { Language = Language }, new CancellationToken());
        }
        #endregion
    }
}
