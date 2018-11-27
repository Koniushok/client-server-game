using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Client;
using System.Windows.Media.Animation;
using DesignInControl;
using Games;
using System.IO;

namespace Game
{
    /// <summary>
    /// Логика взаимодействия для MainGameView.xaml
    /// </summary>
    public partial class MainGameView : Window
    {
        Client.Game game;
        TaskView tasks;
        private Storyboard myStoryboard;


        bool MyProfile;
        bool EnemyProfile;

        int numTask;


        public MainGameView(ImageSource myimg)
        {


            InitializeComponent();
            if (File.Exists("Resources/EnemyPlayer.png"))
            {
                EnemyImage.ImageSource = new BitmapImage(new Uri("Resources/EnemyPlayer.png", UriKind.Relative));

            }
            if (File.Exists("Resources/MyPlayer.png"))
            {
                MyImage.ImageSource = new BitmapImage(new Uri("Resources/MyPlayer.png", UriKind.Relative));

            }




            if (myimg != null)
            {
                MyImage.ImageSource = myimg;
            }
            //BattleEvent(new Games.TaskAnswer("Буш ",1,"да","да","не","т"), 1, 1000);
            //StatusImg.ImageSource = new BitmapImage(new Uri("Resources/21.png", UriKind.Relative));
            Start();

            //CreateAnimation(40);



        }

        public MainGameView()
        {


            InitializeComponent();
            if (File.Exists("Resources/EnemyPlayer.png"))
            {
                EnemyImage.ImageSource = new BitmapImage(new Uri("Resources/EnemyPlayer.png", UriKind.Relative));

            }
            if (File.Exists("Resources/MyPlayer.png"))
            {
                MyImage.ImageSource = new BitmapImage(new Uri("Resources/MyPlayer.png", UriKind.Relative));

            }

            //BattleEvent(new Games.TaskAnswer("Буш ",1,"да","да","не","т"), 1, 1000);
            //StatusImg.ImageSource = new BitmapImage(new Uri("Resources/21.png", UriKind.Relative));
            Start();

            //CreateAnimation(40);
        }


        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        void CreateAnimation(int time)
        {
            // ColorAnimation myColorAnimation = new ColorAnimation();

            // myColorAnimation.From = Color.FromRgb(0, 250, 0);
            // myColorAnimation.To = Color.FromRgb(255, 0, 0);
            // myColorAnimation.Duration = new Duration(TimeSpan.FromSeconds(time-3));

            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 0.0;
            myDoubleAnimation.To = 100.0;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(time));

            myStoryboard = new Storyboard();
            //  myStoryboard2 = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            //myStoryboard2.Children.Add(myColorAnimation);

            Storyboard.SetTarget(myDoubleAnimation, TimeStatus);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(DesignInControl.CircularProgressBar.PercentageProperty));

            //Storyboard.SetTarget(myColorAnimation, TimeStatus);
            //Storyboard.SetTargetProperty(myColorAnimation, new PropertyPath(DesignInControl.CircularProgressBar.SegmentColorProperty));

        }

        void StartTimer()
        {
            myStoryboard.Begin();
            //myStoryboard2.Begin();
        }

        void StopTimer()
        {
            try
            {
                myStoryboard.Stop();
            }
            catch
            {

            }
            TimeStatus.Percentage = 100;
        }

        void PointsUp()
        {
            EnemyPoints.Text = game.Enemy.Point.ToString();
            MyPoints.Text = game.MyPlayer.Point.ToString();
        }

        void Start()
        {
            MainTaskView.Visibility = Visibility.Hidden;

            game = MainClient.GameServer.Game;

            MyName.Text = MainClient.Profile.Login;
            EnemyName.Text = game.Enemy.Login;
            MapView.Start(game);
            PointsUp();
            //MainClient.GetImageClient(MainClient.Profile.Login);

            DataOccasionHandler.GetImageClientEvent += DataOccasionHandler_GetImageClientEvent;

            game.DisconectEvent += StopGameEvent;
            game.ChooseCentralEvent += ChooseCentral;
            game.ResresultChooseCetralEvent += ResresultChooseCetral;
            game.UpDataEvent += UpData;
            game.ChooseEvent += Game_ChooseEvent;
            game.ResresultChooseEvent += ResresultChoose;
            game.ChoosEnemyEvent += ChoosEnemy;
            game.ChoosEnemyCentralEvent += ChoosEnemyCentral;
            game.BattleEvent += BattleEvent;
            game.ResultTaskEvent += ResultTaskEvent;
            game.ResulAnsverEvent += Game_ResulAnsverEvent;
            game.SuperBattleEvent += Game_SuperBattleEvent;

            //MainClient.GetImageClient(game.Enemy.Login);


        }

        private void DataOccasionHandler_GetImageClientEvent(byte[] img, string login)
        {
            try
            {

                var stream = new MemoryStream(img);
                stream.Seek(0, SeekOrigin.Begin);
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();

                if (login == MainClient.Profile.Login)
                {
                    MyImage.ImageSource = image;

                }
                if (game.Enemy.Login == login)
                {
                    EnemyImage.ImageSource = image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #region Event
        private void Game_SuperBattleEvent(Games.GameTask task, int attackTer, int maxtime)
        {
            Snackbar.IsActive = false;
            if (game.Map.Territories[attackTer].Owner == game.MyPlayer)
            {
                if (File.Exists("Resources/MyBattle.png"))
                {
                    StatusImg.ImageSource = new BitmapImage(new Uri("Resources/MyBattle.png", UriKind.Relative));

                }

            }
            else
            {
                if (File.Exists("Resources/EnemyBattle.png"))
                {
                    StatusImg.ImageSource = new BitmapImage(new Uri("Resources/EnemyBattle.png", UriKind.Relative));

                }

            }



            CreateAnimation(maxtime / 1000);
            StartTimer();
            MainTaskView.Visibility = Visibility.Visible;
            BlackGrid.Visibility = Visibility.Visible;

            MainTaskView.Children.Clear();

            tasks = new TaskView();
            tasks.Start(task, true);
            MainTaskView.Children.Add(tasks);
        }

        private void Game_ResulAnsverEvent(int ansver, string myTime, string enemyTime)
        {
            EnemyGridTime.Visibility = Visibility.Visible;
            TextTimeEnemy.Text = enemyTime;
            Snackbar.IsActive = true;
            MyGridTime.Visibility = Visibility.Visible;
            TextTimeMy.Text = myTime;
            tasks.ResulAnswer(ansver);
        }

        private void ResultTaskEvent(int my, int enemy,int numTask)
        {
            this.numTask = numTask;
           
            tasks.ResultTask(my, enemy);
            StopTimer();
        }

        private void StopGameEvent()
        {
            Close();
        }

        private void BattleEvent(Games.GameTask task, int attackTer, int maxtime)
        {
            Snackbar.IsActive = false;
            if (game.Map.Territories[attackTer].Owner == game.MyPlayer)
            {
                if (File.Exists("Resources/MyBattle.png"))
                {
                    StatusImg.ImageSource = new BitmapImage(new Uri("Resources/MyBattle.png", UriKind.Relative));

                }
                
            }
            else
            {
                if (File.Exists("Resources/EnemyBattle.png"))
                {
                    StatusImg.ImageSource = new BitmapImage(new Uri("Resources/EnemyBattle.png", UriKind.Relative));

                }
                
            }


            CreateAnimation(maxtime / 1000);
            StartTimer();
            MainTaskView.Visibility = Visibility.Visible;
            BlackGrid.Visibility = Visibility.Visible;

            MainTaskView.Children.Clear();

            tasks = new TaskView();
            tasks.Start(task, true);
            MainTaskView.Children.Add(tasks);
            //GridMap.Visibility = Visibility.Hidden;

        }

        void TestStart()
        {
            Client.Game game = new Client.Game();
            game.MyPlayer = new Player("val");
            game.Enemy = new Player("bush");
            game.Map = new Maps();

            game.Map.Territories[1].Owner = game.Enemy;
            game.Map.Territories[2].Owner = game.Enemy;
            game.Map.Territories[3].Owner = game.Enemy;
            game.Map.Territories[4].Owner = game.Enemy;
            game.Map.Territories[5].Owner = game.Enemy;
            game.Map.Territories[6].Owner = game.Enemy;
            game.Map.Territories[12].Owner = game.MyPlayer;
            game.Map.Territories[13].Owner = game.MyPlayer;
            game.Map.Territories[14].Owner = game.MyPlayer;
            game.Map.Territories[15].Owner = game.MyPlayer;
            game.Map.Territories[16].Owner = game.MyPlayer;
            game.Map.Territories[17].Owner = game.MyPlayer;


            MapView.Start(game);

            bool[] open = new bool[20];

            for (int i = 0; i < 20; i++)
            {
                if (i % 2 == 0)
                    open[i] = true;
            }


            MapView.Choice(open);

        }

        public void ChooseCentral(bool[] Territory, int maxtime)
        {
            if (File.Exists("Resources/ChooseCentral.png"))
            {
                StatusImg.ImageSource = new BitmapImage(new Uri("Resources/ChooseCentral.png", UriKind.Relative));

            }
           
            

            //TimeStatus.SegmentColor = new SolidColorBrush(Color.FromRgb(8, 255, 46));
            MapView.ChoiceCentral(Territory);
            CreateAnimation(maxtime / 1000);
            StartTimer();
        }

        public void ResresultChooseCetral(int ter)
        {
            StopTimer();
            MapView.ResresultChooseCetral(ter);
        }

        public void UpData()
        {
            //TimeStatus.SegmentColor = new SolidColorBrush(Colors.White);
            

            AnimationPoint(2.0);

            MapView.UpData();

            if (MainTaskView.Visibility == Visibility.Visible)
            {
                
                MainTaskView.Visibility = Visibility.Hidden;
            }

            if (!MyProfile && !EnemyProfile)
                BlackGrid.Visibility = Visibility.Hidden;

            MainTaskView.Children.Clear();
            GridMap.Visibility = Visibility.Visible;

            EnemyGridTime.Visibility = Visibility.Hidden;
            MyGridTime.Visibility = Visibility.Hidden;

            if (File.Exists("Resources/UpData.png"))
            {
                StatusImg.ImageSource = new BitmapImage(new Uri("Resources/UpData.png", UriKind.Relative));

            }
            
            
            


        }

        private void Game_ChooseEvent(bool[] ter, int time)
        {
            if (File.Exists("Resources/Choose.png"))
            {
                StatusImg.ImageSource = new BitmapImage(new Uri("Resources/Choose.png", UriKind.Relative));

            }
           
            

            //TimeStatus.SegmentColor = new SolidColorBrush(Color.FromRgb(8, 255, 46));
            MapView.Choice(ter);
            CreateAnimation(time / 1000);
            StartTimer();
        }

        public void ResresultChoose(int ter)
        {
            StopTimer();
            MapView.ResresultChoose(ter);

            Snackbar.IsActive = false;
        }

        private void ChoosEnemy(int time)
        {
            if (File.Exists("Resources/ChoosEnemy.png"))
            {
                StatusImg.ImageSource = new BitmapImage(new Uri("Resources/ChoosEnemy.png", UriKind.Relative));

            }
           
           

            // TimeStatus.SegmentColor = new SolidColorBrush(Colors.Red);
            CreateAnimation(time / 1000);
            StartTimer();
        }

        private void ChoosEnemyCentral(int time)
        {
            if (File.Exists("Resources/ChoosEnemyCentral.png"))
            {
                StatusImg.ImageSource = new BitmapImage(new Uri("Resources/ChoosEnemyCentral.png", UriKind.Relative));

            }
            
           

            //TimeStatus.SegmentColor = new SolidColorBrush(Colors.Red);
            CreateAnimation(time / 1000);
            StartTimer();
        }

        #endregion

        private void Button_ClickUP(object sender, RoutedEventArgs e)
        {
            MainClient.GetImageClient(game.Enemy.Login);
        }

        private void Button_Click_EnemyProfile(object sender, RoutedEventArgs e)
        {
            if (!EnemyProfile)
            {
                GridProfile.Visibility = Visibility.Visible;
                GridProfile.Children.Clear();
                GridProfile.Children.Add(new Profile(EnemyImage.ImageSource, game.Enemy.Login));
                BlackGrid.Visibility = Visibility.Visible;
            }
            else
            {
                GridProfile.Visibility = Visibility.Hidden;
                BlackGrid.Visibility = MainTaskView.Visibility;
            }
            MyProfile = false;
            EnemyProfile = !EnemyProfile;
        }


        #region Point

        private void MyPointCompleted(object sender, EventArgs e)
        {
            MyPoints.Text = game.MyPlayer.Point.ToString();
        }

        private void EnemyPointCompleted(object sender, EventArgs e)
        {
            EnemyPoints.Text = game.Enemy.Point.ToString();
        }

        void AnimationPoint(double time)
        {
            Thickness MypositionTo = new Thickness(104, 68, 389, 484);
            Thickness MypositionFrom = new Thickness(104, 120, 389, 441);
            int k = game.MyPlayer.Point - int.Parse(MyPoints.Text);


            if (k != 0)
            {
                PointText(MyPointAdd, k);

                ThicknessAnimation Animation = new ThicknessAnimation(MypositionFrom, MypositionTo, TimeSpan.FromSeconds(time));

                Animation.Completed += MyPointCompleted;

                MyPointAdd.BeginAnimation(MarginProperty, Animation);
            }


            Thickness EnemypositionFrom = new Thickness(455, 120, 41, 0);
            Thickness EnemypositionTo = new Thickness(455, 66, 41, 0);

            k = game.Enemy.Point - int.Parse(EnemyPoints.Text);

            if (k != 0)
            {
                PointText(EnemyPointAdd, k);

                ThicknessAnimation Animation2 = new ThicknessAnimation(EnemypositionFrom, EnemypositionTo, TimeSpan.FromSeconds(time));

                Animation2.Completed += EnemyPointCompleted;

                EnemyPointAdd.BeginAnimation(MarginProperty, Animation2);
            }

        }

        void PointText(TextBlock textblock, int point)
        {
            string text = point.ToString();
            if (point > 0)
            {
                text = "+" + text;
                textblock.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 12));
                while (text.Length <= 4)
                {
                    text = " " + text;
                }
            }
            else
            {
                while (text.Length <= 5)
                {
                    text = " " + text;
                }

                textblock.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }



            textblock.Text = text;
        }
        #endregion

        public void GameOver(Window menu, bool win, int myCorrectAnswer, string myTimeAnsver, int enemyCorrectAnswer, string enemyTimeAnsver, int numNumStep, int NumAnswer, string timeGame)
        {
            try
            {
                ResultGrid.Children.Clear();
                BlackGrid.Visibility = Visibility.Visible;
                ResultGrid.Visibility = Visibility.Visible;

                Result result = new Result(this, menu, win, game.MyPlayer.Point, game.Enemy.Point, myCorrectAnswer, myTimeAnsver, enemyCorrectAnswer, enemyTimeAnsver, numNumStep, NumAnswer, MyImage.ImageSource, EnemyImage.ImageSource, timeGame);

                ResultGrid.Children.Add(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_MyProfile(object sender, RoutedEventArgs e)
        {
            if (!MyProfile)
            {
                GridProfile.Visibility = Visibility.Visible;
                GridProfile.Children.Clear();
                GridProfile.Children.Add(new Profile(MyImage.ImageSource, MainClient.Profile.Login));
                BlackGrid.Visibility = Visibility.Visible;
            }
            else
            {
                GridProfile.Visibility = Visibility.Hidden;
                BlackGrid.Visibility = MainTaskView.Visibility;
            }
            EnemyProfile = false;
            MyProfile = !MyProfile;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            GridExit.Visibility = Visibility.Visible;
        }

        private void ButtonOK(object sender, RoutedEventArgs e)
        {
            GameOccasionHandler.ExitGame();

        }

        private void ButtonNO(object sender, RoutedEventArgs e)
        {
            GridExit.Visibility = Visibility.Hidden;
        }

        private void AnsverSnackbarClick(object sender, RoutedEventArgs e)
        {
            if (BasicRatingBar.Value != 0)
            {
                MainClient.RepTask(BasicRatingBar.Value, numTask);
                Snackbar.IsActive = false;
                BasicRatingBar.Value = 0;
            }
            Snackbar.IsActive = false;
        }
    }
}
