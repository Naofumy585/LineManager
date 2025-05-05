using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Line_Manager_Logica
{
    public class Metodos_Sitio
    {
        // Método que obtiene los sitios o los operadores desde la base de datos
        public List<Sitio> ObtenerSitios()
        {
            var listaSitios = new List<Sitio>();

            var conexion = new Line_Manager_Conexion.Conexion();
            MySqlConnection conexionBD = conexion.ObtenerConexion();

            if (conexionBD == null || conexionBD.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("La conexión no está abierta.");
                return listaSitios;
            }

            try
            {
                string query = "SELECT s.nombreSitio, o.UsuarioOpe FROM table_sitios s " +
                               "JOIN table_operadores o ON s.idSitio = o.idSitio";
                MySqlCommand comando = new MySqlCommand(query, conexionBD);
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Sitio sitio = new Sitio
                        {
                            NombreSitio = reader["nombreSitio"].ToString(),
                            UsuarioOpe = reader["UsuarioOpe"].ToString()
                        };
                        listaSitios.Add(sitio);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos: " + ex.Message);
            }

            return listaSitios;
        }
        public class ResultadoLogin
        {
            public bool EsValido { get; set; }
            public int TipoOperador { get; set; }
            public string Usuario { get; set; }
        }
        public ResultadoLogin VerificarCredenciales(string usuario, string contrasena)
        {
            var resultado = new ResultadoLogin();

            var conexion = new Line_Manager_Conexion.Conexion();
            var conexionBD = conexion.ObtenerConexion();

            if (conexionBD == null || conexionBD.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("No se pudo conectar con la base de datos.");
                return resultado;
            }

            try
            {
                string query = @"SELECT UsuarioOpe, passOpe, idTipoOpe
                         FROM table_operadores
                         WHERE UsuarioOpe = @usuario AND passOpe = @contrasena";

                using (var cmd = new MySqlCommand(query, conexionBD))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            resultado.EsValido = true;
                            resultado.Usuario = reader["UsuarioOpe"].ToString();
                            resultado.TipoOperador = Convert.ToInt32(reader["idTipoOpe"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar las credenciales: " + ex.Message);
            }

            return resultado;
        }
        public bool CambiarStatusSitio(int idSitio, int nuevoStatus)
        {
            var conexion = new Line_Manager_Conexion.Conexion();
            MySqlConnection conexionBD = conexion.ObtenerConexion();

            if (conexionBD == null || conexionBD.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("La conexión no está abierta.");
                return false;
            }

            try
            {
                string query = "UPDATE table_sitios SET Status = @nuevoStatus WHERE idSitio = @idSitio";

                using (var comando = new MySqlCommand(query, conexionBD))
                {
                    comando.Parameters.AddWithValue("@nuevoStatus", nuevoStatus);
                    comando.Parameters.AddWithValue("@idSitio", idSitio);

                    int filasAfectadas = comando.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("El estado del sitio fue actualizado correctamente.");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el sitio con el ID especificado.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el estado del sitio: " + ex.Message);
                return false;
            }
        }
        // Método para insertar un nuevo sitio y asignar un operador, ahora con el campo img
        public bool InsertarNuevoSitioYOperador(string nombreSitio, int statusSitio, string usuarioOpe, string passOpe, int tipoOpe, string img)
        {
            // Usamos la conexión abierta desde la clase Conexion
            var conexion = new Line_Manager_Conexion.Conexion();
            MySqlConnection conexionBD = conexion.ObtenerConexion();

            if (conexionBD == null || conexionBD.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("La conexión no está abierta.");
                return false;
            }

            try
            {
                // Iniciamos una transacción para asegurar la integridad de los datos
                MySqlTransaction transaccion = conexionBD.BeginTransaction();

                // Primero insertamos el nuevo sitio en la tabla `table_sitios`
                string querySitio = @"
                    INSERT INTO table_sitios (nombreSitio, Status, img)
                    VALUES (@nombreSitio, @statusSitio, @img);
                    SELECT LAST_INSERT_ID();";

                MySqlCommand comandoSitio = new MySqlCommand(querySitio, conexionBD, transaccion);
                comandoSitio.Parameters.AddWithValue("@nombreSitio", nombreSitio);
                comandoSitio.Parameters.AddWithValue("@statusSitio", statusSitio);
                comandoSitio.Parameters.AddWithValue("@img", img);

                // Obtenemos el ID del nuevo sitio insertado
                int idSitio = Convert.ToInt32(comandoSitio.ExecuteScalar());

                // Ahora insertamos el nuevo operador en la tabla `table_operadores`
                string queryOperador = @"
                    INSERT INTO table_operadores (idSitio, UsuarioOpe, passOpe, idTipoOpe)
                    VALUES (@idSitio, @usuarioOpe, @passOpe, @idTipoOpe);";

                MySqlCommand comandoOperador = new MySqlCommand(queryOperador, conexionBD, transaccion);
                comandoOperador.Parameters.AddWithValue("@idSitio", idSitio);
                comandoOperador.Parameters.AddWithValue("@usuarioOpe", usuarioOpe);
                comandoOperador.Parameters.AddWithValue("@passOpe", passOpe);
                comandoOperador.Parameters.AddWithValue("@idTipoOpe", tipoOpe);

                // Ejecutamos la inserción del operador
                comandoOperador.ExecuteNonQuery();

                // Si todo es correcto, hacemos commit de la transacción
                transaccion.Commit();
                return true; // Inserción exitosa
            }
            catch (Exception ex)
            {
                // Si ocurre algún error, hacemos rollback de la transacción
                Console.WriteLine("Error al insertar los datos: " + ex.Message);

                // Deshacer la transacción
                try
                {
                    //transaccion.Rollback();
                }
                catch (Exception rollbackEx)
                {
                    Console.WriteLine("Error al hacer rollback: " + rollbackEx.Message);
                }

                return false;
            }
        }
        public bool ModificarOperadora(int idOperadora, string nuevoUsuario, string nuevaContrasena, int nuevoTipoOpe)
        {
            var conexion = new Line_Manager_Conexion.Conexion();
            MySqlConnection conexionBD = conexion.ObtenerConexion();

            if (conexionBD == null || conexionBD.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("La conexión no está abierta.");
                return false;
            }

            try
            {
                string query = @"UPDATE table_operadores 
                                 SET UsuarioOpe = @usuario, passOpe = @contrasena, idTipoOpe = @tipo 
                                 WHERE idOperador = @id";

                using (var comando = new MySqlCommand(query, conexionBD))
                {
                    comando.Parameters.AddWithValue("@usuario", nuevoUsuario);
                    comando.Parameters.AddWithValue("@contrasena", nuevaContrasena);
                    comando.Parameters.AddWithValue("@tipo", nuevoTipoOpe);
                    comando.Parameters.AddWithValue("@id", idOperadora);

                    int filasAfectadas = comando.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Operadora modificada exitosamente.");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró una operadora con el ID especificado.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la operadora: " + ex.Message);
                return false;
            }
        }
        public List<(string usuario, string tipo, string sitio)> ObtenerOperadores()
        {
            var conexion = new Line_Manager_Conexion.Conexion();
            var conexionBD = conexion.ObtenerConexion();

            var lista = new List<(string, string, string)>();

            try
            {
                string query = @"SELECT o.UsuarioOpe, t.tipo, s.nombreSitio 
                             FROM table_operadores o
                             JOIN tipo_operador t ON o.idTipoOpe = t.idTipo
                             JOIN table_sitios s ON o.idSitio = s.idSitio";

                using (var cmd = new MySqlCommand(query, conexionBD))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string usuario = reader["UsuarioOpe"].ToString();
                        string tipo = reader["tipo"].ToString();
                        string sitio = reader["nombreSitio"].ToString();
                        lista.Add((usuario, tipo, sitio));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener operadores: " + ex.Message);
            }

            return lista;
        }
    }
    public class Sitio
    {
        public string NombreSitio { get; set; }
        public string UsuarioOpe { get; set; }
    }
}