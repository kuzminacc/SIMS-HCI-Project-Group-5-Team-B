﻿using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public enum USERTYPE { Guide =0, GuideGuest, Owner, OwnerGuest };
    public class User : ISerializable, INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged();
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        private USERTYPE type;
        public USERTYPE Type
        {
            get { return type; }
            set { type = value; OnPropertyChanged(); }
        }

        public User()
        {
            Username = "";
            Password = "";
        }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }


        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            username = values[1];
            password = values[2];
            if (string.Equals(values[3], "Owner"))
                type = USERTYPE.Owner;
            else if (string.Equals(values[3], "OwnerGuest"))
                type = USERTYPE.OwnerGuest;
            else if (string.Equals(values[3],"Guide"))
                type = USERTYPE.Guide;
            else
                type = USERTYPE.GuideGuest;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Type.ToString() };
            return csvValues;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
