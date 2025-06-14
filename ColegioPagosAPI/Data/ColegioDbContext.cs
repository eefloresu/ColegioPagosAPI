using Microsoft.EntityFrameworkCore;
using ColegioPagosAPI.Models;

namespace ColegioPagosAPI.Data
{
    public class ColegioDbContext : DbContext
    {
        public ColegioDbContext(DbContextOptions<ColegioDbContext> options) : base(options) { }

        public DbSet<PagoColegiatura> Pagos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}

