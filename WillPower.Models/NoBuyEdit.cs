using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillPower.Models
{
    public class NoBuyEdit
    {
        public int ItemID { get; set; }
        [Display(Name = "Name of item")]
        public string ItemName { get; set; }
        [Display(Name = "How much was it?")]
        public decimal ItemPrice { get; set; }
        [Display(Name = "Where can you buy it?")]
        public string ItemLocation { get; set; }
        [Display(Name = "What goal is this going towards?")]
        public int? GoalItemID { get; set; }
    }
}
