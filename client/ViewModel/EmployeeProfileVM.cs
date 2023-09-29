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
using client.Model;
using System.Windows;

namespace client.ViewModel
{
    public class EmployeeProfileVM : INotifyPropertyChanged
    {
        private readonly Frame MainFrame;
        private readonly Employee employee;
        private readonly Organization organization;

        public event PropertyChangedEventHandler PropertyChanged;

        private string _profileName;
        private string _profileSerName;
        private string _profilePatronymic;
        private string _profileLogin;
        private string _profileOrganization;

        public ICommand GoBackFromProfileCommand { get; }

        public EmployeeProfileVM(Frame MainFrame, Employee employee, Organization organization)
        {
            this.MainFrame = MainFrame;
            this.employee  = employee;
            this.organization = organization;
            LoadInfo();
            GoBackFromProfileCommand = new RelayCommand(GoBackFromProfile);
        }

        public string ProfileName
        {
            get { return _profileName; }
            set
            {
                _profileName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProfileName)));
            }
        }

        public string ProfileSerName
        {
            get { return _profileSerName; }
            set
            {
                _profileSerName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProfileSerName)));
            }
        }

        public string ProfilePatronymic
        {
            get { return _profilePatronymic; }
            set
            {
                _profilePatronymic = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProfilePatronymic)));
            }
        }

        public string ProfileLogin
        {
            get { return _profileLogin; }
            set
            {
                _profileLogin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProfileLogin)));
            }
        }

        public string ProfileOrganization
        {
            get { return _profileOrganization; }
            set
            {
                _profileOrganization = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProfileOrganization)));
            }
        }

        private void GoBackFromProfile(object parameter)
        {
            MainFrame.Content = new UserLoginAndRegistrationPageView(MainFrame);
        }

        private void LoadInfo()
        {
            ProfileName = employee.fisrstName;
            ProfileSerName = employee.middleName;
            ProfilePatronymic = employee.lastName;
            ProfileLogin = employee.loginEmp;
            ProfileOrganization = organization.nameOrg;
        }
    }
}
