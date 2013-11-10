using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tff.Ransom.CS
{
    public class Dollar
    {
        public Int32 Id { get; set; }
        public String SerialNumber { get; set; }
        public Int32 FederalReserveDistrict { get; set; }
        public Int32 SeriesDate { get; set; }
        public String Signature { get; set; }
    }
}
