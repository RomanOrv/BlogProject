﻿using Blog.Repository;
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
            string _connectionString = ConfigurationManager.ConnectionStrings["BlogEntities"].ConnectionString;
            IUserRepository _userrepository = new EFUserRepository(_connectionString);
          int userId = Convert.ToInt32(HttpContext.Current.Request["userId"]);
            context.Response.ContentType = "image/jpeg";
            byte[] data = _userrepository.GetContentImage(userId);


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