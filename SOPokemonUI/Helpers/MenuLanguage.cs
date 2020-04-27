namespace SOPokemonUI.Helpers
{
    public static class MenuLanguage
    {
        public static string MenuFile(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "ja-Hrkt":
                    tempLanguage = "_伍";
                    break;
                case "ko":
                    tempLanguage = "_미디어";
                    break;
                case "fr":
                    tempLanguage = "_Média";
                    break;
                case "de":
                    tempLanguage = "_Datei";
                    break;
                case "es":
                    tempLanguage = "_Medio";
                    break;
                case "it":
                    tempLanguage = "_Media";
                    break;
                default:
                    tempLanguage = "_File";
                    break;
            }

            return tempLanguage;
        }

        public static string MenuClose(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "ja-Hrkt":
                    tempLanguage = "_終了する";
                    break;
                case "ko":
                    tempLanguage = "_닫기";
                    break;
                case "fr":
                    tempLanguage = "_Quitter";
                    break;
                case "de":
                    tempLanguage = "_Beenden";
                    break;
                case "es":
                    tempLanguage = "_Salir";
                    break;
                case "it":
                    tempLanguage = "_Esci";
                    break;
                default:
                    tempLanguage = "_Close";
                    break;
            }

            return tempLanguage;
        }


        public static string MenuSettings(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "ja-Hrkt":
                    tempLanguage = "_設定";
                    break;
                case "ko":
                    tempLanguage = "_설정";
                    break;
                case "fr":
                    tempLanguage = "_Préférences";
                    break;
                case "de":
                    tempLanguage = "_Einstellungen";
                    break;
                case "es":
                    tempLanguage = "_Preferencias";
                    break;
                case "it":
                    tempLanguage = "_Preferenze";
                    break;
                default:
                    tempLanguage = "_Settings";
                    break;
            }

            return tempLanguage;
        }

        public static string MenuLanguageSelect(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "ja-Hrkt":
                    tempLanguage = "_言語";
                    break;
                case "ko":
                    tempLanguage = "_언어";
                    break;
                case "fr":
                    tempLanguage = "_Langue";
                    break;
                case "de":
                    tempLanguage = "_Sprache";
                    break;
                case "es":
                    tempLanguage = "_Lenguaje";
                    break;
                case "it":
                    tempLanguage = "_Lingua";
                    break;
                default:
                    tempLanguage = "_Language";
                    break;
            }

            return tempLanguage;
        }


    }
}
