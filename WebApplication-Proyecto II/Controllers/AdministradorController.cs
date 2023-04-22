using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication_Proyecto_II__Morera_Vargas_Isaac.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: AdministradorController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult AgregarLibro()
        {
            //TempData["Codigo_Libro"] = GeneradorRandom(0, 98);
            return View();
        }

        public string ConsultaInmediataCodigo(int id)
        {
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
            return (string_retorno);
        }

        public IActionResult AgregarCliente()
        {
            return View();
        }

        public IActionResult ConsultaStock()
        {
            return View();
        }

        // POST: AdministradorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public int GeneradorRandom(int min, int max)
        {
            Random rnd = new Random();
            int numero=rnd.Next(min, max + 1);
            return numero;
        }
    }
}
