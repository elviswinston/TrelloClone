using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagement.Controllers
{
    [Authorize]
    public class CardController : Controller
    {
        private DbProjectManagement db = new DbProjectManagement();
        // GET: Card
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateCard(string listId, string cardTitle)
        {
            db.sp_CreateCard(cardTitle, int.Parse(listId));
            return Json(new { listId = listId, cardTitle = cardTitle }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListCard(string listId)
        {
            var cards = db.sp_GetAllCardByListId(int.Parse(listId)).ToList();
            return View(cards);
        }

        public JsonResult UpdateDescription(string cardId, string desc)
        {
            db.sp_UpdateCardDescription(int.Parse(cardId), desc);
            var card = db.sp_GetCardById(int.Parse(cardId));
            return Json(card, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateTag(string cardId, string tag)
        {
            db.sp_UpdateCardTag(int.Parse(cardId), tag);
            var card = db.sp_GetCardById(int.Parse(cardId));
            return Json(card, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateExpiry(string cardId, string expiry)
        {
            db.sp_UpdateCardExpiry(int.Parse(cardId), DateTime.Parse(expiry));
            var card = db.sp_GetCardById(int.Parse(cardId));
            return Json(card, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCard(string cardID)
        {
            db.sp_DeleteCard(int.Parse(cardID));
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateCardTitle(string cardId, string cardTitle)
        {
            db.sp_UpdateCardTitle(int.Parse(cardId), cardTitle);
            return Json(new { cardTitle = cardTitle}, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SwapCardInsideList(string cardIdA, string cardIdB)
        {
            db.sp_SwapCardInsideList(int.Parse(cardIdA), int.Parse(cardIdB));
            return Json(new { cardIdA = cardIdA, cardIdB = cardIdB }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SwapCardToList(string cardId, string listId)
        {
            db.sp_SwapCardToList(int.Parse(cardId), int.Parse(listId));
            return Json(new { cardId = cardId, listId = listId }, JsonRequestBehavior.AllowGet);
        }
    }
}