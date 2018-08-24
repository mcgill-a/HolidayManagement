using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;

namespace Data
{
    public class DataFacade
    {
        /* 
         * Author:               40276245 (Alex McGill)
         * Description:          This is the data facade class which allows the presentation
         *                       layer to communicate with the data layer
         * Date last modified:   09/12/2017
         * Design Patterns:      This class uses the Facade design pattern
        */
        
        // Declare the private data storage variable
        private DataStorage _dataStorage;

        /// <summary>
        /// Data facade constructor
        /// </summary>
        public DataFacade()
        {
            // Return the singleton instance of the data storage class
            _dataStorage = DataStorage.Instance;
        }

        /// <summary>
        /// Check Serialized File Exists
        /// </summary>
        /// <returns></returns>
        public bool SerializedFileExists()
        {
            return _dataStorage.SerializedFileExists();
        }

        /// <summary>
        /// Serialize Customer List
        /// </summary>
        public void SerializeCustomers()
        {
            _dataStorage.BinarySerialize();
        }

        /// <summary>
        /// Deserialize Customer List
        /// </summary>
        public void DeSerializeCustomers()
        {
            _dataStorage.BinaryDeserialize();
        }

        /// <summary>
        /// Add Customer
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public Customer AddCustomer(string name, string address)
        {
            Customer cust = _dataStorage.AddCustomer(name, address);

            return cust;
        }

        /// <summary>
        /// Remove Customer
        /// </summary>
        /// <param name="refNum"></param>
        public void RemoveCustomer(int refNum)
        {
            _dataStorage.RemoveCustomer(refNum);
        }

        /// <summary>
        /// Get Customers
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {

            return _dataStorage.Customers;
        }

        /// <summary>
        /// Get a Customer
        /// </summary>
        /// <param name="refNum"></param>
        /// <returns></returns>
        public Customer GetCustomer(int refNum)
        {
            return _dataStorage.FindCustomer(refNum);
        }

        /// <summary>
        /// Edit a Customer
        /// </summary>
        /// <param name="refNum"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        public void EditCustomer(int refNum, string name, string address)
        {
            _dataStorage.EditCustomer(refNum, name, address);
        }

        /// <summary>
        /// Add a Booking
        /// </summary>
        /// <param name="custRefNum"></param>
        /// <param name="arrivalDate"></param>
        /// <param name="departureDate"></param>
        /// <param name="bookingChalet"></param>
        /// <param name="bookingCar"></param>
        /// <param name="guests"></param>
        public void AddBooking(int custRefNum, DateTime arrivalDate, DateTime departureDate, Chalet bookingChalet, CarHire bookingCar, List<Guest> guests)
        {
            _dataStorage.AddBooking(custRefNum, arrivalDate, departureDate, bookingChalet, bookingCar, guests);
        }

        /// <summary>
        /// Find a Booking
        /// </summary>
        /// <param name="bookingRefNum"></param>
        /// <returns></returns>
        public Booking FindBooking(int bookingRefNum)
        {
            return _dataStorage.FindBooking(bookingRefNum);
        }

        /// <summary>
        /// Edit a Booking
        /// </summary>
        /// <param name="custBookRef"></param>
        /// <param name="custRefNum"></param>
        /// <param name="arrivalDate"></param>
        /// <param name="departureDate"></param>
        /// <param name="bookingChalet"></param>
        /// <param name="bookingCar"></param>
        /// <param name="guests"></param>
        public void EditBooking(int custBookRef, int custRefNum, DateTime arrivalDate, DateTime departureDate, Chalet bookingChalet, CarHire bookingCar, List<Guest> guests)
        {
            _dataStorage.EditBooking(custBookRef, custRefNum, arrivalDate, departureDate, bookingChalet, bookingCar, guests);
        }

        /// <summary>
        /// Remove a Booking
        /// </summary>
        /// <param name="custRefNum"></param>
        /// <param name="bookingRefNum"></param>
        public void RemoveBooking(int custRefNum, int bookingRefNum)
        {
            _dataStorage.RemoveBooking(custRefNum, bookingRefNum);
        }

        /// <summary>
        /// Get a list of Bookings
        /// </summary>
        /// <returns></returns>
        public List<Booking> GetBookings()
        {
            return _dataStorage.Bookings;
        }
    }
}