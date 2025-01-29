using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoDocs.Modelo
{
    public abstract class Conexion
    {
        private readonly string connectionString = "";
        public Conexion()
        {
            string dbLocation = Properties.Settings.Default.DatabaseLocation1;
            connectionString = $@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {dbLocation}";
        }
        protected OleDbConnection GetConnection()
        {
            return new OleDbConnection(connectionString);
        }
    }
}
