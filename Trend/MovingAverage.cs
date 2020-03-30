
using System.Linq;

namespace TechnicalIndicators.Trend
{
    public class MovingAverage
    {
        /// <summary>
        /// Finds the Simple Moving Average (SMA) for the closing prices
        /// </summary>
        /// <param name="closePrices">Prices from the close of a given period</param>
        public MovingAverage(decimal[] closePrices)
        {
            TwoHundredDayArray = GetAverage(200, closePrices);
            OneHundredDayArray = GetAverage(100, closePrices);
            FiftyDayArray = GetAverage(50, closePrices);
            TwoHundredDay = TwoHundredDayArray[0];
            OneHundredDay = OneHundredDayArray[0];
            FiftyDay = FiftyDayArray[0];
        }
        /// <summary>
        /// Decimal Array of Two Hundred Day Simple Moving Average
        /// </summary>
        public decimal[] TwoHundredDayArray { get; }
        /// <summary>
        /// Decimal Array of One Hundred Day Simple Moving Average
        /// </summary>
        public decimal[] OneHundredDayArray { get; }
        /// <summary>
        /// Decimal Array of Fifty Day Simple Moving Average
        /// </summary>
        public decimal[] FiftyDayArray { get; }
        /// <summary>
        /// Two Hundred Day Simple Moving Average
        /// </summary>
        public decimal TwoHundredDay { get; }
        /// <summary>
        /// One Hundred Day Simple Moving Average
        /// </summary>
        public decimal OneHundredDay { get; }
        /// <summary>
        /// Fifty Day Simple Moving Average
        /// </summary>
        public decimal FiftyDay { get; }


        /// <summary>
        /// Array of Averages for a given period of time. Note: Method assumes prices are decending (newest to oldest)
        /// </summary>
        /// <param name="period">Number of time periods being averaged</param>
        /// <param name="closePrices">Prices from the close of a given period</param>
        /// <returns>Decimal Array</returns>
        public decimal[] GetAverage(int period, decimal[] closePrices)
        {
            decimal[] vs = new decimal[closePrices.Length - period];
            var start = closePrices.Length - (period + 1);
            var end = closePrices.Length - 1;
            for (int i = start; i >= 0; i--)
            {
                vs[i] = closePrices[i..end].Sum() / period;
                end--;
            }
            return vs;
        }


    }
}
