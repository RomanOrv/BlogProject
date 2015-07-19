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
        public ActionResult ShowArticle(string title, int id, int authorId)
        {
            ViewBag.ArTitle = title;
            ViewBag.Id = id;
            ViewBag.AuthorId = authorId;
            return View();
        }


        [HttpGet]
        public ActionResult WriteArticle(string title, int id, int authorId)
        {
            ViewBag.ArTitle = title;
            ViewBag.Id = id;
            ViewBag.AuthorId = authorId;
            return View();
        }



        [HttpPost]
        public ActionResult GetFormattedText(int id)
        {
            string formattedText = _articleRepository
                                    .GetArticleForId(id)
                                    .Content;
            string encodedText = Server.UrlEncode(formattedText);
            return Json(new
            {
                formattedText = encodedText
            });
        }


        [HttpPost]
        public ActionResult SaveFormattedText(int id, string formattedText)
        {
            if (formattedText != string.Empty)
            {
                var decodedText = Server.UrlDecode(formattedText);
                _articleRepository.SetArticleContent(id, decodedText);
            }
            return Json(new { Id = id });
        }






    }
}