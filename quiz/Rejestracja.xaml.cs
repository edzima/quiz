using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Windows.UI.Popups;
using System.IO;

 
 

namespace quiz
{
    public partial class Rejestracja : PhoneApplicationPage
    {
        public Rejestracja()
        {
            InitializeComponent();
        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
   
            /*
            string message = "Hello, MessageBox!";
            string caption = "Caption text";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            // Show message box
            MessageBoxResult result = MessageBox.Show(message, caption, buttons);
            */

            if (has.validateRegister(txtLogin.Text, txtEmail.Text, txtPass.Password, txtPass2.Password))
            {
                var webclient = new WebClient();
                webclient.OpenReadAsync(new Uri("http://www.robocza.h2g.pl/quiz/login.php?login=" + txtLogin.Text + "&pwd=" + has.Hash(txtPass.Password) +"&mail="+txtEmail.Text));
                webclient.OpenReadCompleted += new OpenReadCompletedEventHandler(opener);
            }
     
        }

        private void opener(object sender, OpenReadCompletedEventArgs e)
        {
            //  throw new NotImplementedException();
            using (var reader = new StreamReader(e.Result))
            {
                string response = reader.ReadLine();
                MessageBoxResult result = MessageBox.Show(response);
            }
        }
    }
}