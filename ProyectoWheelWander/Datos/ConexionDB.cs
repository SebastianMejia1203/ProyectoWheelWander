using System.Data.SqlClient;

namespace ProyectoWheelWander.Datos
{

    public class ConexionDB
    {
        private string cadenaSql = string.Empty;
    
        public ConexionDB ()
        {
            var builder = new ConfigurationBuilder().SetBasePath (Directory.GetCurrentDirectory ()).AddJsonFile("appsettings.json").Build();

            cadenaSql = builder.GetSection("ConnectionStrings:sqlServerDB").Value;
        }

        public string getSqlServerDB() { 
            return cadenaSql; 
        }
    }
}
