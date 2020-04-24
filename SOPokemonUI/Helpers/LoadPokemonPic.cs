using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using SOPokemonUI.Models;

namespace SOPokemonUI.Helpers
{
    public class LoadPokemonPic
    {
        private string _httpSprite;

        public PokemonImageModel SetPokemonImage { get; set; }

        public LoadPokemonPic(string httpSprite)
        {
            _httpSprite = httpSprite;
        }

        public async Task<PokemonImageModel> LoadPokemonImage()
        {
            SetPokemonImage = new PokemonImageModel();
            BitmapImage imageTemp;
            var httpClient = new HttpClient();

            using (var response = await httpClient.GetAsync(_httpSprite))
            {
                if (response.IsSuccessStatusCode)
                {
                    using (var uri = new MemoryStream())
                    {
                        await response.Content.CopyToAsync(uri);
                        uri.Seek(0, SeekOrigin.Begin);

                        imageTemp = new BitmapImage();
                        imageTemp.BeginInit();
                        imageTemp.CacheOption = BitmapCacheOption.OnLoad;
                        imageTemp.StreamSource = uri;
                        imageTemp.EndInit();
                        imageTemp.Freeze();
                    }
                }
                else
                {
                    imageTemp = new BitmapImage(new Uri("https://raw.githubusercontent.com/SO-Eng/SoftwOrt-PokeApi.co/master/SOPokemonUI/Pictures/PicNA_Pokemon.png", UriKind.Absolute));
                }
            }

            SetPokemonImage.PokemonImage = imageTemp;
            return SetPokemonImage;
        }

    }
}
