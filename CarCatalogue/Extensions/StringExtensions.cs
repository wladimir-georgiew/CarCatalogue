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
    }
}
