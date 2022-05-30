using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TEMA_3.Layers.BLL
{
    public class AdminBLL
    {
        private HotelEntities context = new HotelEntities();

        public ObservableCollection<Admin> AdminList { get; set; }

        public AdminBLL()
        {
            AdminList = new ObservableCollection<Admin>();
            List<Admin> admins = context.Admins.ToList();
            foreach (Admin admin in admins)
            {
                AdminList.Add(admin);
            }
        }

        public void CreateAdmin(int userID, string name)
        {
            context.CreateAdmin(userID, name);
            context.SaveChanges();
            UpdateAdminList();
        }

        public ObservableCollection<Admin> ReadAllAdmins()
        {
            return AdminList;
        }

        public void UpdateAdmin(int adminID, string name)
        {
            context.UpdateAdmin(adminID, name);
            context.SaveChanges();
            UpdateAdminList();
        }

        public void UpdateAdminList()
        {
            AdminList = new ObservableCollection<Admin>();
            List<Admin> admins = context.Admins.ToList();
            foreach (Admin admin in admins)
            {
                AdminList.Add(admin);
            }
        }
    }
}
