namespace AsoDocs.Vista
{
    partial class Exportar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbxListAH = new System.Windows.Forms.CheckBox();
            this.cbxListaAnoH = new System.Windows.Forms.CheckBox();
            this.cbxRegistro = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbnUno = new System.Windows.Forms.RadioButton();
            this.rbnSeparados = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxListAH
            // 
            this.cbxListAH.AutoSize = true;
            this.cbxListAH.Location = new System.Drawing.Point(26, 89);
            this.cbxListAH.Name = "cbxListAH";
            this.cbxListAH.Size = new System.Drawing.Size(178, 17);
            this.cbxListAH.TabIndex = 0;
            this.cbxListAH.Text = "Lista de asociados con Familires";
            this.cbxListAH.UseVisualStyleBackColor = true;
            // 
            // cbxListaAnoH
            // 
            this.cbxListaAnoH.AutoSize = true;
            this.cbxListaAnoH.Location = new System.Drawing.Point(26, 47);
            this.cbxListaAnoH.Name = "cbxListaAnoH";
            this.cbxListaAnoH.Size = new System.Drawing.Size(115, 17);
            this.cbxListaAnoH.TabIndex = 1;
            this.cbxListaAnoH.Text = "Lista de Asociados";
            this.cbxListaAnoH.UseVisualStyleBackColor = true;
            this.cbxListaAnoH.CheckedChanged += new System.EventHandler(this.cbxListaAnoH_CheckedChanged);
            // 
            // cbxRegistro
            // 
            this.cbxRegistro.AutoSize = true;
            this.cbxRegistro.Location = new System.Drawing.Point(26, 130);
            this.cbxRegistro.Name = "cbxRegistro";
            this.cbxRegistro.Size = new System.Drawing.Size(111, 17);
            this.cbxRegistro.TabIndex = 2;
            this.cbxRegistro.Text = "Lista de Retirados";
            this.cbxRegistro.UseVisualStyleBackColor = true;
            this.cbxRegistro.CheckedChanged += new System.EventHandler(this.cbxRegistro_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbnUno);
            this.groupBox1.Controls.Add(this.rbnSeparados);
            this.groupBox1.Location = new System.Drawing.Point(211, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(157, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Exportar en ...";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // rbnUno
            // 
            this.rbnUno.AutoSize = true;
            this.rbnUno.Location = new System.Drawing.Point(6, 61);
            this.rbnUno.Name = "rbnUno";
            this.rbnUno.Size = new System.Drawing.Size(78, 17);
            this.rbnUno.TabIndex = 1;
            this.rbnUno.Text = "Un Archivo";
            this.rbnUno.UseVisualStyleBackColor = true;
            this.rbnUno.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // rbnSeparados
            // 
            this.rbnSeparados.AutoSize = true;
            this.rbnSeparados.Checked = true;
            this.rbnSeparados.Location = new System.Drawing.Point(6, 29);
            this.rbnSeparados.Name = "rbnSeparados";
            this.rbnSeparados.Size = new System.Drawing.Size(120, 17);
            this.rbnSeparados.TabIndex = 0;
            this.rbnSeparados.TabStop = true;
            this.rbnSeparados.Text = "Archivos Separados";
            this.rbnSeparados.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(127, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "Exportar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Exportar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 233);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbxRegistro);
            this.Controls.Add(this.cbxListaAnoH);
            this.Controls.Add(this.cbxListAH);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Exportar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportar";
            this.Load += new System.EventHandler(this.Exportar_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbxListAH;
        private System.Windows.Forms.CheckBox cbxListaAnoH;
        private System.Windows.Forms.CheckBox cbxRegistro;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbnUno;
        private System.Windows.Forms.RadioButton rbnSeparados;
        private System.Windows.Forms.Button button1;
    }
}