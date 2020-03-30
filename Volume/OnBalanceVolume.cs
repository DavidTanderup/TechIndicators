using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalIndicators.Volume
{
    public class OnBalanceVolume
    {
        public OnBalanceVolume(ulong[] volume, decimal[] closePrices)
        {
            ClosePrices = closePrices;
            Volume = volume;
            OBVArray = GetOBV();
            OBV = OBVArray[0];

        }

        public decimal OBV { get; set; }
        public decimal[] OBVArray { get; set; }
        private decimal[] ClosePrices { get; set; }
        private ulong[] Volume { get; set; }


        private decimal[] GetOBV()
        {
            decimal[] history = new decimal[Volume.Length];
            decimal obv;
            decimal previousOBV = Volume[^1];
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
