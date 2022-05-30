using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace TEMA_3.Layers.BLL
{
    public class BookingBLL
    {
        private HotelEntities context = new HotelEntities();

        public ObservableCollection<Booking> BookingList { get; set; }

        public BookingBLL()
        {
            BookingList = new ObservableCollection<Booking>();
            List<Booking> bookings = context.Bookings.ToList();
            foreach(Booking booking in bookings)
            {
                if (!booking.Deleted)
                    BookingList.Add(booking);
            }
        }

        public void CreateBooking(int customerID, int roomID, DateTime from, DateTime to)
        {
            context.CreateBooking(customerID, roomID, from, to);
            context.SaveChanges();
            UpdateBookingList();
        }

        public ObservableCollection<Booking> ReadAllBookings()
        {
            List<Booking> bookings = context.Bookings.ToList();
            ObservableCollection<Booking> result = new ObservableCollection<Booking>();
            foreach(Booking booking in bookings)
            {
                result.Add(booking);
            }
            return result;
        }

        public void UpdateBooking(int bookingid, int roomid, DateTime from, DateTime to)
        {
            context.UpdateBooking(bookingid, roomid, from, to);
            context.SaveChanges();
            UpdateBookingList();            
        }

        public void UpdateBookingPrice(int bookingID, float price)
        {
            context.UpdateBookingPrice(bookingID, price);
            context.SaveChanges();
            UpdateBookingList();
        }

        public void DeleteBooking(int bookingid)
        {
            context.DeleteBooking(bookingid);
            Booking booking = context.Bookings.Find(bookingid);
            booking.Room.IsAvailableFrom = DateTime.Now;
            context.SaveChanges();
            UpdateBookingList();
        }

        public void UpdateBookingList()
        {
            BookingList = new ObservableCollection<Booking>();
            List<Booking> bookings = context.Bookings.ToList();
            foreach (Booking booking in bookings)
            {
                if (!booking.Deleted)
                    BookingList.Add(booking);
            }
        }
    }
}
