using System.ComponentModel.DataAnnotations;

namespace WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models
{
    public partial class asociar_libro
    {
        [Key]
        public int Llave_Libro { get; set; }

        public int Codigo_Libro { get; set; }

        public string Nombre_Libro { get; set; }

        public string Nombre_Empresa { get; set; }

    }
}
