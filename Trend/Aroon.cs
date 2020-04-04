using System.Linq;

namespace TechnicalIndicators.Trend
{
    /// <summary>
    /// A technical indicator that is used to identify trend changes in the price of an asset, as well as the strength of that trend.
    /// </summary>
    public class Aroon
    {
        private enum EndPoint { Min, Max }
        /// <summary>
        /// A technical indicator that is used to identify trend changes in the price of an asset, as well as the strength of that trend.
        /// The default period of evaluation is 25. Use other constructor to customize period.
        /// </summary>
        /// <param name="highPrices">High price of the asset.</param>
        /// <param name="lowPrices">Low price of the asset.</param>
        public Aroon(decimal[] highPrices, decimal[] lowPrices)
        {
            Period = 25;
            AroonUpArray = GetAroon(highPrices, EndPoint.Max);
            AroonDownArray = GetAroon(lowPrices, EndPoint.Min);
            AroonUp = AroonUpArray[0];
            AroonDown = AroonDownArray[0];
        }
        /// <summary>
        /// A technical indicator that is used to identify trend changes in the price of an asset, as well as the strength of that trend.
        /// The default period of evaluation is 25. Use this constructor to customize period.
        /// </summary>
        /// <param name="highPrices">High price of the asset.</param>
        /// <param name="lowPrices">Low price of the asset.</param>
        public Aroon(decimal[] highPrices, decimal[] lowPrices, int customPeriod)
        {
            Period = customPeriod;
            AroonUpArray = GetAroon(highPrices, EndPoint.Max);
            AroonDownArray = GetAroon(lowPrices, EndPoint.Min);
            AroonUp = AroonUpArray[0];
            AroonDown = AroonDownArray[0];
        }
        /// <summary>
        /// Most recent data point for Aroon Up indicator.
        /// </summary>
        public decimal AroonUp { get; set; }
        /// <summary>
        /// Most recent data point for Aroon Down indicator.
        /// </summary>
        public decimal AroonDown { get; set; }
        /// <summary>
        /// Array of Aroon Up data points.
        /// </summary>
        public decimal[] AroonUpArray { get; set; }
        /// <summary>
        /// Array of Aroon Down data points.
        /// </summary>
        public decimal[] AroonDownArray { get; set; }
        /// <summary>
        /// Period of time being evaluated.
        /// </summary>
        private int Period { get; set; }
        /// <summary>
        /// Performs the Aroon calculations.
        /// </summary>
        /// <param name="prices">The prices: High or Low being evaluated.</param>
        /// <param name="endPoint">enum that specifies if the calculation is the High or Low</param>
        /// <returns></returns>
        private decimal[] GetAroon(decimal[] prices, EndPoint endPoint)
        {
            //define the range
            int end = prices.Length - 1;
            int start = end - (Period+1);
            // create the array
            decimal[] aroonArray = new decimal[prices.Length - Period];
            //
            for (int i = aroonArray.Length - 2; i >= 0; i--)
            {
                decimal highLow;
                if (endPoint == EndPoint.Max)
                {
                    highLow = prices[start..end].Max();
                }
                else
                {
                    highLow = prices[start..end].Min();
                }
                var count = GetCount(prices[start..end], highLow);
                aroonArray[i] = ((Period - count) / Period) * 100;
                start--;
                end--;
            }
            return aroonArray;
        }
        /// <summary>
        /// Finds the distance of the highest high or lowest low from the current value.
        /// </summary>
        /// <param name="aroonArray">Range being evaluated.</param>
        /// <param name="end">The highest high or lowest low</param>
        /// <returns></returns>
        private decimal GetCount(decimal[] aroonArray, decimal end)
        {
            decimal index = 0;
            for (int i = aroonArray.Length - 1; i >= 0; i--)
            {
                if (aroonArray[i] == end)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }
}
