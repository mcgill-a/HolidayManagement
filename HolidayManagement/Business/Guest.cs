using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    [Serializable]
    public class Guest
    {
        /* 
         * Author:               40276245 (Alex McGill)
         * Description:          This is the guest class for the booking which
         *                       defines the guest attributes
         * Date last modified:   09/12/2017
        */

        // Declare private variables
        private int _age;
        private string _name;
        private string _passportNumber;

        /// <summary>
        /// Empty Guest Constructor
        /// </summary>
        public Guest()
        {

        }

        /// <summary>
        /// Guest Age Getter and Setter
        /// </summary>
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                // If the value is not between 0 and 101
                if (value < 0 || value > 101)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Age is not in valid range (0-101)");
                }
                else
                {
                    // Set the age to the value
                    _age = value;
                }
            }
        }

        /// <summary>
        /// Guest Name Getter and Setter
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
                if (String.IsNullOrWhiteSpace(value))
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Name is not valid");
                }
                else
                {
                    // Set the name to the value
                    _name = value;
                }
            }
        }

        /// <summary>
        /// Guest Passport Number Getter and Setter
        /// </summary>
        public string PassportNumber
        {
            get
            {
                return _passportNumber;
            }
            set
            {
                // If the value length is between 1 and 10 characters
                if (value.Length < 1 || value.Length > 10)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Passport number is not valid (1-10 characters)");
                }
                else
                {
                    // Set the passport number to the value
                    _passportNumber = value;
                }
            }
        }
    }
}
