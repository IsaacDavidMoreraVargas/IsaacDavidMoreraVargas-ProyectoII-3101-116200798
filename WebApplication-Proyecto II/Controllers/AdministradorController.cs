using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

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
        [BindProperty]
        public WebApplication_Proyecto_II__Morera_Vargas_Isaac.Models.asociar_retirados Registro_Retiro{ get; set; }

        RegistroLibro_Context context_libro = new RegistroLibro_Context();
        RegistroCliente_Context context_cliente = new RegistroCliente_Context();
        RegistroIngreso_Context context_ingreso = new RegistroIngreso_Context();
        RegistroRetiro_Context context_retiro = new RegistroRetiro_Context();
        public ActionResult Index()
        {
            return View();
        }
        public IActionResult AgregarLibro()
        {
            //TempData["Nombre_Empresa"] = "Libreria la Internacional";
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
            TempData["Fecha_Actual"]= DateTime.Now.ToString("yyyy-MM-dd");
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
        public IActionResult AgregarRetiro()
        {
            return View();
        }
        public IActionResult ConsultaReporte()
        {
            try 
            {   
                var clientes= context_cliente.Registros_Cliente.ToList();
                if(clientes!=null)
                {
                    ViewBag.ListaClientes = clientes;
                }

                var stock = context_ingreso.Registros_Ingreso.ToList();
                if (stock != null)
                {
                    ViewBag.ListaStock = stock;
                }
                }
            catch (Exception e) { Console.WriteLine("Error en ConsultaReporte: "+ e); }
            return View();
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
        public string ConsultaStockDeClienteDisponible(int id)
        {

            string string_retorno_final = "null";
            string string_retorno = "";

            var resultados = context_ingreso.Registros_Ingreso.ToList();
            if (resultados != null)
            {
                int numero = 0;
                bool encontrado = false;
                
                foreach (Models.asociar_stock valorStock in resultados)
                {
                    if (valorStock.Codigo_Cliente == id)
                    {
                        encontrado = true;
                        string_retorno += "*"+numero+"*:{";
                        string_retorno += "*Llave_Libro*:*" + valorStock.Llave_Libro + "*,";
                        string_retorno += "*Codigo_Libro*:*"+ valorStock.Codigo_Libro+ "*,";
                        string_retorno += "*Descripcion_Articulo*:*" + valorStock.Descripcion_Articulo + "*,";
                        double x = Math.Truncate(valorStock.Precio_Articulo * 100) / 100;
                        string_retorno += "*Precio_Articulo*:*" + x + "*,";
                        string_retorno += "*Codigo_Cliente*:*" + valorStock.Codigo_Cliente + "*,";
                        string_retorno += "*Fecha_Ingreso*:*" + valorStock.Fecha_Ingreso + "*";
                        string_retorno += "},";
                        ++numero;
                    }
                }
                if (encontrado == true)
                {
                    string_retorno = string_retorno.Substring(0, string_retorno.Length - 1);
                    string_retorno = string_retorno.Replace('*','"');
                    string_retorno_final = "{" + string_retorno + "}";
                }
            }
            
            return (string_retorno_final);
        }
        public string ConsultaStockDeClienteNoDisponible(int id)
        {
            string string_retorno_final = "null";
            string string_retorno = "";

            var resultados = context_retiro.Registros_Retiro.ToList();
            if (resultados != null)
            {
                int numero = 0;
                bool encontrado = false;

                foreach (Models.asociar_retirados valorStock in resultados)
                {
                    if (valorStock.Codigo_Cliente == id)
                    {
                        encontrado = true;
                        string_retorno += "*" + numero + "*:{";
                        string_retorno += "*Llave_Libro*:*" + valorStock.Llave_retiro + "*,";
                        string_retorno += "*Codigo_Libro*:*" + valorStock.Codigo_Libro + "*,";
                        string_retorno += "*Nombre_Libro*:*" + valorStock.Nombre_Libro + "*,";
                        string_retorno += "*Descripcion_Articulo*:*" + valorStock.Descripcion_Articulo + "*,";
                        string_retorno += "*Codigo_Cliente*:*" + valorStock.Codigo_Cliente + "*,";
                        string_retorno += "*Fecha_Retiro*:*" + valorStock.Fecha_Retiro + "*";
                        string_retorno += "},";
                        ++numero;
                    }
                }

                if (encontrado == true)
                {
                    string_retorno = string_retorno.Substring(0, string_retorno.Length - 1);
                    string_retorno = string_retorno.Replace('*', '"');
                    string_retorno_final = "{" + string_retorno + "}";
                }
            }

            return (string_retorno_final);
        }
        public string ConsultaCambioEstado(string valor)
        {
            string[] dividir = valor.Split("*");
            int id = Int32.Parse(dividir[0]);
            Models.asociar_stock esqueletoStock = new Models.asociar_stock();
            Models.asociar_retirados esqueletoRetirados = new Models.asociar_retirados();
            string string_retorno = "null";
            try
            {
                var resultados = context_ingreso.Registros_Ingreso.ToList();
                if (resultados != null)
                {
                    foreach (Models.asociar_stock valorStock in resultados)
                    {
                        if (valorStock.Llave_Libro == id)
                        {
                            var resultadosLibros = context_libro.Registros_Libro.ToList();
                            if (resultadosLibros != null)
                            {
                                foreach (Models.asociar_libro libro in resultadosLibros)
                                {
                                    if(libro.Codigo_Libro== valorStock.Codigo_Libro)
                                    {
                                    
                                        esqueletoRetirados.Llave_retiro = valorStock.Llave_Libro;
                                        esqueletoRetirados.Codigo_Libro = valorStock.Codigo_Libro;
                                        esqueletoRetirados.Nombre_Libro = libro.Nombre_Libro;
                                        esqueletoRetirados.Descripcion_Articulo = valorStock.Descripcion_Articulo;
                                        esqueletoRetirados.Codigo_Cliente = valorStock.Codigo_Cliente;
                                        esqueletoRetirados.Fecha_Retiro= dividir[1];
                                        context_retiro.Registros_Retiro.Add(esqueletoRetirados);
                                        context_retiro.SaveChanges();

                                        context_ingreso.Remove(valorStock);
                                        context_ingreso.SaveChanges();
                                        string_retorno = "exitoso";
                                        break;
                                    }
                                }  
                            }
                            break;
                        }
                    }
                }
            }catch (Exception e) { }
            return (string_retorno);
        }
    }
}
