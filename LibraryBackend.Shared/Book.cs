using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBackend.Shared;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [MaxLength(100, ErrorMessage = "Max length is 100 characters.")]
    [Required]
    public string Title { get; set; }

    [MaxLength(25, ErrorMessage = "Max length is 25 characters.")]
    [Required]
    public string Author { get; set; }

    [MaxLength(50, ErrorMessage = "Max length is 50 characters.")]
    [Required]
    public string Publisher { get; set; }

    // TODO: 
    [CustomValidation(typeof(Book), "ValidateYearOfPublication")]
    [Required]
    public DateTime YearOfPublication { get; set; }


    public static ValidationResult ValidateYearOfPublication(DateTime yearOfPublication, ValidationContext validationContext)
    {
        if (yearOfPublication < DateTime.MinValue || yearOfPublication > DateTime.MaxValue)
        {
            return new ValidationResult("Error at Book: Year Of Publication. Out of range.", [ validationContext.MemberName ]);
        }

        return ValidationResult.Success;
    }
}
