namespace Gis.Api.Data
{
    using System;
    using System.Globalization;

    public static class ParserHelper
    {
        public static int? ParseInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            value = value.Trim().Replace(",", "");

            if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int result))
                return result;

            return null;
        }

        public static double? ParseDouble(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            value = value.Trim().Replace(",", "");

            if (double.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double result))
                return result;

            return null;
        }
    }

}
