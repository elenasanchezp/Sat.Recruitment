﻿namespace Sat.Recruitment.Model.Models
{
    /// <summary>
    /// Contains data relative to a valid user
    /// </summary>
    public class User
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public UserType UserType { get; set; }

        public decimal Money { get; set; }
    }

}