using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Games;
using Client;
using System.IO;

namespace Game
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {

        public Profile(ImageSource myImg, string login)
        {
            InitializeComponent();
            MyImage.ImageSource = myImg;

            if(File.Exists("Resources/fon.png"))
            {
                Fon.ImageSource = new BitmapImage(new Uri("Resources/fon.png", UriKind.Relative));

            }
              
            


            DataOccasionHandler.GetStatisticsEvent += DataOccasionHandler_GetStatisticsEvent;

            MainClient.GetStatistics(login);
        }

        private void DataOccasionHandler_GetStatisticsEvent(Statistics stat, string login)
        {
            PrintStatistics(stat);
        }

        public void PrintStatistics(Statistics stat)
        {
            TextNumGame.Text = stat.NumGame.ToString();
            TextNumAnswer.Text = stat.NumAnswers.ToString();
            TextLeveGame.Text = stat.LeaveGame.ToString();
            TextWimGame.Text = stat.WinGame.ToString();
            TextCorrectAnswer.Text = stat.CorrectAnswers.ToString();
            if (stat.WinGame == 0 && stat.NumGame == 0)
            {

                TextWinNumGame.Text = "0%";
            }
            else
            {
                TextWinNumGame.Text = Math.Round((((double)stat.WinGame / stat.NumGame) * 100)).ToString() + "%";
            }

            TextLogin.Text = stat.Login;
            TextName.Text = stat.Name;
            TextSurName.Text = stat.SurName;
        }
    }
}
