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
        [BindProperty]
        public WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models.asociar_stock Registro_Ingreso { get; set; }

        RegistroLibro_Context context_libro = new RegistroLibro_Context();
        RegistroCliente_Context context_cliente = new RegistroCliente_Context();
        RegistroIngreso_Context context_ingreso = new RegistroIngreso_Context();

        public ActionResult Index()
        {
            return View();
        }
        public IActionResult AgregarLibro()
        {
            try
            {
                var resultados = context_libro.Registros_Libro.ToList();
                int numero = 0;
                if (resultados != null)
                {
                    numero = 10 + resultados.Count;
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
            }catch (Exception e) { Console.WriteLine("Error Obtener datos de libro: " + e); }


            return View();
        }
        public IActionResult RegistrarLibro()
        {
            try
            {
                if (Registro_Libro.Codigo_Libro < 100)
                {
                    context_libro.Registros_Libro.Add(Registro_Libro);
                    context_libro.SaveChanges();
                }
                
            }
            catch (Exception e) { Console.WriteLine("RegistrarLibro Error: "+ e); }

            return RedirectToAction("AgregarLibro", "Administrador");
        }
        public IActionResult AgregarCliente()
        {
            try
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
            }catch (Exception e) { Console.WriteLine("Error Obtener datos de cliente: " + e); }
            return View();
        }
        public IActionResult RegistrarCliente()
        {
            if (Registro_Cliente.Codigo_Cliente < 100000)
            {
                try
                {
                    context_cliente.Registros_Cliente.Add(Registro_Cliente);
                    context_cliente.SaveChanges();
                }
                catch (Exception e) { Console.WriteLine("RegistrarCliente Error: " + e); }
            }
            return RedirectToAction("AgregarCliente", "Administrador");
        }
        public IActionResult ConsultaStock()
        {
            return View();
        }
        public IActionResult AgregarIngreso()
        {
            return View();
        }
        public IActionResult RegistrarIngreso()
        {
            try 
            {
                //Console.WriteLine("->"+ Registro_Ingreso.Precio_Articulo);
                Registro_Ingreso.Precio_Articulo= (float)Math.Round(Registro_Ingreso.Precio_Articulo * 100f) / 100f;
                context_ingreso.Registros_Ingreso.Add(Registro_Ingreso);
                context_ingreso.SaveChanges();

                string ventana_alerta = "<div class=*bloquear-alerta*><div class=*ventana-alertas*>" +
                                            "<div class=*mensaje-ventana-alertas*>¿Quiere agregar mas libros a custodia?</div>" +
                                            "<div class=*boton-ventana-alertas espacio* onclick=*cerrarAlerta()*>Si</div><div class=*boton-ventana-alertas* onclick=*aInicioIndex()*>No</div>" +
                                        "</div></div>";
                ventana_alerta = ventana_alerta.Replace("*", "'");
                TempData["Alert-Alert"] = ventana_alerta;
            }catch (Exception e) { Console.WriteLine("Error Registo Ingreso: "+e); }
            return RedirectToAction("AgregarIngreso", "Administrador");
        }

        //Funciones Consulta online
        public string ConsultaInmediataCodigoLibro(int id)
        {
            string string_retorno = "null";
            Models.asociar_libro esqueleto = new Models.asociar_libro();

            var resultados = context_libro.Registros_Libro.ToList();

            if (resultados != null)
            {
                bool encontrado = false;

                foreach (Models.asociar_libro valor in resultados)
                {
                    if (valor.Codigo_Libro == id)
                    {
                        encontrado = true;
                        esqueleto = valor;
                        break;
                    }
                }

                if (encontrado == true)
                {
                    string_retorno =
                        "{" +
                        "*Codigo_Libro*:*" + esqueleto.Codigo_Libro + "*" +
                        "}";
                    string_retorno = string_retorno.Replace('*', '"');
                }
            }

            return (string_retorno);
        }
        public string ConsultaInmediataCodigoCliente(int id)
        {
            string string_retorno = "null";
            Models.asociar_cliente esqueleto = new Models.asociar_cliente();

            var resultados = context_cliente.Registros_Cliente.ToList();

            if (resultados != null)
            {
                bool encontrado = false;

                foreach (Models.asociar_cliente valor in resultados)
                {
                    if (valor.Codigo_Cliente == id)
                    {
                        encontrado = true;
                        esqueleto = valor;
                        break;
                    }
                }

                if (encontrado == true)
                {
                    string_retorno = "{" +
                        "*Codigo_Cliente*:*" + esqueleto.Codigo_Cliente + "*" +
                        "}";
                    string_retorno = string_retorno.Replace('*', '"');
                }
            }
            return (string_retorno);
        }
    }
}
