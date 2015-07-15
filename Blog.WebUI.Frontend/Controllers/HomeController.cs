using Blog.Repository;
using Blog.WebUI.Frontend.Models;
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
            RTE.Editor editor = new RTE.Editor();
            string connectionString = ConfigurationManager.ConnectionStrings["BlogEntities"].ConnectionString;
            this._articleRepository = new EFArticleRepository(connectionString);
        }


        [HttpGet]
        public ActionResult Index()
        {
            var articles = this._articleRepository.GetPublished();
            ViewBag.Articles = articles;
            return View();
        }


        [HttpGet]
        public ActionResult NewArticle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewArticle(NewArticleModel blog)
        {
            if (ModelState.IsValid)
            {
                _articleRepository.AddNewArticle(blog.Title);

                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public ActionResult ShowArticle(int? id)
        {
            ViewBag.Id = id;
            return View();
        }





    }
}