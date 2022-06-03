using Ap204_Pronia.DAL;
using Ap204_Pronia.Models;
using Ap204_Pronia.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ap204_Pronia.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        private IHttpContextAccessor _httpContext;

        public LayoutService(AppDbContext context, IHttpContextAccessor _httpContext)
        {
            _context = context;
            this._httpContext = _httpContext;
        }
        public async Task<List<Seetting>> GetDatas()
        {
            List<Seetting> settings = await _context.Seettings.ToListAsync();

            return settings;
        }
        public async Task<BasketVM> GetBasket()
        {
            string basketStr = _httpContext.HttpContext.Request.Cookies["Basket"];
            BasketVM basketVM = new BasketVM();

            if (!string.IsNullOrEmpty(basketStr))
            {
                //BasketVM basketData = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                List<BasketCookieItemVM> basketData = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basketStr);
                List<Plant> query=await _context.Plants.Include(p=>p.PlantImages).ToListAsync();
                foreach (BasketCookieItemVM item in basketData)
                {
                    Plant existedplant = query.FirstOrDefault(p => p.Id == item.Id); 
                    if (existedplant != null)
                    {

                        BasketItemVM basketItemVM = new BasketItemVM
                        {
                            Plant = existedplant,
                            Count = item.Count
                        };
                        basketVM.BasketItemVMs.Add(basketItemVM); 


                    }
                }
                decimal total = default;
                foreach (BasketItemVM item in basketVM.BasketItemVMs)
                {
                    total += item.Plant.Price * item.Count;
                }

                basketVM.TotalPrice = total;
                basketVM.Count = basketVM.BasketItemVMs.Count;


                return basketVM;

            }
            else
            {
                return null;
            }


        }
    }
}
