using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2Iniciales.Models;
using PracticaMvcCore2Iniciales.Repositories;

namespace PracticaMvcCore2Iniciales.Controllers
{
    public class PedidoController : Controller
    {
        private LibrosRepository repo;
        public PedidoController(LibrosRepository repo)
        {
            this.repo = repo;
            
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pedido pedido)
        {
            await this.repo.InsertPedidoAsync
                (pedido.IdPedido, pedido.IdFactura, pedido.Fecha, pedido.IdLibro, pedido.IdUsuario
                , pedido.Cantidad);
            return RedirectToAction("Mesa");
        }



        public IActionResult Index()
        {
            return View();
        }
    }
}
