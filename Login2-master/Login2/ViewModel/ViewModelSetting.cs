using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Client;
using System.Windows;



namespace Login
{
    class ViewModelSetting : INotifyPropertyChanged, IDataErrorInfo
    {
       
        #region View
        MaterialDesignThemes.Wpf.PackIcon KindServerStatus;
        System.Windows.Controls.Grid BlackGrid;
        public ViewModelSetting(MaterialDesignThemes.Wpf.PackIcon kindServerStatus , System.Windows.Controls.Grid blackGrid)
        {
            KindServerStatus = kindServerStatus;
            BlackGrid = blackGrid;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanget([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private string port="3333";
        private string ip="169.254.139.45";

        string errorPort;
        string errorIP;

        public string Port
        {
            get
            {
                return port;
            }
            set
            {

                port = value;
                OnPropertyChanget();
            }
        }

        public string IP
        {
            get
            {
                return ip;
            }
            set
            {
                ip = value;
                OnPropertyChanget();
            }
        }

        bool p;

        public string Error
        {
            get { return null; }
        }

        private DelegateCommand _buttonClick;
        public DelegateCommand ButtonClick
        {
            get
            {

                if (_buttonClick == null)
                    _buttonClick = new DelegateCommand(o =>
                    {
                        Connect();                      
                    });

                return _buttonClick;
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (p)
                {
                    if (columnName == "Port")
                    {

                        return errorPort;

                    }
                    if (columnName == "IP")
                    {
                        return errorIP;

                    }
                }
                return null;
            }
        }
        #endregion

        void Connect()
        {
            int ports;

            try
            {
                ports = int.Parse(port);
                try
                {
                    Server serv = new Server(ports, ip);
                    if (MainClient.Start(serv))
                    {
                        BlackGrid.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        p = true;
                        errorIP = "IP не доступен";
                        OnPropertyChanget("IP");
                        p = false;
                    }

                }
                catch
                {
                    p = true;
                    errorIP = "IP не доступен";
                    OnPropertyChanget("IP");
                    p = false;
                }
            }
            catch
            {
                p = true;
                errorPort = "Не верный формат";
                OnPropertyChanget("Port");
                p = false;
            }

            if (MainClient.Status)
            {
                KindServerStatus.Foreground = new SolidColorBrush(Colors.GreenYellow);
            }
        }
      
    }
}
