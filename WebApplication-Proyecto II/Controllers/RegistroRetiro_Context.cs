using Microsoft.EntityFrameworkCore;

namespace WebApplication_Proyecto_II__Morera_Vargas_Isaac.Controllers
{
    public partial class RegistroRetiro_Context : DbContext
    {
        public RegistroRetiro_Context() { }
        public RegistroRetiro_Context(DbContextOptions<RegistroRetiro_Context> options) : base(options) { }

        public virtual DbSet<WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models.asociar_retirados> Registros_Retiro { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-RVLLMUB;Database=ProyectoII;Trusted_Connection=True;Encrypt=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models.asociar_retirados>(entity =>
            {
                entity.ToTable("retirados");

                entity.Property(e => e.Descripcion_Articulo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
