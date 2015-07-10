using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Frontend.Models
{
    public class NoteModel
    {
        [Required()]
        [DataType(DataType.Text)]
        public string Content;

        //[ScaffoldColumn(false)]
        public string Username;

        //[ScaffoldColumn(false)]
        public DateTime NoteTime;

    }
}