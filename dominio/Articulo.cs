using System;
using System.Collections.Generic;

namespace dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // Composición: el artículo CONOCE a su Marca y Categoría
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }

        public decimal Precio { get; set; } // En C# el tipo 'money' de SQL Server se mapea a 'decimal'

        // La colección de imágenes
        public List<Imagen> Imagenes { get; set; }

        public Articulo()
        {
            // Inicializamos la lista en el constructor para evitar 
            // la famosa excepción "NullReferenceException" cuando queramos agregarle imágenes.
            Imagenes = new List<Imagen>();
        }
    }
}