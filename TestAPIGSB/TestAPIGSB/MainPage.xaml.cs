using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestAPIGSB.ClassesMetier;
using Xamarin.Forms;
using Newtonsoft.Json;
using TestAPIGSB.Pages;

namespace TestAPIGSB
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnSecteurs_Clicked(object sender, EventArgs e)
        {
            PageSecteur page = new PageSecteur();
            await Navigation.PushModalAsync(new NavigationPage(page));
        }

        private async void btnRegions_Clicked(object sender, EventArgs e)
        {
            PageRegion page = new PageRegion();
            await Navigation.PushModalAsync(new NavigationPage(page));
        }

        private async void btnVisiteurs_Clicked(object sender, EventArgs e)
        {
            PageVisiteur page = new PageVisiteur();
            await Navigation.PushModalAsync(new NavigationPage(page));
        }

        private void btnTravailler_Clicked(object sender, EventArgs e)
        {

        }
    }
}
