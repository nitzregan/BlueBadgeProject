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
    [Authorize]
    public class MonthlyFinanceController : Controller
    {
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new GoalItemService(userID);
            var model = service.GetGoalItem();

            var serviceMonthlyFinance = new MonthlyFinanceService(userID);
            var items = serviceMonthlyFinance.GetMonthlyFinance();

            var noBuyService = new NoBuyService(userID);
            var noBuyTotal = noBuyService.GetNoBuys().Sum(e => e.ItemPrice);
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
        public ActionResult Create(MonthlyFinanceCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateMonthlyFinanceService();

            if (service.CreateMonthlyFinance(model))
            {
                TempData["SaveResult"] = "Your monthly finances have been saved.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Finances could not be saved.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateMonthlyFinanceService();
            var model = svc.GetMonthlyFinanceByID(id);

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

            var service = CreateMonthlyFinanceService();
            var detail = service.GetMonthlyFinanceByID(id);
            var model =
                new MonthlyFinanceEdit
                {
                    MonthlyTakeHome = detail.MonthlyTakeHome,
                    CostOfBills = detail.CostOfBills,
                    GoalItemID = detail.GoalItemID,
                    MonthlyFinanceID = detail.MonthlyFinanceID,
                    Month = detail.Month,
                    Year = detail.Year
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MonthlyFinanceEdit model)
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var serviceTwo = new GoalItemService(userID);
            var modelTwo = serviceTwo.GetGoalItem();
            ViewBag.GoalItem = modelTwo;

            if (!ModelState.IsValid) return View(model);

            if (model.MonthlyFinanceID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateMonthlyFinanceService();


            if (service.UpdateMonthlyFinance(model))
            {
                TempData["SaveResult"] = "Your month was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your month could not be updated.");
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateMonthlyFinanceService();
            var model = svc.GetMonthlyFinanceByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateMonthlyFinanceService();

            service.DeleteMonthlyFinance(id);

            TempData["SaveResult"] = "Your item was deleted";

            return RedirectToAction("Index");
        }

        private MonthlyFinanceService CreateMonthlyFinanceService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new MonthlyFinanceService(userID);
            return service;
        }
    }
}