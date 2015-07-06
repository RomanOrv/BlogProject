using Blog.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        public HomeController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BlogEntities"].ConnectionString;
            this._articleRepository = new EFArticleRepository(connectionString);
        }
        //
        // GET: /Home/
        [HttpGet]
        public ActionResult Index()
        {
            var articles = this._articleRepository.GetPublished();
            ViewBag.Articles = articles;
            return View();
        }

    }
}
