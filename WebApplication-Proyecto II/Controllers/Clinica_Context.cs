using Microsoft.EntityFrameworkCore;
using WebApplication_Proyecto_I.Controllers.Profesional;

namespace WebApplication_Proyecto_I.Controllers.Clinica
{
    public partial class Clinica_Context : DbContext
    {
        public Clinica_Context() { }
        public Clinica_Context(DbContextOptions<Clinica_Context> options) : base(options) { }

        public virtual DbSet<WebApplication_Proyecto_I.Models.Clinica.asociar_clinica> Registros_Clinica { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-RVLLMUB;Database=ProyectoI;Trusted_Connection=True;Encrypt=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WebApplication_Proyecto_I.Models.Clinica.asociar_clinica>(entity =>
            {
                entity.ToTable("datosClinica");

                entity.Property(e => e.Nombre_Clinica)
                    .HasMaxLength(300)
                    .IsUnicode(false);

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
