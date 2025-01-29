using AsoDocs.Controles;
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

namespace AsoDocs.Vista
{
    public partial class InsertaAsociado : Form
    {
        Asociado_Modelo asociado = new Asociado_Modelo();
        Familiar_Modelo familiar = new Familiar_Modelo();
        Panelindex panelindex;
        StyleDGV styled = new StyleDGV();
        int idA;
        public InsertaAsociado(Panelindex panelindex)
        {
            InitializeComponent();
            styled.ApplyStylesToDataGridView(dataGridView1);
            this.panelindex = panelindex;
        }

        public InsertaAsociado(int id, string nombre, long identificacion, string direccion, long tele, string correo, DateTime nacimiento, DateTime ingreso, Panelindex panelindex)
        {
            InitializeComponent();
            panelP2.Visible= false;
            btnActualizar.Visible = true;
            button2.Visible = false;

            idA = id;
            txtAsociados.Text = nombre;
            txtCorreo.Text = correo;
            txtIdentificacion.Text = identificacion.ToString();
            txtDireccion.Text = direccion;
            txtTelefono.Text = tele.ToString();
            dtpNacimiento.Value = nacimiento;
            dtpIngreso.Value = ingreso;
            this.panelindex = panelindex;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelP1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
       

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreF.Text;
            DateTime fechaNacimiento = dptNacimiF.Value;
            string sex = cmbGenero.Text;
            dataGridView1.Rows.Add(nombre, fechaNacimiento.ToString("dd/MM/yyyy"), sex);
            txtNombreF.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Nombre, Direccion, fechaNacimiento, fechaIngreso, Correo;
            long Identificacion, Telefono;
            Nombre = txtAsociados.Text;
            Correo = txtCorreo.Text;
            Direccion = txtDireccion.Text;
            fechaIngreso = dtpIngreso.Value.ToString("dd/MM/yyyy");
            fechaNacimiento = dtpNacimiento.Value.ToString("dd/MM/yyyy");

            Identificacion = (txtIdentificacion.Text != "") ? long.Parse(txtIdentificacion.Text) : 0;
            Telefono = (txtTelefono.Text != "") ? long.Parse(txtTelefono.Text) : 0;

            int id_a = 0;

            if (Nombre == "" || Identificacion == 0 || Telefono == 0)
            {
                MessageBox.Show("Faltan datos por digitar.");
            }
            else
            {
                id_a = asociado.InsertarAsociado(Identificacion, Nombre, Direccion, Telefono, Correo, fechaNacimiento, fechaIngreso);
                MessageBox.Show("Asociado Guardado. "+ id_a);
            }

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        string nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";
                        string sex = fila.Cells["Genero"].Value?.ToString() ?? "";
                        DateTime fecha = Convert.ToDateTime(fila.Cells["Nacimiento"].Value.ToString());
                        //MessageBox.Show(nombre + sex + fecha);
                        familiar.InsertarHijo(nombre, fecha, sex, id_a);

                    }
                }
            }

            txtAsociados.Text = "";
            txtCorreo.Text = "";
            txtDireccion.Text = "";
            txtIdentificacion.Text = "";
            txtNombreF.Text = "";
            txtTelefono.Text = "";
            dataGridView1.Rows.Clear();
            

        }

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            string Nombre, Direccion, fechaNacimiento, fechaIngreso, Correo;
            long Identificacion, Telefono;
            Nombre = txtAsociados.Text;
            Correo = txtCorreo.Text;
            Direccion = txtDireccion.Text;
            fechaIngreso = dtpIngreso.Value.ToString("dd/MM/yyyy");
            fechaNacimiento = dtpNacimiento.Value.ToString("dd/MM/yyyy");

            Identificacion = (txtIdentificacion.Text != "") ? long.Parse(txtIdentificacion.Text) : 0;
            Telefono = (txtTelefono.Text != "") ? long.Parse(txtTelefono.Text) : 0;

            asociado.ActualizarAsociado(idA, Nombre, Identificacion, Telefono, Direccion, Correo, fechaNacimiento, fechaIngreso);
            MessageBox.Show("Asociado Actualizado. " + idA);

            panelindex.AbrirFormEnPanel(new listarAsociados(panelindex));
        }
    }
}
