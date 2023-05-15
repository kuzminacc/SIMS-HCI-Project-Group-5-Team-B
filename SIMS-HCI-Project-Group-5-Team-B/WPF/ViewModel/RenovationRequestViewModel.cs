﻿using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class RenovationRequestViewModel:INotifyPropertyChanged
    {
        public RenovationRequest NewRenovationRequest { get; set; }
        private RenovationRequestService renovationRequestService;

        public RelayCommand SendCommand { get;}
        public RelayCommand CloseCommand { get;}
        protected RenovationRequestForm window;
        private string selectedItem;
        private int reservationId;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }


        public string SelectedItem {
            get { return selectedItem; }
            set { 
                if(selectedItem != value)
                {
                    selectedItem = value;
                    NotifyPropertyChanged(nameof(SelectedItem));
                    NewRenovationRequest.Level = GetReservationLevel(selectedItem);

                }
            }
        }


        public RenovationRequestViewModel(int reservationId, RenovationRequestForm window)
        {
            this.reservationId = reservationId;
            NewRenovationRequest = new RenovationRequest(reservationId);
            renovationRequestService = new RenovationRequestService();
            this.window = window;
           SelectedItem = "Level1 - Minor Renovations Needed";
            //commands
            SendCommand = new RelayCommand(Send_Execute,SendCanExecute);
            CloseCommand = new RelayCommand(Close_Execute);
        }

        public void Send_Execute()
        {
            if(NewRenovationRequest.IsValid)
            {
                renovationRequestService.Save(NewRenovationRequest);
                MessageBox.Show("Renovation request sent!");
                window.Close();
            }
            else
            {
                MessageBox.Show("Renovation request can not be sent brcause data is not valid");
            }
        }

        public bool SendCanExecute()
        {
            return !renovationRequestService.Exists(reservationId);
        }

        public void Close_Execute()
        {
            window.Close();
        }

        private ReservationLevel GetReservationLevel(string selectedItem)
        {
            if (selectedItem.Contains("Level1"))
            {
                return ReservationLevel.Level1;
            }
            if (selectedItem.Contains("Level2"))
            {
                return ReservationLevel.Level2;
            }
            if (selectedItem.Contains("Level3"))
            {
                return ReservationLevel.Level3;
            }
            if (selectedItem.Contains("Level4"))
            {
                return ReservationLevel.Level4;
            }
            return ReservationLevel.Level5;
        }
    }
}
