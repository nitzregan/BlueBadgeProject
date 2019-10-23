using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillPower.Data;
using WillPower.Models;

namespace WillPower.Services
{
    public class NoBuyService
    {
        private readonly Guid _userID;

        public NoBuyService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateNoBuy(NoBuyCreate model)
        {
            var entity =
                new NoBuy()
                {
                    UserID = _userID,
                    ItemName = model.ItemName,
                    ItemPrice = model.ItemPrice,
                    ItemLocation = model.ItemLocation,
                    GoalItemID = model.GoalItemID,
                    CreatedUTC = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.NoBuys.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<NoBuyListItem> GetNoBuys()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .NoBuys
                        .Where(e => e.UserID == _userID)
                        .Select(
                            e =>
                                new NoBuyListItem
                                {
                                    ItemID = e.ItemID,
                                    ItemName = e.ItemName,
                                    ItemPrice = e.ItemPrice,
                                    ItemLocation = e.ItemLocation,
                                    GoalItemID = e.GoalItemID,
                                    CreatedUTC = e.CreatedUTC
                                }
                        );

                return query.ToArray();
            }
        }

        public NoBuyDetail GetNoBuyByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .NoBuys
                        .Single(e => e.ItemID == id && e.UserID == _userID);

                var noBuys =
                    ctx
                    .NoBuys
                    .Sum(e => e.ItemPrice);
                return
                    new NoBuyDetail
                    {
                        ItemID = entity.ItemID,
                        ItemName = entity.ItemName,
                        ItemPrice = entity.ItemPrice,
                        ItemLocation = entity.ItemLocation,
                        GoalItemID = entity.GoalItemID,
                        TotalSavedFromNoBuysThisMonth = noBuys,
                        CreatedUTC = entity.CreatedUTC,
                        ModifiedUTC = entity.ModifiedUTC
                    };
            }
        }

        public bool UpdateNoBuy(NoBuyEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .NoBuys
                        .Single(e => e.ItemID == model.ItemID && e.UserID == _userID);

                entity.ItemName = model.ItemName;
                entity.ItemPrice = model.ItemPrice;
                entity.ItemLocation = model.ItemLocation;
                entity.GoalItemID = model.GoalItemID;
                entity.ModifiedUTC = DateTimeOffset.UtcNow;


                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNoBuy(int itemID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .NoBuys
                        .Single(e => e.ItemID == itemID && e.UserID == _userID);

                ctx.NoBuys.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
