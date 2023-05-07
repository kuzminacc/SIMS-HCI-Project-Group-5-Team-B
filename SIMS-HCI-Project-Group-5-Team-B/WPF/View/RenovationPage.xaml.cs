﻿using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for RenovationPage.xaml
    /// </summary>
    public partial class RenovationPage : Page
    {
        private RenovationViewModel renovationViewModel;
        private RenovationService renovationService;
        private ReservationService reservationService;
        private AccommodationService accommodationService;
        private Owner owner;
        public RenovationGridView SelectedRenovationGridView { get; set; }
        public RenovationPage(RenovationService renovationService, ReservationService reservationService, Owner owner, AccommodationService accommodationService)
        {
            InitializeComponent();
            this.renovationService = renovationService;
            this.reservationService = reservationService;
            this.accommodationService = accommodationService;
            this.owner = owner;
            renovationViewModel = new RenovationViewModel(renovationService, reservationService, owner.Id, SelectedRenovationGridView);
            DataContext = renovationViewModel;
        }

        private void Schedule_Button_Click(object sender, RoutedEventArgs e)
        {
            ScheduleRenovationForm scheduleRenovationForm = new ScheduleRenovationForm(renovationService, accommodationService, reservationService, owner, renovationViewModel.FutureRenovations);
            scheduleRenovationForm.Show();
        }

        private void CallOf_Button_Click(object sender, RoutedEventArgs e)
        {
            CallOffRenovationWindow callOffRenovationWindow = new CallOffRenovationWindow(renovationViewModel.SelectedRenovationGridView, renovationViewModel.FutureRenovations, renovationService, reservationService, owner.Id);
            callOffRenovationWindow.Show();
        }
    }
}
