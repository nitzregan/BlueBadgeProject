using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WillPower.Models;
using WillPower.Services;

namespace WillPower.WebMVC.Controllers
{
    public class GoalItemController : Controller
    {
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new GoalItemService(userID);
            var model = service.GetGoalItem();

            var noBuyService = new NoBuyService(userID);
            var noBuyTotal = noBuyService.GetNoBuys().Sum(e => e.ItemPrice);
            ViewBag.TotalSaved = noBuyTotal;

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GoalItemCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateGoalItemService();

            if (service.CreateGoalItem(model))
            {
                TempData["SaveResult"] = "Your goal  purchase was saved.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Goal purchase could not be saved.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateGoalItemService();
            var model = svc.GetGoalItemByID(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateGoalItemService();
            var detail = service.GetGoalItemByID(id);
            var model =
                new GoalItemEdit
                {
                    GoalItemID = detail.GoalItemID,
                    GoalItemName = detail.GoalItemName,
                    GoalItemPrice = detail.GoalItemPrice,
                    GoalItemLocation = detail.GoalItemLocation
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GoalItemEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.GoalItemID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateGoalItemService();


            if (service.UpdateGoalItem(model))
            {
                TempData["SaveResult"] = "Your item was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your item could not be updated.");
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateGoalItemService();
            var model = svc.GetGoalItemByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateGoalItemService();

            service.DeleteGoalItem(id);

            TempData["SaveResult"] = "Your item was deleted";

            return RedirectToAction("Index");
        }

        private GoalItemService CreateGoalItemService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new GoalItemService(userID);
            return service;
        }
    }
}