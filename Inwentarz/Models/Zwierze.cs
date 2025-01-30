using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inwentarz.Models
{
    public class Zwierze
    {
        [Key]
        public int ZwierzeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Imie { get; set; }

        [MaxLength(50)]
        public string? Gatunek { get; set; }

        public int? Rasa { get; set; }

        public DateTime? DataUrodzenia { get; set; }

        [MaxLength(10)]
        public string? Plec { get; set; }

        public DateTime? PrzyjazdData { get; set; }

        [Precision(10, 2)]
        public decimal? Waga { get; set; }

        [MaxLength(100)]
        public string? StatusZdrowotny { get; set; }

        public int? OpiekunId { get; set; }

        [ForeignKey("Rasa")]
        public virtual Rasa? RasaObj { get; set; }

        [ForeignKey("OpiekunId")]
        public virtual Pracownik? Pracownik { get; set; }
    }
}
