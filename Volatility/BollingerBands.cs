using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnicalIndicators.Trend;

namespace Technical_Indicators.Volatility
{
    /// <summary>
    /// Bollinger Bands® are a technical analysis tool developed by John Bollinger.
    /// </summary>
    public class BollingerBands
    {
        /// <summary>
        /// A set of lines plotted two standard deviations (positively and negatively) away from a simple moving average (SMA) of the Asset's price
        /// </summary>
        /// <param name="closePrices"></param>
        public BollingerBands(decimal[] closePrices)
        {
            ClosePrices = closePrices;
            MiddleBandArray = new MovingAverage(ClosePrices).GetAverage(20);
            StandDevArray = GetBands();
            UpperBandArray = GetUpper();
            LowerBandArray = GetLower();
            UpperBand = UpperBandArray[0];
            MiddleBand = MiddleBandArray[0];
            LowerBand = LowerBandArray[0];
        }
        public decimal[] UpperBandArray { get; }
        public decimal[] MiddleBandArray { get; }
        public decimal[] LowerBandArray { get; }
        public decimal UpperBand { get; }
        public decimal MiddleBand { get; }
        public decimal LowerBand { get; }
        /// <summary>
        /// Array of Close Prices
        /// </summary>
        private decimal[] ClosePrices { get; }
        private decimal[] StandDevArray { get; }

        // TODO: https://www.investopedia.com/terms/b/bollingerbands.asp

        private decimal[] GetUpper()
        {
            decimal[] upper = new decimal[MiddleBandArray.Length];
            for (int i = 0; i < MiddleBandArray.Length; i++)
            {
                upper[i] = MiddleBandArray[i] + (StandDevArray[i] * 2);
            }
            return upper;
        }
        private decimal[] GetLower()
        {
            decimal[] lower = new decimal[MiddleBandArray.Length];
            for (int i = 0; i < MiddleBandArray.Length; i++)
            {
                lower[i] = MiddleBandArray[i] - (StandDevArray[i] * 2);
            }
            return lower;
        }
        private decimal[] GetBands()
        {
            decimal[] bands = new decimal[MiddleBandArray.Length];
            var end = ClosePrices.Length - 1;
            var start = end - 20;
            for (int i = start; i >= 0; i--)
            {
                bands[i] = StandardDeviation(ClosePrices[start..end]);
                start--;
                end--;
            }
            return bands;
        }
        private decimal StandardDeviation(decimal[] vs)
        {
            double[] vs1 = new double[vs.Length];
            var mean = vs.Sum() / vs.Length;

            for (int i = 0; i < vs.Length; i++)
            {
                vs1[i] = Math.Pow((double)Math.Abs(mean - vs[i]), 2);
            }
            return Convert.ToDecimal(Math.Sqrt(vs1.Sum() / vs1.Length));
        }

    }
}
