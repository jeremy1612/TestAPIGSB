using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestAPIGSB.ClassesMetier;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestAPIGSB.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSecteur : ContentPage
    {
        public PageSecteur()
        {
            InitializeComponent();
        }
        HttpClient ws;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<Secteur> lesSecteurs = new List<Secteur>();

            ws = new HttpClient();
            var reponse = await ws.GetAsync("http://10.0.2.2/SIO2ALT/APIGSB/secteurs/");
            var content = await reponse.Content.ReadAsStringAsync();
            lesSecteurs = JsonConvert.DeserializeObject<List<Secteur>>(content);
            lvSecteurs.ItemsSource = lesSecteurs;
        }

        private void lvSecteurs_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(lvSecteurs.SelectedItem!=null)
            {
                txtNomSecteur.Text = (lvSecteurs.SelectedItem as Secteur).Nom;
            }
        }

        private async void btnModifier_Clicked(object sender, EventArgs e)
        {
            if(txtNomSecteur.Text == null)
            {
                Toast.MakeText(Android.App.Application.Context, "Sélectionner un secteur", ToastLength.Short).Show();
            }
            else
            {
                ws = new HttpClient();
                Secteur sec = (lvSecteurs.SelectedItem as Secteur);
                sec.Nom = txtNomSecteur.Text;
                JObject jsec = new JObject
                {
                    {"Id",sec.Id},
                    {"Nom",sec.Nom}
                };
                string json = JsonConvert.SerializeObject(jsec);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var reponse = await ws.PutAsync("http://10.0.2.2/SIO2ALT/APIGSB/secteurs/",content);
                List<Secteur> lesSecteurs = new List<Secteur>();

                ws = new HttpClient();
                reponse = await ws.GetAsync("http://10.0.2.2/SIO2ALT/APIGSB/secteurs/");
                var flux = await reponse.Content.ReadAsStringAsync();
                lesSecteurs = JsonConvert.DeserializeObject<List<Secteur>>(flux);
                lvSecteurs.ItemsSource = lesSecteurs;
            }
        }

        private async void btnAjouter_Clicked(object sender, EventArgs e)
        {
            if (txtNomSecteur.Text == null)
            {
                Toast.MakeText(Android.App.Application.Context, "Saisir un nom de secteur", ToastLength.Short).Show();
            }
            else
            {
                ws = new HttpClient();
                //Secteur newSecteur = new Secteur();
                //newSecteur.Nom = txtNomSecteur.Text;
                JObject sec = new JObject
                {
                    { "Sec", txtNomSecteur.Text}
                };
                string json = JsonConvert.SerializeObject(sec);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var reponse = await ws.PostAsync("http://10.0.2.2/SIO2ALT/APIGSB/secteurs/",content);
                
                List<Secteur> lesSecteurs = new List<Secteur>();

                ws = new HttpClient();
                reponse = await ws.GetAsync("http://10.0.2.2/SIO2ALT/APIGSB/secteurs/");
                var flux = await reponse.Content.ReadAsStringAsync();
                lesSecteurs = JsonConvert.DeserializeObject<List<Secteur>>(flux);
                lvSecteurs.ItemsSource = lesSecteurs;
            }
        }
    }
}