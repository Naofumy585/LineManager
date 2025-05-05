using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Line_Manager_Logica;
using Microsoft.Win32;

namespace Line_Manager_Vista
{
    /// <summary>
    /// Lógica de interacción para Nueva_sitio.xaml
    /// </summary>
    public partial class Nueva_sitio : UserControl
    {
        public Nueva_sitio()
        {
            InitializeComponent();
        }

        // Método para manejar el evento click del botón "Agregar Sitio"
        private void btnAgregarSitio_Click(object sender, RoutedEventArgs e)
        {
            string nombreSitio = txtNombreSitio.Text;
            string rutaImagen = txtRutaImagen.Text;

            // Validar ComboBox seleccionado
            if (cmbStatusSitio.SelectedItem is ComboBoxItem itemSeleccionado)
            {
                int statusSitio = int.Parse(itemSeleccionado.Tag.ToString());

                // Valores de ejemplo para el operador (puedes cambiarlos si tienes campos)
                string usuario = "usuario";
                string contrasena = "contraseña";
                int tipoOpe = 1;

                var metodosSitio = new Line_Manager_Logica.Metodos_Sitio();
                bool resultado = metodosSitio.InsertarNuevoSitioYOperador(nombreSitio, statusSitio, usuario, contrasena, tipoOpe, rutaImagen);

                if (resultado)
                {
                    MessageBox.Show("Sitio agregado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error al agregar el sitio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un estado válido", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void btnBuscarImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar imagen del sitio";
            openFileDialog.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                txtRutaImagen.Text = openFileDialog.FileName;
            }
        }
    }
}
