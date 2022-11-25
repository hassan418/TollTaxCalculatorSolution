using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TTC.Model.Enums;
using TTC.Model.Models;
using TTC.Models.Models;
using TTC.Service.Context;

namespace TTC.Api
{
    public static class DbContextExtension
    {
        public static void EnsureSeeded(this TollTaxContext context)
        {
            SeedTollSurge(context);
            SeedTollDiscount(context);
            SeedInterchangePoint(context);
        }

        public static void SeedTollSurge(TollTaxContext context)
        {
            var tollsurge = context.TollSurges.ToList();
            if(tollsurge.Count < 1)
            {
                var TollSurgeList = new List<TollSurge>
                {
                    new TollSurge {SurgeChargingDays = DateTimeFormatInfo.CurrentInfo.GetDayName(DayOfWeek.Saturday),Value = (decimal)1.5,Created=DateTime.Now},
                    new TollSurge {SurgeChargingDays = DateTimeFormatInfo.CurrentInfo.GetDayName(DayOfWeek.Sunday),Value = (decimal)1.5,Created=DateTime.Now}
                };
                context.TollSurges.AddRange(TollSurgeList);
                context.SaveChanges();
            }
        }

        public static void SeedTollDiscount(TollTaxContext context)
        {
            var tolldiscount = context.TollDiscounts.ToList();
            if (tolldiscount.Count < 1)
            {
                var TollDiscountList = new List<TollDiscount>
                {
                    new TollDiscount {DiscountPercentage = 10, DiscountBasis = DiscountBasedOn.Even.ToString(), DisountDays = DateTimeFormatInfo.CurrentInfo.GetDayName(DayOfWeek.Monday),Priority = 2,Created=DateTime.Now},
                    new TollDiscount {DiscountPercentage = 10, DiscountBasis = DiscountBasedOn.Even.ToString(), DisountDays = DateTimeFormatInfo.CurrentInfo.GetDayName(DayOfWeek.Wednesday),Priority = 2,Created=DateTime.Now},
                    new TollDiscount {DiscountPercentage = 10, DiscountBasis = DiscountBasedOn.Odd.ToString(), DisountDays = DateTimeFormatInfo.CurrentInfo.GetDayName(DayOfWeek.Tuesday),Priority = 2,Created=DateTime.Now},
                    new TollDiscount {DiscountPercentage = 10, DiscountBasis = DiscountBasedOn.Odd.ToString(), DisountDays = DateTimeFormatInfo.CurrentInfo.GetDayName(DayOfWeek.Thursday),Priority = 2,Created=DateTime.Now},
                    new TollDiscount {DiscountPercentage = 50, DiscountBasis = null, DisountDays = 23+"-"+getmonthname(3),Priority = 1,Created=DateTime.Now},
                    new TollDiscount {DiscountPercentage = 50, DiscountBasis = null, DisountDays = 14+"-"+getmonthname(8),Priority = 1,Created=DateTime.Now},
                    new TollDiscount {DiscountPercentage = 50, DiscountBasis = null, DisountDays = 25+"-"+getmonthname(12),Priority = 1,Created=DateTime.Now},
                };
                context.TollDiscounts.AddRange(TollDiscountList);
                context.SaveChanges();
            }
        }

        public static void SeedInterchangePoint(TollTaxContext context)
        {
            var InterchangePointlist = context.InterchangePoints.ToList();
            if (InterchangePointlist.Count < 1)
            {
                var InterchangePointsList = new List<InterchangePoint>
                {
                    new InterchangePoint {Name = "Zero point", Distance = 0, Created=DateTime.Now},
                    new InterchangePoint {Name = "NS Interchange", Distance = 5, Created=DateTime.Now},
                    new InterchangePoint {Name = "Ph4 Interchange", Distance = 10, Created=DateTime.Now},
                    new InterchangePoint {Name = "Ferozpur Interchange", Distance = 17, Created=DateTime.Now},
                    new InterchangePoint {Name = "Lake City Interchange", Distance = 24, Created=DateTime.Now},
                    new InterchangePoint {Name = "Raiwand Interchange", Distance = 29, Created=DateTime.Now},
                    new InterchangePoint {Name = "Bahria Interchange", Distance = 34, Created=DateTime.Now},

                };
                context.InterchangePoints.AddRange(InterchangePointsList);
                context.SaveChanges();
            }
        }

        static string getmonthname(int month)
        {
            DateTime date = new DateTime(2020, month, 1);

            return date.ToString("MMMM");
        }

    }
}
