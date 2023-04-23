using Microsoft.EntityFrameworkCore;

namespace WebApplication_Proyecto_II__Morera_Vargas_Isaac.Controllers
{
    public partial class RegistroIngreso_Context : DbContext
    {

        public RegistroIngreso_Context() { }
        public RegistroIngreso_Context(DbContextOptions<RegistroIngreso_Context> options) : base(options) { }

        public virtual DbSet<WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models.asociar_stock> Registros_Ingreso { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-RVLLMUB;Database=ProyectoII;Trusted_Connection=True;Encrypt=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models.asociar_stock>(entity =>
            {
                entity.ToTable("stock");

                entity.Property(e => e.Descripcion_Articulo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
