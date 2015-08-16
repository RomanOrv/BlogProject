using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Frontend.Models
{
    public class NewArticleModel
    {
        [DataType(DataType.Text)]
        [System.Web.Mvc.Remote("ValidateBlogTitle", "Validation", ErrorMessage = "Blog name is already exists")]
        [Required()]
        public string Title { get; set; }
    }
}