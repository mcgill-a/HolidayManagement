using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Business;
using Data;

namespace Presentation
{
    public partial class MainWindow : Window
    {
        /* 
         * Author:                  40276245 (Alex McGill)
         * Description:             This is the main window cs file for the Holiday Management Program.
         *                          It provides the functionality for the UI
         * Date last modified:      09/12/2017
         * Design Patterns:         This class uses the Data Facade
        */

        // Create a new DataFacade
        DataFacade facade = new DataFacade();
        // Create a new list to store current guests
        List<Guest> guests = new List<Guest>();
        public MainWindow()
        {
            InitializeComponent();

            // Check if a serialized file of customers exists
            if (facade.SerializedFileExists())
            {
                // Deserialize the customers (load them into the customer list storage)
                facade.DeSerializeCustomers();
                // Update the lists containing customers and bookings
                UpdateCustList();
                UpdateBookingList();
                UpdateCustCmb();
            }
            
            // Set the default visibility of the car hire driver inputs to hidden
            rctCarHire.Visibility = Visibility.Hidden;

            txtCarHireDriver.Visibility = Visibility.Hidden;
            dpCarHireStart.Visibility = Visibility.Hidden;
            dpCarHireEnd.Visibility = Visibility.Hidden;

            lblCarHireDriver.Visibility = Visibility.Hidden;
            lblCarHireStart.Visibility = Visibility.Hidden;
            lblCarHireEnd.Visibility = Visibility.Hidden;

            // Populate the chalet combo box list with chalets 1-10
            populateChaletList();

        }

        /// <summary>
        /// This method is run when the program is exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            // Serialize the customer list (customer list includes bookings etc.)
            facade.SerializeCustomers();
        }

        /// <summary>
        /// Update the customer combo box with the current list of customers
        /// </summary>
        private void UpdateCustCmb()
        {
            // Clear the combo box
            cmbRefNumber.Items.Clear();
            // Clear the selected customer input (name, address)
            txtBCustAddress.Text = "";
            txtBCustName.Text = "";
            // Loop through each customer in the customer list
            foreach(Customer c in facade.GetCustomers())
            {
                // Add the current customer to the combo box list of customers
                cmbRefNumber.Items.Add(c.RefNumber);
            }
        }

        /// <summary>
        /// Button for automatically generating customers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCustGenerate_Click(object sender, RoutedEventArgs e)
        {
            // Runs the customer generate method
            CustomerGenerate();
        }

        /// <summary>
        /// Automatically generate 5 pre-set customers for quick testing
        /// </summary>
        public void CustomerGenerate()
        {
            // Add 5 Customers using the data facade and pre-set data
            facade.AddCustomer("Alex McGill", "Old Post Office House,\nCulrain,\nArdgay,\nSutherland,\nIV24 3DW");
            facade.AddCustomer("Michael Scott", "Flat 5, Block 7,\nMorrison Circus,\nEH3 8DX");
            facade.AddCustomer("Jim Halpert", "Flat 6, Block 7,\nMorrison Circus,\nEH3 8DX");
            facade.AddCustomer("Pam Beasley", "Flat 7, Block 7,\nMorrison Circus,\nEH3 8DX");
            facade.AddCustomer("Dwight Schrute", "Flat 8, Block 7,\nMorrison Circus,\nEH3 8DX");
            // Update the customer lists + combo box
            UpdateCustCmb();
            UpdateCustList();
        }

        /// <summary>
        /// Populate the combo box list of chalets
        /// </summary>
        public void populateChaletList()
        {
            int maxChaletCount = 10;
            // Loop 10 times
            for (int i = 1; i <= maxChaletCount; i++)
            {
                // Add the current number to the list of chalets
                cmbChalet.Items.Add(i.ToString());
            }
        }

        /// <summary>
        /// Button for adding a Guest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuestAdd_Click(object sender, RoutedEventArgs e)
        {
            // Run the create guest method
            createGuest();
        }

        /// <summary>
        /// Create a guest using the specified user input
        /// </summary>
        public void createGuest()
        {
            // Create new guest object
            Guest created = new Guest();
            try
            {
                // Initialise variables used in guest creation
                int age = -1;
                bool result;
                // Set the guest name to the user input
                created.Name = txtGuestName.Text;
                // Set the passport number to the user input
                created.PassportNumber = txtGuestPassportNum.Text;
                // Try and convert the age from a string to an integer
                result = Int32.TryParse(txtGuestAge.Text, out age);
                // If converting the age input from a string to an integer failed
                if (!result)
                {
                    // Set the age to -1 (out of valid range)
                    age = -1;
                }
                // Set the age to the now converted age integer
                created.Age = age;
                // Add the guest to the current list of guests
                guests.Add(created);
                // Clear the guest inputs
                ClearGuestInput();
            }
                // Catch any argument exceptions
            catch (ArgumentException excep)
            {
                // Set the message box to have an OK button
                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                // Set the message box to have an error icon
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;
                // Set the message box caption to 'booking error'
                string caption = "Booking Error";
                // Show the message box specified
                MessageBox.Show(excep.Message, caption, btnMessageBox, icnMessageBox);
            }
            // Update the guest list
            UpdateGuestList();
        }

        /// <summary>
        /// Update the guest list
        /// </summary>
        public void UpdateGuestList()
        {
            // Clear the guest listbox
            lstGuests.Items.Clear();
            // Loop through the list of guests
            foreach (Guest g in guests)
            {
                // Add the current guest passport number and name
                lstGuests.Items.Add(g.PassportNumber + ": " + g.Name);
            }
        }

        /// <summary>
        /// Button for editing a guest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuestEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Loop through each guest in the guest list
                foreach (Guest g in guests)
                {
                    // If the current guest passport number matches the selected guest passport number
                    if (g.PassportNumber == GetSelectedGuest())
                    {
                        int age = -1;
                        bool result;
                        // Try and set the guest attributes to the inputs provided
                        g.Name = txtGuestName.Text;
                        g.PassportNumber = txtGuestPassportNum.Text;
                        // Try and convert the age from a string to an integer
                        result = Int32.TryParse(txtGuestAge.Text, out age);
                        // If converting the age input from a string to an integer failed
                        if (!result)
                        {
                            // Set the age to -1 (out of valid range)
                            age = -1;
                        }
                        // Set the guest age
                        g.Age = age;
                        // Clear the guest inputs
                        ClearGuestInput();
                    }
                }
            }
            // Catch any argument exceptions
            catch (ArgumentException excep)
            {
                // Set the message box to have an OK button
                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                // Set the message box to have an error icon
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;
                // Set the message box caption to 'booking error'
                string caption = "Booking Error";
                // Show the message box specified
                MessageBox.Show(excep.Message, caption, btnMessageBox, icnMessageBox);
            }
            // Deselect the the guest on the guest list box
            lstGuests.SelectedIndex = -1;
            // Update the guest list box
            UpdateGuestList();
        }

        /// <summary>
        /// Button for removing a guest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuestRemove_Click(object sender, RoutedEventArgs e)
        {
            // If a guest is selected
            if (GetSelectedGuest() != null)
            {
                // Remove the selected guest
                removeGuest(GetSelectedGuest());
                // Clear the guest inputs
                ClearGuestInput();
                // Update the guest list box
                UpdateGuestList();
            }
            // Unselect the guest from the guest list
            lstGuests.SelectedIndex = -1;
        }

        /// <summary>
        /// Remove a guest
        /// </summary>
        /// <param name="passportNum"></param>
        public void removeGuest(string passportNum)
        {
            // Find the guest using the inputted passport number
            Guest g = this.findGuest(passportNum);

            // If the guest was found
            if (g != null)
            {
                // Remove the guest from the guest list
                guests.Remove(g);
            }
            // Update the guest list box
            UpdateGuestList();
        }

        /// <summary>
        /// Find a guest
        /// </summary>
        /// <param name="passportNum"></param>
        /// <returns></returns>
        public Guest findGuest(string passportNum)
        {
            // Create new guest object
            Guest guest = new Guest();
            // Loop through each guest in the guest list
            foreach (Guest g in guests)
            {
                // If the current guest passport number matches the provided passport number
                if (g.PassportNumber == passportNum)
                {
                    // Set the created guest equal to the current guest
                    guest = g;
                    // Return the guest
                    return guest;
                }
            }
            // Otherwise return null
            return null;
        }

        /// <summary>
        /// Display the selected guest
        /// </summary>
        /// <param name="passportNum"></param>
        public void DisplaySelectedGuest(string passportNum)
        {
            // Get the passport number by splitting the string input provided
            passportNum = passportNum.Split(':')[0];
            // Find the guest using the passport number
            Guest guest = findGuest(passportNum);
            // If the guest was found
            if (guest != null)
            {
                // Set the guest text inputs to the found guest
                txtGuestName.Text = guest.Name;
                txtGuestPassportNum.Text = guest.PassportNumber;
                txtGuestAge.Text = guest.Age.ToString();
            }
        }

        /// <summary>
        /// Guest list box selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstGuests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If something is selected
            if (lstGuests.SelectedIndex != -1)
            {
                // Display the selected guest
                DisplaySelectedGuest(lstGuests.SelectedItem.ToString());
            }
        }

        /// <summary>
        /// Clear all the user inputs
        /// </summary>
        public void ClearAll()
        {
            // Clear the customer inputs
            ClearCustomerInput();
            // Clear the guest inputs
            ClearGuestInput();
            // Clear the booking details inputs
            ClearDetails();
        }

        /// <summary>
        /// Clear the customer input
        /// </summary>
        public void ClearSelectedCustInput()
        {
            txtBCustAddress.Text = "";
            txtBCustName.Text = "";
        }

        /// <summary>
        /// Clear the guest inputs
        /// </summary>
        public void ClearGuestInput()
        {
            txtGuestAge.Text = "";
            txtGuestName.Text = "";
            txtGuestPassportNum.Text = "";
        }

        /// <summary>
        /// Clear the customer inputs
        /// </summary>
        public void ClearCustomerInput()
        {
            txtCustAddress.Text = "";
            txtCustFullName.Text = "";
        }

        /// <summary>
        /// Clear the car hire inputs
        /// </summary>
        public void ClearCarHireInputs()
        {
            txtCarHireDriver.Text = "";
            dpCarHireStart.SelectedDate = null;
            dpCarHireEnd.SelectedDate = null;
        }

        /// <summary>
        /// Clear the booking details
        /// </summary>
        public void ClearDetails()
        {
            dpArrival.SelectedDate = null;
            dpDeparture.SelectedDate = null;

            chkMealBreakfast.IsChecked = false;
            chkMealEvening.IsChecked = false;
            chkCarHire.IsChecked = false;
        }

        /// <summary>
        /// Button for adding a booking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Run the add booking method
                AddBookingButton();

            }
            // Catch any argument exceptions
            catch (ArgumentException excep)
            {
                // Set the message box to have an OK button
                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                // Set the message box to have an error icon
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;
                // Set the message box caption to 'booking error'
                string caption = "Booking Error";
                // Set the error message
                string errorMsg = "Booking failed;\n" + excep;
                // Show the message box specified
                MessageBox.Show(errorMsg, caption, btnMessageBox, icnMessageBox);
            }
        }

        /// <summary>
        /// Method that is run when the add booking button is pressed
        /// </summary>
        public void AddBookingButton()
        {
            // Initialise the 'valid' variable, used as a checker to ensure all the inputs are valid
            bool valid = true;
            try
            {
                // Get the selected customer reference number
                int custRefNum = GetSelectedCustRef();
                // If the customer reference number is -1 (no customer has been selected)
                if (custRefNum == -1)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Please select a customer reference number");
                    // Set valid to false;
                    valid = false;
                }
                // Initialise the arrival and departure date
                DateTime arrivalDate;
                DateTime departureDate;
                // If an arrival date has been selected
                if (dpArrival.SelectedDate != null)
                {
                    // Set the arrival date
                    arrivalDate = dpArrival.SelectedDate.Value;
                }
                else
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Arrival date not valid");
                    valid = false;
                }
                // If a departure date has been selected
                if (dpDeparture.SelectedDate != null)
                {
                    // Set the departure date
                    departureDate = dpDeparture.SelectedDate.Value;
                }
                else
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Departure date not valid");
                    valid = false;
                }
                // If the arrival date is after the departure date
                if (arrivalDate > departureDate)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Cannot set arrival date to after departure date");
                    valid = false;
                }    

                // Initialise the booking chalet
                Chalet bookingChalet = new Chalet();
                // If a chalet has been selected
                if (GetSelectedChalet() != -1)
                {
                    // Set the chalet to the selected chalet
                    bookingChalet.ChaletID = GetSelectedChalet();
                }
                else
                {
                    // Throw a new argument exception
                    throw new ArgumentException("No chalet selected");
                    valid = false;
                }
                // If the meal breakfast checkbox is checked
                if (chkMealBreakfast.IsChecked.Value)
                {
                    // Set the meal breakfast variable to true
                    bookingChalet.MealBreakFast = true;
                }
                else
                {
                    // Set the meal breakfast variable to false
                    bookingChalet.MealBreakFast = false;
                }
                // If the meal evening checkbox is checked
                if (chkMealEvening.IsChecked.Value)
                {
                    // Set the meal evening variable to true;
                    bookingChalet.MealEvening = true;
                }
                else
                {
                    // Set the meal evening variable to false;
                    bookingChalet.MealEvening = false;
                }
                
                // Initialise the booking car hire
                CarHire bookingCar = new CarHire();
                // Set the hired flag to false (default)
                bookingCar.Hired = false;
                // If the car hire checkbox is checked
                if (chkCarHire.IsChecked == true)
                {
                    // Set the hired status to true
                    bookingCar.Hired = true;
                    // Set the driver name to the drivfer name text box
                    bookingCar.DriverName = txtCarHireDriver.Text;
                    // If the car hire start date is selected
                    if (dpCarHireStart.SelectedDate != null)
                    {
                        // Set the car hire start date to the selected date
                        bookingCar.DateStart = dpCarHireStart.SelectedDate.Value;
                    }
                    else
                    {
                        // Throw a new argument exception
                        throw new ArgumentException("Please select the car hire start date");
                        valid = false;
                    }
                    // If the car hire end date is selected
                    if (dpCarHireEnd.SelectedDate != null)
                    {
                        // Set the car hire end date to the selected date
                        bookingCar.DateEnd = dpCarHireEnd.SelectedDate.Value;
                    }
                    else
                    {
                        // Throw a new argument exception
                        throw new ArgumentException("Please select the car hire end date");
                        valid = false;
                    }
                    // If the car hire end date is before the car hire start date
                    if (bookingCar.DateEnd < bookingCar.DateStart)
                    {
                        // Throw a new argument exception
                        throw new ArgumentException("Cannot set car hire start date to after car hire end date");
                        valid = false;
                    }
                    // If the car hire start date is before the arrival date or after the departure date
                    if (bookingCar.DateStart < arrivalDate || bookingCar.DateStart > departureDate)
                    {
                        // Throw a new argument exception
                        throw new ArgumentException("Car hire start date is not within the arrival and departure date range");
                        valid = false;
                    }
                    // If the car hire end date is before the arrival date or after the departure date
                    if (bookingCar.DateEnd < arrivalDate || bookingCar.DateEnd > departureDate)
                    {
                        // Throw a new argument exception
                        throw new ArgumentException("Car hire end date is not within the arrival and departure date range");
                        valid = false;
                    }
                }
                // If no guests have been created
                if (guests.Count < 1)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Please enter at least one guest");
                    valid = false;
                }

                // Loop through each booking
                foreach(Booking b in facade.GetBookings())
                {
                    // If the current booking chalet matches the user input chalet
                    if (b.BookingChalet.ChaletID == bookingChalet.ChaletID)
                    {
                        // If the dates clash
                        if(DatesClash(arrivalDate, departureDate, b.Arrival, b.Departure))
                        {
                            // Throw a new argument exception
                            throw new ArgumentException("Chalet " + bookingChalet.ChaletID + " is occupied between "
                                + b.Arrival.ToShortDateString() + " and " + b.Departure.ToShortDateString() + ".");
                            valid = false;
                            break;
                        }
                    }
                }
                // If valid is true (everything has passed validation checks)
                if (valid)
                {
                    // Use the facade to add a new booking using the provided user input
                    facade.AddBooking(custRefNum, arrivalDate, departureDate, bookingChalet, bookingCar, guests);
                    // Update the booking list and reset all inputs
                    UpdateBookingList();
                    resetBookingWindow();
                    clearInvoice();
                    // Display a messagebox to let the user know the booking has been confirmed
                    MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Information;
                    string caption = "Booking Comfirmation";
                    MessageBox.Show("Booking has been confirmed.", caption, btnMessageBox, icnMessageBox);
                }
            }
                // Catch any argument exceptions
            catch (ArgumentException excep)
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;
                string caption = "Booking Error";
                // Display the message box showing the exception
                MessageBox.Show(excep.Message, caption, btnMessageBox, icnMessageBox);
            } 
        }

        /// <summary>
        /// Reset the booking window inputs
        /// </summary>
        public void resetBookingWindow()
        {
            // Clear the user inputs
            ClearAll();
            // Clear the guest list
            guests.Clear();
            // Clear the guest list box
            lstGuests.Items.Clear();
            // Clear the customer input text boxes
            ClearSelectedCustInput();

            // Uncheck any check boxes
            chkMealBreakfast.IsChecked = false;
            chkMealEvening.IsChecked = false;
            chkCarHire.IsChecked = false;

            // Clear the combo boxes
            cmbRefNumber.SelectedItem = null;
            cmbChalet.SelectedItem = null;
        }

        /// <summary>
        /// Check if two sets of dates clash with each other
        /// </summary>
        /// <param name="first_arrival"></param>
        /// <param name="first_departure"></param>
        /// <param name="second_arrival"></param>
        /// <param name="second_departure"></param>
        /// <returns></returns>
        private bool DatesClash(DateTime first_arrival, DateTime first_departure, DateTime second_arrival, DateTime second_departure)
        {
            // IF the first arrival is less than the second arrival AND the first departure is less than the second arrival
            // OR the first arrival is more than the second departure AND the first departure is more than the second departure
            if ((first_arrival < second_arrival && first_departure < second_arrival) || (first_arrival > second_departure && first_departure > second_departure))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Unhide car hire options when the car hire checkbox is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCarHire_Checked(object sender, RoutedEventArgs e)
        {
            // Set the visiblity of the car hire options to visible
            rctCarHire.Visibility = Visibility.Visible;

            txtCarHireDriver.Visibility = Visibility.Visible;
            dpCarHireStart.Visibility = Visibility.Visible;
            dpCarHireEnd.Visibility = Visibility.Visible;

            lblCarHireDriver.Visibility = Visibility.Visible;
            lblCarHireStart.Visibility = Visibility.Visible;
            lblCarHireEnd.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Hide the car hire options when the car hire checkbox is unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCarHire_Unchecked(object sender, RoutedEventArgs e)
        {
            // Clear the car hire inputs
            ClearCarHireInputs();
            // Set the visiblity of the car hire options to hidden
            rctCarHire.Visibility = Visibility.Hidden;

            txtCarHireDriver.Visibility = Visibility.Hidden;
            dpCarHireStart.Visibility = Visibility.Hidden;
            dpCarHireEnd.Visibility = Visibility.Hidden;

            lblCarHireDriver.Visibility = Visibility.Hidden;
            lblCarHireStart.Visibility = Visibility.Hidden;
            lblCarHireEnd.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Remove button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                // If a customer is selected
                if (GetSelectedCustomer() != -1)
                {
                    // Set 'remove' to the selected customer id
                    int remove = GetSelectedCustomer();
                    // Ask the user if they are sure they want to remove the selected customer
                    string sMessageBoxText = "Are you sure you want to delete customer ID: " + remove + "?";
                    string sCaption = "Holiday Management";
                    MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Question;
                    MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                    // If the user clicks yes
                    if (rsltMessageBox == MessageBoxResult.Yes)
                    {
                        // Get the number of bookings that the selected customer has
                        int bookingCount = facade.GetCustomer(remove).Bookings.Count;
                        // If the selected customer has no bookings
                        if (bookingCount < 1)
                        {
                            // Use the facade to remove the customer
                            facade.RemoveCustomer(remove);
                            // Clear the customer inputs
                            ClearCustomerInput();
                            // Update the customer list and combo box
                            UpdateCustCmb();
                            UpdateCustList();
                        }
                        else
                        {
                            // If the customer has 1 booking
                            if (bookingCount == 1)
                            {
                                throw new ArgumentException("Cannot remove customer " + remove + " as they have an existing booking");
                            }
                                // If the customer has more than 1 booking
                            else
                            {
                                throw new ArgumentException("Cannot remove customer " + remove + " as they have " + bookingCount + " existing bookings");
                            }
                        }
                    }
                }
            }
            catch (ArgumentException excep)
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;
                string caption = "Booking Error";
                // Show a message box with the exception
                MessageBox.Show(excep.Message, caption, btnMessageBox, icnMessageBox);
            }
        }

        /// <summary>
        /// Get the selected customer reference number from the list box
        /// </summary>
        /// <returns></returns>
        private int GetSelectedCustomer()
        {
            // If a customer has been selected
            if (lstCustomers.SelectedIndex != -1)
            {
                // Get the string of the selected customer
                string selectedCustomer = lstCustomers.SelectedItem.ToString();
                // Split the string to get only the customer reference number and not the customer name
                string selectedCustomerSplit = selectedCustomer.Split('\t')[0];
                // Convert the customer reference number string to an integer
                int output = Convert.ToInt32(selectedCustomerSplit);
                // Return the customer reference number
                return output;
            }
            else
            {
                // Return -1 to show that nothing was selected
                return -1;
            }
        }

        /// <summary>
        /// Get the selected guest passport number
        /// </summary>
        /// <returns></returns>
        private string GetSelectedGuest()
        {
            // If a guest is selected
            if (lstGuests.SelectedIndex != -1)
            {
                // Get the string of the selected guest
                string selectedGuest = lstGuests.SelectedItem.ToString();
                // Split the string to get only the guest passport number and not the guest name
                string selectedGuestSplit = selectedGuest.Split(':')[0];
                // Return the guest passport number
                return selectedGuestSplit;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get the selected customer reference number from the combo box
        /// </summary>
        /// <returns></returns>
        private int GetSelectedCustRef()
        {
            // If a customer reference number is selected
            if (cmbRefNumber.SelectedItem != null)
            {
                // Get the string of the selected customer
                string selectedCustomer = cmbRefNumber.SelectedItem.ToString();
                // Split the string to get only the customer reference number
                string selectedCustomerSplit = selectedCustomer.Split('\t')[0];
                // Convert the customer reference string to an integer
                int output = Convert.ToInt32(selectedCustomerSplit);
                // Return the customer reference number
                return output;
            }
            else
            {
                // Return -1 to show that nothing was selected
                return -1;
            }

        }

        /// <summary>
        /// Get the selected chalet
        /// </summary>
        /// <returns></returns>
        private int GetSelectedChalet()
        {
            // If a chalet is selected
            if (cmbChalet.SelectedItem != null)
            {
                // Get the string of the selected chalet
                string selectedChalet = cmbChalet.SelectedItem.ToString();
                // Convert the string of the selected chalet to an integer
                int output = Convert.ToInt32(selectedChalet);
                // Return the chalet ID
                return output;
            }
            else
            {
                // Return -1 to show that no chalet was selected
                return -1;
            }
        }

        /// <summary>
        /// Button for editing a customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCustEdit_Click(object sender, RoutedEventArgs e)
        {
            // If a customer is selected
            if (GetSelectedCustomer() != -1)
            {
                // Get the selected customer reference number
                int toEdit = GetSelectedCustomer();
                // Set the name to the name input box
                string name = txtCustFullName.Text;
                // Set the address to the address input box
                string address = txtCustAddress.Text;
                // Use the facade to edit the customer with a matching reference number with the new inputs
                facade.EditCustomer(toEdit, name, address);
                // Update the customer combo box and customer list
                UpdateCustCmb();
                UpdateCustList();
            }
        }

        /// <summary>
        /// Update the customer list box
        /// </summary>
        private void UpdateCustList()
        {
            // Clear the customer list box
            lstCustomers.Items.Clear();
            // Loop through each customer in the customers list using the facade
            foreach (Customer c in facade.GetCustomers())
            {
                // Add the current customers reference number and name to the list box of customers
                lstCustomers.Items.Add(c.RefNumber + "\t" + c.Name);
            }
        }

        /// <summary>
        /// Update the booking list boxes
        /// </summary>
        private void UpdateBookingList()
        {
            // Clear the list of bookings on the main window and invoice window
            lstBookings.Items.Clear();
            lstFinanceBookings.Items.Clear();
            // Loop through each booking using the facade to get the bookings
            foreach (Booking b in facade.GetBookings())
            {
                // Add the current booking to both lists of bookings
                lstBookings.Items.Add(b.BookingRef.ToString());
                lstFinanceBookings.Items.Add(b.BookingRef.ToString());
            }
        }

        /// <summary>
        /// Method for when the list of customer selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If a customer is selected
            if (lstCustomers.SelectedIndex != -1)
            {
                // Get the string of the customer reference number
                string customerOutput = lstCustomers.SelectedItem.ToString();
                // Split the string to get only the reference number and not the name
                string customerOutputSplit = customerOutput.Split('\t')[0];
                // Convert the customer reference string to an integer
                int displayInput = Convert.ToInt32(customerOutputSplit);
                // Run the Display Customer method and pass it the selected customer reference number
                DisplayCustomer(displayInput);
            }
        }

        /// <summary>
        /// Display a customer
        /// </summary>
        /// <param name="refNumber"></param>
        public void DisplayCustomer(int refNumber)
        {
            // Loop through each customer in the list of customers using the facade
            foreach(Customer c in facade.GetCustomers())
            {
                // If the current customer reference number matches the reference number passed in
                if (c.RefNumber == refNumber)
                {
                    // Set the text boxes to the attributes of the current customer
                    txtCustFullName.Text = c.Name;
                    txtCustAddress.Text = c.Address;
                    txtCustRefNum.Text = c.RefNumber.ToString();
                }
            }
        }

        /// <summary>
        /// Add customer button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCustAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the name and address of the customer that was input
                string name = txtCustFullName.Text;
                string address = txtCustAddress.Text;

                // Add the customer using the facade with the name and address
                facade.AddCustomer(name, address);
                // Update the customer list box and combo box
                UpdateCustList();
                UpdateCustCmb();
                // Clear the customer inputs
                ClearCustomerInput();
            }
                // Catch any exceptions
            catch (ArgumentException excep)
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;
                string caption = "Booking Error";
                // Show the messagebox with the exception
                MessageBox.Show(excep.Message, caption, btnMessageBox, icnMessageBox);
            }
        }

        /// <summary>
        /// Clear customer input button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCustClear_Click(object sender, RoutedEventArgs e)
        {
            // Clear the name and address text inputs
            txtCustFullName.Text = "";
            txtCustAddress.Text = "";
        }

        /// <summary>
        /// Customer reference number combo box selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRefNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If a customer reference number is selected
            if (cmbRefNumber.SelectedItem != null)
            {
                // Get the reference number string
                string refNumber = cmbRefNumber.SelectedItem.ToString();
                // Convert the reference number string to an integer
                int displayInput = Convert.ToInt32(refNumber);
                // Call Display Booking Customer method, passing it the reference number
                DisplayBookingCustomer(displayInput);
            }
        }

        /// <summary>
        /// Display Booking Customer
        /// </summary>
        /// <param name="refNumber"></param>
        public void DisplayBookingCustomer(int refNumber)
        {
            // Loop through each customer in the list of customers using the facade
            foreach (Customer c in facade.GetCustomers())
            {
                // If the customer reference number matches the input reference number
                if (c.RefNumber == refNumber)
                {
                    // Set the customer name and address text boxes to the current customer name and address
                    txtBCustName.Text = c.Name;
                    txtBCustAddress.Text = c.Address;
                }
            }
        }

        /// <summary>
        /// Update booking button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateBooking_Click_1(object sender, RoutedEventArgs e)
        {
            // If a booking is selected
            if (lstBookings.SelectedIndex != -1)
            {
                // Convert the selected booking reference number string to an integer
                int bookingRefNum = Convert.ToInt32(lstBookings.SelectedItem.ToString());
                string sMessageBoxText = "Are you sure you want to update the current booking (" + bookingRefNum + ")?";
                string sCaption = "Holiday Management";
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Question;
                // Ask the user if they want to update the selected customer
                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                // If the user clicks yes
                if (rsltMessageBox == MessageBoxResult.Yes)
                {
                    // Call the Update Selected Booking method, passing it the booking reference number
                    updateSelectedBooking(bookingRefNum);
                }
            }
            else
            {
                // Display a messagebox saying no booking is selected
                MessageBox.Show("Cannot update booking: No booking selected");
            }
            
        }

        /// <summary>
        /// Update the selected booking
        /// </summary>
        /// <param name="bookingRefNum"></param>
        public void updateSelectedBooking(int bookingRefNum)
        {
            try
            {
                // Initialise variables used in the creation of a booking
                bool valid = true;
                int custRefNum;
                // Find the booking via the reference number and set it to 'temp'
                Booking temp = facade.FindBooking(bookingRefNum);
                // Get the customer reference number from the booking
                custRefNum = temp.CustomerRef;

                // Initialise the arrival and departure date
                DateTime arrivalDate;
                DateTime departureDate;
                // If an arrival date has been selected
                if (dpArrival.SelectedDate != null)
                {
                    // Set the arrival date
                    arrivalDate = dpArrival.SelectedDate.Value;
                }
                else
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Arrival date not valid");
                    valid = false;
                }
                // If a departure date has been selected
                if (dpDeparture.SelectedDate != null)
                {
                    // Set the departure date
                    departureDate = dpDeparture.SelectedDate.Value;
                }
                else
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Departure date not valid");
                    valid = false;
                }
                // If the arrival date is after the departure date
                if (arrivalDate > departureDate)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Cannot set arrival date to after departure date");
                    valid = false;
                }

                // Initialise the booking chalet
                Chalet bookingChalet = new Chalet();
                // If a chalet has been selected
                if (GetSelectedChalet() != -1)
                {
                    // Set the chalet to the selected chalet
                    bookingChalet.ChaletID = GetSelectedChalet();
                }
                else
                {
                    // Throw a new argument exception
                    throw new ArgumentException("No chalet selected");
                    valid = false;
                }
                // If the meal breakfast checkbox is checked
                if (chkMealBreakfast.IsChecked.Value)
                {
                    // Set the meal breakfast variable to true
                    bookingChalet.MealBreakFast = true;
                }
                else
                {
                    // Set the meal breakfast variable to false
                    bookingChalet.MealBreakFast = false;
                }
                // If the meal evening checkbox is checked
                if (chkMealEvening.IsChecked.Value)
                {
                    // Set the meal evening variable to true;
                    bookingChalet.MealEvening = true;
                }
                else
                {
                    // Set the meal evening variable to false;
                    bookingChalet.MealEvening = false;
                }

                // Initialise the booking car hire
                CarHire bookingCar = new CarHire();
                // Set the hired flag to false (default)
                bookingCar.Hired = false;
                // If the car hire checkbox is checked
                if (chkCarHire.IsChecked == true)
                {
                    // Set the hired status to true
                    bookingCar.Hired = true;
                    // Set the driver name to the drivfer name text box
                    bookingCar.DriverName = txtCarHireDriver.Text;
                    // If the car hire start date is selected
                    if (dpCarHireStart.SelectedDate != null)
                    {
                        // Set the car hire start date to the selected date
                        bookingCar.DateStart = dpCarHireStart.SelectedDate.Value;
                    }
                    else
                    {
                        // Throw a new argument exception
                        throw new ArgumentException("Please select the car hire start date");
                        valid = false;
                    }
                    // If the car hire end date is selected
                    if (dpCarHireEnd.SelectedDate != null)
                    {
                        // Set the car hire end date to the selected date
                        bookingCar.DateEnd = dpCarHireEnd.SelectedDate.Value;
                    }
                    else
                    {
                        // Throw a new argument exception
                        throw new ArgumentException("Please select the car hire end date");
                        valid = false;
                    }
                    // If the car hire end date is before the car hire start date
                    if (bookingCar.DateEnd < bookingCar.DateStart)
                    {
                        // Throw a new argument exception
                        throw new ArgumentException("Cannot set car hire start date to after car hire end date");
                        valid = false;
                    }
                    // If the car hire start date is before the arrival date or after the departure date
                    if (bookingCar.DateStart < arrivalDate || bookingCar.DateStart > departureDate)
                    {
                        // Throw a new argument exception
                        throw new ArgumentException("Car hire start date is not within the arrival and departure date range");
                        valid = false;
                    }
                    // If the car hire end date is before the arrival date or after the departure date
                    if (bookingCar.DateEnd < arrivalDate || bookingCar.DateEnd > departureDate)
                    {
                        // Throw a new argument exception
                        throw new ArgumentException("Car hire end date is not within the arrival and departure date range");
                        valid = false;
                    }
                }
                // If no guests have been created
                if (guests.Count < 1)
                {
                    // Throw a new argument exception
                    throw new ArgumentException("Please enter at least one guest");
                    valid = false;
                }
                
                // Loop through each booking using the facade to get all bookings
                foreach(Booking b in facade.GetBookings())
                {
                    // If the booking chalet ID is equal to the newly inputted chalet ID and the booking reference number is not equal to the current booking reference number
                    if ((b.BookingChalet.ChaletID == bookingChalet.ChaletID) && (bookingRefNum != b.BookingRef))
                    {
                        // If the dates clash
                        if(DatesClash(arrivalDate, departureDate, b.Arrival, b.Departure))
                        {
                            // Throw a new argument exception
                            throw new ArgumentException("Chalet " + bookingChalet.ChaletID + " is occupied between "
                                + b.Arrival.ToShortDateString() + " and " + b.Departure.ToShortDateString() + ".");
                            valid = false;
                            break;
                        }
                    }
                }
                // If valid is true (everything has passed validation checks)
                if (valid)
                {
                    // Use the facade to edit the booking using the new inputs provided
                    facade.EditBooking(bookingRefNum, custRefNum, arrivalDate, departureDate, bookingChalet, bookingCar, guests);
                    // Clear the invoice tab
                    clearInvoice();
                    // Display a message box to let the user know the booking has been updated
                    MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Information;
                    string caption = "Booking Update Comfirmation";
                    MessageBox.Show("Booking has been updated.", caption, btnMessageBox, icnMessageBox);
                }
            }
                // Catch any exceptions
            catch(ArgumentException excep)
            {
                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;
                string caption = "Booking Error";
                // Show the user a message box with the exception message
                MessageBox.Show(excep.Message, caption, btnMessageBox, icnMessageBox);
            }
        }      

        /// <summary>
        /// Remove the selected booking
        /// </summary>
        /// <param name="bookingRefNum"></param>
        public void removeSelectedBooking(int bookingRefNum)
        {
            // Find the booking using the facade with the booking reference number and set it to the temp booking
            Booking temp = facade.FindBooking(bookingRefNum);
            // Use the facade to remove the booking with the customer reference number and booking reference number
            facade.RemoveBooking(temp.CustomerRef, bookingRefNum);
            // Display a message box to let the user know the booking has been removed
            MessageBoxButton btnMessageBox = MessageBoxButton.OK;
            MessageBoxImage icnMessageBox = MessageBoxImage.Information;
            string caption = "Booking Removed";
            MessageBox.Show("Booking " + bookingRefNum + " was successfuly removed.", caption, btnMessageBox, icnMessageBox);
            // Update the booking list
            UpdateBookingList();
        }

        /// <summary>
        /// Remove booking button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveBooking_Click(object sender, RoutedEventArgs e)
        {
            // If a booking is selected
            if (lstBookings.SelectedIndex != -1)
            {
                // Convert the booking reference number string to an integer
                int bookingRefNum = Convert.ToInt32(lstBookings.SelectedItem.ToString());
                // Ask the user if they want to remove the current booking
                string sMessageBoxText = "Are you sure you want to delete the current booking (" + bookingRefNum + ")?";
                string sCaption = "Holiday Management";
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Question;
                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                // If the user clicks yes
                if (rsltMessageBox == MessageBoxResult.Yes)
                {
                    // Run the remove selected booking method, passing it the booking reference number
                    removeSelectedBooking(bookingRefNum);
                    // Reset the booking window
                    resetBookingWindow();
                    // Clear the invoice window
                    clearInvoice();
                }
            }
            else
            {
                MessageBox.Show("Cannot delete booking: No booking selected");
            }            
        }

        /// <summary>
        /// Finance booking list box selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstFinanceBookings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If a booking is selected
            if (lstFinanceBookings.SelectedIndex != -1)
            {
                // Convert the booking reference number string to an integer
                int bookingRefNum = Convert.ToInt32(lstFinanceBookings.SelectedItem.ToString());
                // Display the selected booking invoice
                displaySelectedBookingInvoice(bookingRefNum);
            }
        }

        /// <summary>
        /// Alter the visibility of the evening and breakfast meals on the invoice
        /// </summary>
        /// <param name="breakfast"></param>
        /// <param name="evening"></param>
        public void hideInvoiceMeals(bool breakfast, bool evening)
        {
            // If breakfast is false
            if (!breakfast)
            {
                // Set visiblity of each breakfast item to hidden
                lblF_MealBreakfast.Visibility = Visibility.Hidden;
                lblF_MealBreakfastPrice.Visibility = Visibility.Hidden;
                lblF_MealBreakfastQuantity.Visibility = Visibility.Hidden;
                lblF_MealBreakfastTotal.Visibility = Visibility.Hidden;
            }
            else
            {
                // Set visiblity of each breakfast item to visible
                lblF_MealBreakfast.Visibility = Visibility.Visible;
                lblF_MealBreakfastPrice.Visibility = Visibility.Visible;
                lblF_MealBreakfastQuantity.Visibility = Visibility.Visible;
                lblF_MealBreakfastTotal.Visibility = Visibility.Visible;
            }

            // If evening is false
            if (!evening)
            {
                // Set visiblity of each evening item to hidden
                lblF_MealEvening.Visibility = Visibility.Hidden;
                lblF_MealEveningPrice.Visibility = Visibility.Hidden;
                lblF_MealEveningQuantity.Visibility = Visibility.Hidden;
                lblF_MealEveningTotal.Visibility = Visibility.Hidden;
            }
            else
            {
                // Set visiblity of each evening item to visible
                lblF_MealEvening.Visibility = Visibility.Visible;
                lblF_MealEveningPrice.Visibility = Visibility.Visible;
                lblF_MealEveningQuantity.Visibility = Visibility.Visible;
                lblF_MealEveningTotal.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Alter the visibility of the car hire on the invoice
        /// </summary>
        /// <param name="hired"></param>
        public void hideInvoiceCarHire(bool hired)
        {
            // If hired is false
            if (!hired)
            {
                // Set the visibility of each car hire item to hidden
                lblF_CarHire.Visibility = Visibility.Hidden;
                lblF_CarHirePrice.Visibility = Visibility.Hidden;
                lblF_CarHireQuantity.Visibility = Visibility.Hidden;
                lblF_CarHireTotal.Visibility = Visibility.Hidden;
                lblF_CarHireLength.Visibility = Visibility.Hidden;
                lblF_CarHireLengthTotal.Visibility = Visibility.Hidden;
                
                lblF_Title_AdditionalCost.Visibility = Visibility.Hidden;
                lblF_Additional_SubTotal.Visibility = Visibility.Hidden;
                lblF_Additional_SubtotalTotal.Visibility = Visibility.Hidden;
            }
            else
            {
                // Set the visibility of each car hire item to visible
                lblF_CarHire.Visibility = Visibility.Visible;
                lblF_CarHirePrice.Visibility = Visibility.Visible;
                lblF_CarHireQuantity.Visibility = Visibility.Visible;
                lblF_CarHireTotal.Visibility = Visibility.Visible;
                lblF_CarHireLength.Visibility = Visibility.Visible;
                lblF_CarHireLengthTotal.Visibility = Visibility.Visible;

                lblF_Title_AdditionalCost.Visibility = Visibility.Visible;
                lblF_Additional_SubTotal.Visibility = Visibility.Visible;
                lblF_Additional_SubtotalTotal.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Display the selected booking invoice
        /// </summary>
        /// <param name="bookingRefNum"></param>
        public void displaySelectedBookingInvoice(int bookingRefNum)
        {
            // Find the booking using the facade with the booking reference number
            Booking booking = facade.FindBooking(bookingRefNum);
            // Alter the visibility of the meals and car hire based on the booking options
            hideInvoiceMeals(booking.BookingChalet.MealBreakFast, booking.BookingChalet.MealEvening);
            hideInvoiceCarHire(booking.BookingCarHire.Hired);
            // Set Chalet fields
            int chaletQuantity = 1;
            int chaletFlatRate = booking.BookingChalet.FlatRate;
            lblFbookingRefValue.Content = booking.BookingRef.ToString();
            lblF_Chalet.Content = "Chalet (" + booking.BookingChalet.ChaletID + ")";
            lblF_ChaletPrice.Content = "£" + chaletFlatRate.ToString();
            lblF_ChaletQuantity.Content = chaletQuantity.ToString();
            lblF_ChaletTotal.Content = "£" + (chaletFlatRate * chaletQuantity).ToString();

            // Set Guest fields
            int guestPrice = 25;
            int guestCount = booking.BookingGuests.Count;
            lblF_GuestPrice.Content = "£" + guestPrice.ToString();
            lblF_GuestQuantity.Content = guestCount.ToString();
            lblF_GuestTotal.Content = "£" + (guestPrice * guestCount).ToString();

            // Set Meal (Breakfast) fields
            int mealBreakfastPrice = 5;
            lblF_MealBreakfastPrice.Content = "£" + mealBreakfastPrice.ToString();
            lblF_MealBreakfastQuantity.Content = guestCount.ToString();
            lblF_MealBreakfastTotal.Content = "£" + (mealBreakfastPrice * guestCount).ToString();

            // Set Meal (Evening) fields
            int mealEveningPrice = 10;
            lblF_MealEveningPrice.Content = "£" + mealEveningPrice.ToString();
            lblF_MealEveningQuantity.Content = guestCount.ToString();
            lblF_MealEveningTotal.Content = "£" + (mealEveningPrice * guestCount).ToString();

            // Set the holiday term length
            int tripLength = booking.BookingInvoice.TripLength;
            lblF_TripLengthTotal.Content = tripLength.ToString() + " Days";

            // Set Standard Cost Sub Total
            int standardCostTotal = booking.BookingInvoice.CostPerNight * booking.BookingInvoice.TripLength;
            lblF_Standard_SubtotalTotal_Night.Content = "£" + booking.BookingInvoice.CostPerNight + " / Day";
            lblF_Standard_SubtotalTotal.Content = "£" + standardCostTotal.ToString();
            

            // Set Additional costs
            int carHirePrice = 50;
            lblF_CarHirePrice.Content = "£" + carHirePrice.ToString();
            lblF_CarHireQuantity.Content = "1";
            lblF_CarHireTotal.Content = "£" + carHirePrice.ToString();

            lblF_CarHireLengthTotal.Content = booking.BookingInvoice.CarHireLength.ToString() + " Days";

            // Set Additional Sub Total
            lblF_Additional_SubtotalTotal.Content = "£" + booking.BookingInvoice.CarHireTotalCost.ToString();

            lblF_TotalCostValue.Content = "£" + booking.BookingInvoice.TotalCost.ToString();

        }

        /// <summary>
        /// Display invoice button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisplayInvoice_Click(object sender, RoutedEventArgs e)
        {
            // If a booking is selected
            if (lstBookings.SelectedIndex != -1)
            {
                // Convert the booking reference number string to an integer
                int bookingRefNum = Convert.ToInt32(lstBookings.SelectedItem.ToString());
                // Display the selected booking invoice using the booking reference number
                displaySelectedBookingInvoice(bookingRefNum);
                // Switch tabs to the invoice tab
                tabCntrlTabs.SelectedItem = tabFinance;
            }
            else
            {
                MessageBox.Show("Cannot display invoice, no booking selected");
            }
        }

        /// <summary>
        /// Clear the invoice fields
        /// </summary>
        public void clearInvoice()
        {
            lstFinanceBookings.SelectedIndex = -1;
            lblFbookingRefValue.Content = "";
            lblF_Chalet.Content = "";
            lblF_ChaletPrice.Content = "";
            lblF_ChaletQuantity.Content = "";
            lblF_ChaletTotal.Content = "";

            // Set Guest fields
            lblF_GuestPrice.Content = "";
            lblF_GuestQuantity.Content = "";
            lblF_GuestTotal.Content = "";

            // Set Meal (Breakfast) fields
            lblF_MealBreakfastPrice.Content = "";
            lblF_MealBreakfastQuantity.Content = "";
            lblF_MealBreakfastTotal.Content = "";

            // Set Meal (Evening) fields
            lblF_MealEveningPrice.Content = "";
            lblF_MealEveningQuantity.Content = "";
            lblF_MealEveningTotal.Content = "";

            // Set the holiday term length
            lblF_TripLengthTotal.Content = "";

            // Set Standard Cost Sub Total
            lblF_Standard_SubtotalTotal.Content = "";
            lblF_Standard_SubtotalTotal_Night.Content = "";


            // Set Additional costs
            lblF_CarHirePrice.Content = "";
            lblF_CarHireQuantity.Content = "";
            lblF_CarHireTotal.Content = "";

            lblF_CarHireLengthTotal.Content = "";

            // Set Additional Sub Total
            lblF_Additional_SubtotalTotal.Content = "";

            lblF_TotalCostValue.Content = "";
        }

        /// <summary>
        /// Set the customer reference combo box value
        /// </summary>
        /// <param name="value"></param>
        public void SetCustRefComboBoxValue(int value)
        {
            int index = 0;
            // Loop through each item in the customer reference number combo box
            foreach (var item in cmbRefNumber.Items)
            {
                // If the current item is the same as the ref number passed in
                if (item.ToString() == value.ToString())
                {
                    // Set the selected index of the combo box to the current item
                    cmbRefNumber.SelectedIndex = index;
                    break;
                }
                index++;
            }
        }

        /// <summary>
        /// Set the chalet id combo box value
        /// </summary>
        /// <param name="value"></param>
        public void SetChaletComboBoxValue(int value)
        {
            int index = 0;
            // Loop through each item in the chalet id combo box
            foreach (var item in cmbChalet.Items)
            {
                // If the current item is the same as the chalet id passed in
                if (item.ToString() == value.ToString())
                {
                    // Set the selected index of the combo box to the current item
                    cmbChalet.SelectedIndex = index;
                    break;
                }
                index++;
            }
        }

        /// <summary>
        /// Display the entire booking in the main booking tab
        /// </summary>
        /// <param name="bookingRefNum"></param>
        public void DisplayEntireBooking(int bookingRefNum)
        {
            // Clear the booking window inputs
            resetBookingWindow();
            // Find the current booking using the facade with the booking reference number
            Booking currentBooking = facade.FindBooking(bookingRefNum);
            // Find the current customer using the facade with the booking customer reference number
            Customer currentCustomer = facade.GetCustomer(currentBooking.CustomerRef);
            // Set the customer reference combo box value to the reference number of the customer
            SetCustRefComboBoxValue(currentCustomer.RefNumber);
            // Set the customer name and address field to the current customer name and address
            txtBCustName.Text = currentCustomer.Name;
            txtBCustAddress.Text = currentCustomer.Address;
            // Set the guest list to the list of guests in the booking
            guests = new List<Guest>(currentBooking.BookingGuests);
            // Update the guest list
            UpdateGuestList();
            // Set the arrival and departure date pickers to the current booking arrival and departure dates
            dpArrival.SelectedDate = currentBooking.Arrival;
            dpDeparture.SelectedDate = currentBooking.Departure;
            // Set the chalet combo box value to the current booking chalet ID
            SetChaletComboBoxValue(currentBooking.BookingChalet.ChaletID);

            // If the current booking meal breakfast is true
            if (currentBooking.BookingChalet.MealBreakFast == true)
            {
                // Tick the meal breakfast check box
                chkMealBreakfast.IsChecked = true;
            }
            else
            {
                // Untick the meal breakfast check box
                chkMealBreakfast.IsChecked = false;
            }
            // If the current booking meal evening is true
            if (currentBooking.BookingChalet.MealEvening == true)
            {
                // Tick the meal evening check box
                chkMealEvening.IsChecked = true;
            }
            else
            {
                // Untick the meal evening check box
                chkMealEvening.IsChecked = false;
            }
            // If the current booking car hire status is true
            if (currentBooking.BookingCarHire.Hired == true)
            {
                // Tick the car hire check box
                chkCarHire.IsChecked = true;
                // Set the car hire driver, start date and end date to the current booking car hire details
                txtCarHireDriver.Text = currentBooking.BookingCarHire.DriverName;
                dpCarHireStart.SelectedDate = currentBooking.BookingCarHire.DateStart;
                dpCarHireEnd.SelectedDate = currentBooking.BookingCarHire.DateEnd;
            }
            else
            {
                // Untick the car hire check box
                chkCarHire.IsChecked = false;
            }
        }

        /// <summary>
        /// Booking list selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstBookings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If a booking is selected
            if (lstBookings.SelectedIndex != -1)
            {
                // Get the selected booking reference string
                string bookingRefString = lstBookings.SelectedItem.ToString();
                // Convert the selected booking reference string to an integer
                int bookingRefInt = Convert.ToInt32(bookingRefString);
                // Display the entire booking of the selected booking reference number
                DisplayEntireBooking(bookingRefInt);
            }
        }

        /// <summary>
        /// Clear booking details button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearBooking_Click(object sender, RoutedEventArgs e)
        {
            // Confirm with the user that they want to reset the entire form
            string sMessageBoxText = "Are you sure you want to reset the entire form?";
            string sCaption = "Holiday Management";
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Question;
            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            // If the user selects yes
            if (rsltMessageBox == MessageBoxResult.Yes)
            {
                // Reset the entire booking window
                resetBookingWindow();
            }
        }
    }
}