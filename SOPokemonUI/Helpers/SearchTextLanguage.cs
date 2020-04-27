using System;
using System.Collections.Generic;
using System.Text;

namespace SOPokemonUI.Helpers
{
    public static class SearchTextLanguage
    {
        public static string GetSearchLanguage(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "ja-Hrkt":
                    tempLanguage = "探す:";
                    break;
                case "ko":
                    tempLanguage = "검색:";
                    break;
                case "fr":
                    tempLanguage = "Recherchez:";
                    break;
                case "de":
                    tempLanguage = "Suche:";
                    break;
                case "es":
                    tempLanguage = "Busca en:";
                    break;
                case "it":
                    tempLanguage = "Ricerca:";
                    break;
                default:
                    tempLanguage = "Search:";
                    break;
            }

            return tempLanguage;
        }

    }
}
