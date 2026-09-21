using System;
using System.Collections.Generic;
using System.Windows.Forms;
using dominio;
using negocio;

namespace ui
{
    public partial class FrmListadoArticulos : Form
    {
        private List<Articulo> listaArticulo;

        public FrmListadoArticulos()
        {
            InitializeComponent();
        }

        private void FrmListadoArticulos_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                listaArticulo = negocio.listar();
                aplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void aplicarFiltro()
        {
            string filtro = txtFiltrar.Text.Trim().ToUpper();
            List<Articulo> listaFiltrada;

            if (filtro != "")
            {
                listaFiltrada = listaArticulo.FindAll(x => x.Codigo.ToUpper().Contains(filtro) || x.Nombre.ToUpper().Contains(filtro) || x.Marca.Descripcion.ToUpper().Contains(filtro) || x.Categoria.Descripcion.ToUpper().Contains(filtro));
            }
            else
            {
                listaFiltrada = listaArticulo;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "N2";
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (listaArticulo == null)
                return;

            aplicarFiltro();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmArticulo alta = new FrmArticulo();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccioná un artículo de la lista.");
                return;
            }

            Articulo seleccionado = (Articulo)dgvArticulos.SelectedRows[0].DataBoundItem;
            FrmArticulo modificar = new FrmArticulo(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccioná un artículo de la lista.");
                return;
            }

            Articulo seleccionado = (Articulo)dgvArticulos.SelectedRows[0].DataBoundItem;
            DialogResult respuesta = MessageBox.Show("¿Seguro que querés eliminar el artículo " + seleccionado.Nombre + "?", "Eliminar artículo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

                try
                {
                    negocio.eliminar(seleccionado.Id);
                    cargar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
