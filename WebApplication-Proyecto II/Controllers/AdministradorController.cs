using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication_Proyecto_II__Morera_Vargas_Isaac.Controllers
{
    public class AdministradorController : Controller
    {
        string correo_quien_envia = "";
        string contrasena_correo_enviador = "";
        string host_enviador = "";
        int puerto_enviador = 587;
        string operacion_correcta = "<div class=?ventana-alertas ventana-color_correcto?>";
        string operacion_incorrecta = "<div class=?ventana-alertas ventana-color_incorrecto?>";
        string operacion_cierre = "</div>";

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
                    correcto_incorrecto(0, "");
                }
                else
                {
                    correcto_incorrecto(3, "Numero maximo de registros alcanzados");
                }
                
            }
            catch (Exception e) { Console.WriteLine("RegistrarLibro Error: "+ e); correcto_incorrecto(1,""); }

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
                    correcto_incorrecto(3,"Error: cantidad  registro maximo alcanzado");
                }
                else
                {
                    TempData["Codigo_Cliente"] = numero;
                }
            }catch (Exception e) { Console.WriteLine("Error Obtener datos de cliente: " + e); correcto_incorrecto(1, ""); }
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
                    correcto_incorrecto(0, "");
                }
                catch (Exception e) { Console.WriteLine("RegistrarCliente Error: " + e); }
            }
            else
            {
                correcto_incorrecto(3, "Error: cantidad  registro maximo alcanzado");
            }
            return RedirectToAction("AgregarCliente", "Administrador");
        }
        public IActionResult ConsultaStock()
        {
            return View();
        }
        public IActionResult AgregarIngreso()
        {
            try
            {
                var libros = context_libro.Registros_Libro.ToList();
                if (libros != null)
                {
                    ViewBag.ListaCodigos = libros;
                }
            }
            catch (Exception e) { Console.WriteLine("Error obteniendo libros"); correcto_incorrecto(3, "Error obteniendo datos"); }

            try
            {
                var clientes = context_cliente.Registros_Cliente.ToList();
                if (clientes != null)
                {
                    ViewBag.ListaCliente = clientes;
                }
            }
            catch (Exception e) { Console.WriteLine("Error obteniendo clientes"); correcto_incorrecto(3, "Error obteniendo datos"); }

            TempData["Fecha_Actual"]= DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        public IActionResult RegistrarIngreso()
        {
            bool correcto = false;
            int correcto_codigo_cliente = 0;
            int correcto_codigo_libro = 0;
            string correo_quien_recibe = "";
            string subject = "";
            string message = "";
            try 
            {
                //Console.WriteLine("->"+ Registro_Ingreso.Precio_Articulo);

                correcto_codigo_cliente = Registro_Ingreso.Codigo_Cliente;
                correcto_codigo_libro = Registro_Ingreso.Codigo_Libro;
                message = Registro_Ingreso.Descripcion_Articulo+ "\n Fecha Ingreso: "+ Registro_Ingreso.Fecha_Ingreso;

                Registro_Ingreso.Precio_Articulo= (float)Math.Round(Registro_Ingreso.Precio_Articulo * 100f) / 100f;
                context_ingreso.Registros_Ingreso.Add(Registro_Ingreso);
                context_ingreso.SaveChanges();
                correcto = true;

                correcto_incorrecto(0, "");
                string ventana_alerta = "<div class=*bloquear-alerta*><div class=*ventana-alertas*>" +
                                            "<div class=*mensaje-ventana-alertas*>¿Quiere agregar mas libros a custodia?</div>" +
                                            "<div class=*boton-ventana-alertas espacio* onclick=*cerrarAlerta()*>Si</div><div class=*boton-ventana-alertas* onclick=*aInicioIndex()*>No</div>" +
                                        "</div></div>";
                ventana_alerta = ventana_alerta.Replace("*", "'");
                TempData["Alert-Alert"] = ventana_alerta;
                
            }
            catch (Exception e) { Console.WriteLine("Error Registo Ingreso: "+e); correcto_incorrecto(1, ""); }

            if (correcto == true)
            {
                try
                {
                    var cliente = context_cliente.Registros_Cliente.Find(correcto_codigo_cliente);
                    if (cliente != null)
                    {
                        correo_quien_recibe = cliente.Correo_Electronico;

                        var libro = context_libro.Registros_Libro.Find(correcto_codigo_libro);
                        if (libro != null)
                        {
                            subject = "Libro ingresado a tu nombre: " + libro.Nombre_Libro + ", Cliente: "+cliente.Codigo_Cliente;
                            message = "Nombre Empresa: " + libro.Nombre_Empresa + "\n" + "Descipcion: " + message;
                        }
                    }

                }catch (Exception f) { Console.WriteLine("Error encontrando cliente para correo"); correcto_incorrecto(3, "Error obteniendo datos para enviar correo"); }
                
                if (correcto == true)
                {
                    Enviar_Email(correo_quien_envia, correo_quien_recibe, subject, message, contrasena_correo_enviador, host_enviador, puerto_enviador);
                }
            }
            

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
            catch (Exception e) { Console.WriteLine("Error en ConsultaReporte: "+ e); correcto_incorrecto(3, "Error obteniendo datos"); }
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

        [HttpPost]
        public void Enviar_Email(string correo_quien_envia, string correo_quien_recibe, string subject, string message, string contrasena, string a_host, int puerto)
        {
            try
            {
                var senderEmail = new MailAddress(correo_quien_envia, "NAME");
                var receiverEmail = new MailAddress(correo_quien_recibe, "NAME");
                var password = contrasena;
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    //Host = "smtp.gmail.com",
                    Host = a_host,
                    //Port = 587,
                    Port = puerto,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }

                correcto_incorrecto(2, "Email enviado exitosamente");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error enviando email, Razon: " + e); correcto_incorrecto(3, "Error enviando email");
            }
        }
    
        public void correcto_incorrecto(int numero, string mensaje)
        {
            string crear_alert = "";
            switch (numero)
            {
                case 0:
                    crear_alert = operacion_correcta + "Operacion Exitosa" + operacion_cierre;
                    break;
                case 1:
                    crear_alert = operacion_incorrecta + "Operacion No Exitosa" + operacion_cierre;
                    break;

                case 2:
                    crear_alert = operacion_correcta + mensaje + operacion_cierre;
                    break;
                case 3:
                    crear_alert = operacion_incorrecta + mensaje + operacion_cierre;
                    break;
            }
            crear_alert = crear_alert.Replace('?', '"');
            TempData["Alert-Alert"] = crear_alert;
        }
    }
}
