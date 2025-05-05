using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Line_Manager_Conexion
{
    public class Conexion
    {
        private MySqlConnection _conexion;
        private string _connectionString = "Server=localhost;Database=bdtaxis;Uid=root@localhost;Pwd=#Mamadisimo780;";

        // Variable para mantener el estado de la sesión
        public static string UsuarioConectado { get; private set; }
        public static int TipoOperador { get; private set; } // Para almacenar el tipo de operador
        public static string NombreSitio { get; private set; } // Para almacenar el nombre del sitio

        public Conexion()
        {
            // Inicializamos la conexión con la cadena de conexión
            _conexion = new MySqlConnection(_connectionString);
        }

        // Método para iniciar sesión
        public bool IniciarSesion(string usuario, string contrasena)
        {
            try
            {
                // Abrimos la conexión si no está abierta
                if (_conexion.State != System.Data.ConnectionState.Open)
                {
                    _conexion.Open();
                }

                // Creamos la consulta SQL para validar el usuario, obtener el tipo de operador y el sitio
                string query = "SELECT o.UsuarioOpe, o.passOpe, o.idTipoOpe, s.nombreSitio " +
                               "FROM table_operadores o " +
                               "JOIN table_sitios s ON o.idSitio = s.idSitio " +
                               "WHERE o.UsuarioOpe = @usuario AND o.passOpe = @contrasena";

                // Creamos el comando con la consulta
                using (var comando = new MySqlCommand(query, _conexion))
                {
                    // Añadimos los parámetros para evitar inyecciones SQL
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@contrasena", contrasena);

                    // Ejecutamos la consulta y obtenemos el resultado
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader.Read()) // Si se encuentra un usuario que coincide
                        {
                            // Guardamos los datos para la sesión
                            UsuarioConectado = reader["UsuarioOpe"].ToString();
                            TipoOperador = Convert.ToInt32(reader["idTipoOpe"]);
                            NombreSitio = reader["nombreSitio"].ToString();
                            return true; // Login exitoso
                        }
                        else
                        {
                            return false; // Credenciales incorrectas
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción, mostramos el mensaje de error
                Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
                return false;
            }
        }

        // Método para verificar si hay una sesión activa
        public static bool EsSesionActiva()
        {
            return !string.IsNullOrEmpty(UsuarioConectado);
        }

        // Método para cerrar la sesión
        public void CerrarSesion()
        {
            // Limpiamos los datos de sesión y cerramos la conexión de manera segura
            UsuarioConectado = null;
            TipoOperador = 0;
            NombreSitio = null;

            // Cerramos la conexión si está abierta
            if (_conexion.State == System.Data.ConnectionState.Open)
            {
                _conexion.Close();
            }
        }

        // Getter para la conexión abierta (si la necesitas en otro lugar)
        public MySqlConnection ObtenerConexion()
        {
            return _conexion;
        }
    }
}