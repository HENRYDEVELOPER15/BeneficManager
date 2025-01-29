using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoDocs.Modelo
{
    public class Reportes_Modelo:Conexion
    {
        Familiar_Modelo modeloF = new Familiar_Modelo();
        Asociado_Modelo modeloA = new Asociado_Modelo();
        public int ObtenerCantidadHijosConEstadoCero(int idAsociado, double min, double max)
        {
            int cantidadHijos = 0;

            using (var connection = GetConnection())
            {
                // Consulta SQL que cuenta los hijos con estado = 0 para un asociado específico
                string query = $@"SELECT Count(F.id) AS Cantidad_Familiares_0_10
FROM Familiares AS F
WHERE (((F.id_asociado)={idAsociado}) AND ((DateDiff(""yyyy"",[F].[fecha_nacimiento],Date()))>{min} And (DateDiff(""yyyy"",[F].[fecha_nacimiento],Date()))<{max}));
";

                // Crear el comando
                OleDbCommand command = new OleDbCommand(query, connection);

                try
                {
                    // Abrir la conexión
                    connection.Open();

                    // Ejecutar la consulta y obtener el resultado
                    cantidadHijos = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return cantidadHijos;
        }

        public void InsertarAsociado(ExcelWorksheet hoja)
        {
            DataTable table = modeloA.GetAsociadosHijos();

            int row = 3;
            int row1 = 3;

            foreach (DataRow fila in table.Rows)
            {
                int id = Convert.ToInt32(fila["Id"]);
                string no = fila["Cedula"].ToString();
                string nombre = fila["Nombre"].ToString();
                double max = Properties.Settings.Default.Min;
                double min = Properties.Settings.Default.Max;
                int cant = ObtenerCantidadHijosConEstadoCero(id, min, max);
                DataTable dt = modeloF.GetFamiliaresRango(id, min, max);


                hoja.Cells["A" + row + ":A" + (row + cant - 1)].Merge = true;
                hoja.Cells["A" + row + ":A" + (row + cant - 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["A" + row + ":A" + (row + cant - 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["B" + row + ":B" + (row + cant - 1)].Merge = true;
                hoja.Cells["B" + row + ":B" + (row + cant - 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["B" + row + ":B" + (row + cant - 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["C" + row + ":C" + (row + cant - 1)].Merge = true;
                hoja.Cells["C" + row + ":C" + (row + cant - 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["C" + row + ":C" + (row + cant - 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["D" + row + ":D" + (row + cant - 1)].Merge = true;
                hoja.Cells["D" + row + ":D" + (row + cant - 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["D" + row + ":D" + (row + cant - 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells[row, 1].Value = id;
                hoja.Cells[row, 2].Value = no;
                hoja.Cells[row, 3].Value = nombre;
                hoja.Cells[row, 4].Value = cant;


                foreach (DataRow filah in dt.Rows)
                {
                    string genero = filah["Genero"].ToString();
                    string nombre1 = filah["Nombre"].ToString();
                    DateTime fechaNacimiento = Convert.ToDateTime(filah["Fecha_nacimiento"].ToString());
                    string fecha = fechaNacimiento.ToString("dd/MM/yyyy");
                    int edad = Convert.ToInt32(filah["Edad"].ToString());

                    DateTime currentDate = DateTime.Now;
                    double age = (currentDate - fechaNacimiento).TotalDays / 365.25;
                    age = Math.Round(age, 2);

                    hoja.Cells[row1, 5].Value = nombre1;
                    hoja.Cells[row1, 6].Value = fecha;
                    hoja.Cells[row1, 7].Value = age;
                    hoja.Cells[row1, 8].Value = genero;

                    hoja.Cells["E" + row1 + ":E" + row1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    hoja.Cells["F" + row1 + ":F" + row1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    hoja.Cells["G" + row1 + ":G" + row1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    hoja.Cells["H" + row1 + ":H" + row1].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    row1++;

                }
                hoja.Cells["A" + row + ":A" + (row + cant - 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["B" + row + ":B" + (row + cant - 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["C" + row + ":C" + (row + cant - 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["D" + row + ":D" + (row + cant - 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                hoja.Cells[hoja.Dimension.Address].AutoFitColumns();
                row += cant;
            }

        }


        public void InsertarAsociado(ExcelWorksheet hoja, DataTable table)
        {
            int row = 3;
            int row1 = 3;

            foreach (DataRow fila in table.Rows)
            {
                int id = Convert.ToInt32(fila["Id"]);
                string no = fila["Cedula"].ToString();
                string nombre = fila["Nombre"].ToString();
                double max = Properties.Settings.Default.Min;
                double min = Properties.Settings.Default.Max;
                int cant = ObtenerCantidadHijosConEstadoCero(id, max, 21);
                DataTable dt = modeloF.GetFamiliaresRango(id, max, 21);


                hoja.Cells["A" + row + ":A" + (row + cant - 1)].Merge = true;
                hoja.Cells["A" + row + ":A" + (row + cant - 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["A" + row + ":A" + (row + cant - 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["B" + row + ":B" + (row + cant - 1)].Merge = true;
                hoja.Cells["B" + row + ":B" + (row + cant - 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["B" + row + ":B" + (row + cant - 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["C" + row + ":C" + (row + cant - 1)].Merge = true;
                hoja.Cells["C" + row + ":C" + (row + cant - 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["C" + row + ":C" + (row + cant - 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["D" + row + ":D" + (row + cant - 1)].Merge = true;
                hoja.Cells["D" + row + ":D" + (row + cant - 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["D" + row + ":D" + (row + cant - 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells[row, 1].Value = id;
                hoja.Cells[row, 2].Value = no;
                hoja.Cells[row, 3].Value = nombre;
                hoja.Cells[row, 4].Value = cant;


                foreach (DataRow filah in dt.Rows)
                {
                    string genero = filah["Genero"].ToString();
                    string nombre1 = filah["Nombre"].ToString();
                    DateTime fechaNacimiento = Convert.ToDateTime(filah["Fecha_nacimiento"].ToString());
                    string fecha = fechaNacimiento.ToString("dd/MM/yyyy");
                    int edad = Convert.ToInt32(filah["Edad"].ToString());

                    DateTime currentDate = DateTime.Now;
                    double age = (currentDate - fechaNacimiento).TotalDays / 365.25;
                    age = Math.Round(age, 2);

                    hoja.Cells[row1, 5].Value = nombre1;
                    hoja.Cells[row1, 6].Value = fecha;
                    hoja.Cells[row1, 7].Value = age;
                    hoja.Cells[row1, 8].Value = genero;

                    hoja.Cells["E" + row1 + ":E" + row1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    hoja.Cells["F" + row1 + ":F" + row1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    hoja.Cells["G" + row1 + ":G" + row1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    hoja.Cells["H" + row1 + ":H" + row1].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    row1++;

                }
                hoja.Cells["A" + row + ":A" + (row + cant - 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["B" + row + ":B" + (row + cant - 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["C" + row + ":C" + (row + cant - 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["D" + row + ":D" + (row + cant - 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                hoja.Cells[hoja.Dimension.Address].AutoFitColumns();
                row += cant;
            }

        }

        public void InsertarAsociadoConmayor(ExcelWorksheet hoja)
        {
            DataTable table = modeloA.GetAsociadosHijosFue();

            int row = 3;
            int row1 = 3;

            foreach (DataRow fila in table.Rows)
            {
                int id = Convert.ToInt32(fila["Id"]);
                string no = fila["Cedula"].ToString();
                string nombre = fila["Nombre"].ToString();
                double max = Properties.Settings.Default.Min;
                int cant = ObtenerCantidadHijosConEstadoCero(id, max, max + 1);
                DataTable dt = modeloF.GetFamiliaresRango(id, max, max + 1);


                hoja.Cells["A" + row + ":A" + (row + cant - 1)].Merge = true;
                hoja.Cells["A" + row + ":A" + (row + cant - 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["A" + row + ":A" + (row + cant - 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["B" + row + ":B" + (row + cant - 1)].Merge = true;
                hoja.Cells["B" + row + ":B" + (row + cant - 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["B" + row + ":B" + (row + cant - 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["C" + row + ":C" + (row + cant - 1)].Merge = true;
                hoja.Cells["C" + row + ":C" + (row + cant - 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["C" + row + ":C" + (row + cant - 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["D" + row + ":D" + (row + cant - 1)].Merge = true;
                hoja.Cells["D" + row + ":D" + (row + cant - 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["D" + row + ":D" + (row + cant - 1)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells[row, 1].Value = id;
                hoja.Cells[row, 2].Value = no;
                hoja.Cells[row, 3].Value = nombre;
                hoja.Cells[row, 4].Value = cant;


                foreach (DataRow filah in dt.Rows)
                {
                    string genero = filah["Genero"].ToString();
                    string nombre1 = filah["Nombre"].ToString();
                    DateTime fechaNacimiento = Convert.ToDateTime(filah["Fecha_nacimiento"].ToString());
                    string fecha = fechaNacimiento.ToString("dd/MM/yyyy");
                    int edad = Convert.ToInt32(filah["Edad"].ToString());

                    DateTime currentDate = DateTime.Now;
                    double age = (currentDate - fechaNacimiento).TotalDays / 365.25;
                    age = Math.Round(age, 2);

                    hoja.Cells[row1, 5].Value = nombre1;
                    hoja.Cells[row1, 6].Value = fecha;
                    hoja.Cells[row1, 7].Value = age;
                    hoja.Cells[row1, 8].Value = genero;

                    hoja.Cells["E" + row1 + ":E" + row1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    hoja.Cells["F" + row1 + ":F" + row1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    hoja.Cells["G" + row1 + ":G" + row1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    hoja.Cells["H" + row1 + ":H" + row1].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    row1++;

                }
                hoja.Cells["A" + row + ":A" + (row + cant - 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["B" + row + ":B" + (row + cant - 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["C" + row + ":C" + (row + cant - 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["D" + row + ":D" + (row + cant - 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                hoja.Cells[hoja.Dimension.Address].AutoFitColumns();
                row += cant;
            }

        }



        public void InsertarAsociadoSinHijos(ExcelWorksheet hoja)
        {
            DataTable table = modeloA.GetDataByRangeAndState();
            int row = 3;
            foreach (DataRow fila in table.Rows)
            {
                int id = Convert.ToInt32(fila["Id"]);
                string no = fila["Cedula"].ToString();
                string nombre = fila["Nombre"].ToString();
                string direccion = fila["Direccion"].ToString();
                string telefono = fila["Telefono"].ToString();
                string correo = fila["Correo"].ToString();
                DateTime fechaN = Convert.ToDateTime(fila["Fecha_nacimiento"].ToString());
                DateTime fechaIng = Convert.ToDateTime(fila["Fecha_Ingreso"].ToString());


                hoja.Cells["A" + row + ":A" + row].Merge = true;
                hoja.Cells["A" + row + ":A" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["A" + row + ":A" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["B" + row + ":B" + row].Merge = true;
                hoja.Cells["B" + row + ":B" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["B" + row + ":B" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["C" + row + ":C" + row].Merge = true;
                hoja.Cells["C" + row + ":C" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["C" + row + ":C" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["D" + row + ":D" + row].Merge = true;
                hoja.Cells["D" + row + ":D" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["D" + row + ":D" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["E" + row + ":E" + row].Merge = true;
                hoja.Cells["E" + row + ":E" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["E" + row + ":E" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["F" + row + ":F" + row].Merge = true;
                hoja.Cells["F" + row + ":F" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["F" + row + ":F" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["G" + row + ":G" + row].Merge = true;
                hoja.Cells["G" + row + ":G" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["G" + row + ":G" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["H" + row + ":H" + row].Merge = true;
                hoja.Cells["H" + row + ":H" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["H" + row + ":H" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells[row, 1].Value = id;
                hoja.Cells[row, 2].Value = no;
                hoja.Cells[row, 3].Value = nombre;
                hoja.Cells[row, 4].Value = direccion;
                hoja.Cells[row, 5].Value = telefono;
                hoja.Cells[row, 6].Value = correo;
                hoja.Cells[row, 7].Value = fechaN.ToString("dd/MM/yyyy");
                hoja.Cells[row, 8].Value = fechaIng.ToString("dd/MM/yyyy");


                hoja.Cells["A" + row + ":A" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["B" + row + ":B" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["C" + row + ":C" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["D" + row + ":D" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["E" + row + ":E" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["F" + row + ":F" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["G" + row + ":G" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["H" + row + ":H" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);


                hoja.Cells[hoja.Dimension.Address].AutoFitColumns();
                row++;
            }

        }


        public void InsertarAsociadoSinHijos(ExcelWorksheet hoja, DataTable table)
        {
            int row = 3;
            foreach (DataRow fila in table.Rows)
            {
                int id = Convert.ToInt32(fila["Id"]);
                string no = fila["Cedula"].ToString();
                string nombre = fila["Nombre"].ToString();
                string direccion = fila["Direccion"].ToString();
                string telefono = fila["Telefono"].ToString();
                string correo = fila["Correo"].ToString();
                DateTime fechaN = Convert.ToDateTime(fila["Fecha_nacimiento"].ToString());
                DateTime fechaIng = Convert.ToDateTime(fila["Fecha_Ingreso"].ToString());


                hoja.Cells["A" + row + ":A" + row].Merge = true;
                hoja.Cells["A" + row + ":A" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["A" + row + ":A" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["B" + row + ":B" + row].Merge = true;
                hoja.Cells["B" + row + ":B" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["B" + row + ":B" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["C" + row + ":C" + row].Merge = true;
                hoja.Cells["C" + row + ":C" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["C" + row + ":C" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["D" + row + ":D" + row].Merge = true;
                hoja.Cells["D" + row + ":D" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["D" + row + ":D" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["E" + row + ":E" + row].Merge = true;
                hoja.Cells["E" + row + ":E" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["E" + row + ":E" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["F" + row + ":F" + row].Merge = true;
                hoja.Cells["F" + row + ":F" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["F" + row + ":F" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["G" + row + ":G" + row].Merge = true;
                hoja.Cells["G" + row + ":G" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["G" + row + ":G" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells["H" + row + ":H" + row].Merge = true;
                hoja.Cells["H" + row + ":H" + row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                hoja.Cells["H" + row + ":H" + row].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                hoja.Cells[row, 1].Value = id;
                hoja.Cells[row, 2].Value = no;
                hoja.Cells[row, 3].Value = nombre;
                hoja.Cells[row, 4].Value = direccion;
                hoja.Cells[row, 5].Value = telefono;
                hoja.Cells[row, 6].Value = correo;
                hoja.Cells[row, 7].Value = fechaN.ToString("dd/MM/yyyy");
                hoja.Cells[row, 8].Value = fechaIng.ToString("dd/MM/yyyy");


                hoja.Cells["A" + row + ":A" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["B" + row + ":B" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["C" + row + ":C" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["D" + row + ":D" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["E" + row + ":E" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["F" + row + ":F" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["G" + row + ":G" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                hoja.Cells["H" + row + ":H" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);


                hoja.Cells[hoja.Dimension.Address].AutoFitColumns();
                row++;
            }

        }
    }
}
