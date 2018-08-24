using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class Booking
    {
        private DateTime arrivalDate;
        // booking ref should auto increment starting from 1
        private int bookingReferenceNumber;
        private int chaletID;
        private int customerNumber;
        private DateTime departureDate;
        private List<Guest> guests;

        public Booking(int bookingRef)
        {
            bookingReferenceNumber = bookingRef;
        }

    }
}
