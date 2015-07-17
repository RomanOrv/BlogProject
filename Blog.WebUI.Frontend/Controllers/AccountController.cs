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
                byte[] imagebyte = UploadImage(user.File);

                this._userRepository.AddNewUser(user.Firstname,
                                                user.Surname,
                                                user.Email,
                                                user.Description,
                                                user.Username,
                                                user.Password,
                                                imagebyte);
            }
            return RedirectToAction("Index", "Home");
        }


        private byte[] UploadImage(HttpPostedFileBase imageFile)
        {
            //uploadImage
            string filename = Path.GetFileName(imageFile.FileName);
            var path = Path.Combine(Server.MapPath("~/Images/") + filename);
            imageFile.SaveAs(path);

            //get array bytes of image
            MemoryStream target = new MemoryStream();
            imageFile.InputStream.CopyTo(target);
            byte[] data = target.ToArray();
            return data;
        }



        [HttpGet]
        public ActionResult Logout()
        {
            return null;
        }



        public FileContentResult GetProfileImage(int id)
        {
            byte[] data = _userRepository.GetUser(id).imgFile;
            return data != null 
                ? new FileContentResult(data,"image/jpg")
                :null;
        }

    }
}