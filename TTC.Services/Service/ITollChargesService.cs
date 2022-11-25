using System;
using System.Threading.Tasks;
using TTC.Model.Models;

namespace TTC.Service.Service
{
    public interface ITollChargesService
    {
        decimal CalculateDiscount(decimal DistanceCharges, decimal SurgeCharges, DateTime dateTime, string Number, ref int? DiscountId);
        decimal CalculateSurgeCharges(decimal DistanceCharges, decimal SurgeRate);
        string CheckEvenOdd(string number);
        decimal ConvertToPositive(decimal number);
        Task<TollCharge> EntryInterchange(TollChargesVM tollCharges);
        Task<TollCharge> ExitInterchange(TollChargesVM tollCharges);
        decimal GetDistanceCover(string StartingPoint, string EndingPoint);
        decimal GetSurgeRate(DateTime dateTime, ref int? SurgeId);
    }
}