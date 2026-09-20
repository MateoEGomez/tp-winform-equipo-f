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
                string descripcion = txtDescripcion.Text.Trim();

                if (descripcion == "")
                {
                    MessageBox.Show("Ingresá una descripción para la marca.");
                    return;
                }

                if (contieneNumeros(descripcion))
                {
                    MessageBox.Show("El nombre de la marca no puede contener números.");
                    return;
                }

                if (existeMarca(negocio, descripcion))
                {
                    MessageBox.Show("Ya existe una marca con ese nombre.");
                    return;
                }

                if (marca == null)
                    marca = new Marca();

                marca.Descripcion = descripcion;

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

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private bool contieneNumeros(string texto)
        {
            foreach (char letra in texto)
            {
                if (char.IsDigit(letra))
                    return true;
            }
            return false;
        }

        private bool existeMarca(MarcaNegocio negocio, string descripcion)
        {
            foreach (Marca existente in negocio.listar())
            {
                bool mismoNombre = existente.Descripcion.ToLower() == descripcion.ToLower();
                bool esLaMisma = marca != null && existente.Id == marca.Id;

                if (mismoNombre && !esLaMisma)
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
