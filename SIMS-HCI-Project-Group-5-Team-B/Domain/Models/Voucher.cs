﻿using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }

        private int guideId;
        public int GuideId
        {
            get { return guideId; }
            set { guideId = value; }
        }

        private int guideGuestId;
        public int GuideGuestId
        {
            get { return guideGuestId; }
            set { guideGuestId = value; }
        }

        private DateTime received;

        public DateTime Received
        {
            get { return received; }
            set { received = value; }
        }

        private bool used;
        public bool Used
        {
            get { return used; }
            set { used = value; }
        }


        public Voucher(int guideId, int guideGuestId, DateTime received)
        {
            this.guideId = guideId;
            this.guideGuestId = guideGuestId;
            this.received = received;
            this.Used = false;
        }

        public Voucher() { }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                guideId.ToString(),
                guideGuestId.ToString(),
                received.ToString(),
                used.ToString(),
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            guideId = int.Parse(values[1]);
            guideGuestId = int.Parse(values[2]);
            received = DateTime.Parse(values[3]);
            used = Boolean.Parse(values[4]);
        }
    }
}
