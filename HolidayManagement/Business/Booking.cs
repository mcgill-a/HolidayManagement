using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    [Serializable]
    public class Booking
    {
        /*
         * Author:               40276245 (Alex McGill)         
         * Description:          This is the business class which defines the available attributes for each booking
         * Date last modified:   09/12/2017
        */
        
        // Declare private variables
        private DateTime _arrivalDate;
        private DateTime _departureDate;
        private int _bookingRefNumber;
        private Chalet _bookingChalet;
        private CarHire _bookingCarHire;
        private List<Guest> _bookingGuests;
        private int _customerRefNumber;
        private Invoice _bookingInvoice;
        
        /// <summary>
        /// Empty booking constructor
        /// </summary>
        public Booking()
        {

        }
        /// <summary>
        /// Booking constructor with booking reference
        /// </summary>
        /// <param name="bookingRef"></param>
        public Booking(int bookingRef)
        {
            // Set the booking reference number to the booking reference passed in
            _bookingRefNumber = bookingRef;
        }

        /// <summary>
        /// Booking Customer Reference Getter and Setter
        /// </summary>
        public int CustomerRef
        {
            get
            {
                return _customerRefNumber;
            }
            set
            {
                // If the value is more than or equal to 0
                if (value >= 0)
                {
                    // Set the booking customer reference number to the value
                    _customerRefNumber = value;
                }
                else
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Customer ref number not a valid integer");
                }
            }
        }
        
        /// <summary>
        /// Booking Arrival Getter and Setter
        /// </summary>
        public DateTime Arrival
        {
            get
            {
                return _arrivalDate;
            }
            set
            {
                // If the arrival date is null
                if (value == null)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Arrival date is not valid DD/MM/YYYY");
                }
                else
                {
                    // Set the booking arrival date to the value
                    _arrivalDate = value;
                }
            }
        }

        /// <summary>
        /// Booking Departure Getter and Setter
        /// </summary>
        public DateTime Departure
        {
            get
            {
                return _departureDate;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Departure date is not valid DD/MM/YYYY");
                }
                else
                {
                    _departureDate = value;
                }
            }
        }

        /// <summary>
        /// Booking reference number Getter
        /// </summary>
        public int BookingRef
        {
            get
            {
                return _bookingRefNumber;
            }
        }

        /// <summary>
        /// Booking chalet Getter and Setter
        /// </summary>
        public Chalet BookingChalet
        {
            get
            {
                return _bookingChalet;
            }
            set
            {
                // If chalet is null
                if (value == null)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Booking Chalet value is null");
                }
                else
                {
                    // Set the booking chalet to the value
                    _bookingChalet = value;
                }
            }
        }

        /// <summary>
        /// Booking Car Hire Getter and Setter
        /// </summary>
        public CarHire BookingCarHire
        {
            get
            {
                return _bookingCarHire;
            }
            set
            {
                // If the booking car hire is null
                if (value == null)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Car Hire value is null");
                }
                else
                {
                    // Set the booking car hire to the value
                    _bookingCarHire = value;
                }
            }
        }

        /// <summary>
        /// Booking Guest List Getter and Setter
        /// </summary>
        public List<Guest> BookingGuests
        {
            get
            {
                return _bookingGuests;
            }
            set
            {
                // If the guest list is less than 1 (empty)
                if (value.Count < 1)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Booking guest list is empty");
                }
                else
                {
                    // Set the booking guest list to the value
                    _bookingGuests = value;
                }
            }
        }

        /// <summary>
        /// Booking Invoice Getter and Setter
        /// </summary>
        public Invoice BookingInvoice
        {
            get
            {
                return _bookingInvoice;
            }
            set
            {
                // If the booking invoice is null
                if (value == null)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Booking invoice is null");
                }
                else
                {
                    // Set the booking invoice to the value
                    _bookingInvoice = value;
                }
            }
        }
    }
}