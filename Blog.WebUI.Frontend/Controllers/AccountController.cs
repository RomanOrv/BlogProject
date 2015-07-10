using Blog.Repository;
using Blog.WebUI.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Frontend.Controllers
{
    public class AccountController : Controller
    {
        IUserRepository _userRepository;
        public AccountController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BlogEntities"].ConnectionString;
            this._userRepository = new EFUserRepository(connectionString);
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(UserAuthModel user)
        {
            if (ModelState.IsValid)
            {

            }
            else
            {

            }
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registration(RegistrUserModel user)
        {
            if (ModelState.IsValid)
            {
                string filename = UplaodImage(user.File);

                this._userRepository.AddNewUser(user.Firstname,
                                                user.Surname,
                                                user.Email,
                                                user.Description,
                                                user.Username,
                                                user.Password,
                                                filename);
            }
            return View();
        }


        private string UplaodImage(HttpPostedFileBase imageFile)
        {
            string filename = string.Empty;
            if (imageFile != null)
            {
                 filename = Path.GetFileName(imageFile.FileName);
                 var path = Path.Combine(Server.MapPath("~/Images/") + filename);
                 imageFile.SaveAs(path);
            }
            else
            {
                filename = "User-Default.jpg";
            }
            return filename;
        }
    }
}