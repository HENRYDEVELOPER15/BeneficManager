using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoDocs.Modelo
{
    internal class Familiar_Modelo:Conexion
    {

        public void InsertarHijo(string nombre, DateTime fecha, string sex, int idA)
        {
            using (var conection = GetConnection())
            {
                conection.Open();
                using (var command = new OleDbCommand())
                {
                    string de = fecha.ToString("dd/MM/yyyy");
                    command.Connection = conection;
                    command.CommandText = $"INSERT INTO Familiares (Nombre, Fecha_nacimiento, Genero, id_Asociado)" +
                        $@" VALUES (@nombre, '{de}', @sex, {idA})";
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@sex", sex);
                    command.ExecuteNonQuery();
                }
            }
        }


        //        public DataTable GetFamiliaresRango(int id, double min, double max)
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var connection = GetConnection())
        //            {
        //                connection.Open();
        //                using (var command = new OleDbCommand())
        //                {
        //                    command.Connection = connection;
        //                    command.CommandText = $@"SELECT f.*, DateDiff(""yyyy"",f.fecha_nacimiento,Date())-IIf(Format(f.fecha_nacimiento,""mmdd"")>Format(Date(),""mmdd""),1,0) AS Edad
        //FROM familiares AS f
        //WHERE (((DateDiff(""yyyy"",[f].[fecha_nacimiento],Date())-IIf(Format([f].[fecha_nacimiento],""mmdd"")>Format(Date(),""mmdd""),1,0))>{min} And (DateDiff(""yyyy"",[f].[fecha_nacimiento],Date())-IIf(Format([f].[fecha_nacimiento],""mmdd"")>Format(Date(),""mmdd""),1,0))<{max}) AND ((f.id_Asociado)={id}));";
        //                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
        //                    adapter.Fill(dataTable);
        //                }
        //                return dataTable;
        //            }
        //        }

        public DataTable GetFamiliaresRango(int id, double min, double max)
        {
            DataTable dataTable = new DataTable();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;

                    // Consulta simplificada sin condiciones de rango
                    command.CommandText = @"SELECT f.*, 
                    DateDiff(""yyyy"", f.fecha_nacimiento, Date()) - 
                    IIf(Format(f.fecha_nacimiento, ""mmdd"") > Format(Date(), ""mmdd""), 1, 0) AS Edad 
                    FROM familiares AS f 
                    WHERE f.id_Asociado = ?";

                    // Usar parámetros para evitar inyección SQL
                    command.Parameters.AddWithValue("?", id);

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }

            // Filtrar las filas en memoria según el rango de edad
            DataTable filteredTable = dataTable.Clone(); // Clona la estructura del DataTable original
            foreach (DataRow row in dataTable.Rows)
            {
                int edad = Convert.ToInt32(row["Edad"]); // Asegurarse de que Edad es un entero
                if (edad > min && edad < max)
                {
                    filteredTable.ImportRow(row);
                }
            }

            return filteredTable;
        }


        //        public DataTable GetFamiliares(int id)
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var connection = GetConnection())
        //            {
        //                connection.Open();
        //                using (var command = new OleDbCommand())
        //                {
        //                    command.Connection = connection;
        //                    double max = Properties.Settings.Default.Min;
        //                    command.CommandText = $@"SELECT f.*, DateDiff(""yyyy"",f.fecha_nacimiento,Date())-IIf(Format(f.fecha_nacimiento,""mmdd"")>Format(Date(),""mmdd""),1,0) AS Edad
        //FROM familiares AS f
        //WHERE (((f.id_asociado)={id}) AND ((DateDiff(""yyyy"",[f].[fecha_nacimiento],Date())-IIf(Format([f].[fecha_nacimiento],""mmdd"")>Format(Date(),""mmdd""),1,0))>{max}));";
        //                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
        //                    adapter.Fill(dataTable);
        //                }
        //                return dataTable;
        //            }
        //        }

        public DataTable GetFamiliares(int id)
        {
            DataTable dataTable = new DataTable();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;

                    // Consulta simplificada sin condiciones de rango de edad
                    command.CommandText = @"SELECT f.*, 
                    DateDiff(""yyyy"", f.fecha_nacimiento, Date()) - 
                    IIf(Format(f.fecha_nacimiento, ""mmdd"") > Format(Date(), ""mmdd""), 1, 0) AS Edad 
                    FROM familiares AS f 
                    WHERE f.id_asociado = ?";

                    // Parámetro para id
                    command.Parameters.AddWithValue("?", id);

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }

            // Filtrar las filas en memoria según el valor de max
            double max = Properties.Settings.Default.Min; // Leer configuración
            DataTable filteredTable = dataTable.Clone(); // Clonar la estructura del DataTable original
            foreach (DataRow row in dataTable.Rows)
            {
                int edad = Convert.ToInt32(row["Edad"]); // Convertir el valor de Edad
                if (edad > max)
                {
                    filteredTable.ImportRow(row);
                }
            }

            return filteredTable;
        }

        public void BorrrarFamiliar(int id)
        {
            using (var conection = GetConnection())
            {
                conection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = conection;
                    command.CommandText = $"DELETE Familiares.Id FROM Familiares WHERE (((Familiares.Id)={id}));";
                    command.ExecuteNonQuery();

                }
            }
        }


        public void ActualizarFamiliar(int id, string nombre, string genero, DateTime fecha)
        {
            using (var conection = GetConnection())
            {
                conection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = conection;
                    command.CommandText = $"UPDATE familiares SET Nombre = '{nombre}', Fecha_nacimiento = '{fecha.ToString("dd/MM/yyyy")}', Genero = '{genero}' WHERE id = {id};";
                    command.ExecuteNonQuery();

                }
            }
        }
    }
}
