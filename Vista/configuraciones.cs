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

namespace AsoDocs.Vista
{
    public partial class Configuraciones : Form
    {
        public Configuraciones()
        {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.DatabaseLocation1;
            textBox2.Text = Properties.Settings.Default.Max.ToString();
            textBox3.Text = Properties.Settings.Default.Min.ToString();
        }

        private void Configuraciones_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DatabaseLocation1 = textBox1.Text;
            Properties.Settings.Default.Max = double.Parse(textBox2.Text);
            Properties.Settings.Default.Min = double.Parse(textBox3.Text);


            Properties.Settings.Default.Save();

            MessageBox.Show("Configuraciones guardadas correctamente.");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
