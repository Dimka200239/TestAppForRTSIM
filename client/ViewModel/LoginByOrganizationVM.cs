using client.Common;
using client.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace client.ViewModel
{
    public class LoginByOrganizationVM
    {
        private readonly Frame MainFrame;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand GoBackFromRegByOrgCommand { get; }

        public LoginByOrganizationVM(Frame MainFrame)
        {
            this.MainFrame = MainFrame;
            GoBackFromRegByOrgCommand = new RelayCommand(GoBackFromRegByOrg);
        }

        private void GoBackFromRegByOrg(object parameter)
        {
            MainFrame.Content = new UserLoginAndRegistrationPageView(MainFrame);
        }
    }
}
