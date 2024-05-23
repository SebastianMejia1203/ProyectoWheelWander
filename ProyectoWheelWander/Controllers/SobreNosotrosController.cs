using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Datos;
using ProyectoWheelWander.Models.Data;
using System.Data.SqlClient;

namespace ProyectoWheelWander.Controllers
{
    public class SobreNosotrosController : Controller
    {
        // GET: SobreNosotrosController
        public ActionResult Index()
        {
            var model = GetAllTipoDocumento();
            return View(model);
        }

        public Empresa GetAllTipoDocumento()
        {
            Empresa empresa = new Empresa();
            ConexionDB cn = new ConexionDB();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Empresa", conexion))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                empresa = new Empresa
                                {
                                    ID = Convert.ToInt32(dr["ID"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Direccion = dr["Direccion"].ToString(),
                                    Celular = Convert.ToInt64(dr["Celular"]),
                                    NIT = dr["NIT"].ToString(),
                                    Mision = dr["Mision"].ToString(),
                                    Vision = dr["Vision"].ToString(),
                                    Equipo = dr["Equipo"].ToString()
                                };
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

            return empresa;
        }
    }
}
