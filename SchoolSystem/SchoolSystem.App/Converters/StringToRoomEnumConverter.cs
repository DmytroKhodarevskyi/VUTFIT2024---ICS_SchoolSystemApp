using System;
using System.Globalization;
using CommunityToolkit.Maui.Converters;
using DAL.Enums;
using Microsoft.Maui.Controls;

namespace SchoolSystem.App.Converters;

public class RoomToStringConverter : IValueConverter
// public class RoomToStringConverter : BaseConverterOneWay<Room, string>
{
    // public override string ConvertFrom(Room value, CultureInfo? culture)
    //     => FoodTypeTexts.ResourceManager.GetString(value.ToString(), culture)
    //        ?? FoodTypeTexts.None;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is String str)
        {
            return Enum.Parse(typeof(Room), str);
        }
        
        if (value is Room room)
        {
            return room.ToString();
        }

        return Room.Unknown;
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Room room)
        {
            return room.ToString();
        }
        if (value is string str)
        {
            if (Enum.TryParse(typeof(Room), str, out var parsedRoom)) // Changed 'room' to 'parsedRoom'
            {
                return parsedRoom;
            }
            else
            {
                return Room.Unknown;
            }
    

        }
        throw new NotSupportedException($"{nameof(RoomToStringConverter)} does not support converting back from string to Room.");
    }
}
