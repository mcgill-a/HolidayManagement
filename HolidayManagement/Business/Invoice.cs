using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    [Serializable]
    public class Invoice
    {
        /* 
         * Author:               40276245 (Alex McGill)
         * Description:          This is the invoice class for the booking which
         *                       defines the invoice attributes
         * Date last modified:   09/12/2017
        */
        
        // Declare private variables
        private int _bookingRefNum;
        private int _carHireTotalCost;
        private int _carHireLength;
        private int _chaletCost;
        private int _extrasCost;
        private int _costPerNight;
        private int _tripLength;
        private int _guestCount;
        private int _mealBreakfastCost;
        private int _mealEveningCost;
        private int _totalCost;

        /// <summary>
        /// Empty Invoice Constructor
        /// </summary>
        public Invoice()
        {

        }
        /// <summary>
        /// Invoice Constructor
        /// </summary>
        /// <param name="booking"></param>
        public Invoice (Booking booking)
        {
            // Set the booking reference number to the reference number of the booking
            _bookingRefNum = booking.BookingRef;
            // Set the guest count to the number of guests in the booking guests list
            _guestCount = booking.BookingGuests.Count;
            // Set the trip length to the number of days between the arrival and departure dates
            _tripLength = (booking.Departure - booking.Arrival).Days;
            // If the trip length is less than 1
            if (_tripLength < 1)
            {
                // Throw a new argument exception
                throw new ArgumentException("Departure date must be after arrival date.");
            }
            // If the booking car hire 'hired' status is true
            if (booking.BookingCarHire.Hired)
            {
                // Calculate the car hire length (end date - start date) in days)
                _carHireLength = (booking.BookingCarHire.DateEnd - booking.BookingCarHire.DateStart).Days;
            }
            else
            {
                // Set the car hire length to 0
                _carHireLength = 0;
            }
            // If the booking meal breakfast is true
            if (booking.BookingChalet.MealBreakFast)
            {
                // Set the meal breakfast cost to 5
                _mealBreakfastCost = 5;
            }
            else
            {
                // Set the meal breakfast cost to 0
                _mealBreakfastCost = 0;
            }
            // If the booking meal evening is true
            if (booking.BookingChalet.MealEvening)
            {
                // Set the meal evening cost to 10
                _mealEveningCost = 10;
            }
            else
            {
                // Set the meal evening cost to 0
                _mealEveningCost = 0;
            }
            // Calculate the chalet cost (flat rate + (25 * guest count))
            _chaletCost = booking.BookingChalet.FlatRate + (25 * _guestCount);
            // Calculate the extras cost ((breakfast + evening) * guest count))
            _extrasCost = (_mealBreakfastCost + _mealEveningCost) * _guestCount;
            // Calculate the car hire total cost (50 * car hire days total)
            _carHireTotalCost = 50 * _carHireLength;
            // Calculate the cost per night (chalet cost + extras cost)
            _costPerNight = _chaletCost + _extrasCost;
            // Calculate the total trip cost ((cost per night * trip length) + (car hire total cost))
            _totalCost = (_costPerNight * _tripLength) + (_carHireTotalCost);
        }
        /// <summary>
        /// Car Hire Length Getter
        /// </summary>
        public int CarHireLength
        {
            get
            {
                return _carHireLength;
            }
        }

        /// <summary>
        /// Car Hire Total Cost Getter
        /// </summary>
        public int CarHireTotalCost
        {
            get
            {
                return _carHireTotalCost;
            }
        }

        /// <summary>
        /// Chalet Cost Getter
        /// </summary>
        public int ChaletCost
        {
            get
            {
                return _chaletCost;
            }
        }

        /// <summary>
        /// Booking Extras Getter
        /// </summary>
        public int ExtrasCost
        {
            get
            {
                return _extrasCost;
            }
        }

        /// <summary>
        /// Booking Cost Per Night Getter and Setter
        /// </summary>
        public int CostPerNight
        {
            get
            {
                return _costPerNight;
            }
            set
            {
                // Set the booking cost per night to the value
                _costPerNight = value;
            }
        }

        /// <summary>
        /// Booking Trip Length Getter
        /// </summary>
        public int TripLength
        {
            get
            {
                return _tripLength;
            }
        }

        /// <summary>
        /// Booking Guest Count Getter
        /// </summary>
        public int GuestCount
        {
            get
            {
                return _guestCount;
            }
        }

        /// <summary>
        /// Booking Meal Breakfast Cost Getter
        /// </summary>
        public int MealBreakfastCost
        {
            get
            {
                return _mealBreakfastCost;
            }
        }

        /// <summary>
        /// Booking Meal Evening Cost Getter
        /// </summary>
        public int MealEveningCost
        {
            get
            {
                return _mealEveningCost;
            }
        }

        /// <summary>
        /// Booking Reference Getter
        /// </summary>
        public int BookingRef
        {
            get
            {
                return _bookingRefNum;
            }
        }

        /// <summary>
        /// Booking Total Cost Getter
        /// </summary>
        public int TotalCost
        {
            get
            {
                return _totalCost;
            }
        }
    }
}