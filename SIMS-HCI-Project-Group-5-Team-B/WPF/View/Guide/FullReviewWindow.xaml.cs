﻿using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.Guide;
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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for FullReviewWindow.xaml
    /// </summary>
    public partial class FullReviewWindow : Window
    {
        public FullReviewViewModel FullReviewViewModel { get; set; }
        public FullReviewWindow(Card card)
        {
            FullReviewViewModel = new FullReviewViewModel(card);
            InitializeComponent();
            this.DataContext = FullReviewViewModel;
            
        }
    }
}
