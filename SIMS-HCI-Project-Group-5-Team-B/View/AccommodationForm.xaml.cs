﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Model;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SIMS_HCI_Project_Group_5_Team_B.Controller;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for AccommodationForm.xaml
    /// </summary>
    public partial class AccommodationForm : Window,/*IDataErrorInfo, */INotifyPropertyChanged
    {

        private AccommodationController accommodationController;
        private LocationController locationController;

        public Accommodation accommodation { get; set; }
        public Location location { get; set; }
        public string locationString;

        public string LocationString
        {
            get { return locationString; }
            set
            {
                if (locationString != value)
                {
                    locationString = value;
                    OnPropertyChanged();

                }

            }
        }


        public AccommodationForm()
        {
            locationString = "";
            accommodation = new Accommodation();
            InitializeComponent();
            this.DataContext = this;
            locationController = new LocationController();
            accommodationController = new AccommodationController(locationController);
        }

        

        private void Create_Accommodation_Click(object sender, RoutedEventArgs e)
        {
            if (accommodation.IsValid && IsValid)
            {
                int count = 2;
                String delimeter = ",";
                string[] locationValues = locationString.Split(delimeter, count);
                location = new Location(locationValues[0], locationValues[1]);

                accommodationController.AddAccommodation(accommodation, location);
               
                Close();
            }
            else
            {
                MessageBox.Show("Accommodation can't be created, because fileds are not valid");
            }
        }

        private void TypeChanged_ComboBox(object sender,RoutedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            ComboBoxItem cbItem = (ComboBoxItem)cBox.SelectedItem;
            string newType = (string)cbItem.Content;
            if(newType == "Apartment")
            {
                accommodation.Type = "Apartment";
            }
            else if(newType == "House")
            {
                accommodation.Type = "House";
            }
            else if(newType == "Cottage")
            {
                accommodation.Type = "Cottage";
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Regex adresa_regex = new Regex("[A-Z].{0,20},[A-Z].{0,20}");
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Location")
                {
                    if (string.IsNullOrEmpty(LocationString))
                        return "Filed must be filled";

                    Match match = adresa_regex.Match(LocationString);
                    if (!match.Success)
                        return "Location needs to be in format: city, state";
                }
                return null;
            }

        }
        private readonly string[] _validatedProperties = { "Location" };
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
        
        


    }
}
