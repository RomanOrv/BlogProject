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
        public ValidationController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BlogEntities"].ConnectionString;
            this._userRepository = new EFUserRepository(connectionString);
            this._articleRepository = new EFArticleRepository(connectionString);
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