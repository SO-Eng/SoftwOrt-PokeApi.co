using System;
using System.Collections.Generic;
using System.Text;

namespace SOPokemonUI.LanguagePack
{
    public static class InfoLanguages
    {
        public static string TextOne(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "fr":
                    tempLanguage = "Cette application est basée sur une idée de mon fils et moi. Nous avons conçu l'application ensemble.";
                    break;
                case "de":
                    tempLanguage = "Diese App basiert auf einer Idee von meinem Sohn und mir. Wir haben die App zusammen entworfen.";
                    break;
                case "es":
                    tempLanguage = "Esta aplicación está basada en una idea de mi hijo y mía. Diseñamos la aplicación juntos.";
                    break;
                case "it":
                    tempLanguage = "Questa app si basa su un'idea di me e mio figlio. Abbiamo progettato l'app insieme.";
                    break;
                default:
                    tempLanguage = "This app is based on an idea from my son and me. We designed the app together.";
                    break;
            }

            return tempLanguage;
        }

        public static string TextTwo(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "fr":
                    tempLanguage = "PokeDex de SoftwOrt-Engineering utilise l'API RESTful de ";
                    break;
                case "de":
                    tempLanguage = "PokeDex von SoftwOrt-Engineering nutzt die RESTful API von ";
                    break;
                case "es":
                    tempLanguage = "PokeDex de SoftwOrt-Engineering utiliza la API RESTful de ";
                    break;
                case "it":
                    tempLanguage = "PokeDex di SoftwOrt-Engineering utilizza l'API RESTful di ";
                    break;
                default:
                    tempLanguage = "PokeDex from SoftwOrt-Engineering uses the RESTful API from ";
                    break;
            }

            return tempLanguage;
        }

        public static string TextThree(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "fr":
                    tempLanguage = "Toutes les informations et les packs de langues concernant les Pokémon sont fournis par cette API, il faut donc un accès à Internet pour utiliser le PokeDex.";
                    break;
                case "de":
                    tempLanguage = "Alle Informationen und Sprachpakete über die Pokemons werden von dieser API gespeist, daher ist eine Internetnutztung vorrausgesetzt um den PokeDex nutzen zu können.";
                    break;
                case "es":
                    tempLanguage = "Toda la información y los paquetes de lenguaje sobre los Pokemons son proporcionados por esta API, por lo que se requiere acceso a Internet para utilizar el PokeDex.";
                    break;
                case "it":
                    tempLanguage = "Tutte le informazioni e i pacchetti di lingua sui Pokemon sono forniti da questa API, quindi l'accesso a internet è necessario per utilizzare il PokeDex.";
                    break;
                default:
                    tempLanguage = "All information and language packs about the Pokemons are provided by this API, so internet access is required to use the PokeDex.";
                    break;
            }

            return tempLanguage;
        }

        public static string TextFour(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "fr":
                    tempLanguage = "Informations sur la structure de l'application :\n- L'interface utilisateur est construite dans le design de la MVVM. L'extension de Caliburn Micro est utilisée à cet effet.";
                    break;
                case "de":
                    tempLanguage = "Informationen zum Aufbau der App:\n- Das Nuzterinterface ist im MVVM Design aufgebaut. Dazu wird die Erweiterung von Caliburn Micro eingesetzt.";
                    break;
                case "es":
                    tempLanguage = "La información sobre la estructura de la App:\n- La interfaz de usuario está construida en un diseño MVVM.Para ello se utiliza la extensión de Caliburn Micro.";
                    break;
                case "it":
                    tempLanguage = "Informazioni sulla struttura dell'App:\n- L'interfaccia utente è costruita in design MVVM. A questo scopo viene utilizzata l'estensione di Caliburn Micro.";
                    break;
                default:
                    tempLanguage = "Information about the structure of the App:\n- The user interface is built in MVVM design.For this purpose the extension from Caliburn Micro is used.";
                    break;
            }

            return tempLanguage;
        }

        public static string TextFive(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "fr":
                    tempLanguage = "- Le backend pour le contrôle de l'API provient de PoroCYon ";
                    break;
                case "de":
                    tempLanguage = "- Das Backend für die API Ansteuerung ist ist von PoroCYon ";
                    break;
                case "es":
                    tempLanguage = "- El backend para el control de la API es de PoroCYon ";
                    break;
                case "it":
                    tempLanguage = "- Il backend per il controllo API è di PoroCYon ";
                    break;
                default:
                    tempLanguage = "- The backend for the API control is from PoroCYon ";
                    break;
            }

            return tempLanguage;
        }

        public static string TextSix(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "fr":
                    tempLanguage = "Nous sommes heureux si vous aimez ce PokeDex ! Vous pouvez volontiers m'écrire des suggestions ou me signaler des erreurs sur : ";
                    break;
                case "de":
                    tempLanguage = "Wir freuen uns wenn Sie gefallen an diesem PokeDex haben! Gerne können Sie mir Anregungen schreiben oder auch Fehler melden auf: ";
                    break;
                case "es":
                    tempLanguage = "¡Nos complace que te guste este PokeDex! Con gusto puedes escribirme sugerencias o informar de errores: ";
                    break;
                case "it":
                    tempLanguage = "Siamo lieti se questo PokeDex vi piace! Con piacere potete scrivermi suggerimenti o segnalarmi errori: ";
                    break;
                default:
                    tempLanguage = "We are pleased if you like this PokeDex! Gladly you can write me suggestions or report errors on: ";
                    break;
            }

            return tempLanguage;
        }

        public static string TextSeven(string language)
        {
            string tempLanguage = "";

            switch (language)
            {
                case "fr":
                    tempLanguage = "Le projet est OpenSource et soumis à la licence du MIT.";
                    break;
                case "de":
                    tempLanguage = "Das Projekt ist OpenSource und unterliegt der MIT Lizenz.";
                    break;
                case "es":
                    tempLanguage = "El proyecto es de código abierto y está sujeto a la licencia del MIT.";
                    break;
                case "it":
                    tempLanguage = "Il progetto è OpenSource e soggetto alla licenza del MIT.";
                    break;
                default:
                    tempLanguage = "The project is OpenSource and subject to the MIT license.";
                    break;
            }

            return tempLanguage;
        }

    }
}
