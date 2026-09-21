using dominio;

namespace negocio
{
    public class ImagenNegocio
    {
        public void agregar(Imagen nueva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Insert into IMAGENES (IdArticulo, ImagenUrl) values (@idArticulo, @url)");
                datos.setearParametro("@idArticulo", nueva.IdArticulo);
                datos.setearParametro("@url", nueva.ImagenUrl);
                datos.ejecutarAccion();
            }
            catch (System.Exception ex)
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
