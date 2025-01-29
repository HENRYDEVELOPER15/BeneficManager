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
using System.Windows.Controls;
using System.Windows.Forms;
using AsoDocs.Modelo;

namespace AsoDocs.Vista
{
    public partial class Panelindex : Form
    {
        private int targetWidth;
        int targetHeight;
        private bool activar;
        private bool activar2 = true;
        private int step = 35;
        public Panelindex()
        {
            InitializeComponent();
            activar = true;
            activar2 = false;
            pnl.Width = 75;
            if (activar)
            {
                targetWidth = 285;
                aniWidht.Start();
                activar = false;
            }
            else
            {
                targetWidth = 44;
                aniWidht.Start();
                activar = true;
            }

            if (activar2)
            {
                targetHeight = 130;
                aniHeight.Start();
                activar2 = false;
            }
            else
            {
                targetHeight = 0;
                aniHeight.Start();
                activar2 = true;

            }
            disminuir();
            AbrirFormEnPanel(new listarAsociados(this));
            lblUbi.Text = "Asociados";

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (activar)
            {
                targetWidth = 285;
                aniWidht.Start();
                activar = false;
            }
            else
            {
                targetWidth = 44;
                aniWidht.Start();
                activar = true;
            }

            if (!activar2)
            {
                targetHeight = 0;
                aniHeight.Start();
                activar2 = true;
            }
            disminuir();
        }
        private void disminuir()
        {
            if (activar)
            {
                btnAsocia.Text = "";
                btnConfig.Text = "";
                btnCopy.Text = "";
                btnExpor.Text = "";
            }
            else
            {
                btnAsocia.Text = "                    Asociados";
                btnConfig.Text = "                    Configuraciones";
                btnCopy.Text = "                    Copia de Seguridad";
                btnExpor.Text = "                    Exportar";
            }
        }

        private void aniWidht_Tick(object sender, EventArgs e)
        {
            if (pnl.Width != targetWidth)
            {
                if (pnl.Width < targetWidth)
                    pnl.Width = Math.Min(pnl.Width + step, targetWidth);
                else
                    pnl.Width = Math.Max(pnl.Width - step, targetWidth);
            }

            if (pnl.Width == targetWidth)
            {
                aniWidht.Stop();
            }

        }

        private void aniHeight_Tick(object sender, EventArgs e)
        {
            if (panelSub.Height != targetHeight)
            {
                if (panelSub.Height < targetHeight)
                    panelSub.Height = Math.Min(panelSub.Height + step, targetHeight);
                else
                    panelSub.Height = Math.Max(panelSub.Height - step, targetHeight);
            }

            if (panelSub.Width == targetHeight)
            {
                aniWidht.Stop();
            }
        }


        private void btnAsocia_Click(object sender, EventArgs e)
        {
            if (activar2)
            {
                targetHeight = 130;
                aniHeight.Start();
                activar2 = false;
            }
            else
            {
                targetHeight = 0;
                aniHeight.Start();
                activar2 = true;

            }

            if (activar)
            {
                targetWidth = 285;
                aniWidht.Start();
                activar = false;
            }
            disminuir();
        }

        public void AbrirFormEnPanel(object formhija)
        {
            if (this.panelPadre.Controls.Count > 0)
                this.panelPadre.Controls.RemoveAt(0);
            Form fh = formhija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelPadre.Controls.Add(fh);
            this.panelPadre.Tag = fh;
            fh.Show();

        }
        private void Panel_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new listarAsociados(this));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new ListarArchivados(this));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new InsertaAsociado(this));
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new Configuraciones());
        }

        private void lblUbi_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel(new listarAsociados(this));
        }
       
        private void btnCopy_Click(object sender, EventArgs e)
        {
            string sourceFolder = Properties.Settings.Default.FolderLocation;
            string databaseFile = Properties.Settings.Default.DatabaseLocation1;

            // Abrir un diálogo para seleccionar la carpeta de destino
            using (FolderBrowserDialog destFolderDialog = new FolderBrowserDialog())
            {
                destFolderDialog.Description = "Seleccione la carpeta de destino para la copia de seguridad";
                if (destFolderDialog.ShowDialog() == DialogResult.OK)
                {
                    string destFolder = destFolderDialog.SelectedPath;


                    string databaseBackupFolder = Path.Combine(destFolder, "BaseDatos");

                    if (!Directory.Exists(databaseBackupFolder))
                    {
                        Directory.CreateDirectory(databaseBackupFolder);
                    }

                    // Copiar el archivo de la base de datos a la subcarpeta "BaseDatos"
                    string dbFileName = Path.GetFileName(databaseFile);
                    string destDbFilePath = Path.Combine(databaseBackupFolder, dbFileName);
                    File.Copy(databaseFile, destDbFilePath, true); // Sobrescribir si ya existe

                    MessageBox.Show("Copia de seguridad completada exitosamente.");
                }
            }
        }

        private void btnExpor_Click(object sender, EventArgs e)
        {
            Exportar exportar = new Exportar();
            exportar.ShowDialog();
        }
    }
}
