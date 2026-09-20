using System;
using System.Windows.Forms;
using dominio;
using negocio;

namespace ui
{
    public partial class FrmMarcaEdit : Form
    {
        private Marca marca = null;

        public FrmMarcaEdit()
        {
            InitializeComponent();
        }

        public FrmMarcaEdit(Marca marca)
        {
            InitializeComponent();
            this.marca = marca;
            Text = "Modificar marca";
        }

        private void FrmMarcaEdit_Load(object sender, EventArgs e)
        {
            if (marca != null)
                txtDescripcion.Text = marca.Descripcion;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();

            try
            {
                if (txtDescripcion.Text.Trim() == "")
                {
                    MessageBox.Show("Ingresá una descripción para la marca.");
                    return;
                }

                if (marca == null)
                    marca = new Marca();

                marca.Descripcion = txtDescripcion.Text.Trim();

                if (marca.Id != 0)
                {
                    negocio.modificar(marca);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(marca);
                    MessageBox.Show("Agregado exitosamente");
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
