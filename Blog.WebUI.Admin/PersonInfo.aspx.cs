using Blog.Entities;
using Blog.WebUI.Admin.Code.Keys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            CreateTableRow("UserId", user.Id);

            string strFullName = String.Format("{0} {1}", user.Surname, user.Firstname);
            CreateTableRow("Fullname", strFullName);

            CreateTableRow("UserName", user.Username);

            CreateTableRow("Email", user.Email);

            CreateTableRow("Date Registration", user.DateRegister);

            CreateTableRow("isEnable", user.isEnable);

            CreateTableRow("Date Disable", user.DateDisable);

            CreateTableRow("Password", user.Password);

            CreateTableRow("isAdmin", user.isAdmin);

            CreateTableRow("Image", user.imgFile);
        }


        private void CreateTableRow(string title, object content)
        {
            if (title == "Image")
            {
                content = UploadImage(content.ToString());
            }
            TableRow row = new TableRow();
            row.Cells.Add(CreateTableCell(title));
            row.Cells.Add(CreateTableCell(content));
            tablePersonal.Rows.Add(row);
        }


        private TableCell CreateTableCell(object content)
        {
            TableCell cell = new TableCell();
            if (content != null)
            {
                if (content.GetType() != typeof(Image))
                {
                    cell.Text = content.ToString();
                }
                else
                {
                    cell.Controls.Add(content as Image);
                }
            }
            return cell;
        }

        private TableCell CreateTableCellWithImage(Image image)
        {
            TableCell cell = new TableCell();
            cell.Controls.Add(image);
            return cell;
        }


        private Image UploadImage(string file)
        {
            Image image = new Image();
            string filename = Path.GetFileName(file);
            //string path = Path.Combine(Environment.CurrentDirectory, "123.bmp");
            string path = @"//localhost/Images/" + file;
            image.ImageUrl = path;
            image.Width = 100;
            image.Height = 100;
            image.AlternateText = "Profile image";
            return image;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }



    }
}