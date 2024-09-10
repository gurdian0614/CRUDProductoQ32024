

using CRUDProductoQ32024.Models;
using SQLite;

namespace CRUDProductoQ32024.Services
{
    public class ProductoService
    {
        private readonly SQLiteConnection DbConnection;

        public ProductoService()
        {
            string DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Producto.db3");

            DbConnection = new SQLiteConnection(DbPath);
            DbConnection.CreateTable<Producto>();
        }

        /// <summary>
        /// Lista todos los productos
        /// </summary>
        /// <returns>Listado de productos</returns>
        public List<Producto> GetAll()
        {
            var res = DbConnection.Table<Producto>().ToList();
            return res;
        }

        /// <summary>
        /// Crea un registro en la base de datos
        /// </summary>
        /// <param name="Producto">Objeto con datos del producto a ingresar</param>
        /// <returns>Cantidad de registros ingresados</returns>
        public int Insert(Producto Producto)
        {
            return DbConnection.Insert(Producto);
        }

        /// <summary>
        /// Actualiza un registro en la base de datos
        /// </summary>
        /// <param name="Producto">Objeto con los datos del producto a actualizar</param>
        /// <returns>Cantidad de registros actualizados</returns>
        public int Update(Producto Producto) {
            return DbConnection.Update(Producto); 
        }

        /// <summary>
        /// Elimina un registro de la base de datos
        /// </summary>
        /// <param name="Producto">Objeto con los datos a eliminar</param>
        /// <returns>Cantidad de registros eliminados</returns>
        public int Delete(Producto Producto) { 
            return DbConnection.Delete(Producto); 
        }

    }
}
