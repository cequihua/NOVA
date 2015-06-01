namespace Mega.Synchronizer.Service
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SynchronizerServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SynchronizerServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // SynchronizerServiceProcessInstaller
            // 
            this.SynchronizerServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.SynchronizerServiceProcessInstaller.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SynchronizerServiceInstaller});
            this.SynchronizerServiceProcessInstaller.Password = null;
            this.SynchronizerServiceProcessInstaller.Username = null;
            // 
            // SynchronizerServiceInstaller
            // 
            this.SynchronizerServiceInstaller.DisplayName = "Mega Sincronizador ";
            this.SynchronizerServiceInstaller.ServiceName = "MegaSynch";
            this.SynchronizerServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SynchronizerServiceProcessInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SynchronizerServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SynchronizerServiceInstaller;
    }
}