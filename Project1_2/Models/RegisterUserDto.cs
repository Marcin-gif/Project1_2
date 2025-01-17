﻿using System.ComponentModel.DataAnnotations;

namespace Project1_2.Models
{
    public class RegisterUserDto
    {
        
        public string Email { get; set; }
        
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string? Nationality { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int RoleId { get; set; } = 1;
    }
}