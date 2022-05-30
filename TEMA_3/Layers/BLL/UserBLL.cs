using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace TEMA_3.Layers.BLL
{
    public class UserBLL
    {
        private HotelEntities context = new HotelEntities();

        public ObservableCollection<User> UserList { get; set; }

        public UserBLL()
        {
            UserList = new ObservableCollection<User>();
            List<User> users = context.Users.ToList();
            foreach (User user in users)
            {
                if (!user.Deleted)
                    UserList.Add(user);
            }
        }

        public void CreateUser(User user)
        {
            if (user != null)
            {
                context.Users.Add(new User() { Username = user.Username, Password = user.Password, RoleID = user.RoleID });
                context.SaveChanges();
                user.UserID = context.Users.Max(item => item.UserID);               
                UserList.Add(user);
            }
        }

        public void CreateUser(string username, string password, int roleID)
        {
            context.CreateUser(username, password, roleID);
            context.SaveChanges();
            UpdateUserList();
        }

        public ObservableCollection<User> ReadAllUsers()
        {
            List<User> users = context.Users.ToList();
            ObservableCollection<User> result = new ObservableCollection<User>();
            foreach (User user in users)
            {
                result.Add(user);
            }
            return result;            
        }

        public User GetUser(string username)
        {
            foreach (User user in UserList)
            {
                String username1;
                username1 = user.Username;
                username1 = Regex.Replace(username1, @"\s+", "");
                if (username1 == username) { return user; }
            }
            return null;
        }

        public void UpdateUserPassword(int id, string password)
        {
            User user = context.Users.Find(id);
            user.Password = password;
            context.SaveChanges();
            UpdateUserList();
        }

        public void UpdateUserRole(int id, int roleID)
        {
            context.UpdateUser(id, roleID);
            context.SaveChanges();
            UpdateUserList();
        }
        
        public void DeleteUser(int id)
        {
            context.DeleteUser(id);
            context.SaveChanges();
            UserList.Remove(context.Users.Find(id));
        }

        public void UpdateUserList()
        {
            UserList = new ObservableCollection<User>();
            List<User> users = context.Users.ToList();
            foreach (User user in users)
            {
                if (!user.Deleted)
                    UserList.Add(user);
            }
        }
    }    
}
