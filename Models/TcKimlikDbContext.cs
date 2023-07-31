using Microsoft.EntityFrameworkCore;
namespace TcKimlikKontrol.Models
{
    public class TcKimlikDbContext : DbContext
    {
        public TcKimlikDbContext(DbContextOptions<TcKimlikDbContext> options) : base(options)
        {

        }

        public DbSet<Kayit> TcKimlik { get; set; }

        public async Task<bool> SaveTcmbKurAsync(Kayit kayit)
        {
            try
            {
                TcKimlik.Add(kayit);
                await SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }


}
