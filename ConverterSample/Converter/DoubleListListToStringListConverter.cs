using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Data;

namespace ConverterSample.Converter
{
    /// <summary>
    /// コンバータ
    /// </summary>
    [ValueConversion(sourceType: typeof(IEnumerable<IEnumerable<double>>), targetType: typeof(ICollection<string>))]
    public class DoubleListListToStringListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 変換できない時は DependencyProperty.UnsetValueを呼び出す
            if(value == null) return DependencyProperty.UnsetValue;

            var l = (IEnumerable<IEnumerable<double>>)value;
            return l.Select(x =>
                    string.Join(
                        separator: ",",
                        values: x.Select(y => y.ToString("0"))))
                .ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}