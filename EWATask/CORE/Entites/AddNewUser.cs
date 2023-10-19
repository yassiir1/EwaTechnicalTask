﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Entites
{
    public class AddNewUser
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public UserRole role { get; set; }

    }
    public enum UserRole
    {
        [Display(Name = "Admin")]
        Admin,

        [Display(Name = "User")]
        User
    }
}
