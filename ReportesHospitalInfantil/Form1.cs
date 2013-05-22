using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ReportesHospitalInfantil
{
    public partial class Form1 : Form
    {
        public int mIdHistorialMedico;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hospitalinfantilDataSet.RptHistorialmedicoGetAllById' table. You can move, or remove it, as needed.
            this.RptHistorialmedicoGetAllByIdTableAdapter.Fill(this.hospitalinfantilDataSet.RptHistorialmedicoGetAllById, mIdHistorialMedico);
            this.reportViewer1.RefreshReport();
        }

       
    }
}