using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace AsoDocs.Controles
{
    public class StyleDGV
    {
        public void ApplyStylesToDataGridView(DataGridView dataGridView1)
        {
            // Estilo de encabezados
            dataGridView1.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(52, 73, 94), // Fondo oscuro
                ForeColor = Color.White,               // Texto blanco
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // Bordes de cabecera
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Bordes de celdas
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Estilo de las celdas
            dataGridView1.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(238, 239, 249), // Fondo claro
                ForeColor = Color.Black,                   // Texto negro
                Font = new Font("Segoe UI", 10),
                SelectionBackColor = Color.FromArgb(52, 73, 94), // Fondo verde al seleccionar
                SelectionForeColor = Color.White,          // Texto blanco al seleccionar
                Padding = new Padding(5)
            };

            // Colores alternados para las filas
            dataGridView1.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(214, 234, 248) // Fondo para filas alternadas
            };

            // Estilo general de la tabla
            dataGridView1.BackgroundColor = Color.White;           // Fondo general blanco
            dataGridView1.BorderStyle = BorderStyle.None;          // Sin bordes externos
            dataGridView1.GridColor = Color.FromArgb(231, 234, 242); // Color de las líneas de la cuadrícula

            // Ajustar configuración
            dataGridView1.EnableHeadersVisualStyles = false; // Desactivar estilos predeterminados
            dataGridView1.RowHeadersVisible = false;        // Ocultar columna de encabezado lateral
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selección de toda la fila

            // Ajustar altura de las filas
            dataGridView1.RowTemplate.Height = 40;
        }
    }
}
