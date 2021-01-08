using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagement.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private DbProjectManagement db = new DbProjectManagement();
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListTask(string cardId)
        {
            var tasks = db.sp_GetAllTaskByCardId(int.Parse(cardId)).ToList();
            return PartialView(tasks);
        }

        public JsonResult AddTask(string cardId, string taskName)
        {
            var taskId = db.sp_AddTask(int.Parse(cardId), taskName).FirstOrDefault();
            var task = db.sp_GetTaskById(int.Parse(taskId.ToString())).SingleOrDefault();

            return Json(task, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteTask(string taskId)
        {
            var task = db.sp_GetTaskById(int.Parse(taskId.ToString())).SingleOrDefault();
            db.sp_DeleteTask(task.TaskID);
            return Json(task, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateStatus(string taskId, bool status)
        {
            var task = db.sp_GetTaskById(int.Parse(taskId.ToString())).SingleOrDefault();
            if (status)
                db.sp_UpdateStatusOnTask(task.TaskID);
            else
                db.sp_UpdateStatusOffTask(task.TaskID);
            return Json(task, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateTaskName(string taskId, string taskName)
        {
            db.sp_UpdateTaskName(int.Parse(taskId), taskName);
            return Json(new { taskId = taskId, taskName = taskName }, JsonRequestBehavior.AllowGet);
        }
    }
}