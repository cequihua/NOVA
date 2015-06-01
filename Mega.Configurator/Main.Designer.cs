namespace Mega.Configurator
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabConfig = new System.Windows.Forms.TabControl();
            this.ConnectionStringTabPage = new System.Windows.Forms.TabPage();
            this.ConnectionStringTextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ChangeConnectionStringbutton = new System.Windows.Forms.Button();
            this.SettingsTabPage = new System.Windows.Forms.TabPage();
            this.SettingsDataGridView = new System.Windows.Forms.DataGridView();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabFacturacion = new System.Windows.Forms.TabPage();
            this.grpSucursal = new System.Windows.Forms.GroupBox();
            this.txtPais = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCP = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtMunicipio = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtColinia = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNumExt = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNumInt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCalle = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRfcGral = new System.Windows.Forms.TextBox();
            this.rfcGral = new System.Windows.Forms.Label();
            this.txtSucursal = new System.Windows.Forms.TextBox();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.txtCompFiscal = new System.Windows.Forms.TextBox();
            this.txtRfcCompañia = new System.Windows.Forms.TextBox();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ShowCashierButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.tabConfig.SuspendLayout();
            this.ConnectionStringTabPage.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SettingsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsDataGridView)).BeginInit();
            this.tabFacturacion.SuspendLayout();
            this.grpSucursal.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabConfig
            // 
            this.tabConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabConfig.Controls.Add(this.ConnectionStringTabPage);
            this.tabConfig.Controls.Add(this.SettingsTabPage);
            this.tabConfig.Controls.Add(this.tabFacturacion);
            this.tabConfig.Location = new System.Drawing.Point(0, 39);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.SelectedIndex = 0;
            this.tabConfig.Size = new System.Drawing.Size(623, 376);
            this.tabConfig.TabIndex = 1;
            this.tabConfig.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabConfig_Selected);
            // 
            // ConnectionStringTabPage
            // 
            this.ConnectionStringTabPage.Controls.Add(this.ConnectionStringTextBox);
            this.ConnectionStringTabPage.Controls.Add(this.panel2);
            this.ConnectionStringTabPage.Location = new System.Drawing.Point(4, 22);
            this.ConnectionStringTabPage.Name = "ConnectionStringTabPage";
            this.ConnectionStringTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ConnectionStringTabPage.Size = new System.Drawing.Size(615, 350);
            this.ConnectionStringTabPage.TabIndex = 0;
            this.ConnectionStringTabPage.Text = "Conexión a la Base de Datos";
            this.ConnectionStringTabPage.UseVisualStyleBackColor = true;
            // 
            // ConnectionStringTextBox
            // 
            this.ConnectionStringTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.ConnectionStringTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectionStringTextBox.Location = new System.Drawing.Point(3, 3);
            this.ConnectionStringTextBox.Multiline = true;
            this.ConnectionStringTextBox.Name = "ConnectionStringTextBox";
            this.ConnectionStringTextBox.ReadOnly = true;
            this.ConnectionStringTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConnectionStringTextBox.Size = new System.Drawing.Size(609, 296);
            this.ConnectionStringTextBox.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ChangeConnectionStringbutton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 299);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(609, 48);
            this.panel2.TabIndex = 4;
            // 
            // ChangeConnectionStringbutton
            // 
            this.ChangeConnectionStringbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChangeConnectionStringbutton.Location = new System.Drawing.Point(475, 13);
            this.ChangeConnectionStringbutton.Name = "ChangeConnectionStringbutton";
            this.ChangeConnectionStringbutton.Size = new System.Drawing.Size(116, 23);
            this.ChangeConnectionStringbutton.TabIndex = 2;
            this.ChangeConnectionStringbutton.Text = "Cambiar Conexión";
            this.ChangeConnectionStringbutton.UseVisualStyleBackColor = true;
            this.ChangeConnectionStringbutton.Click += new System.EventHandler(this.ChangeConnectionStringbutton_Click);
            // 
            // SettingsTabPage
            // 
            this.SettingsTabPage.Controls.Add(this.SettingsDataGridView);
            this.SettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.SettingsTabPage.Name = "SettingsTabPage";
            this.SettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTabPage.Size = new System.Drawing.Size(615, 350);
            this.SettingsTabPage.TabIndex = 1;
            this.SettingsTabPage.Text = "Configuraciones";
            this.SettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // SettingsDataGridView
            // 
            this.SettingsDataGridView.AllowUserToAddRows = false;
            this.SettingsDataGridView.AllowUserToDeleteRows = false;
            this.SettingsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SettingsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SettingsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Key,
            this.Value});
            this.SettingsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsDataGridView.Location = new System.Drawing.Point(3, 3);
            this.SettingsDataGridView.MultiSelect = false;
            this.SettingsDataGridView.Name = "SettingsDataGridView";
            this.SettingsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SettingsDataGridView.ShowEditingIcon = false;
            this.SettingsDataGridView.Size = new System.Drawing.Size(609, 344);
            this.SettingsDataGridView.TabIndex = 0;
            this.SettingsDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.SettingsDataGridView_CellEndEdit_1);
            this.SettingsDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.SettingsDataGridView_CellValidating);
            // 
            // Key
            // 
            this.Key.DataPropertyName = "Key";
            this.Key.HeaderText = "Configuración";
            this.Key.Name = "Key";
            this.Key.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            this.Value.HeaderText = "Valor";
            this.Value.Name = "Value";
            // 
            // tabFacturacion
            // 
            this.tabFacturacion.Controls.Add(this.grpSucursal);
            this.tabFacturacion.Controls.Add(this.txtEmail);
            this.tabFacturacion.Controls.Add(this.label8);
            this.tabFacturacion.Controls.Add(this.txtRfcGral);
            this.tabFacturacion.Controls.Add(this.rfcGral);
            this.tabFacturacion.Controls.Add(this.txtSucursal);
            this.tabFacturacion.Controls.Add(this.txtFolio);
            this.tabFacturacion.Controls.Add(this.txtSerie);
            this.tabFacturacion.Controls.Add(this.txtCompFiscal);
            this.tabFacturacion.Controls.Add(this.txtRfcCompañia);
            this.tabFacturacion.Controls.Add(this.txtToken);
            this.tabFacturacion.Controls.Add(this.label6);
            this.tabFacturacion.Controls.Add(this.label5);
            this.tabFacturacion.Controls.Add(this.label4);
            this.tabFacturacion.Controls.Add(this.label3);
            this.tabFacturacion.Controls.Add(this.label2);
            this.tabFacturacion.Controls.Add(this.label1);
            this.tabFacturacion.Location = new System.Drawing.Point(4, 22);
            this.tabFacturacion.Name = "tabFacturacion";
            this.tabFacturacion.Padding = new System.Windows.Forms.Padding(3);
            this.tabFacturacion.Size = new System.Drawing.Size(615, 350);
            this.tabFacturacion.TabIndex = 2;
            this.tabFacturacion.Text = "Facturacion";
            this.tabFacturacion.UseVisualStyleBackColor = true;
            // 
            // grpSucursal
            // 
            this.grpSucursal.Controls.Add(this.txtPais);
            this.grpSucursal.Controls.Add(this.label15);
            this.grpSucursal.Controls.Add(this.txtCP);
            this.grpSucursal.Controls.Add(this.label13);
            this.grpSucursal.Controls.Add(this.txtEstado);
            this.grpSucursal.Controls.Add(this.label14);
            this.grpSucursal.Controls.Add(this.txtMunicipio);
            this.grpSucursal.Controls.Add(this.label12);
            this.grpSucursal.Controls.Add(this.txtColinia);
            this.grpSucursal.Controls.Add(this.label11);
            this.grpSucursal.Controls.Add(this.txtNumExt);
            this.grpSucursal.Controls.Add(this.label10);
            this.grpSucursal.Controls.Add(this.txtNumInt);
            this.grpSucursal.Controls.Add(this.label9);
            this.grpSucursal.Controls.Add(this.txtCalle);
            this.grpSucursal.Controls.Add(this.label7);
            this.grpSucursal.Location = new System.Drawing.Point(7, 132);
            this.grpSucursal.Name = "grpSucursal";
            this.grpSucursal.Size = new System.Drawing.Size(599, 210);
            this.grpSucursal.TabIndex = 16;
            this.grpSucursal.TabStop = false;
            this.grpSucursal.Text = "Domicilio Sucursal";
            // 
            // txtPais
            // 
            this.txtPais.Location = new System.Drawing.Point(387, 22);
            this.txtPais.Name = "txtPais";
            this.txtPais.Size = new System.Drawing.Size(188, 20);
            this.txtPais.TabIndex = 13;
            this.txtPais.Text = "MEXICO";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(351, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(30, 13);
            this.label15.TabIndex = 28;
            this.label15.Text = "Pais:";
            // 
            // txtCP
            // 
            this.txtCP.Location = new System.Drawing.Point(97, 143);
            this.txtCP.Name = "txtCP";
            this.txtCP.Size = new System.Drawing.Size(188, 20);
            this.txtCP.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(67, 143);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "CP:";
            // 
            // txtEstado
            // 
            this.txtEstado.Location = new System.Drawing.Point(389, 79);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Size = new System.Drawing.Size(188, 20);
            this.txtEstado.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(338, 82);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 24;
            this.label14.Text = "Estado:";
            // 
            // txtMunicipio
            // 
            this.txtMunicipio.Location = new System.Drawing.Point(389, 49);
            this.txtMunicipio.Name = "txtMunicipio";
            this.txtMunicipio.Size = new System.Drawing.Size(188, 20);
            this.txtMunicipio.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(328, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Municipio:";
            // 
            // txtColinia
            // 
            this.txtColinia.Location = new System.Drawing.Point(97, 113);
            this.txtColinia.Name = "txtColinia";
            this.txtColinia.Size = new System.Drawing.Size(188, 20);
            this.txtColinia.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(46, 116);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Colonia:";
            // 
            // txtNumExt
            // 
            this.txtNumExt.Location = new System.Drawing.Point(97, 83);
            this.txtNumExt.Name = "txtNumExt";
            this.txtNumExt.Size = new System.Drawing.Size(188, 20);
            this.txtNumExt.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(26, 86);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Numero Ext:";
            // 
            // txtNumInt
            // 
            this.txtNumInt.Location = new System.Drawing.Point(97, 51);
            this.txtNumInt.Name = "txtNumInt";
            this.txtNumInt.Size = new System.Drawing.Size(188, 20);
            this.txtNumInt.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Numero Int:";
            // 
            // txtCalle
            // 
            this.txtCalle.Location = new System.Drawing.Point(97, 22);
            this.txtCalle.Name = "txtCalle";
            this.txtCalle.Size = new System.Drawing.Size(188, 20);
            this.txtCalle.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Calle:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(396, 93);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(188, 20);
            this.txtEmail.TabIndex = 7;
            this.txtEmail.Text = "cnvexhimoda@megagdl.com.mx";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(352, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "E-mail:";
            // 
            // txtRfcGral
            // 
            this.txtRfcGral.Location = new System.Drawing.Point(114, 93);
            this.txtRfcGral.Name = "txtRfcGral";
            this.txtRfcGral.Size = new System.Drawing.Size(188, 20);
            this.txtRfcGral.TabIndex = 3;
            this.txtRfcGral.Text = "XAXX010101000";
            // 
            // rfcGral
            // 
            this.rfcGral.AutoSize = true;
            this.rfcGral.Location = new System.Drawing.Point(37, 96);
            this.rfcGral.Name = "rfcGral";
            this.rfcGral.Size = new System.Drawing.Size(71, 13);
            this.rfcGral.TabIndex = 12;
            this.rfcGral.Text = "RFC General:";
            // 
            // txtSucursal
            // 
            this.txtSucursal.Location = new System.Drawing.Point(396, 61);
            this.txtSucursal.Name = "txtSucursal";
            this.txtSucursal.Size = new System.Drawing.Size(188, 20);
            this.txtSucursal.TabIndex = 6;
            // 
            // txtFolio
            // 
            this.txtFolio.Location = new System.Drawing.Point(396, 32);
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.Size = new System.Drawing.Size(188, 20);
            this.txtFolio.TabIndex = 5;
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(396, 4);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(188, 20);
            this.txtSerie.TabIndex = 4;
            // 
            // txtCompFiscal
            // 
            this.txtCompFiscal.Location = new System.Drawing.Point(114, 65);
            this.txtCompFiscal.Name = "txtCompFiscal";
            this.txtCompFiscal.Size = new System.Drawing.Size(188, 20);
            this.txtCompFiscal.TabIndex = 2;
            // 
            // txtRfcCompañia
            // 
            this.txtRfcCompañia.Location = new System.Drawing.Point(114, 33);
            this.txtRfcCompañia.Name = "txtRfcCompañia";
            this.txtRfcCompañia.Size = new System.Drawing.Size(188, 20);
            this.txtRfcCompañia.TabIndex = 1;
            this.txtRfcCompañia.Text = "HDI120719S23";
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(114, 4);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(188, 20);
            this.txtToken.TabIndex = 0;
            this.txtToken.Text = "9fc9d7e1b444b0e8f15f463949f2bef3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(339, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Sucursal:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(358, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Folio:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(356, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Serie:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Comprobante Fiscal:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "RFC Compañia:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Token XSA:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.ShowCashierButton);
            this.panel1.Controls.Add(this.SaveButton);
            this.panel1.Controls.Add(this.LoadButton);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(623, 36);
            this.panel1.TabIndex = 2;
            // 
            // ShowCashierButton
            // 
            this.ShowCashierButton.Enabled = false;
            this.ShowCashierButton.Location = new System.Drawing.Point(493, 8);
            this.ShowCashierButton.Name = "ShowCashierButton";
            this.ShowCashierButton.Size = new System.Drawing.Size(105, 23);
            this.ShowCashierButton.TabIndex = 2;
            this.ShowCashierButton.Text = "Mostrar Cajas";
            this.ShowCashierButton.UseVisualStyleBackColor = true;
            this.ShowCashierButton.Click += new System.EventHandler(this.ShowCashierButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(184, 8);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Guardar";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(16, 8);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(162, 23);
            this.LoadButton.TabIndex = 0;
            this.LoadButton.Text = "Seleccionar Configuración";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 415);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabConfig);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(639, 453);
            this.Name = "Main";
            this.Text = "Mega Configuraciones";
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabConfig.ResumeLayout(false);
            this.ConnectionStringTabPage.ResumeLayout(false);
            this.ConnectionStringTabPage.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.SettingsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SettingsDataGridView)).EndInit();
            this.tabFacturacion.ResumeLayout(false);
            this.tabFacturacion.PerformLayout();
            this.grpSucursal.ResumeLayout(false);
            this.grpSucursal.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabConfig;
        private System.Windows.Forms.TabPage ConnectionStringTabPage;
        private System.Windows.Forms.TabPage SettingsTabPage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ChangeConnectionStringbutton;
        private System.Windows.Forms.TextBox ConnectionStringTextBox;
        private System.Windows.Forms.DataGridView SettingsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button ShowCashierButton;
        private System.Windows.Forms.TabPage tabFacturacion;
        private System.Windows.Forms.TextBox txtSucursal;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.TextBox txtCompFiscal;
        private System.Windows.Forms.TextBox txtRfcCompañia;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRfcGral;
        private System.Windows.Forms.Label rfcGral;
        private System.Windows.Forms.GroupBox grpSucursal;
        private System.Windows.Forms.TextBox txtMunicipio;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtColinia;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNumExt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNumInt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCalle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtCP;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPais;
        private System.Windows.Forms.Label label15;
    }
}

