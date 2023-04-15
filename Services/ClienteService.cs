using CarRentalCompany.Data;
using CarRentalCompany.Models;
using CarRentalCompany.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CarRentalCompany.Services
{
    public class ClienteService
    {
        private readonly ICarRentalCompanyContext _context;

        public ClienteService(ICarRentalCompanyContext context)
        {
            _context = context;
        }

        public List<Cliente> FindAll()
        {
            return _context.Cliente.ToList();
        }

        public void Insert(Cliente cliente)
        {
            cliente.DataCadastro = DateTime.Now;

            _context.Cliente.Add(cliente);
            _context.SaveChanges();
        }

        public Cliente FindById(int id)
        {
            var cliente = _context.Cliente.FirstOrDefault(obj => obj.Id == id);

            if (cliente == null)
            {
                throw new UserException("Cliente não foi encontrado");
            }

            return cliente;
        }

        public void Update(Cliente cliente)
        {
            bool hasAny = _context.Cliente.Any(x => x.Id == cliente.Id);

            if (!hasAny)
            {
                throw new UserException("Cliente não encontrado");
            }

            cliente.DataAtualizado = DateTime.Now;

            _context.Cliente
                .Update(cliente)
                .Property(x => x.DataCadastro).IsModified = false;

            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var obj = _context.Cliente.Find(id);

            if (obj == null)
            {
                throw new UserException("Cliente não encontrado");
            }

            _context.Cliente.Remove(obj);
            _context.SaveChanges();
        }
    }
}
