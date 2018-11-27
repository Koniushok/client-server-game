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
using System.Threading;
using System.IO;

namespace Login
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        Settings SettingView;
        bool setting;
        Authorization AuthorizationView = new Authorization();
        bool authorization;

        MainMenu menu;

        Registration RegistrationView;

        public MainWindow()
        {
            MainClient.ServerOff += Disconnect;
            InitializeComponent();
            menu = new MainMenu();
            GridPrincipal.Children.Add(AuthorizationView);
            authorization = true;
            SettingView = new Settings(KindServerStatus);

            RegistrationView = new Registration();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            OccasionHandler.AuthoriztionEvent += AuthorizationResult;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

            try
            {
                throw (Exception)e.ExceptionObject;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            MainClient.DisconnectToServer();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {

            }
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(SettingView);
            setting = true;
            authorization = false;
            ButtonSettings1.Visibility = Visibility.Visible;
            ButtonSettings2.Visibility = Visibility.Hidden;
            ButtonHome1.Visibility = Visibility.Hidden;
            ButtonHome2.Visibility = Visibility.Visible;
            Result.Visibility = Visibility.Hidden;
        }

        private void ButtonHome2_Click(object sender, RoutedEventArgs e)
        {

            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(AuthorizationView);
            setting = false;
            authorization = true;
            ButtonSettings1.Visibility = Visibility.Hidden;
            ButtonSettings2.Visibility = Visibility.Visible;
            ButtonHome2.Visibility = Visibility.Hidden;
            ButtonHome1.Visibility = Visibility.Visible;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainAddUserControl(RegistrationView);

        }

        public void VisibilityChildren(Visibility vis)
        {
            foreach (UIElement item in MainGrid.Children)
            {
                item.Visibility = vis;
            }

            if (authorization)
            {
                ButtonSettings1.Visibility = Visibility.Hidden;
                ButtonHome2.Visibility = Visibility.Hidden;
            }
            else
            {
                ButtonSettings2.Visibility = Visibility.Hidden;
                ButtonHome1.Visibility = Visibility.Hidden;
            }
        }

        public void MainAddUserControl(UserControl elemen)
        {
            VisibilityChildren(Visibility.Hidden);
            this.Width = elemen.Width;
            this.Height = elemen.Height;
            MainGrid.Children.Add(elemen);
        }

        public void GridAddUserControl(Grid grid, UserControl elemen)
        {
            grid.Children.Clear();
            grid.Width = elemen.Width;
            grid.Height = elemen.Height;
            MainGrid.Children.Add(elemen);
        }

        public void Disconnect()
        {
            this.Visibility = Visibility.Visible;
            menu.Visibility = Visibility.Hidden;
            KindServerStatus.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));

        }

        void AuthorizationResult(bool test, string message)
        {
            if (test)
            {

                menu.Show();
                menu.Start(this);
                Result.Visibility = Visibility.Hidden;
                this.Visibility = Visibility.Hidden;
                
            }
            else
            {
                Result.Visibility = Visibility.Visible;

                Result.Text = message;
            }
        }

    }

}


