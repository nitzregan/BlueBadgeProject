using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillPower.Data;

namespace WillPower.Models
{
    public class MonthlyFinanceListItem
    {
        public int MonthlyFinanceID { get; set; }
        public Months Month { get; set; }
        public int Year { get; set; }
        [Display(Name = "Income")]
        public decimal MonthlyTakeHome { get; set; }
        [Display(Name = "Bills")]
        public decimal CostOfBills { get; set; }
        [Display(Name = "Money Left")]
        public decimal MoneyLeftToSpend { get; set; }
        public decimal TotalSavedFromNoBuysThisMonth { get; set; }
        public int? GoalItemID { get; set; }
        public DateTimeOffset CreatedUTC { get; set; }
        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}
