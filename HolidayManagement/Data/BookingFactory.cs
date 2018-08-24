using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class BookingFactory
    {
        /*
         * A Factory that creates bookings
         * Each booking has a unique id allocated by the factory
         * 
         */

        private int lastBookingID = 0;

        public Booking FactoryMethod()
        {
            lastBookingID++;
            return new Booking(lastBookingID);
        }

    }
}
