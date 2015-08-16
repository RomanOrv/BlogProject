using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Blog.Repository;

namespace Blog.WebUI.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.TitleText = "Log in page";
        }



        protected void lgAuth_OnLoggingIn(object sender, LoginCancelEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BlogEntities"].ConnectionString;
            IUserRepository userRepository = new EFUserRepository(connectionString);
            ISecurityManager securityManager = new FormsSecurityManager(userRepository);
            string userName = lgAuth.UserName;
            string password = lgAuth.Password;

            if (securityManager.Login(userName, password) == true)
            {

            }
            else
            {

            }
        }

        protected void lgAuth_OnAuthenticate(object sender, AuthenticateEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }


    }
}