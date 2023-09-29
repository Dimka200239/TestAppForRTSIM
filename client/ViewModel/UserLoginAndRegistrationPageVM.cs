using client.Common;
using client.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace client.ViewModel
{
    public class UserLoginAndRegistrationPageVM : INotifyPropertyChanged
    {
        private string _login;
        private string _password;
        private readonly Frame MainFrame;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoginCommand { get; }
        public ICommand RegCommand { get; }
        public ICommand LoginByOrgCommand { get; }

        public UserLoginAndRegistrationPageVM(Frame MainFrame)
        {
            this.MainFrame = MainFrame;
            LoginCommand = new RelayCommand(Authorize);
            RegCommand = new RelayCommand(Registration);
            LoginByOrgCommand = new RelayCommand(AuthorizeByOrg);
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Login)));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }

        private async void Authorize(object parameter)
        {

        }

        private async void Registration(object parameter)
        {
            MainFrame.Content = new RegistrationPageView(MainFrame);
        }

        private async void AuthorizeByOrg(object parameter)
        {
            MainFrame.Content = new LoginByOrganizationView(MainFrame);
        }
    }
}
