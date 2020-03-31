
using System.Linq;

namespace TechnicalIndicators.Trend
{
    /// <summary>
    /// A moving average is a popular indicator in technical analysis that helps to filter out the “noise” from short-term price fluctuations.
    /// </summary>
    public class MovingAverage
    {
        /// <summary>
        /// Finds the Simple Moving Average (SMA) for the closing prices
        /// </summary>
        /// <param name="closePrices">Prices from the close of a given period</param>
        public MovingAverage(decimal[] closePrices)
        {
            ClosePrices = closePrices;
            TwoHundredDayArray = GetAverage(200);
            OneHundredDayArray = GetAverage(100);
            FiftyDayArray = GetAverage(50);
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
        /// Array of Close Prices
        /// </summary>
        public decimal[] ClosePrices { get; }

        /// <summary>
        /// Array of averages for a given period of time. Note: Method assumes prices are decending (newest to oldest)
        /// </summary>
        /// <param name="period">Number of time periods being averaged</param>
        /// <param name="ClosePrices">Prices from the close of a given period</param>
        /// <returns>Decimal Array</returns>
        public decimal[] GetAverage(int period)
        {
            decimal[] vs = new decimal[ClosePrices.Length - period];
            var start = ClosePrices.Length - (period + 1);
            var end = ClosePrices.Length - 1;
            for (int i = start; i >= 0; i--)
            {
                vs[i] = ClosePrices[i..end].Sum() / period;
                end--;
            }
            return vs;
        }


    }
}
