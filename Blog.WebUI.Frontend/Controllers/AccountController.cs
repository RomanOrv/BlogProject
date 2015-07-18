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
        const string DEF_IMG_FILE = "User-Default.jpg";
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
                string imgFileName = GetImgFileName(user.File);
                byte[] imagebyte = GetImageByteArray(user.File);

                this._userRepository.AddNewUser(user.Firstname,
                                                user.Surname,
                                                user.Email,
                                                user.Description,
                                                user.Username,
                                                user.Password,
                                                imagebyte,
                                                imgFileName);
            }
            return RedirectToAction("Index", "Home");
        }



        private string GetImgFileName(HttpPostedFileBase imageFile)
        {
            //uploadImage
            string filename = string.Empty;
            if (imageFile != null)
            {
                filename = Path.GetFileName(imageFile.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/" + filename));
                imageFile.SaveAs(path);
            }
            else
            {
                filename = DEF_IMG_FILE;
            }
            return filename;
        }
        

        private byte[] GetImageByteArray(HttpPostedFileBase imageFile)
        {
            byte[] data  = null;
            if (imageFile != null)
            {
                //get array bytes of image
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



        public JsonResult GetProfileImagePath(int id)
        {
            string imgFilename = _userRepository.GetUser(id).imgFileName;
            return Json(new { imgSrc = imgFilename }, JsonRequestBehavior.AllowGet);
        }

    }
}