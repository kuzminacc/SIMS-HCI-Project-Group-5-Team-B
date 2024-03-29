
﻿using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class AppointmentService
    {
        private IAppointmentRepository appointmentRepository;
        private TourAttendanceService tourAttendanceService;
        private VoucherService voucherService;
        public AppointmentService()
        {
            this.tourAttendanceService = new TourAttendanceService();
            this.appointmentRepository = Injector.Injector.CreateInstance<IAppointmentRepository>();
            this.voucherService = new VoucherService();
        }

        public Appointment Find(int appointmentId)
        {
            return appointmentRepository.GetAll().Find(a => a.Id == appointmentId);
        }

        public bool IsEnded(int appointmentId)
        {
            return appointmentRepository.GetAll().Find(a => a.Id == appointmentId).Ended;
        }

        public Tour GetLiveTourFor(int guideGuestId)
        {
            Appointment appointment;

            foreach(var attendance in tourAttendanceService.GetAllFor(guideGuestId))
            {
                appointment = Find(attendance.AppointmentId);
                if(appointment.Started && !appointment.Ended)
                {
                    return appointment.Tour;
                }
            }
            return null;
        }

        public List<Appointment> GetAllHeldFor(int guideGuestId)
        {
            List<Appointment> heldAppointments = new List<Appointment>();
            Appointment appointment;
            foreach(var tourAttendance in tourAttendanceService.GetAllFor(guideGuestId))
            {
                appointment = Find(tourAttendance.AppointmentId);
                if (appointment.Ended)
                {
                    heldAppointments.Add(appointment);
                }
            }
            return heldAppointments;
        }

        public List<string> GetAllYears()
        {
            return GetAll().Select(a => a.Start.Year.ToString()).Distinct().ToList();
        }

        public List<TourAttendance> GetAllFor(int guideGuestId)
        {
            return tourAttendanceService.GetAllFor(guideGuestId);
        }

        public TourAttendance GetAttendance(int guideGuestId, int appointmentId)
        {
            foreach (var tourAttendance in tourAttendanceService.GetAllFor(guideGuestId))
            {
                if (tourAttendance.AppointmentId == appointmentId)
                {
                    return tourAttendance;
                }
            }
            return null;
        }

        public int GetGuideWithMostTours()
        {
            var a = GetAll()
            .GroupBy(appointment => appointment.GuideId)
            .OrderByDescending(group => group.Count())
            .FirstOrDefault();
            return a.Key;
        }

        public string GetMostSpokenLanguage()
        {
            var a = GetAll()
            .GroupBy(appointment => appointment.Tour.Language)
            .OrderByDescending(group => group.Count())
            .FirstOrDefault();
            return a.Key;
        }

        public Appointment GetMostVisitedTour(int year, int userId)
        {
            int id = 0;
            int people = 0;
            if (GetFinishedToursByYear(year, userId) == null)
                return new Appointment();

            foreach (Appointment appointment in GetFinishedToursByYear(year, userId))
            {
                if (people < tourAttendanceService.GetTotalGuest(appointment.Id) && appointment.GuideId == userId)
                {
                    id = appointment.Id;
                    people = tourAttendanceService.GetTotalGuest(appointment.Id);
                }
            }

            if (id == 0)
                return null;
            return getById(id);
        }
        public List<Appointment> GetAll()
        {
            return appointmentRepository.GetAll();
        }
        public List<Appointment> GetAllAvaillable(int userId)
        {
            return appointmentRepository.GetAll().FindAll(a => a.Start.Date == DateTime.Now.Date && a.Cancelled == false && a.GuideId == userId);
        }
        public List<Appointment> GetUpcoming(int userId)
        {
            return appointmentRepository.GetAll().Where(a => (a.Start - DateTime.Now).TotalHours >= 48 && a.Cancelled == false && a.GuideId == userId).ToList();
        }
        public bool IsAvailable(int userId, DateTime date)
        {
            List<Appointment> all = appointmentRepository.GetAll().FindAll(a => a.Start.Year == date.Year && a.Start.Month == date.Month && a.Start.Day == date.Day && a.GuideId == userId).ToList();
            if(all.Count == 0) 
                return true;
            return false;
        }
        public List<Appointment> GetFinishedToursByYear(int year, int userId) 
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach(Appointment a in GetAll())
            {
                if(a.Ended == true && a.Start.Year == year && a.GuideId == userId)
                    appointments.Add(a);
            }
            if (appointments.Count() == 0)
                return null;
            return appointments;
        }
        public string GetFinishedToursLastYear(int userId)
        {
            return appointmentRepository.GetAll()
                .Where(a => a.Ended == true && a.Start >= DateTime.Now.AddYears(-1) && a.GuideId == userId)
                .Select(a => a.Tour.Language)
                .GroupBy(language => language)
                .Where(group => group.Count() > 20)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .FirstOrDefault();
        }
        public List<Appointment> GetScheduledToursForPeriod(DateTime start,DateTime end)
        {
            return GetAll().FindAll(a => a.Start.Date >= start && a.Start.Date <= end);
        }
        public Appointment StartedAppointment(int guideId)
        {
            foreach (Appointment appointment in GetAll())
            {
                if (appointment.Started == true && appointment.Ended != true && guideId == appointment.GuideId)
                {
                    return appointment;
                }
            }
            return null;
        }
        public void CancelAllGuideAppointments(int guideId)
        {
            List<Appointment> all = appointmentRepository.GetAll().FindAll(a => a.GuideId == guideId).ToList();
            foreach (Appointment appointment in all)
            {
                appointment.Cancelled = true;
                voucherService.SendVouchers(guideId, appointment.Id);
                appointmentRepository.Update(appointment);
            }
        }
        public void Save(Appointment newAppointment)
        {
            appointmentRepository.Save(newAppointment);
        }
        public void SaveAll(List<Appointment> appointments)
        {
            foreach(Appointment appointment in appointments)
                appointmentRepository.Save(appointment);
        }
        public void Delete(Appointment appointment)
        {
            appointmentRepository.Delete(appointment);
        }
        public void Update(Appointment appointment)
        {
            appointmentRepository.Update(appointment);
        }
        public Appointment getById(int id)
        {
            return GetAll().Find(a => a.Id == id);
        }
        public List<Appointment> GetAllBookable(int tourId)
        {
            return GetAll().FindAll(appointment => appointment.TourId == tourId && appointment.Start > DateTime.Now && !appointment.Started);
        }
    }
}
