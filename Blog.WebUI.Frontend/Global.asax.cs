using Blog.Repository;
using Blog.WebUI.Frontend.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;
//using Logger = dotless.Core.Loggers.Logger;
//using LogLevel = dotless.Core.Loggers.LogLevel;

namespace Blog.WebUI.Frontend
{
    public class MvcApplication : System.Web.HttpApplication
    {


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            UnityConfig.RegisterComponents();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BlogEntities"].ConnectionString;
            IUserRepository userRepository = new EFUserRepository(connectionString);
            ISecurityManager securityManager = new FormsSecurityManager(userRepository);

            securityManager.RefreshPrincipal();
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                if (Server.GetLastError() != null)
                {
                    Exception ex = Server.GetLastError().GetBaseException();
                    if (ex is HttpException == false)
                    {
                        Logger logger = LogManager.GetCurrentClassLogger();
                        logger.Log(LogLevel.Error, ex);
                    }
                    Server.Transfer("~/Error.aspx");
                }
            }
            catch
            { }
        }

    }
}
