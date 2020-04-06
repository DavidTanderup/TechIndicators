using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalIndicators.Volume
{
    public class OnBalanceVolume
    {
        // TODO: Test and verify
        public OnBalanceVolume(long[] volume, decimal[] closePrices)
        {
            ClosePrices = closePrices;
            Volume = volume;
            OBVArray = GetOBV();
            OBV = OBVArray[0];

        }

        public long OBV { get; set; }
        public long[] OBVArray { get; set; }
        private decimal[] ClosePrices { get; set; }
        private long[] Volume { get; set; }


        private long[] GetOBV()
        {
            long[] history = new long[Volume.Length];
            long obv;
            long previousOBV = Volume[^1];
            history[Volume.Length - 1] = previousOBV;
            for (int i = Volume.Length - 2; i >= 0; i--)
            {
                if (ClosePrices[i] > ClosePrices[i + 1])
                {
                    obv = previousOBV + Volume[i];
                    history[i] = obv;
                    previousOBV = obv;
                }
                else if (ClosePrices[i] == ClosePrices[i + 1])
                {
                    obv = previousOBV + 0;
                    history[i] = obv;
                    previousOBV = obv;
                }
                else
                {
                    obv = previousOBV - Volume[i];
                    history[i] = obv;
                    previousOBV = obv;
                }
            }
            return history;
        }
    }
}
