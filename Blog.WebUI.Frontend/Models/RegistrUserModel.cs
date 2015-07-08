using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Blog.WebUI.Frontend.Models
{
    public class RegistrUserModel
    {
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6)]
        [Required()]
        public string Firstname { get; set; }

        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6)]
        [Required()]
        public string Surname { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(30, MinimumLength = 6)]
        [Required()]
        public string Email { get; set; }


        public DateTime DateRegister { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6)]
        [Required()]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        [Required()]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        public string ConfirmPassword { get; set; }
    }
}