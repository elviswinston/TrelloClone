using ProjectManagement.Code;
using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectManagement.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (model.Email != "" && model.Password != "")
            {
                var result = new AccountModel().Login(model.Email, model.Password);
                if (result && ModelState.IsValid)
                {
                    SessionHelper.SetSession(new UserSession() { Email = model.Email });
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The email or password is incorrect");
                }
            }
          
            return View(model);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public JsonResult RegisterAccount(string email, string pass, string name)
        {
            if (email != "" && pass != "" && name != "")
            {
                var result = new AccountModel().Register(email, pass, name);
                if (result)
                    return Json(new { msg = "Đăng ký thành công", url = Url.Action("Login", "Account") }, JsonRequestBehavior.AllowGet);       
            };
            return Json(new { msg = "Email đã tồn tại", url = "" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}