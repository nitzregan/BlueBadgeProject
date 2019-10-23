using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillPower.Models
{
    public class NoBuyDetail
    {
        public int ItemID { get; set; }
        [Display(Name = "Item")]
        public string ItemName { get; set; }
        [Display(Name = "Price")]
        public decimal ItemPrice { get; set; }
        [Display(Name = "Purchase Location")]
        public string ItemLocation { get; set; }
        [Display(Name = "Designated Goal")]
        public int? GoalItemID { get; set; }
        [Display(Name = "Total Saved")]
        public decimal TotalSavedFromNoBuysThisMonth { get; set; }
        [Display(Name = "Date Added")]
        public DateTimeOffset CreatedUTC { get; set; }
        [Display(Name = "Date Modified")]
        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}
