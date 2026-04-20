using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using Microsoft.Maui.Networking;



namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try 
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);
                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n " +
                                         $"Longitude: {t.lon} \n " +
                                         $"Nascer do Sol: {t.sunrise} \n " +
                                         $"Por do Sol: {t.sunset} \n " +
                                         $"Temp Máx: {t.temp_max} \n " +
                                         $"Temp Min: {t.temp_min} \n ";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        lbl_res.Text = "Cidade não encontrada";
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a Cidade";
                }
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayAlert("Sem internet", "Verifique sua conexão.", "OK");
                    return;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

}
