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
using DurationCalculatorApp;
using ControlDePacientes.Logic;
using TipoEnfermedad = ControlDePacientes.Dal.TipoEnfermedad;

namespace ControlDePacientes
{
    /// <summary>
    /// Interaction logic for frmExamenFisico.xaml
    /// </summary>
    public partial class frmExamenFisico : Page
    {
        public frmExamenFisico()
        {
            InitializeComponent();
        }

        public int mIdHistorialMedico;
        public int mIdPaciente;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lblDate.Content = DateTime.Now;
            expExamenFisico.IsEnabled = false;
            expExamenFisico.IsExpanded = false;
            wrpButtons.Visibility = Visibility.Collapsed;
            txtIdPaciente.Focus();
            btnDelete.Visibility = Visibility.Collapsed;

        }

        private void txtIdPaciente_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                try
                {
                   

                    fillData();
                    
                    fillCmbServicio();
                }
                catch (Exception ex)
                {

                    ControlOperation.alertWarning("Error:--> " + ex);
                }
            }
        }

        private void AddRowComboBox(DataTable pDt)
        {
            DataRow lDr = pDt.NewRow();
            lDr[0] = 0;
            lDr[1] = "-- Seleccione --";
            pDt.Rows.InsertAt(lDr, 0);
        }

        //private void fillCmbDiagnosticoEntrada()
        //{
        //    ControlDePacientes.Dal.TipoEnfermedad lTipoEnfermedad = new ControlDePacientes.Dal.TipoEnfermedad();
        //    DataTable lDtTipoEnfermedad = new DataTable();
        //    lDtTipoEnfermedad = lTipoEnfermedad.TipoEnfermedadGetAll();
        //    AddRowComboBox(lDtTipoEnfermedad);
        //    cmbDiagnosticoInicial.ItemsSource = lDtTipoEnfermedad.DefaultView;
        //    cmbDiagnosticoInicial.DisplayMemberPath = "TipoEnfermedadName";
        //    cmbDiagnosticoInicial.SelectedValuePath = "IdTipoEnfermedad";
        //    cmbDiagnosticoInicial.SelectedIndex = 0;
        //}

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

        private string ValidateFields()
        {
            decimal lNumber;
            //if (txtMotivoConsulta.Text == "")
            //    return "Escriba el motivo de la consulta";

            //if (cmbDiagnosticoInicial.SelectedIndex == 0)
            //    return "Seleccione un diagnóstico inicial";
            if (cmbServicio.SelectedIndex == 0)
                return "Seleccione un servicio";

            //if (txtPA.Text == "")
            //    return "Ingrese el valor de la Presión Arterial";

            //if (txtFR.Text == "")
            //    return "Ingrese la frecuencia respiratoria";
            ////if (txtRX.Text =="")
            ////    return "Ingrese el valor de Rayos X ingresada debe ser numérico";
            //if (txtFC.Text == "")
            //    return "Ingrese el valor de la frecuencia Cardiaca";
            //if (txtTemp.Text == "")
            //    return "Ingrese el valor de la temperatura";
            //if (txtCC.Text == "")
            //    return "Ingrese el valor de la circunferencia cefálica";
            //if (txtPeso.Text == "")
            //    return "Ingrese el valor del peso";
            //if (txtTalla.Text == "")
            //    return "Ingrese el valor de la talla";
            //if (txtMC.Text == "")
            //    return "Ingrese el valor de la masa corporal";
            //if (txtTE.Text == "")
            //    return "Ingrese el valor la talla edad";
            //if (txtPE.Text == "")
            //    return "Ingrese el valor del Peso edad";
            //if (txtSaturacion.Text == "")
            //    return "Ingrese el valor de la Saturación CO2";

            //if (txtExamenFisico.Text == "")
            //    return "Escriba un exámen físico";
            //if (txtAntecedentes.Text == "")
            //    return "Escriba un antecedente importante";



            return "";
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //Validar que  tenga un historial activo
            DataTable lDtVerifyHistorial = new DataTable();
            ControlDePacientes.Dal.HistorialMedico lHistorialMedicoVerify = new HistorialMedico();
            lDtVerifyHistorial = lHistorialMedicoVerify.HistorialMedicoVerify(mIdPaciente, 10); //10 es Iniciado

            if (lDtVerifyHistorial.Rows.Count > 0)
            {
                mIdHistorialMedico = (int)lDtVerifyHistorial.Rows[0]["IdHistorialMedico"];


                string lMessage = ValidateFields();
                if (lMessage == "")
                {
                    SaveExamenFisico();
                    ControlOperation.alertInformation("Se Guardó el exámen físico del paciente");
                    btnDelete.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Collapsed;
                }
                else
                    ControlOperation.alertWarning(lMessage);
            }
            else
            {
                ControlOperation.alertWarning("El Paciente no tiene un Registro Médico, no se puede guardar el examen físico");
            }
        }

        private void SaveExamenFisico()
        {
            try
            {
                ControlDePacientes.Entities.historialmedico lHistorialmedico = new historialmedico();
                lHistorialmedico.Idhistorialmedico = mIdHistorialMedico;
                lHistorialmedico.Motivoconsulta = txtMotivoConsulta.Text != "" ? txtMotivoConsulta.Text : "";
                //lHistorialmedico.Dxingreso = (int)cmbDiagnosticoInicial.SelectedValue;
                lHistorialmedico.Fecha = DateTime.Now;
                lHistorialmedico.Codusuario = ControlDePacientes.Entities.GlobalUser.Idusuario;
                lHistorialmedico.Presionarterial = txtPA.Text != "" ? txtPA.Text : "";
                lHistorialmedico.Frecuenciacardiaca = txtFC.Text != "" ? txtFC.Text : "";
                lHistorialmedico.Rayosx = txtRX.Text != "" ? txtRX.Text : "";
                lHistorialmedico.Frecuenciarespiratora = txtFR.Text != "" ? txtFR.Text : "";
                lHistorialmedico.Temperatura = txtTemp.Text != "" ? txtTemp.Text : "";
                lHistorialmedico.Cc = txtCC.Text != "" ? txtCC.Text : "";
                lHistorialmedico.Peso = txtPeso.Text != "" ? txtPeso.Text : "";
                lHistorialmedico.Talla = txtTalla.Text != "" ? txtTalla.Text : "";
                lHistorialmedico.Masacorporal = txtMC.Text != "" ? txtMC.Text : "";
                lHistorialmedico.Te = txtTE.Text != "" ? txtTE.Text : "";
                lHistorialmedico.Pe = txtPE.Text != "" ? txtPE.Text : "";
                lHistorialmedico.Saturaciono2 = txtSaturacion.Text != "" ? txtSaturacion.Text : "";
                lHistorialmedico.Examenfisico = txtExamenFisico.Text != "" ? txtExamenFisico.Text : "";
                lHistorialmedico.Antecedentesimportancia = txtAntecedentes.Text != "" ? txtAntecedentes.Text : "";
                lHistorialmedico.Codservicio = (int)cmbServicio.SelectedValue;
                lHistorialmedico.Codestado = 1;
                lHistorialmedico.Pesotalla = txtPesoTalla.Text != "" ? txtPesoTalla.Text : "";

                ControlDePacientes.Dal.HistorialMedico lHistorialMedicos = new HistorialMedico();
                lHistorialMedicos.AddHistorialMedicoUpdExamenFisico(lHistorialmedico);


                txtMotivoConsulta.IsEnabled = false;
                //cmbDiagnosticoInicial.IsEnabled = false;
                cmbServicio.IsEnabled = false;
                txtPA.IsEnabled = false;
                txtFR.IsEnabled = false;
                txtRX.IsEnabled = false;
                txtFR.IsEnabled = false;
                txtTemp.IsEnabled = false;
                txtCC.IsEnabled = false;
                txtPeso.IsEnabled = false;
                txtTalla.IsEnabled = false;
                txtMC.IsEnabled = false;
                txtTE.IsEnabled = false;
                txtPE.IsEnabled = false;
                txtSaturacion.IsEnabled = false;
                txtExamenFisico.IsEnabled = false;
                txtAntecedentes.IsEnabled = false;
                txtFC.IsEnabled = false;
                txtPesoTalla.IsEnabled = false;

            }
            catch (Exception ex)
            {

                ControlOperation.alertWarning("" + ex);
            }


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CleanControls();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (mIdHistorialMedico != 0)
            {
                if (ControlOperation.alertConfirm("Desea Eliminar el Exámen Físico del paciente?") == MessageBoxResult.Yes)
                {
                    ControlDePacientes.Dal.HistorialMedico lHistorialMedico = new HistorialMedico();
                    lHistorialMedico.DeleteHistorialMedico(mIdHistorialMedico);

                    ControlOperation.alertInformation("El Exámen Físico se eliminó con éxito");
                    CleanControls();

                }
            }
        }

        private void CleanControls()
        {
            txtIdPaciente.IsEnabled = true;
            txtIdPaciente.Text = "";
            txtNombrePaciente.IsEnabled = true;
            txtNombrePaciente.Text = "";
            txtApellidoPaciente.IsEnabled = true;
            txtApellidoPaciente.Text = "";
            txtSexo.IsEnabled = true;
            txtSexo.Text = "";
            //txtEdad.IsEnabled = true;
            //txtEdad.Text = "";
            txtNombrePadre.IsEnabled = true;
            txtNombrePadre.Text = "";
            txtNombreMadre.IsEnabled = true;
            txtNombreMadre.Text = "";
            txtDireccion.IsEnabled = true;
            txtDireccion.Text = "";
            imgPaciente.Source = new BitmapImage(new Uri("/ControlDePacientes;component/Images/Attach.png", UriKind.Relative));
            lblEdad.Content = "";
            txtMotivoConsulta.Text = "";
            // cmbDiagnosticoInicial.SelectedIndex = 0;
            cmbServicio.SelectedIndex = 0;
            txtPA.Text = "";
            txtFR.Text = "";
            txtRX.Text = "";
            txtFR.Text = "";
            txtTemp.Text = "";
            txtCC.Text = "";
            txtPeso.Text = "";
            txtTalla.Text = "";
            txtMC.Text = "";
            txtTE.Text = "";
            txtPE.Text = "";
            txtSaturacion.Text = "";
            txtExamenFisico.Text = "";
            txtAntecedentes.Text = "";
            txtFC.Text = "";
            expExamenFisico.IsEnabled = false;
            expExamenFisico.IsExpanded = false;
            wrpButtons.Visibility = Visibility.Collapsed;
        }

        private void fillData()
        {
            DataTable lDtPaciente = new DataTable();
            ControlDePacientes.Dal.Paciente lPaciente = new Paciente();
            //lDtPaciente = lPaciente.GetPacienteById(Convert.ToInt32(txtIdPaciente.Text));
            lDtPaciente = lPaciente.GetPacienteByRegistroMedico(txtIdPaciente.Text);

            if (lDtPaciente.Rows.Count > 0)
            {

                mIdPaciente = (int)lDtPaciente.Rows[0]["IdPaciente"];

                DataTable lDtVerifyHistorial = new DataTable();
                ControlDePacientes.Dal.HistorialMedico lHistorialMedicoVerify = new HistorialMedico();
                lDtVerifyHistorial = lHistorialMedicoVerify.HistorialMedicoVerify(mIdPaciente, 10); //10 es Iniciado

                if (lDtVerifyHistorial.Rows.Count > 0)
                {

                    txtIdPaciente.IsEnabled = false;
                    txtNombrePaciente.Text = (string)lDtPaciente.Rows[0]["Nombre"];
                    txtNombrePaciente.IsEnabled = false;
                    txtApellidoPaciente.Text = lDtPaciente.Rows[0]["Apellido"].ToString() != "" || lDtPaciente.Rows[0]["SegundoApellido"].ToString() != "" ? (string)lDtPaciente.Rows[0]["Apellido"] + " " + (string)lDtPaciente.Rows[0]["SegundoApellido"] : "";
                    txtApellidoPaciente.IsEnabled = false;
                    txtSexo.Text = lDtPaciente.Rows[0]["SexoName"].ToString() != "" ? (string)lDtPaciente.Rows[0]["SexoName"] : "";
                    txtSexo.IsEnabled = false;
                    if (lDtPaciente.Rows[0]["FechaNacimiento"].ToString() != "")
                    {
                        DateDifference dateDifference = new DateDifference(DateTime.Parse(Convert.ToString(lDtPaciente.Rows[0]["FechaNacimiento"])), DateTime.Now.Date);
                        lblEdad.Content = dateDifference.ToString();
                    }
                    else lblEdad.Content = "";

                    txtNombrePadre.Text = lDtPaciente.Rows[0]["NombrePadre"].ToString() != "" ? (string)lDtPaciente.Rows[0]["NombrePadre"] : "";
                    txtNombrePadre.IsEnabled = false;
                    txtNombreMadre.Text = lDtPaciente.Rows[0]["NombreMadre"].ToString() != "" ? (string)lDtPaciente.Rows[0]["NombreMadre"] : "";
                    txtNombreMadre.IsEnabled = false;
                    txtDireccion.Text = lDtPaciente.Rows[0]["Direccion"].ToString() != "" ? (string)lDtPaciente.Rows[0]["Direccion"] : "";
                    txtDireccion.IsEnabled = false;


                    if (lDtPaciente.Rows[0]["FotoPath"] == DBNull.Value || (string) lDtPaciente.Rows[0]["FotoPath"] == "")
                    {}
                    else ControlOperation.LoadImageFromDB((byte[])lDtPaciente.Rows[0]["FotoPaciente"], imgPaciente);
                        
                     //   imgPaciente.Source = new BitmapImage(new Uri((string)lDtPaciente.Rows[0]["FotoPath"]));

                    expExamenFisico.IsEnabled = true;
                    expExamenFisico.IsExpanded = true;
                    wrpButtons.Visibility = Visibility.Visible;

                    //lleno el examen fisico
                    txtMotivoConsulta.Text = lDtVerifyHistorial.Rows[0]["MotivoConsulta"].ToString() != "" ? (string)lDtVerifyHistorial.Rows[0]["MotivoConsulta"] : "";
                    txtPA.Text = lDtVerifyHistorial.Rows[0]["PresionArterial"].ToString() != "" ? Convert.ToString(lDtVerifyHistorial.Rows[0]["PresionArterial"]) : "";
                    if (lDtVerifyHistorial.Rows[0]["CodServicio"].ToString() != "")
                        cmbServicio.SelectedValue = (int)lDtVerifyHistorial.Rows[0]["CodServicio"];
                    else cmbServicio.SelectedIndex = 0;
                    txtFC.Text = lDtVerifyHistorial.Rows[0]["FrecuenciaCardiaca"].ToString() != "" ? Convert.ToString(lDtVerifyHistorial.Rows[0]["FrecuenciaCardiaca"]) : "";
                    txtRX.Text = lDtVerifyHistorial.Rows[0]["RayosX"].ToString() != "" ? Convert.ToString(lDtVerifyHistorial.Rows[0]["RayosX"]) : "";
                    txtFR.Text = lDtVerifyHistorial.Rows[0]["FrecuenciaRespiratora"].ToString() != "" ? Convert.ToString(lDtVerifyHistorial.Rows[0]["FrecuenciaRespiratora"]) : "";
                    txtTemp.Text = lDtVerifyHistorial.Rows[0]["Temperatura"].ToString() != "" ? Convert.ToString(lDtVerifyHistorial.Rows[0]["Temperatura"]) : "";
                    txtCC.Text = lDtVerifyHistorial.Rows[0]["CC"].ToString() != "" ? Convert.ToString(lDtVerifyHistorial.Rows[0]["CC"]) : "";
                    txtPeso.Text = lDtVerifyHistorial.Rows[0]["Peso"].ToString() != "" ? Convert.ToString(lDtVerifyHistorial.Rows[0]["Peso"]) : "";
                    txtTalla.Text = lDtVerifyHistorial.Rows[0]["Talla"].ToString() != "" ? Convert.ToString(lDtVerifyHistorial.Rows[0]["Talla"]) : "";
                    txtMC.Text = lDtVerifyHistorial.Rows[0]["MasaCorporal"].ToString() != "" ? Convert.ToString(lDtVerifyHistorial.Rows[0]["MasaCorporal"]) : "";
                    txtTE.Text = lDtVerifyHistorial.Rows[0]["TE"].ToString() != "" ? Convert.ToString(lDtVerifyHistorial.Rows[0]["TE"]) : "";
                    txtPE.Text = lDtVerifyHistorial.Rows[0]["PE"].ToString() != "" ? Convert.ToString(lDtVerifyHistorial.Rows[0]["PE"]) : "";
                    txtPesoTalla.Text = lDtVerifyHistorial.Rows[0]["PesoTalla"].ToString() != "" ? Convert.ToString(lDtVerifyHistorial.Rows[0]["PesoTalla"]) : "";
                    txtSaturacion.Text = lDtVerifyHistorial.Rows[0]["SaturacionO2"].ToString() != "" ? Convert.ToString(lDtVerifyHistorial.Rows[0]["SaturacionO2"]) : "";
                    txtExamenFisico.Text = lDtVerifyHistorial.Rows[0]["ExamenFisico"].ToString() != "" ? (string)lDtVerifyHistorial.Rows[0]["ExamenFisico"] : "";
                    txtAntecedentes.Text = lDtVerifyHistorial.Rows[0]["AntecedentesImportancia"].ToString() != "" ? (string)lDtVerifyHistorial.Rows[0]["AntecedentesImportancia"] : "";



                }
                else
                    ControlOperation.alertWarning("El paciente no tiene un historial nuevo, debe registrarse en Admisión");



            }

        }



        public int CalcularEdad(DateTime birthDate)
        {


            int age = DateTime.Now.Year - birthDate.Year;

            if (DateTime.Now.Month < birthDate.Month || (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day))
                age--;

            return age;


        }


    }
}
