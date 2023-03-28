using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2Iniciales.Models;
using PracticaMvcCore2Iniciales.Repositories;

namespace PracticaMvcCore2Iniciales.Controllers
{
    public class LibroController : Controller
    {
        private LibrosRepository repo;

        public LibroController(LibrosRepository repo )
        {
            this.repo = repo;
           
        }

        public IActionResult Libro()
        {
            List<Libro> libros = this.repo.GetLibros();
            return View(libros);
        }

        public IActionResult LibroGenero(int idgenero)
        {
            List<Libro> libros = this.repo.GetLibroGenero(idgenero);
            return View(libros);
        }

        public async Task<IActionResult> Details(int idlibro)
        {
            Libro libros = await this.repo.GetLibroID(idlibro);
            return View(libros);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
