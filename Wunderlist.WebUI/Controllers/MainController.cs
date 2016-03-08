﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wunderlist.Services.Interfaces.Entities;
using Wunderlist.Services.Interfaces.Services;

namespace Wunderlist.WebUI.Controllers
{
    public class MainController : Controller
    {
        private readonly IToDoListService _toDoListService;

        public MainController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        [Authorize]
        public ActionResult Main()
        {
            return View("Main");
        }

        [Authorize]
        public JsonResult GetToDoLists()
        {
            List<ToDoListServiceEntity> toDoLists = _toDoListService.GetAllToDoListEntities().ToList();
            return Json(toDoLists, JsonRequestBehavior.AllowGet);
        }
    }
}