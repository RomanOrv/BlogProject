using Blog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Entities;
using System.Configuration;
using System.Web.Routing;


namespace Blog.WebUI.Frontend.Filters
{

    public class AllowAuthorAttribute : AuthorizeAttribute
    {
        private readonly IUserRepository _userRepository;
        private readonly IArticleRepository _articleRepository;

        private bool isAccess;

        public AllowAuthorAttribute()
        {
            isAccess = false;

            _userRepository = DependencyResolver.Current.GetService<IUserRepository>();
            _articleRepository = DependencyResolver.Current.GetService<IArticleRepository>();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                return false;
            }

            var rd = httpContext.Request.RequestContext.RouteData;

            var id = rd.Values["id"];
            var userName = httpContext.User.Identity.Name;

            Article article = _articleRepository.GetArticleForId(Convert.ToInt32(id));
            User user = _userRepository.GetUser(userName);
            isAccess = article.AuthorId == user.Id;

            return isAccess;
        }




        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //User isn't logged in
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Account", action = "Login" })
                );
            }
            //User is logged in but has no access
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Home", action = "Index" })
                );
            }
        }
    }
}
