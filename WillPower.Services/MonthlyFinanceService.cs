using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillPower.Data;
using WillPower.Models;

namespace WillPower.Services
{
    public class MonthlyFinanceService
    {
        private readonly Guid _userID;

        public MonthlyFinanceService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateMonthlyFinance(MonthlyFinanceCreate model)
        {
            var entity =
                new MonthlyFinance()
                {
                    UserID = _userID,
                    MonthlyFinanceID = model.MonthlyFinanceID,
                    Month = model.Month,
                    Year = model.Year,
                    MonthlyTakeHome = model.MonthlyTakeHome,
                    CostOfBills = model.CostOfBills,
                    GoalItemID = model.GoalItemID,
                    CreatedUTC = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.MonthlyFinances.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MonthlyFinanceListItem> GetMonthlyFinance()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .MonthlyFinances
                        .Where(e => e.UserID == _userID).ToList();
                List<MonthlyFinanceListItem> saved = new List<MonthlyFinanceListItem>();
                foreach (var item in query)
                {
                    var noBuys = ctx.NoBuys.Where(e => e.CreatedUTC.Month == (int)item.Month).ToList().Sum(e => e.ItemPrice);
                    var thing = new MonthlyFinanceListItem
                    {
                        MonthlyFinanceID = item.MonthlyFinanceID,
                        Month = item.Month,
                        Year = item.Year,
                        MonthlyTakeHome = item.MonthlyTakeHome,
                        CostOfBills = item.CostOfBills,
                        MoneyLeftToSpend = item.MonthlyTakeHome - item.CostOfBills,
                        GoalItemID = item.GoalItemID,
                    };
                    saved.Add(thing);

                }

                return saved;
            }
        }

        public MonthlyFinanceDetail GetMonthlyFinanceByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .MonthlyFinances
                        .Single(e => e.MonthlyFinanceID == id && e.UserID == _userID);

                var noBuys =
                    ctx
                    .NoBuys
                    .Where(e => e.CreatedUTC.Month == (int)entity.Month && e.CreatedUTC.Year == entity.Year && e.UserID == _userID).ToList()
                    .Sum(e => e.ItemPrice);


                return
                    new MonthlyFinanceDetail
                    {
                        MonthlyFinanceID = entity.MonthlyFinanceID,
                        Month = entity.Month,
                        Year = entity.Year,
                        MonthlyTakeHome = entity.MonthlyTakeHome,
                        CostOfBills = entity.CostOfBills,
                        GoalItemID = entity.GoalItemID,
                        MoneyLeftToSpend = entity.MonthlyTakeHome - entity.CostOfBills,
                        TotalSavedFromNoBuysThisMonth = noBuys,
                        CreatedUTC = entity.CreatedUTC,
                        ModifiedUTC = entity.ModifiedUTC
                    };

            }
        }

        public bool UpdateMonthlyFinance(MonthlyFinanceEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .MonthlyFinances
                        .Single(e => e.MonthlyFinanceID == model.MonthlyFinanceID && e.UserID == _userID);

                entity.MonthlyTakeHome = model.MonthlyTakeHome;
                entity.Month = model.Month;
                entity.Year = model.Year;
                entity.CostOfBills = model.CostOfBills;
                entity.GoalItemID = model.GoalItemID;
                entity.ModifiedUTC = DateTimeOffset.Now;


                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMonthlyFinance(int monthlyFinanceID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .MonthlyFinances
                        .Single(e => e.MonthlyFinanceID == monthlyFinanceID && e.UserID == _userID);

                ctx.MonthlyFinances.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
