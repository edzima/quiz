using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;

namespace quiz
{
    public partial class selectCategoryQuiz : PhoneApplicationPage
    {
        public selectCategoryQuiz()
        {
            InitializeComponent();

            var webclient = new WebClient();
            webclient.DownloadStringCompleted += webClient_DownloadStringCompleted;
            webclient.DownloadStringAsync(new Uri(string.Format("http://www.robocza.h2g.pl/quiz/category.php?cat=1")));
        }


        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            // Parse to Json String
            if (!string.IsNullOrEmpty(e.Result))
            {
                var filterdata = e.Result;
                var filtervalue = JsonConvert.DeserializeObject<List<categories>>(filterdata); //JSON to Obcject C#
                listCategory.ItemsSource = filtervalue;
      


            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            String id_cat = (listCategory.SelectedItem as categories).id_cat;
            NavigationService.Navigate(new Uri("/quiz.xaml?id_cat=" + id_cat, UriKind.Relative));
        }
    }
}