using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Business
{

    public class CustomerFactory
    {
        /* 
         * Author:               40276245 (Alex McGill)
         * Description:          This is the customer factory class file which creates customers
         *                       Each customer has a unique id allocated by the factory
         * Date last modified:   09/12/2017
         * Design Patterns:      This class uses the Factory design pattern
        */

        // Declare private variables
        private int _lastRefNumber = 0;
        private string filePath = "STORAGE\\customerIDS.txt";

        /// <summary>
        /// Empty Customer Factory constructor
        /// </summary>
        public CustomerFactory()
        {
            // Run the method to setup the customer ids
            SetupCustomerIDS();
        }

        /// <summary>
        /// Set up the customer IDS file which stores the list of customer reference numbers
        /// </summary>
        public void SetupCustomerIDS()
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
                    // Set the last reference number to the number loaded in from the last line of the file
                    _lastRefNumber = number;
                }
            }
            else
            {
                // Create the file
                File.Create(filePath);
            }
        }

        /// <summary>
        /// Append the customer id to the file
        /// </summary>
        /// <param name="customerRef"></param>
        public void AddIdToFile(int customerRef)
        {
            // Append the file id to the end of the file
            File.AppendAllText(filePath, customerRef.ToString() + Environment.NewLine);
        }

        /// <summary>
        /// Factory method for customer class
        /// </summary>
        /// <returns></returns>
        public Customer FactoryMethod()
        {
            // Increment the last reference number
            _lastRefNumber++;
            // Add the last customer id to the file
            AddIdToFile(_lastRefNumber);
            // Return the new customer object with the last reference number
            return new Customer(_lastRefNumber);
        }
    }
}
