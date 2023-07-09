namespace CarCatalogue.Extensions
{
    public static class FormFileExtensions
    {
        public static string GetExtension(this IFormFile file)
        {
            var sliced = file.FileName.Split('.');
            return sliced[sliced.Count() - 1];
        }
    }
}
