using System.ComponentModel.DataAnnotations;

namespace CarCatalogue.Data.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        public string? Make { get; set; }

        public string? Model { get; set; }

        public DateTime? Year { get; set; }

        public int Horsepower { get; set; }

        public double Acceleration { get; set; }

        public int Weight { get; set; }
    }
}
