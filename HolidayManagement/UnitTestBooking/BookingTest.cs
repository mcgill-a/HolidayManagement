using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business;

namespace UnitTestBooking
{
    [TestClass]
    public class BookingTest
    {
        /* 
         * Author:               40276245 (Alex McGill)
         * Description:          This is the booking unit test class which tests each part
         *                       of the booking to ensure it works as expected
         * Date last modified:   10/12/2017
        */
        
        private TestContext testContextInstance;


        // Gets or sets the test context which provides
        // information about the functionality for the current test run
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }        
        
        [TestMethod()]
        public void ArrivalTest()
        {
            // Create booking with ID '100'
            Booking booking = new Booking(100);

            DateTime arrival = Convert.ToDateTime("10/10/2010");
            booking.Arrival = arrival;
            Assert.AreEqual(arrival, booking.Arrival, "Arrival Test");
        }

        [TestMethod()]
        public void DepartureTest()
        {
            // Create booking with ID '100'
            Booking booking = new Booking(100);

            DateTime departure = Convert.ToDateTime("20/10/2010");
            booking.Departure = departure;
            Assert.AreEqual(departure, booking.Departure, "Departure Test");
        }

        [TestMethod()]
        public void ChaletTest()
        {
            // Create booking with ID '100'
            Booking booking = new Booking(100);
            Chalet chalet = new Chalet();

            int id = 10;
            chalet.ChaletID = id;
            Assert.AreEqual(id, chalet.ChaletID, "Chalet - ID Test");

            bool breakfast = true;
            chalet.MealBreakFast = breakfast;
            Assert.AreEqual(breakfast, chalet.MealBreakFast, "Chalet - Meal Breakfast Test");

            bool evening = true;
            chalet.MealEvening = evening;
            Assert.AreEqual(evening, chalet.MealEvening, "Chalet - Meal Evening Test");
            
        }

        [TestMethod()]
        public void CarHireTest()
        {
            // Create booking with ID '100'
            Booking booking = new Booking(100);
            CarHire carHire = new CarHire();

            bool hired = true;
            carHire.Hired = hired;
            Assert.AreEqual(hired, carHire.Hired, "Car hire - Hired Test");

            DateTime start = Convert.ToDateTime("10/10/2010");
            carHire.DateStart = start;
            Assert.AreEqual(start, carHire.DateStart, "Car Hire - Date Start Test");

            DateTime end = Convert.ToDateTime("20/10/2010");
            carHire.DateEnd = end;
            Assert.AreEqual(end, carHire.DateEnd, "Car Hire - Date End Test");

            int cost = 500;
            carHire.Cost = cost;
            Assert.AreEqual(cost, carHire.Cost, "Car Hire - Cost Test");

            string name = "Person Person";
            carHire.DriverName = name;
            Assert.AreEqual(name, carHire.DriverName, "Car Hire - Driver Name Test");
        }

        [TestMethod()]
        public void GuestTest()
        {
            // Create booking with ID '100'
            Booking booking = new Booking(100);
            Guest guest = new Guest();

            int age = 30;
            guest.Age = age;
            Assert.AreEqual(age, guest.Age, "Guest - Age Test");

            string name = "Person Person";
            guest.Name = name;
            Assert.AreEqual(name, guest.Name, "Guest - Name Test");

            string passportNumber = "0123456789";
            guest.PassportNumber = passportNumber;
            Assert.AreEqual(passportNumber, guest.PassportNumber, "Guest - Passport Number Test");
        }

        [TestMethod()]
        public void InvoiceTest()
        {
            // Create booking with ID '100'
            Booking booking = new Booking(100);
            booking.CustomerRef = 200;
            DateTime arrival = Convert.ToDateTime("10/10/2010");
            booking.Arrival = arrival;
            DateTime departure = Convert.ToDateTime("20/10/2010");
            booking.Departure = departure;

            Chalet chalet = new Chalet();
            int id = 10;
            chalet.ChaletID = id;

            bool breakfast = true;
            chalet.MealBreakFast = breakfast;

            bool evening = true;
            chalet.MealEvening = evening;
            booking.BookingChalet = chalet;

            CarHire carHire = new CarHire();

            bool hired = true;
            carHire.Hired = hired;

            DateTime start = Convert.ToDateTime("10/10/2010");
            carHire.DateStart = start;

            DateTime end = Convert.ToDateTime("20/10/2010");
            carHire.DateEnd = end;

            int cost = 500;
            carHire.Cost = cost;

            string name = "Person Driver";
            carHire.DriverName = name;
            booking.BookingCarHire = carHire;

            Guest guest = new Guest();

            int age = 30;
            guest.Age = age;

            string guestName = "Person Guest";
            guest.Name = guestName;

            string passportNumber = "0123456789";
            guest.PassportNumber = passportNumber;

            List<Guest> guests = new List<Guest>();
            guests.Add(guest);
            booking.BookingGuests = guests;
            
            Invoice invoice = new Invoice(booking);

            int carHireLength = 10;
            Assert.AreEqual(carHireLength, invoice.CarHireLength, "Invoice - Car Hire Length Test");

            int carHireTotalCost = 500;
            Assert.AreEqual(carHireTotalCost, invoice.CarHireTotalCost, "Invoice - Car Hire Total Cost Test");

            int chaletCost = 85;
            Assert.AreEqual(chaletCost, invoice.ChaletCost, "Invoice - Chalet Cost Test");

            int extrasCost = 15;
            Assert.AreEqual(extrasCost, invoice.ExtrasCost, "Invoice - Chalet Extras Cost Test");

            int costPerNight = 100;
            Assert.AreEqual(costPerNight, invoice.CostPerNight, "Invoice - Chalet Total Cost Per Night Test");

            int tripLength = 10;
            Assert.AreEqual(tripLength, invoice.TripLength, "Invoice - Trip Length Test");

            int guestCount = 1;
            Assert.AreEqual(guestCount, invoice.GuestCount, "Invoice - Guest Count Test");

            int mealBreakFastCost = 5;
            Assert.AreEqual(mealBreakFastCost, invoice.MealBreakfastCost, "Invoice - Meal Breakfast Cost Test");

            int mealEveningCost = 10;
            Assert.AreEqual(mealEveningCost, invoice.MealEveningCost, "Invoice - Meal Evening Cost Test");

            int totalCost = 1500;
            Assert.AreEqual(totalCost, invoice.TotalCost, "Invoice - Total Cost Test");
        }
    }
}
