﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class SearchPage : Excercise_1___Student_Registration.Common.LayoutAwarePage
    {
        public SearchPage()
        {
            this.InitializeComponent();
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

        private void resultsSelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            carryOutSearch((string)searchTypeComboBox.SelectedValue, searchTermTxtBox.Text);
        }

        async void carryOutSearch(string searchType,string searchTxt)
        {
            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            if (searchType.ToLower() == "firstname")
            {
                var results = from s in (App.Current as App).RegisteredStudents
                              where s.Firstname.ToLower() == searchTxt.ToLower()
                              select s;
                resultsSelectionBox.ItemsSource = results.ToList();

                Search search = new Search();
                search.SearchTerm = searchTxt.ToLower();
                search.TypeOfSearch = Search.SearchType.firstname;

                XmlSerializer serializer = new XmlSerializer(typeof(Search));

                StorageFile searchSaveFile=await storageFolder.CreateFileAsync("save.xml");

                using (Stream stream = await searchSaveFile.OpenStreamForWriteAsync())
                {
                    serializer.Serialize(stream, search);
                }



            }
            else if (searchType.ToLower()=="surname")
            {

            }
            else if (searchType.ToLower() == "course")
            {

            }
        }
    }
}
