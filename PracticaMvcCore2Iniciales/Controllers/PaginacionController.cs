using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2Iniciales.Models;
using PracticaMvcCore2Iniciales.Repositories;

namespace PracticaMvcCore2Iniciales.Controllers
{
    public class PaginacionController : Controller
    {
        private LibrosRepository repo;

        public PaginacionController(LibrosRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult>
            PaginarVistaLibros(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int numeroregistros = this.repo.GetNumeroVistaLibros();
            ViewData["REGISTROS"] = numeroregistros;
            List<VistaLibro> departamentos =
                await this.repo.GetVistaLibroAsync(posicion.Value);
            return View(departamentos);
        }

    }
}
