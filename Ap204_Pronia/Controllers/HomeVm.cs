
using Ap204_Pronia.Models;
using System.Collections.Generic;

namespace Ap204_Pronia.Controllers
{
    public class HomeVm
    {
        public List<Slider> Sliders { get; set; }
        public List<Customer> Customers { get;  set; }
    }
}