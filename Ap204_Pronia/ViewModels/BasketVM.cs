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

        public BasketVM()
        {
            BasketItemVMs = new List<BasketItemVM>();
        }
    }
}
