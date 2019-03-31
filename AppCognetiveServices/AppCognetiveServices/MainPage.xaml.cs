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

            this.ResetComponents();

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
                        this.ShowScore(score);

                        this.CloseActivityIndicator();
                        await DisplayAlert("Score", score + "", "OK");
                    }
                }
            }
            else
            {
                this.CloseActivityIndicator();
                await DisplayAlert("Error", "Por favor ingrese la información solicitada.", "OK");
            }

            btn.IsEnabled = true;
        }

        private void CloseActivityIndicator()
        {
            this.actInd.IsVisible = false;
            this.actInd.IsRunning = false;
        }

        private void ResetComponents()
        {
            this.actInd.IsVisible = true;
            this.actInd.IsRunning = true;
            this.spanScore.Text = "";
            this.lblScore.IsVisible = false;
            this.prgBar.IsVisible = false;
        }

        private void ShowScore(double score)
        {
            this.spanScore.Text = score + "";
            this.lblScore.IsVisible = true;
            this.prgBar.IsVisible = true;
            this.prgBar.Progress = score;

            if (score <= 0.1)
            {
                this.prgBar.ProgressColor = Color.DarkRed;
            }
            else if (score <= 0.2)
            {
                this.prgBar.ProgressColor = Color.Red;
            }
            else if (score <= 0.3)
            {
                this.prgBar.ProgressColor = Color.OrangeRed;
            }
            else if (score <= 0.4)
            {
                this.prgBar.ProgressColor = Color.DarkOrange;
            }
            else if (score <= 0.5)
            {
                this.prgBar.ProgressColor = Color.Orange;
            }
            else if (score <= 0.6)
            {
                this.prgBar.ProgressColor = Color.Yellow;
            }
            else if (score <= 0.7)
            {
                this.prgBar.ProgressColor = Color.GreenYellow;
            }
            else if (score <= 0.8)
            {
                this.prgBar.ProgressColor = Color.YellowGreen;
            }
            else if (score <= 0.9)
            {
                this.prgBar.ProgressColor = Color.Green;
            }
            else
            {
                this.prgBar.ProgressColor = Color.DarkGreen;
            }
        }
    }
}