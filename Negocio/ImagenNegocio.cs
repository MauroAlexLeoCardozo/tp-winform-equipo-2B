using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ImagenNegocio
    {
        public List<dominio.Imagen> listar()
        {
            List<dominio.Imagen> lista = new List<dominio.Imagen>();
            accesoDatos datos = new accesoDatos();
            try
            {
                datos.setearConsulta("select id, idArticulo, imagenUrl from IMAGENES");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    dominio.Imagen aux = new dominio.Imagen();
                    aux.id = (int)datos.Lector["id"];
                    aux.idArticulo = (int)datos.Lector["idArticulo"];
                    aux.imgUrl = (string)datos.Lector["imagenUrl"];
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
