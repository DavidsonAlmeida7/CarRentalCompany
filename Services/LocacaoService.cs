using CarRentalCompany.Data;
using CarRentalCompany.Models;
using CarRentalCompany.Models.Enums;
using CarRentalCompany.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CarRentalCompany.Services
{
    public class LocacaoService
    {
        private readonly ICarRentalCompanyContext _context;

        public LocacaoService(ICarRentalCompanyContext context)
        {
            _context = context;
        }

        public List<Locacao> FindAll()
        {
            return _context.Locacao
                .Include(obj => obj.Veiculo)
                .Include(obj => obj.Cliente)
                .ToList();
        }

        public Locacao FindById(int id)
        {
            var locacao = _context.Locacao
                .Include(obj => obj.Veiculo)
                .Include(obj => obj.Cliente)
                .FirstOrDefault(obj => obj.Id == id);

            if (locacao == null)
            {
                throw new UserException("Locação não foi encontrada");
            }

            return locacao;
        }

        public Veiculo FindVeiculoById(int id)
        {
            var veiculo = _context.Veiculo
                .FirstOrDefault(obj => obj.Id == id);

            if (veiculo == null)
            {
                throw new UserException("Veiculo não foi encontrado");
            }

            return veiculo;
        }

        public Cliente FindClienteByEmail(string email)
        {
            var cliente = _context.Cliente
                .FirstOrDefault(obj => obj.Email == email);

            if (cliente == null)
            {
                throw new UserException("E-mail não encontrado! Favor verificar se o cliente está cadastrado no sistema.");
            }

            return cliente;
        }

        public int FindAllByCliente(Cliente cliente)
        {
            var locacao = _context.Locacao
                .Where(x => x.Cliente.Id == cliente.Id)
                .ToList();

            return locacao.Count;
        }

        public int CalculateRentalTime(Locacao locacao)
        {
            TimeSpan date = (locacao.DataDevolucao - locacao.DataRetirada);

            return date.Days;
        }

        public void Insert(Locacao locacao)
        {
            Locacao _locacao = new()
            {
                Status = LocacaoStatus.Ativa,
                Valor = locacao.Valor,
                TempoAluguelDias = locacao.TempoAluguelDias,
                DataRetirada = locacao.DataRetirada,
                DataDevolucao = locacao.DataDevolucao,
                DataAtiva = DateTime.Now,
                ClienteId = locacao.Cliente.Id,
                VeiculoId = locacao.Veiculo.Id
            };

            Veiculo veiculo = FindVeiculoById(_locacao.VeiculoId);
            veiculo.Alugado = true;
            veiculo.DataAtualizado = DateTime.Now;

            _context.Veiculo
                .Update(veiculo)
                .Property(x => x.DataCadastro).IsModified = false;

            _context.Locacao.Add(_locacao);
            _context.SaveChanges();
        }
    }
}
