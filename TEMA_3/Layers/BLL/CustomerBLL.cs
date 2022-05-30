using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace TEMA_3.Layers.BLL
{
    public class CustomerBLL
    {
        private HotelEntities context = new HotelEntities();

        public ObservableCollection<Customer> CustomerList { get; set; }
       
        public CustomerBLL()
        {
            CustomerList = new ObservableCollection<Customer>();
            List<Customer> customers = context.Customers.ToList();
            foreach (Customer customer in customers)
            {
                if (!customer.Deleted)
                    CustomerList.Add(customer);
            }
        }

        public void CreateCustomer(int userid, string name)
        {
            context.CreateCustomer(userid, name);
            context.SaveChanges();
            UpdateCustomerList();
        }

        public ObservableCollection<Customer> ReadAllCustomers()
        {
            List<Customer> customers = context.Customers.ToList();
            ObservableCollection<Customer> result = new ObservableCollection<Customer>();
            foreach(Customer customer in customers)
            {
                result.Add(customer);
            }

            return result;
        }

        public void UpdateCustomer(int id, string name)
        {
            context.UpdateCustomer(id, name);
            context.SaveChanges();
            UpdateCustomerList();
        }

        public void DeleteCustomer(int id)
        {
            context.DeleteCustomer(id);
            context.SaveChanges();
            UpdateCustomerList();
        }

        public void UpdateCustomerList()
        {
            CustomerList = new ObservableCollection<Customer>();
            List<Customer> customers = context.Customers.ToList();
            foreach (Customer customer in customers)
            {
                if (!customer.Deleted)
                    CustomerList.Add(customer);
            }
        }
    }
}
