using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                datos.setearConsulta("WITH PrimeraImagen AS (SELECT idArticulo, ImagenUrl, ROW_NUMBER() OVER (PARTITION BY idArticulo ORDER BY Id) AS rn FROM dbo.IMAGENES) SELECT A.id, A.codigo, A.nombre, A.descripcion, A.precio, M.descripcion Marca, C.descripcion Categoria, A.idMarca, A.idCategoria, PI.ImagenUrl FROM dbo.ARTICULOS A LEFT JOIN dbo.MARCAS M ON M.id = A.idMarca LEFT JOIN dbo.CATEGORIAS C ON C.id = A.idCategoria LEFT JOIN PrimeraImagen PI ON PI.idArticulo = A.id AND PI.rn = 1");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    dominio.Articulo aux = new dominio.Articulo();
                    aux.id = (int)datos.Lector["id"];
                    aux.codigo = (string)datos.Lector["codigo"];
                    aux.nombre = (string)datos.Lector["nombre"];
                    aux.descripcion = (string)datos.Lector["descripcion"];
                    aux.precio = (decimal)datos.Lector["precio"];
                    aux.img = new dominio.Imagen();
                    aux.img.imgUrl = (string)datos.Lector["ImagenUrl"];
                    aux.marca = new dominio.Marca();
                    aux.marca.descripcion = (string)datos.Lector["Marca"];
                    aux.categoria = new dominio.Categoria();
                    if (!(datos.Lector["Categoria"] is DBNull))
                    {
                        aux.categoria.descripcion = (string)datos.Lector["Categoria"];
                    }else
                    {
                        aux.categoria.descripcion = "Sin categoria";
                    }
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
