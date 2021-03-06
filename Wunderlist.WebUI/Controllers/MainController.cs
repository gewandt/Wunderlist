﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wunderlist.Services.Interfaces.Entities;
using Wunderlist.Services.Interfaces.Services;

namespace Wunderlist.WebUI.Controllers
{
    public class MainController : Controller
    {
        private enum Status
        {
            Wait = 1,
            Completed = 2
        };

        private readonly IToDoListService _toDoListService;
        private readonly IUserService _userService;
        private readonly IToDoTaskService _toDoTaskService;

        public MainController(IToDoListService toDoListService, IUserService userService, IToDoTaskService toDoTaskService)
        {
            _toDoListService = toDoListService;
            _userService = userService;
            _toDoTaskService = toDoTaskService;
        }

        [Authorize]
        public ActionResult Main()
        {
            return View("Main");
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetToDoLists()
        {
            var userEmail = HttpContext.User.Identity.Name;
            var userId = _userService.GetUserEntity(userEmail).Id;
            List<ToDoListServiceEntity> toDoLists = _toDoListService.GetAllToDoListEntitiesById(userId).ToList();
            return Json(toDoLists, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddToDoList(string name)
        {
            var userEmail = HttpContext.User.Identity.Name;
            var userId = _userService.GetUserEntity(userEmail).Id;
            _toDoListService.Create(name, userId);
            List<ToDoListServiceEntity> toDoLists = _toDoListService.GetAllToDoListEntitiesById(userId).ToList();
            return Json(toDoLists, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetToDoItems(string listName)
        {
            var userEmail = HttpContext.User.Identity.Name;
            var userId = _userService.GetUserEntity(userEmail).Id;
            var toDoList = _toDoListService.GetAllToDoListEntitiesById(userId)
                    .FirstOrDefault(c => c.Name == listName);
            if (toDoList != null)
            {
                var tasks = _toDoTaskService.GetAllTasksByListIdAndStatusId(toDoList.Id, (int)Status.Wait).ToList();
                return Json(tasks, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddToDoItem(string name, string listname)
        {
            var userEmail = HttpContext.User.Identity.Name;
            var userId = _userService.GetUserEntity(userEmail).Id;
            var toDoListServiceEntity = _toDoListService.GetAllToDoListEntitiesById(userId)
                .FirstOrDefault(c => c.Name == listname);
            if (toDoListServiceEntity == null)
                return Json(null, JsonRequestBehavior.AllowGet);
            var toDoListId = toDoListServiceEntity.Id;
            _toDoTaskService.Create(name, toDoListId);
            return GetToDoItems(listname);
        }

        [HttpPut]
        public JsonResult DeleteToDoItem(int taskId, string listname)
        {
            _toDoTaskService.Delete(taskId);
            return GetToDoItems(listname);
        }

        [HttpPut]
        public JsonResult DeleteToDoList(int listItemId, string listname)
        {
            _toDoListService.Delete(listItemId);
            return GetToDoLists();
        }

        [HttpPost]
        public JsonResult RenameToDoList(int listItemId, string listname)
        {
            _toDoListService.Update(listItemId, listname);
            return GetToDoLists();
        }

        [HttpPost]
        public JsonResult RenameToDoItem(int taskItemId, string taskname, string listname)
        {
            var currentTask = _toDoTaskService.GetTaskById(taskItemId);
            _toDoTaskService.Update(taskItemId, taskname, currentTask.TaskStatusId);
            return GetToDoItems(listname);
        }

        [HttpPost]
        public JsonResult ChangeTaskStatus(int taskId, bool status, int listId)
        {
            var currentToDoList = _toDoListService.GetById(listId);
            var currentTask = _toDoTaskService.GetTaskById(taskId);
            var statusId = (status) ? Status.Completed : Status.Wait;
            _toDoTaskService.Update(taskId, currentTask.Name, (int)statusId);
            return GetToDoItems(currentToDoList.Name);
        }


        [HttpPost]
        public JsonResult GetCompletedToDoItems(int listId)
        {
            var currentToDoList = _toDoListService.GetById(listId);
            var toDoItems = _toDoTaskService.GetAllTasksByListIdAndStatusId(currentToDoList.Id, (int) Status.Completed);
            if (toDoItems == null)
                return Json(null, JsonRequestBehavior.AllowGet);
            return Json(toDoItems, JsonRequestBehavior.AllowGet);
        }
    }
}