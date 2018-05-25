using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Data;

namespace ConverterSample
{
    [ValueConversion(sourceType: typeof(List<List<double>>), targetType: typeof(List<string>))]
    public class DoubleListListToStringListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null) return DependencyProperty.UnsetValue;
            var l = value as List<List<double>>;
            return l.Select(x => string.Join(",", x.Select(y => y.ToString("0.0"))))
                .ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}