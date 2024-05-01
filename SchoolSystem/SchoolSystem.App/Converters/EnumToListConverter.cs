using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace SchoolSystem.App.Converters;

public class EnumToListConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Type enumType && enumType.IsEnum)
        {
            return Enum.GetNames(enumType).ToList();
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

