using dominio;

namespace negocio
{
    public class MarcaNegocio
    {
        public void agregar(Marca nueva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Insert into MARCAS (Descripcion) values (@descripcion)");
                datos.setearParametro("@descripcion", nueva.Descripcion);
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
