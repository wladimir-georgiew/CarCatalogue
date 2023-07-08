using System.ComponentModel.DataAnnotations;

namespace CarCatalogue.Models.Request
{
    public class CarRequestModel
    {
        [Required]
        public string? Make { get; set; }

        [Required]
        public string? Model { get; set; }

        [Required]
        public string? Year { get; set; }

        [Required]
        public int Horsepower { get; set; }

        [Required]
        public double Acceleration { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        //Add more attributes
        public IFormFile? Image { get; set; }
    }
}
