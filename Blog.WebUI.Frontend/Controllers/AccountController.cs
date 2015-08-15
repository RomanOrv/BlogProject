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
using System.Threading;
using Blog.WebUI.Frontend.Filters;

namespace Blog.WebUI.Frontend.Controllers
{
    [BlogAuthorize]
    public class AccountController : Controller
    {
        readonly IUserRepository _userRepository;
        readonly IPictureRepository _pictureRepository;
        readonly private ISecurityManager _sequrityManager;
        private const string DEF_IMG_FILE = "User-Default.jpg";
        public AccountController(IUserRepository userRepository, IPictureRepository pictureRepository, ISecurityManager sequrityManager)
        {
            this._userRepository = userRepository;
            this._pictureRepository = pictureRepository;
            this._sequrityManager = sequrityManager;
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HandleError(ExceptionType = typeof(Exception), View = "Error")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserAuthModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _sequrityManager.Login(user.Login, user.Password);
                }
                catch
                {
                    throw new Exception();
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
           //
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [AllowAnonymous]
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
                                                imgFileType);
            }
            return RedirectToAction("Login", "Account");
        }




        [HttpPost]
        public ActionResult Logout()
        {
            _sequrityManager.Logout();
            return RedirectToAction("Login", "Account");
        }



        public JsonResult GetUserName(int id)
        {
            string username = _userRepository.GetUser(id).Username;
            return Json(new { userName = username }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public FileContentResult GetImage(int authorId)
        {
            User user = _userRepository.GetUser(authorId);
            Picture picture = _pictureRepository.GetPicture(user.PictureId);
            return new FileContentResult(picture.FileData, picture.ImageMimeType);
        }


        [AllowAnonymous]
        public FileContentResult GetImageProf(string username)
        {
            User user = _userRepository.GetUser(username);
            Picture picture = _pictureRepository.GetPicture(user.PictureId);
            return new FileContentResult(picture.FileData, picture.ImageMimeType);
        }



        #region Helpers
        [AllowAnonymous]
        private string GetImgFileType(HttpPostedFileBase imageFile)
        {
            return (imageFile != null) ? imageFile.ContentType : DEF_IMG_FILE;
        }

        [AllowAnonymous]
        private byte[] GetImageByteArray(HttpPostedFileBase imageFile)
        {
            byte[] data = null;
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

        #endregion



    }
}