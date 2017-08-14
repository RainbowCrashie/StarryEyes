using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using StarryEyes.ViewModels.WindowParts.Flips.Analysis;

namespace StarryEyes.Views.WindowParts.Flips.Analysis
{
    public class AnalysisUserToReletiveWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = System.Convert.ToDouble(values[0]);
            var highestValueInChart = System.Convert.ToDouble(values[1]);
            var maxWidth = System.Convert.ToDouble(values[2]);

            var reletiveSumRatio = value / highestValueInChart;

            if (reletiveSumRatio < 0.15)
                reletiveSumRatio = 0.15;

            return reletiveSumRatio * maxWidth;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AnalysisUserToTweetRatioComverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var user = (AnalysisUser)value;

            if (user == null)
                throw new ArgumentException();

            var ratio = (double)user.TweetCount / user.Sum;

            return new GridLength(ratio, GridUnitType.Star);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AnalysisUserToRetweetRatioComverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var user = (AnalysisUser)value;

            if (user == null)
                throw new ArgumentException();

            var ratio = (double)user.RetweetCount / user.Sum;

            return new GridLength(ratio, GridUnitType.Star);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}