using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Game
{
    public partial class MainMap : UserControl
    {
        #region Style
        Style NormalT;
        Style MyT;
        Style EnemyT;
        Style NotOpenT;
        Style NotOpenEnemyT;
        Style MoveEnemyT;
        Style MoveT;
        Style СhoiceT;
        Style СhoiceEnemyT;
        Style СhoiceMyT;
        #endregion

        Client.Game game;
        List<TextBlock> Points { get; set; }
        List<Path> Ters { get; set; }
        bool[] OpenTer { get; set; }
        bool choice;
        bool choiceCentral;

        public MainMap()
        {
            InitializeComponent();

            NormalT = (Style)FindResource("NormalT");
            MyT = (Style)FindResource("MyT");
            EnemyT = (Style)FindResource("EnemyT");
            NotOpenT = (Style)FindResource("NotOpenT");
            NotOpenEnemyT = (Style)FindResource("NotOpenEnemyT");
            MoveEnemyT = (Style)FindResource("MoveEnemyT");
            MoveT = (Style)FindResource("MoveT");
            СhoiceT = (Style)FindResource("СhoiceT");
            СhoiceEnemyT = (Style)FindResource("СhoiceEnemyT");
            СhoiceMyT = (Style)FindResource("СhoiceMyT");

            //game = MainClient.GameServer.Game;
            Ters = new List<Path>() { t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18, t19 };
            Points = new List<TextBlock> { Point0, Point1, Point2, Point3, Point4, Point5, Point6, Point7, Point8, Point9, Point10, Point11, Point12, Point13, Point14, Point15, Point16, Point17, Point18, Point19, };
            OpenTer = new bool[20];
        }

        #region Масштаб
        public double ScaleFactor
        {
            get { return (double)GetValue(ScaleFactorProperty); }
            set { SetValue(ScaleFactorProperty, value); }
        }

        public static readonly DependencyProperty ScaleFactorProperty =
            DependencyProperty.Register(
               "ScaleFactor", typeof(double), typeof(MainMap), new PropertyMetadata(1.0));

        private void Grid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

            var p = v.Margin;

            if (e.Delta > 0)
            {
                val3.CenterX = e.GetPosition(v).X;
                val3.CenterY = e.GetPosition(v).Y;
                val.Value *= 1.1;


            }
            if (e.Delta < 0)
            {
                val3.CenterX = e.GetPosition(v).X;
                val3.CenterY = e.GetPosition(v).Y;
                val.Value /= 1.1;

            }


            v.Margin = p;
        }
        #endregion

        private void Ter_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void Ter_MouseLeave(object sender, MouseEventArgs e)
        {
            if (choice || choiceCentral)
            {
                Path ter = sender as Path;
                StyleTer(ter);
            }
        }

        private void Ter_MouseEnter(object sender, MouseEventArgs e)
        {
            if (choice || choiceCentral)
            {
                Path ter = sender as Path;
                StyleMoveTer(ter);
            }
        }

        private void Ter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (choice)
            {
                Path ter2 = sender as Path;
                
                foreach (Path ter in Ters)
                {
                    ter.IsEnabled = false;
                }
                ter2.IsEnabled = true;
                СhoiceTer(ter2);

                MainClient.GameServer.ChooseResult(Ters.IndexOf(ter2));

                choice = false;
            }

            if(choiceCentral)
            {
                Path ter2 = sender as Path;

                foreach (Path ter in Ters)
                {
                    ter.IsEnabled = false;
                }
                ter2.IsEnabled = true;
                СhoiceTer(ter2);

                MainClient.GameServer.ChooseCentralResult(Ters.IndexOf(ter2));

                choiceCentral = false;
            }
        }

        public void ShowTer()
        {
            foreach (Path ter in Ters)
            {
                StyleTer(ter);
                ter.IsEnabled = false;              
            }
        }

        public void ShowTerСhoice()
        {
            foreach (Path ter in Ters)
            {
                int k = Ters.IndexOf(ter);
                if (!OpenTer[k])
                {
                    
                    NotOpenTer(ter);
                }
                else
                {
                    ter.IsEnabled = true;
                }
            }
        }

        public void Choice(bool[] openTer)
        {
            choice = true;
            OpenTer = openTer;
            ShowTerСhoice();
        }

        public void ChoiceCentral(bool[] openTer)
        {
            choiceCentral = true;
            OpenTer = openTer;
            ShowTerСhoice();
        }

        #region StyleTer
        public void StyleTer(Path ter)
        {
            Territory territory = game.Map.Territories[Ters.IndexOf(ter)];

            if (territory.Owner == null)
            {
                ter.Style = NormalT;
            }
            if (territory.Owner == game.MyPlayer)
            {
                ter.Style = MyT;
            }
            if (territory.Owner == game.Enemy)
            {
                ter.Style = EnemyT;
            }
        }

        public void StyleMoveTer(Path ter)
        {
            Territory territory = game.Map.Territories[Ters.IndexOf(ter)];

            if (territory.Owner == null)
            {
                ter.Style = MoveT;
            }
            if (territory.Owner == game.Enemy)
            {
                ter.Style = MoveEnemyT;
            }
        }

        public void СhoiceTer(Path ter)
        {
            Territory territory = game.Map.Territories[Ters.IndexOf(ter)];

            if (territory.Owner == null)
            {
                ter.Style = СhoiceT;
            }
            if (territory.Owner == game.Enemy)
            {
                ter.Style = СhoiceEnemyT;
            }
            if(territory.Owner == game.MyPlayer)
            {
                ter.Style = СhoiceMyT;
            }
        }

        public void NotOpenTer(Path ter)
        {
            Territory territory = game.Map.Territories[Ters.IndexOf(ter)];

            if (territory.Owner == null)
            {
                ter.Style = NotOpenT;
            }
            if (territory.Owner == game.Enemy)
            {
                ter.Style = NotOpenEnemyT;
            }
        }
        #endregion

        public void ShowPoint()
        {
            foreach (TextBlock point in Points)
            {
                Territory territory = game.Map.Territories[Points.IndexOf(point)];
                point.Text = territory.Point.ToString();
            }
        }

        public void Start(Client.Game game)
        {
            this.game = game;
            ShowTer();
           // ShowPoint();
        }

        public void UpData()
        {
            ShowTer();
            ShowPoint();
        }

        public void ResresultChooseCetral(int k)
        {
            foreach (Path ter in Ters)
            {
                ter.IsEnabled = false;
            }
            Ters[k].IsEnabled = true;
            СhoiceTer(Ters[k]);
            choice = false;
        }

        public void ResresultChoose(int k)
        {
            ShowTer();
            Ters[k].IsEnabled = true;
            СhoiceTer(Ters[k]);
            choice = false;
        }

        private void CheckTer_Checked(object sender, RoutedEventArgs e)
        {
            if (false)
            {
                foreach (Path ter in Ters)
                {


                    Territory territory = game.Map.Territories[Ters.IndexOf(ter)];

                    if (territory.Owner == null)
                    {
                        if (CheckNullTer.IsChecked==false)
                            ter.Visibility = Visibility.Hidden;
                        else
                            ter.Visibility = Visibility.Visible;
                    }
                    if (territory.Owner == game.MyPlayer)
                    {
                        if (!CheckMyTer.IsChecked == false)
                            ter.Visibility = Visibility.Hidden;
                        else
                            ter.Visibility = Visibility.Visible;
                    }
                    if (territory.Owner == game.Enemy)
                    {
                        if (!CheckEnemyTer.IsChecked == false)
                            ter.Visibility = Visibility.Hidden;
                        else
                            ter.Visibility = Visibility.Visible;
                    }
                }
            }
        }
    }

    public class RestGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type tt, object parameter, CultureInfo ci)
        {
            return new GridLength((double)value - 1, GridUnitType.Star);
        }

        public object ConvertBack(object value, Type tt, object parameter, CultureInfo ci)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}



