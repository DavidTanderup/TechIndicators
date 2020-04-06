using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicalIndicators.Momentum
{
    /// <summary>
    /// The relative strength index (RSI) is a momentum indicator that measures the magnitude of recent price changes 
    /// to determine overbought or oversold conditions in the price of the asset.
    /// </summary>
    public class RelativeStrengthIndex
    {    /// <summary>
         /// The relative strength index (RSI) is a momentum indicator that measures the magnitude of recent price changes 
         /// to determine overbought or oversold conditions in the price of the asset.
         /// </summary>
         /// <param name="closePrices">Array of market close prices for the asset.</param>
        public RelativeStrengthIndex(decimal[] closePrices)
        {
            ClosePrices = closePrices;
            RSIArray = GetRSI();
            RSI = RSIArray[0];
        }
        /// <summary>
        /// Array of close Prices
        /// </summary>
        private decimal[] ClosePrices { get; }
        public decimal[] RSIArray { get; }
        public decimal RSI { get; }


        public decimal[] GetRSI()
        {
            // track the changes in closing price
            var changeSize = ClosePrices.Length - 1;
            var rsiSize = ClosePrices.Length - 14;
            decimal[] upMove = new decimal[changeSize];
            decimal[] downMove = new decimal[changeSize];

            for (int i = ClosePrices.Length - 2; i >= 0; i--)
            {
                // if today price is greater than yesterday price => value in upMove && 0 to downMove
                if (ClosePrices[i] - ClosePrices[i + 1] > 0)
                {
                    upMove[i] = Math.Abs(ClosePrices[i] - ClosePrices[i + 1]);
                    downMove[i] = 0;
                }
                else
                {
                    upMove[i] = 0;
                    downMove[i] = Math.Abs(ClosePrices[i] - ClosePrices[i + 1]);
                }
            }

            // end of the range
            var end = upMove.Length - 1;
            // start of the range
            var start = end - 14;
            // create relative strength array
            decimal[] rsi = new decimal[rsiSize];
            for (int i = rsi.Length - 2; i >= 0; i--)
            {
                rsi[i] = 100 - 100 / (((upMove[start..end].Sum() / 14) / (downMove[start..end].Sum() / 14)) + 1);
                start--;
                end--;
            }

            return rsi;
        }
    }
}
