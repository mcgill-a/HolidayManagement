using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    [Serializable]
    public class CarHire
    {
        /* 
         * Author:               40276245 (Alex McGill)
         * Description:          This is the car hire class for the booking which
         *                       defines the car hire attributes
         * Date last modified:   09/12/2017
        */
        
        // Declare private variables
        private bool _hired;
        private int _cost;
        private DateTime _dateStart;
        private DateTime _dateEnd;
        private string _driverName;

        /// <summary>
        /// Empty Car Hire Constructor
        /// </summary>
        public CarHire()
        {

        }

        /// <summary>
        /// Car Hire Status (Hired) Getter and Setter
        /// </summary>
        public bool Hired
        {
            get
            {
                return _hired;
            }
            set
            {
                // If value is not null
                if (value != null)
                {
                    // Set the booking car hire status to value (true/false)
                    _hired = value;
                }
                else
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Car hire status is not valid (true/false)");
                }
            }
        }

        /// <summary>
        /// Car Hire Cost Getter and Setter
        /// </summary>
        public int Cost
        {
            get
            {
                return _cost;
            }
            set
            {
                // If the value is more than 0
                if (value > 0)
                {
                    // Set the car hire cost to the value
                    _cost = value;
                }
                else
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Car hire cost is not valid");
                }
            }
        }

        /// <summary>
        /// Car Hire Date Start Getter and Setter
        /// </summary>
        public DateTime DateStart
        {
            get
            {
                return _dateStart;
            }
            set
            {
                // If the value is not null
                if (value != null)
                {
                    // Set the start date to the value
                    _dateStart = value;
                }
                else
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Car hire start date is not valid (true/false)");
                }
            }
        }

        /// <summary>
        /// Car Hire Date End Getter and Setter
        /// </summary>
        public DateTime DateEnd
        {
            get
            {
                return _dateEnd;
            }
            set
            {
                // If the value is not null
                if (value != null)
                {
                    // Set the end date to the value
                    _dateEnd = value;
                }
                else
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Car Hire end date is not valid (true/false)");
                }
            }
        }

        /// <summary>
        /// Car Hire Driver Name Getter and Setter
        /// </summary>
        public string DriverName
        {
            get
            {
                return _driverName;
            }
            set
            {
                // If the driver name is not empty
                if (!(String.IsNullOrWhiteSpace(value)))
                {
                    // Set the driver name to the value
                    _driverName = value;
                }
                else
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Car Hire driver name is not valid");
                }
            }
        }
    }
}
