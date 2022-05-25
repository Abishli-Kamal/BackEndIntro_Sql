using Ap204_Pronia.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing;
//using System.Drawing;
using System.Threading.Tasks;

namespace Ap204_Pronia.Areas.ProniaAdmin.Controllers
{

    [Area("ProniaAdmin")]

    public class SizeController : Controller
    {
        private readonly AppDbContext _context;

        public SizeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Models.Size> sizes = await _context.Sizes.ToListAsync();
            return View(sizes);
        }
        public IActionResult Create()
        {
            return Json("Create");
        }
        public IActionResult Detail(int id)
        {
            return Json(id);
        }
        public IActionResult Edit(int id)
        {
            return Json(id);
        }

        public IActionResult Delete(int id)
        {
            return Json(id);
        }

    }

}
