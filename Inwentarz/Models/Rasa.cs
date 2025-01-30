using System.ComponentModel.DataAnnotations;

namespace Inwentarz.Models
{
    public class Rasa
    {
        [Key]
        public int RasyId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Gatunek { get; set; }

        [Required]
        [MaxLength(50)]
        public string NazwaRasy { get; set; }

        public string? Opis { get; set; }
    }
}
