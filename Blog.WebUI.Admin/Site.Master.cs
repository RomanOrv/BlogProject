using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    }
}