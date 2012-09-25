using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Excercise_1___Student_Registration
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class StudentDetailsPage : Excercise_1___Student_Registration.Common.LayoutAwarePage
    {
        private int currentSelectedIndex = 0;
        public StudentDetailsPage()
        {
            this.InitializeComponent();
            //This function will set every UI element default
            resetUIToDefault();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if ((App.Current as App).RegisteredStudents.Count > 0)
            {
                currentSelectedIndex = 0;
                updateUIWithRegisteredStudent((App.Current as App).RegisteredStudents[currentSelectedIndex]);
            }
            else
            {
                resetUIToDefault();
            }
            base.OnNavigatedTo(e);
        }
        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            currentSelectedIndex--;
            if (currentSelectedIndex < 0)
                currentSelectedIndex = 0;
            updateUIWithRegisteredStudent((App.Current as App).RegisteredStudents[currentSelectedIndex]);
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Student s = (App.Current as App).RegisteredStudents[currentSelectedIndex];
            Address address = s.TermAddress;
            Contact contact = s.ContactDetails;
            //We are parsing the contents of the text box to an int, we should probably use TryParse instead
            address.HouseNumber = int.Parse(houseNoTxtBox.Text);
            address.StreetName = streetTxtBox.Text;
            address.City = cityTxtBox.Text;
            address.PostCode = postcodeTxtBox.Text;
            address.Country = countryTxtBox.Text;

            contact.HomePhoneNumber = homeNumberTxtBox.Text;
            contact.MobilePhoneNumber = mobileNumberTxtBox.Text;
            contact.EmailAddress = emailTxtBox.Text;

            //Create Student
            s.ContactDetails = contact;
            s.TermAddress = address;
            s.Surname = surnameTxtBox.Text;
            s.Firstname = firstNameTxtBox.Text;
            //We are parsing the contents of the text box to a DateTime value, we should use TryParse instead
            s.DateOfBirth = DateTime.Parse(dateTxtBox.Text);
            s.CourseStudying = courseTxtBox.Text;
            //Casting the selected index of the combobox, this should map to the enum. We would probably be better
            //binding the actual YearOfStudy type
            s.Year = (YearOfStudy)yearComboBox.SelectedIndex;
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            (App.Current as App).RegisteredStudents.RemoveAt(currentSelectedIndex);
            if ((App.Current as App).RegisteredStudents.Count > 0)
            {
                currentSelectedIndex = 0;
                updateUIWithRegisteredStudent((App.Current as App).RegisteredStudents[currentSelectedIndex]);
            }
            else
            {
                //clear ui
                resetUIToDefault();
            }
        }

        private void forwardBtn_Click(object sender, RoutedEventArgs e)
        {
            currentSelectedIndex++;
            if (currentSelectedIndex > (App.Current as App).RegisteredStudents.Count - 1)
                currentSelectedIndex = (App.Current as App).RegisteredStudents.Count - 1;
            updateUIWithRegisteredStudent((App.Current as App).RegisteredStudents[currentSelectedIndex]);
        }

        private void updateUIWithRegisteredStudent(Student student)
        {
            streetTxtBox.Text = student.TermAddress.StreetName;
            houseNoTxtBox.Text = student.TermAddress.HouseNumber.ToString();
            cityTxtBox.Text = student.TermAddress.City;
            postcodeTxtBox.Text = student.TermAddress.PostCode;
            countryTxtBox.Text = student.TermAddress.Country;

            homeNumberTxtBox.Text = student.ContactDetails.HomePhoneNumber;
            mobileNumberTxtBox.Text = student.ContactDetails.MobilePhoneNumber;
            emailTxtBox.Text = student.ContactDetails.EmailAddress;

            surnameTxtBox.Text =student.Surname;
            firstNameTxtBox.Text = student.Firstname;
            dateTxtBox.Text = student.DateOfBirth.ToString();
            courseTxtBox.Text = student.CourseStudying;
            yearComboBox.SelectedIndex = (int)student.Year;
        }

        private void resetUIToDefault()
        {
            streetTxtBox.Text = "";
            houseNoTxtBox.Text = "";
            cityTxtBox.Text = "";
            postcodeTxtBox.Text = "";
            countryTxtBox.Text = "";

            homeNumberTxtBox.Text = "";
            mobileNumberTxtBox.Text = "";
            emailTxtBox.Text = "";

            surnameTxtBox.Text = "";
            firstNameTxtBox.Text = "";
            dateTxtBox.Text = "";
            courseTxtBox.Text = "";
            yearComboBox.SelectedIndex = -1;
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchPage));
        }
    }
}
