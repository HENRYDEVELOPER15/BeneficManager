using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoDocs.Modelo
{
    public class Asociado_Modelo : Conexion
    {
        public int InsertarAsociado(long cedula, string nombre, string direccion, long telefono, string correo, string fechaNa, string fechaIng)
        {
            using (var conection = GetConnection())
            {
                try
                {
                    conection.Open();

                    // Primero, verificar si el número de identificación ya está registrado
                    string consultaExiste = "SELECT Id FROM Asociados WHERE Cedula = @noIdentificacion";
                    using (OleDbCommand comandoVerificar = new OleDbCommand(consultaExiste, conection))
                    {
                        comandoVerificar.Parameters.AddWithValue("@noIdentificacion", cedula);
                        object resultado = comandoVerificar.ExecuteScalar();

                        if (resultado != null) // Si el número de identificación existe
                        {
                            return Convert.ToInt32(resultado); // Devolver el ID correspondiente
                        }
                    }

                    // Si no existe, insertar el nuevo registro
                    string fechaActual = DateTime.Now.ToString("dd/MM/yyyy");
                    string consultaInsertar = "INSERT INTO Asociados " +
                        "(Cedula, Nombre, Direccion, Telefono, Correo, Fecha_nacimiento, Fecha_Ingreso, Estado, Fecha_modificacion) " +
                        $"VALUES (@Cedula, @Nombre, @Direccion, @Telfono, @Correo, '{fechaNa}', '{fechaIng}', 0, '{fechaActual}')";
                    using (OleDbCommand comandoInsertar = new OleDbCommand(consultaInsertar, conection))
                    {

                        comandoInsertar.Parameters.AddWithValue("@Cedula", cedula);
                        comandoInsertar.Parameters.AddWithValue("@Nombre", nombre);
                        comandoInsertar.Parameters.AddWithValue("@Direccion", direccion);
                        comandoInsertar.Parameters.AddWithValue("@Telfono", telefono);
                        comandoInsertar.Parameters.AddWithValue("@Correo", correo);
                        comandoInsertar.ExecuteNonQuery();
                    }

                    // Recuperar el ID del nuevo registro insertado
                    string consultaNuevoId = "SELECT @@IDENTITY";
                    using (OleDbCommand comandoId = new OleDbCommand(consultaNuevoId, conection))
                    {
                        object nuevoId = comandoId.ExecuteScalar();
                        return Convert.ToInt32(nuevoId);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al insertar o recuperar el asociado: " + ex.Message);
                }
            }

        }

        public DataTable GetDataByRangeAndState()
        {
            DataTable dataTable = new DataTable();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;
                    command.CommandText = " SELECT * FROM Asociados WHERE Estado = 0 ORDER BY Nombre ASC;";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                return dataTable;
            }
        }

        public DataTable GetDataByRangeAndState(string referencia)
        {
            DataTable dataTable = new DataTable();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $@"SELECT * FROM Asociados WHERE Estado = 0  AND (Nombre LIKE '%{referencia}%' OR Cedula LIKE '%{referencia}%') ORDER BY Nombre ASC;";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                return dataTable;
            }
        }


        public DataTable GetArchivados()
        {
            DataTable dataTable = new DataTable();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM Asociados WHERE Estado = 1 ORDER BY Nombre ASC;";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                return dataTable;
            }
        }

        public DataTable GetArchivados(string referencia)
        {
            DataTable dataTable = new DataTable();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $@"SELECT * FROM Asociados WHERE Estado = 1 AND (Nombre LIKE '%{referencia}%' OR Cedula LIKE '%{referencia}%') ORDER BY Nombre ASC;";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                return dataTable;
            }
        }

        //        public DataTable GetArchivados(int max, int min)
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var connection = GetConnection())
        //            {
        //                connection.Open();
        //                using (var command = new OleDbCommand())
        //                {
        //                    command.Connection = connection;
        //                    command.CommandText = $@"SELECT A.*
        //FROM Asociados AS A
        //WHERE (((A.Estado)=1) AND ((Year([A].[fecha_modificacion]))>={min} And (Year([A].[fecha_modificacion]))<={max}));";
        //                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
        //                    adapter.Fill(dataTable);
        //                }
        //                return dataTable;
        //            }
        //        }


        public DataTable GetArchivados(int max, int min)
        {
            DataTable dataTable = new DataTable();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;

                    // Consulta simplificada sin condiciones de rango de años
                    command.CommandText = @"SELECT A.*
                                            FROM Asociados AS A
                                            WHERE A.Estado = 1";

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }

            // Filtrar las filas en memoria según el rango de años
            DataTable filteredTable = dataTable.Clone(); // Clonar la estructura del DataTable original
            foreach (DataRow row in dataTable.Rows)
            {
                if (DateTime.TryParse(row["fecha_modificacion"].ToString(), out DateTime fechaModificacion))
                {
                    int year = fechaModificacion.Year; // Extraer el año
                    if (year >= min && year <= max)
                    {
                        filteredTable.ImportRow(row);
                    }
                }
            }

            return filteredTable;
        }

        //        public DataTable GetAsociadosHijos()
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var connection = GetConnection())
        //            {
        //                connection.Open();
        //                using (var command = new OleDbCommand())
        //                {
        //                    double max = Properties.Settings.Default.Min;
        //                    command.Connection = connection;
        //                    command.CommandText = $@"SELECT DISTINCT a.*
        //FROM asociados AS a INNER JOIN familiares AS f ON a.id = f.id_asociado
        //WHERE (((DateDiff(""yyyy"",[f].[fecha_nacimiento],Date())-IIf(Format([f].[fecha_nacimiento],""mmdd"")>Format(Date(),""mmdd""),1,0))<={max}) AND ((a.[Estado])=0));";
        //                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
        //                    adapter.Fill(dataTable);
        //                }
        //                return dataTable;
        //            }
        //        }

        public DataTable GetAsociadosHijos()
        {
            DataTable dataTable = new DataTable();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;

                    // Consulta simplificada: sin filtrar por la edad en SQL
                    command.CommandText = @"SELECT DISTINCT a.*, f.fecha_nacimiento
                                    FROM asociados AS a 
                                    INNER JOIN familiares AS f ON a.id = f.id_asociado
                                    WHERE a.Estado = 0";

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }

            // Filtrar en memoria por edad máxima
            double max = Properties.Settings.Default.Min; // Leer la edad máxima de configuración
            DataTable filteredTable = dataTable.Clone(); // Clonar estructura del DataTable original

            HashSet<int> addedAssociates = new HashSet<int>(); // Usar un HashSet para asegurar no repetidos

            foreach (DataRow row in dataTable.Rows)
            {
                if (DateTime.TryParse(row["f.fecha_nacimiento"].ToString(), out DateTime fechaNacimiento))
                {
                    // Calcular la edad del familiar
                    int edad = DateTime.Now.Year - fechaNacimiento.Year;
                    if (DateTime.Now < fechaNacimiento.AddYears(edad)) // Ajustar edad si no ha cumplido años este año
                    {
                        edad--;
                    }

                    if (edad <= max)
                    {
                        // Verificar si el asociado ya fue añadido al filtro
                        int idAsociado = Convert.ToInt32(row["id"]);
                        if (!addedAssociates.Contains(idAsociado))
                        {
                            filteredTable.ImportRow(row);
                            addedAssociates.Add(idAsociado); // Marcar como añadido
                        }
                    }
                }
            }

            return filteredTable;
        }


        //        public DataTable GetAsociadosFamul()
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var connection = GetConnection())
        //            {
        //                connection.Open();
        //                using (var command = new OleDbCommand())
        //                {
        //                    double max = Properties.Settings.Default.Min;
        //                    command.Connection = connection;
        //                    command.CommandText = $@"SELECT DISTINCT A.*
        //FROM Asociados AS A INNER JOIN Familiares AS F ON A.id = F.id_asociado
        //WHERE (((A.Estado)=0) AND ((DateDiff(""d"",[F].[fecha_nacimiento],Date())/365.25)>{max} And (DateDiff(""d"",[F].[fecha_nacimiento],Date())/365.25)<=21));;
        //";
        //                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
        //                    adapter.Fill(dataTable);
        //                }
        //                return dataTable;
        //            }
        //        }


        public DataTable GetAsociadosFamul()
        {
            DataTable dataTable = new DataTable();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;

                    // Consulta simplificada para obtener todos los registros relevantes
                    command.CommandText = @"SELECT DISTINCT A.*, F.fecha_nacimiento
                                            FROM Asociados AS A 
                                            INNER JOIN Familiares AS F ON A.id = F.id_asociado
                                            WHERE A.Estado = 0";

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }

            // Filtrar en memoria según las edades
            double minAge = Properties.Settings.Default.Min; // Edad mínima (en años)
            double maxAge = 21; // Edad máxima (en años)
            DataTable filteredTable = dataTable.Clone(); // Clonar estructura del DataTable original

            foreach (DataRow row in dataTable.Rows)
            {
                if (DateTime.TryParse(row["F.fecha_nacimiento"].ToString(), out DateTime fechaNacimiento))
                {
                    // Calcular la edad en años
                    double edad = (DateTime.Now - fechaNacimiento).TotalDays / 365.25;

                    if (edad > minAge && edad <= maxAge)
                    {
                        filteredTable.ImportRow(row);
                    }
                }
            }

            return filteredTable;
        }


        //        public DataTable GetAsociadosHijosFue()
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var connection = GetConnection())
        //            {
        //                connection.Open();
        //                using (var command = new OleDbCommand())
        //                {
        //                    double max = Properties.Settings.Default.Min;
        //                    command.Connection = connection;
        //                    command.CommandText = $@"SELECT DISTINCT a.*
        //FROM asociados AS a INNER JOIN familiares AS f ON a.id = f.id_asociado
        //WHERE (((DateDiff(""yyyy"",[f].[fecha_nacimiento],Date())-IIf(Format([f].[fecha_nacimiento],""mmdd"")>Format(Date(),""mmdd""),1,0))>{max} And (DateDiff(""yyyy"",[f].[fecha_nacimiento],Date())-IIf(Format([f].[fecha_nacimiento],""mmdd"")>Format(Date(),""mmdd""),1,0))<{max +1}) AND ((a.Estado)=0));";
        //                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
        //                    adapter.Fill(dataTable);
        //                }
        //                return dataTable;
        //            }
        //        }

        public DataTable GetAsociadosHijosFue()
        {
            DataTable dataTable = new DataTable();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;

                    // Consulta simplificada que recupera todos los datos relevantes
                    command.CommandText = @"SELECT DISTINCT a.*, f.fecha_nacimiento
                                        FROM asociados AS a 
                                        INNER JOIN familiares AS f ON a.id = f.id_asociado
                                        WHERE a.Estado = 0";

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }

            // Filtrar en memoria según las edades
            double minAge = Properties.Settings.Default.Min; // Edad mínima
            double maxAge = minAge + 1; // Edad máxima
            DataTable filteredTable = dataTable.Clone(); // Clonar estructura del DataTable original

            foreach (DataRow row in dataTable.Rows)
            {
                if (DateTime.TryParse(row["f.fecha_nacimiento"].ToString(), out DateTime fechaNacimiento))
                {
                    // Calcular la edad en años
                    double edad = (DateTime.Now - fechaNacimiento).TotalDays / 365.25;

                    if (edad > minAge && edad < maxAge)
                    {
                        filteredTable.ImportRow(row);
                    }
                }
            }

            return filteredTable;
        }

        //        public DataTable GetAsociadosSinHijos()
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var connection = GetConnection())
        //            {
        //                connection.Open();
        //                using (var command = new OleDbCommand())
        //                {
        //                    double max = Properties.Settings.Default.Min;
        //                    double min = Properties.Settings.Default.Max;
        //                    command.Connection = connection;
        //                    command.CommandText = $@"SELECT A.*
        //FROM Asociados AS A
        //WHERE (((Exists (SELECT 1 
        //    FROM Familiares AS F 
        //    WHERE F.id_asociado = A.id 
        //    AND (DateDiff(""d"", F.fecha_nacimiento, Date()) / 365.25) >= 0
        //    AND (DateDiff(""d"", F.fecha_nacimiento, Date()) / 365.25) <= 10
        //))=False) AND ((A.Estado)=0));
        //";
        //                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
        //                    adapter.Fill(dataTable);
        //                }
        //                return dataTable;
        //            }
        //        }

        public DataTable GetAsociadosSinHijos()
        {
            DataTable dataTable = new DataTable();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;

                    // Recuperar todos los asociados activos
                    command.CommandText = @"SELECT A.id, A.Nombre, A.Estado 
FROM Asociados AS A
WHERE A.Estado = 0";

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }

            // Obtener familiares y filtrar asociados que no tienen hijos menores de 10 años
            DataTable familiaresTable = new DataTable();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;

                    // Recuperar familiares con sus datos de nacimiento
                    command.CommandText = @"SELECT F.id_asociado, F.fecha_nacimiento 
FROM Familiares AS F";

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    adapter.Fill(familiaresTable);
                }
            }

            // Crear una lista con los IDs de asociados que tienen hijos menores de 10 años
            HashSet<int> asociadosConHijos = new HashSet<int>();

            foreach (DataRow row in familiaresTable.Rows)
            {
                if (DateTime.TryParse(row["f.fecha_nacimiento"].ToString(), out DateTime fechaNacimiento))
                {
                    double edad = (DateTime.Now - fechaNacimiento).TotalDays / 365.25;

                    if (edad >= 0 && edad <= 10.9)
                    {
                        int idAsociado = Convert.ToInt32(row["id_asociado"]);
                        asociadosConHijos.Add(idAsociado);
                    }
                }
            }

            // Filtrar el DataTable original para excluir asociados con hijos menores de 10 años
            DataTable resultTable = dataTable.Clone(); // Clonar la estructura del DataTable original

            foreach (DataRow row in dataTable.Rows)
            {
                int idAsociado = Convert.ToInt32(row["id"]);
                if (!asociadosConHijos.Contains(idAsociado))
                {
                    resultTable.ImportRow(row);
                }
            }

            return resultTable;
        }

        public void ActualizarEstadoAsociado(int id, string estado)
        {
            using (var conection = GetConnection())
            {
                conection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = conection;
                    command.CommandText = $"UPDATE Asociados SET Asociados.Estado = '{estado}' WHERE (((Asociados.Id)={id}))";
                    command.ExecuteNonQuery();

                }
            }
        }


        public void ActualizarAsociado(int id, string nombre, long cedula, long telefono, string direccion, string correo, string nacimiento, string ingreso)
        {
            using (var conection = GetConnection())
            {
                conection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = conection;
                    command.CommandText = $"UPDATE Asociados SET Asociados.Nombre = '{nombre}', Asociados.Cedula = {cedula}, Asociados.Direccion = '{direccion}', Asociados.Correo = '{correo}', Asociados.Fecha_nacimiento = '{nacimiento}', Asociados.Fecha_Ingreso = '{ingreso}', Asociados.Telefono = {telefono.ToString()}  WHERE (((Asociados.Id)={id}))";
                    command.ExecuteNonQuery();

                }
            }
        }


        public void BorrrarAsociado(int id)
        {
            using (var conection = GetConnection())
            {
                conection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = conection;
                    command.CommandText = $"DELETE Asociados.Id FROM Asociados WHERE (((Asociados.Id)={id}));";
                    command.ExecuteNonQuery();

                }
            }
        }

    }
}
