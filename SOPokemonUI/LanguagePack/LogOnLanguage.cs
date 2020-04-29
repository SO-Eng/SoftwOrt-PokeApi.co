namespace SOPokemonUI.LanguagePack
{
    public static class LogOnLanguage
    {
        public static string GetLoadingText(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "ja-Hrkt":
                    tempLanguage = "外を見る！\n最初の起動時、または新しい言語に変更するときに、ポケモンのロードに時間がかかることがあります。少々お待ちください。";
                    break;
                case "ko":
                    tempLanguage = "조심해!\n처음 시작할 때 또는 새로운 언어로 변경할 때 포켓몬을로드하는 데 시간이 더 걸릴 수 있습니다. 조금만 기다려주세요!";
                    break;
                case "fr":
                    tempLanguage = "Attention !\nAu premier démarrage, ou lors du passage à une nouvelle langue, le chargement des Pokémon peut prendre plus de temps. Veuillez faire preuve d'un peu de patience !";
                    break;
                case "de":
                    tempLanguage = "Achtung!\nBeim ersten Start, oder beim wechsel in eine neue Sprache, kann das Laden der Pokemons länger dauern. Bitte habe etwas Geduld!";
                    break;
                case "es":
                    tempLanguage = "¡Cuidado!\nAl principio, o al cambiar de idioma, cargar los Pokemons puede llevar más tiempo. ¡Por favor, ten un poco de paciencia!";
                    break;
                case "it":
                    tempLanguage = "Attenzione!\nAl primo avvio, o quando si passa a una nuova lingua, il caricamento dei Pokemon può richiedere più tempo. Abbiate un po' di pazienza!";
                    break;
                default:
                    tempLanguage = "Look out!\nAt the first start, or when changing to a new language, loading the Pokemons may take longer. Please have a little patience!";
                    break;
            }

            return tempLanguage;
        }

        public static string SelectLanguageHeader(int language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case 0:
                    tempLanguage = "言語を選択";
                    break;
                case 1:
                    tempLanguage = "언어를 선택하십시오";
                    break;
                case 2:
                    tempLanguage = "Sélectionnez votre langue";
                    break;
                case 3:
                    tempLanguage = "Wähle Deine Sprache aus";
                    break;
                case 4:
                    tempLanguage = "Seleccione su idioma";
                    break;
                case 5:
                    tempLanguage = "Seleziona la tua lingua";
                    break;
                default:
                    tempLanguage = "Select your language";
                    break;
            }

            return tempLanguage;
        }
    }
}
