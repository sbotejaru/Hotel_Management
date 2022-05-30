using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using TEMA_3.Layers.BLL;
using TEMA_3.Views;

namespace TEMA_3
{
    public class LoginVM : INotifyPropertyChanged
    {
        private String username;
        private String password;
        private String loginStatus;
        MainVM mainVM;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public String Username
        {
            get { return username; }
            set { username = value; NotifyPropertyChanged("Username"); }
        }

        public String LoginStatus
        {
            get { return loginStatus; }
            set { loginStatus = value; NotifyPropertyChanged("LoginStatus"); }
        }

        public String Password
        {
            get { return password; }
            set { password = value; NotifyPropertyChanged("Password"); }
        }

        public RelayCommand LoginCommand { get; set; }

        public LoginVM(MainVM vm)
        {
            mainVM = vm;
            LoginCommand = new RelayCommand(o =>
            {

                UserBLL user = new UserBLL();
                User user1 = new User();
                user1 = user.GetUser(Username);
                if (user1 != null)
                {
                    String password1;
                    password1 = user1.Password;
                    password1 = Regex.Replace(password1, @"\s+", "");
                    if (password1 != Password)
                    {
                        //login failed
                        LoginStatus = " Login Failed";
                    }
                    else
                    {
                        //send user visibilities login succesful
                        LoginStatus = " Login Succeded";
                        vm.CurrentUser = user1;
                        if (user1.RoleID == 3)
                        {

                            AdminBLL adminBLL = new AdminBLL();
                            Admin admin = new Admin();
                            foreach (Admin a in adminBLL.AdminList)
                            {
                                if (a.UserID == user1.UserID)
                                    admin = a;
                            }

                            AdminVM adminVM = new AdminVM(admin);
                            AdminView adminView = new AdminView(adminVM);
                            adminView.DataContext = adminVM;
                            App.Current.MainWindow.Close();
                            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).Close();


                            App.Current.MainWindow = adminView;
                            adminView.Show();
                        }

                        if (user1.RoleID == 2)
                        {
                            EmployeeBLL employeeBLL = new EmployeeBLL();
                            Employee employee = new Employee();

                            foreach (Employee e in employeeBLL.EmployeeList)
                            {
                                if (e.UserID == user1.UserID)
                                    employee = e;
                            }

                            EmployeeVM employeeVM = new EmployeeVM(employee);
                            App.Current.MainWindow.Close();
                        }

                        vm.GuestVis = System.Windows.Visibility.Hidden;
                        vm.CustomerVis = System.Windows.Visibility.Visible;
                    }

                }
                else
                {
                    // no username send to register
                    LoginStatus = " No such username!";
                }


            });

        }




    }
}
