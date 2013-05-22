using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ControlDePacientes.Dal;
using ControlDePacientes.Entities;
using ControlDePacientes.Logic;

namespace ControlDePacientes
{
    /// <summary>
    /// Interaction logic for RegistroUpdate.xaml
    /// </summary>
    public partial class RegistroUpdate : Page
    {
        public RegistroUpdate()
        {
            InitializeComponent();
        }

        public DataTable mDtPaciente;
        public int mIdPaciente;

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ControlDePacientes.Dal.Paciente lPaciente = new Dal.Paciente();


            mDtPaciente = new DataTable();
            
            if (txtSearchText.Text == "")
                mDtPaciente = lPaciente.GetPacientePorOldParameters("");

            else if (txtSearchText.Text != "")
                mDtPaciente = lPaciente.GetPacientePorOldParameters(txtSearchText.Text);

            dtgPaciente.ItemsSource = mDtPaciente.DefaultView;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (ControlOperation.alertConfirm("Esta seguro de actualizar el registro médico?") == MessageBoxResult.Yes)
            {

                if (mIdPaciente != 0)
                {
                    ControlDePacientes.Dal.Paciente lPaciente = new Paciente();
                    lPaciente.UpdRegistroMedico(txtRegistroMedico.Text, mIdPaciente);
                    ControlOperation.alertInformation("Paciente actualizado exitosamente");
                    mDtPaciente = lPaciente.GetPacientePorParametros(txtSearchText.Text);

                    dtgPaciente.ItemsSource = mDtPaciente.DefaultView;
                }
                else
                {
                    ControlOperation.alertWarning("Seleccione un Regsitro Médcio para actualizar");
                }
            }
        }

        private void dtgPaciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView lCurrentDataRowView = ((DataRowView)((DataGrid)sender).SelectedItem);

            if (lCurrentDataRowView == null)
                return;

            ControlDePacientes.Entities.paciente lPaciente = new paciente();

            lPaciente = new paciente();
            lPaciente.Idpaciente = (int)lCurrentDataRowView.Row["IdPaciente"];
            mIdPaciente = lPaciente.Idpaciente;
            txtRegistroMedico.Text = (string)lCurrentDataRowView.Row["RegistroMedico"];
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            mIdPaciente = 0;
            txtRegistroMedico.Text = "";
            txtSearchText.Text = "";
            dtgPaciente.ItemsSource = null;

        }
    }
}
