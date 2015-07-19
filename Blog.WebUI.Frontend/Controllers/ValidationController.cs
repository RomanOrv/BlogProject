using Blog.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Frontend.Controllers
{
    public class ValidationController : Controller
    {
        IUserRepository _userRepository;
        IArticleRepository _articleRepository;
        public ValidationController(IUserRepository userRepository, IArticleRepository articleRepository)
        {
            this._userRepository = userRepository;
            this._articleRepository = articleRepository;
        }


        public JsonResult ValidateUsername(string Username)
        {
            bool isUnicue = this._userRepository.CheckUnicueUsername(Username);
            return Json(isUnicue, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ValidateBlogTitle(string Title)
        {
            bool isUnicue = this._articleRepository.CheckUniqueTitle(Title);
            return Json(isUnicue, JsonRequestBehavior.AllowGet);
        }

	}
}