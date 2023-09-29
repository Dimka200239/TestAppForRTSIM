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
    public class EmployeeProfileVM : INotifyPropertyChanged
    {
        private readonly Frame MainFrame;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand GoBackFromProfileCommand { get; }

        public EmployeeProfileVM(Frame MainFrame)
        {
            this.MainFrame = MainFrame;
            GoBackFromProfileCommand = new RelayCommand(GoBackFromProfile);
        }

        private void GoBackFromProfile(object parameter)
        {
            MainFrame.Content = new UserLoginAndRegistrationPageView(MainFrame);
        }
    }
}
