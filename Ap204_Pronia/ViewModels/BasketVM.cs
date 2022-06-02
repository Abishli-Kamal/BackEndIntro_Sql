 using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ap204_Pronia.ViewModels
{
    public class BasketVM
    {
        public List<BasketItemVM> BasketItemVMs { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal TotalPrice { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

        public BasketVM()
        {
            BasketItemVMs = new List<BasketItemVM>();
        }
    }
}
