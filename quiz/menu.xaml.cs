using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace quiz
{
    public partial class menu : PhoneApplicationPage
    {
        public menu()
        {
            InitializeComponent();
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            has.saveSettings("0", "id_user");
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
       
     
        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult mRes = MessageBox.Show("Czy chcesz zamknąć aplikacje?", "Exit", MessageBoxButton.OKCancel);
            if (mRes == MessageBoxResult.OK)
            {
                Application.Current.Terminate();
            }
            if (mRes == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}