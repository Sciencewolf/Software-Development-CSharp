using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBackend;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(100)]
    public string Author { get; set; }

    [MaxLength(100)]
    public string Publisher { get; set; }

    // TODO: 
    [CustomValidation(typeof(Book), "ValidateYearOfPublication")]
    public DateTime YearOfPublication { get; set; }


    public static ValidationResult ValidateYearOfPublication(DateTime yearOfPublication, ValidationContext validationContext)
    {
        if (yearOfPublication < DateTime.MinValue || yearOfPublication > DateTime.MaxValue)
        {
            return new ValidationResult("Out of range.", new[] { validationContext.MemberName });
        }

        return ValidationResult.Success;
    }
}
