using System;
using System.Collections.Generic;
using System.Text;

namespace TTC.Model.Models
{
    public class TollCharge : BaseEntity
    {
        public string EntryInterchangeName { get; set; }
        public string ExitInterchangeName { get; set; }
        public string  Number { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public decimal BaseRate { get; set; }
        public decimal DistanceCharges { get; set; }
        public decimal Discount { get; set; }
        public decimal Surge { get; set; }
        public int? DiscountId { get; set; }
        public TollDiscount TollDiscount { get; set; }
        public int? SurgeId { get; set; }
        public TollSurge TollSurge { get; set; }
    }
}
