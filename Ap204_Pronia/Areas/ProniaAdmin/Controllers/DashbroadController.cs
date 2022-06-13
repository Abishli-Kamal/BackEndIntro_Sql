using Microsoft.AspNetCore.Mvc;

namespace Ap204_Pronia.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class DashbroadController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
