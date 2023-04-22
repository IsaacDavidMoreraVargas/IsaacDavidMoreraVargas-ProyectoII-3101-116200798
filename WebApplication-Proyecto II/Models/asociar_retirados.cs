using System.ComponentModel.DataAnnotations;

namespace WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models
{
    public partial class asociar_retirados
    {
        [Key]
        public int Llave_retiro { get; set; }

        public int Codigo_Libro { get; set; }

        public string Nombre_Libro { get; set; }

        public string Descripcion_Articulo { get; set; }

        public int Codigo_Cliente { get; set; }

        public string Nombre_Empresa { get; set; }

        public string Fecha_Retiro { get; set; }
    }
}
