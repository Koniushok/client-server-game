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
using Command;
using Client;
using System.IO;

namespace Game
{
    /// <summary>
    /// Логика взаимодействия для Report.xaml
    /// </summary>
    public partial class Report : UserControl
    {
        public Report()
        {
            InitializeComponent();
            ComboBoxType.Items.Add(ReporType.недоработка);
            ComboBoxType.Items.Add(ReporType.правонарушение);
            ComboBoxType.Items.Add(ReporType.предложения);
            ComboBoxType.Items.Add(ReporType.сбой_программы);
            ComboBoxType.Items.Add(ReporType.другое);

            if (File.Exists("Resources/report.png"))
            {
                Icon.ImageSource = new BitmapImage(new Uri("Resources/report.png", UriKind.Relative));

            }
            
            

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextReport.Text != "")
                TextError.Visibility = Visibility.Hidden;
        }

        private void Button_ClickSend(object sender, RoutedEventArgs e)
        {
            ReporType type;
            if (ComboBoxType.SelectedItem != null)
            {
                type = (ReporType)ComboBoxType.SelectedItem;

            }
            else
            {
                TextError.Text = "Тип информации не указан";
                TextError.Visibility = Visibility.Visible;
                return;
            }

            if (TextReport.Text == "")
            {
                TextError.Text = "Поле текст пустое";
                TextError.Visibility = Visibility.Visible;
                return;
            }

            MainClient.Report(type, TextReport.Text);
            ResultText.Visibility = Visibility.Visible;

        }

        private void TextReport_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (TextReport.Text != "" && ComboBoxType.SelectedItem != null)
                TextError.Visibility = Visibility.Hidden;
        }
    }
}
