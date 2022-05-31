using Ap204_Pronia.DAL;
using Ap204_Pronia.Exntention;
using Ap204_Pronia.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Ap204_Pronia.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class PlantController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public PlantController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            List<Plant> plants = await _context.Plants.Include(p => p.PlantImages).ToListAsync();
            return View(plants);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Plant plant)
        {
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            if (!ModelState.IsValid) return View();

            if (plant.MainImage == null || plant.AnotherImage == null)
            {
                ModelState.AddModelError("", "Please choose main image or another image");
                return View();
            }
            if (!plant.MainImage.IsOkay(5))
            {
                ModelState.AddModelError("MainImage", "Please choose  image file and max 1Mb ");
                return View();
            }
            foreach (var img in plant.AnotherImage)
            {
                if (!img.IsOkay(5))
                {
                    ModelState.AddModelError("AnotherImage", "Please choose  image file and min 1Mb ");
                    return View();
                }
            }


            plant.PlantImages = new List<PlantImage>();
            PlantImage mainImage = new PlantImage
            {
                ImagePath = await plant.MainImage.FileCreate(_env.WebRootPath, "assets/images/website-images"),
                IsMain = true,
                Plant = plant

            };
            plant.PlantImages.Add(mainImage);


            foreach (var img in plant.AnotherImage)
            {
                PlantImage anotherimage = new PlantImage
                {
                    ImagePath = await img.FileCreate(_env.WebRootPath, "assets/images/website-images"),
                    IsMain = false,
                    Plant = plant

                };
                plant.PlantImages.Add(anotherimage);
            }

            plant.PlantCategories = new List<PlantCategory>();

            foreach (var id in plant.CategoryIds)
            {
                PlantCategory plantCategory = new PlantCategory
                {
                    Plant = plant,
                    CategoryId = id
                };
                plant.PlantCategories.Add(plantCategory);
            }

            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            Plant plant = await _context.Plants.Include(p => p.PlantImages).Include(p => p.PlantCategories).FirstOrDefaultAsync(p => p.Id == id);
            return View(plant);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Edit(Plant plant, int id)
        {

            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            Plant existed = await _context.Plants.Include(p => p.PlantImages).Include(p => p.PlantCategories).FirstOrDefaultAsync(p => p.Id == id);



            if (plant.ImageIds == null && plant.AnotherImage == null)
            {
                ModelState.AddModelError("", "You can not delete all images without adding another image");
                return View(existed);
            }





            List<PlantImage> removeImage = existed.PlantImages.Where(p => p.IsMain == false && !plant.ImageIds.Contains(p.Id)).ToList();
            existed.PlantImages.RemoveAll(p => removeImage.Any(rp => rp.Id == p.Id));
            List<PlantImage> removeMainImage = existed.PlantImages.Where(p => p.IsMain == true && plant.MainImageIds != p.Id).ToList();
            existed.PlantImages.RemoveAll(p => removeMainImage.Any(rp => rp.Id == p.Id));

            List<PlantCategory> removeCategory = existed.PlantCategories.Where(pc => !plant.CategoryIds.Contains(pc.CategoryId)).ToList();

            foreach (var item in plant.CategoryIds)
            {
                PlantCategory existedCategory = existed.PlantCategories.FirstOrDefault(pc => pc.CategoryId == item);
                if (existedCategory == null)
                {
                    PlantCategory plantCategory = new PlantCategory
                    {
                        PlantId = existed.Id,
                        CategoryId = item
                    };
                    existed.PlantCategories.Add(plantCategory);
                }
            }


            if (plant.MainImageIds == null && plant.MainImage == null)
            {
                ModelState.AddModelError("", "You can not delete all images without adding main image");
                return View(existed);
            }


            foreach (var image in removeImage)
            {
                FileUtilities.FileDelete(_env.WebRootPath, @"assets\images\website-images", image.ImagePath);
            }

            if (plant.AnotherImage != null)
            {
                foreach (var image in plant.AnotherImage)
                {
                    PlantImage plantImage = new PlantImage
                    {
                        ImagePath = await image.FileCreate(_env.WebRootPath, @"assets\images\website-images"),
                        IsMain = false,
                        PlantId = existed.Id
                    };
                    existed.PlantImages.Add(plantImage);
                }
            }




            if (plant.MainImage != null)
            {
                plant.PlantImages = new List<PlantImage>();
                PlantImage mainImage = new PlantImage
                {
                    ImagePath = await plant.MainImage.FileCreate(_env.WebRootPath, "assets/images/website-images"),
                    IsMain = true,
                    Plant = plant

                };
                plant.PlantImages.Add(mainImage);

            }
            //FileUtilities.FileDelete(_env.WebRootPath, @"assets\images\website-images", removeMainImage.ImagePath);


            _context.Entry(existed).CurrentValues.SetValues(plant);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Detail(int id)
        {
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            Plant plant = await _context.Plants.Include(p => p.PlantImages).FirstOrDefaultAsync(p => p.Id == id);
            return View(plant);
        }
        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            Plant plant = await _context.Plants.Include(p => p.PlantImages).FirstOrDefaultAsync(p => p.Id == id);
            return View(plant);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePlant(Plant plant, int id)
        {
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Colors = await _context.Colors.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            plant = await _context.Plants.Include(p => p.PlantImages).FirstOrDefaultAsync(p => p.Id == id);

            if (plant == null) return NotFound();


            PlantImage plantImage = new PlantImage();
            string Path = $"~/assets/images/website-images/{plantImage.ImagePath}";
            FileInfo file = new FileInfo(Path);
            if (file.Exists)
            {
                file.Delete();
            }
            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


    }
}
