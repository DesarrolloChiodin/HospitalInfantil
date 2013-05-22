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
using DurationCalculatorApp;


namespace ControlDePacientes
{
    /// <summary>
    /// Interaction logic for frmRegistroMedico.xaml
    /// </summary>
    public partial class frmRegistroMedico : Page
    {
        public frmRegistroMedico()
        {
            InitializeComponent();
        }

        public int mIdPaciente;
        public int mIdHistorialMedico;

        private void AddRowComboBox(DataTable pDt)
        {
            DataRow lDr = pDt.NewRow();
            lDr[0] = 0;
            lDr[1] = "-- Seleccione --";
            pDt.Rows.InsertAt(lDr, 0);
        }

        private void fillCmbDiagnosticoEntrada()
        {
            ControlDePacientes.Dal.TipoEnfermedad lTipoEnfermedad = new ControlDePacientes.Dal.TipoEnfermedad();
            DataTable lDtTipoEnfermedad = new DataTable();
            lDtTipoEnfermedad = lTipoEnfermedad.TipoEnfermedadGetAll();
            AddRowComboBox(lDtTipoEnfermedad);
            cmbDiagnosticoInicial.ItemsSource = lDtTipoEnfermedad.DefaultView;
            cmbDiagnosticoInicial.DisplayMemberPath = "TipoEnfermedadName";
            cmbDiagnosticoInicial.SelectedValuePath = "IdTipoEnfermedad";
            cmbDiagnosticoInicial.SelectedIndex = 0;
        }



        private void fillCmbDiagnosticoSalida()
        {
            ControlDePacientes.Dal.TipoEnfermedad lTipoEnfermedad = new ControlDePacientes.Dal.TipoEnfermedad();
            DataTable lDtTipoEnfermedad = new DataTable();
            lDtTipoEnfermedad = lTipoEnfermedad.TipoEnfermedadGetAll();
            AddRowComboBox(lDtTipoEnfermedad);
            cmbDiagnosticoFinal.ItemsSource = lDtTipoEnfermedad.DefaultView;
            cmbDiagnosticoFinal.DisplayMemberPath = "TipoEnfermedadName";
            cmbDiagnosticoFinal.SelectedValuePath = "IdTipoEnfermedad";
            cmbDiagnosticoFinal.SelectedIndex = 0;
        }

        private void fillCmbServicio()
        {
            ControlDePacientes.Dal.Service lService = new ControlDePacientes.Dal.Service();
            DataTable lDtService = new DataTable();
            lDtService = lService.ServicioGetAll();
            AddRowComboBox(lDtService);
            cmbServicio.ItemsSource = lDtService.DefaultView;
            cmbServicio.DisplayMemberPath = "ServicioName";
            cmbServicio.SelectedValuePath = "IdServicio";
            cmbServicio.SelectedIndex = 0;

        }

        private void fillDestinoCaso()
        {
            ControlDePacientes.Dal.DestinoCaso lDestinoCaso = new ControlDePacientes.Dal.DestinoCaso();
            DataTable lDtDestinoCaso = new DataTable();
            lDtDestinoCaso = lDestinoCaso.DestinoCasoGetAll();
            AddRowComboBox(lDtDestinoCaso);
            cmbDestinoCaso.ItemsSource = lDtDestinoCaso.DefaultView;
            cmbDestinoCaso.DisplayMemberPath = "DestinoCasoName";
            cmbDestinoCaso.SelectedValuePath = "IdDestinoCaso";
            cmbDestinoCaso.SelectedIndex = 0;


        }

        private void txtIdPaciente_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                try
                {

                    ControlDePacientes.Dal.Paciente lPacienteDal = new Paciente();
                    DataTable lDtPaciente = lPacienteDal.GetPacienteByRegistroMedico(txtIdPaciente.Text);
                    if (lDtPaciente.Rows.Count > 0)
                    {

                        mIdPaciente = (int)lDtPaciente.Rows[0]["IdPaciente"];


                        DataTable lDtVerifyHistorial = new DataTable();
                        ControlDePacientes.Dal.HistorialMedico lHistorialMedicoVerify = new HistorialMedico();
                        lDtVerifyHistorial = lHistorialMedicoVerify.HistorialMedicoVerify(mIdPaciente, 10);
                        //1 es Iniciado


                        if (lDtVerifyHistorial.Rows.Count > 0)
                        {
                            fillCmbDiagnosticoSalida();
                            fillCmbDiagnosticoEntrada();
                            fillCmbDiagnosticoEntrada();
                            fillCmbServicio();
                            fillDestinoCaso();
                            mIdHistorialMedico = (int)lDtVerifyHistorial.Rows[0]["IdHistorialMedico"];
                            mIdPaciente = (int)lDtVerifyHistorial.Rows[0]["CodPaciente"];
                            fillData();
                            fillDataExamenFisico(mIdHistorialMedico);
                            fillDtgHistorialMedico();


                        }
                        else
                        {
                            ControlOperation.alertWarning("El Paciente no tiene exámen físico realizado");

                        }

                    }
                }
                catch (Exception ex)
                {

                    ControlOperation.alertWarning("Error:--> " + ex);
                }
            }
        }

        private void fillDtgHistorialMedico()
        {
            HistorialMedico lHistorialMedicoPaciente = new HistorialMedico();
            DataTable lDtHistorialMedicoPaciente = lHistorialMedicoPaciente.HistorialMedicoGetAllByIdPaciente(mIdPaciente);

            dtgHistorialPaciente.ItemsSource = lDtHistorialMedicoPaciente.DefaultView;

        }

        private void fillData()
        {
            DataTable lDtHistorialMedico = new DataTable();
            ControlDePacientes.Dal.Paciente lPaciente = new Paciente();

            lDtHistorialMedico = lPaciente.GetPacienteByRegistroMedico(txtIdPaciente.Text);

            if (lDtHistorialMedico.Rows.Count > 0)
            {
                txtIdPaciente.IsEnabled = false;
                txtNombrePaciente.Text = (string)lDtHistorialMedico.Rows[0]["Nombre"];
                txtNombrePaciente.IsEnabled = false;
                if (lDtHistorialMedico.Rows[0]["Apellido"].ToString() != "" || lDtHistorialMedico.Rows[0]["SegundoApellido"].ToString() != "")
                    txtApellidoPaciente.Text = (string)lDtHistorialMedico.Rows[0]["Apellido"] + " " + (string)lDtHistorialMedico.Rows[0]["SegundoApellido"];
                else txtApellidoPaciente.Text = "";
                txtApellidoPaciente.IsEnabled = false;
                if (lDtHistorialMedico.Rows[0]["SexoName"].ToString() != "")
                    txtSexo.Text = (string)lDtHistorialMedico.Rows[0]["SexoName"];
                else txtSexo.Text = "";
                txtSexo.IsEnabled = false;
                //lblEdad.Content = Convert.ToString( CalcularEdad(DateTime.Parse(Convert.ToString(lDtHistorialMedico.Rows[0]["FechaNacimiento"]))));
                if (lDtHistorialMedico.Rows[0]["FechaNacimiento"].ToString() != "")
                {
                    DateDifference dateDifference =
                        new DateDifference(DateTime.Parse(Convert.ToString(lDtHistorialMedico.Rows[0]["FechaNacimiento"])),
                                           DateTime.Now.Date);
                    lblEdad.Content = dateDifference.ToString();
                }
                else lblEdad.Content = "";

                if (lDtHistorialMedico.Rows[0]["NombrePadre"].ToString() != "")
                    txtNombrePadre.Text = (string)lDtHistorialMedico.Rows[0]["NombrePadre"];
                else txtNombrePadre.Text = "";
                txtNombrePadre.IsEnabled = false;
                if (lDtHistorialMedico.Rows[0]["NombreMadre"].ToString() != "")
                    txtNombreMadre.Text = (string)lDtHistorialMedico.Rows[0]["NombreMadre"];
                else txtNombreMadre.Text = "";
                txtNombreMadre.IsEnabled = false;
                if (lDtHistorialMedico.Rows[0]["Direccion"].ToString() != "")
                    txtDireccion.Text = (string)lDtHistorialMedico.Rows[0]["Direccion"]; //unir con municipio y departamento
                else txtDireccion.Text = "";
                txtDireccion.IsEnabled = false;
                if (lDtHistorialMedico.Rows[0]["FotoPath"] == DBNull.Value || (string)lDtHistorialMedico.Rows[0]["FotoPath"] == "")
                { }
                else ControlOperation.LoadImageFromDB((byte[])lDtHistorialMedico.Rows[0]["FotoPaciente"], imgPaciente);

                expDetalle.IsEnabled = true;
                expDetalle.IsExpanded = true;
                wrpButtons.Visibility = Visibility.Visible;

            }
        }

        private void fillDataExamenFisico(int pIdHistorialFisico)
        {

            fillCmbDiagnosticoSalida();
            //Filldesstino.....
            ControlDePacientes.Dal.HistorialMedico lHistorialMedico = new HistorialMedico();
            DataTable lDtHistorialMedico = new DataTable();
            lDtHistorialMedico = lHistorialMedico.HistorialMedicoGetAll(mIdHistorialMedico);

            if (lDtHistorialMedico.Rows.Count > 0)
            {
                txtMotivoConsulta.Text = lDtHistorialMedico.Rows[0]["MotivoConsulta"].ToString() != "" ? (string)lDtHistorialMedico.Rows[0]["MotivoConsulta"] : "";
                txtMotivoConsulta.IsEnabled = false;

                txtPA.Text = lDtHistorialMedico.Rows[0]["PresionArterial"].ToString() != "" ? Convert.ToString(lDtHistorialMedico.Rows[0]["PresionArterial"]) : "";
                txtPA.IsEnabled = false;
                //cmbDiagnosticoInicial.SelectedValue = (int)lDtHistorialMedico.Rows[0]["DxIngreso"];
                //cmbDiagnosticoInicial.IsEnabled = false;


                if (lDtHistorialMedico.Rows[0]["CodServicio"].ToString() != "")//|| (int) lDtHistorialMedico.Rows[0]["CodServicio"] != 0)
                    cmbServicio.SelectedValue = (int)lDtHistorialMedico.Rows[0]["CodServicio"];
                else cmbServicio.SelectedIndex = 0;

                cmbServicio.IsEnabled = false;

                txtFC.Text = lDtHistorialMedico.Rows[0]["FrecuenciaCardiaca"].ToString() != "" ? Convert.ToString(lDtHistorialMedico.Rows[0]["FrecuenciaCardiaca"]) : "";
                txtFC.IsEnabled = false;
                txtRX.Text = lDtHistorialMedico.Rows[0]["RayosX"].ToString() != "" ? Convert.ToString(lDtHistorialMedico.Rows[0]["RayosX"]) : "";
                txtRX.IsEnabled = false;
                txtFR.Text = lDtHistorialMedico.Rows[0]["FrecuenciaRespiratora"].ToString() != "" ? Convert.ToString(lDtHistorialMedico.Rows[0]["FrecuenciaRespiratora"]) : "";
                txtFR.IsEnabled = false;
                txtTemp.Text = lDtHistorialMedico.Rows[0]["Temperatura"].ToString() != "" ? Convert.ToString(lDtHistorialMedico.Rows[0]["Temperatura"]) : "";
                txtTemp.IsEnabled = false;
                txtCC.Text = lDtHistorialMedico.Rows[0]["CC"].ToString() != "" ? Convert.ToString(lDtHistorialMedico.Rows[0]["CC"]) : "";
                txtCC.IsEnabled = false;
                txtPeso.Text = lDtHistorialMedico.Rows[0]["Peso"].ToString() != "" ? Convert.ToString(lDtHistorialMedico.Rows[0]["Peso"]) : "";
                txtPeso.IsEnabled = false;
                txtTalla.Text = lDtHistorialMedico.Rows[0]["Talla"].ToString() != "" ? Convert.ToString(lDtHistorialMedico.Rows[0]["Talla"]) : "";
                txtTalla.IsEnabled = false;
                txtMC.Text = lDtHistorialMedico.Rows[0]["MasaCorporal"].ToString() != "" ? Convert.ToString(lDtHistorialMedico.Rows[0]["MasaCorporal"]) : "";
                txtMC.IsEnabled = false;
                txtTE.Text = lDtHistorialMedico.Rows[0]["TE"].ToString() != "" ? Convert.ToString(lDtHistorialMedico.Rows[0]["TE"]) : "";
                txtTE.IsEnabled = false;
                txtPE.Text = lDtHistorialMedico.Rows[0]["PE"].ToString() != "" ? Convert.ToString(lDtHistorialMedico.Rows[0]["PE"]) : "";
                txtPE.IsEnabled = false;
                txtPesoTalla.Text = lDtHistorialMedico.Rows[0]["PesoTalla"].ToString() != "" ? Convert.ToString(lDtHistorialMedico.Rows[0]["PesoTalla"]) : "";
                txtPesoTalla.IsEnabled = false;
                txtSaturacion.Text = lDtHistorialMedico.Rows[0]["SaturacionO2"].ToString() != "" ? Convert.ToString(lDtHistorialMedico.Rows[0]["SaturacionO2"]) : "";
                txtSaturacion.IsEnabled = false;
                txtExamenFisico.Text = lDtHistorialMedico.Rows[0]["ExamenFisico"].ToString() != "" ? (string)lDtHistorialMedico.Rows[0]["ExamenFisico"] : "";
                txtExamenFisico.IsEnabled = false;
                txtAntecedentes.Text = lDtHistorialMedico.Rows[0]["AntecedentesImportancia"].ToString() != "" ? (string)lDtHistorialMedico.Rows[0]["AntecedentesImportancia"] : "";

                txtAntecedentes.IsEnabled = false;


            }
            else
            {
                ControlOperation.alertWarning("El Paciente no tiene Examen Físico realizado");
            }


        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ControlDePacientes.Dal.HistorialMedico lHistorialMedico = new HistorialMedico();
                ControlDePacientes.Entities.historialmedico lHistorialmedicoEntity = new historialmedico();


                lHistorialmedicoEntity.Idhistorialmedico = mIdHistorialMedico;
                if ((int)cmbDiagnosticoFinal.SelectedIndex != 0)
                    lHistorialmedicoEntity.Dxegreso = (int)cmbDiagnosticoFinal.SelectedValue;
                if ((int)cmbDiagnosticoInicial.SelectedIndex != 0)
                    lHistorialmedicoEntity.Dxingreso = (int)cmbDiagnosticoInicial.SelectedValue;
                //else lHistorialmedicoEntity.Dxegreso = 0;
                lHistorialmedicoEntity.Estadonutricinal = txtEstadoNutricional.Text;
                lHistorialmedicoEntity.Estudiosiniciales = txtEstudioInicial.Text;
                lHistorialmedicoEntity.Impresionclinica = txtImpresion.Text;
                lHistorialmedicoEntity.Tratamientoconsultaexterna = txtTratamiento.Text;
                lHistorialmedicoEntity.Notasdescargo = txtNotaDescargo.Text;
                lHistorialmedicoEntity.Coddestinocaso = (int)cmbDestinoCaso.SelectedValue;
                lHistorialmedicoEntity.Observaciones = txtObservaciones.Text;
                lHistorialmedicoEntity.Codestado = 11;//estado finalizado
                lHistorialmedicoEntity.Coddoctor = ControlDePacientes.Entities.GlobalUser.Idusuario;

                lHistorialMedico.UpdHistorialMedico(lHistorialmedicoEntity);

                ControlOperation.alertInformation("Se Guardó la Información éxitosamente");



                cmbDiagnosticoFinal.IsEnabled = false;
                txtEstadoNutricional.IsEnabled = false;
                txtEstudioInicial.IsEnabled = false;
                txtImpresion.IsEnabled = false;
                txtTratamiento.IsEnabled = false;
                cmbDestinoCaso.IsEnabled = false;
                txtObservaciones.IsEnabled = false;
                txtNotaDescargo.IsEnabled = false;
                cmbDiagnosticoInicial.IsEnabled = false;

            }
            catch (Exception ex)
            {

                ControlOperation.alertWarning(ex.Message);
            }


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            LoopVisualTree(this);


            imgPaciente.Source = new BitmapImage(new Uri("/ControlDePacientes;component/Images/nophoto.png", UriKind.Relative));
            frmRegistroMedico lFrmRegistroMedico = new frmRegistroMedico();
            this.NavigationService.Navigate(lFrmRegistroMedico);
        }

        void LoopVisualTree(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                if (obj is TextBox)
                    ((TextBox)obj).Text = null;
                if (obj is ComboBox)
                    ((ComboBox)obj).SelectedIndex = 0;
                if (obj is DataGrid)
                    ((DataGrid)obj).ItemsSource = null;
                LoopVisualTree(VisualTreeHelper.GetChild(obj, i));
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ControlOperation.alertConfirm("Desea eliminar el Historial Médico del paciente?") == MessageBoxResult.Yes)
                {
                    ControlDePacientes.Dal.HistorialMedico lHistorialMedico = new HistorialMedico();
                    lHistorialMedico.DeleteHistorialMedico(mIdHistorialMedico);

                    ControlOperation.alertInformation("El Historial Médico se eliminó con éxito");
                    cmbDiagnosticoFinal.IsEnabled = false;
                    txtEstadoNutricional.IsEnabled = false;
                    txtEstudioInicial.IsEnabled = false;
                    txtImpresion.IsEnabled = false;
                    txtTratamiento.IsEnabled = false;
                    cmbDestinoCaso.IsEnabled = false;
                    txtObservaciones.IsEnabled = false;
                    txtNotaDescargo.IsEnabled = false;
                    cmbDiagnosticoInicial.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {

                ControlOperation.alertWarning(ex.Message);
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lblDate.Content = DateTime.Today.Date.ToShortDateString();
        }

        private void dtgHistorialPaciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnMedicamento_Click(object sender, RoutedEventArgs e)
        {
            frmRecetaMedica lFrmRecetaMedica = new frmRecetaMedica();
            this.NavigationService.Navigate(lFrmRecetaMedica);
            //MainFrame.NavigationService.Navigate(new Uri("frmRecetaMedica.xaml", UriKind.Relative));
        }
    }
}
