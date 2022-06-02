using Ap204_Pronia.Models;

namespace Ap204_Pronia.ViewModels
{
    public class BasketItemVM
    {
        public Plant Plant { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

    }
}
