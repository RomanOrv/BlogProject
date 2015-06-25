using Blog.Entities;
using Blog.Repository;
using Blog.WebUI.Admin.Code.Keys;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blog.WebUI.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        readonly string _connectionString;



        public Default()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["BlogEntities"].ConnectionString;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.TitleText = "Info about all users";
        }

        protected void odsUser_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = new UserRepository(_connectionString);
        }


        protected void odsUser_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            CheckBox checkBox = grvUser.Rows[grvUser.EditIndex].FindControl("chbxEnable") as CheckBox;
            e.InputParameters["IsEnable"] = checkBox.Checked;
        }

        protected void btnInfo_Click(object sender, EventArgs e)
        {
            odsUser.SelectMethod = "GetUser";
            odsUser.SelectParameters.Add(new Parameter("id", TypeCode.Int32));
          var par =  ((sender as Button).Parent.Parent as GridViewRow).Cells[2].Text;
          odsUser.SelectParameters["id"].DefaultValue = par;

          IEnumerable users =  odsUser.Select();

          Session[SessionKeys.USER_INFO] = users.OfType<User>().FirstOrDefault();
          Response.Redirect("~/PersonInfo.aspx");
        }

        protected void grvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        protected void grvUser_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }


       // private int GetIdFrom
    }
}