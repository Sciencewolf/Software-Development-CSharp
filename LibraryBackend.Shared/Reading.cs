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

    [Range(typeof(DateTime), "1900-01-01", "2020-01-01")]
    [Required]
    public DateTime BirthDate { get; set; }
}