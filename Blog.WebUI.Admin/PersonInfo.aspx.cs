using Blog.Entities;
using Blog.WebUI.Admin.Code.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blog.WebUI.Admin
{
    public partial class PersonInfo : System.Web.UI.Page
    {
        private User _user;



        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    //var theme = Request["theme"];
        //    if (Session[SessionKeys.USER_INFO] == null)
        //    {
        //        Response.Redirect("~/Default.aspx");
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[SessionKeys.USER_INFO] != null)
            {
                _user = (User)Session[SessionKeys.USER_INFO];
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
            FillTable(_user);
            this.Master.TitleText = "Personal info about " + _user.Username;
        }


        private void FillTable(User user)
        {
            CreateTableRow("UserId", user.Id.ToString());

            string strFullName = String.Format("{0} {1}", user.Surname, user.Firstname);
            CreateTableRow("Fullname", strFullName);

            CreateTableRow("UserName", user.Username);

            CreateTableRow("Email", user.Email);

            CreateTableRow("Date Registration", user.DateRegister.ToString());

            CreateTableRow("isEnable", user.isEnable.ToString());

            CreateTableRow("Date Disable", user.DateDisable.ToString());

            CreateTableRow("Password", user.Password);

            CreateTableRow("isAdmin", user.isAdmin.ToString());
        }


        private void CreateTableRow(string title, string content)
        {
            TableRow row = new TableRow();
            row.Cells.Add(CreateTableCell(title));
            row.Cells.Add(CreateTableCell(content));
            tablePersonal.Rows.Add(row);
        }


        private TableCell CreateTableCell(string content)
        {
            TableCell cell = new TableCell();
            cell.Text = content;
            return cell;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }



    }
}