//------------------------------------------------------------------
// <copyright company="Microsoft">
//     Copyright (c) Microsoft.  All rights reserved.
// </copyright>
//------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlDePacientes.Reports.dsReporteTableAdapters;
using Microsoft.Reporting.WinForms;

namespace ReportesInfantil
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
           // this.dsReporte.RptHistorialmedicoGetAllById.FindByIdHistorialMedico(mIdHistorialMedico);// = mIdHistorialMedico)
           
            RptHistorialmedicoGetAllByIdTableAdapter lAllByIdTableAdapter = new RptHistorialmedicoGetAllByIdTableAdapter();
            lAllByIdTableAdapter.GetData(mIdHistorialMedico);//.Fill(this.dsReporte.RptHistorialmedicoGetAllById, mIdHistorialMedico);
            //this.dsReporte.RptHistorialmedicoGetAllById.FindByIdHistorialMedico(mIdHistorialMedico):Select();
            this.reportViewer1.RefreshReport();

            
        }
    }
}