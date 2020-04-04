using System;
using System.Linq;
using TechnicalIndicators.Trend;

namespace TechnicalIndicators.Volatility
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
            StandDevArray = GetStanDev();
            UpperBandArray = GetUpper();
            LowerBandArray = GetLower();
            UpperBand = UpperBandArray[0];
            MiddleBand = MiddleBandArray[0];
            LowerBand = LowerBandArray[0];
        }
        /// <summary>
        /// Decimal array of Upper Band data points.
        /// </summary>
        public decimal[] UpperBandArray { get; }
        /// <summary>
        /// Decimal array of 20 period SMA data points.
        /// </summary>
        public decimal[] MiddleBandArray { get; }
        /// <summary>
        /// Decimal array of Lower Band data points.
        /// </summary>
        public decimal[] LowerBandArray { get; }
        /// <summary>
        ///  Point plotted two standard deviations positively away from a simple moving average (SMA) of the Asset's price. 
        /// </summary>
        public decimal UpperBand { get; }
        /// <summary>
        /// Simple moving average (SMA) of the Asset's price for 20 periods.
        /// </summary>
        public decimal MiddleBand { get; }
        /// <summary>
        ///  Point plotted two standard deviations negatively away from a simple moving average (SMA) of the Asset's price.
        /// </summary>
        public decimal LowerBand { get; }
        /// <summary>
        /// Array of Close Prices
        /// </summary>
        private decimal[] ClosePrices { get; }
        /// <summary>
        /// Array of standard deviations points.
        /// </summary>
        private decimal[] StandDevArray { get; }

        /// <summary>
        /// Calculates the Upper Band. 
        /// </summary>
        /// <returns>Decimal Array</returns>
        private decimal[] GetUpper()
        {
            // set array size
            decimal[] upper = new decimal[MiddleBandArray.Length];
            for (int i = 0; i < MiddleBandArray.Length; i++)
            {
                // upper band = SMA + (StanDev*2)
                upper[i] = MiddleBandArray[i] + (StandDevArray[i] * 2);
            }
            return upper;
        }
        /// <summary>
        /// Calculates the Lower Band.
        /// </summary>
        /// <returns>Decimal Array</returns>
        private decimal[] GetLower()
        {
            // set array size
            decimal[] lower = new decimal[MiddleBandArray.Length];

            for (int i = 0; i < MiddleBandArray.Length; i++)
            {
                // lower band = SMA - (StanDev*2)
                lower[i] = MiddleBandArray[i] - (StandDevArray[i] * 2);
            }
            return lower;
        }
        /// <summary>
        /// Gets the calculations of standard deviations for the Close price data set.
        /// </summary>
        /// <returns></returns>
        private decimal[] GetStanDev()
        {
            decimal[] bands = new decimal[MiddleBandArray.Length];
            // set variable indexs
            var end = ClosePrices.Length - 1;
            var start = end - 20;
            // calculate standard deviation for each data set.
            for (int i = start; i >= 0; i--)
            {
                bands[i] = StandardDeviation(ClosePrices[start..end]);
                start--;
                end--;
            }
            return bands;
        }
        /// <summary>
        /// Calculates the standard deviation for a given data set.
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        private decimal StandardDeviation(decimal[] vs)
        {
            // create the return array
            double[] vs1 = new double[vs.Length];
            // find the mean
            var mean = vs.Sum() / vs.Length;
            // subtract the value from the mean and square the result.
            for (int i = 0; i < vs.Length; i++)
            {
                vs1[i] = Math.Pow((double)Math.Abs(mean - vs[i]), 2);
            }
            // get the squareroot of the mean for the squared results 
            return Convert.ToDecimal(Math.Sqrt(vs1.Sum() / vs1.Length));
        }

    }
}
