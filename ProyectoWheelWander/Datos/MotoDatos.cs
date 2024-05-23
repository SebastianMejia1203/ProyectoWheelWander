using ProyectoWheelWander.Models.Data;
using ProyectoWheelWander.Models.ViewModel;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;

namespace ProyectoWheelWander.Datos
{
    public class MotoDatos
    {
        public List<Moto> listaDeMotos()
        {
            List<Moto> listaMotos = new List<Moto>();
            ConexionDB cn = new ConexionDB();

            using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("getAllMoto", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaMotos.Add(new Moto
                            {
                                PlacaMoto = dr["PlacaMoto"].ToString(),
                                FkidmarcaMoto = Convert.ToInt32(dr["FkidmarcaMoto"]),
                                CilindrajeMoto = Convert.ToInt32(dr["CilindrajeMoto"]),
                                Potencia = Convert.ToInt32(dr["Potencia"]),
                                Peso = Convert.ToInt32(dr["Peso"]),
                                FkidtransmicionMoto = Convert.ToInt32(dr["FkidtransmicionMoto"]),
                                CapacidadCombustible = Convert.ToInt32(dr["CapacidadCombustible"]),
                                ConsumoCombustible = Convert.ToInt32(dr["ConsumoCombustible"]),
                                InformacionAdicional = dr["InformacionAdicional"].ToString(),
                                Urlfoto = dr["Urlfoto"].ToString(),
                                PrecioReserva = Convert.ToDouble(dr["PrecioReserva"]),
                                EstadoReserva = Convert.ToByte(dr["EstadoReserva"]),
                                EstadoMoto = Convert.ToByte(dr["EstadoMoto"]),
                                Fkidubicacion = Convert.ToInt32(dr["Fkidubicacion"]),
                                FkcedulaPropietario = Convert.ToInt32(dr["FkcedulaPropietario"]),
                                UrlfotoPlaca = dr["UrlfotoPlaca"].ToString(),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Aquí deberías manejar la excepción o re-lanzarla dependiendo de tu política de manejo de errores
                    throw new Exception("Error al obtener la lista de motos", ex);
                }
            }

            return listaMotos;
        }

        public Moto buscarMoto(String placa)
        {
            Moto moto = null;  // Inicializa a null para manejar casos donde no se encuentren datos
            ConexionDB cn = new ConexionDB();
            using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("buscarMoto", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PlacaMoto", placa);  // Asegúrate de que el nombre del parámetro coincida exactamente

                try
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())  // Asegúrate de que hay al menos una fila para leer
                        {
                            moto = new Moto {   // Instancia el usuario solo si hay datos
                                PlacaMoto = dr["PlacaMoto"].ToString(),
                                FkidmarcaMoto = Convert.ToInt32(dr["FkidmarcaMoto"]),
                                CilindrajeMoto = Convert.ToInt32(dr["CilindrajeMoto"]),
                                Potencia = Convert.ToInt32(dr["Potencia"]),
                                Peso = Convert.ToInt32(dr["Peso"]),
                                FkidtransmicionMoto = Convert.ToInt32(dr["FkidtransmicionMoto"]),
                                CapacidadCombustible = Convert.ToInt32(dr["CapacidadCombustible"]),
                                ConsumoCombustible = Convert.ToInt32(dr["ConsumoCombustible"]),
                                InformacionAdicional = dr["InformacionAdicional"].ToString(),
                                Urlfoto = dr["Urlfoto"].ToString(),
                                PrecioReserva = Convert.ToDouble(dr["PrecioReserva"]),
                                EstadoReserva = Convert.ToByte(dr["EstadoReserva"]),
                                EstadoMoto = Convert.ToByte(dr["EstadoMoto"]),
                                Fkidubicacion = Convert.ToInt32(dr["Fkidubicacion"]),
                                FkcedulaPropietario = Convert.ToInt32(dr["FkcedulaPropietario"]),
                                ClaseMoto = Convert.ToInt32(dr["ClaseMoto"]),
                                UrlfotoPlaca = dr["UrlfotoPlaca"].ToString(),
                            };
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron datos para la moto: " + placa);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en buscarMoto: " + ex.Message);
                    throw; // Re-lanzar la excepción para que pueda ser manejada más arriba en la cadena
                }
            }
            return moto;
        }



        public bool InsertarMoto(Moto moto)
        {
            bool respuesta;

            try
            {
                ConexionDB cn = new ConexionDB();
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("InsertarMoto", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Añadir los parámetros necesarios para el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@PlacaMoto", moto.PlacaMoto);
                    cmd.Parameters.AddWithValue("@FKIDMarcaMoto", moto.FkidmarcaMoto);
                    cmd.Parameters.AddWithValue("@CilindrajeMoto", moto.CilindrajeMoto); // Asumiendo que puede ser nulo
                    cmd.Parameters.AddWithValue("@Potencia", moto.Potencia);
                    cmd.Parameters.AddWithValue("@Peso", moto.Peso);
                    cmd.Parameters.AddWithValue("@FKIDTransmicionMoto", moto.FkidtransmicionMoto);
                    cmd.Parameters.AddWithValue("@CapacidadCombustible", moto.CapacidadCombustible);
                    cmd.Parameters.AddWithValue("@ConsumoCombustible", moto.ConsumoCombustible);
                    cmd.Parameters.AddWithValue("@InformacionAdicional", moto.InformacionAdicional);
                    cmd.Parameters.AddWithValue("@URLFoto", moto.Urlfoto);
                    cmd.Parameters.AddWithValue("@PrecioReserva", moto.PrecioReserva);
                    cmd.Parameters.AddWithValue("@FKIDUbicacion", moto.Fkidubicacion);
                    cmd.Parameters.AddWithValue("@FKCedulaPropietario", moto.FkcedulaPropietario);
                    cmd.Parameters.AddWithValue("@ClaseMoto", moto.ClaseMoto);
                    //cmd.Parameters.AddWithValue("@URLFotoPlaca", moto.UrlfotoPlaca);


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


        public List<Ubicacion> GetAllUbicaciones()
        {
            List<Ubicacion> listaUbicaciones = new List<Ubicacion>();
            ConexionDB cn = new ConexionDB();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand("getAllUbicaciones", conexion))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listaUbicaciones.Add(new Ubicacion
                                {
                                    Idubicacion = Convert.ToInt32(dr["Idubicacion"]),
                                    NombreUbicacion = dr["NombreUbicacion"].ToString(),
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

            return listaUbicaciones;
        }

        public List<TransmicionMoto> GetTransmicion()
        {
            List<TransmicionMoto> transmiciones = new List<TransmicionMoto>();
            ConexionDB cn = new ConexionDB();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand("select * from TransmicionMoto", conexion))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                transmiciones.Add(new TransmicionMoto
                                {
                                    IdtransmicionMoto = Convert.ToInt32(dr["IDTransmicionMoto"]),
                                    TipoTransmicion = dr["TipoTransmicion"].ToString()
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

            return transmiciones;
        }

        public List<AdminMotoModel> motosPorUsuario(int cedula)
        {
            List<AdminMotoModel> motos = new List<AdminMotoModel>();
            ConexionDB cn = new ConexionDB();

            using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ObtenerMotosPorUsuario", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CedulaUsuario", cedula);

                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            motos.Add(
                                new AdminMotoModel
                                {
                                    PlacaMoto = dr["PlacaMoto"].ToString(),
                                    Urlfoto = dr["Urlfoto"].ToString(),
                                    EstadoReserva = Convert.ToInt32(dr["EstadoReserva"]),
                                    EstadoMoto = Convert.ToInt32(dr["EstadoMoto"]),
                                    NombreMarca = dr["NombreMarca"].ToString(),
                                    ModeloMoto = dr["ModeloMoto"].ToString(),
                                });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Aquí deberías manejar la excepción o re-lanzarla dependiendo de tu política de manejo de errores
                    throw new Exception("Error al obtener la lista de motos", ex);
                }
            }

            return motos;
        }

        public List<AdminReservaModel> reservaPorUsuario(int cedula)
        {
            List<AdminReservaModel> reservas = new List<AdminReservaModel>();
            ConexionDB cn = new ConexionDB();

            using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ObtenerReservasPorUsuario", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CedulaUsuario", cedula);

                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            reservas.Add(
                                new AdminReservaModel
                                {
                                    IDReserva = Convert.ToInt32(dr["IDReserva"]),
                                    PlacaMoto = dr["FKPlacaMoto"].ToString(),
                                    FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                    HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                    FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                                    HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString()),
                                    CostoReserva = Convert.ToDouble(dr["CostoReserva"]),
                                    comentario = dr["Comentario"].ToString(),
                                    IDUbicacion = Convert.ToInt32(dr["IDUbicacion"]),
                                    NombreUbicacion = dr["NombreUbicacion"].ToString(),
                                    URLFoto = dr["URLFoto"].ToString(),
                                    EstadoReserva = Convert.ToByte(dr["EstadoReserva"])
                                });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Aquí deberías manejar la excepción o re-lanzarla dependiendo de tu política de manejo de errores
                    throw new Exception("Error al obtener la lista de reservas", ex);
                }
            }

            return reservas;
        }

        public Reserva ObtenerUltimaReservaValidaPorPlaca(string placaMoto)
        {
            Reserva reserva = new Reserva();
            ConexionDB cn = new ConexionDB();

            using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ObtenerUltimaReservaValidaPorMoto", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PlacaMoto", placaMoto);

                try {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            reserva = new Reserva
                            {
                                IDReserva = Convert.ToInt32(dr["IDReserva"]),
                                FKCedulaUsuario = Convert.ToInt32(dr["FKCedulaUsuario"]),
                                FKPlacaMoto = dr["FKPlacaMoto"].ToString(),
                                CorreoPSE = dr["CorreoPSE"].ToString(),
                                FKIDUbicacion = Convert.ToInt32(dr["FKIDUbicacion"]),
                                CostoReserva = Convert.ToDouble(dr["CostoReserva"]),
                                Comentario = dr["Comentario"].ToString(),
                                FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                                HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString())
                            };
                       }
                    }
                }
                catch (Exception ex)
                {
                    // Aquí deberías manejar la excepción o re-lanzarla dependiendo de tu política de manejo de errores
                    throw new Exception("Error al obtener la lista de motos", ex);
                }
            }

            return reserva;
        }

        public List<historialViewModel> ObtenerHistorial(string placa)
        {
            List<historialViewModel> historial = new List<historialViewModel>();
            ConexionDB cn = new ConexionDB();

            using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ObtenerHistorial", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PlacaMoto", placa);

                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            historial.Add(
                                new historialViewModel
                                {
                                    idReserva = Convert.ToInt32(dr["IDReserva"]),
                                    cedulaUsuario = Convert.ToInt32(dr["FKCedulaUsuario"]),
                                    ganancia = Convert.ToDouble(dr["CostoReserva"]),
                                    FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                    HoraInicio = TimeSpan.Parse(dr["HoraInicio"].ToString()),
                                    FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                                    HoraFin = TimeSpan.Parse(dr["HoraFin"].ToString())
                                });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Aquí deberías manejar la excepción o re-lanzarla dependiendo de tu política de manejo de errores
                    throw new Exception("Error al obtener el historial", ex);
                }
            }

            return historial;
        }

        public gananciaViewModel FindUsuarioByCedula(int cedula)
        {
            gananciaViewModel usuario = null;  // Inicializa a null para manejar casos donde no se encuentren datos
            ConexionDB cn = new ConexionDB();
            using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ObtenerGanancias", conexion)
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
                            usuario = new gananciaViewModel();  // Instancia el usuario solo si hay datos
                            usuario.cedula = Convert.ToInt32(dr["Cedula"]);
                            usuario.gananciaA = Convert.ToDouble(dr["gananciaActual"]);
                            usuario.gananciaH = Convert.ToDouble(dr["gananciaHistorica"]);
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


        public bool inhabilitarMoto(string placa)
        {
            bool respuesta;

            try
            {
                ConexionDB cn = new ConexionDB();
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("InhabilitarMoto", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@PlacaMoto", placa);

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

    }
}

