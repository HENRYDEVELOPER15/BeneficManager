using AsoDocs.Vista;
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

namespace AsoDocs
{
    public partial class Conexion : Form
    {
        public Conexion()
        {
            InitializeComponent();
            textBox1.Text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MainBasedeDatos.accdb");
        }

        private void Conexion_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nuevaRuta = textBox1.Text;

            Properties.Settings.Default.DatabaseLocation1 = textBox1.Text;

            Properties.Settings.Default.Save();

            MessageBox.Show("Configuraciones guardadas correctamente.");

            Vista.Panelindex form = new Vista.Panelindex();
            form.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Access Database (*.accdb)|*.accdb";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = openFileDialog.FileName;
                }
            }
        }

        private void Conexion_Load(object sender, EventArgs e)
        {

        }
    }
}
