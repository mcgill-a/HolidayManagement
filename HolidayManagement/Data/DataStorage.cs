using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Business;

namespace Data
{
    public class DataStorage
    {
        /* 
         * Author:               40276245 (Alex McGill)
         * Description:          This is the data storage class which stores the customers list
         *                       using binary serialization and deserialization
         * Date last modified:   09/12/2017
         * Design Patterns:      This class uses the Singleton and Factory design patterns
        */

        // Declare private variables
        private List<Customer> _customers = new List<Customer>();
        private CustomerFactory _custFactory = new CustomerFactory();
        private BookingFactory _bookFactory = new BookingFactory();
        string fileDirectory = "STORAGE";
        string filePath = "STORAGE\\customerList.bin";
        // Declare singleton instance
        private static DataStorage _instance;

        /// <summary>
        /// Empty data storage constructor
        /// </summary>
        private DataStorage()
        {
            /// Create the file directory if it doesn't already exist
            Directory.CreateDirectory(fileDirectory);
        }

        /// <summary>
        /// Singleton Instance of the Business Storage class
        /// </summary>
        public static DataStorage Instance
        {
            get
            {
                // If an instance doesn't exist
                if (_instance == null)
                {
                    // Create a new instance of the business storage class
                    _instance = new DataStorage();
                }
                // Return the existing instance of the business storage class
                return _instance;
            }
        }

        /// <summary>
        /// Check if the serialized file exists
        /// </summary>
        /// <returns></returns>
        public bool SerializedFileExists()
        {
            // If the serialized file exists
            if (File.Exists(filePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Binary Serialization
        /// </summary>
        public void BinarySerialize()
        {
            // Create an instance of the Customer List class
            CustomerList customerList = new CustomerList(_customers);

            // Write and serialize to the binary file
            Stream streamWrite = File.Create(filePath);
            BinaryFormatter binaryWrite = new BinaryFormatter();
            binaryWrite.Serialize(streamWrite, customerList);
            streamWrite.Close();
        }

        /// <summary>
        /// Binary Deserialization
        /// </summary>
        public void BinaryDeserialize()
        {
            // Read and deserialize the binary file
            Stream streamRead = File.OpenRead(filePath);
            BinaryFormatter binaryRead = new BinaryFormatter();
            CustomerList customersBinary = (CustomerList)binaryRead.Deserialize(streamRead);
            streamRead.Close();
            // Clear the existing customers list
            _customers.Clear();
            // Set the customers list to the deserialized customers list
            _customers = new List<Customer>(customersBinary.GetCustomers());
        }

        /// <summary>
        /// Add a new customer to the customers list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public Customer AddCustomer(string name, string address)
        {
            // Create a new customer object using the factory method to generate a unique id
            Customer cust = _custFactory.FactoryMethod();
            // Set the customer details using the name and address provided
            cust.Name = name;
            cust.Address = address;
            // Create a new booking list to store bookings for the customer
            cust.Bookings = new List<Booking>();
            // Add the created customer to the list of customers
            _customers.Add(cust);
            return cust;
        }

        /// <summary>
        /// Find a customer
        /// </summary>
        /// <param name="refNum"></param>
        /// <returns></returns>
        public Customer FindCustomer(int refNum)
        {
            // Loop through each customer in the customer list
            foreach (Customer c in _customers)
            {
                // If the current customer reference number equals the input reference number
                if (c.RefNumber == refNum)
                {
                    // Return the current customer
                    return c;
                }
            }
            return null;
        }

        /// <summary>
        /// Remove a customer
        /// </summary>
        /// <param name="refNum"></param>
        public void RemoveCustomer(int refNum)
        {
            // Find the customer using the FindCustomer method
            Customer c = this.FindCustomer(refNum);
            // If the customer was found
            if (c != null)
            {
                // Remove the customer from the customer list
                _customers.Remove(c);
            }
        }

        /// <summary>
        /// Customer List Getter
        /// </summary>
        public List<Customer> Customers
        {
            get
            {
                // Return the list of customers
                return _customers;
            }
        }

        /// <summary>
        /// Edit a customer
        /// </summary>
        /// <param name="refNum"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        public void EditCustomer(int refNum, string name, string address)
        {
            // Loop through each customer in the customer list
            for (int i = 0; i < _customers.Count; i++)
            {
                // If the customer reference number matches the input reference number
                if (_customers[i].RefNumber == refNum)
                {
                    // Set the customer name and address
                    _customers[i].Name = name;
                    _customers[i].Address = address;
                    break;
                }
            }
        }

        /// <summary>
        /// Add a new booking
        /// </summary>
        /// <param name="custRefNum"></param>
        /// <param name="arrivalDate"></param>
        /// <param name="departureDate"></param>
        /// <param name="bookingChalet"></param>
        /// <param name="bookingCar"></param>
        /// <param name="guests"></param>
        /// <returns></returns>
        public Booking AddBooking(int custRefNum, DateTime arrivalDate, DateTime departureDate, Chalet bookingChalet, CarHire bookingCar, List<Guest> guests)
        {
            // Create a new booking object using the booking factory method
            Booking book = _bookFactory.FactoryMethod();
            // Set the booking attributes using the inputs provided
            book.Arrival = arrivalDate;
            book.Departure = departureDate;
            book.BookingChalet = bookingChalet;
            book.BookingCarHire = bookingCar;
            book.BookingGuests = new List<Guest>(guests);
            book.CustomerRef = custRefNum;
            book.BookingInvoice = new Invoice(book);

            // get all the customers bookings, add to new list, add booking to list, set bookings to new list
            // Create a temporary list of bookings
            List<Booking> tempBookings;
            // Loop through the list of customers
            for (int i = 0; i < _customers.Count; i++)
            {
                // If the current customer reference number matches the input customer reference number
                if (_customers[i].RefNumber == custRefNum)
                {
                    // If the current customer has existing bookings
                    if (_customers[i].Bookings.Count > 0)
                    {
                        // Set the temporary list of bookings to the customer list of bookings
                        tempBookings = new List<Booking>(_customers[i].Bookings);
                    }
                    else
                    {
                        // Create a new list of bookings
                        tempBookings = new List<Booking>();
                    }
                    // Add the newly created booking to the temporary list of bookings
                    tempBookings.Add(book);
                    // Set the customer list of bookings to the temporary list of bookings
                    _customers[i].Bookings = tempBookings;
                    break;
                }
            }
            return book;
        }

        /// <summary>
        /// Edit a booking
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
            // Loop through each customer in the customer list
            for (int i = 0; i < _customers.Count; i++)
            {
                // If the current customer reference number matches the input customer reference number
                if (_customers[i].RefNumber == custRefNum)
                {
                    // Loop through each booking in the current customers list of bookings
                    for (int j = 0; j < _customers[i].Bookings.Count; j++)
                    {
                        // If the current booking matches the booking reference input provided
                        if (_customers[i].Bookings[j].BookingRef == custBookRef)
                        {
                            // Set the attributes of the booking to the inputs provided
                            _customers[i].Bookings[j].Arrival = arrivalDate;
                            _customers[i].Bookings[j].Departure = departureDate;
                            _customers[i].Bookings[j].BookingChalet = bookingChalet;
                            _customers[i].Bookings[j].BookingCarHire = bookingCar;
                            _customers[i].Bookings[j].BookingGuests.Clear();
                            _customers[i].Bookings[j].BookingGuests = new List<Guest>(guests);
                            _customers[i].Bookings[j].BookingInvoice = new Invoice(_customers[i].Bookings[j]);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Find a booking
        /// </summary>
        /// <param name="custBookRef"></param>
        /// <returns></returns>
        public Booking FindBooking(int custBookRef)
        {
            // Loop through customer list
            foreach (Customer c in Customers)
            {
                // Loop through the booking list of the current customer
                foreach (Booking b in c.Bookings)
                {
                    // If the current booking reference number matches the booking reference number provided
                    if (b.BookingRef == custBookRef)
                    {
                        // Return the current booking
                        return b;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Remove a booking
        /// </summary>
        /// <param name="custRefNum"></param>
        /// <param name="custBookRef"></param>
        public void RemoveBooking(int custRefNum, int custBookRef)
        {
            // Find the customer using the customer reference number provided
            Customer current = this.FindCustomer(custRefNum);
            // Find the booking using the customer booking reference
            Booking b = this.FindBooking(custBookRef);
            // If the customer was found
            if (current != null)
            {
                // Remove the booking from the current customer
                current.Bookings.Remove(b);
            }
        }

        /// <summary>
        /// Booking List Getter
        /// </summary>
        public List<Booking> Bookings
        {
            get
            {
                // Create a new list of type booking
                List<Booking> bookings = new List<Booking>();
                // Loop through each customer in the customer list
                foreach (Customer c in _customers)
                {
                    // Loop through each booking in the current customer bookings list
                    foreach (Booking b in c.Bookings)
                    {
                        // Add the current booking to the list of bookings
                        bookings.Add(b);
                    }
                }
                // Return an ordered version of the bookings list
                return bookings.OrderBy(x => x.BookingRef).ToList();
            }
        }
    }
}