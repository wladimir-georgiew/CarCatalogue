using System.ComponentModel.DataAnnotations;

namespace CarCatalogue.Common.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            this.extensions = extensions;
        }

        public string GetErrorMessage()
        {
            return $"Supported extensions are - {string.Join(", ", extensions)}";
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if (value is IFormFile item)
            {
                var extension = Path.GetExtension(item.FileName);
                if (!this.extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(this.GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }
    }
}
