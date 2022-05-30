using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using TEMA_3.Layers.BLL;
using TEMA_3.Views;

namespace TEMA_3
{
    public class AdminVM : INotifyPropertyChanged
    {
        private Visibility menuVis;
        private Visibility userVis;
        private Visibility customerVis;
        private Visibility employeeVis;
        private Visibility bookingVis;
        private Visibility roomVis;
        private Visibility backVis;
        private Visibility empVis;
        private Visibility errorVis;
        private String currentAdminName;
        private String username;
        private String password;
        private String name;
        private String roomType;
        private CreateUser createUserWindow;
        private ReadUsers readUsersWindow;
        private CreateCustomer createCustomerWindow;
        private ReadAllCustomers readAllCustomers;
        private CreateEmployee createEmployeeWindow;
        private ReadEmployees readEmployees;
        private CreateBooking createBookingWindow;
        private ReadBookings readBookingsWindow;
        private CreateRoom createRoomWindow;
        private ReadRooms readRoomsWindow;
        private UpdateRoom updateRoomWindow;
        private UpdateBooking updateBookingWindow;
        private DateTime dateFrom;
        private DateTime dateTo;

        private int roomNr;
        private int roleID;
        private int selectedUser;
        private double roomPrice;

        public UserBLL userBLL;
        public BookingBLL bookingBLL;
        public CustomerBLL customerBLL;
        public EmployeeBLL employeeBLL;
        public RoomBLL roomBLL;
        public AdminBLL adminBLL;

        private Admin CurrentAdmin { get; set; }
        public User CurrentUser { get; set; }
        public Employee CurrentEmployee { get; set; }

        public ObservableCollection<User> CurrentUserList { get; set; }
        public ObservableCollection<Customer> CurrentCustomerList { get; set; }
        public ObservableCollection<Employee> CurrentEmployeeList { get; set; }
        public ObservableCollection<Booking> CurrentBookingList { get; set; }
        public ObservableCollection<Room> CurrentRoomList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region binding vars

        public int RoomNr
        {
            get { return roomNr; }
            set { roomNr = value; NotifyPropertyChanged("RoomNr"); }
        }
        public String RoomType
        {
            get { return roomType; }
            set { roomType = value; NotifyPropertyChanged("RoomType"); }
        }
        public double RoomPrice
        {
            get { return roomPrice; }
            set { roomPrice = value; NotifyPropertyChanged("RoomPrice"); }
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
        public String CurrentAdminName
        {
            get { return currentAdminName; }
            set { currentAdminName = value; NotifyPropertyChanged("CurrentAdminName"); }
        }
        public String Username
        {
            get { return username; }
            set { username = value; NotifyPropertyChanged("Username"); }
        }
        public String Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged("Name"); }
        }
        public String Password
        {
            get { return password; }
            set { password = value; NotifyPropertyChanged("Password"); }
        }
        public int RoleID
        {
            get { return roleID; }
            set { roleID = value; NotifyPropertyChanged("RoleID"); }
        }
        public Visibility MenuVis
        {
            get { return menuVis; }
            set { menuVis = value; NotifyPropertyChanged("MenuVis"); }
        }
        public Visibility UserVis
        {
            get { return userVis; }
            set { userVis = value; NotifyPropertyChanged("UserVis"); }
        }
        public Visibility EmployeeVis
        {
            get { return employeeVis; }
            set { employeeVis = value; NotifyPropertyChanged("EmployeeVis"); }
        }
        public Visibility CustomerVis
        {
            get { return customerVis; }
            set { customerVis = value; NotifyPropertyChanged("CustomerVis"); }
        }
        public Visibility BookingVis
        {
            get { return bookingVis; }
            set { bookingVis = value; NotifyPropertyChanged("BookingVis"); }
        }
        public Visibility ErrorVis
        {
            get { return errorVis; }
            set { errorVis = value; NotifyPropertyChanged("ErrorVis"); }
        }
        public Visibility EmpVis
        {
            get { return empVis; }
            set { empVis = value; NotifyPropertyChanged("EmpVis"); }
        }
        public Visibility RoomVis
        {
            get { return roomVis; }
            set { roomVis = value; NotifyPropertyChanged("RoomVis"); }
        }
        public Visibility BackVis
        {
            get { return backVis; }
            set { backVis = value; NotifyPropertyChanged("BackVis"); }
        }
        public int SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; NotifyPropertyChanged("SelectedUser"); }
        }
        #endregion

        public RelayCommand ManageUsers { get; set; }
        public RelayCommand ManageCustomers { get; set; }
        public RelayCommand ManageEmployees { get; set; }
        public RelayCommand ManageBookings { get; set; }
        public RelayCommand ManageRooms { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand CreateUserCommand { get; set; }
        public RelayCommand CreateRoomCommand { get; set; }
        public RelayCommand CreateCustomerCommand { get; set; }
        public RelayCommand CreateEmployeeCommand { get; set; }
        public RelayCommand CreateBookingCommand { get; set; }
        public RelayCommand SubmitUserCreation { get; set; }
        public RelayCommand SubmitRoomCreation { get; set; }
        public RelayCommand SubmitRoomUpdateCommand { get; set; }
        public RelayCommand SubmitCustomerCreation { get; set; }
        public RelayCommand SubmitEmployeeCreation { get; set; }
        public RelayCommand SubmitBookingCreation { get; set; }
        public RelayCommand SubmitBookingUpdateCommand { get; set; }
        public RelayCommand ReadAllUsersCommand { get; set; }
        public RelayCommand ReadAllRoomsCommand { get; set; }
        public RelayCommand ReadAllCustomersCommand { get; set; }
        public RelayCommand ReadAllEmployeesCommand { get; set; }
        public RelayCommand ReadBookingsCommand { get; set; }
        public RelayCommand UpdateUserPasswordCommand { get; set; }
        public RelayCommand UpdateCustomerNameCommand { get; set; }
        public RelayCommand UpdateEmployeeNameCommand { get; set; }
        public RelayCommand UpdateUserRoleCommand { get; set; }
        public RelayCommand UpdateRoomCommand { get; set; }
        public RelayCommand UpdateBookingCommand { get; set; }
        public RelayCommand DeleteUserCommand { get; set; }
        public RelayCommand DeleteRoomCommand { get; set; }
        public RelayCommand DeleteCustomerCommand { get; set; }
        public RelayCommand DeleteEmployeeCommand { get; set; }
        public RelayCommand DeleteBookingCommand { get; set; }

        public void ResetVis()
        {
            MenuVis = Visibility.Visible;
            UserVis = Visibility.Hidden;
            EmployeeVis = Visibility.Hidden;
            CustomerVis = Visibility.Hidden;
            BookingVis = Visibility.Hidden;
            RoomVis = Visibility.Hidden;
            BackVis = Visibility.Hidden;
        }

        public AdminVM(Admin admin)
        {
            if (admin != null)
            {
                CurrentAdmin = admin;
                CurrentAdminName = CurrentAdmin.Name;
                EmpVis = Visibility.Visible;
            }
            else
            {
                CurrentAdmin = null;
                EmpVis = Visibility.Hidden;                
            }

            MenuVis = Visibility.Visible;
            UserVis = Visibility.Hidden;
            EmployeeVis = Visibility.Hidden;
            CustomerVis = Visibility.Hidden;
            BookingVis = Visibility.Hidden;
            RoomVis = Visibility.Hidden;
            BackVis = Visibility.Hidden;
            ErrorVis = Visibility.Hidden;

            userBLL = new UserBLL();
            employeeBLL = new EmployeeBLL();
            adminBLL = new AdminBLL();
            customerBLL = new CustomerBLL();
            roomBLL = new RoomBLL();
            bookingBLL = new BookingBLL();

            RoleID = 1;
            RoomNr = 0;
            RoomType = "";
            RoomPrice = 0;
            DateFrom = DateTime.Now;
            DateTo = DateTime.Now;

            ManageUsers = new RelayCommand(o =>
            {
                MenuVis = Visibility.Hidden;
                UserVis = Visibility.Visible;
                BackVis = Visibility.Visible;
            });

            BackCommand = new RelayCommand(o =>
            {
                ResetVis();
            });

            CreateUserCommand = new RelayCommand(o =>
            {
                createUserWindow = new CreateUser(this);
                createUserWindow.Show();
            });

            SubmitUserCreation = new RelayCommand(o =>
            {
                userBLL.CreateUser(Username, Password, RoleID);
                int userID = userBLL.UserList.Last().UserID;
                switch (RoleID)
                {
                    case 1:
                        customerBLL.CreateCustomer(userID, Name);
                        break;
                    case 2:
                        employeeBLL.CreateEmployee(userID, Name);
                        break;
                    case 3:
                        adminBLL.CreateAdmin(userID, Name);
                        break;
                }

                Username = Password = Name = "";
                RoleID = 1;
            });

            ReadAllUsersCommand = new RelayCommand(o =>
            {
                CurrentUserList = userBLL.ReadAllUsers();
                readUsersWindow = new ReadUsers(this);
                readUsersWindow.Show();
                readUsersWindow.Data_Grid.Columns[0].Visibility = Visibility.Hidden;
                readUsersWindow.Data_Grid.Columns[2].Visibility = Visibility.Hidden;
                for (int index = 5; index < readUsersWindow.Data_Grid.Columns.Count; ++index)
                {
                    readUsersWindow.Data_Grid.Columns[index].Visibility = Visibility.Hidden;
                }
                if (EmpVis == Visibility.Hidden)
                {
                    readUsersWindow.Data_Grid.Columns[2].Visibility = Visibility.Hidden;
                    readUsersWindow.Data_Grid.Columns[3].Visibility = Visibility.Hidden;
                }
            });

            UpdateUserPasswordCommand = new RelayCommand(o =>
            {
                if (Password != "")
                {
                    User user = userBLL.ReadAllUsers()[SelectedUser];
                    userBLL.UpdateUserPassword(user.UserID, Password);
                    CurrentUserList = userBLL.ReadAllUsers();
                }
                Password = "";
            });

            UpdateUserRoleCommand = new RelayCommand(o =>
            {
                if (RoleID != 0)
                {
                    User user = userBLL.ReadAllUsers()[SelectedUser];
                    userBLL.UpdateUserRole(user.UserID, RoleID);
                    CurrentUserList = new ObservableCollection<User>();
                    CurrentUserList = userBLL.ReadAllUsers();
                }
                RoleID = 1;
            });

            DeleteUserCommand = new RelayCommand(o =>
            {
                User user = userBLL.ReadAllUsers()[SelectedUser];
                userBLL.DeleteUser(user.UserID);
                CurrentUserList = userBLL.ReadAllUsers();
            });

            SubmitCustomerCreation = new RelayCommand(o =>
            {
                userBLL.CreateUser(Username, Password, 1);
                customerBLL.CreateCustomer(userBLL.UserList.Last().UserID, Name);
                Username = Password = Name = "";
            });

            ManageCustomers = new RelayCommand(o =>
           {
               MenuVis = Visibility.Hidden;
               CustomerVis = Visibility.Visible;
               BackVis = Visibility.Visible;
           });

            CreateCustomerCommand = new RelayCommand(o =>
            {
                createCustomerWindow = new CreateCustomer(this);
                createCustomerWindow.Show();
            });

            ReadAllCustomersCommand = new RelayCommand(o =>
            {
                CurrentCustomerList = customerBLL.ReadAllCustomers();
                readAllCustomers = new ReadAllCustomers(this);
                readAllCustomers.Show();
                readAllCustomers.Data_Grid.Columns[0].Visibility = Visibility.Hidden;
                readAllCustomers.Data_Grid.Columns[1].Visibility = Visibility.Hidden;
                for (int index = 4; index < readAllCustomers.Data_Grid.Columns.Count(); ++index)
                    readAllCustomers.Data_Grid.Columns[index].Visibility = Visibility.Hidden;
                
            });

            UpdateCustomerNameCommand = new RelayCommand(o =>
            {
                Customer customer = customerBLL.ReadAllCustomers()[SelectedUser];
                customerBLL.UpdateCustomer(customer.CustomerID, Name);
                CurrentCustomerList = customerBLL.ReadAllCustomers();
                Name = "";
            });

            DeleteCustomerCommand = new RelayCommand(o =>
            {
                Customer customer = customerBLL.ReadAllCustomers()[SelectedUser];
                customerBLL.DeleteCustomer(customer.CustomerID);
                CurrentCustomerList = customerBLL.ReadAllCustomers();
            });

            ManageEmployees = new RelayCommand(o =>
            {
                MenuVis = Visibility.Hidden;
                EmployeeVis = Visibility.Visible;
                BackVis = Visibility.Visible;
            });

            CreateEmployeeCommand = new RelayCommand(o =>
            {
                createEmployeeWindow = new CreateEmployee(this);
                createEmployeeWindow.Show();
            });

            SubmitEmployeeCreation = new RelayCommand(o =>
            {
                userBLL.CreateUser(Username, Password, 2);
                employeeBLL.CreateEmployee(userBLL.UserList.Last().UserID, Name);
                Username = Password = Name = "";
            });

            ReadAllEmployeesCommand = new RelayCommand(o =>
            {
                CurrentEmployeeList = employeeBLL.ReadAllEmployees();
                readEmployees = new ReadEmployees(this);
                readEmployees.Show();
                readEmployees.Data_Grid.Columns[0].Visibility = Visibility.Hidden;
                readEmployees.Data_Grid.Columns[2].Visibility = Visibility.Hidden;
                for (int index = 4; index < readEmployees.Data_Grid.Columns.Count(); ++index)
                    readEmployees.Data_Grid.Columns[index].Visibility = Visibility.Hidden;

            });

            UpdateEmployeeNameCommand = new RelayCommand(o =>
            {
                Employee employee = employeeBLL.ReadAllEmployees()[SelectedUser];
                employeeBLL.UpdateEmployee(employee.EmployeeID, Name);
                Name = "";
            });

            DeleteEmployeeCommand = new RelayCommand(o =>
            {
                Employee employee = employeeBLL.ReadAllEmployees()[SelectedUser];
                employeeBLL.DeleteEmployee(employee.EmployeeID);
            });

            ManageBookings = new RelayCommand(o =>
            {
                CurrentBookingList = bookingBLL.ReadAllBookings();
                readBookingsWindow = new ReadBookings(this);
                readBookingsWindow.Show();
                readBookingsWindow.Data_Grid.Columns[0].Visibility = Visibility.Hidden;
                for (int index = 7; index < readBookingsWindow.Data_Grid.Columns.Count(); ++index)
                    readBookingsWindow.Data_Grid.Columns[index].Visibility = Visibility.Hidden;

            });

            CreateBookingCommand = new RelayCommand(o =>
            {
                createBookingWindow = new CreateBooking(this);
                createBookingWindow.Show();
            });

            SubmitBookingCreation = new RelayCommand(o =>
            {
                Room room = roomBLL.GetRoomByNr(RoomNr);
                Customer customer = customerBLL.ReadAllCustomers()[SelectedUser];
                bookingBLL.CreateBooking(customer.CustomerID, room.RoomID, DateFrom, DateTo);
                TimeSpan diff = DateTo.Subtract(DateFrom);
                double roomPrice = room.Price;
                bookingBLL.UpdateBookingPrice(bookingBLL.BookingList.Last().BookingID, (float)roomPrice * diff.Days);
                DateFrom = DateTime.Now;
                DateTo = DateTime.Now;
                RoomNr = 0;
            });

            DeleteBookingCommand = new RelayCommand(o =>
            {
                Booking booking = bookingBLL.ReadAllBookings()[SelectedUser];
                bookingBLL.DeleteBooking(booking.BookingID);
            });

            ManageRooms = new RelayCommand(o =>
            {
                MenuVis = Visibility.Hidden;
                BackVis = Visibility.Visible;
                RoomVis = Visibility.Visible;
            });

            CreateRoomCommand = new RelayCommand(o =>
            {
                createRoomWindow = new CreateRoom(this);
                createRoomWindow.Show();
            });

            SubmitRoomCreation = new RelayCommand(o =>
            {
                roomBLL.CreateRoom(RoomType, RoomNr, RoomPrice);
                RoomType = "";
                RoomNr = 0;
                RoomPrice = 0;
            });

            ReadAllRoomsCommand = new RelayCommand(o =>
           {
               CurrentRoomList = roomBLL.ReadAllRooms();
               readRoomsWindow = new ReadRooms(this);
               readRoomsWindow.Show();
               readRoomsWindow.Data_Grid.Columns[0].Visibility = Visibility.Hidden;
               readRoomsWindow.Data_Grid.Columns[6].Visibility = Visibility.Hidden;
           });

            DeleteRoomCommand = new RelayCommand(o =>
            {
                Room room = roomBLL.ReadAllRooms()[SelectedUser];
                roomBLL.DeleteRoom(room.RoomID);
            });

            UpdateRoomCommand = new RelayCommand(o =>
            {
                updateRoomWindow = new UpdateRoom(this);
                updateRoomWindow.Show();
            });

            SubmitRoomUpdateCommand = new RelayCommand(o =>
            {
                Room room = roomBLL.ReadAllRooms()[SelectedUser];
                roomBLL.UpdateRoom(room.RoomID, RoomType, RoomNr, room.IsAvailableFrom, RoomPrice);
            });

            UpdateBookingCommand = new RelayCommand(o =>
            {
                updateBookingWindow = new UpdateBooking(this);
                updateBookingWindow.Show();
            });

            SubmitBookingUpdateCommand = new RelayCommand(o =>
            {
                
                Booking booking = bookingBLL.ReadAllBookings()[SelectedUser];
                
                if (RoomNr != 0)
                {
                    Room room = roomBLL.GetRoomByNr(RoomNr);
                    bookingBLL.UpdateBooking(booking.BookingID, room.RoomID, DateFrom, DateTo);
                    TimeSpan diff = DateTo.Subtract(DateFrom);
                    double roomPrice = room.Price;
                    bookingBLL.UpdateBookingPrice(bookingBLL.BookingList.Last().BookingID, (float)roomPrice * diff.Days);
                    DateFrom = DateTime.Now;
                    DateTo = DateTime.Now;
                    RoomNr = 0;
                    ErrorVis = Visibility.Hidden;
                }
                else
                    ErrorVis = Visibility.Visible;
            });
        }
    }
}
