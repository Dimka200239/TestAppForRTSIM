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
    public class RegistrationPageVM : INotifyPropertyChanged
    {
        private readonly Frame MainFrame;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand GoBackFromRegCommand { get; }

        public RegistrationPageVM(Frame MainFrame)
        {
            this.MainFrame = MainFrame;
            GoBackFromRegCommand = new RelayCommand(GoBackFromReg);
        }

        private void GoBackFromReg(object parameter)
        {
            MainFrame.Content = new UserLoginAndRegistrationPageView(MainFrame);
        }
    }
}
