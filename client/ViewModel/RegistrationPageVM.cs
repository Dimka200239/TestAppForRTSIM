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
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace client.ViewModel
{
    public class RegistrationPageVM : INotifyPropertyChanged
    {
        private readonly Frame MainFrame;

        public event PropertyChangedEventHandler PropertyChanged;

        private string _addNewName;
        private string _addNewMiddleName;
        private string _addNewLastName;
        private string _addNewLogin;
        private string _addNewPassword;
        private ObservableCollection<string> _items;
        private string _selectedItem;

        public ICommand GoBackFromRegCommand { get; }
        public ICommand AddNewCommand { get; }

        public RegistrationPageVM(Frame MainFrame)
        {
            LoadAllOrganization();
            this.MainFrame = MainFrame;
            GoBackFromRegCommand = new RelayCommand(GoBackFromReg);
            AddNewCommand = new RelayCommand(AddNew);
        }

        public string AddNewName
        {
            get { return _addNewName; }
            set
            {
                _addNewName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewName)));
            }
        }

        public string AddNewSerName
        {
            get { return _addNewMiddleName; }
            set
            {
                _addNewMiddleName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewSerName)));
            }
        }

        public string AddNewPatronymic
        {
            get { return _addNewLastName; }
            set
            {
                _addNewLastName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewPatronymic)));
            }
        }

        public string AddNewLogin
        {
            get { return _addNewLogin; }
            set
            {
                _addNewLogin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewLogin)));
            }
        }

        public string AddNewPassword
        {
            get { return _addNewPassword; }
            set
            {
                _addNewPassword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewPassword)));
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
            }
        }


        private void GoBackFromReg(object parameter)
        {
            MainFrame.Content = new UserLoginAndRegistrationPageView(MainFrame);
        }

        private async void AddNew(object parameter)
        {
            try
            {
                Employee employee = new Employee();
                employee.fisrstName = AddNewName;
                employee.middleName = AddNewSerName;
                employee.lastName = AddNewPatronymic;
                employee.loginEmp = AddNewLogin;
                employee.passwordEmp = AddNewPassword;

                EmployeeOrganizationMap employeeOrganizationMap = new EmployeeOrganizationMap();
                employeeOrganizationMap.loginEmp = AddNewLogin;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7289");

                    var response = await client.GetAsync($"/api/organizationController/getOrganizationByName/{SelectedItem}");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Organization>(responseContent);

                        if (result != null)
                        {
                            employeeOrganizationMap.orgId = result.orgId;
                        }
                    }
                    else
                    {
                        MessageBox.Show("При регистрации произошла ошибка...");
                        return;
                    }
                }

                new Common.DataValidationContext().Validate(employee);

                new Common.DataValidationContext().Validate(employeeOrganizationMap);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7289");

                    var response = await client.GetAsync($"/api/employeeController/getEmployeeByLogin/{AddNewLogin}");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Employee>(responseContent);

                        if (result == null)
                        {
                            var jsonFirst = JsonConvert.SerializeObject(employee);
                            var contentFirst = new StringContent(jsonFirst, Encoding.UTF8, "application/json");
                            var responseFirst = await client.PostAsync($"/api/employeeController/addNewEmployee", contentFirst);

                            if (responseFirst.IsSuccessStatusCode)
                            {
                                var jsonSecond = JsonConvert.SerializeObject(employeeOrganizationMap);
                                var contentSecond = new StringContent(jsonSecond, Encoding.UTF8, "application/json");
                                var responseSecond = await client.PostAsync($"/api/employeeOrganizationMapController/addNewEmployeeOrganizationMap", contentSecond);

                                MessageBox.Show("Успешная регистрация");
                                AddNewName = "";
                                AddNewSerName = "";
                                AddNewPatronymic = "";
                                AddNewLogin = "";
                                AddNewPassword = "";
                            }
                            else
                            {
                                MessageBox.Show("При регистрации произошла ошибка...");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Данный логин уже занят!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("При регистрации произошла ошибка...");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
    }
}
