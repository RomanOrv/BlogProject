using Blog.Entities;
using Blog.Repository;
using Blog.WebUI.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Blog.WebUI.Frontend.Filters;


namespace Blog.WebUI.Frontend.Controllers
{
    [BlogAuthorize]
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IPictureRepository _pictureRepository;
        private ICommentRepository _commentRepository;
        readonly private ISecurityManager _sequrityManager;
        const string LOCAL_IMAGE_ROOT = "/Home/GetImage?url=";

        public HomeController(IUserRepository userRepository,
                            IArticleRepository articleRepository,
                            IPictureRepository pictureRepository,
                            ICommentRepository commentRepository,
                            ISecurityManager sequrityManager)
        {
            this._articleRepository = articleRepository;
            this._pictureRepository = pictureRepository;
            this._sequrityManager = sequrityManager;
            this._userRepository = userRepository;
            this._commentRepository = commentRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index(string searchTitle = "")
        {

            var articles = this._articleRepository.GetPublished();
            if (!String.IsNullOrEmpty(searchTitle))
            {
                articles = articles.Where(a => a.Title.IndexOf(searchTitle, StringComparison.OrdinalIgnoreCase) != -1)
                                                .ToList();
            }
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
                User user = GerCurrentUser();
                _articleRepository.AddNewArticle(blog.Title, user.Id);

                return RedirectToAction("Index");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ShowArticle(string title, int id, int authorId)
        {
            ViewBag.ArTitle = title;
            ViewBag.Id = id;
            ViewBag.AuthorId = authorId;
            ViewBag.ListComment = _commentRepository.GetArticleComments(id);
            return View();
        }





        [AllowAuthor]
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

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetFormattedText_Show(int id)
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


        private string SearchImageSrc(string text)
        {
            int index = text.IndexOf("http");
            int idx = text.IndexOf('"', index);
            string src = text.Substring(index, idx - index);
            return src;
        }

        [HttpPost]
        public ActionResult SaveFormattedText(int id, string formattedText)
        {
            if (formattedText != string.Empty)
            {
                var decodedText = Server.UrlDecode(formattedText);
                string src = SearchImageSrc(decodedText);
                SaveImage(src);
                if (decodedText.IndexOf(LOCAL_IMAGE_ROOT) != -1)
                {
                    decodedText = decodedText.Replace(LOCAL_IMAGE_ROOT, "");
                }
                decodedText = decodedText.Replace(src, LOCAL_IMAGE_ROOT + src);
                _articleRepository.SetArticleContent(id, decodedText);
            }
            return Json(new { Id = id });
        }


        private void SaveImage(string url)
        {
            byte[] data = DownloadData(url);
            string mimeType = GetImageMimeType(url);
            _pictureRepository.AddPicture(data, mimeType, url);
        }



        private byte[] DownloadData(string url)
        {
            //Get a data stream from the url
            WebRequest req = WebRequest.Create(url);
            WebResponse response = req.GetResponse();
            Stream stream = response.GetResponseStream();

            //Download in chuncks
            byte[] buffer = new byte[1024];

            //Get Total Size
            int dataLength = (int)response.ContentLength;

            MemoryStream memStream = new MemoryStream();
            while (true)
            {
                //Try to read the data
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    break;
                }
                else
                {
                    //Write the downloaded data
                    memStream.Write(buffer, 0, bytesRead);
                }
            }
            return memStream.ToArray();
        }


        [AllowAnonymous]
        private string GetImageMimeType(string url)
        {
            string ext = url.Substring(url.LastIndexOf('.'));
            string mimetype = string.Empty;

            switch (ext)
            {
                case ".jpg":
                case ".jpeg":
                    mimetype = "image/jpeg";
                    break;
                case ".png":
                    mimetype = "image/png";
                    break;
                case ".gif":
                    mimetype = "image/gif";
                    break;
            }

            return mimetype;

        }


        [AllowAnonymous]
        public FileContentResult GetImage(string url)
        {
            Picture picture = _pictureRepository.GetPicture(url);
            return new FileContentResult(picture.FileData, picture.ImageMimeType);
        }



        public ActionResult ShowOwnArticles()
        {
            User user = GerCurrentUser();
            List<Article> userArticles = _articleRepository.GetUserArticles(user.Id);
            ViewBag.UserArticles = userArticles;
            return View();
        }

        [HttpGet]
        public ActionResult ChangePublish(int articleId)
        {
           // User user = GerCurrentUser();
            Article userArticle = _articleRepository.GetArticleForId(articleId);
            ViewBag.UserArticles = userArticle;
            return View(userArticle);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ChangePublish(Article userArticle)
        {
            _articleRepository.ChangePublishingStatus(userArticle);
            return RedirectToAction("ShowOwnArticles");
        }


        private User GerCurrentUser()
        {
            var userPrincipal = _sequrityManager.CurrentUser;
            string login = userPrincipal.Identity.Name;
            User user = _userRepository.GetUser(login);
            return user;
        }




        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult CommentListPartial(Comment[] commetaries)
        {
            CommentViewModel[] commViewList = commetaries.Select(x => ToCommentViewModel(x)).ToArray();
            ViewBag.Commentaries = commViewList;
            return View();
        }




        [HttpPost]
        public ActionResult ArticleCommentPartial(CommentModel commModel)
        {
            User user = GerCurrentUser();
            Article article = _articleRepository.GetArticleForId(commModel.ArticleId);
            _commentRepository.AddNewComment(commModel.Content, user.Id, commModel.ArticleId);
            return RedirectToAction("ShowArticle", new { title = article.Title, id = article.Id, authorId = article.AuthorId });
        }


        private CommentViewModel ToCommentViewModel(Comment comment)
        {
            return new CommentViewModel()
            {
                UserName = _userRepository.GetUser(comment.AuthorId).Username,
                Content = comment.Content,
                Date = comment.CommDate
            };
        }

    }
}