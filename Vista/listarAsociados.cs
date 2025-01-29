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
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace AsoDocs.Vista
{
    public partial class listarAsociados : Form
    {

       
        StyleDGV style = new StyleDGV();
        Asociado_Modelo asociados = new Asociado_Modelo();
        Panelindex panel;
        bool HiAso = false;
        public listarAsociados(Panelindex panelindex)
        {
            InitializeComponent();
            style.ApplyStylesToDataGridView(dataGridView1);
            dataGridView1.DataSource = asociados.GetDataByRangeAndState();
            dataGridView1.Columns["Estado"].Visible = false;
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Direccion"].Visible = false;
            dataGridView1.Columns["Correo"].Visible = false;
            dataGridView1.Columns["Fecha_nacimiento"].Visible = false;
            dataGridView1.Columns["Fecha_Ingreso"].Visible = false;
            dataGridView1.Columns["Fecha_modificacion"].Visible = false;
            this.panel = panelindex;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                HeaderText = "Acciones",
                Name = "Acciones",
                Image = Properties.Resources.masOpciones, // Aquí usas la imagen desde los recursos
                ImageLayout = DataGridViewImageCellLayout.Zoom // Ajusta la imagen al tamaño del botón
            };

            dataGridView1.Columns.Add(imageColumn);
        }

      
        private void listarAsociados_Load(object sender, EventArgs e)
        {
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
      
        }

        private void CalculateFilteredTotalRecords()
        {
            
        }

        private void LoadFilteredPage(int page)
        {
           
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
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

                //MessageBox.Show($"Se hizo clic en la fila con ID: {id} y Nombre: {nombre}");
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = asociados.GetAsociadosHijos();
            dataGridView1.Columns["f.fecha_nacimiento"].Visible = false;
            dataGridView1.Columns["a.fecha_nacimiento"].Visible = false;
            HiAso = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = asociados.GetDataByRangeAndState();
            dataGridView1.Columns["Fecha_nacimiento"].Visible = false;
            HiAso = false;

        }

        private void archivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIndex = (int)menu.Tag;
            int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);
            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas archivar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                asociados.ActualizarEstadoAsociado(id, "1");
                MessageBox.Show("Registro Archivado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = asociados.GetDataByRangeAndState();
            }
            else
            {

                MessageBox.Show("Operación cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            panel.AbrirFormEnPanel(new ListarArchivados(panel));
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            panel.AbrirFormEnPanel(new InsertaAsociado(panel));
        }

        private void menu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void actualizarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIndex = (int)menu.Tag;
            int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);
            long cedula = Convert.ToInt64(dataGridView1.Rows[rowIndex].Cells["Cedula"].Value);
            long telefono = Convert.ToInt64(dataGridView1.Rows[rowIndex].Cells["Telefono"].Value);
            string nombre = dataGridView1.Rows[rowIndex].Cells["Nombre"].Value.ToString();
            string direccion = dataGridView1.Rows[rowIndex].Cells["Direccion"].Value.ToString();
            string correo = dataGridView1.Rows[rowIndex].Cells["Correo"].Value.ToString();
            DateTime ingreso =(DateTime)dataGridView1.Rows[rowIndex].Cells["Fecha_Ingreso"].Value;
            DateTime nacimiento;

            if (HiAso)
            {
                nacimiento = (DateTime)dataGridView1.Rows[rowIndex].Cells["a.fecha_nacimiento"].Value;
            }
            else
            {
                nacimiento = (DateTime)dataGridView1.Rows[rowIndex].Cells["Fecha_nacimiento"].Value;
            }

            panel.AbrirFormEnPanel(new InsertaAsociado(id,nombre,cedula,direccion,telefono,correo,nacimiento,ingreso, panel));

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
            DateTime nacimiento;

            if (HiAso)
            {
                nacimiento = (DateTime)dataGridView1.Rows[rowIndex].Cells["a.fecha_nacimiento"].Value;
            }
            else
            {
                nacimiento = (DateTime)dataGridView1.Rows[rowIndex].Cells["Fecha_nacimiento"].Value;
            }

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

          

            panel.AbrirFormEnPanel(new Detalles(asociados_Datos, panel));
        }

        private void insertarHijosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIndex = (int)menu.Tag;
            int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Id"].Value);
            Asociados_Datos datos = new Asociados_Datos();
            datos.Id = id;
            InsertarFamiliarAux aux = new InsertarFamiliarAux(datos);
            aux.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = asociados.GetDataByRangeAndState(textBox1.Text);
        }
    }
}
