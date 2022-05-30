using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TEMA_3.Layers.BLL;
using TEMA_3.Views;

namespace TEMA_3
{
    public class BookingsVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Room> CurrentRoomList { get; set; }

        RoomBLL roomBLL;
        User user;
        Customer cust;
        DateTime dateFrom;
        DateTime dateTo;
        Visibility userVisibility;
        Visibility selectedVisibility;
        int selectedItem;
        float totalPrice;
        double roomPrice;


        public int SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
                SelectedVisibility = Visibility.Visible;
                roomPrice = CurrentRoomList[SelectedItem].Price;
                TimeSpan diff = DateTo.Subtract(DateFrom);
                TotalPrice = (float)roomPrice * diff.Days;
            }
        }
        public float TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; NotifyPropertyChanged("TotalPrice"); }
        }

        public Visibility UserVisibility
        {
            get { return userVisibility; }
            set { userVisibility = value; NotifyPropertyChanged("UserVisibility"); }
        }
        public Visibility SelectedVisibility
        {
            get { return selectedVisibility; }
            set { selectedVisibility = value; NotifyPropertyChanged("SelectedVisibility"); }
        }

        public User User
        {
            get { return user; }
            set { user = value; NotifyPropertyChanged("User"); }
        }
        public Customer Cust
        {
            get { return cust; }
            set { cust = value; NotifyPropertyChanged("Cust"); }
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

        public RelayCommand AddBookingCommand { get; set; }

        public BookingsVM(Customer _customer, DateTime dateFrom , DateTime dateTo)
        {
            Cust = _customer;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;
            
            if (Cust != null) { UserVisibility = Visibility.Visible; }
            else { UserVisibility = Visibility.Hidden; }

            roomBLL = new RoomBLL();
            CurrentRoomList = roomBLL.ReadAvailableRooms(dateFrom);

            

            AddBookingCommand = new RelayCommand(o =>
            {
                CustomerBLL customerBLL = new CustomerBLL();
                Customer customer = new Customer();
                foreach (Customer c in customerBLL.CustomerList)
                {
                    if (c.UserID == User.UserID)
                        customer = c;
                }

                Room room = CurrentRoomList[SelectedItem];

                BookingBLL bookingBLL = new BookingBLL();
                bookingBLL.CreateBooking(customer.CustomerID, room.RoomID , DateFrom, DateTo);
                TimeSpan diff = DateTo.Subtract(DateFrom);
                double roomPrice = room.Price;
                bookingBLL.UpdateBookingPrice(bookingBLL.BookingList.Last().BookingID, (float)roomPrice * diff.Days);                              

            });


        }



    }
}
