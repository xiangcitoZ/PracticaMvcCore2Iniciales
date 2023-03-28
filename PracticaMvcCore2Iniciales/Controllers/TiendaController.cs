using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PracticaMvcCore2Iniciales.Models;
using PracticaMvcCore2Iniciales.Repositories;
using PracticaMvcCore2Iniciales.Extensions;

namespace PracticaMvcCore2Iniciales.Controllers
{
    public class TiendaController : Controller
    {
        private LibrosRepository repo;
        private IMemoryCache memoryCache;
        public TiendaController(LibrosRepository repo
            , IMemoryCache memoryCache)
        {
            this.repo = repo;
            this.memoryCache = memoryCache;
        }

        public IActionResult CarritoLibros(int? idlibro
            )
        {
           


            if (idlibro != null)
            {
                List<int> idsLibro;
                if (HttpContext.Session.GetObject<List<int>>("IDSLIBROS") == null)
                {
                    //CREAMOS LA LISTA PARA LOS IDS
                    idsLibro = new List<int>();
                }
                else
                {
                    //RECUPERAMOS LOS IDS ALMACENADOS PREVIAMENTE
                    idsLibro =
                        HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
                }
                idsLibro.Add(idlibro.Value);
                HttpContext.Session.SetObject("IDSLIBROS", idsLibro);
                ViewData["MENSAJE"] = "Libros almacenados: "
                    + idsLibro.Count;
            }
            List<Libro> libros = this.repo.GetLibros();
            return RedirectToAction("PaginarVistaLibros","Paginacion");
        }


        public IActionResult CarritoAlmacenado(int? ideliminar)
        {
            //RECUPERAMOS LOS DATOS DE SESSION
            List<int> idsLibro =
                HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
            if (idsLibro == null)
            {
                //NO HAY NADA EN SESSION
                ViewData["MENSAJE"] = "No existen LIBROS almacenados";
                //DEVOLVEMOS LA VISTA SIN MODEL
                return View();
            }
            else
            {
                if (ideliminar != null)
                {
                    //ELIMINAMOS EL ELEMENTO QUE NOS HAN SOLICITADO
                    idsLibro.Remove(ideliminar.Value);
                    if (idsLibro.Count == 0)
                    {
                        HttpContext.Session.Remove("IDSLIBROS");
                    }
                    else
                    {
                        //DEBEMOS ACTUALIZAR DE NUEVO SESSION
                        HttpContext.Session.SetObject("IDSLIBROS", idsLibro);
                    }
                }
                List<Libro> empleadosSession =
                    this.repo.GetLibroSession(idsLibro);
                return View(empleadosSession);
            }
        }





        //public IActionResult CarritoLibros(int? idlibro)
        //{
        //    if (idlibro != null)
        //    {

        //        Libro libro = this.repo.FindLbro(idlibro.Value);

        //        List<Libro> librossession;
        //        if (HttpContext.Session.GetObject<List<Libro>>("LIBROS") != null)
        //        {
        //            librossession =
        //                HttpContext.Session.GetObject<List<Libro>>("LIBROS");
        //        }
        //        else
        //        {

        //            librossession = new List<Libro>();
        //        }

        //        librossession.Add(libro);

        //        HttpContext.Session.SetObject("LIBROS", librossession);
        //        ViewData["MENSAJE"] = "Libro " + libro.Titulo
        //            + " almacenado en Session.";
        //    }
        //    List<Libro> libros = this.repo.GetLibros();
        //    return View(libros);
        //}

        //public IActionResult CarritoAlmacenado()
        //{
        //    return View();
        //}

    }
}
