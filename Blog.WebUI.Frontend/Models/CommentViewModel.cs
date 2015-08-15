using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Frontend.Models
{
    public class CommentViewModel
    {
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}