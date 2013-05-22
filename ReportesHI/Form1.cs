using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReportesHI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string mRegistroMedico;
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hids.RptHistorialmedicoGetAllById' table. You can move, or remove it, as needed.
            this.rptHistorialmedicoGetAllByIdTableAdapter1.Fill(this.hospitalinfantilDataSet.RptHistorialmedicoGetAllById,mRegistroMedico);

            this.reportViewer1.RefreshReport();
        }
    }
}
