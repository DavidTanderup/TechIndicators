using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalIndicators.Volume
{
    public class OnBalanceVolume
    {
        public OnBalanceVolume(decimal[] volume, decimal[] closePrice)
        {
            OBV = GetOBV(volume, closePrice).OBV;
            OBVHistory = GetOBV(volume, closePrice).OBVHistory;
        }

        private OnBalanceVolume() { }
        public decimal OBV { get; set; }
        public decimal [] OBVHistory { get; set; }


        private OnBalanceVolume GetOBV(decimal[] volume, decimal[] closePrice)
        {
            decimal[] history = new decimal[volume.Length];
            decimal obv = 0m;
            decimal previousOBV = volume[volume.Length - 1];
            history[volume.Length-1]= previousOBV;
            for (int i = volume.Length - 2; i >= 0; i--)
            {
                if (closePrice[i] > closePrice[i + 1])
                {
                    obv = previousOBV + volume[i];
                    history[i] = obv;
                    previousOBV = obv;
                }
                else if (closePrice[i] == closePrice[i + 1])
                {
                    obv = previousOBV + 0;
                    history[i] = obv;
                    previousOBV = obv;
                }
                else
                {
                    obv = previousOBV - volume[i];
                    history[i] = obv;
                    previousOBV = obv;
                }
            }
            return new OnBalanceVolume() { OBVHistory = history, OBV = obv};
        }
    }
}
