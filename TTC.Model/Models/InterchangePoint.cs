using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTC.Model.Models;

namespace TTC.Models.Models
{
    public class InterchangePoint : BaseEntity
    {
        public string Name { get; set; }
        public decimal Distance { get; set; }
    }
}
