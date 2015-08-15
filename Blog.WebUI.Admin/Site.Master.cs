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
    public partial class Site : System.Web.UI.MasterPage
    {
        public string TitleText
        {
            get
            {
                return lblInfoTitle.Text;
            }
            set
            {
                lblInfoTitle.Text = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnLoggingOut(object sender, LoginCancelEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BlogEntities"].ConnectionString;
            IUserRepository userRepository = new EFUserRepository(connectionString);
            ISecurityManager securityManager = new FormsSecurityManager(userRepository);
            securityManager.Logout();
        }
    }
}