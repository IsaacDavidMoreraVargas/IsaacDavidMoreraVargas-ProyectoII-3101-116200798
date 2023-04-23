using Microsoft.EntityFrameworkCore;

namespace WebApplication_Proyecto_II__Morera_Vargas_Isaac.Controllers
{
    public partial class RegistroLibro_Context : DbContext
    {

        public RegistroLibro_Context() { }
        public RegistroLibro_Context(DbContextOptions<RegistroLibro_Context> options) : base(options) { }

        public virtual DbSet<WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models.asociar_libro> Registros_Libro { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-RVLLMUB;Database=ProyectoII;Trusted_Connection=True;Encrypt=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models.asociar_libro>(entity =>
            {
                entity.ToTable("libro");

                entity.Property(e => e.Nombre_Empresa)
                    .HasMaxLength(30)
                    .IsUnicode(false);

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
