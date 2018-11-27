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
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : UserControl 
    {
        public Settings(MaterialDesignThemes.Wpf.PackIcon KindServerStatus)
        {
            InitializeComponent();
           
            DataContext = new ViewModelSetting(KindServerStatus,GridBlack);
            MainClient.ServerOff += MainClient_ServerOff;
        }

        private void MainClient_ServerOff()
        {
            GridBlack.Visibility = Visibility.Hidden;
        }

        private void ButtonD_Click(object sender, RoutedEventArgs e)
        {
            MainClient.DisconnectToServer();
        }
    }
}
