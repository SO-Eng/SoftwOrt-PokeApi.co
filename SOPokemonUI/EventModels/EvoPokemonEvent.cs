using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using SOPokemonUI.Models;

namespace SOPokemonUI.EventModels
{
    public class EvoPokemonEvent
    {
        public PokemonModel SelectedEvo { get; set; }
    }
}
