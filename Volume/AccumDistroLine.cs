using System;


namespace TechnicalIndicators.Volume
{
    /// <summary>
    /// Volume-based indicator designed to measure the cumulative flow of money into and out of a asset.
    /// </summary>
    public class AccumDistroLine
    {
        /// <summary>
        /// Volume-based indicator designed to measure the cumulative flow of money into and out of a asset.
        /// </summary>
        /// <param name="highPrices">High price of the asset.</param>
        /// <param name="lowPrices">Low price of the asset.</param>
        /// <param name="closePrices">Close price of the asset.</param>
        /// <param name="volumes">Quantity of assets exchanged.</param>
        public AccumDistroLine(decimal[] highPrices, decimal[] lowPrices, decimal[] closePrices, long[] volumes)
        {
            ADLArray = GetADL(highPrices, lowPrices, closePrices, volumes);
            ADL = ADLArray[0];
        }
        /// <summary>
        /// Volume-based indicator designed to measure the cumulative flow of money into and out of a asset. Use this contructor to continue previous calculations.
        /// </summary>
        /// <param name="highPrices">High price of the asset.</param>
        /// <param name="lowPrices">Low price of the asset.</param>
        /// <param name="closePrices">Close price of the asset.</param>
        /// <param name="volumes">Quantity of assets exchanged.</param>
        /// <param name="previousADL">Most recent calculated ADL data point from a previous calculation.</param>
        public AccumDistroLine(decimal[] highPrices, decimal[] lowPrices, decimal[] closePrices, long[] volumes, long previousADL)
        {
            ADLArray = GetADL(highPrices, lowPrices, closePrices, volumes, previousADL);
            ADL = ADLArray[0];
        }
        /// <summary>
        /// Array of ADL data points.
        /// </summary>
        public long[] ADLArray { get; }
        /// <summary>
        /// Most recent ADL
        /// </summary>
        public long ADL { get; }
        /// <summary>
        /// Gets an array of Accumulation Distribution Line (ADL) points.
        /// </summary>
        /// <param name="highPrices">High price of the asset.</param>
        /// <param name="lowPrices">Low price of the asset.</param>
        /// <param name="closePrices">Close price of the asset.</param>
        /// <param name="volumes">Quantity of asset exchanged.</param>
        /// <returns>Long Array</returns>
        private long[] GetADL(decimal[] highPrices, decimal[] lowPrices, decimal[] closePrices, long[] volumes)
        {
            long[] adl = new long[closePrices.Length];
            // Money Flow Multiplier = ((Close-Low)-(High-Close))/(High-Low)
            var moneyFlowMultiplier = ((closePrices[^1] - lowPrices[^1]) - (highPrices[^1] - closePrices[^1])) / (highPrices[^1] - lowPrices[^1]);
            adl[^1] = Convert.ToInt64(moneyFlowMultiplier * volumes[^1]);
            for (int i = adl.Length - 2; i >= 0; i--)
            {
                moneyFlowMultiplier = ((closePrices[i] - lowPrices[i]) - (highPrices[i] - closePrices[i])) / (highPrices[i] - lowPrices[i]);
                // Money Flow Volume = Money Flow Multiplier x Volume for the Period
                var moneyFlow = moneyFlowMultiplier * volumes[i];
                // ADL = Previous ADL + Current Period's Money Flow Volume
                adl[i] = Convert.ToInt64(moneyFlow + adl[i + 1]);
            }
            return adl;
        }
        /// <summary>
        /// Gets an array of Accumulation Distribution Line (ADL) points. Provides the ability to continue a data series calculation.
        /// </summary>
        /// <param name="highPrices">High price of the asset.</param>
        /// <param name="lowPrices">Low price of the asset.</param>
        /// <param name="closePrices">Close price of the asset.</param>
        /// <param name="volumes">Quantity of asset exchanged.</param>
        /// <param name="previousADL">Most recent ADL data point from previous calculation.</param>
        /// <returns>Long Array</returns>
        public long [] GetADL(decimal[] highPrices, decimal[] lowPrices, decimal[] closePrices, long[] volumes, long previousADL)
        {
            
            long[] adl = new long[closePrices.Length + 1];
            adl[^1] = previousADL;
            for (int i = adl.Length - 2; i >= 0; i--)
            {
                var moneyFlowMultiplier = ((closePrices[i] - lowPrices[i]) - (highPrices[i] - closePrices[i])) / (highPrices[i] - lowPrices[i]);
                // Money Flow Volume = Money Flow Multiplier x Volume for the Period
                var moneyFlow = moneyFlowMultiplier * volumes[i];
                // ADL = Previous ADL + Current Period's Money Flow Volume
                adl[i] = Convert.ToInt64(moneyFlow + adl[i + 1]);
            }
            return adl;
        }
    }
}
