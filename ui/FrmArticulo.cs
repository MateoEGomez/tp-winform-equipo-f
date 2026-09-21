using System;
using System.Collections.Generic;
using System.Windows.Forms;
using dominio;
using negocio;

namespace ui
{
    public partial class FrmArticulo : Form
    {
        private Articulo articulo = null;

        public FrmArticulo()
        {
            InitializeComponent();
        }

        public FrmArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar artículo";
        }

        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
                cboMarca.SelectedIndex = -1;

                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                cboCategoria.SelectedIndex = -1;

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString("0.####");
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;

                    foreach (Imagen imagen in articulo.Imagenes)
                    {
                        lstImagenes.Items.Add(imagen.ImagenUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
                e.Handled = true;
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            string url = txtUrlImagen.Text.Trim();

            if (url == "")
            {
                MessageBox.Show("Ingresá la URL de la imagen.");
                return;
            }

            if (!url.ToLower().StartsWith("http://") && !url.ToLower().StartsWith("https://"))
            {
                MessageBox.Show("La URL debe empezar con http:// o https://");
                return;
            }

            foreach (string existente in lstImagenes.Items)
            {
                if (existente.ToLower() == url.ToLower())
                {
                    MessageBox.Show("Esa imagen ya fue agregada.");
                    return;
                }
            }

            lstImagenes.Items.Add(url);
            txtUrlImagen.Clear();
            txtUrlImagen.Focus();
        }

        private void btnQuitarImagen_Click(object sender, EventArgs e)
        {
            if (lstImagenes.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccioná una imagen de la lista.");
                return;
            }

            lstImagenes.Items.RemoveAt(lstImagenes.SelectedIndex);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                string codigo = txtCodigo.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                decimal precio;

                if (codigo == "")
                {
                    MessageBox.Show("Ingresá el código del artículo.");
                    return;
                }

                if (nombre == "")
                {
                    MessageBox.Show("Ingresá el nombre del artículo.");
                    return;
                }

                if (cboMarca.SelectedItem == null)
                {
                    MessageBox.Show("Seleccioná una marca.");
                    return;
                }

                if (cboCategoria.SelectedItem == null)
                {
                    MessageBox.Show("Seleccioná una categoría.");
                    return;
                }

                if (!decimal.TryParse(txtPrecio.Text.Trim(), out precio) || precio <= 0)
                {
                    MessageBox.Show("Ingresá un precio válido, mayor a cero.");
                    return;
                }

                if (existeCodigo(negocio, codigo))
                {
                    MessageBox.Show("Ya existe un artículo con ese código.");
                    return;
                }

                if (articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = codigo;
                articulo.Nombre = nombre;
                articulo.Descripcion = txtDescripcion.Text.Trim();
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.Precio = precio;
                articulo.Imagenes = new List<Imagen>();

                foreach (string url in lstImagenes.Items)
                {
                    Imagen nueva = new Imagen();
                    nueva.ImagenUrl = url;
                    articulo.Imagenes.Add(nueva);
                }

                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool existeCodigo(ArticuloNegocio negocio, string codigo)
        {
            foreach (Articulo existente in negocio.listar())
            {
                bool mismoCodigo = existente.Codigo.ToLower() == codigo.ToLower();
                bool esElMismo = articulo != null && existente.Id == articulo.Id;

                if (mismoCodigo && !esElMismo)
                    return true;
            }
            return false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
