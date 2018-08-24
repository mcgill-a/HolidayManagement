using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Business
{
    
    public class BookingFactory
    {
        /* 
         * Author:               40276245 (Alex McGill)
         * Description:          This is the booking factory class file which creates bookings
         *                       Each booking has a unique id allocated by the factory
         * Date last modified:   09/12/2017
         * Design Patterns:      This class uses the Factory design pattern
        */

        // Decalare private variables
        private int _lastBookingID = 0;
        private string filePath = "STORAGE\\bookingIDS.txt";

        /// <summary>
        /// Empty booking factory constructor
        /// </summary>
        public BookingFactory()
        {
            // Run the method to setup the booking IDS
            SetupBookingIDS();
        }

        /// <summary>
        /// Set up the booking IDS file which stores the list of booking IDS
        /// </summary>
        public void SetupBookingIDS()
        {
            // If the file exists
            if (File.Exists(filePath))
            {
                string lastLine = "";
                
                // If the file length is not 0
                if (!(new FileInfo(filePath).Length == 0))
                {
                    // Set the lastLine to the last line in the file
                    lastLine = File.ReadLines(filePath).Last();
                }
                // Try and convert the last line to an integer
                int number = 0;
                bool isNumber = int.TryParse(lastLine, out number);
                // If the conversion worked
                if (isNumber)
                {
                    // Set the last booking ID to the number loaded in from the last line of the file
                    _lastBookingID = number;
                }
            }
            else
            {
                // Create the file
                File.Create(filePath);
            }
        }

        /// <summary>
        /// Append the booking ID to the file
        /// </summary>
        /// <param name="bookingID"></param>
        public void AddIdToFile(int bookingID)
        {
            // Append the file id to the end of the file
            File.AppendAllText(filePath, bookingID.ToString() + Environment.NewLine);
        }

        /// <summary>
        /// Factory method for Booking class
        /// </summary>
        /// <returns></returns>
        public Booking FactoryMethod()
        {
            // Increment the last booking ID
            _lastBookingID++;
            // Add the last booking ID to the file
            AddIdToFile(_lastBookingID);
            // Return the new booking object with the new booking id
            return new Booking(_lastBookingID);
        }

    }
}
