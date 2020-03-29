﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Technical_Indicators.Momentum
{
    /// <summary>
    /// Moving Average Convergence Divergence
    /// </summary>
    public class MACD
    {
        /// <summary>
        /// Constant period value of nine.
        /// </summary>
        private const int nine = 9;

        /// <summary>
        /// Constant period value of twelve.
        /// </summary>
        private const int twelve = 12;

        /// <summary>
        /// Constant period value of Twenty-Six.
        /// </summary>
        private const int twentySix = 26;

        /// <summary>
        /// Takes a decimal array of close prices and returns arrays of EMA 12, EMA 26, MACD, and Signal calculations.
        /// </summary>
        /// <param name="closePrices">Array of market close prices for the Asset.</param>
        public MACD(decimal[] closePrices)
        {
            if (closePrices.Length < 34)
            {
                Exception ex = new Exception($"{closePrices.Length} Close prices are an insufficient number. Min = 34. Please add {34 - ClosePrices.Length} more to perform a calculation.");
                throw ex;
            }
            ClosePrices = closePrices;
            MACDArray = GetMACD();
            SignalArray = GetSignal();
            EMA12Array = GetEMA(GetPriorEMA(twelve), twelve);
            EMA26Array = GetEMA(GetPriorEMA(twentySix), twentySix);
        }

        /// <summary>
        /// Returns single value EMA 12, EMA 26, MACD, and Signal calculations.
        /// </summary>
        /// <param name="close">Most recent Close price for the Asset.</param>
        /// <param name="ema12">Most recent EMA 12 calculation for the Asset.</param>
        /// <param name="ema26">Most recent EMA 26 calculation for the Asset.</param>
        /// <param name="signal">Most recent Signal calculation for the Asset.</param>
        public MACD(decimal close, decimal ema12, decimal ema26, decimal signal)
        {
            Ema12 = GetEMA(close, ema12, twelve);
            Ema26 = GetEMA(close, ema26, twentySix);
            Macd = Ema12 - Ema26;
            Signal = GetEMA(Macd, signal, nine);
        }

        /// <summary>
        /// Moving Average Convergence Divergence
        /// </summary>
        public MACD() { }

        /// <summary>
        /// Array of MACD calculations.
        /// </summary>
        public decimal[] MACDArray { get; }

        /// <summary>
        /// Array of Signal calculations.
        /// </summary>
        public decimal[] SignalArray { get; }

        /// <summary>
        /// Array of EMA 12 calculations.
        /// </summary>
        public decimal[] EMA12Array { get; }

        /// <summary>
        /// Array of EMA 26 calculations.
        /// </summary>
        public decimal[] EMA26Array { get; }

        /// <summary>
        /// Array of Close Prices
        /// </summary>
        private decimal[] ClosePrices { get; }

        /// <summary>
        /// Exponential Moving Average for 12 Periods. Calculated from close prices.
        /// </summary>
        public decimal Ema12 { get; set; }

        /// <summary>
        /// Exponential Moving Average for 26 Periods. Calculated from close prices.
        /// </summary>
        public decimal Ema26 { get; set; }

        /// <summary>
        /// Moving Average Convergence Divergence. MACD = EMA 12 - EMA 26
        /// </summary>
        public decimal Macd { get; set; }

        /// <summary>
        /// Exponential Moving Average for 9 Periods. Calculated from MACD values.
        /// </summary>
        public decimal Signal { get; set; }

        /// <summary>
        /// Provides calculation for the most recent MACD values. 
        /// </summary>
        /// <param name="close">Most recent close price for the Asset.</param>
        /// <param name="ema12">Most recent EMA 12 calculation for the Asset.</param>
        /// <param name="ema26">Most recent EMA 26 calculation for the Asset.</param>
        /// <param name="signal">Most recent Signal calculation for the Asset.</param>
        /// <returns>MACD: EMA 12, EMA 26, MACD, and Signal calculations. </returns>
        public MACD GetCurrent(decimal close, decimal ema12, decimal ema26, decimal signal)
        {
            return new MACD()
            {
                Ema12 = GetEMA(close, ema12, twelve),
                Ema26 = GetEMA(close, ema26, twentySix),
                Macd = Ema12 - Ema26,
                Signal = GetEMA(Macd, signal, nine)
            };

        }

        /// <summary>
        /// Gets the first EMA value in the EMA Array
        /// </summary>
        /// <param name="period">Period of time to be evaluated.</param>
        /// <returns>Decimal</returns>
        private decimal GetPriorEMA(int period)
        {
            int first = ClosePrices.Length - period;
            return ClosePrices[first..^0].Sum() / period;
        }

        /// <summary>
        /// Calculates the Moving Average Convergence Divergence
        /// </summary>
        /// <returns>Decimal array of MACD values</returns>
        private decimal[] GetMACD()
        {

            //Find SMA for 12 period
            var eMA12 = GetPriorEMA(twelve);
            // Get EMA Array for 12 period
            var EMA12Array = GetEMA(eMA12, twelve);

            // Find SMA for 26 period
            var eMA26 = GetPriorEMA(twentySix);
            // Get EMA Array for 26 period
            var EMA26Array = GetEMA(eMA26, twentySix);

            // create MACD Array
            decimal[] macd = new decimal[EMA26Array.Length];
            //MACD = EMA 12 - EMA 26
            for (int i = 0; i < macd.Length; i++)
            {
                macd[i] = EMA12Array[i] - EMA26Array[i];
            }
            return macd;
        }

        /// <summary>
        /// Calculates a single EMA. Requires the current Close price, current EMA of the same period, and the period being evaluated.
        /// </summary>
        /// <param name="close">Most recent Close price for the Asset.</param>
        /// <param name="ema">Most recent EMA for the same period.</param>
        /// <param name="period">Period of time to be evaluated.</param>
        /// <returns></returns>
        public decimal GetEMA(decimal close, decimal ema, decimal period)
        {
            return (close * TodaySmooth(period)) + (ema * YesterdaySmooth(period));
        }

        /// <summary>
        /// Calculates EMA values for a series of Close prices. Returns an array of EMA.
        /// </summary>
        /// <param name="ema">The first EMA in the series.</param>
        /// <param name="period">Period of time to be evaluated.</param>
        /// <returns>Decimal array of EMA</returns>
        private decimal[] GetEMA(decimal ema, decimal period)
        {
            // EMA.Today = (Close*Smooth) + (EMA.Yesterday *(1-Smooth)) 
            decimal[] EMA = new decimal[ClosePrices.Length - (int)period + 1];
            EMA[^1] = ema;
            for (int i = EMA.Length - 2; i >= 0; i--)
            {
                EMA[i] = GetEMA(ClosePrices[i], EMA[i + 1], period);
            }
            return EMA;
        }

        /// <summary>
        /// Calculates Signal values for a series of MACD values. Returns an array of Signals.
        /// </summary>
        /// <returns>Decimal array of Signal Calculations</returns>
        private decimal[] GetSignal()
        {
            int period = nine;

            int first = MACDArray.Length - period;
            decimal[] signal = new decimal[MACDArray.Length - 8];
            signal[^1] = MACDArray[first..^0].Sum() / period;

            for (int i = signal.Length - 2; i >= 0; i--)
            {
                signal[i] = GetEMA(MACDArray[i], signal[i + 1], period);
            }
            return signal;
        }

        /// <summary>
        /// Calculates the "Today" smoothing weight for the Exponential Moving Average.
        /// </summary>
        /// <param name="period">Period of time to be evaluated</param>
        /// <returns>Decimal</returns>
        private decimal TodaySmooth(decimal period)
        {
            //2. Calc Smooth: 2/(period+1)
            return 2 / (period + 1);
        }

        /// <summary>
        /// Calculates the "Tomorrow" smoothing weight for the Exponential Moving Average.
        /// </summary>
        /// <param name="period">Period of time to be evaluated</param>
        /// <returns>Decimal</returns>
        private decimal YesterdaySmooth(decimal period)
        {
            return 1 - TodaySmooth(period);
        }
    }
}