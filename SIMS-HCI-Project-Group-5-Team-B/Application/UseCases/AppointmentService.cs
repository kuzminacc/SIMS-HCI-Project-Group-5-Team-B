﻿using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class AppointmentController
    {
        private IAppointmentRepository appointmentRepository;
        private TourController tourController;
        public AppointmentController(TourController tourController)
        {
            appointmentRepository = Injector.CreateInstance<IAppointmentRepository>();
            this.tourController = tourController;
            GetTourReference();
        }
        public AppointmentController()
        {
            appointmentRepository = Injector.CreateInstance<IAppointmentRepository>();
            this.tourController = new TourController();
            GetTourReference();
        }
        public List<Appointment> GetAll()
        {
            return appointmentRepository.GetAll();
        }
        public List<Appointment> GetAllAvaillable()
        {
            return appointmentRepository.GetAll().FindAll(a => a.Start.Date == DateTime.Now.Date && a.Cancelled == false);
        }
        public List<Appointment> GetUpcoming()
        {
            GetTourReference();
            return appointmentRepository.GetAll().Where(a => (a.Start - DateTime.Now).TotalHours >= 48 && a.Cancelled == false).ToList();
        }
        public void Save(Appointment newAppointment)
        {
            appointmentRepository.Save(newAppointment);
        }
        public void SaveAll(List<Appointment> newAppointment)
        {
            appointmentRepository.SaveAll(newAppointment);
        }
        public void Delete(Appointment appointment)
        {
            appointmentRepository.Delete(appointment);
        }
        public void Update(Appointment appointment)
        {
            appointmentRepository.Update(appointment);
        }
        public List<Appointment> FindBy(string[] propertyNames, string[] values)
        {
            return appointmentRepository.FindBy(propertyNames, values);
        }
        public Appointment getById(int id)
        {
            return GetAll().Find(a => a.Id == id);
        }
        private void GetTourReference()
        {
            foreach (var appointment in GetAll())
            {
                Tour tour = tourController.getById(appointment.TourId);
                if (tour != null)
                {
                    appointment.Tour = tour;
                }
            }
        }
    }
}