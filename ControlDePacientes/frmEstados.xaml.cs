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
using ControlDePacientes.Logic;
using ControlDePacientes.Dal;
using ControlDePacientes.Entities;

namespace ControlDePacientes
{
    /// <summary>
    /// Interaction logic for frmEstados.xaml
    /// </summary>
    public partial class frmEstados : Page
    {
        public frmEstados()
        {
            InitializeComponent();
        }

        public int mIdTipoEstado;
        public int mIdEstado;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ComboTipoEstadoLoad();
            loadDatagridTipoEstado();
            loadDatagridEstado();


        }



        private void loadDatagridTipoEstado()
        {
            DataTable lDtTipoEstadoGrid = new DataTable();
            Estados lEstadosGrid = new Estados();
            lDtTipoEstadoGrid = lEstadosGrid.GetTipodEstado();

            dtgTipoEstado.ItemsSource = lDtTipoEstadoGrid.DefaultView;
        }
        private void loadDatagridEstado()
        {
            DataTable lDtEstadoGrid = new DataTable();
            Estados lEstadosGrid = new Estados();
            lDtEstadoGrid = lEstadosGrid.GetEstado();

            dtgEstado.ItemsSource = lDtEstadoGrid.DefaultView;
        }

        private void AddRowComboBox(DataTable pDt)
        {
            DataRow lDr = pDt.NewRow();
            lDr[0] = 0;
            lDr[1] = "-- Seleccione --";
            pDt.Rows.InsertAt(lDr, 0);
        }

        private void ComboTipoEstadoLoad()
        {
            DataTable lDtTipoEstado = new DataTable();
            Estados lEstados = new Estados();
            lDtTipoEstado = lEstados.GetTipodEstado();

            AddRowComboBox(lDtTipoEstado);

            cmbTipoEstado.ItemsSource = lDtTipoEstado.DefaultView;
            cmbTipoEstado.DisplayMemberPath = "TipoEstadoName";
            cmbTipoEstado.SelectedValuePath = "IdTipoEstado";
            cmbTipoEstado.SelectedIndex = 0;
        }

        private void btnSaveTipoEstado_Click(object sender, RoutedEventArgs e)
        {
            string lStringValidate;
            lStringValidate = ValidateTipoEstadoAdd();

            if (lStringValidate == "")
            {
                tipoestado lTipoestado = new tipoestado();
                lTipoestado.Tipoestadoname = txtNombreTipoEstado.Text;

                Estados lEstados = new Estados();
                lEstados.AddTipoEstado(lTipoestado);

               ControlOperation.alertWarning( "Tipo de Estado guardado con éxito");
                loadDatagridTipoEstado();
            }
            else ControlOperation.alertWarning(lStringValidate);



        }

        private string ValidateTipoEstadoAdd()
        {
            if (txtNombreTipoEstado.Text == "")
                return "Debe de Ingresar una descripción del tipo de estado";
            else return "";
        }

        private string ValidateEstadoAdd()
        {
            if (txtNombreEstado.Text == "")
                return "Debe de Ingresar una descripción del tipo de estado";
            else if (cmbTipoEstado.SelectedIndex == 0)
                return "Debe seleccionar un Tipo de Estado";
            else return "";

        }

        private void BtnSaveEstado_Click(object sender, RoutedEventArgs e)
        {
            string lStringValidate;
           lStringValidate = ValidateEstadoAdd();
            if(lStringValidate=="")
            {
                Estados lEstados = new Estados();
                ControlDePacientes.Entities.Estado lEstado = new Estado();

                lEstado.Estadoname = txtNombreEstado.Text;
                lEstado.Codtipoestado = (int)cmbTipoEstado.SelectedValue;

                lEstados.AddEstado(lEstado);

               ControlOperation.alertInformation("Estado guardado con éxito");
                loadDatagridEstado();

            }
            else
            {
                ControlOperation.alertWarning(lStringValidate);
            }
        }

        private void btnCancelTipoEstado_Click(object sender, RoutedEventArgs e)
        {
            txtNombreTipoEstado.Text = "";
        }

        private void btnCancelEstado_Click(object sender, RoutedEventArgs e)
        {
            txtNombreEstado.Text = "";
        }

        private void btnDeleteTipoEstado_Click(object sender, RoutedEventArgs e)
        {
            if (mIdTipoEstado != 0)
            {
                ControlDePacientes.Dal.Estados lEstados = new Estados();
                lEstados.DeleteTipoEstado(mIdTipoEstado);
                mIdTipoEstado = 0;
                ControlOperation.alertInformation("Registro eliminado con éxito");
                loadDatagridTipoEstado();
            }
            else
            {
                ControlOperation.alertWarning("Seleccione un registro para eliminar");
            }

           
        }

        private void dtgTipoEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView lCurrentDataRowView = ((DataRowView)((DataGrid)sender).SelectedItem);

            if (lCurrentDataRowView == null)
                return;

            mIdTipoEstado = (int)lCurrentDataRowView.Row[0];
            

        }

        private void btnDeleteEstado_Click(object sender, RoutedEventArgs e)
        {
            if (mIdEstado != 0)
            {
                ControlDePacientes.Dal.Estados lEstados = new Estados();
                lEstados.DeleteEstado(mIdEstado);
                mIdEstado = 0;
                ControlOperation.alertInformation("Registro eliminado con éxito");
                loadDatagridEstado();
            }
            else
            {
                ControlOperation.alertWarning("Seleccione un registro para eliminar");
            }
        }

        private void dtgEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataRowView lCurrentDataRowView = ((DataRowView)((DataGrid)sender).SelectedItem);

            if (lCurrentDataRowView == null)
                return;

            mIdEstado = (int)lCurrentDataRowView.Row[0];
            
        }

       


    }
}
