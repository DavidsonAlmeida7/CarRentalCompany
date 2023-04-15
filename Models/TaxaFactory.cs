using CarRentalCompany.Services;

namespace CarRentalCompany.Models
{
    public class TaxaFactory
    {
        private readonly LocacaoService _locacaoService;
        private readonly Locacao _locacao;

        public TaxaFactory(Locacao locacao, LocacaoService locacaoService)
        {
            _locacao = locacao;
            _locacaoService = locacaoService;
        }

        public ITaxa Make()
        {
            if (this.ChecksIfThereIsMoreThanOneRentalMade(_locacao.Cliente))
            {
                return new TaxaVip(_locacao, _locacaoService);
            }

            return new TaxaPattern(_locacao, _locacaoService);
        }

        private bool ChecksIfThereIsMoreThanOneRentalMade(Cliente cliente)
        {
            int count = _locacaoService.FindAllByCliente(cliente);

            if (count >= 2)
            {
                return true;
            }

            return false;
        }
    }
}
