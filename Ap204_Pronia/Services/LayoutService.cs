using Ap204_Pronia.DAL;
using Ap204_Pronia.Models;
using Ap204_Pronia.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ap204_Pronia.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        private IHttpContextAccessor _httpContext;

        public LayoutService(AppDbContext context,IHttpContextAccessor _httpContext)
        {
           _context = context;
            this._httpContext = _httpContext;
        }
        public async Task<List<Seetting>> GetDatas()
        {
            List<Seetting> settings = await _context.Seettings.ToListAsync();

            return settings;
        }
        public BasketVM GetBasket()
        {
            string basketStr = _httpContext.HttpContext.Request.Cookies["Basket"];
          
            if (!string.IsNullOrEmpty(basketStr))
            {
                BasketVM basketData = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                return basketData;

            }
            else
            {
                return null;
            }


        }
    }
}
