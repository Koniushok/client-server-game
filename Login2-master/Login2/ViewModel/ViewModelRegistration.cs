using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Client;

namespace Login
{
    class ViewModelRegistration : INotifyPropertyChanged, IDataErrorInfo
    {
        public ViewModelRegistration()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanget([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        bool test;
        private string login;
        private string password;
        private string name;
        private string surname;

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

        public string Name
        {
            get
            {
                return name;
            }
            set
            {

                name = value;
                OnPropertyChanget();
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
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
                        OnPropertyChanget("Name");
                        OnPropertyChanget("Surname");
                        p = false;
                        if (test)
                        {
                            Registration();
                        }
                    });

                return _buttonClick;
            }
        }
        string alf = "йцукенгшщзхъфывапролджэячсмитьбю";
        public string this[string columnName]
        {
            get
            {
                if (p)
                {
                    switch (columnName)
                    {
                        case "Login":
                            {
                                if (string.IsNullOrEmpty(login))
                                {
                                    test = false;
                                    return "Пустое поле";
                                }
                                break;
                            }
                        case "Password":
                            {
                                if (string.IsNullOrEmpty(password))
                                {
                                    test = false;
                                    return "Пустое поле";
                                }
                                if (password.Length < 4)
                                {
                                    test = false;
                                    return "Пароль слишком маленький";
                                }
                               //for (int i = 0; i < password.Length; i++)
                                //{

                               // }
                                break;
                            }
                        case "Name":
                            {
                                if (string.IsNullOrEmpty(Name))
                                {
                                    test = false;
                                    return "Пустое поле";
                                }
                                break;
                            }
                        case "Surname":
                            {
                                if (string.IsNullOrEmpty(Surname))
                                {
                                    test = false;
                                    return "Пустое поле";
                                }
                                break;
                            }

                    }


                }
                return null;
            }
        }

        void Registration()
        {
            MainClient.Registration(login, password, name, surname).ToString();
        }


    }
}

