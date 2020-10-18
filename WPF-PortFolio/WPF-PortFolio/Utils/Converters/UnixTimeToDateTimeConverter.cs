using System;
using System.Globalization;
using System.Windows.Data;

namespace WPF_PortFolio.Utils.Converters
{
    public class UnixTimeToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeStr = value.ToString();
            if (string.IsNullOrEmpty(timeStr))
                return "";
            int time = 0;

            bool success = int.TryParse(timeStr, out time);
            if (success == false)
                return "";

            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(time).ToLocalTime();
            return dtDateTime;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
