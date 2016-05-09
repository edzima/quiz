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
using System.IO;

namespace quiz
{
    public partial class addQuiz : PhoneApplicationPage
    {
        int indexAnswer = -1;
        int time = 0;
        public addQuiz()
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


        public class categories
        {
            public string name { get; set; }
            public string id_cat { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (getIndexAnswer() && has.validateAddQuiz(txtQuestion.Text, txtAnswer0.Text, txtAnswer1.Text, txtAnswer2.Text, txtAnswer3.Text))
            {
                   time = getTime();
                   String id_cat = (listCategory.SelectedItem as categories).id_cat;
                   WebClient webclient = new WebClient();
                   string url = "http://robocza.h2g.pl/quiz/question.php?id_user=" + has.loadSettings("id_user") + "&time=" + time + "&cat=" + id_cat + "&question=" + txtQuestion.Text + "&answer0=" + txtAnswer0.Text + "&answer1=" + txtAnswer1.Text + "&answer2=" + txtAnswer2.Text + "&answer3=" + txtAnswer3.Text + "&true_answ=" + indexAnswer;
                   webclient.OpenReadAsync(new Uri(url));
                   webclient.OpenReadCompleted += new OpenReadCompletedEventHandler(addQuestion);  
             
            }
         
        }

             
        private bool getIndexAnswer()
        {
            var checkedButton = answersPanel.Children.OfType<RadioButton>()
                                         .FirstOrDefault(n => n.IsChecked == true);
            if (checkedButton != null)
            {
                indexAnswer = int.Parse(checkedButton.Name.Remove(0, 1));
                return true;
                //return int.Parse(checkedButton.Name.Remove(0, 1));// obciecie dolnej spacji z nazwy RadioButton
            }
            else
            {
                MessageBox.Show("Nie zaznaczyłeś poprawnej odpowiedzi!");
                return false;
            }
       
        }

        private int getTime()
        {
            time = listTime.SelectedIndex;
            if (time == 0) time = 10;
            else if (time == 1) time = 20;
            else time = 5;
            return time;
        }

        private void addQuestion (object sender, OpenReadCompletedEventArgs e){

            //  throw new NotImplementedException();
            using (var reader = new StreamReader(e.Result))
            {
                string response = reader.ReadLine();
                MessageBoxResult result = MessageBox.Show(response);
            }
        }
    }
}