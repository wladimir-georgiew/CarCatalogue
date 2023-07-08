namespace CarCatalogue.Models.Request
{
    public class CarRequestModel
    {
        public string? Make { get; set; }

        public string? Model { get; set; }

        public string? Year { get; set; }

        public int Horsepower { get; set; }

        public double Acceleration { get; set; }

        public int Weight { get; set; }

        public IFormFile? ImageUrl { get; set; }
    }
}
