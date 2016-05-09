using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Windows.UI.Popups;
using System.IO.IsolatedStorage;


namespace quiz
{
    public class categories
    {
        public string name { get; set; }
        public string id_cat { get; set; }

        public categories(String n, String id)
        {
            name = n;
            id_cat = id;
        }
    }
    class has
    {
        public static string Hash(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

    



        public static bool   validateRegister(string login, string email, string pass, string pass2)
        {
            bool validate = false;
            
            if (login.Length < 3)
            {
                MessageBoxResult result = MessageBox.Show("Podaj poprawnie login, wymagana długość to 4 znaki");
             
            }
            else
            {
                if (email.Length < 3)
                {
                    MessageBoxResult result = MessageBox.Show("Podaj poprawnie adres, wymagana długość to 4 znaki");
          
                }
                else
                {
                    if (pass.Length < 6)
                    {
                        MessageBoxResult result = MessageBox.Show("Podaj poprawnie hasło, wymagana długość to 7 znaków");
                       
                    }
                    else
                    {
                        if (pass != pass2)
                        {
                            MessageBoxResult result = MessageBox.Show("Hasła różnią się od siebie!");
                        }
                        else
                        {
                            validate = true;
                        }
                    }
                }


            }
            return validate;
        }

        public static bool validateAddQuiz(string question, string answer0, string answer1, string answer2, string answer3)
        {
            bool validate = false;

            if (question.Length < 10)   MessageBox.Show("Podaj poprawnie pytanie, minimalna długość to 10 znaków");
      
            else
            {
                if (answer0.Length < 3 || answer1.Length < 3 || answer2.Length < 3 || answer3.Length < 3)   MessageBox.Show("Podaj poprawnie odpowiedź, wymagana długość: 4-70 znaków");
                else    validate = true;
            }
            return validate;
        }

        public static bool validateCategory(string name)
        {
            bool validate = false;
            if (name.Length < 3)  MessageBox.Show("Wymagana długość to minimum 4 znaki");
            else    validate = true;
            return validate;
        }



        public static void saveSettings(string message, string name)
        {
            IsolatedStorageSettings.ApplicationSettings[name] = message;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public static string loadSettings(string name)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(name))
            {
                return (string)IsolatedStorageSettings.ApplicationSettings[name];
            }
            else
            {
                return null;
            }
        }
    }
}
