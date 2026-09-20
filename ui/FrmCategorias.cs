using System;
using System.Windows.Forms;
using dominio;
using negocio;

namespace ui
{
    public partial class FrmCategorias : Form
    {
        public FrmCategorias()
        {
            InitializeComponent();
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                dgvCategorias.DataSource = negocio.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmCategoriaEdit alta = new FrmCategoriaEdit();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná una categoría de la lista.");
                return;
            }

            Categoria seleccionada = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;
            FrmCategoriaEdit modificar = new FrmCategoriaEdit(seleccionada);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná una categoría de la lista.");
                return;
            }

            Categoria seleccionada = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;
            DialogResult respuesta = MessageBox.Show("¿Seguro que querés eliminar la categoría " + seleccionada.Descripcion + "?", "Eliminar categoría", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                CategoriaNegocio negocio = new CategoriaNegocio();

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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
