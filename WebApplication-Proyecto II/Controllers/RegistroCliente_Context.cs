using Microsoft.EntityFrameworkCore;

namespace WebApplication_Proyecto_II__Morera_Vargas_Isaac.Controllers
{
    public partial class RegistroCliente_Context : DbContext
    {
        public RegistroCliente_Context() { }
        public RegistroCliente_Context(DbContextOptions<RegistroCliente_Context> options) : base(options) { }

        public virtual DbSet<WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models.asociar_cliente> Registros_Cliente { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-RVLLMUB;Database=ProyectoII;Trusted_Connection=True;Encrypt=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models.asociar_cliente>(entity =>
            {
                entity.ToTable("cliente");

                entity.Property(e => e.Nombre_Cliente)
                    .HasMaxLength(30)
                    .IsUnicode(false);

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
