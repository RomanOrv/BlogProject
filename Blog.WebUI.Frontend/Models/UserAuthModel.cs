using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Frontend.Models
{
    public class UserAuthModel
    {
        //[Required(ErrorMessage = "Please enter your name", AllowEmptyStrings = false)]
        //public string Login { set; get; }

        //[Required(ErrorMessage = "Please enter your password", AllowEmptyStrings = false)]
        //[DataType(DataType.Password)]
        //public string Password { set; get; }

        [Required()]
        public string Login { get; set; }

        [Required()]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}