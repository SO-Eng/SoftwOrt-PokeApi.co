using System.Windows.Media;

namespace SOPokemonUI.Helpers
{
    public static class ColorDeclaration
    {
        public static Brush FillTypeColorTextBoxes(int typeId)
        {
            int _typeId = typeId - 1;
            Brush tempColor = null;

            switch (_typeId)
            {
                case 0: //Normal
                    tempColor = Brushes.LightSlateGray;
                    break;
                case 1: //Fighting
                    tempColor = Brushes.SaddleBrown;
                    break;
                case 2: //Flying
                    tempColor = Brushes.DeepSkyBlue;
                    break;
                case 3: //Poison
                    tempColor = Brushes.Purple;
                    break;
                case 4: //Ground
                    tempColor = Brushes.SandyBrown;
                    break;
                case 5: //Rock
                    tempColor = Brushes.Gray;
                    break;
                case 6: //Bu g
                    tempColor = Brushes.LightGreen;
                    break;
                case 7: //Ghost
                    tempColor = Brushes.MediumPurple;
                    break;
                case 8: //Steel
                    tempColor = Brushes.Silver;
                    break;
                case 9: //Fire
                    tempColor = Brushes.Coral;
                    break;
                case 10: //Water
                    tempColor = Brushes.Blue;
                    break;
                case 11: //Grass
                    tempColor = Brushes.Green;
                    break;
                case 12: //Electric
                    tempColor = Brushes.Yellow;
                    break;
                case 13: //Psychic
                    tempColor = Brushes.CornflowerBlue;
                    break;
                case 14: //Ice
                    tempColor = Brushes.LightSkyBlue;
                    break;
                case 15: //Dragon
                    tempColor = Brushes.BlueViolet;
                    break;
                case 16: //Dark
                    tempColor = Brushes.DarkGray;
                    break;
                case 17: //Fairy
                    tempColor = Brushes.MediumVioletRed;
                    break;
                case 18: //Unknown
                    tempColor = Brushes.Sienna;
                    break;
                default: //Shadow
                    tempColor = Brushes.DimGray;
                    break;
            }

            return tempColor;
        }
    }
}
