using System;
using System.Collections.Generic;
using System.Text;
using TechnicalIndicators.Momentum;
using TechnicalIndicators.Trend;
using TechnicalIndicators.Volatility;
using TechnicalIndicators.Volume;

namespace TechnicalIndicators
{
    class DataErrors
    {
        private Exception UnEqual
        {
            get
            {
                return new Exception($"The data sets entered are not the same length.");
            }
        }
        public void ValidData(string indicator,int dataOne, int dataTwo)
        {
            if (dataOne == dataTwo)
            {
                IsReqLength(indicator, dataOne);
            }
            else
            {
                throw UnEqual;
            }

        }
        public void ValidData(string indicator, int dataOne, int dataTwo, int dataThree)
        {
            if (dataOne == dataTwo && dataOne == dataThree)
            {
                IsReqLength(indicator, dataOne);
            }
            else
            {
                throw UnEqual;
            }
        }
        public void ValidData(string indicator, int dataOne, int dataTwo, int dataThree, int dataFour)
        {
            if (dataOne == dataTwo && dataOne == dataThree && dataOne == dataFour)
            {
                IsReqLength(indicator, dataOne);
            }
            else
            {
                throw UnEqual;
            }
        }
        
        public void IsReqLength(string indicator, int dataLength)
        {
            switch (indicator)
            {
                case "ADL":
                    var minLength = 1;
                    EnoughData(dataLength, minLength);
                    break;
                case "Aroon":
                    minLength = 26;
                    EnoughData(dataLength, minLength);
                    break;
                case "ADX":
                    minLength = 28;
                    EnoughData(dataLength, minLength);
                    break;
                case "ATR":
                    minLength = 15;
                    EnoughData(dataLength, minLength);
                    break;
                case "BB":
                    minLength = 21;
                    EnoughData(dataLength, minLength);
                    break;
                case "MACD":
                    minLength = 34;
                    EnoughData(dataLength, minLength);
                    break;
                case "MA":
                    minLength = 201;
                    EnoughData(dataLength, minLength);
                    break;
                case "OBV":
                    minLength = 1;
                    EnoughData(dataLength, minLength);
                    break;
                case "RSI":
                    minLength = 15;
                    EnoughData(dataLength, minLength);
                    break;
                case "Stochastic":
                    minLength = 201;
                    EnoughData(dataLength, minLength);
                    break;
                default:
                    Exception unKnownIndicator = new Exception($"The indicator class: {indicator} is not a valid type.");
                    throw unKnownIndicator;

            }          

        }

        private static void EnoughData(int dataLength, int minLength)
        {
            if (dataLength < minLength)
            {
                Exception ex = new Exception($"{dataLength} is an insufficient number of data points. Minimum = {minLength}.");
                throw ex;
            }
        }
    }

}
