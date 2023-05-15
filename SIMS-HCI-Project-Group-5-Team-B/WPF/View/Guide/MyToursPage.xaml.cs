﻿using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for MyToursPage.xaml
    /// </summary>
    public partial class MyToursPage : Page
    {
        public MyToursViewModel myToursViewModel { get; set; }
        public MyToursPage()
        {
            InitializeComponent();
            this.myToursViewModel = new MyToursViewModel(8);
            this.DataContext = myToursViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Report!");
        }
    }
}
