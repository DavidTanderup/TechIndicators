# TechIndicators
Technical Indicators

C# Technical Indicators README

The C# Technical Indicator Library is a collection of the fundamental technical indicators used to make trading and investment decisions. These indicators are among the most widely used and are considered to be the most reliable. Technical Indicators are mathematical calculations of historical data used to gain insight into the current state of the asset being evaluated and make educated predictions about the future movements. Technical Indicators fall into four categories: 
	•	Momentum is the rate of acceleration of a security's price or volume.
	•	Trend is the general direction the market is taking during a specified period of time.
	•	Volatility indicators look at changes in market prices over a specified period of time. 
	•	Volume used to quantify the relative significance of market moves. Often used to track movements of “smart money” or institutional investors.

There are 2 – 3 major indicators provided from each category in this library.

1.	Momentum
	a.	Moving Average Convergence Divergence (MACD) is a trend-following momentum indicator that shows the relationship between two moving averages of a security's price. The MACD 		is calculated by subtracting the 26-period Exponential Moving Average (EMA) from the 12-period EMA.

	b.	Relative Strength Index (RSI) is a momentum indicator that measures the magnitude of recent price changes to evaluate overbought or oversold conditions in the price of a 		stock or other asset. The RSI is displayed as an oscillator (a line graph that moves between two extremes) and can have a reading from 0 to 100.

	c.	Stochastic Oscillator a momentum indicator comparing a particular closing price of a security to a range of its prices over a certain period of time. The sensitivity of the 		oscillator to market movements is reducible by adjusting that time period or by taking a moving average of the result.

2.	Trend
	a.	Aroon is a technical indicator that is used to identify trend changes in the price of an asset, as well as the strength of that trend.

	b.	Average Directional Index (ADX) is a technical analysis indicator used by some traders to determine the strength of a trend. The trend can be either up or down, and this is 	shown by two accompanying indicators, the Negative Directional Indicator (-DI) and the Positive Directional Indicator (+DI). Therefore, ADX commonly includes three separate lines. 	These are used to help assess whether a trade should be taken long or short, or if a trade should be taken at all.

	c.	Moving Average is a simple average of the sum of the asset values / number of periods being evaluated.

3.	Volatility
	a.	Average True Range (ATR) is a technical analysis indicator that measures market volatility by decomposing the entire range of an asset price for that period. The average 	true range is then a moving average, generally using 14 days, of the true ranges.

	b.	Bollinger Bands is a technical analysis tool defined by a set of lines plotted two standard deviations (positively and negatively) away from a simple moving average (SMA) of 	the security's price but can be adjusted to user preferences.

4.	Volume
	a.	Accumulation Distribution Line (ADL) is a cumulative indicator that uses volume and price to assess whether a stock is being accumulated or distributed.

	b.	On Balance Volume (OBV) is a technical trading momentum indicator that uses volume flow to predict changes in stock price.

How to Use:
The beauty of this library is that it allows the user to perform complex mathematical calculations by simply providing the required decimal or long array of data (sorted in descending order) to the class constructor. The data is returned via the class properties in the data array and most recent data point, i.e. from the RelativeStrengthIndex class the RSIArray would provide all calculated data points, and RSI would provide the most current or recent data point (index 0).

