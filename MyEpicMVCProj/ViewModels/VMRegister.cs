﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyEpicMVCProj.ViewModels
{
    public class VMRegister
    {
        [DisplayName("User name")]
        public string Username { get; set; }
        [DisplayName("E-mail")]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Confirm e-mail")]
        [Compare("Email")]
        public string Email2 { get; set; }
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Country")]
        public string Country { get; set; } = null!;

        [DisplayName("Last name")]
        public string LastName { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }
        [DisplayName("Repeat password")]
        [Compare("Password")]
        public string Password2 { get; set; }
    }
}
