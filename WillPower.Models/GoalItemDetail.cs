using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillPower.Models
{
    public class GoalItemDetail
    {
        public int GoalItemID { get; set; }
        [Display(Name = "Name of item")]
        public string GoalItemName { get; set; }
        [Display(Name = "Price")]
        public decimal GoalItemPrice { get; set; }
        [Display(Name = "Purchase Location")]
        public string GoalItemLocation { get; set; }
        public decimal TotalSavedFromNoBuys { get; set; }
        [Display(Name = "How Close to Goal")]
        public decimal HowMuchCloserToGoal { get; set; }
        [Display(Name = "Date Added")]
        public DateTimeOffset CreatedUTC { get; set; }
        [Display(Name = "Date Modified")]
        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}
