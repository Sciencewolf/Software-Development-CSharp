using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBackend.Shared
{
    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid BookId { get; set; }

        // TODO: 
        [CustomValidation(typeof(Loan), "ValidateRentalDate")]
        [Required]
        public DateTime BorrowingDate { get; set; }

        [CustomValidation(typeof(Loan), "ValidateReturnDate")]
        [Required]
        public DateTime ReturnDeadLine { get; set; }

        public static ValidationResult ValidateRentalDate(DateTime rentalDate, ValidationContext validationContext)
        {
            if (rentalDate <= DateTime.Today)
            {
                return new ValidationResult("Error at Loan: RentalDate", [validationContext.MemberName]);
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidateReturnDate(DateTime returnDate, ValidationContext validationContext)
        {
            var rental = (Loan)validationContext.ObjectInstance;

            if (returnDate <= rental.BorrowingDate)
            {
                return new ValidationResult("Error at Loan: ReturnDate must be in future", [validationContext.MemberName]);
            }

            return ValidationResult.Success;
        }
    }
}
