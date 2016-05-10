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
    public partial class ranking : PhoneApplicationPage
    {
        public ranking()
        {
            InitializeComponent();

            var webclient = new WebClient();
            webclient.Headers[HttpRequestHeader.IfModifiedSince] = DateTime.UtcNow.ToString();
            webclient.DownloadStringCompleted += webClient_DownloadStringCompleted;
            webclient.DownloadStringAsync(new Uri(string.Format("http://www.robocza.h2g.pl/quiz/ranking.php?id_user=1")));

        }
        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            // Parse to Json String
            if (!string.IsNullOrEmpty(e.Result))
            {
                var filterdata = e.Result;
                var filtervalue = JsonConvert.DeserializeObject<List<Ranking>>(filterdata); //JSON to Obcject C#

                lst.ItemsSource = filtervalue;
                //listCategory.ItemsSource = filtervalue;
                /* 
                 JArray catJson = JArray.Parse(e.Result);
                 cmbCurrFrom.ItemsSource = catJson;
                 foreach (var cat in catJson)
                 {
                     // cmbCurrFrom.Items.Add(cat["name"].ToString());

                 }
                 */
            }
        }
        public class Ranking
        {
            public string login { get; set; }
            public string rank { get; set; }
            public string Sredni_Czas { get; set; }
            public string Poprawne_Odpowiedzi { get; set; }
        }
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}