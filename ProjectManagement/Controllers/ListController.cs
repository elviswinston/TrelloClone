using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagement.Controllers
{
    [Authorize]
    public class ListController : Controller
    {
        private DbProjectManagement db = new DbProjectManagement();
        // GET: Card
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateList(string boardId, string listName)
        {
            db.sp_CreateList(listName, int.Parse(boardId));
            return Json(new { boardId = boardId, listName = listName }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteList(string listId)
        {
            db.sp_DeleteList(int.Parse(listId));
            return Json(new { listId = listId }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateListName(string listId, string listName)
        {
            db.sp_UpdateListName(int.Parse(listId), listName);
            return Json(new { listName = listName }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SwapList(string listIdA, string listIdB)
        {
            db.sp_SwapList(int.Parse(listIdA), int.Parse(listIdB));
            return Json(new { listIdA = listIdA, listIdB = listIdB }, JsonRequestBehavior.AllowGet);
        }
    }
}