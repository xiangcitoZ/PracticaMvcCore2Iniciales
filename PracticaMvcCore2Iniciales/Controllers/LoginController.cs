using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2Iniciales.Models;
using PracticaMvcCore2Iniciales.Repositories;
using System.Security.Claims;


namespace TiendaPractica.Controllers
{
    public class LoginController : Controller
    {
        private LibrosRepository repo;
        public LoginController(LibrosRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn
            (string username, string password)
        {
            Usuario usuario =
                await this.repo.ExisteUsuario(username, password);
            if (usuario != null)
            {
                ClaimsIdentity identity =
                    new ClaimsIdentity
                    (CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);

                Claim claimName = new Claim(ClaimTypes.Name, username);
                identity.AddClaim(claimName);

                Claim claimId = new Claim(ClaimTypes.NameIdentifier,
                    usuario.Pass);
                identity.AddClaim(claimId);

                Claim claimOfICIO = new Claim(ClaimTypes.Role,
                   usuario.Nombre);
                identity.AddClaim(claimOfICIO);

                Claim claimSalario = new Claim("Apellidos",
                 usuario.Apellidos);
                identity.AddClaim(claimSalario);

                Claim claimDepartamento = new Claim("Foto",
                   usuario.Foto);
                identity.AddClaim(claimDepartamento);

                ClaimsPrincipal userPrincipal =
                    new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme
                    , userPrincipal);
                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();


                return RedirectToAction("PaginarVistaLibros", "Paginacion");
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }

        public IActionResult ErrorAcceso()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("PerfilEmpleado", "Empleados");
        }
    }
}
