using System;
using System.Windows.Forms;
using negocio;
using dominio;

namespace ui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Este es el evento que se creó al hacer doble clic en el formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                // Le decimos a la grilla que su origen de datos (DataSource)
                // es la lista que nos devuelve el método listar()
                dgvArticulos.DataSource = negocio.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}