using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace TEMA_3.Layers.BLL
{
    public class EmployeeBLL
    {
        private HotelEntities context = new HotelEntities();
        public ObservableCollection<Employee> EmployeeList { get; set; }

        public EmployeeBLL()
        {
            EmployeeList = new ObservableCollection<Employee>();
            List<Employee> employees = context.Employees.ToList();
            foreach (Employee emp in employees)
            {
                if (!emp.Deleted)
                    EmployeeList.Add(emp);
            }
        }

        public void CreateEmployee(int userID, string name)
        {
            context.CreateEmployee(userID, name);
            context.SaveChanges();
            UpdateEmployeeList();
        }

        public ObservableCollection<Employee> ReadAllEmployees()
        {
            List<Employee> emps = context.Employees.ToList();
            ObservableCollection<Employee> result = new ObservableCollection<Employee>();
            foreach(Employee emp in emps)
            {
                result.Add(emp);
            }
            return result;
        }

        public void UpdateEmployee(int empID, string name)
        {
            context.UpdateEmployee(empID, name);
            context.SaveChanges();
            UpdateEmployeeList();
        }

        public void DeleteEmployee(int empID)
        {
            context.DeleteEmployee(empID);
            context.SaveChanges();
            UpdateEmployeeList();
        }

        public void UpdateEmployeeList()
        {
            EmployeeList = new ObservableCollection<Employee>();
            List<Employee> employees = context.Employees.ToList();
            foreach (Employee emp in employees)
            {
                if (!emp.Deleted)
                    EmployeeList.Add(emp);
            }
        }
    }
}
