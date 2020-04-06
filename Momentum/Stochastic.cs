using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicalIndicators.Momentum
{
    /// <summary>
    /// A momentum indicator comparing a particular closing price of a security to a range of its prices over a certain period of time.
    /// </summary>
    public class Stochastic
    {
        /// <summary>
        /// A momentum indicator that shows the location of the close relative to the high-low range over a set number of periods.
        /// </summary>
        /// <param name="highPrices">High price of the asset.</param>
        /// <param name="lowPrices">Low price of the asset.</param>
        /// <param name="closePrices">Close price of the asset.</param>
        public Stochastic(decimal [] highPrices, decimal [] lowPrices,decimal[] closePrices)
        {
            SlowArray = GetStochastic(highPrices, lowPrices, closePrices);
            Slow = SlowArray[0];
            FastArray = GetFastStochastic(SlowArray);
            Fast = FastArray[0];

        }
        /// <summary>
        /// Array of the "Fast" %D stochastic data points.
        /// </summary>
        public decimal [] FastArray { get; }
        /// <summary>
        /// Array of the "Slow" %K stochastic data points.
        /// </summary>
        public decimal [] SlowArray { get; }
        /// <summary>
        /// The "Fast" Stochastic (%D) is the 3 period moving average of %K
        /// </summary>
        public decimal Fast { get;}
        /// <summary>
        /// The "Slow" Stochastic (%K) 
        /// </summary>
        public decimal Slow { get; }

        private decimal [] GetStochastic(decimal [] highPrices, decimal [] lowPrices, decimal [] closePrices)
        {
            // %K = (C-L14/H14-L14)*100
            // define range
            var end = closePrices.Length - 1;
            var start = end - 14;
            decimal[] stochastic = new decimal[closePrices.Length-14];
            for (int i = stochastic.Length-1; i >= 0; i--)
            {
                var high = highPrices[start..end].Max();
                var low = lowPrices[start..end].Min();
                stochastic[i] = ((closePrices[i] - low) / (high - low)) * 100;
                end--;
                start--;
            }
            return stochastic;
        }
        private decimal [] GetFastStochastic(decimal[] stochasticSlow)
        {
            decimal[] fastStochastic = new decimal[stochasticSlow.Length - 2];

            var end = stochasticSlow.Length - 1;
            var start = end - 3;
            for (int i = fastStochastic.Length-2; i >=0; i--)
            {
                fastStochastic[i] = stochasticSlow[start..end].Average();
                end--;
                start--;
            }
            return fastStochastic;
        }
    }
}
