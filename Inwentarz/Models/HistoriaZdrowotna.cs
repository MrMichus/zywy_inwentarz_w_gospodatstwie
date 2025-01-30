using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inwentarz.Models
{
    public class HistoriaZdrowotna
    {
        [Key]
        public int RekordId { get; set; }

        public int? IdentyfikatorZwierzecia { get; set; }

        [MaxLength(255)]
        public string? Diagnoza { get; set; }

        public DateTime? DataDiagnozy { get; set; }

        public int? LeczenieId { get; set; }

        public string? Opis { get; set; }

        [ForeignKey("IdentyfikatorZwierzecia")]
        public virtual Zwierze? Zwierze { get; set; }

        [ForeignKey("LeczenieId")]
        public virtual ZabiegWeterynaryjny? Leczenie { get; set; }
    }
}
