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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Client;
using Game;
using System.IO;

namespace Login
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        MainWindow mainWindow;
        GameMenu gameMenu;

        Storyboard OpenMenu;
        Storyboard CloseMenu;
        bool openMenuStatus = true;
        public bool TestImg;
        public MainMenu()
        {
            InitializeComponent();
            gameMenu = new GameMenu(this);
        }

        public void Start(MainWindow window)
        {
            TextLogin.Text = MainClient.Profile.Login;
            OpenMenu = (Storyboard)FindResource("OpenMenu");
            CloseMenu = (Storyboard)FindResource("CloseMenu");
            BlackGrid.Visibility = Visibility.Visible;
            GridMain.Visibility = Visibility.Hidden;
            OpenMenu.Begin();

            mainWindow = window;
            if (File.Exists("Resources/user.png"))
            {
                MyImage.ImageSource = new BitmapImage(new Uri("Resources/user.png", UriKind.Relative));
            }


            DataOccasionHandler.GetImageEvent += DataOccasionHandler_GetImageEvent;

            MainClient.GetImage();
        }



        private void DataOccasionHandler_GetImageEvent(byte[] bytes)
        {

            var stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            TestImg = true;
            MyImage.ImageSource = image;
        }



        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {

            MainClient.DisconnectToServer();
            //mainWindow.Close();
            Application.Current.Shutdown();
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

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            if (openMenuStatus)
            {
                CloseMenu.Begin();
                BlackGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                OpenMenu.Begin();
                BlackGrid.Visibility = Visibility.Visible;
            }
            openMenuStatus = !openMenuStatus;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CloseMenu.Begin();
            GridMain.Visibility = Visibility.Visible;
            BlackGrid.Visibility = Visibility.Hidden;
            openMenuStatus = false;

            int index = ListViewMenu.SelectedIndex;

            listViewItem.Background = null;
            listViewItem1.Background = null;
            listViewItem2.Background = null;
            listViewItem3.Background = null;

            Color color = Color.FromRgb(1, 227, 231);
            color.A = 30;

            GridViewMenu.Children.Clear();

            switch (index)
            {
                case 0:
                    IconTitle.Kind = MaterialDesignThemes.Wpf.PackIconKind.GamepadVariant;
                    TextTitle.Text = "Быстрая игра";
                    listViewItem.Background = new SolidColorBrush(color);
                    GridViewMenu.Children.Add(gameMenu);
                    break;
                case 1:
                    IconTitle.Kind = MaterialDesignThemes.Wpf.PackIconKind.Account;
                    TextTitle.Text = "Профиль";
                    listViewItem1.Background = new SolidColorBrush(color);
                    GridViewMenu.Children.Add(new Profile(MyImage.ImageSource, MainClient.Profile.Login));
                    break;
                case 2:
                    IconTitle.Kind = MaterialDesignThemes.Wpf.PackIconKind.Settings;
                    TextTitle.Text = "Настройки";
                    listViewItem2.Background = new SolidColorBrush(color);
                    GridViewMenu.Children.Add(new SettingsMenu(this));

                    break;
                case 3:
                    IconTitle.Kind = MaterialDesignThemes.Wpf.PackIconKind.BookOpenVariant;
                    TextTitle.Text = "Информация";
                    listViewItem3.Background = new SolidColorBrush(color);
                    GridViewMenu.Children.Add(new Report());
                    break;
                case 4:
                    ListViewMenu.SelectedIndex = -1;
                    if (!gameMenu.gameStatus)
                    {
                        MainClient.Exit();
                        mainWindow.Visibility = Visibility.Visible;
                        Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        IconTitle.Kind = MaterialDesignThemes.Wpf.PackIconKind.GamepadVariant;
                        TextTitle.Text = "Быстрая игра";
                        GridViewMenu.Children.Add(gameMenu);
                        //MessageBox.Show("Идёт поиск игры");
                    }
                    break;
                default:
                    break;
            }




        }



        public byte[] Convert()
        {

            ImageSource img = MyImage.ImageSource;
            byte[] data;
            var t = new TransformedBitmap(img as BitmapSource, new ScaleTransform(0.05, 0.05));

            MyImage.ImageSource = t;


            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(t));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            while (data.Length > 8000)
            {
                t = new TransformedBitmap(img as BitmapSource, new ScaleTransform(0.05, 0.05));

                MyImage.ImageSource = t;


                encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(t));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }
            }
            MessageBox.Show(data.Length.ToString());


            //Microsoft.Win32.OpenFileDialog file_dialog = new Microsoft.Win32.OpenFileDialog();
            //file_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            //file_dialog.CheckFileExists = true;
            //Nullable<bool> result = file_dialog.ShowDialog();
            //if (result == true)
            //{
            //    try
            //    {
            //        image = new BitmapImage(new Uri(file_dialog.FileName));

            //        encoder = new JpegBitmapEncoder();
            //        encoder.Frames.Add(BitmapFrame.Create(image));
            //        using (MemoryStream ms = new MemoryStream())
            //        {
            //            encoder.Save(ms);
            //            data = ms.ToArray();
            //        }
            //        MessageBox.Show(data.Length.ToString());
            //        return data;
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Невозможно открыть выбранный файл");
            //    }
            //}


            return null;
        }

        public static byte[] ConvertBitmapSourceToByteArray(ImageSource imageSource)
        {
            var image = imageSource as BitmapSource;
            byte[] data;
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            MainClient.NewImage(data);
            //MainClient.NewImage(new byte[] { 1,2,3,4,5,6,7,8,9,10,1,1,1,1,111});
            return data;
        }

        private void listViewItem4_MouseDown_Exit(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
