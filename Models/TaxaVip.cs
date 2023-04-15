using CarRentalCompany.Services;

namespace CarRentalCompany.Models
{
    public class TaxaVip : ITaxa
    {
        private readonly Locacao _locacao;
        private readonly LocacaoService _locacaoService;

        private const decimal TAXA = 0.2m;

        public TaxaVip(Locacao locacao, LocacaoService locacaoService)
        {
            _locacao = locacao;
            _locacaoService = locacaoService;
        }

        public decimal Taxa()
        {
            int dias = _locacaoService.CalculateRentalTime(_locacao);
            Veiculo veiculo = _locacaoService.FindVeiculoById(_locacao.Veiculo.Id);

            decimal valor = veiculo.Valor / 100 * TAXA;
            decimal taxa = valor * dias;

            return taxa;
        }
    }
}
