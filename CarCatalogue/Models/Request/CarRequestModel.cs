using CarCatalogue.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogue.Models.Request
{
    public class CarRequestModel
    {
        [Required]
        public string? Make { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string? Model { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Range(1880, 3000, ErrorMessage = "Invalid year")]
        public string? Year { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Range(0, int.MaxValue, ErrorMessage = "Must be a positive number")]
        public int Horsepower { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Range(0, int.MaxValue, ErrorMessage = "Must be a positive number")]
        public double Acceleration { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int Weight { get; set; }

        [Required]
        [AllowedExtensions(new[] { ".jpg", ".png", ".jpeg" })]
        [MaxFileSizeBytes(3 * 1024 * 1024)] // 3mb
        public IFormFile? Image { get; set; }
    }
}
