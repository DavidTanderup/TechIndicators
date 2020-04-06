using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalIndicators.Volume
{
    public class AccumDistroLine
    {
        public AccumDistroLine(decimal[] highPrices, decimal[] lowPrices, decimal[] closePrices, long[] volumes)
        {
            ADLArray = GetADL(highPrices, lowPrices, closePrices, volumes);
            ADL = ADLArray[0];
        }
        public long[] ADLArray { get; set; }
        public long ADL { get; set; }

        // Money Flow Multiplier = ((Close-Low)-(High-Close))/(High-Low)
        private long[] GetADL(decimal[] highPrices, decimal[] lowPrices, decimal[] closePrices, long[] volumes)
        {
            long[] adl = new long[closePrices.Length];
            var mfm = ((closePrices[^1] - lowPrices[^1]) - (highPrices[^1] - closePrices[^1])) / (highPrices[^1] - lowPrices[^1]);
            adl[^1] = Convert.ToInt64(mfm * volumes[^1]);
            for (int i = adl.Length - 2; i >= 0; i--)
            {
                mfm = ((closePrices[i] - lowPrices[i]) - (highPrices[i] - closePrices[i])) / (highPrices[i] - lowPrices[i]);
                // Money Flow Volume = Money Flow Multiplier x Volume for the Period
                var moneyFlow = mfm * volumes[i];
                // ADL = Previous ADL + Current Period's Money Flow Volume
                adl[i] = Convert.ToInt64(moneyFlow + adl[i + 1]);
            }
            return adl;
        }
    }
}
