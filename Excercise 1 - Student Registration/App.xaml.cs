using Callisto.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Search;
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

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace Excercise_1___Student_Registration
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public List<Student> RegisteredStudents { get; set; }
        private StorageFile studentsFile=null;

        // This is the container that will hold our custom content.
        SettingsFlyout settings;

        // Desired width for the settings UI. UI guidelines specify this should be 346 or 646 depending on your needs.
        private double settingsWidth = 323;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            RegisteredStudents = new List<Student>();
            this.Suspending += OnSuspending;
            
        }

        void App_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            UICommandInvokedHandler handler = new UICommandInvokedHandler(onSettingsCommand);
            SettingsCommand generalCommand = new SettingsCommand("generalSettings", "General", handler);
            args.Request.ApplicationCommands.Add(generalCommand);

            SettingsCommand helpCommand = new SettingsCommand("helpPage", "Help", handler);
            args.Request.ApplicationCommands.Add(helpCommand);

            SettingsCommand defaultCommand = new SettingsCommand("defaultPage", "Default", handler);
            args.Request.ApplicationCommands.Add(defaultCommand);
        }

        void onSettingsCommand(IUICommand command)
        {
            SettingsCommand settingsCommand = (SettingsCommand)command;
            if ((string)settingsCommand.Id == "defaultPage")
            {
                settings = new SettingsFlyout();
                settings.FlyoutWidth = (Callisto.Controls.SettingsFlyout.SettingsFlyoutWidth)Enum.Parse(typeof(Callisto.Controls.SettingsFlyout.SettingsFlyoutWidth), settingsWidth.ToString());
                settings.HeaderText = "Settings";

                StackPanel sp = new StackPanel();
                sp.Width = settingsWidth;

                TextBlock t = new TextBlock();
                t.Text = "User name";

                TextBox box = new TextBox();
                box.Name = "userNameTxtBox";

                Button b = new Button();
                b.Content = "Save";
                b.Click += b_Click;

                sp.Children.Add(t);
                sp.Children.Add(box);
                sp.Children.Add(b);
                settings.Content = sp;

                settings.IsOpen = true;

            }

        }

        void b_Click(object sender, RoutedEventArgs e)
        {
            //Grab the stack panel
            StackPanel sp = (StackPanel)settings.Content;
            //Get the text box
            TextBox box=(TextBox)sp.FindName("userNameTxtBox");
            //retrieve string from text box
            string s=box.Text;
        }

        private void OnWindowActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                settings.IsOpen = false;
            }
        }

        private void OnPopupClosed(object sender, object e)
        {
            Window.Current.Activated -= OnWindowActivated;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }


            LoadStudentDetails();

            // Ensure the current window is active
            Window.Current.Activate();
            //
            SettingsPane.GetForCurrentView().CommandsRequested += App_CommandsRequested;
        }

        async void LoadStudentDetails()
        {
            // Check to see if file exists
            //Get local storage folder
            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            //create a file query
            StorageFileQueryResult fileResults = storageFolder.CreateFileQuery();
            //Get all files in the directory
            IReadOnlyList<StorageFile> fileList = await fileResults.GetFilesAsync();
            //check to see if we have a file
            studentsFile=fileList.SingleOrDefault(file => file.Name == "students.txt");
            if (studentsFile == null)
            {
                //if not create
                studentsFile=await storageFolder.CreateFileAsync("students.txt");
            }
            else
            {
                //else load from a stream
                using (StreamReader reader=new StreamReader(await studentsFile.OpenStreamForReadAsync()))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        Student student = new Student();
                        student.parse(line);
                        RegisteredStudents.Add(student);
                    }
                }
            }
        }

        async void SaveStudentDetails()
        {
            using (StreamWriter writer = new StreamWriter(await studentsFile.OpenStreamForWriteAsync()))
            {
                foreach (Student student in RegisteredStudents)
                {
                    writer.WriteLine(student.ToString());
                }
            }
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            SaveStudentDetails();
            deferral.Complete();
        }
    }
}
