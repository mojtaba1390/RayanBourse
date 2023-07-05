using RayanBourse.Domain;

namespace RayanBourse.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public EnumYesNo IsAvailable { get; set; }

        public DateTime ProduceDate { get; set; }

    }
}
