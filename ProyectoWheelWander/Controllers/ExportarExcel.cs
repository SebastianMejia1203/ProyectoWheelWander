using iText.Signatures;
using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Datos;
using ProyectoWheelWander.Models.Data;
using System.Data;
using System.Data.SqlClient;
using ClosedXML.Excel;

namespace ProyectoWheelWander.Controllers
{
    public class ExportarExcel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Exportar()
        {
            DataTable usuarios = new DataTable();

            ConexionDB cn = new ConexionDB();

            using (SqlConnection conexion = new SqlConnection(cn.getSqlServerDB()))
            {
                conexion.Open();
                using (var adapter = new SqlDataAdapter())
                {
                    adapter.SelectCommand = new SqlCommand("excelUsuario", conexion);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.Fill(usuarios);
                }
            }

            using (var libro = new XLWorkbook())
            {
                usuarios.TableName = "Usuarios";
                var hoja = libro.Worksheets.Add(usuarios);
                hoja.ColumnsUsed().AdjustToContents();

                using (var memoria = new MemoryStream())
                {
                    libro.SaveAs(memoria);

                    var nombreArchivo = $"Usuarios_{DateTime.Now.ToString("yyyyMMdd")}.xlsx";

                    return File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
                };
            }
        }
    }
}
