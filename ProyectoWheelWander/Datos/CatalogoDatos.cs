using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Models.Data;
using ProyectoWheelWander.Models.ViewModel;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoWheelWander.Datos
{
    public class CatalogoDatos
    {
        public List<CatalogoModel> getCatalogo()
        {
            List<CatalogoModel> listaMotos = new List<CatalogoModel>();


            ConexionDB cn = new ConexionDB();

            using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("getCatalogo", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaMotos.Add(new CatalogoModel
                            {
                                placaMoto = dr["Placa"].ToString(),
                                nombreMarca = dr["Marca"].ToString(),
                                modelo = dr["Modelo"].ToString(),
                                urlFoto = dr["URLFoto"].ToString(),
                                precioReserva = Convert.ToDouble(dr["PrecioReserva"]),
                                ubicacion = dr["Ubicacion"].ToString(),
                                clase = Convert.ToInt32(dr["Clase"]),
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

        public List<MarcaMoto> GetAllMarcas()
        {
            List<MarcaMoto> listaMarcas = new List<MarcaMoto>();
            ConexionDB cn = new ConexionDB();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand("getAllMarcas", conexion))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listaMarcas.Add(new MarcaMoto
                                {
                                    IdmarcaMoto = Convert.ToInt32(dr["IdmarcaMoto"]),
                                    NombreMarca = dr["NombreMarca"].ToString(),
                                    ModeloMoto = dr["ModeloMoto"].ToString(),
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

            return listaMarcas;
        }

        public List<CatalogoModel> FiltrarMotos(int? ubicacionId, int? marcaId, int? claseId, int? ordenarPorNombre, int? ordenarPorPrecio)
        {
            List<CatalogoModel> motosFiltradas = new List<CatalogoModel>();
            ConexionDB cn = new ConexionDB();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("FiltrarMotos", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    // Configura los parámetros
                    cmd.Parameters.AddWithValue("@UbicacionId", ubicacionId ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@MarcaId", marcaId ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@ClaseMoto", claseId ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@OrdenarPorNombre", ordenarPorNombre ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@OrdenarPorPrecio", ordenarPorPrecio ?? Convert.DBNull);
                    
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            motosFiltradas.Add(new CatalogoModel
                            {
                                placaMoto = dr["PlacaMoto"].ToString(),
                                nombreMarca = dr["NombreMarca"].ToString(),
                                modelo = dr["ModeloMoto"].ToString(),
                                urlFoto = dr["URLFoto"].ToString(),
                                precioReserva = Convert.ToDouble(dr["PrecioReserva"]),
                                ubicacion = dr["NombreUbicacion"].ToString(),
                                clase = Convert.ToInt32(dr["ClaseMoto"]),
                            }
                           );
                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                // Manejar la excepción, por ejemplo, loggear el error.
                throw; // O manejar de otra forma, como devolver un código de error específico.
            }

            return motosFiltradas;  // Devuelve la vista con la lista de motos filtradas
        }

        public List<CatalogoModel> FiltrarMotosFecha(int? ubicacionId, DateTime? fechaInicio, TimeSpan? horaInicio, DateTime? fechaFin, TimeSpan? horaFin)
        {
            List<CatalogoModel> motosFiltradas = new List<CatalogoModel>();
            ConexionDB cn = new ConexionDB();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("filtroFechaMoto", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@IDUbicacion", ubicacionId ?? Convert.DBNull);
                    cmd.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = fechaInicio.HasValue ? fechaInicio.Value.ToString("yyyy-dd-MM") : (object)DBNull.Value;
                    cmd.Parameters.Add("@HoraInicio", SqlDbType.Time).Value = horaInicio.HasValue ? horaInicio.Value : (object)DBNull.Value;
                    cmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = fechaFin.HasValue ? fechaFin.Value.ToString("yyyy-dd-MM") : (object)DBNull.Value;
                    cmd.Parameters.Add("@HoraFin", SqlDbType.Time).Value = horaFin.HasValue ? horaFin.Value : (object)DBNull.Value;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            motosFiltradas.Add(new CatalogoModel
                            {
                                placaMoto = dr["PlacaMoto"].ToString(),
                                nombreMarca = dr["NombreMarca"].ToString(),
                                modelo = dr["ModeloMoto"].ToString(),
                                urlFoto = dr["URLFoto"].ToString(),
                                precioReserva = Convert.ToDouble(dr["PrecioReserva"]),
                                ubicacion = dr["NombreUbicacion"].ToString(),
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejar la excepción

            }
            return motosFiltradas;  // Devuelve la vista con la lista de motos filtradas
        }


        public List<CatalogoModel> FiltrarMotos2(int? ubicacionId, int? marcaId, int? claseId, DateTime? fechaInicio, TimeSpan? horaInicio, DateTime? fechaFin, TimeSpan? horaFin, int? ordenarPorNombre, int? ordenarPorPrecio)
        {
            List<CatalogoModel> motosFiltradas = new List<CatalogoModel>();
            ConexionDB cn = new ConexionDB();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("FiltrarMotos2", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    // Configura los parámetros
                    cmd.Parameters.AddWithValue("@UbicacionId", ubicacionId ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@MarcaId", marcaId ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@ClaseMoto", claseId ?? Convert.DBNull);
                    cmd.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = fechaInicio.HasValue ? fechaInicio.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value;
                    cmd.Parameters.Add("@HoraInicio", SqlDbType.Time).Value = horaInicio.HasValue ? horaInicio.Value : (object)DBNull.Value;
                    cmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = fechaFin.HasValue ? fechaFin.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value;
                    cmd.Parameters.Add("@HoraFin", SqlDbType.Time).Value = horaFin.HasValue ? horaFin.Value : (object)DBNull.Value;
                    cmd.Parameters.AddWithValue("@OrdenarPorNombre", ordenarPorNombre ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@OrdenarPorPrecio", ordenarPorPrecio ?? Convert.DBNull);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            motosFiltradas.Add(new CatalogoModel
                            {
                                placaMoto = dr["PlacaMoto"].ToString(),
                                nombreMarca = dr["NombreMarca"].ToString(),
                                modelo = dr["ModeloMoto"].ToString(),
                                urlFoto = dr["URLFoto"].ToString(),
                                precioReserva = Convert.ToDouble(dr["PrecioReserva"]),
                                ubicacion = dr["NombreUbicacion"].ToString(),
                                clase = Convert.ToInt32(dr["ClaseMoto"]),
                            }
                           );
                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                // Manejar la excepción, por ejemplo, loggear el error.
                throw; // O manejar de otra forma, como devolver un código de error específico.
            }

            return motosFiltradas;  // Devuelve la vista con la lista de motos filtradas
        }
    }
}
