using System.ComponentModel.DataAnnotations;

namespace WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models
{
    public partial class asociar_cliente
    {
        [Key]
        public int Codigo_Cliente { get; set; }

        public string Nombre_Cliente { get; set; }

        public int Numero_Identificacion { get; set; }

        public string Fecha_Nacimiento { get; set; }
    }
}
