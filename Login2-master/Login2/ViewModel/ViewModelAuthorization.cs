using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Forms;
using Client;

namespace Login
{
    public class DelegateCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public DelegateCommand(Action<object> execute,
                       Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }

    public class ViewModelAuthorization : INotifyPropertyChanged, IDataErrorInfo
    {
        PasswordBox PasswordBox;
        public ViewModelAuthorization(PasswordBox passwordBox)
        {
            PasswordBox = passwordBox;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanget([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private string login ;
        private string password ;
        bool test;

        public string Login
        {
            get
            {
                return login;
            }
            set
            {

                login = value;
                OnPropertyChanget();
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
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
                        p = true;
                        test = true;
                        OnPropertyChanget("Login");
                        OnPropertyChanget("Password");
                        p = false;
                        if (test)
                        {
                            Authorcation();
                        }
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
                    if (columnName == "Login")
                    {

                        if (string.IsNullOrEmpty(this.login))
                        {
                            test = false;
                            return "Логин не указан";
                        }                        

                    }
                    if (columnName == "Password")
                    {

                        if (string.IsNullOrEmpty(this.password))
                        {
                            test = false;
                            return "Пароль не указан";
                            
                        }
                    }
                }
                return null;
            }
        }

        void Authorcation()
        {
            if(!MainClient.Authoriztion(login, password))
            {
                OccasionHandler.Authoriztion(false,"Сервер не подключён");
            }
        }
    }
}

