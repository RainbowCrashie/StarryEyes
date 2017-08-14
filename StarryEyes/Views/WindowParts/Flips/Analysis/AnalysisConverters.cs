using System;
using System.Globalization;
using System.Windows.Data;

namespace StarryEyes.Views.WindowParts.Flips.Analysis
{
    public class AnalysisDateTimeFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = (DateTime?) value;
            if (dateTime == null)
                return "...";

            var ago = DateTime.Now.Subtract(dateTime.Value);

            if (ago.TotalSeconds < 120)
                return $"{(int)ago.TotalSeconds} secs ago";
            if (ago.TotalMinutes < 120)
                return $"{(int)ago.TotalMinutes} mins ago";
            if (ago.TotalHours < 48)
                return $"{(int)ago.TotalHours} hrs ago";

            return $"{(int)ago.TotalDays} days ago";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}