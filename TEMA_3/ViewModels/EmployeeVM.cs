using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TEMA_3.Views;

namespace TEMA_3
{
    public class EmployeeVM : INotifyPropertyChanged
    {
        private Visibility menuVis;
        private Visibility userVis;
        private Visibility customerVis;
        private Visibility employeeVis;
        private Visibility bookingVis;
        private Visibility roomVis;
        private Visibility backVis;
        private String currentEmployeeName;
        private EmployeeView employeeWindow;

        private Employee CurrentEmployee { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public String CurrentEmployeeName
        {
            get { return currentEmployeeName; }
            set { currentEmployeeName = value; NotifyPropertyChanged("CurrentEmployeeName"); }
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

        public EmployeeVM(Employee employee)
        {
            CurrentEmployee = employee;
            if (employee != null)
            {
                CurrentEmployeeName = employee.Name;
            }
            AdminVM vm = new AdminVM(null);
            vm.CurrentAdminName = employee.Name;
            employeeWindow = new EmployeeView(vm);
            employeeWindow.Show();
        }
    }
}
