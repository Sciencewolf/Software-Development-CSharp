using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBackend.Shared;

public class Reading
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [MaxLength(50)]
    [Required]
    public string Name { get; set; }

    [MaxLength(100)]
    [Required]
    public string Address { get; set; }

    [CustomValidation(typeof(Reading), "ValidateDate")]
    [Required]
    public DateTime BirthDate { get; set; }

    public static ValidationResult ValidateDate(DateTime MinDate, ValidationContext validationContext)
    {
        var date = new DateTime(1900, 1, 1);
        if (MinDate < date)
        {
            return new ValidationResult("Error at Reading: BirthDate must be 1900/01/01 above", [validationContext.MemberName]);
        }
        return ValidationResult.Success;
    }
}