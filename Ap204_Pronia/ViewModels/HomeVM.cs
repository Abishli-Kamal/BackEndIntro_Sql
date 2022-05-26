using Ap204_Pronia.Models;
using System.Collections.Generic;

namespace Ap204_Pronia.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Plant>Plants { get; set; }
        public List<Category> Categories { get; set; }
    }
}
