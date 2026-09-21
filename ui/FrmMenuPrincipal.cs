using System;
using System.Windows.Forms;

namespace ui
{
    public partial class FrmMenuPrincipal : Form
    {
        public FrmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void btnArticulos_Click(object sender, EventArgs e)
        {
            FrmListadoArticulos articulos = new FrmListadoArticulos();
            articulos.ShowDialog();
        }

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            FrmMarcas marcas = new FrmMarcas();
            marcas.ShowDialog();
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            FrmCategorias categorias = new FrmCategorias();
            categorias.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
