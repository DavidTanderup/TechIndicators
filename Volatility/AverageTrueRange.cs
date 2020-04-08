using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicalIndicators.Volatility
{
    public class AverageTrueRange
    {
        public AverageTrueRange(decimal[] highPrices, decimal[] lowPrices, decimal[] closePrices)
        {
            Period = 14;
            ATRArray = Smooth(GetTR(highPrices, lowPrices, closePrices));
            ATR = ATRArray[0];
        }
        public int Period { get; set; }
        public decimal[] ATRArray { get; set; }
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
