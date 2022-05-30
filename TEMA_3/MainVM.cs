using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Navigation;
using System.Collections;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Data.SqlClient;
using System.Data;
using TEMA_3.Layers.BLL;
using TEMA_3.Views;


namespace TEMA_3
{
    public class MainVM : INotifyPropertyChanged
    {
        private String username;
        private String password;
        private int role;
        private UserBLL userBLL;
        private CustomerBLL customerBLL;
        private AdminView adminView;
        private EmployeeView employeeView;
        private EmployeeVM employeeVM;
        private Visibility guestVis;
        private Visibility customerVis;
        private DateTime dateFrom;
        private DateTime dateTo;
        private String newString="";
        private User currentUser;
        public String Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public Visibility GuestVis
        {
            get { return guestVis; }
            set { guestVis = value; NotifyPropertyChanged("GuestVis"); }
        }


        public Visibility CustomerVis
        {
            get { return customerVis; }
            set { customerVis = value; NotifyPropertyChanged("CustomerVis"); }
        }

        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; NotifyPropertyChanged("CurrentUser"); }
        }

        public String Username
        {
            get { return username; }
            set { username = value; NotifyPropertyChanged("Username"); }
        }

        public String NewString
        {
            get { return newString; }
            set { newString = value; NotifyPropertyChanged("NewString"); }
        }

        public String Password
        {
            get { return password; }
            set { password = value; NotifyPropertyChanged("Password"); }
        }
        public int Role
        {
            get { return role; }
            set { role = value; NotifyPropertyChanged("Role"); }
        }

        public DateTime DateFrom
        {
                get { return dateFrom; }
                set { dateFrom = value; NotifyPropertyChanged("DateFrom"); }
        }

        public DateTime DateTo
        {
            get { return dateTo; }
            set { dateTo = value; NotifyPropertyChanged("DateTo"); }
        }

        public RelayCommand SeeDatesSelected { get; set; }
        public RelayCommand CreateUser { get; set; }
        public RelayCommand AdminViewCommand { get; set; }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }
        public RelayCommand AdminViewCommand1 { get; set; }
        public RelayCommand MyBookingsCommand { get; set; }
        public RelayCommand AllBookingsCommand { get; set; }


        public MainVM()
        {
            userBLL = new UserBLL();
            customerBLL = new CustomerBLL();
            DateFrom = DateTime.Now;
            DateTo = DateTime.Now;
            GuestVis = Visibility.Visible;
            CustomerVis = Visibility.Hidden;
            

            CreateUser = new RelayCommand(o =>
            {
                userBLL.CreateUser(Username, Password, Role);
            });

            AdminViewCommand = new RelayCommand(o =>
            {
                /*adminView = new AdminView();
                adminView.Show();*/
                employeeVM = new EmployeeVM(null);
            });

            SeeDatesSelected = new RelayCommand(o => {
                NewString = DateFrom.ToShortDateString() + " " + DateTo.Date.ToShortDateString();
            
            });


            LoginCommand = new RelayCommand(o => {

               

                LoginView loginView = new LoginView(this);
                LoginVM loginVM = new LoginVM(this);
                loginView.DataContext = loginVM;
                loginView.Show();

            });

            RegisterCommand = new RelayCommand(o => {



                RegisterView registerView = new RegisterView(this);
                RegisterVM registerVM  = new RegisterVM(this);
                registerView.DataContext = registerVM;
                registerView.Show();

            });

            MyBookingsCommand = new RelayCommand(o => {

                

                BookingsView bookingsView = new BookingsView();
                Customer customer = new Customer();
                if (CurrentUser != null)
                {
                    foreach (Customer c in customerBLL.CustomerList)
                    {
                        if (c.UserID == CurrentUser.UserID)
                            customer = c;
                    }
                }
                else
                    customer = null;
                CustomerVM customerVM = new CustomerVM(customer);
                bookingsView.DataContext = customerVM;
                //App.Current.MainWindow.Close();
                //App.Current.MainWindow = bookingsView;
                bookingsView.Show();
                bookingsView.Data_Grid.Columns[0].Visibility = Visibility.Hidden;
                bookingsView.Data_Grid.Columns[1].Visibility = Visibility.Hidden;
                bookingsView.Data_Grid.Columns[5].Visibility = Visibility.Hidden;
                bookingsView.Data_Grid.Columns[7].Visibility = Visibility.Hidden;
                bookingsView.Data_Grid.Columns[8].Visibility = Visibility.Hidden;


            });

            AllBookingsCommand = new RelayCommand(o => {


                AllBookingsView allBookingsView = new AllBookingsView();
                Customer customer = new Customer();
                if (CurrentUser != null)
                {
                    foreach (Customer c in customerBLL.CustomerList)
                    {
                        if (c.UserID == CurrentUser.UserID)
                            customer = c;
                    }
                }
                else
                    customer = null;
                BookingsVM bookingsVM = new BookingsVM(customer, DateFrom, DateTo);
                allBookingsView.DataContext = bookingsVM;
                //App.Current.MainWindow.Close();
               // App.Current.MainWindow = allBookingsView;
                allBookingsView.Show();
                allBookingsView.Data_Grid.Columns[0].Visibility = Visibility.Hidden;
                allBookingsView.Data_Grid.Columns[3].Visibility = Visibility.Hidden;
                allBookingsView.Data_Grid.Columns[4].Visibility = Visibility.Hidden;
                allBookingsView.Data_Grid.Columns[6].Visibility = Visibility.Hidden;

            });





        }

        



    }
}
