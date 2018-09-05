using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfApp4
{
    public class SecondsToMinutesConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int seconds = (int)value;
            string stringResult = TimeSpan.FromSeconds(seconds).ToString();
            return stringResult.Remove(0, 3);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<string> time = (value as string).Split(':','.',',').ToList();
            int result = int.Parse(time[0]) * 60;
            result += int.Parse(time[1]);
            return result;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
