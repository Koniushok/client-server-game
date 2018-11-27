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
    class ViewModelMenuSettings : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        System.Windows.Controls.TextBlock textResult;
        public ViewModelMenuSettings(System.Windows.Controls.TextBlock textResult)
        {
            Name = MainClient.Profile.Name;
            Surname = MainClient.Profile.SurName;
            this.textResult = textResult;
        }
        public void OnPropertyChanget([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        bool test;
        bool PasswordTest = true;
        private string password;
        private string name;
        private string surname;

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
                        PasswordTest = true;
                        OnPropertyChanget("Password");
                        OnPropertyChanget("Name");
                        OnPropertyChanget("Surname");
                        p = false;
                        if (test)
                        {
                            UpData();
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
                    switch (columnName)
                    {
                        case "Password":
                            {
                                if (string.IsNullOrEmpty(password))
                                {
                                    PasswordTest = false;
                                }
                                else
                                {
                                    if (password.Length < 4)
                                    {
                                        PasswordTest = false;
                                        return "Пароль слишком маленький";
                                    }
                                }
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

        void UpData()
        {
            textResult.Visibility = System.Windows.Visibility.Visible;
            MainClient.UpData(PasswordTest, Password, Name, Surname);
            MainClient.Profile.Name = name;
            MainClient.Profile.SurName = Surname;
        }


    }
}

