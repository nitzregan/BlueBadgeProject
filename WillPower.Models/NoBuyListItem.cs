using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillPower.Models
{
    public class NoBuyListItem
    {
        public int ItemID { get; set; }
        [Display(Name = "Item")]
        public string ItemName { get; set; }
        [Display(Name = "Price")]
        public decimal ItemPrice { get; set; }
        [Display(Name = "Where can you buy it?")]
        public string ItemLocation { get; set; }
        [Display(Name = "What goal is this going towards?")]
        public int? GoalItemID { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUTC { get; set; }
    }
}
