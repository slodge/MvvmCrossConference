using System;
using Cirrious.MvvmCross.Converters;

namespace Cirrious.Conference.Core.Converters
{
    public class TeamImageValueConverter
        : MvxBaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var stringValue = (string) value;
            return "/ConfResources/TeamImages/" + stringValue;
        }
    }
}