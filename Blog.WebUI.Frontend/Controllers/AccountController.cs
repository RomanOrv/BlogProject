using Blog.WebUI.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Frontend.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(UserAuthModel user)
        {
            if (ModelState.IsValid)
            {

            }
            else
            {

            }
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registration(RegistrUserModel user)
        {
            if (ModelState.IsValid)
            {

            }
            else
            {

            }
            return View();
        }
	}
}