﻿using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.Injector
{
    public class ServiceInjector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
    {
        
        { typeof(IRenovationService), new RenovationService() },
        { typeof(ICommentService), new CommentService() },
        // Add more implementations here
    };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
