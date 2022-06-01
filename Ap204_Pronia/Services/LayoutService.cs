using Ap204_Pronia.DAL;
using Ap204_Pronia.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ap204_Pronia.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
           _context = context;
        }
        public async Task<Seetting> GetDatas()
        {
            Seetting setting = await _context.Seettings.FirstOrDefaultAsync();

            return setting;
        }
    }
}
