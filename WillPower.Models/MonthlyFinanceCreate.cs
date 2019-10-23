using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillPower.Data;

namespace WillPower.Models
{
    public class MonthlyFinanceCreate
    {
        public int MonthlyFinanceID { get; set; }
        public Months Month { get; set; }
        public int Year { get; set; }
        [Display(Name = "How much did you make this month??")]
        public decimal MonthlyTakeHome { get; set; }
        [Display(Name = "What is the total cost of your bills this month?")]
        public decimal CostOfBills { get; set; }
        [Display(Name = "What are you saving for this month?")]
        public int? GoalItemID { get; set; }

    }
}
