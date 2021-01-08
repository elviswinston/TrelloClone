using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagement.Controllers
{
    public class BoardController : Controller
    {
        
        private DbProjectManagement db = new DbProjectManagement();
        // GET: Board
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult OnBoard(string boardId)
        {
            var board = db.sp_GetBoardById(int.Parse(boardId)).SingleOrDefault();
            ViewBag.BoardName = board.Name;
            ViewBag.BoardID = board.BoardID;
            var list = db.sp_GetAllListByBoardID(board.BoardID).Select(
                x => new List { ListID = x.ListID,
                                ListName = x.ListName,
                                BoardID = x.BoardID}).ToList();
            return View(list);
        }

        public JsonResult DeleteBoard(string boardId)
        {
            db.sp_DeleteBoard(int.Parse(boardId));
            return Json(new { boardId = boardId }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult UpdateBoardName(string boardId, string boardName)
        {
            db.sp_UpdateBoardName(int.Parse(boardId), boardName);
            return Json(new { boardName = boardName }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddCollab(string email, string boardId)
        {
            var user = db.sp_GetAccountByEmail(email).SingleOrDefault();
            if (user == null)
                return Json(new { }, JsonRequestBehavior.AllowGet);
            else
            {
                var res = db.sp_CreateProject(user.AccountID, int.Parse(boardId)).SingleOrDefault() ?? false;
                if (res)
                    return Json(new { res = res }, JsonRequestBehavior.AllowGet);
                else return Json(new { res = res }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { boardId = boardId, email = email, userId = user.AccountID }, JsonRequestBehavior.AllowGet);
        }
    }
}