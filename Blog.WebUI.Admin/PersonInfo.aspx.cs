using Blog.Entities;
using Blog.Repository;
using Blog.WebUI.Admin.Code.Keys;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private readonly string _connectionString;
        private User _user;
        private IPictureRepository _pictureRepository;



        public PersonInfo()
        {
            this._connectionString = ConfigurationManager.ConnectionStrings["BlogEntities"].ConnectionString;
            this._pictureRepository = new EFPictureRepository(this._connectionString);
        }

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

           Picture picture = _pictureRepository.GetPicture(user.PictureId);

            CreateTableRow("Image", picture.FileData);

            CreateTableRow("ImageType", picture.ImageMimeType);
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

            image.ImageUrl = String.Format("~/Handlers/UserAvatar.ashx?userid={0}", _user.Id);
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