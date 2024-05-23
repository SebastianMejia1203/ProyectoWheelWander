using ProyectoWheelWander.Models.Data;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;

namespace ProyectoWheelWander.Datos
{
    public class UsuarioDatos
    {
        public List<Usuario> GetAllUsuario()
        {
            List<Usuario> listaUsuario = new List<Usuario>();
            ConexionDB cn = new ConexionDB();

            using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("getAllUsuario", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaUsuario.Add(new Usuario
                            {
                                Cedula = Convert.ToInt32(dr["Cedula"]),
                                PrimerNombre = dr["PrimerNombre"].ToString(),
                                SegundoNombre = dr["SegundoNombre"]?.ToString(), // Usar el operador de propagación nula en caso de que el campo pueda ser NULL
                                PrimerApellido = dr["PrimerApellido"].ToString(),
                                SegundoApellido = dr["SegundoApellido"]?.ToString(), // Igual aquí para campos opcionales
                                Email = dr["Email"].ToString(),
                                Contrasena = dr["Contrasena"].ToString(),
                                Celular = Convert.ToInt64(dr["Celular"]), // Cambiado a Int64 si es un BIGINT
                                EstadoUsuario = Convert.ToByte(dr["EstadoUsuario"]),
                                IDRol = Convert.ToInt32(dr["IDRol"]),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
                                FKIDTipoDocumento = Convert.ToInt32(dr["FKIDTipoDocumento"]),
                                FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Aquí deberías manejar la excepción o re-lanzarla dependiendo de tu política de manejo de errores
                    throw new Exception("Error al obtener la lista de usuarios", ex);
                }
            }

            return listaUsuario;
        }

        public Usuario FindUsuarioByCedula(int cedula)
        {
            Usuario usuario = null;  // Inicializa a null para manejar casos donde no se encuentren datos
            ConexionDB cn = new ConexionDB();
            using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("FindUsuarioByCedula", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Cedula", cedula);  // Asegúrate de que el nombre del parámetro coincida exactamente

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())  // Asegúrate de que hay al menos una fila para leer
                        {
                            usuario = new Usuario();  // Instancia el usuario solo si hay datos
                            usuario.Cedula = Convert.ToInt32(dr["Cedula"]);
                            usuario.PrimerNombre = dr["PrimerNombre"].ToString();
                            usuario.SegundoNombre = dr["SegundoNombre"]?.ToString(); // Usar el operador de propagación nula
                            usuario.PrimerApellido = dr["PrimerApellido"].ToString();
                            usuario.SegundoApellido = dr["SegundoApellido"]?.ToString();
                            usuario.Email = dr["Email"].ToString();
                            usuario.Contrasena = dr["Contrasena"].ToString();
                            usuario.Celular = Convert.ToInt64(dr["Celular"]);
                            usuario.EstadoUsuario = Convert.ToByte(dr["EstadoUsuario"]);
                            usuario.IDRol = Convert.ToInt32(dr["IDRol"]);
                            usuario.FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);
                            usuario.FKIDTipoDocumento = Convert.ToInt32(dr["FKIDTipoDocumento"]);
                            usuario.FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]);
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron datos para la cédula: " + cedula);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en FindUsuarioByCedula: " + ex.Message);
                    throw; // Re-lanzar la excepción para que pueda ser manejada más arriba en la cadena
                }
            }
            return usuario;
        }

        public string BuscarUsuarioEmail(string email)
        {
            String PrimerNombre = string.Empty;  // Inicializa a null para manejar casos donde no se encuentren datos
            ConexionDB cn = new ConexionDB();
            using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("BuscarUsuarioEmail", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", email);  // Asegúrate de que el nombre del parámetro coincida exactamente

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())  // Asegúrate de que hay al menos una fila para leer
                        {
                            PrimerNombre = dr["PrimerNombre"].ToString();
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron datos");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en FindUsuarioByCedula: " + ex.Message);
                    throw; // Re-lanzar la excepción para que pueda ser manejada más arriba en la cadena
                }
            }
            return PrimerNombre;
        }

        public bool InsertUsuario(Usuario usuario)
        {
            bool respuesta;

            try {
                ConexionDB cn = new ConexionDB();
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("InsertUsuario", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Añadir los parámetros necesarios para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@Cedula", usuario.Cedula);
                    cmd.Parameters.AddWithValue("@PrimerNombre", usuario.PrimerNombre);
                    cmd.Parameters.AddWithValue("@SegundoNombre", usuario.SegundoNombre ?? (object)DBNull.Value); // Asumiendo que puede ser nulo
                    cmd.Parameters.AddWithValue("@PrimerApellido", usuario.PrimerApellido);
                    cmd.Parameters.AddWithValue("@SegundoApellido", usuario.SegundoApellido ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@FKIDTipoDocumento", usuario.FKIDTipoDocumento);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);

                    
                    cmd.ExecuteNonQuery(); // Ejecuta el comando
                }
                respuesta = true;
            }
            catch(Exception e) {
                string error = e.Message;
                respuesta = false; 
            }

            return respuesta;
        }

        public bool UpdateUsuario(Usuario usuario)
        {
            bool respuesta;

            try
            {
                ConexionDB cn = new ConexionDB();
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("UpdateUsuario", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Añadir los parámetros necesarios para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@Cedula", usuario.Cedula);
                    cmd.Parameters.AddWithValue("@PrimerNombre", usuario.PrimerNombre);
                    cmd.Parameters.AddWithValue("@SegundoNombre", usuario.SegundoNombre ?? (object)DBNull.Value); // Asumiendo que puede ser nulo
                    cmd.Parameters.AddWithValue("@PrimerApellido", usuario.PrimerApellido);
                    cmd.Parameters.AddWithValue("@SegundoApellido", usuario.SegundoApellido ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);


                    cmd.ExecuteNonQuery(); // Ejecuta el comando
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }

        public string GenerarContrasena()
        {
            const int longitud = 8;
            const string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            StringBuilder resultado = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < longitud; i++)
            {
                int indice = random.Next(caracteres.Length);
                resultado.Append(caracteres[indice]);
            }

            return resultado.ToString();
        }

        public bool CambiarContrasena(string email, string contrasena)
        {
            bool respuesta;
            try
            {
                ConexionDB cn = new ConexionDB();
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("cambiarContrasena", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@NuevaContrasena", contrasena);

                    cmd.ExecuteNonQuery(); // Ejecuta el comando
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }

            return respuesta;
        }

        public bool DeleteUsuario(long cedula)
        {
            bool respuesta;

            try {
                ConexionDB cn = new ConexionDB();
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("DeleteUsuario", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@Cedula", cedula);

                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }


        public List<TipoDocumento> GetAllTipoDocumento()
        {
            List<TipoDocumento> listaTipoDoc = new List<TipoDocumento>();
            ConexionDB cn = new ConexionDB();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand("SELECT IDTipoDocumento, NombreTipoDocumento FROM TipoDocumento", conexion))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listaTipoDoc.Add(new TipoDocumento
                                {
                                    IDTipoDocumento = Convert.ToInt32(dr["IDTipoDocumento"]),
                                    NombreTipoDocumento = dr["NombreTipoDocumento"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejar la excepción, por ejemplo, loggear el error.
                throw; // O manejar de otra forma, como devolver un código de error específico.
            }

            return listaTipoDoc;
        }

    }
}
