using client.Common;
using client.Model;
using client.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7289");

                    var response = await client.GetAsync($"/api/employeeController/getEmployeeByLoginAndPassword/{Login}/{Password}");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Employee>(responseContent);

                        if (result != null)
                        {
                            var responseSecond = await client.GetAsync($"/api/employeeOrganizationMapController/getOrganizationByEmployeeLogin/{Login}/{true}");

                            if (responseSecond.IsSuccessStatusCode)
                            {
                                string responseContentSecond = await responseSecond.Content.ReadAsStringAsync();
                                var resultSecond = JsonConvert.DeserializeObject<Organization>(responseContentSecond);

                                if (resultSecond != null)
                                {
                                    MainFrame.Content = new EmployeeProfileView(MainFrame, result, resultSecond);
                                }
                            }
                            else
                            {
                                MessageBox.Show("При авторизации произошла ошибка...");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль");
                        }
                    }
                    else
                    {
                        MessageBox.Show("При авторизации произошла ошибка...");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
