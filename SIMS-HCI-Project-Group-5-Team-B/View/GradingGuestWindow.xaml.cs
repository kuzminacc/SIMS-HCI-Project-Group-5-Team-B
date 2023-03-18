﻿using SIMS_HCI_Project_Group_5_Team_B.Model;
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
using System.Windows.Shapes;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using System.Collections.ObjectModel;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for GradingGuestWindow.xaml
    /// </summary>
    public partial class GradingGuestWindow : Window
    {
        public ReservationController reservationController;
        public OwnerGuestGradeContoller ownerGuestGradeContoller;
        public OwnerGuestGrade NewOwnerGuestGrade { get; set; }
        public Reservation SelectedReservation { get; set; }

        public ObservableCollection<Reservation> ReservationsForGrading { get; set; }


        public GradingGuestWindow(OwnerGuestGradeContoller ownerGuestGradeContoller, ReservationController reservationController,Reservation SelectedReservation, ObservableCollection<Reservation> ReservationsForGrading)
        {
            InitializeComponent();
            this.DataContext = this;
            this.ownerGuestGradeContoller = ownerGuestGradeContoller;
            this.reservationController = reservationController;
            this.SelectedReservation = SelectedReservation;
            this.ReservationsForGrading = ReservationsForGrading;
            SetOwnerGuestGradeParameters();

        }

        private void SetOwnerGuestGradeParameters()
        {
            NewOwnerGuestGrade = new OwnerGuestGrade();
            NewOwnerGuestGrade.ReservationId = SelectedReservation.Id;
            NewOwnerGuestGrade.reservation = SelectedReservation;
            
        }

        private void Grade_Button_Click(object sender, RoutedEventArgs e)
        {
            ownerGuestGradeContoller.Save(NewOwnerGuestGrade);
            SelectedReservation.IsGraded = true;
            reservationController.Update(SelectedReservation);
            ReservationsForGrading.Remove(SelectedReservation);
            Close();

        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PaymentOnTimeComboBox(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            ComboBoxItem cbItem = (ComboBoxItem)cBox.SelectedItem;
            string IsPaymentOnTime = (string)cbItem.Content;
            if (IsPaymentOnTime == "Yes")
            {
                NewOwnerGuestGrade.IsPaymentCompletedOnTime = true;
            }
            else if (IsPaymentOnTime == "No")
            {
                NewOwnerGuestGrade.IsPaymentCompletedOnTime = false;
            }
            
        }

        private void ComplaintsFromOtherGuestsComboBox(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            ComboBoxItem cbItem = (ComboBoxItem)cBox.SelectedItem;
            string ComplatintsFromGuests= (string)cbItem.Content;
            if (ComplatintsFromGuests == "Yes")
            {
                NewOwnerGuestGrade.ComplaintsFromOtherGuests = true;
            }
            else if (ComplatintsFromGuests == "No")
            {
                NewOwnerGuestGrade.ComplaintsFromOtherGuests = false;
            }
        }
    }
}