using client.Common;
using client.Model;
using client.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class LoginByOrganizationVM : INotifyPropertyChanged
    {
        private readonly Frame MainFrame;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<string> _items;
        private string _selectedItem;

        private ObservableCollection<string> _itemsEmployee;
        private string _selectedItemEmployee;

        private string _password;

        public ICommand GoBackFromRegByOrgCommand { get; }
        public ICommand LoginCommand { get; }

        public LoginByOrganizationVM(Frame MainFrame)
        {
            LoadAllOrganization();
            this.MainFrame = MainFrame;
            GoBackFromRegByOrgCommand = new RelayCommand(GoBackFromRegByOrg);
            LoginCommand = new RelayCommand(Login);
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

        public ObservableCollection<string> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
            }
        }
        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
                ItemsEmployee = null;
                LoadAllEmployeeFromOrganization();
            }
        }

        public ObservableCollection<string> ItemsEmployee
        {
            get { return _itemsEmployee; }
            set
            {
                _itemsEmployee = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemsEmployee)));
            }
        }
        public string SelectedItemEmployee
        {
            get { return _selectedItemEmployee; }
            set
            {
                _selectedItemEmployee = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItemEmployee)));
            }
        }

        private void GoBackFromRegByOrg(object parameter)
        {
            MainFrame.Content = new UserLoginAndRegistrationPageView(MainFrame);
        }

        private async void LoadAllOrganization()
        {
            try
            {
                List<Organization> organizations = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7289");

                    var response = await client.GetAsync($"/api/organizationController/getAllOrganizations");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<Organization>>(responseContent);

                        if (result != null)
                        {
                            if (Items == null)
                            {
                                Items = new ObservableCollection<string>();
                            }

                            foreach (Organization organization in result)
                            {
                                Items.Add(organization.nameOrg);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("При получении данных произошла ошибка...");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void LoadAllEmployeeFromOrganization()
        {
            try
            {
                List<Employee> employees = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7289");

                    var response = await client.GetAsync($"/api/employeeOrganizationMapController/getAllEmployeeInOrganization/{SelectedItem}");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<Employee>>(responseContent);

                        if (result != null)
                        {
                            if (ItemsEmployee == null)
                            {
                                ItemsEmployee = new ObservableCollection<string>();
                            }

                            foreach (Employee employee in result)
                            {
                                ItemsEmployee.Add(employee.loginEmp);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("При получении данных произошла ошибка...");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Login (object parameter)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7289");

                    var response = await client.GetAsync($"/api/employeeController/getEmployeeByLoginAndPassword/{SelectedItemEmployee}/{Password}");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Employee>(responseContent);

                        if (result != null)
                        {
                            var responseSecond = await client.GetAsync($"/api/employeeOrganizationMapController/getOrganizationByEmployeeLogin/{SelectedItemEmployee}/{true}");

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
    }
}
