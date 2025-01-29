using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsoDocs.Vista;

namespace AsoDocs
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //// Verificar si la configuración de la base de datos está guardada
            //if (string.IsNullOrEmpty(Properties.Settings.Default.DatabaseLocation1) ||
            //    string.IsNullOrEmpty(Properties.Settings.Default.FolderLocation))
            //{
            //    // Ruta de la carpeta "Documentos" del usuario
            //    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //    // Nombre de la carpeta que queremos crear
            //    string folderName = "Documentos_BenefiManager";
            //    // Ruta completa donde se creará la carpeta
            //    string folderPath = Path.Combine(documentsPath, folderName);

            //    // Verificar si la carpeta existe, si no, crearla
            //    if (!Directory.Exists(folderPath))
            //    {
            //        Directory.CreateDirectory(folderPath);
            //    }

            //    // Guardar la ruta en la configuración de la aplicación
            //    Properties.Settings.Default.FolderLocation = folderPath;
            //    Properties.Settings.Default.Save();

            //    // Mostrar el formulario de conexión
            //    Conexion connectionForm = new Conexion();
            //    Application.Run(connectionForm);
            //}


            //Application.Run(new Vista.Panelindex());
            Application.Run(new Conexion());
        }
    }
}
