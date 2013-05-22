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
    /// Interaction logic for frmConsultaReceta.xaml
    /// </summary>
    public partial class frmConsultaReceta : Page
    {
        public frmConsultaReceta()
        {
            InitializeComponent();
        }

        public string mRegistroMedico;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void txtIdPaciente_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                try
                {

                    fillData();
                   
                }
                catch (Exception ex)
                {

                  ControlOperation.alertWarning("" + ex);
                }
            }
        }

        private void fillData()
        {
            DataTable lDtReceta = new DataTable();
            ControlDePacientes.Dal.Receta lReceta = new Receta();


            lDtReceta = lReceta.GetRecetaByParameter(txtPaciente.Text == "" ? "-1" : txtPaciente.Text);

            dtgReceta.ItemsSource = lDtReceta.DefaultView;


            txtPaciente.Text = "";

        }

        private void dtgReceta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SELECCIONA LA FILA
            DataRowView lCurrentDataRowView = ((DataRowView)((DataGrid)sender).SelectedItem);

            if (lCurrentDataRowView == null)
                return;

            ControlDePacientes.Entities.receta lReceta = new receta();
            ControlDePacientes.Entities.recetadetalle lRecetadetalle = new recetadetalle();
           

            //lleno la receta
            lReceta.Fechareceta = (DateTime)lCurrentDataRowView.Row["FechaReceta"];
            lReceta.Codpaciente = (Int32)lCurrentDataRowView.Row["CodPaciente"];
            lReceta.Codestado = (Int32)lCurrentDataRowView.Row["CodEstado"];
            lReceta.Idreceta = (Int32)lCurrentDataRowView.Row["IdReceta"];
            mRegistroMedico = (string)lCurrentDataRowView.Row["RegistroMedico"];
            frmRecetaMedica lfrmRecetaMedica = new frmRecetaMedica {mReceta = lReceta, mIsEdition = true, mRegistroMedico = mRegistroMedico};
            this.NavigationService.Navigate(lfrmRecetaMedica);
        }


    }
}
