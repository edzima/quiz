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

namespace quiz
{
    public partial class addCategory : PhoneApplicationPage
    {
        public addCategory()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (has.validateCategory(txtCategory.Text))
            {
                var webclient = new WebClient();
                webclient.OpenReadAsync(new Uri("http://www.robocza.h2g.pl/quiz/category.php?name=" + txtCategory.Text));
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