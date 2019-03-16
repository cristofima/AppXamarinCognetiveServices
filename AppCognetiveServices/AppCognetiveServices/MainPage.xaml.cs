using AppCognetiveServices.Models;
using AppCognetiveServices.Services;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AppCognetiveServices
{
    public partial class MainPage : ContentPage
    {
        private ApiServices ApiServices;

        public MainPage()
        {
            InitializeComponent();
            this.lblWelcome.Text = "Cognetive Services Text Analytics";
            this.ApiServices = new ApiServices();
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            var texto = this.txtValor.Text;
            if (!string.IsNullOrEmpty(texto))
            {
                texto = texto.Trim();
                var request = new Request
                {
                    documents = new List<Document>()
                    {
                        new Document{language="es", id=1,text=texto}
                    }
                };

                var response = await ApiServices.Post(request);
                if (response.errors != null)
                {
                    if (response.errors.Count < 1)
                    {
                        var score = response.documents[0].score;
                        await DisplayAlert("Escore", score + "", "OK");
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "Por favor ingrese un texto", "OK");
            }
        }
    }
}