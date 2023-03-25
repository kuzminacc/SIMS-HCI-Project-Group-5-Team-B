﻿using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Model;
using System;
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

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for ReservationsWindow.xaml
    /// </summary>
    /// 
    
    public partial class ReservationsWindow : Window
    {
        private ReservationController reservationController;
        private OwnerAccommodationGradeController ownerAccommodationGradeController;
        public ObservableCollection<ReservationView> ReservationViews { get; set; }
        public ReservationView SelectedReservationView { get; set; }
        public ReservationsWindow(ReservationController reservationController, OwnerAccommodationGradeController ownerAccommodationGradeController)
        {
            InitializeComponent();
            this.DataContext = this;

            this.reservationController = reservationController;
            this.ownerAccommodationGradeController = ownerAccommodationGradeController;

            //add method for checking the userId when showing reservations
            ReservationViews = new ObservableCollection<ReservationView>(reservationController.GetReservationsForGuestGrading());
            
            
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grade_Button_Click(object sender, RoutedEventArgs e)
        {

            if(SelectedReservationView != null)
            {
                GradingOwnerAccommodation gradingOwnerAccommodatoinWindow = new GradingOwnerAccommodation(ownerAccommodationGradeController, reservationController, SelectedReservationView, ReservationViews);
                gradingOwnerAccommodatoinWindow.Show();
                
            }

        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                Grade_Button_Click(sender, e);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.G))
                Grade_Button_Click(sender,e);
        }

        
    }
}