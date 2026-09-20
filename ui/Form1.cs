using System;
using System.Windows.Forms;
using negocio;
using dominio;
using System.Collections.Generic;

namespace ui
{
    public partial class Form1 : Form
    {
        List<Articulo> listaArticulo;
        public Form1()
        {
            InitializeComponent();
        }

        // Este es el evento que se creó al hacer doble clic en el formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulo = negocio.listar();
                dgvArticulos.DataSource = listaArticulo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            FrmMarcas marcas = new FrmMarcas();
            marcas.ShowDialog();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            listaFiltrada = listaArticulo.FindAll(x => x.Nombre == txtFiltrar.Text);
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
        }
    }
}