using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class PhoneNumberType
{
    [Key]
    public int PhoneNumberTypeID { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public ICollection<PersonPhone> PersonPhones { get; set; } = new List<PersonPhone>();
}
