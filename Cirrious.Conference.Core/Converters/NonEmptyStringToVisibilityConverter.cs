using System.Globalization;
using Cirrious.MvvmCross.Converters.Visibility;

namespace Cirrious.Conference.Core.Converters
{
    public class NonEmptyStringToVisibilityConverter
        : MvxBaseVisibilityConverter
    {
        #region Overrides of MvxBaseVisibilityConverter

        public override MvxVisibility ConvertToMvxVisibility(object value, object parameter, CultureInfo culture)
        {
            var stringValue = (string) value;

            return string.IsNullOrWhiteSpace(stringValue) ? MvxVisibility.Collapsed : MvxVisibility.Visible;
        }

        #endregion
    }
}