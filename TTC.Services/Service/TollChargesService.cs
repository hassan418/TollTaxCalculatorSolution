using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTC.Model.Enums;
using TTC.Model.Models;
using TTC.Service.Context;

namespace TTC.Service.Service
{
    public class TollChargesService : ITollChargesService
    {
        private readonly TollTaxContext _context;
        public TollChargesService(TollTaxContext context)
        {
            _context = context;
        }
        public async Task<TollCharge> EntryInterchange(TollChargesVM tollCharges)
        {
            TollCharge charges = new TollCharge()
            {
                EntryInterchangeName = tollCharges.InterchangeName,
                Number = tollCharges.Number,
                EntryTime = tollCharges.Time,
                BaseRate = (decimal)BaseRates.Car,
                Created = DateTime.Now
            };
            await _context.TollCharges.AddAsync(charges);
            await _context.SaveChangesAsync();
            return charges;
        }

        public async Task<TollCharge> ExitInterchange(TollChargesVM tollCharges)
        {
            decimal Totaldistance = 0;
            decimal PerKmRate = (decimal)0.2;
            int? DiscountId = null;
            int? SurgeId = null;
            var gettoolcharges = _context.TollCharges.Where(c => c.Number == tollCharges.Number).OrderByDescending(c => c.Id).FirstOrDefault();
            gettoolcharges.ExitInterchangeName = tollCharges.InterchangeName;
            gettoolcharges.ExitTime = tollCharges.Time;
            Totaldistance = GetDistanceCover(gettoolcharges.EntryInterchangeName, gettoolcharges.ExitInterchangeName);
            decimal SurgeRate = GetSurgeRate(gettoolcharges.EntryTime, ref SurgeId);
            gettoolcharges.DistanceCharges = Totaldistance * PerKmRate;
            gettoolcharges.Surge = CalculateSurgeCharges(gettoolcharges.DistanceCharges, SurgeRate);
            gettoolcharges.Discount = CalculateDiscount(gettoolcharges.DistanceCharges, gettoolcharges.Surge, gettoolcharges.EntryTime, gettoolcharges.Number, ref DiscountId);
            gettoolcharges.SurgeId = SurgeId;
            gettoolcharges.DiscountId = DiscountId;
            gettoolcharges.Updated = DateTime.Now;
            _context.TollCharges.Update(gettoolcharges);
            await _context.SaveChangesAsync();
            return gettoolcharges;
        }

        public decimal GetDistanceCover(string StartingPoint, string EndingPoint)
        {
            var StartingPointDistance = _context.InterchangePoints.Where(c => c.Name == StartingPoint).FirstOrDefault().Distance;
            var EndingPointDistance = _context.InterchangePoints.Where(c => c.Name == EndingPoint).FirstOrDefault().Distance;
            var value = StartingPointDistance - EndingPointDistance;
            return ConvertToPositive(value);
        }

        public decimal CalculateSurgeCharges(decimal DistanceCharges, decimal SurgeRate)
        {
            decimal GetActualCost = DistanceCharges;
            decimal TotalCost = SurgeRate > 0 ? GetActualCost * SurgeRate : GetActualCost;
            return SurgeRate > 0 ? TotalCost - GetActualCost : 0;
        }

        public decimal CalculateDiscount(decimal DistanceCharges, decimal SurgeCharges, DateTime dateTime, string Number, ref int? DiscountId)
        {
            decimal discount = 0;
            var specialDay = dateTime.Day + "-" + dateTime.ToString("MMMM");
            var SpecialDiscount = _context.TollDiscounts.Where(c => c.DisountDays == specialDay && c.Priority == 1).FirstOrDefault();
            if (SpecialDiscount != null)
            {
                discount = ((DistanceCharges + SurgeCharges) * SpecialDiscount.DiscountPercentage) / 100;
                DiscountId = SpecialDiscount.Id;
            }
            else
            {
                var normalDay = dateTime.ToString("dddd");
                var numbertype = CheckEvenOdd(Number);
                var NormalDiscount = _context.TollDiscounts.Where(c => c.DisountDays == normalDay && c.DiscountBasis == numbertype && c.Priority > 1).FirstOrDefault();
                if (NormalDiscount != null)
                {
                    discount = ((DistanceCharges + SurgeCharges) * NormalDiscount.DiscountPercentage) / 100;
                    DiscountId = NormalDiscount.Id;
                }
                else
                {
                    DiscountId = null;
                }
            }

            return discount;
        }

        public decimal GetSurgeRate(DateTime dateTime, ref int? SurgeId)
        {
            decimal SurgePercentage = 0;
            string CheckDay = dateTime.DayOfWeek.ToString();
            var checksurge = _context.TollSurges.Where(c => c.SurgeChargingDays.ToLower() == CheckDay.ToLower()).FirstOrDefault();
            if (checksurge != null)
            {
                SurgeId = checksurge.Id;
                SurgePercentage = checksurge.Value;
            }
            else
            {
                SurgeId = null;
            }
            return SurgePercentage;
        }

        public decimal ConvertToPositive(decimal number)
        {
            return number > 0 ? number : Math.Abs(number);
        }

        public string CheckEvenOdd(string number)
        {
            string numbertype = "";
            string[] splitnumber = number.Split('-');
            var digits = Convert.ToInt32(splitnumber[splitnumber.Length - 1]);
            if (digits % 2 == 0)
            {
                numbertype = DiscountBasedOn.Even.ToString();
            }
            else
            {
                numbertype = DiscountBasedOn.Odd.ToString();
            }
            return numbertype;
        }
    }
}
