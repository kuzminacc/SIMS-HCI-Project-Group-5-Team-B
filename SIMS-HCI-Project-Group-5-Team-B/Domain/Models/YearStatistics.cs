﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class YearStatistics
    {
        public int Year { get; set; }
        public int NumberOfRequests { get; set; }
        public YearStatistics(int year, int number) 
        {
            this.Year = year;
            this.NumberOfRequests = number;
        }
    }
}
