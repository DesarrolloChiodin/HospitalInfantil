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
using TipoEnfermedad = ControlDePacientes.Dal.TipoEnfermedad;

namespace ControlDePacientes.Administracion
{
    /// <summary>
    /// Interaction logic for frmDiagnostico.xaml
    /// </summary>
    public partial class frmDiagnostico : Page
    {
        public frmDiagnostico()
        {
            InitializeComponent();
        }
        public int mIdDiagnostico;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            fillDtgDiagnostico();
        }

        private void UsrCtrlBasicButtons_ButtonSaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtTipoEnfermedadName.Text != "")
                {
                    if (mIdDiagnostico == 0)
                    {
                        ControlDePacientes.Entities.TipoEnfermedad lTipoEnfermedadEntity = new ControlDePacientes.Entities.TipoEnfermedad();
                        lTipoEnfermedadEntity.Tipoenfermedadname = txtTipoEnfermedadName.Text;
                        ControlDePacientes.Dal.TipoEnfermedad lTipoEnfermedad = new TipoEnfermedad();
                        mIdDiagnostico = lTipoEnfermedad.AddTipoEnfermedad(lTipoEnfermedadEntity);
                        ControlOperation.alertInformation("Se ha registrado el dignóstico éxitosamente");
                        mIdDiagnostico = 0;
                        txtTipoEnfermedadName.Text = "";
                        fillDtgDiagnostico();
                    }
                    else
                    {
                        ControlDePacientes.Entities.TipoEnfermedad lTipoEnfermedadEntity = new ControlDePacientes.Entities.TipoEnfermedad();
                       lTipoEnfermedadEntity.Tipoenfermedadname = txtTipoEnfermedadName.Text;
                        lTipoEnfermedadEntity.Idtipoenfermedad = mIdDiagnostico;
                       ControlDePacientes.Dal.TipoEnfermedad  lTipoEnfermedad = new TipoEnfermedad();
                        lTipoEnfermedad.UpdateTipoEnfermedad(lTipoEnfermedadEntity);
                        ControlOperation.alertInformation("Se ha actualizado el diagnóstico éxitosamente");
                        txtTipoEnfermedadName.Text = "";
                        fillDtgDiagnostico();
                        mIdDiagnostico = 0;
                    }
                }
                else
                {
                    ControlOperation.alertWarning("Debe colocar el nombre del diagnóstico");
                }
            }
            catch (Exception ex)
            {

                ControlOperation.alertWarning("" + ex);
            }


        }

        private void UsrCtrlBasicButtons_ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            UsrCtrlBasicButtons.ShowHideButtons(true, true, true);
            txtTipoEnfermedadName.Text = "";
            mIdDiagnostico = 0;
        }

        private void UsrCtrlBasicButtons_ButtonDeleteClick(object sender, RoutedEventArgs e)
        {
            if (mIdDiagnostico != 0)
            {
                if (ControlOperation.alertConfirm("Desea eliminar el diagnóstico seleccionada?") == MessageBoxResult.Yes)
                {
                   ControlDePacientes.Dal.TipoEnfermedad  lTipoEnfermedad = new TipoEnfermedad();
                    lTipoEnfermedad.DeleteTipoEnfermedad(mIdDiagnostico);
                    ControlOperation.alertInformation("Diagnóstico eliminado éxitosamente");
                    fillDtgDiagnostico();
                    mIdDiagnostico = 0;
                    txtTipoEnfermedadName.Text = "";
                }


            }
            else
            {
                ControlOperation.alertWarning("Seleccione un dignóstico para eliminar");
            }

        }

        public void fillDtgDiagnostico()
        {
            DataTable lDtOcupacion = new DataTable();
           ControlDePacientes.Dal.TipoEnfermedad  lTipoEnfermedad = new TipoEnfermedad();
            lDtOcupacion = lTipoEnfermedad.TipoEnfermedadGetAll();

            dtgDiagnostico.ItemsSource = lDtOcupacion.DefaultView;

        }

        private void dtgOcupacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            //SELECCIONA LA FILA
            DataRowView lCurrentDataRowView = ((DataRowView)((DataGrid)sender).SelectedItem);

            if (lCurrentDataRowView == null)
                return;

            mIdDiagnostico = (int)lCurrentDataRowView.Row["IdTipoEnfermedad"];
            txtTipoEnfermedadName.Text = (string)lCurrentDataRowView.Row["TipoEnfermedadName"];

        }
    }
}
