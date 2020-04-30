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
        private readonly string _language;

        public PokemonImageModel SetPokemonImage { get; set; }

        public LoadPokemonPic(string httpSprite, string language)
        {
            _httpSprite = httpSprite;
            _language = language;
        }

        public async Task<PokemonImageModel> LoadPokemonImage()
        {
            SetPokemonImage = new PokemonImageModel();
            BitmapImage imageTemp;
            var httpClient = new HttpClient();

            try
            {
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
                        SetPokemonImage.PokemonImage = imageTemp;

                    }
                }
            }
            catch
            {
                imageTemp = new BitmapImage(new Uri($"https://raw.githubusercontent.com/SO-Eng/SoftwOrt-PokeApi.co/master/SOPokemonUI/LanguagePack/Pictures/PicNA_Pokemon_{ _language }.png", UriKind.Absolute));
                SetPokemonImage.PokemonImage = imageTemp;
            }


            return SetPokemonImage;
        }

    }
}
