using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tff.Ransom.CS
{
    public class DollarProvider
    {
        public List<Dollar> GetDollars(Int32 numberOfDollars)
        {
            List<Dollar> dollars = new List<Dollar>();
            Dollar currentDollar = null;
            Random random = new Random();
            for (int i = 0; i < numberOfDollars; i++)
            {
                currentDollar = new Dollar();
                currentDollar.FederalReserveDistrict = random.Next(1, 13);
                currentDollar.Id = i;

                String serialNumber = String.Empty;
                for (int j = 0; j < 9; j++)
                {
                    serialNumber += random.Next(0, 9).ToString();
                }
                currentDollar.SerialNumber = serialNumber;
                currentDollar.SeriesDate = 2000 + random.Next(0, 10);
                dollars.Add(currentDollar);
            }
            return dollars;
        }
    }
}
