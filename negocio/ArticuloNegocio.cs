using System;
using System.Collections.Generic;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Hacemos los JOIN para traer las descripciones de Marca y Categoría
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, M.Id as IdMarca, M.Descripcion as Marca, C.Id as IdCategoria, C.Descripcion as Categoria FROM ARTICULOS A JOIN MARCAS M on A.IdMarca = M.Id JOIN CATEGORIAS C on A.IdCategoria = C.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    // El casteo a decimal es necesario porque en la BD está como money
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    // Instanciamos y cargamos el objeto Marca dentro del Articulo
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    // Instanciamos y cargamos el objeto Categoria dentro del Articulo
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

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