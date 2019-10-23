using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillPower.Data
{
    public class NoBuy
    {
        [Key]
        public int ItemID { get; set; }
        [Required]
        [Display(Name = "Your ID")]
        public Guid UserID { get; set; }
        [Required]
        [Display(Name = "Name of item")]
        public string ItemName { get; set; }
        [Required]
        [Display(Name = "How much was it?")]
        public decimal ItemPrice { get; set; }
        [Display(Name = "Where can you buy it?")]
        public string ItemLocation { get; set; }
        [ForeignKey("GoalItem")]
        public int? GoalItemID { get; set; }
        public virtual GoalItem GoalItem { get; set; }
        public DateTimeOffset CreatedUTC { get; set;}
        public DateTimeOffset? ModifiedUTC { get; set; }

    }
}
