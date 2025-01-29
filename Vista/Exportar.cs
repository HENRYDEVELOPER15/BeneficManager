using AsoDocs.Modelo;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO.Packaging;

namespace AsoDocs.Vista
{
    public partial class Exportar : Form
    {
        Reportes_Modelo modelo = new Reportes_Modelo();
        public Exportar()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void HojasRegistrosocial(ExcelPackage package)
        {
            ExcelWorksheet hoja = package.Workbook.Worksheets.Add("Lista de Niños");

            hoja.Cells["A1:H1"].Merge = true;
            hoja.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            hoja.Cells["A1"].Value = "LISTADO DE NiÑOS ASOCIADOS AÑO " + DateTime.Now.ToString("yyyy");

            hoja.Cells[2, 1].Value = "ID";
            hoja.Cells[2, 2].Value = "No. de Identificacion";
            hoja.Cells[2, 3].Value = "Nombre del Asociado                             l";
            hoja.Cells[2, 4].Value = "Cantidad de Hijos";
            hoja.Cells[2, 5].Value = "Nombre Hijo del Asociado";
            hoja.Cells[2, 6].Value = "Fecha de Nacimiento";
            hoja.Cells[2, 7].Value = "Edad";
            hoja.Cells[2, 8].Value = "Genero";

            using (var rango = hoja.Cells["A2:H2"])
            {
                rango.Style.Font.Bold = true;
            }


            ExcelWorksheet hoja1 = package.Workbook.Worksheets.Add("Lista de Excluidos");
            //modelo.InsertarAsociado(hoja);
            hoja1.Cells["A1:H1"].Merge = true;
            hoja1.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Escribir un título en la celda combinada
            hoja1.Cells["A1"].Value = "LISTADO DE HIJOS FUERA DEL RANGO AÑO " + DateTime.Now.ToString("yyyy");

            // Escribir algunos datos en la hoja
            hoja1.Cells[2, 1].Value = "ID";
            hoja1.Cells[2, 2].Value = "No. de Identificacion";
            hoja1.Cells[2, 3].Value = "Nombre del Asociado                             l";
            hoja1.Cells[2, 4].Value = "Cantidad de Hijos";
            hoja1.Cells[2, 5].Value = "Nombre Hijo del Asociado";
            hoja1.Cells[2, 6].Value = "Fecha de Nacimiento";
            hoja1.Cells[2, 7].Value = "Edad";
            hoja1.Cells[2, 8].Value = "Genero";

            using (var rango = hoja1.Cells["A2:H2"])
            {
                rango.Style.Font.Bold = true;
            }

            ExcelWorksheet hoja2 = package.Workbook.Worksheets.Add("Lista de Familiares");
            //modelo.InsertarAsociado(hoja);
            hoja2.Cells["A1:H1"].Merge = true;
            hoja2.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Escribir un título en la celda combinada
            hoja2.Cells["A1"].Value = "LISTADO DE FAMILIARES AÑO " + DateTime.Now.ToString("yyyy");

            // Escribir algunos datos en la hoja
            hoja2.Cells[2, 1].Value = "ID";
            hoja2.Cells[2, 2].Value = "No. de Identificacion";
            hoja2.Cells[2, 4].Value = "Cantidad de Hijos";
            hoja2.Cells[2, 3].Value = "Nombre del Asociado                             l";
            hoja2.Cells[2, 5].Value = "Nombre Hijo del Asociado";
            hoja2.Cells[2, 7].Value = "Edad";
            hoja2.Cells[2, 6].Value = "Fecha de Nacimiento";
            hoja2.Cells[2, 8].Value = "Genero";

            using (var rango = hoja2.Cells["A2:H2"])
            {
                rango.Style.Font.Bold = true;
            }

            Asociado_Modelo asociado = new Asociado_Modelo();
            DataTable table = asociado.GetAsociadosFamul();
            modelo.InsertarAsociado(hoja);
            modelo.InsertarAsociadoConmayor(hoja1);
            modelo.InsertarAsociado(hoja2, table);
            hoja.Cells[hoja.Dimension.Address].AutoFitColumns();
            hoja1.Cells[hoja1.Dimension.Address].AutoFitColumns();
            hoja2.Cells[hoja2.Dimension.Address].AutoFitColumns();
        }

        private void HojasAsociadosHijos(ExcelPackage package)
        {
            ExcelWorksheet hoja = package.Workbook.Worksheets.Add("Lista Asociados");
            //modelo.InsertarAsociado(hoja);
            hoja.Cells["A1:H1"].Merge = true;
            hoja.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Escribir un título en la celda combinada
            hoja.Cells["A1"].Value = "LISTADO DE ASOCIADOS AÑO " + DateTime.Now.ToString("yyyy");

            // Escribir algunos datos en la hoja
            hoja.Cells[2, 1].Value = "ID";
            hoja.Cells[2, 2].Value = "No. de Identificacion";
            hoja.Cells[2, 3].Value = "Nombre del Asociado                             l";
            hoja.Cells[2, 4].Value = "Dirección";
            hoja.Cells[2, 5].Value = "Teléfono";
            hoja.Cells[2, 6].Value = "Correo Electronico";
            hoja.Cells[2, 7].Value = "Fecha de Nacimiento";
            hoja.Cells[2, 8].Value = "Fecha de Ingreso";

            using (var rango = hoja.Cells["A2:H2"])
            {
                rango.Style.Font.Bold = true;
            }
            Asociado_Modelo asociado = new Asociado_Modelo();
            DataTable table = asociado.GetAsociadosFamul();
            modelo.InsertarAsociadoSinHijos(hoja);
            hoja.Cells[hoja.Dimension.Address].AutoFitColumns();
        }

        private void HojasAsociadosSinHijos(ExcelPackage package, int min, int max)
        {
            ExcelWorksheet hoja = package.Workbook.Worksheets.Add("Lista de Retirados");
            //modelo.InsertarAsociado(hoja);
            hoja.Cells["A1:H1"].Merge = true;
            hoja.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Escribir un título en la celda combinada
            hoja.Cells["A1"].Value = "LISTADO DE ASOCIADOS RETIRADOS AÑO " + DateTime.Now.ToString("yyyy");

            // Escribir algunos datos en la hoja
            hoja.Cells[2, 1].Value = "ID";
            hoja.Cells[2, 2].Value = "No. de Identificacion";
            hoja.Cells[2, 3].Value = "Nombre del Asociado                             l";
            hoja.Cells[2, 4].Value = "Dirección";
            hoja.Cells[2, 5].Value = "Teléfono";
            hoja.Cells[2, 6].Value = "Correo Electronico";
            hoja.Cells[2, 7].Value = "Fecha de Nacimiento";
            hoja.Cells[2, 8].Value = "Fecha de Ingreso";

            using (var rango = hoja.Cells["A2:H2"])
            {
                rango.Style.Font.Bold = true;
            }
            Asociado_Modelo asociado = new Asociado_Modelo();
            DataTable table = asociado.GetArchivados(max, min);
            modelo.InsertarAsociadoSinHijos(hoja, table);
            hoja.Cells[hoja.Dimension.Address].AutoFitColumns();
        }

        private void CrearArchivoExcel(string rutaArchivo)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Configurar la licencia

                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet hoja = package.Workbook.Worksheets.Add("Lista de Niños");
                    //modelo.InsertarAsociado(hoja);
                    hoja.Cells["A1:H1"].Merge = true;
                    hoja.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Escribir un título en la celda combinada
                    hoja.Cells["A1"].Value = "LISTADO DE NiÑOS ASOCIADOS AÑO " + DateTime.Now.ToString("yyyy");

                    // Escribir algunos datos en la hoja
                    hoja.Cells[2, 1].Value = "ID";
                    hoja.Cells[2, 2].Value = "No. de Identificacion";
                    hoja.Cells[2, 3].Value = "Nombre del Asociado                             l";
                    hoja.Cells[2, 4].Value = "Cantidad de Hijos";
                    hoja.Cells[2, 5].Value = "Nombre Hijo del Asociado";
                    hoja.Cells[2, 6].Value = "Fecha de Nacimiento";
                    hoja.Cells[2, 7].Value = "Edad";
                    hoja.Cells[2, 8].Value = "Genero";

                    using (var rango = hoja.Cells["A2:H2"])
                    {
                        rango.Style.Font.Bold = true;
                    }


                    ExcelWorksheet hoja1 = package.Workbook.Worksheets.Add("Lista de Excluidos");
                    //modelo.InsertarAsociado(hoja);
                    hoja1.Cells["A1:H1"].Merge = true;
                    hoja1.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Escribir un título en la celda combinada
                    hoja1.Cells["A1"].Value = "LISTADO DE HIJOS FUERA DEL RANGO AÑO " + DateTime.Now.ToString("yyyy");

                    // Escribir algunos datos en la hoja
                    hoja1.Cells[2, 1].Value = "ID";
                    hoja1.Cells[2, 2].Value = "No. de Identificacion";
                    hoja1.Cells[2, 3].Value = "Nombre del Asociado                             l";
                    hoja1.Cells[2, 4].Value = "Cantidad de Hijos";
                    hoja1.Cells[2, 5].Value = "Nombre Hijo del Asociado";
                    hoja1.Cells[2, 6].Value = "Fecha de Nacimiento";
                    hoja1.Cells[2, 7].Value = "Edad";
                    hoja1.Cells[2, 8].Value = "Genero";

                    using (var rango = hoja1.Cells["A2:H2"])
                    {
                        rango.Style.Font.Bold = true;
                    }

                    ExcelWorksheet hoja2 = package.Workbook.Worksheets.Add("Lista de Familiares");
                    //modelo.InsertarAsociado(hoja);
                    hoja2.Cells["A1:H1"].Merge = true;
                    hoja2.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Escribir un título en la celda combinada
                    hoja2.Cells["A1"].Value = "LISTADO DE FAMILIARES AÑO " + DateTime.Now.ToString("yyyy");

                    // Escribir algunos datos en la hoja
                    hoja2.Cells[2, 1].Value = "ID";
                    hoja2.Cells[2, 2].Value = "No. de Identificacion";
                    hoja2.Cells[2, 4].Value = "Cantidad de Hijos";
                    hoja2.Cells[2, 3].Value = "Nombre del Asociado                             l";
                    hoja2.Cells[2, 5].Value = "Nombre Hijo del Asociado";
                    hoja2.Cells[2, 7].Value = "Edad";
                    hoja2.Cells[2, 6].Value = "Fecha de Nacimiento";
                    hoja2.Cells[2, 8].Value = "Genero";

                    using (var rango = hoja2.Cells["A2:H2"])
                    {
                        rango.Style.Font.Bold = true;
                    }

                    Asociado_Modelo asociado = new Asociado_Modelo();
                    DataTable table = asociado.GetAsociadosFamul();
                    modelo.InsertarAsociado(hoja);
                    modelo.InsertarAsociadoConmayor(hoja1);
                    modelo.InsertarAsociado(hoja2, table);
                    hoja.Cells[hoja.Dimension.Address].AutoFitColumns();
                    hoja1.Cells[hoja1.Dimension.Address].AutoFitColumns();
                    hoja2.Cells[hoja2.Dimension.Address].AutoFitColumns();
                    FileInfo archivo = new FileInfo(rutaArchivo);
                    package.SaveAs(archivo);

                    MessageBox.Show("Archivo Excel guardado exitosamente en: " + rutaArchivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el archivo Excel: " + ex.Message);
            }
        }

        private void CrearArchivoExcel1(string rutaArchivo)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Configurar la licencia

                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet hoja = package.Workbook.Worksheets.Add("Lista de Asociados");
                    //modelo.InsertarAsociado(hoja);
                    hoja.Cells["A1:H1"].Merge = true;
                    hoja.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Escribir un título en la celda combinada
                    hoja.Cells["A1"].Value = "LISTADO DE ASOCIADOS AÑO " + DateTime.Now.ToString("yyyy");

                    // Escribir algunos datos en la hoja
                    hoja.Cells[2, 1].Value = "ID";
                    hoja.Cells[2, 2].Value = "No. de Identificacion";
                    hoja.Cells[2, 3].Value = "Nombre del Asociado                             l";
                    hoja.Cells[2, 4].Value = "Dirección";
                    hoja.Cells[2, 5].Value = "Teléfono";
                    hoja.Cells[2, 6].Value = "Correo Electronico";
                    hoja.Cells[2, 7].Value = "Fecha de Nacimiento";
                    hoja.Cells[2, 8].Value = "Fecha de Ingreso";

                    using (var rango = hoja.Cells["A2:H2"])
                    {
                        rango.Style.Font.Bold = true;
                    }
                    Asociado_Modelo asociado = new Asociado_Modelo();
                    DataTable table = asociado.GetAsociadosFamul();
                    modelo.InsertarAsociadoSinHijos(hoja);
                    hoja.Cells[hoja.Dimension.Address].AutoFitColumns();
                    FileInfo archivo = new FileInfo(rutaArchivo);
                    package.SaveAs(archivo);

                    MessageBox.Show("Archivo Excel guardado exitosamente en: " + rutaArchivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el archivo Excel: " + ex.Message);
            }
        }

        private void CrearArchivoExcel2(string rutaArchivo, int min, int max)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Configurar la licencia

                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet hoja = package.Workbook.Worksheets.Add("Lista de Asociados");
                    //modelo.InsertarAsociado(hoja);
                    hoja.Cells["A1:H1"].Merge = true;
                    hoja.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Escribir un título en la celda combinada
                    hoja.Cells["A1"].Value = "LISTADO DE ASOCIADOS RETIRADOS AÑO " + DateTime.Now.ToString("yyyy");

                    // Escribir algunos datos en la hoja
                    hoja.Cells[2, 1].Value = "ID";
                    hoja.Cells[2, 2].Value = "No. de Identificacion";
                    hoja.Cells[2, 3].Value = "Nombre del Asociado                             l";
                    hoja.Cells[2, 4].Value = "Dirección";
                    hoja.Cells[2, 5].Value = "Teléfono";
                    hoja.Cells[2, 6].Value = "Correo Electronico";
                    hoja.Cells[2, 7].Value = "Fecha de Nacimiento";
                    hoja.Cells[2, 8].Value = "Fecha de Ingreso";

                    using (var rango = hoja.Cells["A2:H2"])
                    {
                        rango.Style.Font.Bold = true;
                    }
                    Asociado_Modelo asociado = new Asociado_Modelo();
                    DataTable table = asociado.GetArchivados(max, min);
                    modelo.InsertarAsociadoSinHijos(hoja, table);
                    hoja.Cells[hoja.Dimension.Address].AutoFitColumns();
                    FileInfo archivo = new FileInfo(rutaArchivo);
                    package.SaveAs(archivo);

                    MessageBox.Show("Archivo Excel guardado exitosamente en: " + rutaArchivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el archivo Excel: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (rbnSeparados.Checked)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {

                    if (cbxListaAnoH.Checked)
                    {

                        saveFileDialog.Filter = "Excel Files|*.xlsx";
                        saveFileDialog.Title = "Guardar Archivo Excel";
                        saveFileDialog.FileName = "Lista-Asociados.xlsx"; // Nombre predeterminado

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Obtener la ruta seleccionada por el usuario
                            string rutaArchivo = saveFileDialog.FileName;

                            // Llamar al método para crear el archivo Excel
                            CrearArchivoExcel1(rutaArchivo);
                        }

                    }


                    if (cbxListAH.Checked)
                    {

                        saveFileDialog.Filter = "Excel Files|*.xlsx";
                        saveFileDialog.Title = "Guardar Archivo Excel";
                        saveFileDialog.FileName = "Hijos-Asociados.xlsx"; // Nombre predeterminado

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Obtener la ruta seleccionada por el usuario
                            string rutaArchivo = saveFileDialog.FileName;

                            // Llamar al método para crear el archivo Excel
                            CrearArchivoExcel(rutaArchivo);
                        }

                    }
                    
                    if (cbxRegistro.Checked)
                    {
                        string input = Interaction.InputBox("Por favor, ingresa el año en el que inicia:", "Entrada de número", "2024");
                        int añoMin = 0, añoMax = 0;
                        // Verificar si se ingresó un valor
                        if (int.TryParse(input, out int result))
                        {
                            añoMin = result;
                        }
                        else
                        {
                            Console.WriteLine("No se ingresó un número válido.");
                        }

                        string input2 = Interaction.InputBox("Por favor, ingresa el año en el que finaliza:", "Entrada de número", "2024");

                        // Verificar si se ingresó un valor
                        if (int.TryParse(input2, out int result2))
                        {
                            añoMax = result2;
                        }
                        else
                        {
                            Console.WriteLine("No se ingresó un número válido.");
                        }

                        saveFileDialog.Filter = "Excel Files|*.xlsx";
                        saveFileDialog.Title = "Guardar Archivo Excel";
                        saveFileDialog.FileName = "Registro Social.xlsx"; // Nombre predeterminado

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Obtener la ruta seleccionada por el usuario
                            string rutaArchivo = saveFileDialog.FileName;

                            // Llamar al método para crear el archivo Excel
                            CrearArchivoExcel2(rutaArchivo, añoMin, añoMax);
                        }
                    }

                }
            }

            if (rbnUno.Checked)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.Title = "Guardar Archivo Excel";
                    saveFileDialog.FileName = "Datos.xlsx"; // Nombre predeterminado

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string rutaArchivo = saveFileDialog.FileName;
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Configurar la licencia

                        using (ExcelPackage package = new ExcelPackage())
                        {

                            if (cbxListaAnoH.Checked)
                            {
                                HojasAsociadosHijos(package);
                            }


                            if (cbxListAH.Checked)
                            {
                                HojasRegistrosocial(package);
                            }

                           
                            if (cbxRegistro.Checked)
                            {
                                string input = Interaction.InputBox("Por favor, ingresa el año en el que inicia:", "Entrada de número", "2024");
                                int añoMin = 0, añoMax = 0;
                                // Verificar si se ingresó un valor
                                if (int.TryParse(input, out int result))
                                {
                                    añoMin = result;
                                }
                                else
                                {
                                    Console.WriteLine("No se ingresó un número válido.");
                                }

                                string input2 = Interaction.InputBox("Por favor, ingresa el año en el que finaliza:", "Entrada de número", "2024");

                                // Verificar si se ingresó un valor
                                if (int.TryParse(input2, out int result2))
                                {
                                    añoMax = result2;
                                }
                                else
                                {
                                    Console.WriteLine("No se ingresó un número válido.");
                                }

                                HojasAsociadosSinHijos(package, añoMin, añoMax);

                            }

                            FileInfo archivo = new FileInfo(rutaArchivo);
                            package.SaveAs(archivo);

                            MessageBox.Show("Archivo Excel guardado exitosamente en: " + rutaArchivo);
                        }

                    }
                       
                }
            }
        }

        private void Exportar_Load(object sender, EventArgs e)
        {

        }

        private void cbxListaAnoH_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbxRegistro_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
