using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TEMA_3.Layers.BLL;

namespace TEMA_3
{
    public class RegisterVM : INotifyPropertyChanged
    {

        private String username="";
        private String password="";
        private String name = "";
        private String registerStatus;
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
        public String Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged("Name"); }
        }

        public String RegisterStatus
        {
            get { return registerStatus; }
            set { registerStatus = value; NotifyPropertyChanged("RegisterStatus"); }
        }

        public String Password
        {
            get { return password; }
            set { password = value; NotifyPropertyChanged("Password"); }
        }


        public RelayCommand RegisterCommand { get; set; }

        public RegisterVM(MainVM vm)
        {
            mainVM = vm;
            RegisterCommand = new RelayCommand(o =>
            {
                UserBLL user = new UserBLL();
                User user1 = new User();
                CustomerBLL customerBLL = new CustomerBLL();
                if (Username != "" && Password != "")
                {
                    user1 = user.GetUser(Username);
                    if (user1 == null)
                    {
                        RegisterStatus = "Registered succesfully!";
                        user.CreateUser(Username, Password, 1);
                        customerBLL.CreateCustomer(user.UserList.Last().UserID, Name);
                    }
                    else
                    {
                        RegisterStatus = "Username already taken!";
                    }

                }
                else
                {
                    RegisterStatus = "Fields cannot be empty!";
                }
            });

        }





    }

 



}
