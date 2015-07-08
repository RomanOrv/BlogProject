using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Frontend.Models
{
    public class NewBlogModel
    {
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6)]
        [Required()]
        public string Title { get; set; }
    }
}