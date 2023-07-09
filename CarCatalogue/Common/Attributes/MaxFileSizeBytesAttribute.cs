using System.ComponentModel.DataAnnotations;

namespace CarCatalogue.Common.Attributes
{
    public class MaxFileSizeBytesAttribute : ValidationAttribute
    {
        private readonly int maxFileSizeBytes;

        public MaxFileSizeBytesAttribute(int maxFileSize)
        {
            this.maxFileSizeBytes = maxFileSize;
        }

        public string GetErrorMessage()
        {
            return $"You can't upload image with size bigger than {this.maxFileSizeBytes / 1024 / 1024} mb.";
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if (value is List<IFormFile> files)
            {
                foreach (var item in files)
                {
                    if (item.Length > this.maxFileSizeBytes)
                    {
                        return new ValidationResult(this.GetErrorMessage());
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
