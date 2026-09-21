using System;
using System.ComponentModel;
using System.Windows.Forms;
using dominio;

namespace ui
{
    public partial class FrmDetalleArticulo : Form
    {
        private Articulo articulo;
        private int indiceImagen = 0;

        public FrmDetalleArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void FrmDetalleArticulo_Load(object sender, EventArgs e)
        {
            txtCodigo.Text = articulo.Codigo;
            txtNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            txtMarca.Text = articulo.Marca.Descripcion;
            txtCategoria.Text = articulo.Categoria.Descripcion;
            txtPrecio.Text = articulo.Precio.ToString("N2");
            mostrarImagen();
        }

        private void mostrarImagen()
        {
            int cantidad = articulo.Imagenes.Count;

            if (cantidad == 0)
            {
                pbxImagen.Image = null;
                lblImagen.Text = "Este artículo no tiene imágenes.";
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;
                return;
            }

            lblImagen.Text = "Imagen " + (indiceImagen + 1) + " de " + cantidad;
            btnAnterior.Enabled = indiceImagen > 0;
            btnSiguiente.Enabled = indiceImagen < cantidad - 1;

            try
            {
                pbxImagen.LoadAsync(articulo.Imagenes[indiceImagen].ImagenUrl);
            }
            catch (Exception)
            {
                pbxImagen.Image = null;
                lblImagen.Text += " (no se pudo cargar)";
            }
        }

        private void pbxImagen_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                pbxImagen.Image = null;
                lblImagen.Text += " (no se pudo cargar)";
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            indiceImagen--;
            mostrarImagen();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            indiceImagen++;
            mostrarImagen();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
