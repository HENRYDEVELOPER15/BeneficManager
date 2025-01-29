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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AsoDocs.Vista
{
    public partial class Detalles : Form
    {
        Asociados_Datos asociado;
        Familiar_Modelo modelo = new Familiar_Modelo();
        StyleDGV style = new StyleDGV();
        public Panelindex panel;
        public Detalles(Asociados_Datos datos, Panelindex panelindex)
        {
            InitializeComponent();

            this.asociado= datos;
            lbnoid.Text = "Identificación: "+ datos.Cedula.ToString();
            lblemail.Text = "Correo Electronico: " + datos.Correo1;
            lblingre.Text = "Fecha de Ingreso: " + datos.Ingreso.ToString("dd/MM/yyyy");
            lblnacer.Text = "Fecha de Nacimiento: " + datos.Nacimiento.ToString("dd/MM/yyyy");
            lblname.Text = datos.Nombre.ToString();
            lblphone.Text = "Teléfono: " + datos.Telefono1;
            lbldirec.Text = "Dirección: " + datos.Direccion1;

            //int id = datos.Id;
            this.panel = panelindex;
            ////MessageBox.Show("Id " + id);

            Cargardatos(datos.Id);

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
            {
                HeaderText = "Acciones",
                Name = "Acciones",
                Image = Properties.Resources.masOpciones, // Aquí usas la imagen desde los recursos
                ImageLayout = DataGridViewImageCellLayout.Zoom, // Ajusta la imagen al tamaño del botón
                FillWeight = 0.2f
            };

            DataGridViewImageColumn imageColumn1 = new DataGridViewImageColumn
            {
                HeaderText = "Acciones1",
                Name = "Acciones",
                Image = Properties.Resources.masOpciones, // Aquí usas la imagen desde los recursos
                ImageLayout = DataGridViewImageCellLayout.Zoom, // Ajusta la imagen al tamaño del botón
                FillWeight = 0.2f
            };


            dataGridView4.Columns.Add(imageColumn);
            dataGridView3.Columns.Add(imageColumn1);

            configurarTabla(dataGridView3);
            configurarTabla(dataGridView4);
        }

        public void Cargardatos(int id)
        {
            dataGridView4.DataSource = modelo.GetFamiliares(id);
            double max = Properties.Settings.Default.Min;
            double min = Properties.Settings.Default.Max;
            dataGridView3.DataSource = modelo.GetFamiliaresRango(id, min, max);

            style.ApplyStylesToDataGridView(dataGridView3);
            style.ApplyStylesToDataGridView(dataGridView4);
        }

        private void configurarTabla(DataGridView dataGridView1)
        {
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["id_Asociado"].Visible = false;
            dataGridView1.Columns["Fecha_nacimiento"].HeaderText = "Nacimiento";
            dataGridView1.Columns["Nombre"].FillWeight = 2;
            dataGridView1.Columns["Genero"].FillWeight = 0.5f;
            dataGridView1.Columns["Edad"].FillWeight = 0.5f;
            dataGridView1.Columns["Fecha_nacimiento"].FillWeight = 1;
        }

        private void Detalles_Load(object sender, EventArgs e)
        {

        }

        private void lbltelefono_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["Acciones"].Index && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["Id"].Value);
                string nombre = dataGridView2.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();

                // Seleccionar la celda para que se muestre el menú en la fila correcta
                dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Mostrar el ContextMenuStrip en la posición del ratón
                menu.Show(Cursor.Position);

                menu.Tag = e.RowIndex;

                MessageBox.Show($"Se hizo clic en la fila con ID: {id} y Nombre: {nombre}");
            }
        }


        private void archivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIndex = (int)menu.Tag;
            int id = Convert.ToInt32(dataGridView3.Rows[rowIndex].Cells["Id"].Value);
            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas Eliminar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                modelo.BorrrarFamiliar(id);
                MessageBox.Show("Registro Eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cargardatos(asociado.Id);
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void actualizarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIndex = (int)menu.Tag;
            Familiar_Datos familiar = new Familiar_Datos()
            {
                Id = Convert.ToInt32(dataGridView3.Rows[rowIndex].Cells["Id"].Value),
                Nombre = dataGridView3.Rows[rowIndex].Cells["Nombre"].Value.ToString(),
                Genero = dataGridView3.Rows[rowIndex].Cells["Genero"].Value.ToString(),
                Nacimiento = (DateTime)dataGridView3.Rows[rowIndex].Cells["Fecha_nacimiento"].Value
            };
            InsertarFamiliarAux aux = new InsertarFamiliarAux(familiar, asociado, this);
            aux.ShowDialog();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView3.Columns["Acciones"].Index && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells["Id"].Value);
                string nombre = dataGridView3.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();

                // Seleccionar la celda para que se muestre el menú en la fila correcta
                dataGridView3.CurrentCell = dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Mostrar el ContextMenuStrip en la posición del ratón
                menu.Show(Cursor.Position);

                menu.Tag = e.RowIndex;

               // MessageBox.Show($"Se hizo clic en la fila con ID: {id} y Nombre: {nombre}");
            }
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView4.Columns["Acciones"].Index && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView4.Rows[e.RowIndex].Cells["Id"].Value);
                string nombre = dataGridView4.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();

                // Seleccionar la celda para que se muestre el menú en la fila correcta
                dataGridView4.CurrentCell = dataGridView4.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Mostrar el ContextMenuStrip en la posición del ratón
                contextMenuStrip1.Show(Cursor.Position);

                contextMenuStrip1.Tag = e.RowIndex;

               // MessageBox.Show($"Se hizo clic en la fila con ID: {id} y Nombre: {nombre}");
            }

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int rowIndex = (int)contextMenuStrip1.Tag;
            int id = Convert.ToInt32(dataGridView3.Rows[rowIndex].Cells["Id"].Value);
            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas Eliminar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                modelo.BorrrarFamiliar(id);
                MessageBox.Show("Registro Eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cargardatos(asociado.Id);
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void menu2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void menu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void actualizarDatosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int rowIndex = (int)contextMenuStrip1.Tag;
            Familiar_Datos familiar = new Familiar_Datos()
            {
                Id = Convert.ToInt32(dataGridView4.Rows[rowIndex].Cells["Id"].Value),
                Nombre = dataGridView4.Rows[rowIndex].Cells["Nombre"].Value.ToString(),
                Genero = dataGridView4.Rows[rowIndex].Cells["Genero"].Value.ToString(),
                Nacimiento = (DateTime)dataGridView4.Rows[rowIndex].Cells["Fecha_nacimiento"].Value
            };
            InsertarFamiliarAux aux = new InsertarFamiliarAux(familiar, asociado, this);
            aux.ShowDialog();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIndex = (int)contextMenuStrip1.Tag;
            int id = Convert.ToInt32(dataGridView4.Rows[rowIndex].Cells["Id"].Value);
            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas archivar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                modelo.BorrrarFamiliar(id);
                MessageBox.Show("Registro Eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cargardatos(asociado.Id);
            }
            else
            {
                MessageBox.Show("Operación cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel.AbrirFormEnPanel(new InsertaAsociado(asociado.Id, asociado.Nombre, asociado.Cedula, asociado.Direccion1, long.Parse(asociado.Telefono1), asociado.Correo1, asociado.Nacimiento, asociado.Ingreso, panel));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertarFamiliarAux aux = new InsertarFamiliarAux(asociado, this);
            aux.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas archivar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                Asociado_Modelo asociados = new Asociado_Modelo();
                asociados.ActualizarEstadoAsociado(asociado.Id, "1");
                MessageBox.Show("Registro Archivado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                MessageBox.Show("Operación cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
