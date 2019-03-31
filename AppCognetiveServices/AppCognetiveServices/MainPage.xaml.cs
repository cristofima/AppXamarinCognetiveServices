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

            var languagesList = new List<Language>()
            {
                new Language { Code = "de", Name = "Alemán" },
                new Language { Code = "da", Name = "Danés" },
                new Language { Code = "es", Name = "Español" },
                new Language { Code = "fr", Name = "Francés" },
                new Language { Code = "el", Name = "Griego" },
                new Language { Code = "en", Name = "Inglés" },
                new Language { Code = "it", Name = "Italiano" },
                new Language { Code = "no", Name = "Noruego" },
                new Language { Code = "pl", Name = "Polaco" },
                new Language { Code = "pt", Name = "Portugués" },
                new Language { Code = "ru", Name = "Ruso" },
                new Language { Code = "sv", Name = "Sueco" },
                new Language { Code = "tr", Name = "Turco" }
            };

            this.picker.ItemsSource = languagesList;
            picker.ItemDisplayBinding = new Binding("Name");
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            btn.IsEnabled = false;

            this.actInd.IsVisible = true;
            this.actInd.IsRunning = true;

            var texto = this.txtValor.Text;
            if (!string.IsNullOrEmpty(texto) && this.picker.SelectedItem != null)
            {
                texto = texto.Trim();
                var language = (Language)this.picker.SelectedItem;

                var request = new Request
                {
                    documents = new List<Document>()
                    {
                        new Document{language=language.Code, id=1,text=texto}
                    }
                };

                var response = await ApiServices.Post(request);
                if (response.errors != null)
                {
                    if (response.errors.Count < 1)
                    {
                        var score = response.documents[0].score;
                        await DisplayAlert("Score", score + "", "OK");
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "Por favor ingrese la información solicitada.", "OK");
            }

            btn.IsEnabled = true;
            this.actInd.IsVisible = false;
            this.actInd.IsRunning = false;
        }
    }
}