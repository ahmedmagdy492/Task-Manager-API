using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Manager.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z]+ [A-Za-z]+$", ErrorMessage = "Full Name Must Consisit of first Name and last Name sperated by space")]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
