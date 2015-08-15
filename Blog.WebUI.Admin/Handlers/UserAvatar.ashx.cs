using Blog.Entities;
using Blog.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Admin.Handlers
{
    /// <summary>
    /// Summary description for UserAvatar
    /// </summary>
    public class UserAvatar : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BlogEntities"].ConnectionString;
            IUserRepository userRepository = new EFUserRepository(connectionString);
            IPictureRepository pictureRepository = new EFPictureRepository(connectionString);
            int userId = Convert.ToInt32(HttpContext.Current.Request["userId"]);
            User user = userRepository.GetUser(userId);
            Picture picture = pictureRepository.GetPicture(user.PictureId);
            context.Response.ContentType = picture.ImageMimeType;
            byte[] data = picture.FileData;


            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                ms.WriteTo(HttpContext.Current.Response.OutputStream);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}