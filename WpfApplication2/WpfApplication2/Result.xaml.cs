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
using Client;
using System.IO;

namespace Game
{
    /// <summary>
    /// Логика взаимодействия для Result.xaml
    /// </summary>
    public partial class Result : UserControl
    {
        Window Game;
        Window Menu;

        public Result()
        {
            InitializeComponent();
        }

        public Result(Window game,Window menu, bool win,int myPoint,int enemyPoint ,int myCorrectAnswer, string myTimeAnsver, int enemyCorrectAnswer, string enemyTimeAnsver, int numNumStep, int NumAnswer,ImageSource myImage, ImageSource enemyImage, string timeGame)
        {
            InitializeComponent();
            Game = game;
            Menu = menu;
            MyPoint.Text += myPoint.ToString();
            this.myCorrectAnswer.Text += myCorrectAnswer.ToString();
            this.myTimeAnsver.Text += myTimeAnsver;

            this.enemyPoint.Text += enemyPoint.ToString();
            this.enemyCorrectAnswer.Text += enemyCorrectAnswer.ToString();
            this.enemyTimeAnsver.Text += enemyTimeAnsver;

            this.numNumStep.Text += numNumStep.ToString();
            this.NumAnswer.Text += NumAnswer.ToString();

            if (File.Exists("Resources/EnemyPlayer.png"))
            {
                EnemyImage.ImageSource = new BitmapImage(new Uri("Resources/EnemyPlayer.png", UriKind.Relative));
            }

            if (File.Exists("Resources/MyPlayer.png"))
            {

                MyImage.ImageSource = new BitmapImage(new Uri("Resources/MyPlayer.png", UriKind.Relative));
            }


            this.GameTime.Text += timeGame;

            if (!win)
            {
                TextResult.Text = "Поражение";
                TextResult.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }

        }

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainClient.GameServer = null;
            Menu.Visibility = Visibility.Visible;
            Game.Close();
        }
    }
}
