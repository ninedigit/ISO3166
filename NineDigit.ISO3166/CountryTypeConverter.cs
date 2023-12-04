using System.ComponentModel;
using System.Globalization;

namespace NineDigit.ISO3166;

public sealed class CountryTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        => sourceType == typeof(string) || sourceType == typeof(int);

    public override object? ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object? value)
    {
        return value switch
        {
            string stringValue => Country.Parse(stringValue),
            int intValue => Country.GetByNumericCode(intValue),
            _ => base.ConvertFrom(context, culture, value)
        };
    }
}