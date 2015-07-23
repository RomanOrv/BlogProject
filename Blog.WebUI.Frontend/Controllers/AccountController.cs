using Blog.Repository;
using Blog.Entities;
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
        const string DEF_IMG_FILE = "User-Default.jpg";
        const string DEF_IMAGE_TYPE = "image/jpeg";
        public AccountController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
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
                string imgFileType = GetImgFileType(user.File);
                byte[] imagebyte = GetImageByteArray(user.File);

                this._userRepository.AddNewUser(user.Firstname,
                                                user.Surname,
                                                user.Email,
                                                user.Description,
                                                user.Username,
                                                user.Password,
                                                imagebyte,
                                                imgFileType );
            }
            return RedirectToAction("Login", "Account");
        }



        private string GetImgFileType(HttpPostedFileBase imageFile)
        {
            return (imageFile != null) ? imageFile.ContentType : DEF_IMG_FILE;
        }
        

        private byte[] GetImageByteArray(HttpPostedFileBase imageFile)
        {
            byte[] data  = null;
            if (imageFile != null)
            {
                MemoryStream target = new MemoryStream();
                imageFile.InputStream.CopyTo(target);
                data = target.ToArray();
            }
            else
            {
                data = System.IO.File.ReadAllBytes(Server.MapPath("~/Images/" + DEF_IMG_FILE));
            }
            return data;
        }



        [HttpGet]
        public ActionResult Logout()
        {
            return null;
        }



        public JsonResult GetGetUserName(int id)
        {
            string username = _userRepository.GetUser(id).Username;
            return Json(new { userName = username }, JsonRequestBehavior.AllowGet);
        }

        public FileContentResult GetImage(int authorId)
        {
            User user = _userRepository.GetUser(authorId);
            return new FileContentResult(user.imgFile, user.ImageMimeType);
        }



    }
}