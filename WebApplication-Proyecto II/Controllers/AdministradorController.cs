using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication_Proyecto_II__Morera_Vargas_Isaac.Controllers
{
    public class AdministradorController : Controller
    {

        [BindProperty]
        public WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models.asociar_libro Registro_Libro { get; set; }
        [BindProperty]
        public WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models.asociar_cliente Registro_Cliente { get; set; }
        RegistroLibro_Context context_libro = new RegistroLibro_Context();
        RegistroCliente_Context context_cliente = new RegistroCliente_Context();
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult AgregarLibro()
        {
            var resultados = context_libro.Registros_Libro.ToList();
            int numero = 0;            
            if (resultados != null)
            {
                numero = 10+resultados.Count;
            }
            else
            {
                numero = 10;
            }
            if (numero > 99)
            {
               
            }
            else
            {
                TempData["Codigo_Libro"] = numero;
            }
            
            return View();
        }
        public IActionResult RegistrarLibro(int orden)
        {
            try
            {
                if (Registro_Libro.Codigo_Libro < 100)
                {
                    context_libro.Registros_Libro.Add(Registro_Libro);
                    context_libro.SaveChanges();
                    string ventana_alerta = "<div class=*bloquear-alerta*><div class=*ventana-alertas*>" +
                                                "<div class=*mensaje-ventana-alertas*>¿Quiere agregar mas registros?</div>" +
                                                "<div class=*boton-ventana-alertas espacio* onclick=*cerrarAlerta()*>Si</div><div class=*boton-ventana-alertas* onclick=*aInicioIndex()*>No</div>" +
                                            "</div></div>";
                    ventana_alerta = ventana_alerta.Replace("*", "'");
                    TempData["Alert-Alert"] = ventana_alerta;
                }
                
            }
            catch (Exception e) { Console.WriteLine("RegistrarLibro Error: "+ e); }

            return RedirectToAction("AgregarLibro", "Administrador");
        }
        public string ConsultaInmediataCodigoLibro(int id)
        {
            string string_retorno = "null";
            /*
            var resultados = context_profesional.Registros_Profesional.ToList();

            if (resultados != null)
            {
                bool encontrado = false;

                foreach (Models.Profesional.asociar_profesional valor in resultados)
                {
                    if (valor.Identificacion_Profesional == id && valor.Codigo_Profesional == codigo)
                    {
                        encontrado = true;
                        esqueleto = valor;
                        break;
                    }
                }

                if (encontrado == true)
                {
                    
                }
            }
            */
            return (string_retorno);
        }

        public string ConsultaInmediataCodigoCliente(int id)
        {
            string string_retorno = "null";
            /*
            var resultados = context_profesional.Registros_Profesional.ToList();

            if (resultados != null)
            {
                bool encontrado = false;

                foreach (Models.Profesional.asociar_profesional valor in resultados)
                {
                    if (valor.Identificacion_Profesional == id && valor.Codigo_Profesional == codigo)
                    {
                        encontrado = true;
                        esqueleto = valor;
                        break;
                    }
                }

                if (encontrado == true)
                {
                    
                }
            }
            */
            return (string_retorno);
        }
        public IActionResult AgregarCliente()
        {
            var resultados = context_cliente.Registros_Cliente.ToList();
            int numero = 0;
            if (resultados != null)
            {
                numero = 10000 + resultados.Count;
            }
            else
            {
                numero = 10000;
            }
            if (numero > 99999)
            {

            }
            else
            {
                TempData["Codigo_Cliente"] = numero;
            }

            return View();
        }
        public IActionResult RegistrarCliente()
        {
            if (Registro_Libro.Codigo_Libro < 100000)
            {
                try
                {
                    context_cliente.Registros_Cliente.Add(Registro_Cliente);
                    context_cliente.SaveChanges();
                }
                catch (Exception e) { Console.WriteLine("RegistrarLibro Error: " + e); }
            }
            return RedirectToAction("AgregarCliente", "Administrador");
        }
        public IActionResult ConsultaStock()
        {
            return View();
        }
        public int GeneradorRandom(int min, int max)
        {
            Random rnd = new Random();
            int numero=rnd.Next(min, max + 1);
            return numero;
        }
    }
}
