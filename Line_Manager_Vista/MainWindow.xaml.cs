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

namespace Line_Manager_Vista
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Obtén los datos de la capa lógica
            var metodosSitio = new Metodos_Sitio();
            var listaSitios = metodosSitio.ObtenerSitios();
            var listaOperadoras = metodosSitio.ObtenerOperadores();

            foreach (var (usuario, tipo, sitio) in listaOperadoras)
            {
                string status = "Activo"; // Cambia esto si tienes el status en la consulta

                var card = new cardUC(); // Suponiendo que aquí es donde va el botón
                card.txtNombreSitio.Text = sitio;
                card.txtStatus.Text = status;
                card.txtOperadora.Text = usuario;

                // Suscribirse al evento del botón
                card.AgregarLineaClick += (s, e) =>
                {
                    // Asegúrate de tener este contenedor en tu MainWindow.xaml
                    ContenidoPrincipal.Children.Clear();
                    ContenidoPrincipal.Children.Add(new Nueva_sitio());
                };

                datosWrappanel.Children.Add(card);
            }
        }

    }
}