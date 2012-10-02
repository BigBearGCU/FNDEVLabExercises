using Excercise_1___Student_Registration.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Excercise_1___Student_Registration
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : Excercise_1___Student_Registration.Common.LayoutAwarePage
    {
        //Instance of Student
        private Student registeredStudent;
        // This is the container that will hold our custom content.
        private Popup settingsPopup;

        // Desired width for the settings UI. UI guidelines specify this should be 346 or 646 depending on your needs.
        private double settingsWidth = 646;


        // Used to determine the correct height to ensure our custom UI fills the screen.
        private Rect windowBounds;

        public MainPage()
        {
            this.InitializeComponent();
            windowBounds = Window.Current.Bounds;
            registeredStudent = null;
            resetUIToDefault();
            SettingsPane.GetForCurrentView().CommandsRequested += MainPage_CommandsRequested;
        }

        void onSettingsCommand(IUICommand command)
        {
            SettingsCommand settingsCommand = (SettingsCommand)command;
            if ((string)settingsCommand.Id == "defaultPage")
            {
                
                // Create a Popup window which will contain our flyout.
                settingsPopup = new Popup();
                settingsPopup.Closed += OnPopupClosed;
                Window.Current.Activated += OnWindowActivated;
                settingsPopup.IsLightDismissEnabled = true;
                settingsPopup.Width = settingsWidth;
                settingsPopup.Height = windowBounds.Height;

                // Add the proper animation for the panel.
                settingsPopup.ChildTransitions = new TransitionCollection();
                settingsPopup.ChildTransitions.Add(new PaneThemeTransition()
                {
                    Edge = (SettingsPane.Edge == SettingsEdgeLocation.Right) ?
                           EdgeTransitionLocation.Right :
                           EdgeTransitionLocation.Left
                });

                // Create a SettingsFlyout the same dimenssions as the Popup.
                SettingsFlyout mypane = new SettingsFlyout();
                mypane.Width = settingsWidth;
                mypane.Height = windowBounds.Height;

                // Place the SettingsFlyout inside our Popup window.
                settingsPopup.Child = mypane;

                // Let's define the location of our Popup.
                settingsPopup.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (windowBounds.Width - settingsWidth) : 0);
                settingsPopup.SetValue(Canvas.TopProperty, 0);
                settingsPopup.IsOpen = true;
            }
           
        }

        void MainPage_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            UICommandInvokedHandler handler = new UICommandInvokedHandler(onSettingsCommand);

            SettingsCommand generalCommand = new SettingsCommand("generalSettings", "General", handler);
            args.Request.ApplicationCommands.Add(generalCommand);

            SettingsCommand helpCommand = new SettingsCommand("helpPage", "Help", handler);
            args.Request.ApplicationCommands.Add(helpCommand);

            SettingsCommand defaultCommand = new SettingsCommand("defaultPage", "Default", handler);
            args.Request.ApplicationCommands.Add(defaultCommand);


        }

        private void OnWindowActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                settingsPopup.IsOpen = false;
            }
        }

        private void OnPopupClosed(object sender, object e)
        {
            Window.Current.Activated -= OnWindowActivated;
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

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            //Navigate to the next page
            this.Frame.Navigate(typeof(StudentDetailsPage));
        }

        private void regsiterBtn_Click(object sender, RoutedEventArgs e)
        {
            //Create new Address
            Address address = new Address();
            //We are parsing the contents of the text box to an int, we should probably use TryParse instead
            address.HouseNumber = int.Parse(houseNoTxtBox.Text);
            address.StreetName=streetTxtBox.Text;
            address.City=cityTxtBox.Text;
            address.PostCode=postcodeTxtBox.Text;
            address.Country=countryTxtBox.Text;

            //Create new Contact
            Contact contact = new Contact();
            contact.HomePhoneNumber = homeNumberTxtBox.Text;
            contact.MobilePhoneNumber = mobileNumberTxtBox.Text;
            contact.EmailAddress = emailTxtBox.Text;

            //Create Student
            registeredStudent = new Student();
            registeredStudent.Surname =surnameTxtBox.Text;
            registeredStudent.Firstname = firstNameTxtBox.Text;
            //We are parsing the contents of the text box to a DateTime value, we should use TryParse instead
            registeredStudent.DateOfBirth = DateTime.Parse(dateTxtBox.Text);
            registeredStudent.TermAddress = address;
            registeredStudent.CourseStudying =courseTxtBox.Text;
            //Casting the selected index of the combobox, this should map to the enum. We would probably be better
            //binding the actual YearOfStudy type
            registeredStudent.Year = (YearOfStudy)yearComboBox.SelectedIndex;
            registeredStudent.ContactDetails = contact;

            (App.Current as App).RegisteredStudents.Add(registeredStudent); 

            resetUIToDefault();

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
    }
}
