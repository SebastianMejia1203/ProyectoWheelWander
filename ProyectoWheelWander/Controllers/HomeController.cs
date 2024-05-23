using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Datos;
using ProyectoWheelWander.Models;
using ProyectoWheelWander.Models.Data;
using ProyectoWheelWander.Models.ViewModel;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ProyectoWheelWander.Controllers
{
    public class HomeController : Controller
    {
        CatalogoDatos catalogoService = new CatalogoDatos();

        public ActionResult Index()
        {
            var viewModel = new CatalogoViewModel
            {
                motosCatalogo = catalogoService.CatalogoMotos(),
                ubicaciones = catalogoService.GetAllUbicaciones(),
                marcas = catalogoService.GetAllMarcas()
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
                                    Idubicacion = Convert.ToInt32(dr["IDUbicacion"]),
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
    }
}
