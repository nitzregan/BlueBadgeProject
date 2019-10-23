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
    public class NoBuyController : Controller
    {
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new GoalItemService(userID);
            var model = service.GetGoalItem();

            var serviceNoBuy = new NoBuyService(userID);
            var items = serviceNoBuy.GetNoBuys();
            var noBuyTotal = serviceNoBuy.GetNoBuys().Sum(e => e.ItemPrice);
            ViewBag.TotalSaved = noBuyTotal;


            return View(items);
        }

        public ActionResult Create()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new GoalItemService(userID);
            var model = service.GetGoalItem().ToList();
            model.Insert(0, new GoalItemListItem
            {
                GoalItemID = null,
                GoalItemName = "No Goal"
            });
            ViewBag.GoalItem = model;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoBuyCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateNoBuyService();

            if (service.CreateNoBuy(model))
            {
                TempData["SaveResult"] = "Your non-purchase was saved.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Non-purchase could not be saved.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateNoBuyService();
            var model = svc.GetNoBuyByID(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var serviceTwo = new GoalItemService(userID);
            var modelTwo = serviceTwo.GetGoalItem().ToList();
            modelTwo.Insert(0, new GoalItemListItem
            {
                GoalItemID = null,
                GoalItemName = "No Goal"
            });
            ViewBag.GoalItem = modelTwo;

            var service = CreateNoBuyService();
            var detail = service.GetNoBuyByID(id);
            var model =
                new NoBuyEdit
                {
                    ItemID = detail.ItemID,
                    ItemName = detail.ItemName,
                    ItemPrice = detail.ItemPrice,
                    ItemLocation = detail.ItemLocation,
                    GoalItemID = detail.GoalItemID
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoBuyEdit model)
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var serviceTwo = new GoalItemService(userID);
            var modelTwo = serviceTwo.GetGoalItem();
            ViewBag.GoalItem = modelTwo;

            if (!ModelState.IsValid) return View(model);

            if (model.ItemID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateNoBuyService();


            if (service.UpdateNoBuy(model))
            {
                TempData["SaveResult"] = "Your item was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your item could not be updated.");
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateNoBuyService();
            var model = svc.GetNoBuyByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateNoBuyService();

            service.DeleteNoBuy(id);

            TempData["SaveResult"] = "Your item was deleted";

            return RedirectToAction("Index");
        }

        private NoBuyService CreateNoBuyService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new NoBuyService(userID);
            return service;
        }
    }
}