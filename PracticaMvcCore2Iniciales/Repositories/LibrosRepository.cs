using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2Iniciales.Data;
using PracticaMvcCore2Iniciales.Models;

namespace PracticaMvcCore2Iniciales.Repositories
{
    public class LibrosRepository
    {
        private LibrosContext context;

        public LibrosRepository(LibrosContext context) 
        { 
            this.context = context;

        }

        public async Task<List<Genero>> GetGenero()
        {
            return await this.context.Generos.ToListAsync();
        }

        public async Task<Genero> GetGenero(int idgenero)
        {
            Genero genero = await this.context.Generos.
                FirstOrDefaultAsync(z => z.IdGenero == idgenero);
            return genero;
        }


        public List<Libro> GetLibros()
        {
            var consulta = from datos in this.context.Libros
                           select datos;
            return consulta.ToList();
        }

        public List<Libro> GetLibroGenero(int idgenero)
        {
            var consulta = from datos in this.context.Libros
                           where datos.IdGenero == idgenero
                           select datos;
            return consulta.ToList();
        }
        public async Task<Libro> GetLibroID(int idlibro)
        {
            Libro libro = await this.context.Libros.
                FirstOrDefaultAsync(z => z.IdLibro == idlibro);
            return libro;
        }

        //PAGINACION


        public int GetNumeroVistaLibros()
        {
            return this.context.VistaLibros.Count();
        }

        public async Task<List<VistaLibro>>
            GetVistaLibroAsync(int posicion)
        {

            var consulta = from datos in this.context.VistaLibros
                           where datos.Posicion >= posicion
                           && datos.Posicion < (posicion + 10)
                           select datos;
            return await consulta.ToListAsync();
        }


        //CARRITO LIBROS

        public Libro FindLbro(int idlibro)
        {
            return
                this.context.Libros.FirstOrDefault(x => x.IdLibro == idlibro);
        }
        public List<Libro> GetLibroCarrito(List<int> ids)
        {
            
            var consulta = from datos in this.context.Libros
                           where ids.Contains(datos.IdLibro)
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            return consulta.ToList();
        }

        public List<Libro> GetLibroSession(List<int> ids)
        {
         
            var consulta = from datos in this.context.Libros
                           where ids.Contains(datos.IdLibro)
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            return consulta.ToList();
        }


        //PEDIDOS

        public async Task InsertPedidoAsync
        (int idpedido, int idfactura, DateTime fecha , int idlibro,
           int idusuario, int cantidad)
        {

            Pedido pedido = new Pedido();

            pedido.IdPedido = idpedido;
            pedido.IdFactura = idfactura;
            pedido.Fecha = fecha;
            pedido.IdLibro = idlibro;
            pedido.IdUsuario = idusuario;
            pedido.Cantidad = cantidad;


            this.context.Pedidos.Add(pedido);

            await this.context.SaveChangesAsync();
        }

        //LOGIN
        public async Task<Usuario> ExisteUsuario
             (string email, string password)
        {
            var consulta =
                this.context.Usuarios.Where(x => x.Email == email
                && x.Pass == password);
            return await consulta.FirstOrDefaultAsync();
        }
    }
}
