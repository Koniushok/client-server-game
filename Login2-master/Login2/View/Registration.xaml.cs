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

namespace Login
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>u
    public partial class Registration : UserControl
    {
        MainWindow mainView;
        public Registration()
        {
            InitializeComponent();
            mainView = Application.Current.MainWindow as MainWindow;
            DataContext = new ViewModelRegistration();
            OccasionHandler.RegistationEvent += ResulRegistration;
        }
        private void ButtonExit_Click(object sender, RoutedEventArgs e) 
        {
            Result.Visibility = Visibility.Hidden;
            mainView.MainGrid.Children.Remove(this);
            mainView.VisibilityChildren(Visibility.Visible);
        }

        

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainView.DragMove();
        }


        void ResulRegistration(bool test, string message)
        {
            Result.Text = message;
            Result.Visibility = Visibility.Visible;
            if (test)
            {
                
                Result.Foreground = new SolidColorBrush(Color.FromRgb(1,176,47));
            }
            else
            {
                Result.Foreground = new SolidColorBrush(Colors.Red);
            }

        }

        private void ButtonEntry_Click(object sender, RoutedEventArgs e)
        {
            if (!MainClient.Status)
            {
                Result.Visibility = Visibility.Visible;
                Result.Text = "Сервер не подключён";
                Result.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
