using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    [Serializable]
    public class Chalet
    {
        /* 
         * Author:               40276245 (Alex McGill)
         * Description:          This is the chalet class for the booking which
         *                       defines the chalet attributes
         * Date last modified:   09/12/2017
        */
        
        // Declare private variables
        private int _chaletID;
        private int _flatRate;
        private bool _mealBreakfast;
        private bool _mealEvening;

        /// <summary>
        /// Empty Chalet Constructor
        /// </summary>
        public Chalet()
        {
            // Set the flat rate to the standard chalet flat rate (60)
            _flatRate = 60;
        }

        /// <summary>
        /// Chalet ID Getter and Setter
        /// </summary>
        public int ChaletID
        {
            get
            {
                return _chaletID;
            }
            set
            {
                // If the chalet ID is between 1 and 10
                if (value >= 1 && value <= 10)
                {
                    // Set the chalet ID to the value
                    _chaletID = value;
                }
                else
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Chalet ID is not in valid range");
                }
            }
        }

        /// <summary>
        /// Flat Rate Getter
        /// </summary>
        public int FlatRate
        {
            get
            {
                return _flatRate;
            }
        }

        /// <summary>
        /// Meal Breakfast Getter and Setter
        /// </summary>
        public bool MealBreakFast
        {
            get
            {
                return _mealBreakfast;
            }
            set
            {
                // If the value is not set
                if (value == null)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Breakfast meal bool is null");
                }
                else
                {
                    // Set the chalet meal breakfast to the value (true/false)
                    _mealBreakfast = value;
                }
            }
        }

        /// <summary>
        /// Meal Evening Getter and Setter
        /// </summary>
        public bool MealEvening
        {
            get
            {
                return _mealEvening;
            }
            set
            {
                // If the value is not set
                if (value == null)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Evening meal bool is null");
                }
                else
                {
                    // Set the chalet evening meal to the value
                    _mealEvening = value;
                }
            }
        }
    }
}
