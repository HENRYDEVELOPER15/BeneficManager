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

namespace AsoDocs.Vista
{
    public partial class InsertarFamiliarAux : Form
    {
        Asociados_Datos asociados;
        Familiar_Datos familiar;
        Detalles detalles;
        public InsertarFamiliarAux(Asociados_Datos asociados, Detalles detalles)
        {
            InitializeComponent();
            this.asociados = asociados;
            this.detalles = detalles;
            button2.Visible = false;
        }

        public InsertarFamiliarAux(Asociados_Datos asociados)
        {
            InitializeComponent();
            this.asociados = asociados;
            button2.Visible = false;
        }

        public InsertarFamiliarAux(Familiar_Datos familiar, Asociados_Datos asociados, Detalles detalles)
        {
            InitializeComponent();
            this.familiar = familiar;
            this.asociados = asociados;
            this.detalles = detalles;
            txtNH.Text = familiar.Nombre;
            txtfecha.Value = familiar.Nacimiento;
            comboBox1.SelectedItem = familiar.Genero;
            button1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = txtNH.Text;
            DateTime fecha = txtfecha.Value;
            string genero = comboBox1.Text;
            Familiar_Modelo familiar = new Familiar_Modelo();
            if (nombre != "" && genero != "")
            {
                familiar.InsertarHijo(nombre, fecha, genero, asociados.Id);
                MessageBox.Show("Familiar Guardado.");
                if (detalles != null)
                {
                    detalles.Cargardatos(asociados.Id);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Faltan datos por digitar.");
            }

        }

        private void InsertarFamiliarAux_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nombre = txtNH.Text;
            DateTime fecha = txtfecha.Value;
            string genero = comboBox1.Text;
            Familiar_Modelo familiar = new Familiar_Modelo();
            if (nombre != "" && genero != "")
            {
                familiar.ActualizarFamiliar(this.familiar.Id,nombre,genero, fecha);
                MessageBox.Show("Familiar Actualizado.");
                if (detalles != null || this.familiar != null) 
                {
                    detalles.Cargardatos(asociados.Id);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Faltan datos por digitar.");
            }
        }
    }
}
