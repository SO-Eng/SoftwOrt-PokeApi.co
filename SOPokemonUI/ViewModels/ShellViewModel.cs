using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Caliburn.Micro;

namespace SOPokemonUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        #region Fields



        #endregion


        #region Methods

        public ShellViewModel()
        {
            
        }

        // End Application
        public void Exit()
        {
            TryCloseAsync();
        }

        #endregion
    }
}
