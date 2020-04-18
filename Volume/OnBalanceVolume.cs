using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalIndicators.Volume
{
    /// <summary>
    /// On-balance volume (OBV) is a technical indicator of momentum, using volume changes to make price predictions.
    /// </summary>
    public class OnBalanceVolume
    {
        /// <summary>
        /// On-balance volume (OBV) is a technical indicator of momentum, using volume changes to make price predictions.
        /// </summary>
        /// <param name="volumes">Quantity of assets exchanged.</param>
        /// <param name="closePrices">Close price of the asset.</param>
        public OnBalanceVolume(long[] volumes, decimal[] closePrices)
        {
            new DataErrors().ValidData("OBV", volumes.Length, closePrices.Length);
            ClosePrices = closePrices;
            Volume = volumes;
            OBVArray = GetOBV();
            OBV = OBVArray[0];

        }
        /// <summary>
        /// Most recent On Balance Volume data point.
        /// </summary>
        public long OBV { get; set; }
        /// <summary>
        /// Array of On Balance Volume data points.
        /// </summary>
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
