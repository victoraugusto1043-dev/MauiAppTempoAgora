using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo t = null;

            string chave = "6135072afe7f6cec1537d5cb08a5a1a2";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={chave}&units=metric&lang=pt_br";
            using (HttpClient Client = new HttpClient())
            {
                HttpResponseMessage resp = await Client.GetAsync(url);
                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    var rascunho = JObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();
                    t = new ()
                    {
                        lon = (double)rascunho["coord"]["lon"],
                        lat = (double)rascunho["coord"]["lat"],
                        visibility = (int)rascunho["visibility"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        temp_max = (double)rascunho["main"]["temp_max"],
                        sunrise = sunrise.ToString(),
                        sunset =  sunset.ToString(),
                        description = (string)rascunho["weather"][0]["description"],
                        id = (int)rascunho["weather"][0]["id"],
                        speed = (double)rascunho["wind"]["speed"],
                        main = (string)rascunho["weather"][0]["main"],
                    };
                }
                return t;


            }

        }
    }
}
