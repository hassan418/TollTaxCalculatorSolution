using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TTC.Model.Models;
using TTC.Models.Models;

namespace TTC.Service.Context
{
    public class TollTaxContext : DbContext
    {
        public TollTaxContext()
        {
        }

        public TollTaxContext(DbContextOptions<TollTaxContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TollSurge> TollSurges { get; set; }
        public virtual DbSet<TollDiscount> TollDiscounts { get; set; }
        public virtual DbSet<TollCharge> TollCharges { get; set; }
        public virtual DbSet<InterchangePoint> InterchangePoints { get; set; }
    }
}
