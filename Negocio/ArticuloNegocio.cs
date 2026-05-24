using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<dominio.Articulo> listar()
        {
            List<dominio.Articulo> lista = new List<dominio.Articulo>();
            accesoDatos datos = new accesoDatos();
            try
            {
                datos.setearConsulta("select A.id, A.codigo, A.nombre, A.descripcion, A.precio, M.descripcion Marca, C.descripcion Categoria, A.idMarca, A.idCategoria from ARTICULOS A, MARCAS M, CATEGORIAS C where M.id = A.idMarca and C.id = A.idCategoria");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    dominio.Articulo aux = new dominio.Articulo();
                    aux.id = (int)datos.Lector["id"];
                    aux.codigo = (string)datos.Lector["codigo"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.descripcion = (string)datos.Lector["descripcion"];
                    aux.precio = (decimal)datos.Lector["precio"];
                    aux.marca = new dominio.Marca();
                    aux.marca.descripcion = (string)datos.Lector["Marca"];
                    aux.categoria = new dominio.Categoria();
                    aux.categoria.descripcion = (string)datos.Lector["Categoria"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
