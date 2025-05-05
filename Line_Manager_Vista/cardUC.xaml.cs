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

namespace Line_Manager_Vista
{
    /// <summary>
    /// Lógica de interacción para cardUC.xaml
    /// </summary>
    public partial class cardUC : UserControl
    {
        public cardUC()
        {
            InitializeComponent();
        }
        public void SetDatos(string nombreSitio, string status, string operadora)
        {
            txtNombreSitio.Text = $"Sitio: {nombreSitio}";
            txtStatus.Text = $"Estado: {status}";
            txtOperadora.Text = $"Operadora: {operadora}";
        }
        public event EventHandler AgregarLineaClick;

        private void BtnAgregarLinea_Click(object sender, RoutedEventArgs e)
        {
            AgregarLineaClick?.Invoke(this, EventArgs.Empty);
        }
    }
}
