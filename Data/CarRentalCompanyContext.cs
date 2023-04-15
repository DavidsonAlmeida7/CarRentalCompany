using CarRentalCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalCompany.Data
{
    public class CarRentalCompanyContext : DbContext, ICarRentalCompanyContext
    {
        public CarRentalCompanyContext(DbContextOptions<CarRentalCompanyContext> options) : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; } = default!;
        public virtual DbSet<Locacao> Locacao { get; set; } = default!;
        public virtual DbSet<Veiculo> Veiculo { get; set; } = default!;
    }
}
