﻿using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface IAppointmentRepository
    {
        public List<Appointment> GetAll();
        public void Save(Appointment appointment);
        public void Delete(Appointment appointment);
        public void Update(Appointment appointment);
        void SaveAll(List<Appointment> newAppointment);
    }
}
