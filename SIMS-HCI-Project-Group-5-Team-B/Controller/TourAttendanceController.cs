﻿using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System.Collections.Generic;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class TourAttendanceController
    {
        private Repository<TourAttendance> tourAttendanceRepository;
        public TourAttendanceController()
        {
            tourAttendanceRepository = new Repository<TourAttendance>();
        }
        public List<TourAttendance> GetAll()
        {
            return tourAttendanceRepository.GetAll();
        }
        public void Save(TourAttendance newTourAttendance)
        {
            tourAttendanceRepository.Save(newTourAttendance);
        }
        public void Delete(TourAttendance tourAttendance)
        {
            tourAttendanceRepository.Delete(tourAttendance);
        }
        public void Update(TourAttendance tourAttendance)
        {
            tourAttendanceRepository.Update(tourAttendance);
        }
        public List<TourAttendance> FindBy(string[] propertyNames, string[] values)
        {
            return tourAttendanceRepository.FindBy(propertyNames, values);
        }
        public TourAttendance getById(int id)
        {
            return GetAll().Find(to => to.Id == id);
        }
        public int makeId()
        {
            return tourAttendanceRepository.NextId();
        }
    }
}
