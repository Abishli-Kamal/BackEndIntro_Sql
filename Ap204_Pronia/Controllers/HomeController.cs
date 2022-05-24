using Ap204_Pronia.Models;
using Ap204_Pronia.DAL;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ap204_Pronia.Controllers
{
    public class HomeController : Controller
    {
        private  AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVm model = new HomeVm
            {
            Sliders = await _context.Sliders.ToListAsync(),
            Customers = await _context.Customers.ToListAsync()
            };
            
            return View(model);
        }

       
    }
}
