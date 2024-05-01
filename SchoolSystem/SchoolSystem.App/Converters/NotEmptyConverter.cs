using System;
using System.Globalization;
using CommunityToolkit.Maui.Converters;
using DAL.Enums;
using Microsoft.Maui.Controls;

namespace SchoolSystem.App.Converters;


public class NotEmptyConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.All(v => v is string str && !string.IsNullOrEmpty(str)))
        {
            return true;
        }

        return false;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}