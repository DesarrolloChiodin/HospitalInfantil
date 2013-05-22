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
    /// Interaction logic for frmOcupacion.xaml
    /// </summary>
    public partial class frmOcupacion : Page
    {
        public frmOcupacion()
        {
            InitializeComponent();
        }

        public int mIdOcupacion;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            fillDtgOcuapcion();
        }

        private void UsrCtrlBasicButtons_ButtonSaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtOcupacionName.Text != "")
                {
                    if (mIdOcupacion == 0)
                    {
                        ControlDePacientes.Entities.ocupacion lOcupacionEntity = new ocupacion();
                        lOcupacionEntity.Ocupacionname = txtOcupacionName.Text;
                        ControlDePacientes.Dal.Ocupacion lOcupacion = new Ocupacion();
                        mIdOcupacion = lOcupacion.AddOcupacion(lOcupacionEntity);
                        ControlOperation.alertInformation("Se ha registrado la ocupación éxitosamente");
                        mIdOcupacion = 0;
                        txtOcupacionName.Text = "";
                        fillDtgOcuapcion();
                    }
                    else
                    {
                        ControlDePacientes.Entities.ocupacion lOcupacionEntity = new ocupacion();
                        lOcupacionEntity.Ocupacionname = txtOcupacionName.Text;
                        lOcupacionEntity.Idocupacion = mIdOcupacion;
                        ControlDePacientes.Dal.Ocupacion lOcupacion = new Ocupacion();
                        lOcupacion.UpdateOcupacion(lOcupacionEntity);
                        ControlOperation.alertInformation("Se ha actualizado la ocupación éxitosamente");
                        txtOcupacionName.Text = "";
                        fillDtgOcuapcion();
                        mIdOcupacion = 0;
                    }
                }
                else
                {
                    ControlOperation.alertWarning("Debe colocar el nombre de la ocupación");
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
            txtOcupacionName.Text = "";
            mIdOcupacion = 0;
        }

        private void UsrCtrlBasicButtons_ButtonDeleteClick(object sender, RoutedEventArgs e)
        {
            if (mIdOcupacion != 0)
            {
                if (ControlOperation.alertConfirm("Desea eliminar la ocupación seleccionada?") == MessageBoxResult.Yes)
                {
                    ControlDePacientes.Dal.Ocupacion lOcupacion = new Ocupacion();
                    lOcupacion.DeleteOcupacion(mIdOcupacion);
                    ControlOperation.alertInformation("Ocupación eliminada éxitosamente");
                    fillDtgOcuapcion();
                    mIdOcupacion = 0;
                    txtOcupacionName.Text = "";
                }


            }
            else
            {
                ControlOperation.alertWarning("Seleccione una ocupación para eliminar");
            }

        }

        public void fillDtgOcuapcion()
        {
            DataTable lDtOcupacion = new DataTable();
            ControlDePacientes.Dal.Ocupacion lOcupacion = new Ocupacion();
            lDtOcupacion = lOcupacion.GetOcupacion();

            dtgOcupacion.ItemsSource = lDtOcupacion.DefaultView;

        }

        private void dtgOcupacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            //SELECCIONA LA FILA
            DataRowView lCurrentDataRowView = ((DataRowView)((DataGrid)sender).SelectedItem);

            if (lCurrentDataRowView == null)
                return;

            mIdOcupacion = (int)lCurrentDataRowView.Row["IdOcupacion"];
            txtOcupacionName.Text = (string)lCurrentDataRowView.Row["OcupacionName"];

        }
    }
}
