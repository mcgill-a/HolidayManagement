using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;

namespace Data
{
    [Serializable]
    public class CustomerList
    {
        /* 
         * Author:               40276245 (Alex McGill)
         * Description:          This is the customer list class which is used in the
         *                       Binary Serialization process of storing and retrieving customer lists
         * Date last modified:   09/12/2017
        */
        
        // Declare the customer list
        public List<Customer> customerList;

        /// <summary>
        /// Empty Customer List Constructor
        /// </summary>
        public CustomerList()
        {
            // Set the customer list to a new list of customers
            customerList = new List<Customer>();
        }

        /// <summary>
        /// Customer List Constructor (taking in an existing customers list)
        /// </summary>
        /// <param name="customers"></param>
        public CustomerList(List<Customer> customers)
        {
            // Set the customer list to a new list based of the existing customer list
            customerList = new List<Customer>(customers);
        }

        /// <summary>
        /// Get the list of customers
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            return customerList;
        }
    }
}
