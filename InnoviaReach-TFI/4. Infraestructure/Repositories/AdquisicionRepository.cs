using Core.Contracts.Repositories;
using Core.Domain.Models;
using Infrastructure.Data.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace _4._Infraestructure.Repositories
{
    public class AdquisicionRepository : GenericRepository<AdquisicionModel>, IAdquisicionRepository
    {
        public AdquisicionRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }

        public async Task LimpiarJuegoReciente(string User_ID)
        {
            var juegos = this.Entities.Where(x => x.TiempoJuegoReciente!=null && x.User_ID==User_ID);
            await juegos.ExecuteUpdateAsync(y => y.SetProperty(z => z.TiempoJuegoReciente, z => (int?)null));
            await _context.SaveChangesAsync();
        }
    }
}
