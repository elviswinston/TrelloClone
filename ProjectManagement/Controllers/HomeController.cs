using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectManagement.Controllers
{
    public class HomeController : Controller
    {
        private DbProjectManagement db = new DbProjectManagement();
        [Authorize]
        public ActionResult Index()
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            string userEmail = ticket.Name;

            var user = db.sp_GetAccountByEmail(userEmail).SingleOrDefault();

            dynamic boards = new ExpandoObject();
            boards.PersonalBoard = db.sp_GetPersonalBoard(user.AccountID).ToList();
            boards.TeamBoard = db.sp_GetTeamBoard(user.AccountID).ToList();

            return View(boards);
        }

        [Authorize]
        public ActionResult CreateBoard(string boardName)
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            string userEmail = ticket.Name;

            var user = db.sp_GetAccountByEmail(userEmail).SingleOrDefault();

            var boardId = db.sp_CreateBoard(boardName).FirstOrDefault();
            var board = db.sp_GetBoardById(int.Parse(boardId.Value.ToString())).SingleOrDefault();
            db.sp_CreateProject(user.AccountID, board.BoardID);
            
            return RedirectToAction("Index");
        }
    }
}