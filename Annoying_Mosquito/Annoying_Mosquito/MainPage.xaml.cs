using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Annoying_Mosquito
{

    public partial class MainPage : PhoneApplicationPage
    {

        public static IsolatedStorageSettings settings = System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
          //  SimpleMosquito simpleMosquito00 = new SimpleMosquito();
            //Loaded += AppBar_Loaded;
            //if (!settings.Contains("Score"))
            //{
            //    settings.Add("Score", "0");
    
            //}
          
            //settings.Save();
            //string totalget;
            //settings.TryGetValue("Score",out totalget);
            //txtTotal.Text = totalget;
            //totalget = (Convert.ToInt16(totalget) + simpleMosquito00.getTotal()).ToString();
            //simpleMosquito00.setTotal();
        }

        //void AppBar_Loaded(object sender, RoutedEventArgs e)
        //{
        //    ApplicationBar appBar = new ApplicationBar();
        //    ApplicationBarIconButton store = new ApplicationBarIconButton(new Uri(@"/Image/storelogo.png"/*logo url here*/, UriKind.Relative));
        //    store.Click += new EventHandler(StoreLoad);
        //    store.Text = "Store";
        //    var help = new ApplicationBarMenuItem("How to Play?");
        //    help.Click += new EventHandler(ShowHelp);
        //    appBar.Buttons.Add(store);
        //    appBar.MenuItems.Add(help);
        //    ApplicationBar = appBar;
        //}

        private void StoreLoad(object sender, EventArgs e)
        {
            //navigate to store page
            NavigationService.Navigate(new Uri("/StorePage.xaml?b=MainPage.xaml", UriKind.Relative));
        }

        private void ShowHelp(object sender, EventArgs e)
        {
            //navigate to help page
            NavigationService.Navigate(new Uri("/HelpPage.xaml?b=MainPage.xaml", UriKind.Relative));
        }
      

      


      
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            IsolatedStorageSettings appset = IsolatedStorageSettings.ApplicationSettings;
            if (appset.Contains("best"))
            {
                txtBest.Text = appset["best"].ToString();
            }
            
            ImageBrush background = new ImageBrush();
            background.ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Image/red_dot.png", UriKind.Relative));
            btnSurvive.Background = background;
            // var data = this.NavigationContext.QueryString;
            //if (data.ContainsKey("b") )
            //{
            //    if (data["b"] != "0")
            //    {
            //        //txtTotal.Text = settings["Scoress"].ToString();
            //        if(Convert.ToInt16(txtTotal.Text.ToString())< Convert.ToInt16( data["b"].ToString()))
            //        {
            //            txtTotal.Text = data["b"];
            //        }
            //        data["b"] = "0";
            //    }
            //   // txtTotal.Text = settings["Score"].ToString();
            //}
        }

       

        private void btnSurvive_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ImageBrush background = new ImageBrush();
            background.ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Image/red_splash.png", UriKind.Relative));
            btn.Background = background;
            Stream streamS = TitleContainer.OpenStream("Sounds/squash.wav");
            SoundEffect squash = SoundEffect.FromStream(streamS);

            SoundEffectInstance voiS = squash.CreateInstance();
            voiS.Play();
            NavigationService.Navigate(new Uri("/Survive.xaml?b=MainPage.xaml", UriKind.Relative));
        }

    




    }
}