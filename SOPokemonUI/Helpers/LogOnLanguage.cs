using System;
using System.Collections.Generic;
using System.Text;

namespace SOPokemonUI.Helpers
{
    public static class LogOnLanguage
    {
        public static string GetLoadingText(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "ja-Hrkt":
                    tempLanguage = "進化";
                    break;
                case "ko":
                    tempLanguage = "진화";
                    break;
                case "fr":
                    tempLanguage = "Évolutions";
                    break;
                case "de":
                    tempLanguage = "Beim ersten mal kann das Laden der Pokemons etwas ländger dauern. Bitte habe etwas Geduld!";
                    break;
                case "es":
                    tempLanguage = "Evoluciones";
                    break;
                case "it":
                    tempLanguage = "Evoluzioni";
                    break;
                default:
                    tempLanguage = "Evolutions";
                    break;
            }

            return tempLanguage;
        }
    }
}
