using Blog.Entities;
using Blog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Blog.WebUI
{
  public class FormsSecurityManager : ISecurityManager
    {

        #region Fields

        private readonly IUserRepository _userRepository;

        #endregion

        #region Constructors

        public FormsSecurityManager(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
          
        }

        #endregion

        #region ISecurityManager

        public bool Login(string userName, string password)
        {
            User user = this._userRepository.GetUser(userName, password);

            if (user == null)
            {
                return false;
            }

            this.RefreshPrincipal();

            FormsAuthentication.SetAuthCookie(userName, false);

            return true;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.User != null
                    && HttpContext.Current.User.Identity != null
                    && HttpContext.Current.User.Identity.IsAuthenticated == true;
            }
        }

        public void RefreshPrincipal()
        {
            IPrincipal incomingPrincipal = HttpContext.Current.User;
            if (this.IsAuthenticated == true)
            {
                User user = this._userRepository.GetUser(incomingPrincipal.Identity.Name);
                HttpContext.Current.User = this.CreatePrincipal(user);
            }
        }




        public IPrincipal CurrentUser
        {
            get
            {
                return HttpContext.Current.User;
            }
        }

        #endregion

        #region Helpers

        private ClaimsPrincipal CreatePrincipal(User user)
        {
            string userName = user.Username;
            List<string> roles = new List<string>();
            roles.Add("user");
            if (user.isAdmin)
            {
                roles.Add("admin");
            }
            GenericIdentity identity = new GenericIdentity(user.Username);
            GenericPrincipal principal = new GenericPrincipal(identity, roles.ToArray());

            return principal;
        }

        #endregion

    }
}
