using System.ComponentModel.DataAnnotations;

namespace CarRentalCompany.Models.Enums
{
    public enum VeiculoTipo : int
    {
        [Display(Name = "Conversível")]
        Conversivel = 1, 
        Sedan = 2, 
        Hatch = 3,
        [Display(Name = "SUV")]
        Suv = 4, 
        Picape = 5, 
        Outros = 6
    }
}
