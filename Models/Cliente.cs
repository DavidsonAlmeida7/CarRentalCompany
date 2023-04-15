using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalCompany.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(TypeName = "date")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(1200.00, 9000000.00, ErrorMessage = "{0} tem que ser entre {1} e {2}")]
        [Display(Name = "Salário")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalarioBase { get; set; }

        [Display(Name = "Data de Criação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Data de Atualização")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", NullDisplayText = "<i class='text-danger'>(NULL)<i>", HtmlEncode = false)]
        public DateTime? DataAtualizado { get; set; }

        public List<Locacao>? ListLocacao { get; set; }

        public Cliente()
        {
        }

        public Cliente(int id, string nome, string email, DateTime dataNascimento, decimal salarioBase, DateTime dataCadastro, DateTime dataAtualizado)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            SalarioBase = salarioBase;
            DataCadastro = dataCadastro;
            DataAtualizado = dataAtualizado;
        }
    }
}
