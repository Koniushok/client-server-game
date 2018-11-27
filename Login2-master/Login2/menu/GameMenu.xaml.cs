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
using Login;
using System.IO;
using Game;

namespace Login
{
    /// <summary>
    /// Логика взаимодействия для GameMenu.xaml
    /// </summary>
    public partial class GameMenu : UserControl
    {
        public MainGameView gameview;
        public MainMenu mainMenu;
        public bool gameStatus;

        public GameMenu(MainMenu mainMenu)
        {
            InitializeComponent();
            GameServerOccasionHandler.StartGameEvent += StartGame;
            GameOccasionHandler.ExitGameEvent += GameOccasionHandler_ExitGameEvent;
            MainClient.ServerOff += MainClient_ServerOff;
            this.mainMenu = mainMenu;

            if (File.Exists("Resources/game.png"))
            {
                ImgGame.ImageSource = new BitmapImage(new Uri("Resources/game.png", UriKind.Relative));
            }
            
            


        }

        private void MainClient_ServerOff()
        {
            gameStatus = false;
            if (gameview != null)
                gameview.Close();
        }

        private void GameOccasionHandler_ExitGameEvent()
        {
            if (MainClient.GameServer != null)
            {
                MainClient.GameServer.Disconnect();
                MainClient.GameServer = null;
                gameStatus = false;
                gameview.Close();
                mainMenu.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MainClient.QuickGame();
            gameStatus = true;
            GridLoading.Visibility = Visibility.Visible;
        }

        public void StartGame()
        {
            MainClient.GameServer.Game.GameOverEvent += Game_GameOverEvent;
            {
                if (mainMenu.TestImg)
                {
                    gameview = new MainGameView(mainMenu.MyImage.ImageSource);
                }
                else
                {
                    gameview = new MainGameView();
                }
                gameview.Show();
                mainMenu.Visibility = Visibility.Hidden;
                GridLoading.Visibility = Visibility.Hidden;

            }
        }

        private void Game_GameOverEvent(bool win, int myCorrectAnswer, string myTimeAnsver, int enemyCorrectAnswer, string enemyTimeAnsver, int numNumStep, int NumAnswer, string timeGame)
        {
            gameview.GameOver(mainMenu, win, myCorrectAnswer, myTimeAnsver, enemyCorrectAnswer, enemyTimeAnsver, numNumStep, NumAnswer, timeGame);
            MainClient.GameServer.Disconnect();
            gameStatus = false;
        }

        private void Button_Click_Stop(object sender, RoutedEventArgs e)
        {
            if (MainClient.GameServer != null)
            {
                MainClient.GameServer.Disconnect();
                MainClient.GameServer = null;
                GridLoading.Visibility = Visibility.Hidden;
                gameStatus = false;
            }
        }
    }
}
