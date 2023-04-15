using CarRentalCompany.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalCompany.Models
{
    public class Veiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        public VeiculoTipo Tipo { get; set; }

        public string Modelo { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Ano")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}")]
        [Column(TypeName = "date")]
        public DateTime Ano { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EnumDataType(typeof(VeiculoCor))]
        public VeiculoCor Cor { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        [StringLength(255)]
        [Display(Name = "Detalhes")]
        public string? DescricaoDetalhes { get; set; }

        [Display(Name = "Imagem")]
        public string? Imagem { get; set; }

        [Display(Name = "Importar imagem")]
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Data de Criação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Data de Atualização")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", NullDisplayText = "<i class='text-danger'>(NULL)<i>", HtmlEncode = false)]
        public DateTime? DataAtualizado { get; set; }

        public bool Alugado { get; set; } = false;

        public Veiculo()
        {
        }

        public Veiculo(int id, VeiculoTipo conversivel, string modelo, DateTime ano, decimal valor, string? descricaoDetalhes, string? imagem, IFormFile? imageFile)
        {
            Id = id;
            Modelo = modelo;
            Ano = ano;
            Valor = valor;
            DescricaoDetalhes = descricaoDetalhes;
            Imagem = imagem;
            ImageFile = imageFile;
        }

        public int Somar(int n1, int n2)
        {
            return n1 + n2;
        }
    }
}
