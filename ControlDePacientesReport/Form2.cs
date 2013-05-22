using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlDePacientesReport
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public int mIdHistorialMedico { get; set; }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: This line of code loads data into the 'hospitalinfantilDataSet.RptHistorialmedicoGetAllById' table. You can move, or remove it, as needed.
                this.RptHistorialmedicoGetAllByIdTableAdapter.Fill(
                    this.hospitalinfantilDataSet.RptHistorialmedicoGetAllById, mIdHistorialMedico);

                this.reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
