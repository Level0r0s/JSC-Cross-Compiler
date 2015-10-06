namespace MobileAuthenticateExperiment
{
    partial class UserControl1
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.identitySheet1BindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.telnrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isikukoodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.riikDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kirjeldusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.näidisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.identitySheet1BindingSourceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.telnrDataGridViewTextBoxColumn,
            this.isikukoodDataGridViewTextBoxColumn,
            this.riikDataGridViewTextBoxColumn,
            this.kirjeldusDataGridViewTextBoxColumn,
            this.näidisDataGridViewTextBoxColumn,
            this.keyDataGridViewTextBoxColumn,
            this.tagDataGridViewTextBoxColumn,
            this.timestampDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.identitySheet1BindingSourceBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(824, 333);
            this.dataGridView1.TabIndex = 0;
            // 
            // identitySheet1BindingSourceBindingSource
            // 
            this.identitySheet1BindingSourceBindingSource.DataSource = typeof(MobileAuthenticateExperiment.Data.identitySheet1BindingSource);
            this.identitySheet1BindingSourceBindingSource.Position = 0;
            // 
            // telnrDataGridViewTextBoxColumn
            // 
            this.telnrDataGridViewTextBoxColumn.DataPropertyName = "Tel_nr";
            this.telnrDataGridViewTextBoxColumn.HeaderText = "Tel_nr";
            this.telnrDataGridViewTextBoxColumn.Name = "telnrDataGridViewTextBoxColumn";
            this.telnrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isikukoodDataGridViewTextBoxColumn
            // 
            this.isikukoodDataGridViewTextBoxColumn.DataPropertyName = "Isikukood";
            this.isikukoodDataGridViewTextBoxColumn.HeaderText = "Isikukood";
            this.isikukoodDataGridViewTextBoxColumn.Name = "isikukoodDataGridViewTextBoxColumn";
            this.isikukoodDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // riikDataGridViewTextBoxColumn
            // 
            this.riikDataGridViewTextBoxColumn.DataPropertyName = "Riik";
            this.riikDataGridViewTextBoxColumn.HeaderText = "Riik";
            this.riikDataGridViewTextBoxColumn.Name = "riikDataGridViewTextBoxColumn";
            this.riikDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // kirjeldusDataGridViewTextBoxColumn
            // 
            this.kirjeldusDataGridViewTextBoxColumn.DataPropertyName = "Kirjeldus";
            this.kirjeldusDataGridViewTextBoxColumn.HeaderText = "Kirjeldus";
            this.kirjeldusDataGridViewTextBoxColumn.Name = "kirjeldusDataGridViewTextBoxColumn";
            this.kirjeldusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // näidisDataGridViewTextBoxColumn
            // 
            this.näidisDataGridViewTextBoxColumn.DataPropertyName = "Näidis";
            this.näidisDataGridViewTextBoxColumn.HeaderText = "Näidis";
            this.näidisDataGridViewTextBoxColumn.Name = "näidisDataGridViewTextBoxColumn";
            this.näidisDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // keyDataGridViewTextBoxColumn
            // 
            this.keyDataGridViewTextBoxColumn.DataPropertyName = "Key";
            this.keyDataGridViewTextBoxColumn.HeaderText = "Key";
            this.keyDataGridViewTextBoxColumn.Name = "keyDataGridViewTextBoxColumn";
            this.keyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tagDataGridViewTextBoxColumn
            // 
            this.tagDataGridViewTextBoxColumn.DataPropertyName = "Tag";
            this.tagDataGridViewTextBoxColumn.HeaderText = "Tag";
            this.tagDataGridViewTextBoxColumn.Name = "tagDataGridViewTextBoxColumn";
            this.tagDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // timestampDataGridViewTextBoxColumn
            // 
            this.timestampDataGridViewTextBoxColumn.DataPropertyName = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.HeaderText = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.Name = "timestampDataGridViewTextBoxColumn";
            this.timestampDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(824, 333);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.identitySheet1BindingSourceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.BindingSource identitySheet1BindingSourceBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn telnrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isikukoodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn riikDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kirjeldusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn näidisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tagDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn;
    }
}
