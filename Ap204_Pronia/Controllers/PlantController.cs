using Ap204_Pronia.DAL;
using Ap204_Pronia.Models;
using Ap204_Pronia.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ap204_Pronia.Controllers
{

    public class PlantController : Controller
    {
        private readonly AppDbContext _context;

        public PlantController(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> DeleteBasket(int id)
        //{
        //    Plant plant = await _context.Plants.FirstOrDefaultAsync(p => p.Id == id);
        //    if (plant == null) return NotFound();
        //    string basketStr = HttpContext.Request.Cookies["Basket"];

        //    List<BasketCookieItemVM> basket;

        //    if (string.IsNullOrEmpty(basketStr))
        //    {

        //        basket = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basketStr);
        //        BasketCookieItemVM existedcookie = basket.FirstOrDefault(i => i.Id == plant.Id);

        //        if (existedcookie == null)
        //        {
        //            BasketCookieItemVM cookie = new BasketCookieItemVM
        //            {
        //                Id = plant.Id,
        //                Count = 1
        //            };


        //            basket.Remove(cookie);

        //        }

        //    }

        //    HttpContext.Response.Cookies.Delete("Basket", basketStr);
        //    return RedirectToAction("Index", "Home");
        //}

        public IActionResult DeleteBasket()
        {
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
                
            }
            return RedirectToAction("Index", "Home");
        }




        public async Task<IActionResult> AddBasket(int id)
        {
            Plant plant = await _context.Plants.FirstOrDefaultAsync(p => p.Id == id);
            if (plant == null) return NotFound();
            string basketStr = HttpContext.Request.Cookies["Basket"];
        
            List<BasketCookieItemVM> basket;
            if (string.IsNullOrEmpty(basketStr))
            {
                basket = new List<BasketCookieItemVM>();
              

                BasketCookieItemVM cookie = new BasketCookieItemVM
                {
                    Id = plant.Id,
                    Count = 1
                };
               
                basket.Add(cookie);
                basketStr = JsonConvert.SerializeObject(basket);

            }
            else
            {
        
                basket = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basketStr);
                BasketCookieItemVM existedcookie = basket.FirstOrDefault(i => i.Id == plant.Id);

                if (existedcookie == null)
                {
                    BasketCookieItemVM cookie = new BasketCookieItemVM
                    {
                        Id = plant.Id,
                        Count = 1
                    };

                    
                    basket.Add(cookie);
                }
                else
                {
                    existedcookie.Count++;
                }

               
                basketStr = JsonConvert.SerializeObject(basket);

            }

            HttpContext.Response.Cookies.Append("Basket", basketStr);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ShowBasket()
        {
            return Content(HttpContext.Request.Cookies["Basket"]);
        }
    }

}
