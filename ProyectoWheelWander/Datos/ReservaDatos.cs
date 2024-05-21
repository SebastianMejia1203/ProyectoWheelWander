using ProyectoWheelWander.Models.Data;
using ProyectoWheelWander.Models.ViewModel;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoWheelWander.Datos
{
    public class ReservaDatos
    {
        public bool crearReserva(Reserva reserva)
        {
            bool respuesta;

            try
            {
                ConexionDB cn = new ConexionDB();
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("InsertarReserva", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Añadir los parámetros necesarios para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@FKCedulaUsuario", reserva.FKCedulaUsuario);
                    cmd.Parameters.AddWithValue("@FKPlacaMoto", reserva.FKPlacaMoto);
                    cmd.Parameters.AddWithValue("@CorreoPSE", reserva.CorreoPSE);
                    cmd.Parameters.AddWithValue("@FKIDUbicacion", reserva.FKIDUbicacion);
                    cmd.Parameters.AddWithValue("@CostoReserva", reserva.CostoReserva);
                    cmd.Parameters.AddWithValue("@Comentario", reserva.Comentario);
                    cmd.Parameters.AddWithValue("@FechaInicio", reserva.FechaInicio);
                    cmd.Parameters.AddWithValue("@HoraInicio", reserva.HoraInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", reserva.FechaFin);
                    cmd.Parameters.AddWithValue("@HoraFin", reserva.HoraFin);

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

        public bool cancelarReserva(int idReserva)
        {
            bool respuesta;

            try
            {
                ConexionDB cn = new ConexionDB();
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("cancelarReserva", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Añadir los parámetros necesarios para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@IDReserva", idReserva);


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

        public PDFViewModel PDFInfo(int cedula)
        {
            PDFViewModel pdf = null;  // Inicializa a null para manejar casos donde no se encuentren datos
            ConexionDB cn = new ConexionDB();
            using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("PDFInfo", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CedulaUsuario", cedula);  // Asegúrate de que el nombre del parámetro coincida exactamente

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())  // Asegúrate de que hay al menos una fila para leer
                        {
                            pdf = new PDFViewModel();  // Instancia el usuario solo si hay datos
                            pdf.IDReserva = Convert.ToInt32(dr["IDReserva"]);
                            pdf.FKCedulaUsuario = Convert.ToInt32(dr["FKCedulaUsuario"]);
                            pdf.FKPlacaMoto = dr["FKPlacaMoto"]?.ToString();
                            pdf.CorreoPSE = dr["CorreoPSE"].ToString();
                            pdf.CostoReserva = Convert.ToDouble(dr["CostoReserva"]);
                            pdf.FechaInicio = Convert.ToDateTime(dr["FechaInicio"]);
                            pdf.HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString());
                            pdf.FechaFin = Convert.ToDateTime(dr["FechaFin"]);
                            pdf.HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString());
                            pdf.EstadoReserva = Convert.ToInt32(dr["EstadoReserva"]);
                            pdf.Ubicacion = dr["Ubicacion"].ToString();
                            pdf.PrimerNombre = dr["PrimerNombre"].ToString();
                            pdf.SegundoNombre = dr["SegundoNombre"].ToString();
                            pdf.PrimerApellido = dr["PrimerApellido"].ToString();
                            pdf.SegundoApellido = dr["SegundoApellido"].ToString();
                            pdf.Celular = Convert.ToInt64(dr["Celular"]);
                            pdf.Email = dr["Email"].ToString();
                            pdf.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                            pdf.URLFoto = dr["URLFoto"].ToString();
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
            return pdf;
        }
    }
}
