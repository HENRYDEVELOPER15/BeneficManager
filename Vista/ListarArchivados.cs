using AsoDocs.Controles;
using AsoDocs.Datos;
using AsoDocs.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace AsoDocs.Vista
{
    public partial class ListarArchivados : Form
    {
        Asociado_Modelo modelo = new Asociado_Modelo();
        StyleDGV style = new StyleDGV();
        Panelindex panelindex1;
        public ListarArchivados(Panelindex panelindex)
        {
            InitializeComponent();
            dataGridView1.DataSource= modelo.GetArchivados();
            style.ApplyStylesToDataGridView(dataGridView1);
            dataGridView1.Columns["Estado"].Visible = false;
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Direccion"].Visible = false;
            dataGridView1.Columns["Correo"].Visible = false;
            dataGridView1.Columns["Fecha_nacimiento"].Visible = false;
            dataGridView1.Columns["Fecha_Ingreso"].HeaderText = "Fecha de Ingreso";
            dataGridView1.Columns["Fecha_modificacion"].HeaderText = "Fecha de Retiro";

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                HeaderText = "Acciones",
                Name = "Acciones",
                Image = Properties.Resources.masOpciones, // Aquí usas la imagen desde los recursos
                ImageLayout = DataGridViewImageCellLayout.Zoom // Ajusta la imagen al tamaño del botón
            };

            dataGridView1.Columns.Add(imageColumn);

            this.panelindex1 = panelindex; 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Acciones"].Index && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
                string nombre = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();

                // Seleccionar la celda para que se muestre el menú en la fila correcta
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Mostrar el ContextMenuStrip en la posición del ratón
                menu.Show(Cursor.Position);

                menu.Tag = e.RowIndex;

            }
        }

        private void panelP1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ListarArchivados_Load(object sender, EventArgs e)
        {

        }

        private void activarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIndex = (int)menu.Tag;
            int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);
            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas activar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                modelo.ActualizarEstadoAsociado(id, "0");
                MessageBox.Show("Registro Activado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = modelo.GetArchivados();
            }
            else
            {

                MessageBox.Show("Operación cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void verDetallesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIndex = (int)menu.Tag;
            int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);
            long cedula = Convert.ToInt64(dataGridView1.Rows[rowIndex].Cells["Cedula"].Value);
            long telefono = Convert.ToInt64(dataGridView1.Rows[rowIndex].Cells["Telefono"].Value);
            string nombre = dataGridView1.Rows[rowIndex].Cells["Nombre"].Value.ToString();
            string direccion = dataGridView1.Rows[rowIndex].Cells["Direccion"].Value.ToString();
            string correo = dataGridView1.Rows[rowIndex].Cells["Correo"].Value.ToString();
            DateTime ingreso = (DateTime)dataGridView1.Rows[rowIndex].Cells["Fecha_Ingreso"].Value;
            DateTime nacimiento = (DateTime)dataGridView1.Rows[rowIndex].Cells["Fecha_nacimiento"].Value;

            Asociados_Datos asociados_Datos = new Asociados_Datos()
            {
                Id = id,
                Cedula = cedula,
                Telefono1 = telefono.ToString(),
                Nombre = nombre,
                Correo1 = correo,
                Direccion1 = direccion,
                Nacimiento = nacimiento,
                Ingreso = ingreso
            };



            panelindex1.AbrirFormEnPanel(new Detalles(asociados_Datos, panelindex1));
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIndex = (int)menu.Tag;
            int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);
            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas Eliminar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                modelo.BorrrarAsociado(id);
                MessageBox.Show("Registro Eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = modelo.GetArchivados(); ;
            }
            else
            {

                MessageBox.Show("Operación cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = modelo.GetArchivados(textBox1.Text);
        }
    }
}
