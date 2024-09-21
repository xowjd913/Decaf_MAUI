using System;
using System.Diagnostics;
using System.Globalization;

namespace Decaf.Converter
{
    public class BoolToColorConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean value to a color based on the provided parameters.
        /// </summary>
        /// <param name="value">The boolean value to convert. This should be of type <see cref="bool"/>.</param>
        /// <param name="targetType">The type of the target property, which is expected to be <see cref="Color"/> in this case.</param>
        /// <param name="parameter">A string parameter containing two colors separated by a '|'. The first color is used for true values, and the second color is used for false values. The colors should be in ARGB format, e.g., "FF0000FF|FFFF0000".</param>
        /// <param name="culture">Not used property</param>
        /// <returns>A <see cref="Color"/> corresponding to the boolean value. Returns <see cref="Colors.Black"/> if the value is not a boolean or if the parameter is not correctly formatted.</returns>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                if (parameter is string colorParameters)
                {
                    var colors = colorParameters.Split('|');
                    if (colors.Length == 2)
                    {
                        var trueColor = Color.FromArgb(colors[0]);
                        var falseColor = Color.FromArgb(colors[1]);

                        return booleanValue ? trueColor
                                            : falseColor;
                    }
                }
            }

            Debug.WriteLine($"{nameof(BoolToColorConverter)} [E] = value type is not boolean | return black color");
            return Colors.Black;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

