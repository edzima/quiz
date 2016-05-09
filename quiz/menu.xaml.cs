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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/quiz.xaml", UriKind.Relative));
        }




        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ranking.xaml", UriKind.Relative));
        }


        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
          // Parse to Json String
            if (!string.IsNullOrEmpty(e.Result))
            {

                JArray catJson = JArray.Parse(e.Result);
                //cmbCurrFrom.ItemsSource = catJson;

                var filterdata = e.Result;
                var filtervalue = JsonConvert.DeserializeObject<List<categories>>(filterdata); //JSON to Obcject C#

                drugaLista.ItemsSource = filtervalue;
         


                foreach (var cat in catJson)
                {
                   // cmbCurrFrom.Items.Add(cat["name"].ToString());

                }

            }
        }

        public class categories
        {
            public string name { get; set; }
            public string id_cat { get; set; }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

          
        }

      
    }
}