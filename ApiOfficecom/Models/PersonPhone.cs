using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PersonPhone
{
    [Key, Column(Order = 0)]
    public int BusinessEntityID { get; set; }

    [Required, StringLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Key, Column(Order = 1)]
    [Required] // Adicione este atributo se desejar garantir que o ID não pode ser nulo
    public int PhoneNumberTypeID { get; set; }
}
