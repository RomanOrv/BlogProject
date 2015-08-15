using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Frontend.Models
{
    public class CommentModel
    {
        [Required()]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        //[ScaffoldColumn(false)]
        public int ArticleId { get; set; }

        //[ScaffoldColumn(false)]
      //  public DateTime CommentTime;

    }
}