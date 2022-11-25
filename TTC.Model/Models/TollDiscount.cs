using System;
using System.Collections.Generic;
using System.Text;

namespace TTC.Model.Models
{
    public class TollDiscount : BaseEntity
    {
        public decimal DiscountPercentage { get; set; }
        public string DiscountBasis { get; set; }
        public string DisountDays { get; set; }
        public int Priority { get; set; }
        public IEnumerable<TollCharge> TollCharges { get; set; }
    }
}
