namespace Mega.POS.Client
{
    partial class FindClient
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
            this.components = new System.ComponentModel.Container();
            this.FindButton = new System.Windows.Forms.Button();
            this.FindByDimTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FindByLastNameTextBox = new System.Windows.Forms.TextBox();
            this.ResultInfoLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.FindByNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Names = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectDimButton = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.GridPager = new Mega.POS.Client.Pager();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            this.SuspendLayout();
            // 
            // FindButton
            // 
            this.FindButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindButton.Location = new System.Drawing.Point(413, 13);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(95, 73);
            this.FindButton.TabIndex = 3;
            this.FindButton.Text = "Buscar";
            this.FindButton.UseVisualStyleBackColor = true;
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // FindByDimTextBox
            // 
            this.FindByDimTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindByDimTextBox.Location = new System.Drawing.Point(128, 13);
            this.FindByDimTextBox.Name = "FindByDimTextBox";
            this.FindByDimTextBox.Size = new System.Drawing.Size(241, 22);
            this.FindByDimTextBox.TabIndex = 0;
            this.FindByDimTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FindTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Buscar en No. Dim:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FindByLastNameTextBox);
            this.groupBox1.Controls.Add(this.ResultInfoLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.FindByNameTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.FindByDimTextBox);
            this.groupBox1.Controls.Add(this.FindButton);
            this.groupBox1.Location = new System.Drawing.Point(5, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // FindByLastNameTextBox
            // 
            this.FindByLastNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindByLastNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindByLastNameTextBox.Location = new System.Drawing.Point(128, 64);
            this.FindByLastNameTextBox.Name = "FindByLastNameTextBox";
            this.FindByLastNameTextBox.Size = new System.Drawing.Size(241, 22);
            this.FindByLastNameTextBox.TabIndex = 2;
            this.FindByLastNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FindTextBox_KeyPress);
            // 
            // ResultInfoLabel
            // 
            this.ResultInfoLabel.AutoSize = true;
            this.ResultInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultInfoLabel.Location = new System.Drawing.Point(7, 108);
            this.ResultInfoLabel.Name = "ResultInfoLabel";
            this.ResultInfoLabel.Size = new System.Drawing.Size(78, 13);
            this.ResultInfoLabel.TabIndex = 3;
            this.ResultInfoLabel.Text = "Resultados: ";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "y en Apellido Paterno: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FindByNameTextBox
            // 
            this.FindByNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FindByNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindByNameTextBox.Location = new System.Drawing.Point(128, 39);
            this.FindByNameTextBox.Name = "FindByNameTextBox";
            this.FindByNameTextBox.Size = new System.Drawing.Size(241, 22);
            this.FindByNameTextBox.TabIndex = 1;
            this.FindByNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FindTextBox_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "y en Nombre: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(418, 323);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(100, 51);
            this.CloseButton.TabIndex = 6;
            this.CloseButton.Text = "&Cerrar";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Names,
            this.Status});
            this.dataGridView1.Location = new System.Drawing.Point(5, 140);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(514, 144);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "Id";
            this.ID.HeaderText = "DIM";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Names
            // 
            this.Names.DataPropertyName = "FullName";
            this.Names.HeaderText = "Nombres y Apellidos";
            this.Names.Name = "Names";
            this.Names.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Estatus";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // SelectDimButton
            // 
            this.SelectDimButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectDimButton.ForeColor = System.Drawing.Color.Green;
            this.SelectDimButton.Location = new System.Drawing.Point(4, 323);
            this.SelectDimButton.Name = "SelectDimButton";
            this.SelectDimButton.Size = new System.Drawing.Size(154, 51);
            this.SelectDimButton.TabIndex = 5;
            this.SelectDimButton.Text = "Seleccionar DIM y Cerrar (F12)";
            this.SelectDimButton.UseVisualStyleBackColor = true;
            this.SelectDimButton.Click += new System.EventHandler(this.SelectDimButton_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // GridPager
            // 
            this.GridPager.AutoSize = true;
            this.GridPager.Data = null;
            this.GridPager.ItemsCount = ((long)(0));
            this.GridPager.Location = new System.Drawing.Point(52, 288);
            this.GridPager.Name = "GridPager";
            this.GridPager.SDataGridView = null;
            this.GridPager.Size = new System.Drawing.Size(467, 30);
            this.GridPager.TabIndex = 7;
            // 
            // FindClient
            // 
            this.AcceptButton = this.FindButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(522, 378);
            this.ControlBox = false;
            this.Controls.Add(this.GridPager);
            this.Controls.Add(this.SelectDimButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FindClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Buscar Dim/Patrocinador";
            this.Load += new System.EventHandler(this.FindClient_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FindClient_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.TextBox FindByDimTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button SelectDimButton;
        private System.Windows.Forms.Label ResultInfoLabel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Names;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.TextBox FindByLastNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FindByNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private Pager GridPager;
    }
}