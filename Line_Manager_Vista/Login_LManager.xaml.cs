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
using System.Windows.Shapes;
using Line_Manager_Logica;

namespace Line_Manager_Vista
{
    /// <summary>
    /// Lógica de interacción para Login_LManager.xaml
    /// </summary>
    public partial class Login_LManager : Window
    {
        public Login_LManager()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Password.Trim();

            var metodos = new Metodos_Sitio();
            var resultado = metodos.VerificarCredenciales(usuario, contrasena);

            if (!resultado.EsValido)
            {
                MessageBox.Show("Credenciales incorrectas.");
                return;
            }

            if (resultado.TipoOperador == 2)
            {
                MessageBox.Show("Este usuario requiere solicitar permisos al administrador.");
                return;
            }

            MessageBox.Show($"¡Bienvenido {resultado.Usuario}!");

            // Aquí puedes abrir la ventana principal
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtUsuario.Text == "Usuario")
            {
                txtUsuario.Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                txtUsuario.Text = "Usuario";
            }
        }
    }
}
