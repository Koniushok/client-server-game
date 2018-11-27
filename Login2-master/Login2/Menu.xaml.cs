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
using System.Windows.Shapes;
using Client;
using Game;


namespace Login
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public MainGameView gameview;

        public Menu()
        {
            InitializeComponent();
            GameServerOccasionHandler.StartGameEvent += StartGame;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainClient.QuickGame();
        }

        public void StartGame()
        {
            MainClient.GameServer.Game.GameOverEvent += Game_GameOverEvent;
            {
                gameview = new MainGameView();
                gameview.Show();
                this.Visibility = Visibility.Hidden;
            }
        }

        private void Game_GameOverEvent(bool win, int myCorrectAnswer, string myTimeAnsver, int enemyCorrectAnswer, string enemyTimeAnsver, int numNumStep, int NumAnswer, string timeGame)
        {

            gameview.GameOver(this, win, myCorrectAnswer, myTimeAnsver, enemyCorrectAnswer, enemyTimeAnsver, numNumStep, NumAnswer, timeGame);
            //gameview = null;
            MainClient.GameServer.Disconnect();
            // MainClient.GameServer = null;


        }
    }

 

}


