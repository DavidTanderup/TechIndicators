using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Technical_Indicators.Momentum
{
    /// TODO: Incomplete => https://www.investopedia.com/terms/i/ichimoku-cloud.asp
    public class IchimokuCloud
    {
        public IchimokuCloud(decimal[] closeArray)
        {
            CloseArray = closeArray;
            ConversionLine = (closeArray[0..8].Max() + closeArray[0..8].Min()) / 2;
            BaseLine = (closeArray[0..25].Max() + closeArray[0..25].Min()) / 2;
            LeadingSpanA = (closeArray[0] + BaseLine) / 2;
            LeadingSpanB = (closeArray[0..51].Max() + closeArray[0..51].Min()) / 2;
        }
        /// <summary>
        /// kenkan sen
        /// </summary>
        public decimal ConversionLine { get; set; }

        /// <summary>
        /// kijun sen
        /// </summary>
        public decimal BaseLine { get; set; }

        /// <summary>
        /// senkou span A
        /// </summary>
        public decimal LeadingSpanA { get; set; }

        /// <summary>
        /// senkou span B
        /// </summary>
        public decimal LeadingSpanB { get; set; }

        /// <summary>
        /// chikou span
        /// </summary>
        public decimal LaggingSpan { get; set; }

        public decimal[] CloseArray { get; set; }
    }
}
