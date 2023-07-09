namespace CarCatalogue.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string value)
        {
            if (value.Length == 0)
                return string.Empty;
            else if (value.Length == 1)
                return char.ToUpper(value[0]).ToString();
            else
                return char.ToUpper(value[0]) + value.Substring(1);
        }

        public static DateTime ToDateTimeYear(this string value)
        {
            var isValidYear = int.TryParse(value, out var year);

            if (!isValidYear || year < 0 || year > 3000)
            {
                throw new Exception($"The value '{value}' is not a valid year.");
            }

            return new DateTime(year: Convert.ToInt32(value), 1, 1);
        }
    }
}
