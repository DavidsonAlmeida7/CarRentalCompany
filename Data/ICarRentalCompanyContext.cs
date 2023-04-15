using CarRentalCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalCompany.Data
{
    public interface ICarRentalCompanyContext
    {
        DbSet<Cliente> Cliente { get; set; }
        DbSet<Locacao> Locacao { get; set; }
        DbSet<Veiculo> Veiculo { get; set; }
        int SaveChanges();
    }
}
