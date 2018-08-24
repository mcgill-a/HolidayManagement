using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Business
{
    [Serializable]
    public class Customer
    {
        /* 
         * Author:               40276245 (Alex McGill)
         * Description:          This is the Customer class which defines the name, address,
         *                       customer reference and list of bookings attributes
         * Date last modified:   09/12/2017
        */
        
        // Declare private variables
        private string _name;
        private string _address;
        private int _customerRefNumber;
        private List<Booking> _bookings;

        /// <summary>
        /// Empty Customer Constructor
        /// </summary>
        public Customer()
        {

        }
        
        /// <summary>
        /// Customer Constructor with Reference Number
        /// </summary>
        /// <param name="refNumber"></param>
        public Customer(int refNumber)
        {
            // Set the customer reference number to the ref number passed in by the customer factory
            _customerRefNumber = refNumber;
        }
     
        /// <summary>
        /// Customer Name Getter and Setter
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                // If the value is empty
                if (string.IsNullOrWhiteSpace(value))
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Customer name cannot be empty");
                }
                else
                {
                    // Set the customer name to the value
                    _name = value;
                }
            }
        }

        /// <summary>
        /// Customer Address Getter and Setter
        /// </summary>
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                // If the value is empty
                if (string.IsNullOrWhiteSpace(value))
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Customer address cannot be empty");
                }
                else
                {
                    // Set the customer address to the value
                    _address = value;
                }
            }
        }

        /// <summary>
        /// Customer Reference Number Getter
        /// </summary>
        public int RefNumber
        {
            get
            {
                return _customerRefNumber;
            }
        }

        /// <summary>
        /// Customer Booking List Getter and Setter
        /// </summary>
        public List<Booking> Bookings
        {
            get
            {
                return _bookings;
            }
            set
            {
                // If the value is null
                if (value == null)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Customer does not have any bookings");
                }
                else
                {
                    // Set the customer bookings to the value
                    _bookings = value;
                }
            }
        }
    }
}
