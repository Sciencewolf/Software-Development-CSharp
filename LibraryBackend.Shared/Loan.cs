using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBackend.Shared
{
    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid BookId { get; set; }

        // TODO: 
        [CustomValidation(typeof(Loan), "ValidateRentalDate")]
        public DateTime BorrowingDate { get; set; }

        [CustomValidation(typeof(Loan), "ValidateReturnDate")]
        public DateTime ReturnDeadLine { get; set; }

        public static ValidationResult ValidateRentalDate(DateTime rentalDate, ValidationContext validationContext)
        {
            if (rentalDate < DateTime.Today)
            {
                return new ValidationResult("Error", new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidateReturnDate(DateTime returnDate, ValidationContext validationContext)
        {
            var rental = (Loan)validationContext.ObjectInstance;

            if (returnDate <= rental.BorrowingDate)
            {
                return new ValidationResult("Error", new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
