using System;
using System.Collections.Generic;
using System.Text;

namespace TTC.Model.Models
{
    public class TollChargesVM
    {
        public string InterchangeName { get; set; }
        public string Number { get; set; }
        public DateTime Time { get; set; }
        public bool IsExit { get; set; }
    }
}
