using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsUDPJoinGroup
{
    public partial class ApplicationControl : UserControl
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            // Note: This jsc project does not support unmanaged resources.
            //base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ApplicationControl
            // 
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(468, 295);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            this.ResumeLayout(false);

        }
    }
}
