using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Inwentarz.Models
{
    public class PlanZywieniowy
    {
        [Key]
        public int KarmienieId { get; set; }

        [MaxLength(50)]
        public string? PoraKarmienia { get; set; }

        [MaxLength(100)]
        public string? RodzajPaszy { get; set; }

        [Precision(10, 2)]
        public decimal? Ilosc { get; set; }
    }
}
