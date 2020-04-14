using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicalIndicators.Volatility
{   /// <summary>
    /// The average true range (ATR) is a technical analysis indicator that measures market volatility by decomposing the entire range of an asset price for that period.
    /// </summary>
    public class AverageTrueRange
    {
        /// <summary>
        /// The average true range (ATR) is a technical analysis indicator that measures market volatility by decomposing the entire range of an asset price for that period.
        /// </summary>
        /// <param name="highPrices">High price of the asset.</param>
        /// <param name="lowPrices">Low price of the asset.</param>
        /// <param name="closePrices">Close price of the asset.</param>
        public AverageTrueRange(decimal[] highPrices, decimal[] lowPrices, decimal[] closePrices)
        {
            Period = 14;
            ATRArray = Smooth(GetTR(highPrices, lowPrices, closePrices));
            ATR = ATRArray[0];
        }
        private int Period { get; set; }
        /// <summary>
        /// Array of Average True Range data points.
        /// </summary>
        public decimal[] ATRArray { get; set; }
        /// <summary>
        /// Most recent Average True Range data point.
        /// </summary>
        public decimal ATR { get; set; }

        private decimal[] GetTR(decimal[] highPrices, decimal[] lowPrices, decimal[] closePrices)
        {
            int min = new int[] { highPrices.Length, lowPrices.Length, closePrices.Length }.Min();
            decimal[] TR = new decimal[min - 1];
            for (int i = min - 2; i >= 0; i--)
            {
                TR[i] = new decimal[] { Math.Abs(highPrices[i] - lowPrices[i]), Math.Abs(highPrices[i] - closePrices[i + 1]), Math.Abs(lowPrices[i] - closePrices[i + 1]) }.Max();
            }

            return TR;

        }

        private decimal[] Smooth(decimal[] vs)
        {
            int startIndex = vs.Length - Period;
            decimal[] smooth = new decimal[vs.Length - Period + 1];
            smooth[^1] = vs[startIndex..^0].Average();
            startIndex--;
            for (int i = smooth.Length - 2; i >= 0; i--)
            {
                smooth[i] = ((smooth[i + 1] * (Period - 1)) + vs[startIndex]) / Period;
                startIndex--;
            }
            return smooth;
        }

    }


}
