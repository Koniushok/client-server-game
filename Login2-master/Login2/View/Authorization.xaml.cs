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

namespace Login
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : UserControl
    {
        ViewModelAuthorization val;
        public Authorization()
        {
            InitializeComponent();
            val = new ViewModelAuthorization(Passwor);
            DataContext = val;
            Binding passwordBinding = new Binding("Password");
            passwordBinding.Source = val;
            passwordBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            passwordBinding.Mode = BindingMode.TwoWay;
            passwordBinding.ValidatesOnDataErrors = true;

            Passwor.SetBinding(SecurePasswordProperty, passwordBinding);


        }

        public static readonly DependencyProperty SecurePasswordProperty =
   DependencyProperty.RegisterAttached("Password", typeof(string), typeof(MainWindow));


        private void Passwor_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            val.Password = Passwor.Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            val.Password = Passwor.Password;                 
        }

        

    }
}
