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

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (listaArticulo == null)
                return;

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
        }
    }
}