using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlDePacientes.Reports
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int mIdHistorialMedico;
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsReporte.RptHistorialmedicoGetAllById' table. You can move, or remove it, as needed.
            this.RptHistorialmedicoGetAllByIdTableAdapter.Fill(this.dsReporte.RptHistorialmedicoGetAllById, mIdHistorialMedico);

            this.reportViewer1.RefreshReport();
        }
    }
}
