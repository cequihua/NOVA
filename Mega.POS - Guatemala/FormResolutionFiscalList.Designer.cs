namespace Mega.POS
{
    partial class FormResolutionFiscalList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormResolutionFiscalList));
            this.datGridResolution = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtResolution = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_accion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_auto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resolution = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.range = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.datGridResolution)).BeginInit();
            this.SuspendLayout();
            // 
            // datGridResolution
            // 
            this.datGridResolution.AllowUserToAddRows = false;
            this.datGridResolution.AllowUserToDeleteRows = false;
            this.datGridResolution.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Numero,
            this.fecha_accion,
            this.fecha_auto,
            this.resolution,
            this.range,
            this.active});
            this.datGridResolution.Location = new System.Drawing.Point(5, 53);
            this.datGridResolution.Name = "datGridResolution";
            this.datGridResolution.ReadOnly = true;
            this.datGridResolution.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datGridResolution.Size = new System.Drawing.Size(610, 190);
            this.datGridResolution.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(316, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Resolucion:";
            // 
            // txtResolution
            // 
            this.txtResolution.Location = new System.Drawing.Point(385, 15);
            this.txtResolution.Name = "txtResolution";
            this.txtResolution.Size = new System.Drawing.Size(152, 20);
            this.txtResolution.TabIndex = 2;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(545, 6);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(68, 37);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(5, 254);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 40);
            this.btnAgregar.TabIndex = 4;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(539, 254);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 40);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "Cerrar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(92, 254);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(75, 40);
            this.btnEditar.TabIndex = 6;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // Numero
            // 
            this.Numero.DataPropertyName = "IdResolution";
            this.Numero.HeaderText = "No.";
            this.Numero.Name = "Numero";
            this.Numero.ReadOnly = true;
            this.Numero.Width = 20;
            // 
            // fecha_accion
            // 
            this.fecha_accion.DataPropertyName = "FechaAccion";
            this.fecha_accion.HeaderText = "Fecha Accion";
            this.fecha_accion.Name = "fecha_accion";
            this.fecha_accion.ReadOnly = true;
            // 
            // fecha_auto
            // 
            this.fecha_auto.DataPropertyName = "FechaAuto";
            this.fecha_auto.HeaderText = "Fecha Autorizacion";
            this.fecha_auto.Name = "fecha_auto";
            this.fecha_auto.ReadOnly = true;
            // 
            // resolution
            // 
            this.resolution.DataPropertyName = "Resolution";
            this.resolution.HeaderText = "Resolucion";
            this.resolution.Name = "resolution";
            this.resolution.ReadOnly = true;
            this.resolution.Width = 130;
            // 
            // range
            // 
            this.range.DataPropertyName = "Rango";
            this.range.HeaderText = "Rango";
            this.range.Name = "range";
            this.range.ReadOnly = true;
            this.range.Width = 150;
            // 
            // active
            // 
            this.active.DataPropertyName = "Active";
            this.active.HeaderText = "Activo";
            this.active.Name = "active";
            this.active.ReadOnly = true;
            this.active.Width = 50;
            // 
            // FormResolutionFiscalList
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 300);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtResolution);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.datGridResolution);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormResolutionFiscalList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resoluciones Fiscales";
            this.Load += new System.EventHandler(this.FormResolutionFiscalList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datGridResolution)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView datGridResolution;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtResolution;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_accion;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_auto;
        private System.Windows.Forms.DataGridViewTextBoxColumn resolution;
        private System.Windows.Forms.DataGridViewTextBoxColumn range;
        private System.Windows.Forms.DataGridViewCheckBoxColumn active;
    }
}