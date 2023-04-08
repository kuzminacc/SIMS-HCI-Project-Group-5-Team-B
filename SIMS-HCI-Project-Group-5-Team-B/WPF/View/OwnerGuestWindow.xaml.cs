﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.View;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for OwnerGuestWindow.xaml
    /// </summary>
    public partial class OwnerGuestWindow : Window
    {
        private AccommodationService accommodationController;
        private LocationController locationController;
        private ReservationService reservationController;
        private OwnerService ownerController;
        private OwnerAccommodationGradeSevice ownerAccommodationGradeController;
        private SuperOwnerService superOwnerController;
        private OwnerGuestService ownerGuestService;
        private OwnerGuest activeOwnerGuest;

        private OwnerGuestCSVRepository ownerGuestRepository;
        public OwnerGuestWindow(string username)
        {
            InitializeComponent();
            ownerGuestRepository = new OwnerGuestCSVRepository();
            locationController = new LocationController();
            ownerController = new OwnerService();
            accommodationController = new AccommodationService(locationController, ownerController);
            reservationController = new ReservationService(accommodationController, ownerGuestRepository);
            ownerAccommodationGradeController = new OwnerAccommodationGradeSevice(reservationController);
            superOwnerController = new SuperOwnerService(reservationController, ownerAccommodationGradeController, ownerController, accommodationController);
            ownerGuestService = new OwnerGuestService();
            activeOwnerGuest =  ownerGuestService.GetByUsername(username);
        }

        private void ShowAccomodation_Button_Click(object sender, RoutedEventArgs e)
        {

            AccommodationsWindow accomodationsWindow = new AccommodationsWindow(activeOwnerGuest.Id, locationController, ownerController, accommodationController, reservationController);
            accomodationsWindow.Show();
        }

        private void Reservations_Button_Click(object sender, RoutedEventArgs e)
        {
            ReservationsWindow reservationsWindow = new ReservationsWindow(reservationController,ownerAccommodationGradeController,superOwnerController,ownerController, activeOwnerGuest.Id);
            reservationsWindow.Show();
        }
    }
}
