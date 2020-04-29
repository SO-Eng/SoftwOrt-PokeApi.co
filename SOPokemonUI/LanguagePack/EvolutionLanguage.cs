namespace SOPokemonUI.LanguagePack
{
    public static class EvolutionLanguage
    {
        public static string GetEvolutionHeader(string language)
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
                    tempLanguage = "Entwicklungen";
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

        public static string GetBasisHeader(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "ja-Hrkt":
                    tempLanguage = "基礎ポケモン";
                    break;
                case "ko":
                    tempLanguage = "기초 포켓몬";
                    break;
                case "fr":
                    tempLanguage = "Base Pokemon";
                    break;
                case "de":
                    tempLanguage = "Basis Pokemon";
                    break;
                case "es":
                    tempLanguage = "Base Pokemon";
                    break;
                case "it":
                    tempLanguage = "Base Pokemon";
                    break;
                default:
                    tempLanguage = "Basis Pokemon";
                    break;
            }

            return tempLanguage;
        }

        public static string GetEvoOneHeader(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "ja-Hrkt":
                    tempLanguage = "最初の進化";
                    break;
                case "ko":
                    tempLanguage = "1 차 진화";
                    break;
                case "fr":
                    tempLanguage = "1ère évolution";
                    break;
                case "de":
                    tempLanguage = "1. Evolution";
                    break;
                case "es":
                    tempLanguage = "1ª evolución";
                    break;
                case "it":
                    tempLanguage = "1a evoluzione";
                    break;
                default:
                    tempLanguage = "1st evolution";
                    break;
            }

            return tempLanguage;
        }

        public static string GetEvoTwoHeader(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "ja-Hrkt":
                    tempLanguage = "第2進化";
                    break;
                case "ko":
                    tempLanguage = "두 번째 진화";
                    break;
                case "fr":
                    tempLanguage = "2ème évolution";
                    break;
                case "de":
                    tempLanguage = "2. Evolution";
                    break;
                case "es":
                    tempLanguage = "2ª evolución";
                    break;
                case "it":
                    tempLanguage = "2a evoluzione";
                    break;
                default:
                    tempLanguage = "2nd evolution";
                    break;
            }

            return tempLanguage;
        }
    }
}
