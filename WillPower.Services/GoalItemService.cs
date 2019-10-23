using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillPower.Data;
using WillPower.Models;

namespace WillPower.Services
{
    public class GoalItemService
    {
        private readonly Guid _userID;

        public GoalItemService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateGoalItem(GoalItemCreate model)
        {
            var entity =
                new GoalItem()
                {
                    UserID = _userID,
                    GoalItemName = model.GoalItemName,
                    GoalItemPrice = model.GoalItemPrice,
                    GoalItemLocation = model.GoalItemLocation,
                    CreatedUTC = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.GoalItems.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<GoalItemListItem> GetGoalItem()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .GoalItems
                        .Where(e => e.UserID == _userID).ToList();
                List<GoalItemListItem> goals = new List<GoalItemListItem>();
                foreach(var item in query)
                {
                    var noBuys = ctx.NoBuys.Where(e => e.GoalItemID == item.GoalItemID).ToList().Sum(e => e.ItemPrice);
                    var thing = new GoalItemListItem
                    {
                        GoalItemID = item.GoalItemID,
                        GoalItemName = item.GoalItemName,
                        GoalItemPrice = item.GoalItemPrice,
                        GoalItemLocation = item.GoalItemLocation,
                        TotalSavedFromNoBuys = noBuys,
                        HowMuchCloserToGoal = item.GoalItemPrice - noBuys,
                        CreatedUTC = item.CreatedUTC
                    };
                    goals.Add(thing);
                }

                return goals;
            }
        }

        public GoalItemDetail GetGoalItemByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .GoalItems
                        .Single(e => e.GoalItemID == id && e.UserID == _userID);

                var noBuys =
                    ctx
                        .NoBuys
                        .Where(e => e.GoalItemID == entity.GoalItemID).ToList();
                decimal noBuySum = 0m;
                if(noBuys.Count != 0)
                {
                    noBuySum = noBuys.Sum(e => e.ItemPrice);
                }

                return
                    new GoalItemDetail
                    {
                        GoalItemID = entity.GoalItemID,
                        GoalItemName = entity.GoalItemName,
                        GoalItemPrice = entity.GoalItemPrice,
                        GoalItemLocation = entity.GoalItemLocation,
                        TotalSavedFromNoBuys = noBuySum,
                        HowMuchCloserToGoal = (entity.GoalItemPrice - noBuySum <= 0) ? 0 : entity.GoalItemPrice - noBuySum,
                        CreatedUTC = entity.CreatedUTC,
                        ModifiedUTC = entity.ModifiedUTC
                    };
            }
        }

        public bool UpdateGoalItem(GoalItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .GoalItems
                        .Single(e => e.GoalItemID == model.GoalItemID && e.UserID == _userID);

                entity.GoalItemName = model.GoalItemName;
                entity.GoalItemPrice = model.GoalItemPrice;
                entity.GoalItemLocation = model.GoalItemLocation;
                entity.ModifiedUTC = DateTimeOffset.UtcNow;


                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteGoalItem(int goalItemID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .GoalItems
                        .Single(e => e.GoalItemID == goalItemID && e.UserID == _userID);
                var noBuys = ctx.NoBuys.Where(e => e.GoalItemID == goalItemID).ToList();
                foreach(var item in noBuys)
                {
                    item.GoalItemID = null;
                }

                var monthlyFinances = ctx.MonthlyFinances.Where(e => e.GoalItemID == goalItemID).ToList();
                foreach (var item in monthlyFinances)
                {
                    item.GoalItemID = null;
                }

                ctx.GoalItems.Remove(entity);

                var num = ctx.SaveChanges() == 1 + noBuys.Count + monthlyFinances.Count;
                return num;
            }
        }
    }
}
