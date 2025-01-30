using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inwentarz.Models
{
    public class InwentarzDbContext : IdentityDbContext
    {
        public InwentarzDbContext(DbContextOptions<InwentarzDbContext> options) : base(options) { }


        public DbSet<Pracownik> Pracownik { get; set; }
        public DbSet<Rasa> Rasa { get; set; }
        public DbSet<Zwierze> Zwierze { get; set; }
        public DbSet<ZabiegWeterynaryjny> ZabiegWeterynaryjny { get; set; }
        public DbSet<HistoriaZdrowotna> HistoriaZdrowotna { get; set; }
        public DbSet<PlanZywieniowy> PlanZywieniowy { get; set; }

    }
}
