using CarRentalCompany.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalCompany.Models
{
    public class Locacao
    {
        public int Id { get; set; }

        public LocacaoStatus Status { get; set; }

        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        [Display(Name = "Tempo do Aluguel em dias")]
        public int TempoAluguelDias { get; set; }

        [Display(Name = "Data Retirada")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime DataRetirada { get; set; }

        [Display(Name = "Data Devolução")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime DataDevolucao { get; set; }

        [Display(Name = "Data Ativa")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime DataAtiva { get; set; }

        [Display(Name = "Data Finalizada")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime? DataFinalizada { get; set; }

        [Display(Name = "Data Cancelada")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", NullDisplayText = "<i class='text-danger'>(NULL)<i>", HtmlEncode = false)]
        public DateTime? DataCancelada { get; set; }

        public Cliente? Cliente { get; set; }

        public int ClienteId { get; set; }

        [Display(Name = "Veículo")]
        public Veiculo? Veiculo { get; set; }

        public int VeiculoId { get; set; }

        public Locacao()
        {
        }

        public Locacao(int id, LocacaoStatus status, decimal valor, DateTime dataRetirada, DateTime dataDevolucao, DateTime dataAtiva, DateTime dataFinalizada, DateTime? dataCancelada, Cliente cliente, Veiculo veiculo)
        {
            Id = id;
            Status = status;
            Valor = valor;
            DataRetirada = dataRetirada;
            DataDevolucao = dataDevolucao;
            DataAtiva = dataAtiva;
            DataFinalizada = dataFinalizada;
            DataCancelada = dataCancelada;
            Cliente = cliente;
            Veiculo = veiculo;
        }
    }
}
