using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEMA_3.Layers.BLL;

namespace TEMA_3
{
    public class CustomerVM : INotifyPropertyChanged
    {
        User user;
        Customer customer;
        BookingBLL bookingBLL;
        int selectedItem;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Booking> CurrentBookingList { get; set; }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public User User
        {
            get { return user; }
            set { user = value; NotifyPropertyChanged("User"); }
        }

        public int SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; NotifyPropertyChanged("SelectedItem"); }
        }

        public RelayCommand DeleteBookingCommand { get; set; }

        public CustomerVM(Customer customer) 
        { 
            this.customer = customer;
            bookingBLL = new BookingBLL();
            CurrentBookingList = new ObservableCollection<Booking>();

            foreach(Booking b in bookingBLL.BookingList)
            {
                if (b.CustomerID == customer.CustomerID)
                    CurrentBookingList.Add(b);
            }

            DeleteBookingCommand = new RelayCommand(o =>
            {
                Booking booking = CurrentBookingList[SelectedItem];
                bookingBLL.DeleteBooking(booking.BookingID);
            });
        }
    }
}
