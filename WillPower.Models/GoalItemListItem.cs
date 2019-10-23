using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillPower.Models
{
    public class GoalItemListItem
    {
        public int? GoalItemID { get; set; }

        [Display(Name = "Item")]
        public string GoalItemName { get; set; }
        [Display(Name = "Price")]
        public decimal GoalItemPrice { get; set; }
        [Display(Name = "Purchase Location")]
        public string GoalItemLocation { get; set; }
        public decimal TotalSavedFromNoBuys { get; set; }
        [Display(Name = "How Close to Goal")]
        public decimal HowMuchCloserToGoal { get; set; }
        public DateTimeOffset CreatedUTC { get; set; }
    }
}
