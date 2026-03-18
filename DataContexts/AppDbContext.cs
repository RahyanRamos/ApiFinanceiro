using Microsoft.EntityFrameworkCore;
using ApiFinanceiro.Models;

namespace ApiFinanceiro.DataContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Despesa> Despesas { get; set; }
    }
}
