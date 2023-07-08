namespace CarCatalogue.Models.Response
{
    public class CarResponseModel
    {
        public string? Make { get; set; }

        public string? Model { get; set; }

        public DateTime? Year { get; set; }

        public int Horsepower { get; set; }

        public double Acceleration { get; set; }

        public int Weight { get; set; }
    }
}
