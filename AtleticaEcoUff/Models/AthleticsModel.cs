namespace AtleticaEcoUff.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AthleticsModel : DbContext
    {
        public AthleticsModel()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Athlete> Athletes { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>()
                .Property(e => e.athlete_name)
                .IsUnicode(false);

            modelBuilder.Entity<Sport>()
                .Property(e => e.sport_name)
                .IsUnicode(false);

            modelBuilder.Entity<Sport>()
                .HasMany(e => e.Athletes)
                .WithRequired(e => e.Sport)
                .WillCascadeOnDelete(false);
        }
    }
}
