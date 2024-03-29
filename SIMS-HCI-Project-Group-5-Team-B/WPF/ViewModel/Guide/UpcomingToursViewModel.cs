﻿using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class UpcomingToursViewModel : ViewModel
    {
        #region fields
        private AppointmentService appointmentService;
        private VoucherService voucherService;
        public TourAttendanceService tourAttendanceService;
        public ObservableCollection<Appointment> AvailableAppointments { get; }
        private Appointment selectedAppointment;
        public Appointment SelectedAppointment 
        {
            get { return selectedAppointment; } 
            set
            {
                if(selectedAppointment != value) 
                {
                    selectedAppointment = value;
                    OnPropertyChanged(nameof(SelectedAppointment));
                }
            }
        }
        private bool isOpenedPopup;
        public bool IsOpenedPopup
        {
            get
            {
                return isOpenedPopup;
            }
            set
            {
                if (isOpenedPopup != value)
                {
                    isOpenedPopup = value;
                    OnPropertyChanged(nameof(IsOpenedPopup));
                }
            }
        }
        public RelayCommandWithParams CancelTourCommand { get; set; }
        public RelayCommandWithParams OpenPopupCommand { get; set; }

        public int userId;
        #endregion

        #region actions
        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        private void Execute_OpenPopupCommand(object obj)
        {
            IsOpenedPopup = !IsOpenedPopup;
        }
        private void Execute_CancelTourCommand(object obj)
        {
            bool result = MessageBox.Show("Are you sure you want to cancel selected tour?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (result && SelectedAppointment.Cancelled == false)
            {
                SelectedAppointment.Cancelled = true;
                appointmentService.Update(SelectedAppointment);
                voucherService.SendVouchers(1,SelectedAppointment.Id);
                RefreshAppointments();
            }
            else if(SelectedAppointment.Cancelled == true)
            {
                MessageBox.Show("Already cancelled!");
            }
        }
        #endregion

        public UpcomingToursViewModel()
        {
            //this.userId = userId;
            KeyPointCSVRepository keyPointCSVRepository = new KeyPointCSVRepository();
            LocationCSVRepository locationCSVRepository = new LocationCSVRepository();
            TourCSVRepository tourCSVRepository = new TourCSVRepository();
            AppointmentCSVRepository appointmentCSVRepository = new AppointmentCSVRepository();
            TourAttendanceCSVRepository tourAttendanceCSVRepository = new TourAttendanceCSVRepository();
            tourAttendanceService = new TourAttendanceService();
            this.appointmentService = new AppointmentService();
            voucherService = new VoucherService();

            SelectedAppointment = new Appointment();
            this.CancelTourCommand = new RelayCommandWithParams(Execute_CancelTourCommand, CanExecute_Command);
            this.OpenPopupCommand = new RelayCommandWithParams(Execute_OpenPopupCommand, CanExecute_Command);
            AvailableAppointments = new ObservableCollection<Appointment>();

            RefreshAppointments();
            IsOpenedPopup = false;
        }

        private void RefreshAppointments()
        {
            AvailableAppointments.Clear();
            foreach (Appointment appointment in appointmentService.GetUpcoming(8))
            {
                AvailableAppointments.Add(appointment);
            }
        }
    }
}
