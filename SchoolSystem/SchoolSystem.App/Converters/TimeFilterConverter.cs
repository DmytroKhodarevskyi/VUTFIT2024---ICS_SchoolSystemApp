using System;
using System.Globalization;
using CommunityToolkit.Maui.Converters;
using DAL.Enums;
using Microsoft.Maui.Controls;

namespace SchoolSystem.App.Converters;


public class TimeFilterConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // Assuming "NoFilter" means no filtering should be applied and controls should be enabled
        return value?.ToString() == "NoFilter";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}