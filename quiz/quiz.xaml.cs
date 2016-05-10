using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Threading;
using Newtonsoft.Json.Linq;
using System.IO;

namespace quiz
{
    public partial class add : PhoneApplicationPage
    {


        JArray questionJson;
        DispatcherTimer timer;
        int question; // aktualnie pobierane pytanie z tablicy
        int time; // licznik czasu
        int timeMinus;
        string indexAnswer;

        double timeAll;
        int goodAnswer;
        string id_cat = "";

      
        public add()
        {
            InitializeComponent();
        }

        

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            // Parse to Json String

            String response = e.Result.ToString();

            if (string.IsNullOrEmpty(e.Result.ToString().Trim())) // brak pytan w bazie
            {
                MessageBoxResult result = MessageBox.Show("Brak quizów do rozwiązania, poczekaj na nowe!");
                if (result == MessageBoxResult.OK) NavigationService.GoBack();
            }

                //pobranie pytan i rozpoaczecie quizu
            else
            { 
                questionJson = JArray.Parse(e.Result.ToString());
                question = 0;
                timeAll = 0;
                goodAnswer = 0;

                nextQuestion();
               
            }

        }



        //ustawienie odpowiedzi pod RadioButton
        private void SetAnswerText()
        {   
            int index= 0;
            string key = "answer" + index;
            answersPanel.Children
                .OfType<RadioButton>()
                .ToList()
                .ForEach(b => b.Content = getAnswerText(index++));
        }

        //pobranie wartosci dla pytań
        private string getAnswerText( int id_answer)
        {
            string key = "answer" + id_answer;
            return questionJson[question][key].ToString();
        }
        

        //sprawdzenie czy zaznaczono odpowiedz
        private bool getIndexAnswer()
        {
            var checkedButton = answersPanel.Children.OfType<RadioButton>()
                                         .FirstOrDefault(n => n.IsChecked == true);
            if (checkedButton != null)
            {
                checkedButton.IsChecked = false; //odznaczenie 
                indexAnswer = checkedButton.Name.Remove(0, 1);
                return true;
            }
            else
            {
                if (time >= 0)  MessageBox.Show("Nie zaznaczyłeś odpowiedzi!");
                return false;            
            }     
        }
        
  

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            nextQuestion();
        }


        private void nextQuestionSet()
        {
            if (!String.IsNullOrEmpty(txtQuestion.Text)) checkAnswerAndSendResult();
            txtQuizNr.Text = "Quiz nr: "+questionJson[question]["id_pyt"].ToString();
            txtQuestion.Text = questionJson[question]["question"].ToString();
            SetAnswerText();
            czasBar.Value = 100;
            time = int.Parse(questionJson[question]["time"].ToString());
            timeMinus = 100 / time;

            txtPozostalo.Text = "Pozostało Ci " + time + " s";
            question++;
        }

        private void checkAnswerAndSendResult()
        {   
            double timeAnswer=-1;

            if (indexAnswer == questionJson[question - 1]["true_answ"].ToString()) // poprawna odpowiedz
            {
                timeAnswer = (100 - czasBar.Value) / 10;
                goodAnswer++;
                timeAll += timeAnswer;
              
            }
            WebClient webclient = new WebClient();
            webclient.OpenReadAsync(new Uri("http://robocza.h2g.pl/quiz/answer.php?id_user=" + has.loadSettings("id_user") + "&id_pyt=" + questionJson[question - 1]["id_pyt"].ToString() + "&time=" + timeAnswer));

        }

        private void nextQuestion()
        {
            if (!String.IsNullOrEmpty(txtQuestion.Text))
            {
                if (getIndexAnswer() || time < 0)
                {
                    if (question < questionJson.Count) nextQuestionSet();
                    else
                    { // end question 

                        timer.Stop();
                        checkAnswerAndSendResult();
                        String text;
                        if(goodAnswer>0)  text = "Koniec quizu. Odpowiedziałeś poprawnie na: "+goodAnswer+" pytania. Średni czas poprawnej odpowiedzi: "+timeAll/goodAnswer +"s.";
                        else  text = "Koniec quizu. Nie odpowiedziałeś poprawnie na żadne pytanie :(";

                        MessageBoxResult result = MessageBox.Show(text);
                        if (result == MessageBoxResult.OK)  NavigationService.GoBack();
                    }
                }
            }
            else // pierwsze pytanie
            {
                nextQuestionSet();
                Start_timer();
            }
        }


        //odmierzanie czasu 
        private void Start_timer()
        {
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(00, 0, 1);
            bool enabled = timer.IsEnabled;
            timer.Start();
        }

        void timer_Tick(object sender, object e)
        {
            czasBar.Value -= timeMinus;
            time--;
            if (time >= 0) txtPozostalo.Text = "Pozostało Ci " + time + " s";         
            else nextQuestion();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

            if (NavigationContext.QueryString.TryGetValue("id_cat", out id_cat))
            {

                WebClient webclient = new WebClient();
                txtQuizNr.Text = "Pobieram pytania...";

                webclient.Headers[HttpRequestHeader.IfModifiedSince] = DateTime.UtcNow.ToString();

                webclient.DownloadStringAsync(new Uri(("http://robocza.h2g.pl/quiz/question.php?id_user=" + has.loadSettings("id_user") + "&id_cat=" + id_cat)));
                webclient.DownloadStringCompleted += webClient_DownloadStringCompleted;

            }

        }
    }
}