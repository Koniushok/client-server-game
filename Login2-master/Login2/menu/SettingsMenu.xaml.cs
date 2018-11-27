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

namespace Login
{
    /// <summary>
    /// Логика взаимодействия для SettingsMenu.xaml
    /// </summary>
    public partial class SettingsMenu : UserControl
    {
        MainMenu menu;
        bool loadingImg;
        public SettingsMenu(MainMenu menu)
        {
            InitializeComponent();
            DataContext = new ViewModelMenuSettings(ResultText);

            this.menu = menu;

            TextLogin.Text = MainClient.Profile.Login;
            MyImage.ImageSource = menu.MyImage.ImageSource;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (loadingImg)
            {
                menu.MyImage.ImageSource = MyImage.ImageSource;
                menu.TestImg = true;
                MainClient.NewImage(ConvertBitmapSourceToByteArray(MyImage.ImageSource));
            }
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
            return data;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog file_dialog = new Microsoft.Win32.OpenFileDialog();
            file_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            file_dialog.CheckFileExists = true;
            Nullable<bool> result = file_dialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    MyImage.ImageSource = new BitmapImage(new Uri(file_dialog.FileName));
                    loadingImg = true;                    
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл");
                }
            }
        }
    }
}
