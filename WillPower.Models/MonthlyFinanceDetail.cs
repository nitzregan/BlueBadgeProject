using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillPower.Data;

namespace WillPower.Models
{
    public class MonthlyFinanceDetail
    {
        public int MonthlyFinanceID { get; set; }
        public Months Month { get; set; }
        public int Year { get; set; }
        [Display(Name = "Monthly Income")]
        public decimal MonthlyTakeHome { get; set; }
        [Display(Name = "Total Cost of Bills")]
        public decimal CostOfBills { get; set; }
        [Display(Name = "Designated Goal")]
        public int? GoalItemID { get; set; }
        [Display(Name = "Money Left After Bills")]
        public decimal MoneyLeftToSpend { get; set; }
        [Display(Name = "Total Saved")]
        public decimal TotalSavedFromNoBuysThisMonth { get; set; }
        [Display(Name = "Date Added")]
        public DateTimeOffset CreatedUTC { get; set; }
        [Display(Name = "Date Modified")]
        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}
