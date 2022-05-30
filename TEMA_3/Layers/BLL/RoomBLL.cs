using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TEMA_3.Layers.BLL
{
    public class RoomBLL
    {
        private HotelEntities context = new HotelEntities();

        public ObservableCollection<Room> RoomList { get; set; }

        public RoomBLL()
        {
            RoomList = new ObservableCollection<Room>();
            List<Room> rooms = context.Rooms.ToList();
            foreach (Room room in rooms)
            {
                if (!room.Deleted)
                    RoomList.Add(room);
            }
        }

        public void CreateRoom(string roomType, int roomNr, double price)
        {
            context.CreateRoom(roomType, roomNr, price);
            context.SaveChanges();
            UpdateRoomList();
        }

        public ObservableCollection<Room> ReadAllRooms()
        {
            List<Room> rooms = context.Rooms.ToList();
            ObservableCollection<Room> result = new ObservableCollection<Room>();
            foreach (Room room in rooms)
            {
                result.Add(room);
            }
            return result;
        }

        public ObservableCollection<Room> ReadAvailableRooms(DateTime dateFrom)
        {
            ObservableCollection<Room> result = new ObservableCollection<Room>();
            foreach(Room room in RoomList)
            {
                if (room.IsAvailableFrom <= dateFrom)
                    result.Add(room);
            }

            return result;
        }

        public Room GetRoomByNr(int roomNr)
        {
            //Room room = new Room();
            foreach (Room room in RoomList)
            {
                if (room.RoomNr == roomNr)
                    return room;
            }
            return null;
        }

        public Room GetRoomByID(int roomID)
        {
            foreach (Room room in RoomList)
            {
                if (room.RoomID == roomID)
                    return room;
            }
            return null;
        }

        public void UpdateRoom(int roomID, string roomType, int roomnr, DateTime availableFrom, double price)
        {
            context.UpdateRoom(roomID, roomType, roomnr, availableFrom, price);
            context.SaveChanges();
            UpdateRoomList();
        }

        public void DeleteRoom(int roomID)
        {
            context.DeleteRoom(roomID);
            context.SaveChanges();
            UpdateRoomList();
        }

        public void UpdateRoomList()
        {
            RoomList = new ObservableCollection<Room>();
            List<Room> rooms = context.Rooms.ToList();
            foreach (Room room in rooms)
            {
                if (!room.Deleted)
                    RoomList.Add(room);
            }
        }
    }
}
