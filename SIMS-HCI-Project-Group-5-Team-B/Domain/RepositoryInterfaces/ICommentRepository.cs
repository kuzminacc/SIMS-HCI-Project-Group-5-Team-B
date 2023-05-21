﻿using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface ICommentRepository
    {
        public void Save(Comment newComment);
        public List<Comment> GetAll();
        public Comment GetById(int id);
    }
}
