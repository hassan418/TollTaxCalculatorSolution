using System;
using System.Collections.Generic;
using System.Text;

namespace TTC.Model.Models
{
    public class TollSurge : BaseEntity
    {
        public decimal Value { get; set; }
        public string SurgeChargingDays { get; set; }
        public IEnumerable<TollCharge> TollCharges { get; set; }
    }
}
