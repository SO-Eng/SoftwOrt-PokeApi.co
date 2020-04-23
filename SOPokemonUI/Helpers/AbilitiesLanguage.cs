using System;
using System.Collections.Generic;
using System.Text;

namespace SOPokemonUI.Helpers
{
    public static class AbilitiesLanguage
    {
        public static string GetWeightLanguage(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "ja-Hrkt":
                    tempLanguage = "重量";
                    break;
                case "ko":
                    tempLanguage = "무게";
                    break;
                case "fr":
                    tempLanguage = "Poids";
                    break;
                case "de":
                    tempLanguage = "Gewicht";
                    break;
                case "es":
                    tempLanguage = "Peso";
                    break;
                case "it":
                    tempLanguage = "Peso";
                    break;
                default:
                    tempLanguage = "Weight";
                    break;
            }

            return tempLanguage;
        }
        public static string GetHeightLanguage(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "ja-Hrkt":
                    tempLanguage = "高さ";
                    break;
                case "ko":
                    tempLanguage = "신장";
                    break;
                case "fr":
                    tempLanguage = "Hauteur";
                    break;
                case "de":
                    tempLanguage = "Größe";
                    break;
                case "es":
                    tempLanguage = "Altura";
                    break;
                case "it":
                    tempLanguage = "Altezza";
                    break;
                default:
                    tempLanguage = "Height";
                    break;
            }

            return tempLanguage;
        }
        public static string GetAbilityLanguage(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "ja-Hrkt":
                    tempLanguage = "能力";
                    break;
                case "ko":
                    tempLanguage = "능력";
                    break;
                case "fr":
                    tempLanguage = "Aptitude";
                    break;
                case "de":
                    tempLanguage = "Fähigkeit";
                    break;
                case "es":
                    tempLanguage = "Habilidad";
                    break;
                case "it":
                    tempLanguage = "Capacità";
                    break;
                default:
                    tempLanguage = "Ability";
                    break;
            }

            return tempLanguage;
        }
    }
}
