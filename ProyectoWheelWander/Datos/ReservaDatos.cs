using ProyectoWheelWander.Models.Data;
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
                    cmd.Parameters.AddWithValue("@FechaInicio", reserva.FechaInicio); // Asumiendo que puede ser nulo
                    cmd.Parameters.AddWithValue("@HoraInicio", reserva.HoraInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", reserva.FechaFin);
                    cmd.Parameters.AddWithValue("@HoraFin", reserva.FechaFin);
                    cmd.Parameters.AddWithValue("@CorreoPSE", reserva.CorreoPSE);
                    cmd.Parameters.AddWithValue("@FKIDUbicacion", reserva.FKIDUbicacion);
                    cmd.Parameters.AddWithValue("@CostoReserva", reserva.CostoReserva);
                    cmd.Parameters.AddWithValue("@Comentario", reserva.Comentario);


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
    }
}
