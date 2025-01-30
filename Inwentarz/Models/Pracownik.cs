using System.ComponentModel.DataAnnotations;
using Inwentarz.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inwentarz.Models
{
    public class Pracownik
    {
        [Key]
        public int IdPracownika { get; set; }

        [Required]
        [MaxLength(50)]
        public string Imie { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nazwisko { get; set; }

        [Required]
        [MaxLength(50)]
        public string Stanowisko { get; set; }

        [MaxLength(12)]
        [Phone]
        public string? Telefon { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        public DateOnly? DataZatrudnienia { get; set; }



        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
