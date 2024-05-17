using System;
using System.Globalization;
using CommunityToolkit.Maui.Converters;
using DAL.Enums;
using Microsoft.Maui.Controls;

namespace SchoolSystem.App.Converters;


public class SubjectSaveConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length != 2)
            return false;

        var name = values[0] as string;
        var tag = values[1] as string;

        // Example logic: Return true only if both are not empty
        bool isNameNotEmpty = !string.IsNullOrEmpty(name);
        bool isTagValid = int.TryParse(tag, out int tagValue) && tagValue > 0;

        // Example logic: Return true only if name is not empty and tag is valid
        return isNameNotEmpty && isTagValid;

    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}