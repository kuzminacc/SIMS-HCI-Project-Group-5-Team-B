﻿using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class SpecialTourRequestService
    {
        private ISpecialTourRequestsRepository specialTourRequestsRepository;

        public SpecialTourRequestService()
        {
            specialTourRequestsRepository = Injector.Injector.CreateInstance<ISpecialTourRequestsRepository>();
        }

        public void Save(SpecialTourRequest newSpecialTourRequest)
        {
            specialTourRequestsRepository.Save(newSpecialTourRequest);
        }

        public List<SpecialTourRequest> GetAll()
        {
            return specialTourRequestsRepository.GetAll();
        }

        public List<SpecialTourRequest> GetFor(int guideGuestId)
        {
            return GetAll().FindAll(specReq => specReq.TourRequests[0].GuideGuestId == guideGuestId);
        }

        public SpecialTourRequest Get(SpecialTourRequest specialTourRequest)
        {
            return GetAll().Find(req => req.Name.Equals(specialTourRequest.Name) && req.TourRequests.Equals(specialTourRequest.TourRequests));
        }
    }
}
