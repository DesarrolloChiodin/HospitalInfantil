using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReportesHospitalInfantil
{
    public partial class rptRegistroMedico : Form
    {
        public rptRegistroMedico()
        {
            InitializeComponent();
        }


        public int mIdRegistroMedico;
        private void rptRegistroMedico_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hospitalinfantilDataSet.RptHistorialmedicoGetAllById' table. You can move, or remove it, as needed.
            this.RptHistorialmedicoGetAllByIdTableAdapter.Fill(this.hospitalinfantilDataSet.RptHistorialmedicoGetAllById, mIdRegistroMedico);

            this.reportViewer1.RefreshReport();
        }
    }
}
