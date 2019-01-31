
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base (options) 
        { }

        public DbSet<acesso_siaf> acesso_siaf { get; set; }
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<usuario_permissao> usuario_permissao { get; set; }
        public DbSet<usuario_log> usuario_log { get; set; }
        public DbSet<controle_acesso> controle_acesso { get; set; }
      
    }
}