
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
            TwoHundredDay = Average(200, closePrices);
            OneHundredDay = Average(100, closePrices);
            FiftyDay = Average(50, closePrices);
            GoldenCross = TwoHundredDay < FiftyDay;
            DeathCross = FiftyDay < TwoHundredDay;
        }
        /// <summary>
        /// Finds the Simple Moving Average (SMA) for the closing prices
        /// </summary>
        /// <param name="closePrices">Prices from the close of a given period</param>

        public decimal TwoHundredDay { get; }
        public decimal OneHundredDay { get; }
        public decimal FiftyDay { get; }
        public bool GoldenCross { get; }
        public bool DeathCross { get; }

        /// <summary>
        /// Method assumes prices are decending (newest to oldest)
        /// </summary>
        /// <param name="period">Number of time periods being averaged</param>
        /// <param name="closePrices">Prices from the close of a given period</param>
        /// <returns></returns>
        private decimal Average(int period, decimal[] closePrices)
        {
            decimal sum = 0;
            for (int i = 0; i < period; i++)
            {
                sum += closePrices[i];
            }
            return sum / (decimal)period;
        }
    }
}
