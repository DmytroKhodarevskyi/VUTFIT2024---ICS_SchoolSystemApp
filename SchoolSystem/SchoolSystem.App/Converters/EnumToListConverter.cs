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
        var enumType = value as Type;
        if (enumType != null && enumType.IsEnum)
        {
            return Enum.GetValues(enumType);
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}

