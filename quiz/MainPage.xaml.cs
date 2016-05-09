using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using quiz.Resources;

using System.IO;

namespace quiz
{
    public partial class MainPage : PhoneApplicationPage
    {
        int id_user = 0;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
    
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void opener(object sender, OpenReadCompletedEventArgs e)
        {
            //  throw new NotImplementedException();
            using (var reader = new StreamReader(e.Result))
            {
                int id_user =0;
                string response = reader.ReadLine();
                //response = response.Trim();
                if (Int32.TryParse(response, out id_user))
                {
                    has.saveSettings(id_user.ToString(), "id_user");
                    NavigationService.Navigate(new Uri("/menu.xaml", UriKind.Relative));
                }
                else phpStatus.Text = response;
               
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            phpStatus.Text = "Sprawdzam login...";
            var webclient = new WebClient();
            webclient.OpenReadAsync(new Uri("http://www.robocza.h2g.pl/quiz/login.php?login=" + txtLogin.Text + "&pwd=" + has.Hash(txtPass.Password)));
            webclient.OpenReadCompleted += new OpenReadCompletedEventHandler(opener);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Rejestracja.xaml", UriKind.Relative));
        }


        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(has.loadSettings("id_user"), out id_user))
            {
                if (id_user > 0)
                {
                    NavigationService.Navigate(new Uri("/menu.xaml", UriKind.Relative));
                }
            }
            
          
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}