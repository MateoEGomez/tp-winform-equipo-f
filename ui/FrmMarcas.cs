using System;
using System.Windows.Forms;
using dominio;
using negocio;

namespace ui
{
    public partial class FrmMarcas : Form
    {
        public FrmMarcas()
        {
            InitializeComponent();
        }

        private void FrmMarcas_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            MarcaNegocio negocio = new MarcaNegocio();

            try
            {
                dgvMarcas.DataSource = negocio.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmMarcaEdit alta = new FrmMarcaEdit();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná una marca de la lista.");
                return;
            }

            Marca seleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
            FrmMarcaEdit modificar = new FrmMarcaEdit(seleccionada);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná una marca de la lista.");
                return;
            }

            Marca seleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;

            try
            {
                if (marcaEnUso(seleccionada))
                {
                    MessageBox.Show("No se puede eliminar la marca " + seleccionada.Descripcion + " porque hay artículos que la usan.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Seguro que querés eliminar la marca " + seleccionada.Descripcion + "?", "Eliminar marca", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                MarcaNegocio negocio = new MarcaNegocio();

                try
                {
                    negocio.eliminar(seleccionada.Id);
                    cargar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private bool marcaEnUso(Marca marca)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            foreach (Articulo articulo in negocio.listar())
            {
                if (articulo.Marca.Id == marca.Id)
                    return true;
            }
            return false;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
