namespace ControlDePacientesReport
{
    partial class Form2
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.rptHistorialmedicoGetAllByIdBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.hospitalinfantilDataSet = new ControlDePacientesReport.hospitalinfantilDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.RptHistorialmedicoGetAllByIdBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.RptHistorialmedicoGetAllByIdTableAdapter = new ControlDePacientesReport.hospitalinfantilDataSetTableAdapters.RptHistorialmedicoGetAllByIdTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.rptHistorialmedicoGetAllByIdBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hospitalinfantilDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RptHistorialmedicoGetAllByIdBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // rptHistorialmedicoGetAllByIdBindingSource1
            // 
            this.rptHistorialmedicoGetAllByIdBindingSource1.DataMember = "RptHistorialmedicoGetAllById";
            this.rptHistorialmedicoGetAllByIdBindingSource1.DataSource = this.hospitalinfantilDataSet;
            // 
            // hospitalinfantilDataSet
            // 
            this.hospitalinfantilDataSet.DataSetName = "hospitalinfantilDataSet";
            this.hospitalinfantilDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.rptHistorialmedicoGetAllByIdBindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ControlDePacientesReport.Reportes.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(669, 398);
            this.reportViewer1.TabIndex = 0;
            // 
            // RptHistorialmedicoGetAllByIdBindingSource
            // 
            this.RptHistorialmedicoGetAllByIdBindingSource.DataMember = "RptHistorialmedicoGetAllById";
            this.RptHistorialmedicoGetAllByIdBindingSource.DataSource = this.hospitalinfantilDataSet;
            // 
            // RptHistorialmedicoGetAllByIdTableAdapter
            // 
            this.RptHistorialmedicoGetAllByIdTableAdapter.ClearBeforeFill = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 398);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rptHistorialmedicoGetAllByIdBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hospitalinfantilDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RptHistorialmedicoGetAllByIdBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource RptHistorialmedicoGetAllByIdBindingSource;
        private hospitalinfantilDataSet hospitalinfantilDataSet;
        private hospitalinfantilDataSetTableAdapters.RptHistorialmedicoGetAllByIdTableAdapter RptHistorialmedicoGetAllByIdTableAdapter;
        private System.Windows.Forms.BindingSource rptHistorialmedicoGetAllByIdBindingSource1;
    }
}