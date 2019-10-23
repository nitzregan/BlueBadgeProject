using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillPower.Models
{
    public class GoalItemCreate
    {
        [Display(Name = "Name of item")]
        public string GoalItemName { get; set; }
        [Display(Name = "How much is it?")]
        public decimal GoalItemPrice { get; set; }
        [Display(Name = "Where can you buy it?")]
        public string GoalItemLocation { get; set; }
        public DateTimeOffset CreatedUTC { get; set; }
    }
}
