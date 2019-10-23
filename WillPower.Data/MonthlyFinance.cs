using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillPower.Data
{
    public enum Months { January=1, February, March, April, May, June, July, August, September, October, November, December}
    public class MonthlyFinance
    {
        [Key]
        public int MonthlyFinanceID { get; set; }
        [Required]
        [Display(Name = "Your ID")]
        public Guid UserID { get; set; }
        [Required]
        [Display(Name="What month is it?")]
        public Months Month { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        [Display(Name = "How much did you make this month??")]
        public decimal MonthlyTakeHome { get; set; }
        [Required]
        [Display(Name = "What is the total cost of your bills this month?")]
        public decimal CostOfBills { get; set; }
        [ForeignKey("GoalItem")]
        public int? GoalItemID { get; set; }
        public virtual GoalItem GoalItem { get; set; }
        public DateTimeOffset CreatedUTC { get; set; }
        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}
