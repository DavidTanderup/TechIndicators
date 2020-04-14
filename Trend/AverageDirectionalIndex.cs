using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnicalIndicators.Volatility;

namespace TechnicalIndicators.Trend
{
    /// <summary>
    /// Average Directional Index (ADX) is a technical analysis indicator used by some traders to determine the strength of a trend.
    /// </summary>
    public class AverageDirectionalIndex
    {
        private enum DX { Plus, Minus };
        /// <summary>
        /// Average Directional Index (ADX) is a technical analysis indicator used by some traders to determine the strength of a trend.
        /// </summary>
        /// <param name="highPrices">High price of the asset.</param>
        /// <param name="lowPrices">Low price of the asset.</param>
        /// <param name="closePrices">Close price of the asset.</param>
        public AverageDirectionalIndex(decimal[] highPrices, decimal[] lowPrices, decimal[] closePrices)
        {
            Period = 14;
            ATR = new AverageTrueRange(highPrices, lowPrices, closePrices).ATRArray;
            PlusDMIArray = GetDMI(ATR, GetSmoothDX(highPrices, lowPrices, DX.Plus));
            MinusDMIArray = GetDMI(ATR, GetSmoothDX(highPrices, lowPrices, DX.Minus));
            ADXArray = GetADX();
            ADX = ADXArray[0];
            PlusDMI = PlusDMIArray[0];
            MinusDMI = MinusDMIArray[0];
        }
        /// <summary>
        /// Array of ADX data points.
        /// </summary>
        public decimal[] ADXArray { get; }
        /// <summary>
        /// Most recent ADX data point.
        /// </summary>
        public decimal ADX { get; }
        /// <summary>
        /// Array of positive Directional Movement Indicator (+DMI) data points.
        /// </summary>
        public decimal[] PlusDMIArray { get; }
        /// <summary>
        /// Most recent positive Directional Movement Indicator (+DMI) data point.
        /// </summary>
        public decimal PlusDMI { get; }
        /// <summary>
        /// Array of negative Directional Movement Indicator (-DMI) data points.
        /// </summary>
        public decimal[] MinusDMIArray { get; }
        /// <summary>
        /// Most recent negative Directional Movement Indicator (-DMI) data point.
        /// </summary>
        public decimal MinusDMI { get; }
        private decimal[] ATR { get; }
        private int Period { get; }
        // TODO: Throw error if array lengths are not same.
        private decimal[] GetADX()
        {
            // DX
            decimal[] dX = GetDX(PlusDMIArray, MinusDMIArray);
            return Smooth(dX);
        }

        private decimal[] GetDX(decimal[] plusDMI, decimal[] minusDMI)
        {
            decimal[] dx = new decimal[plusDMI.Length];
            for (int i = 0; i < dx.Length; i++)
            {
                dx[i] = (Math.Abs(plusDMI[i] - minusDMI[i]) / (plusDMI[i] + minusDMI[i])) * 100;
            }
            return dx;
        }

        private decimal[] GetDMI(decimal[] atr, decimal[] smoothDX)
        {            decimal[] dmi = new decimal[atr.Length];
            for (int i = 0; i < dmi.Length; i++)
            {
                dmi[i] = (atr[i] / smoothDX[i]) * 100;
            }
            return dmi;
        }

        private decimal[] GetSmoothDX(decimal[] highPrices, decimal[] lowPrices, DX plusMinus)
        {
            decimal[] dx = new decimal[highPrices.Length - 1];

            if (plusMinus == DX.Plus)
            {
                for (int i = dx.Length - 1; i >= 0; i--)
                {
                    var high = highPrices[i] - highPrices[i + 1];
                    var low = lowPrices[i + 1] - lowPrices[i];
                    if (high > low && high > 0)
                    {
                        dx[i] = high;
                    }
                    else
                    {
                        dx[i] = 0;
                    }
                }
            }
            else
            {
                for (int i = dx.Length - 1; i >= 0; i--)
                {
                    var high = highPrices[i] - highPrices[i + 1];
                    var low = lowPrices[i + 1] - lowPrices[i];
                    if (low > high && low > 0)
                    {
                        dx[i] = low;
                    }
                    else
                    {
                        dx[i] = 0;
                    }
                }
            }
            return Smooth(dx);
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
