using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inwentarz.Models
{
    public class ZabiegWeterynaryjny
    {
        [Key]
        public int LeczenieId { get; set; }

        public int? ZwierzeId { get; set; }

        public DateTime? Data { get; set; }

        [MaxLength(100)]
        public string? RodzajStworzenia { get; set; }

        public string? Opis { get; set; }

        [MaxLength(100)]
        public string? Weterynarz { get; set; }

        [ForeignKey("ZwierzeId")]
        public virtual Zwierze? Zwierze { get; set; }
    }
}
