namespace CarCatalogue.Models.Response
{
    public class CarResponseModel
    {
        public int Id { get; set; }

        public string? Make { get; set; }

        public string? Model { get; set; }

        public int? Year { get; set; }

        public int Horsepower { get; set; }

        public double Acceleration { get; set; }

        public int Weight { get; set; }

        public string? ImageUrl { get; set; }
    }
}
