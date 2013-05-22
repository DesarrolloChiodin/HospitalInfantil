namespace ReportesHospitalInfantil
{
    partial class Form1
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
            this.RptHistorialmedicoGetAllByIdBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.hospitalinfantilDataSet = new ReportesHospitalInfantil.hospitalinfantilDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.RptHistorialmedicoGetAllByIdTableAdapter = new ReportesHospitalInfantil.hospitalinfantilDataSetTableAdapters.RptHistorialmedicoGetAllByIdTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.RptHistorialmedicoGetAllByIdBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hospitalinfantilDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // RptHistorialmedicoGetAllByIdBindingSource
            // 
            this.RptHistorialmedicoGetAllByIdBindingSource.DataMember = "RptHistorialmedicoGetAllById";
            this.RptHistorialmedicoGetAllByIdBindingSource.DataSource = this.hospitalinfantilDataSet;
            // 
            // hospitalinfantilDataSet
            // 
            this.hospitalinfantilDataSet.DataSetName = "hospitalinfantilDataSet";
            this.hospitalinfantilDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DtsHospitalInfantil";
            reportDataSource1.Value = this.RptHistorialmedicoGetAllByIdBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ReportesHospitalInfantil.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(682, 386);
            this.reportViewer1.TabIndex = 0;
            // 
            // RptHistorialmedicoGetAllByIdTableAdapter
            // 
            this.RptHistorialmedicoGetAllByIdTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 386);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RptHistorialmedicoGetAllByIdBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hospitalinfantilDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource RptHistorialmedicoGetAllByIdBindingSource;
        private hospitalinfantilDataSet hospitalinfantilDataSet;
        private hospitalinfantilDataSetTableAdapters.RptHistorialmedicoGetAllByIdTableAdapter RptHistorialmedicoGetAllByIdTableAdapter;
    }
}

