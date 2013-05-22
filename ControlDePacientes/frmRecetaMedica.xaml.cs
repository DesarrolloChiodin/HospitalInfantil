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
    /// Interaction logic for frmRecetaMedica.xaml
    /// </summary>
    public partial class frmRecetaMedica : Page
    {

        public DataTable mDtRecetaDetail;
        public bool mIsEdition { get; set; }
        public bool mIsForDeliver { get; set; }
        public int mIdReceta;
        public int mIdPaciente;
        public string mRegistroMedico { get; set; }
        public ControlDePacientes.Entities.receta mReceta { get; set; }
        public int mIdRecetaDetail;
        public frmRecetaMedica()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lblDate.Content = DateTime.Today;
            btnDeliver.Visibility = Visibility.Collapsed;
            if (mIsEdition)
            {
                txtIdPaciente.Text = mRegistroMedico;// mReceta.Codpaciente.ToString();
                mIdReceta = mReceta.Idreceta;
                fillData();
                fillDtgMedicamento();
                FillComboBoxMedicina();


            }

            if (mIsForDeliver)
            {
                btnDeliver.Visibility = Visibility.Visible;
                HideControls();

            }
        }

        private void HideControls()
        {
            lblCantidad.Visibility = Visibility.Collapsed;
            txtCantidad.Visibility = Visibility.Collapsed;
            lblMedicamento.Visibility = Visibility.Collapsed;
            cmbMedicamento.Visibility = Visibility.Collapsed;
            lblIndicaciones.Visibility = Visibility.Collapsed;
            txtIndicaciones.Visibility = Visibility.Collapsed;

            btnAdd.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Collapsed;
            btnCancel.Visibility = Visibility.Collapsed;

        }

        private void txtIdPaciente_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                try
                {

                    fillData();
                    FillComboBoxMedicina();
                }
                catch (Exception ex)
                {

                    ControlOperation.alertWarning(" " + ex);
                }
            }
        }

        private void fillData()
        {
            DataTable lDtPaciente = new DataTable();
            ControlDePacientes.Dal.Paciente lPaciente = new Paciente();

            lDtPaciente = lPaciente.GetPacienteByRegistroMedico(txtIdPaciente.Text);

            if (lDtPaciente.Rows.Count > 0)
            {
                txtIdPaciente.IsEnabled = false;
                lblPacienteName.Content = (string)lDtPaciente.Rows[0]["Nombre"] + " " + (string)lDtPaciente.Rows[0]["Apellido"] + " " + (string)lDtPaciente.Rows[0]["SegundoApellido"];
                mIdPaciente = (int) lDtPaciente.Rows[0]["IdPaciente"];
            }
        }

        private void AddRowComboBox(DataTable pDt)
        {
            DataRow lDr = pDt.NewRow();
            lDr[0] = 0;
            lDr[1] = "-- Seleccione --";
            pDt.Rows.InsertAt(lDr, 0);
        }

        private void FillComboBoxMedicina()
        {
            ControlDePacientes.Dal.Medicina lMedicina = new ControlDePacientes.Dal.Medicina();
            DataTable lDtMedicina = new DataTable();
            lDtMedicina = lMedicina.GetMedicina();
            AddRowComboBox(lDtMedicina);
            cmbMedicamento.ItemsSource = lDtMedicina.DefaultView;
            cmbMedicamento.DisplayMemberPath = "MedicinaName";
            cmbMedicamento.SelectedValuePath = "IdMedicina";
            cmbMedicamento.SelectedIndex = 0;


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string lString;
            lString = Validacion();
            if (lString == "")
            {

                if (mIdReceta == 0)
                {
                    SaveReceta();
                    txtIdPaciente.IsEnabled = false;
                    SaveRecetaDetail();
                    fillDtgMedicamento();
                    CleanControls();
                }
                else
                {
                    SaveRecetaDetail();
                    fillDtgMedicamento();
                    CleanControls();
                }
            }
            else
            {
                ControlOperation.alertWarning(lString);
            }
        }

        private void SaveReceta()
        {
            ControlDePacientes.Entities.receta lRecetaEntity = new receta();
            lRecetaEntity.Fechareceta = DateTime.Now;
            lRecetaEntity.Codpaciente = mIdPaciente;
            lRecetaEntity.Recetaname = "";
            lRecetaEntity.Codestado = 3;//ESTADO PENDIENTE DE ENTREGA

            ControlDePacientes.Dal.Receta lReceta = new Receta();
            mIdReceta = lReceta.AddReceta(lRecetaEntity);

        }

        private void SaveRecetaDetail()
        {
            ControlDePacientes.Entities.recetadetalle lRecetadetalleEntity = new recetadetalle();
            lRecetadetalleEntity.Cantidad = Convert.ToInt32(txtCantidad.Text);
            lRecetadetalleEntity.Codmedicina = (int)cmbMedicamento.SelectedValue;
            lRecetadetalleEntity.Indicaciones = txtIndicaciones.Text;
            lRecetadetalleEntity.Recetadetallename = "";
            lRecetadetalleEntity.Codreceta = mIdReceta;

            ControlDePacientes.Dal.RecetaDetalle lRecetaDetalle = new RecetaDetalle();
            mIdRecetaDetail = lRecetaDetalle.AddRecetaDetalle(lRecetadetalleEntity);
        }

        private string Validacion()
        {
            int lIntNumber;
            if (txtIdPaciente.Text == "")
                return "Ingrese el Código del Paciente";
            //if (!Int32.TryParse(txtIdPaciente.Text, out lIntNumber))
            //    return "El Código del Paciente debe ser numeríco";
            if (txtCantidad.Text == "")
                return "Ingrese una Cantidad";
            if (!Int32.TryParse(txtCantidad.Text, out lIntNumber))
                return "La Cantidad debe ser numeríco";
            if (txtIndicaciones.Text == "")
                return "Ingrese las indicaciones del medicamento";
            if (cmbMedicamento.SelectedIndex == 0)
                return "Debe seleccionar un medicamento";
            return "";

        }

        private void CleanControls()
        {
            txtCantidad.Text = "";
            txtIndicaciones.Text = "";
            cmbMedicamento.SelectedIndex = 0;
        }

        private void fillDtgMedicamento()
        {
            ControlDePacientes.Dal.RecetaDetalle lRecetaDetalle = new RecetaDetalle();
            mDtRecetaDetail = new DataTable();
            mDtRecetaDetail = lRecetaDetalle.GetRecetaDetalleByIdReceta(mIdReceta);
            dtgDetalleMedicamento.ItemsSource = mDtRecetaDetail.DefaultView;

        }

        private void dtgDetalleMedicamento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SELECCIONA LA FILA
            DataRowView lCurrentDataRowView = ((DataRowView)((DataGrid)sender).SelectedItem);

            if (lCurrentDataRowView == null)
                return;

            mIdRecetaDetail = (int)lCurrentDataRowView.Row["IdRecetaDetalle"];
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (dtgDetalleMedicamento.ItemsSource == null || dtgDetalleMedicamento.Items.Count == 0 || mIdReceta == 0)
            {
                CleanControls();
                lblPacienteName.Content = "";
                txtIdPaciente.Text = "";
                txtIdPaciente.IsEnabled = true;
                dtgDetalleMedicamento.ItemsSource = null;
                mIdReceta = 0;
                mIdRecetaDetail = 0;


            }
            else
            {
                if (
                    ControlOperation.alertConfirm("Desea Cancelar  la receta médica?")== MessageBoxResult.Yes)
                {
                    ControlDePacientes.Dal.Receta lReceta = new Receta();
                    lReceta.DeleteRecetaAndRecetaDetalle(mIdReceta);
                    MessageBox.Show("Se ha cancelado con éxito", "Información del Sistema");
                }
            }


        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (mIdRecetaDetail == 0)
            {
                ControlOperation.alertWarning("Seleccione un detalle para eliminar");
                return;
            }

            if (ControlOperation.alertConfirm("Desea eliminar el registro selccionado?") == MessageBoxResult.Yes)
            {
                ControlDePacientes.Dal.RecetaDetalle lRecetaDetalle = new RecetaDetalle();
                lRecetaDetalle.DeleteRecetaDetalle(mIdRecetaDetail);
                fillDtgMedicamento();

                MessageBox.Show("Se ha eliminado con éxito", "Información del Sistema");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (dtgDetalleMedicamento.ItemsSource == null || dtgDetalleMedicamento.Items.Count == 0 || mIdReceta == 0)
            {
                ControlOperation.alertWarning("No se ha ingresado ningún medicamento");
            }
            else
            {
                ControlOperation.alertInformation("Receta No: --" + mIdReceta + " --generada con éxito");

            }

        }

        private void btnDeliver_Click(object sender, RoutedEventArgs e)
        {
            ControlDePacientes.Dal.Receta lReceta = new Receta();
            lReceta.UpdReceta(mIdReceta,ControlDePacientes.Entities.GlobalUser.Idusuario);//componer lo del id del usuario
            dtgDetalleMedicamento.ItemsSource = null;
            ControlOperation.alertInformation("Medicamento Entregado con éxito");
        }
    }
}
