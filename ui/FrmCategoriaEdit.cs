using System;
using System.Windows.Forms;
using dominio;
using negocio;

namespace ui
{
    public partial class FrmCategoriaEdit : Form
    {
        private Categoria categoria = null;

        public FrmCategoriaEdit()
        {
            InitializeComponent();
        }

        public FrmCategoriaEdit(Categoria categoria)
        {
            InitializeComponent();
            this.categoria = categoria;
            Text = "Modificar categoría";
        }

        private void FrmCategoriaEdit_Load(object sender, EventArgs e)
        {
            if (categoria != null)
                txtDescripcion.Text = categoria.Descripcion;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                string descripcion = txtDescripcion.Text.Trim();

                if (descripcion == "")
                {
                    MessageBox.Show("Ingresá una descripción para la categoría.");
                    return;
                }

                if (contieneNumeros(descripcion))
                {
                    MessageBox.Show("El nombre de la categoría no puede contener números.");
                    return;
                }

                if (existeCategoria(negocio, descripcion))
                {
                    MessageBox.Show("Ya existe una categoría con ese nombre.");
                    return;
                }

                if (categoria == null)
                    categoria = new Categoria();

                categoria.Descripcion = descripcion;

                if (categoria.Id != 0)
                {
                    negocio.modificar(categoria);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(categoria);
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

        private bool existeCategoria(CategoriaNegocio negocio, string descripcion)
        {
            foreach (Categoria existente in negocio.listar())
            {
                bool mismoNombre = existente.Descripcion.ToLower() == descripcion.ToLower();
                bool esLaMisma = categoria != null && existente.Id == categoria.Id;

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
