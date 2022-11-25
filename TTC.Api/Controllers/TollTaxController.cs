using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTC.Model.Models;
using TTC.Service.Service;

namespace TTC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TollTaxController : ControllerBase
    {
        private ITollChargesService _tollChargesService;
        public TollTaxController(ITollChargesService tollChargesService)
        {
            _tollChargesService = tollChargesService;
        }

        // POST: api/TollTax
        [HttpPost]
        public async Task<TollCharge> Post([FromBody] TollChargesVM tollCharges)
        {
            if (tollCharges.InterchangeName == null)
            {
                throw new ArgumentNullException("tollCharges.InterchangeName");
            }
            if (tollCharges.Number == null)
            {
                throw new ArgumentNullException("tollCharges.Number");
            }
            if (tollCharges.Time == null)
            {
                throw new ArgumentNullException("tollCharges.Time");
            }
            return await (tollCharges.IsExit == false ? _tollChargesService.EntryInterchange(tollCharges) : _tollChargesService.ExitInterchange(tollCharges));
        }

    }
}
