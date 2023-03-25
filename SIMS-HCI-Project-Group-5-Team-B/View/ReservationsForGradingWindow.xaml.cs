﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Model;


namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for ReservationsForGradingWindow.xaml
    /// </summary>
    public partial class ReservationsForGradingWindow : Window
    {
        private OwnerGuestGradeContoller ownerGuestGradeContoller;
        private ReservationController reservationController;
        private AccommodationController accommodationController;
        private LocationController locationController;
        private OwnerController ownerController;
        private OwnerAccommodationGradeController ownerAccommodationGradeController;
        public ObservableCollection<Reservation> ReservationsForGrading { get; set; }
        public ObservableCollection<OwnerAccommodationGrade> OwnerAccommodationGradesForShowing { get; set; }
        public Reservation SelectedReservation { get; set; }
        public OwnerAccommodationGrade SelectedOwnerAccommodationGrade { get; set; }
        

        public ReservationsForGradingWindow()
        {
            InitializeComponent();
            DataContext = this;
            locationController = new LocationController();
            ownerController = new OwnerController();
            accommodationController = new AccommodationController(locationController, ownerController);
            reservationController = new ReservationController(accommodationController);
            ReservationsForGrading = new ObservableCollection<Reservation>(reservationController.GetSuiableReservationsForGrading());
            ownerGuestGradeContoller = new OwnerGuestGradeContoller(reservationController);
            ownerAccommodationGradeController = new OwnerAccommodationGradeController(reservationController);
            OwnerAccommodationGradesForShowing = new ObservableCollection<OwnerAccommodationGrade>(ownerAccommodationGradeController.GetOwnerAccommodationGradesForShowing());
            //OwnerAccommodationGradesForShowing = new ObservableCollection<OwnerAccommodationGrade>(ownerAccommodationGradeController.GetAll());

        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void Grade_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                GradingGuestWindow gradingGuestWindow = new GradingGuestWindow(ownerGuestGradeContoller,ownerAccommodationGradeController, reservationController,SelectedReservation, ReservationsForGrading, OwnerAccommodationGradesForShowing);
                gradingGuestWindow.Show();
            }
            else
            {
                MessageBox.Show("Item is not selected");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(SelectedOwnerAccommodationGrade != null)
            {
                OwnerAccommodationGradeDetailsWindow ownerAccommodationGradeDetailsWindow = new OwnerAccommodationGradeDetailsWindow(SelectedOwnerAccommodationGrade);
                ownerAccommodationGradeDetailsWindow.Show();
            }
        }
    }
}
