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
//using ControlDePacientesReport;
using DurationCalculatorApp;
using ControlDePacientes.Logic;



namespace ControlDePacientes
{
    /// <summary>
    /// Interaction logic for frmHistorialMedicoPrint.xaml
    /// </summary>
    public partial class frmHistorialMedicoPrint : Page
    {
        public frmHistorialMedicoPrint()
        {
            InitializeComponent();
        }

        public string mRegistroMedico;
        public int mIdHistorialMedico;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            expSearch.IsExpanded = true;

            expDetail.IsExpanded = false;
            expDetail.IsEnabled = false;



        }

        private void txtIdPaciente_KeyDown(object sender, KeyEventArgs e)
        {

            //if (e.Key == Key.Return)
            //{
            //    try
            //    {
            //        fillCmbDiagnosticoEntrada();
            //        fillCmbServicio();
            //        fillDestinoCaso();
            //        mIdHistorialMedico = Convert.ToInt32(txtIdPaciente.Text);
            //        fillData();
            //        fillDataExamenFisico(mIdHistorialMedico);
            //        fillDoctorsInfo(mIdHistorialMedico);

            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show("Error:--> " + ex, "info");
            //    }
            //}
        }

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

        private void fillData()
        {
            DataTable lDtHistorialMedico = new DataTable();
            ControlDePacientes.Dal.HistorialMedico lHistorialMedico = new HistorialMedico();

            lDtHistorialMedico = lHistorialMedico.HistorialMedicoGetAllForPrint(mIdHistorialMedico);

            if (lDtHistorialMedico.Rows.Count > 0)
            {
                if (lDtHistorialMedico.Rows[0]["Nombre"].ToString() != "")
                    txtNombrePaciente.Text = (string)lDtHistorialMedico.Rows[0]["Nombre"];
                else txtNombrePaciente.Text = "";


                if (lDtHistorialMedico.Rows[0]["Apellido"].ToString() != "" || lDtHistorialMedico.Rows[0]["SegundoApellido"].ToString() != "")
                    txtApellidoPaciente.Text = (string)lDtHistorialMedico.Rows[0]["Apellido"] + " " + (string)lDtHistorialMedico.Rows[0]["SegundoApellido"];
                else txtApellidoPaciente.Text = "";
                txtApellidoPaciente.IsEnabled = false;
                if (lDtHistorialMedico.Rows[0]["SexoName"].ToString() != "")
                    txtSexo.Text = (string)lDtHistorialMedico.Rows[0]["SexoName"];
                else txtSexo.Text = "";
                txtSexo.IsEnabled = false;
                //lblEdad.Content = Convert.ToString( CalcularEdad(DateTime.Parse(Convert.ToString(lDtPaciente.Rows[0]["FechaNacimiento"]))));
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


                // lblDate.Content = (string)lDtPaciente.Rows[0]["FECHA"];

                wrpButtons.Visibility = Visibility.Visible;

                txtMotivoConsulta.Text = lDtHistorialMedico.Rows[0]["MotivoConsulta"].ToString() != "" ? (string)lDtHistorialMedico.Rows[0]["MotivoConsulta"] : "";
                txtMotivoConsulta.IsEnabled = false;

                txtPA.Text = lDtHistorialMedico.Rows[0]["PresionArterial"].ToString() != "" ? Convert.ToString(lDtHistorialMedico.Rows[0]["PresionArterial"]) : "";
                txtPA.IsEnabled = false;

                if (lDtHistorialMedico.Rows[0]["CodServicio"].ToString() != "")//(int) lDtPaciente.Rows[0]["CodServicio"] != 0)
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

                if (lDtHistorialMedico.Rows[0]["EstudiosIniciales"].ToString() != "")
                    txtEstudioInicial.Text = (string)lDtHistorialMedico.Rows[0]["EstudiosIniciales"];
                txtEstudioInicial.IsEnabled = false;
                if (lDtHistorialMedico.Rows[0]["EstadoNutricinal"].ToString() != "")
                    txtEstadoNutricional.Text = (string)lDtHistorialMedico.Rows[0]["EstadoNutricinal"];
                txtEstadoNutricional.IsEnabled = false;
                if (lDtHistorialMedico.Rows[0]["ImpresionClinica"].ToString() != "")
                    txtImpresion.Text = (string)lDtHistorialMedico.Rows[0]["ImpresionClinica"];
                txtImpresion.IsEnabled = false;
                if (lDtHistorialMedico.Rows[0]["TratamientoConsultaExterna"].ToString() != "")
                    txtTratamiento.Text = (string)lDtHistorialMedico.Rows[0]["TratamientoConsultaExterna"];
                txtTratamiento.IsEnabled = false;
                if (lDtHistorialMedico.Rows[0]["Observaciones"].ToString() != "")
                    txtObservaciones.Text = (string)lDtHistorialMedico.Rows[0]["Observaciones"];
                txtObservaciones.IsEnabled = false;
                if (lDtHistorialMedico.Rows[0]["NotasDescargo"].ToString() == "")
                    txtNotaDescargo.Text = "";
                else
                    txtNotaDescargo.Text = (string)lDtHistorialMedico.Rows[0]["NotasDescargo"];
                txtNotaDescargo.IsEnabled = false;


                if (lDtHistorialMedico.Rows[0]["DxEgreso"].ToString() != "")
                    cmbDiagnosticoFinal.SelectedValue = (int)lDtHistorialMedico.Rows[0]["DxEgreso"];
                else cmbDiagnosticoFinal.SelectedIndex = 0;
                cmbDiagnosticoFinal.IsEnabled = false;

                if (lDtHistorialMedico.Rows[0]["DxIngreso"].ToString() != "")
                    cmbDiagnosticoInicial.SelectedValue = (int)lDtHistorialMedico.Rows[0]["DxIngreso"];
                else cmbDiagnosticoInicial.SelectedIndex = 0;
                cmbDiagnosticoInicial.IsEnabled = false;

                if (lDtHistorialMedico.Rows[0]["CodDestinoCaso"].ToString() != "")
                    cmbDestinoCaso.SelectedValue = (int)lDtHistorialMedico.Rows[0]["CodDestinoCaso"];
                else cmbDestinoCaso.SelectedIndex = 0;
                cmbDestinoCaso.IsEnabled = false;

                txtIdPaciente.IsEnabled = false;
                txtIdPaciente.Text = txtIdPacienteSearch.Text;
                txtNombrePaciente.IsEnabled = false;
                txtApellidoPaciente.IsEnabled = false;
                txtSexo.IsEnabled = false;
                txtNombrePadre.IsEnabled = false;
                txtNombreMadre.IsEnabled = false;
                txtDireccion.IsEnabled = false;

                txtNombrePaciente.IsEnabled = false;

            }
            else
            {
                ControlOperation.alertWarning("El Paciente no tiene Historial");
            }





            //DataTable lDtPaciente = new DataTable();
            //ControlDePacientes.Dal.Paciente lPaciente = new Paciente();

            //lDtPaciente = lPaciente.GetPacienteByRegistroMedico(txtIdPacienteSearch.Text);

            //if (lDtPaciente.Rows.Count > 0)
            //{
            //    txtIdPaciente.IsEnabled = false;
            //    txtIdPaciente.Text = txtIdPacienteSearch.Text;
            //    txtNombrePaciente.IsEnabled = false;
            //    txtApellidoPaciente.IsEnabled = false;
            //    txtSexo.IsEnabled = false;
            //    txtNombrePadre.IsEnabled = false;
            //    txtNombreMadre.IsEnabled = false;
            //    txtDireccion.IsEnabled = false;

            //txtNombrePaciente.IsEnabled = false;
            //if (lDtPaciente.Rows[0]["Apellido"].ToString() != "" || lDtPaciente.Rows[0]["SegundoApellido"].ToString() != "")
            //    txtApellidoPaciente.Text = (string)lDtPaciente.Rows[0]["Apellido"] + " " + (string)lDtPaciente.Rows[0]["SegundoApellido"];
            //else txtApellidoPaciente.Text = "";
            //txtApellidoPaciente.IsEnabled = false;
            //if (lDtPaciente.Rows[0]["SexoName"].ToString() != "")
            //    txtSexo.Text = (string)lDtPaciente.Rows[0]["SexoName"];
            //else txtSexo.Text = "";
            //txtSexo.IsEnabled = false;
            ////lblEdad.Content = Convert.ToString( CalcularEdad(DateTime.Parse(Convert.ToString(lDtPaciente.Rows[0]["FechaNacimiento"]))));
            //if (lDtPaciente.Rows[0]["FechaNacimiento"].ToString() != "")
            //{
            //    DateDifference dateDifference =
            //        new DateDifference(DateTime.Parse(Convert.ToString(lDtPaciente.Rows[0]["FechaNacimiento"])),
            //                           DateTime.Now.Date);
            //    lblEdad.Content = dateDifference.ToString();
            //}
            //else lblEdad.Content = "";

            //if (lDtPaciente.Rows[0]["NombrePadre"].ToString() != "")
            //    txtNombrePadre.Text = (string)lDtPaciente.Rows[0]["NombrePadre"];
            //else txtNombrePadre.Text = "";
            //txtNombrePadre.IsEnabled = false;
            //if (lDtPaciente.Rows[0]["NombreMadre"].ToString() != "")
            //    txtNombreMadre.Text = (string)lDtPaciente.Rows[0]["NombreMadre"];
            //else txtNombreMadre.Text = "";
            //txtNombreMadre.IsEnabled = false;
            //if (lDtPaciente.Rows[0]["Direccion"].ToString() != "")
            //    txtDireccion.Text = (string)lDtPaciente.Rows[0]["Direccion"]; //unir con municipio y departamento
            //else txtDireccion.Text = "";
            //txtDireccion.IsEnabled = false;
            //if (lDtPaciente.Rows[0]["FotoPath"] == DBNull.Value || (string)lDtPaciente.Rows[0]["FotoPath"] == "")
            //{ }
            //else imgPaciente.Source = new BitmapImage(new Uri((string)lDtPaciente.Rows[0]["FotoPath"]));


            //// lblDate.Content = (string)lDtPaciente.Rows[0]["FECHA"];

            //wrpButtons.Visibility = Visibility.Visible;

            // }
        }

        //private void fillDataExamenFisico(int pIdHistorialFisico)
        //{

        //    //fillCmbDiagnosticoSalida();
        //    ////Filldesstino.....
        //    //ControlDePacientes.Dal.HistorialMedico lHistorialMedico = new HistorialMedico();
        //    //DataTable lDtPaciente = new DataTable();
        //    //lDtPaciente = lHistorialMedico.HistorialMedicoGetAll(mIdHistorialMedico);

        //    //txtMotivoConsulta.Text = lDtPaciente.Rows[0]["MotivoConsulta"].ToString() != "" ? (string)lDtPaciente.Rows[0]["MotivoConsulta"] : "";
        //    //txtMotivoConsulta.IsEnabled = false;

        //    //txtPA.Text = lDtPaciente.Rows[0]["PresionArterial"].ToString() != "" ? Convert.ToString(lDtPaciente.Rows[0]["PresionArterial"]) : "";
        //    //txtPA.IsEnabled = false;

        //    //if (lDtPaciente.Rows[0]["CodServicio"].ToString() != "")//(int) lDtPaciente.Rows[0]["CodServicio"] != 0)
        //    //    cmbServicio.SelectedValue = (int) lDtPaciente.Rows[0]["CodServicio"];
        //    //else cmbServicio.SelectedIndex = 0;
        //    //cmbServicio.IsEnabled = false;

        //    //txtFC.Text = lDtPaciente.Rows[0]["FrecuenciaCardiaca"].ToString() != "" ? Convert.ToString(lDtPaciente.Rows[0]["FrecuenciaCardiaca"]) : "";
        //    //txtFC.IsEnabled = false;
        //    //txtRX.Text = lDtPaciente.Rows[0]["RayosX"].ToString() != "" ? Convert.ToString(lDtPaciente.Rows[0]["RayosX"]) : "";
        //    //txtRX.IsEnabled = false;
        //    //txtFR.Text = lDtPaciente.Rows[0]["FrecuenciaRespiratora"].ToString() != "" ? Convert.ToString(lDtPaciente.Rows[0]["FrecuenciaRespiratora"]) : "";
        //    //txtFR.IsEnabled = false;
        //    //txtTemp.Text = lDtPaciente.Rows[0]["Temperatura"].ToString() != "" ? Convert.ToString(lDtPaciente.Rows[0]["Temperatura"]) : "";
        //    //txtTemp.IsEnabled = false;
        //    //txtCC.Text = lDtPaciente.Rows[0]["CC"].ToString() != "" ? Convert.ToString(lDtPaciente.Rows[0]["CC"]) : "";
        //    //txtCC.IsEnabled = false;
        //    //txtPeso.Text = lDtPaciente.Rows[0]["Peso"].ToString() != "" ? Convert.ToString(lDtPaciente.Rows[0]["Peso"]) : "";
        //    //txtPeso.IsEnabled = false;
        //    //txtTalla.Text = lDtPaciente.Rows[0]["Talla"].ToString() != "" ? Convert.ToString(lDtPaciente.Rows[0]["Talla"]) : "";
        //    //txtTalla.IsEnabled = false;
        //    //txtMC.Text = lDtPaciente.Rows[0]["MasaCorporal"].ToString() != "" ? Convert.ToString(lDtPaciente.Rows[0]["MasaCorporal"]) : "";
        //    //txtMC.IsEnabled = false;
        //    //txtTE.Text = lDtPaciente.Rows[0]["TE"].ToString() != "" ? Convert.ToString(lDtPaciente.Rows[0]["TE"]) : "";
        //    //txtTE.IsEnabled = false;
        //    //txtPE.Text = lDtPaciente.Rows[0]["PE"].ToString() != "" ? Convert.ToString(lDtPaciente.Rows[0]["PE"]) : "";
        //    //txtPE.IsEnabled = false;
        //    //txtSaturacion.Text = lDtPaciente.Rows[0]["SaturacionO2"].ToString() != "" ? Convert.ToString(lDtPaciente.Rows[0]["SaturacionO2"]) : "";
        //    //txtSaturacion.IsEnabled = false;
        //    //txtExamenFisico.Text = lDtPaciente.Rows[0]["ExamenFisico"].ToString() != "" ? (string)lDtPaciente.Rows[0]["ExamenFisico"] : "";
        //    //txtExamenFisico.IsEnabled = false;
        //    //txtAntecedentes.Text = lDtPaciente.Rows[0]["AntecedentesImportancia"].ToString() != "" ? (string)lDtPaciente.Rows[0]["AntecedentesImportancia"] : "";

        //    //txtAntecedentes.IsEnabled = false;



        //}

        //private void fillDoctorsInfo(int pIdHistorialMedico)
        //{
        //    //ControlDePacientes.Dal.HistorialMedico lHistorialMedico = new HistorialMedico();
        //    //DataTable lDtPaciente = new DataTable();
        //    //lDtPaciente = lHistorialMedico.GetDoctorsNotes(mIdHistorialMedico);

        //    //if (lDtPaciente.Rows[0]["EstudiosIniciales"].ToString() != "")
        //    //txtEstudioInicial.Text = (string)lDtPaciente.Rows[0]["EstudiosIniciales"];
        //    //txtEstudioInicial.IsEnabled = false;
        //    //if (lDtPaciente.Rows[0]["EstadoNutricinal"].ToString() != "")
        //    //txtEstadoNutricional.Text = (string)lDtPaciente.Rows[0]["EstadoNutricinal"];
        //    //txtEstadoNutricional.IsEnabled = false;
        //    //if (lDtPaciente.Rows[0]["ImpresionClinica"].ToString() != "")
        //    //txtImpresion.Text = (string)lDtPaciente.Rows[0]["ImpresionClinica"];
        //    //txtImpresion.IsEnabled = false;
        //    //if (lDtPaciente.Rows[0]["TratamientoConsultaExterna"].ToString() != "")
        //    //txtTratamiento.Text = (string)lDtPaciente.Rows[0]["TratamientoConsultaExterna"];
        //    //txtTratamiento.IsEnabled = false;
        //    //if (lDtPaciente.Rows[0]["Observaciones"].ToString() != "")
        //    //txtObservaciones.Text = (string)lDtPaciente.Rows[0]["Observaciones"];
        //    //txtObservaciones.IsEnabled = false;
        //    //if (lDtPaciente.Rows[0]["NotasDescargo"].ToString() == "")
        //    //    txtNotaDescargo.Text = "";
        //    //else
        //    //    txtNotaDescargo.Text = (string)lDtPaciente.Rows[0]["NotasDescargo"];
        //    //txtNotaDescargo.IsEnabled = false;


        //    //if (lDtPaciente.Rows[0]["DxEgreso"].ToString()!= "")
        //    //    cmbDiagnosticoFinal.SelectedValue = (int)lDtPaciente.Rows[0]["DxEgreso"];
        //    //else cmbDiagnosticoFinal.SelectedIndex = 0;
        //    //cmbDiagnosticoFinal.IsEnabled = false;

        //    //if (lDtPaciente.Rows[0]["DxIngreso"].ToString() != "")
        //    //    cmbDiagnosticoInicial.SelectedValue = (int)lDtPaciente.Rows[0]["DxIngreso"];
        //    //else cmbDiagnosticoInicial.SelectedIndex = 0;
        //    //cmbDiagnosticoInicial.IsEnabled = false;

        //    //if (lDtPaciente.Rows[0]["CodDestinoCaso"].ToString() != "")
        //    //cmbDestinoCaso.SelectedValue = (int)lDtPaciente.Rows[0]["CodDestinoCaso"];
        //    //else cmbDestinoCaso.SelectedIndex = 0;
        //    //cmbDestinoCaso.IsEnabled = false;

        //}

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            //ControlDePacientesReport.Form1 lForm1 = new Form1();
            //lForm1.mIdHistorialMedico = mIdHistorialMedico;
            //lForm1.ShowDialog();
            //LoopVisualTree(expDetail);
            //imgPaciente.Source = new BitmapImage(new Uri("/ControlDePacientes;component/Images/nophoto.png", UriKind.Relative));
            //dtgRegistroMedico.ItemsSource = null;
            //expDetail.IsExpanded = false;

            //ControlDePacientesReport.Form2 lForm1 = new Form2();
            //lForm1.mIdHistorialMedico = mIdHistorialMedico;
            //lForm1.ShowDialog();
            //LoopVisualTree(expDetail);
            //imgPaciente.Source = new BitmapImage(new Uri("/ControlDePacientes;component/Images/nophoto.png", UriKind.Relative));
            //dtgRegistroMedico.ItemsSource = null;
            //expDetail.IsExpanded = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            expDetail.IsExpanded = false;
            LoopVisualTree(expDetail);
            imgPaciente.Source = new BitmapImage(new Uri("/ControlDePacientes;component/Images/nophoto.png", UriKind.Relative));
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoopVisualTree(expDetail);
           
            DataTable lDtRegistroMedico = new DataTable();

            ControlDePacientes.Dal.HistorialMedico lHistorialMedico = new HistorialMedico();
            lDtRegistroMedico = lHistorialMedico.HistorialMedicoByIdPaciente(txtIdPacienteSearch.Text);
            dtgRegistroMedico.ItemsSource = lDtRegistroMedico.DefaultView;
            //expDetail.IsExpanded = false;
        }

        private void dtgRegistroMedico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            expDetail.IsExpanded = true;
            //SELECCIONA LA FILA
            DataRowView lCurrentDataRowView = ((DataRowView)((DataGrid)sender).SelectedItem);

            expDetail.IsEnabled = true;
            expDetail.IsExpanded = true;

            LoopVisualTree(expDetail);



            if (lCurrentDataRowView == null)
                return;
            fillCmbDiagnosticoEntrada();
            fillCmbDiagnosticoSalida();
            fillCmbServicio();
            fillDestinoCaso();
            mRegistroMedico = lCurrentDataRowView.Row["RegistroMedico"].ToString();
            mIdHistorialMedico = (int)lCurrentDataRowView.Row["IdHistorialMedico"];
            fillData();
            //fillDataExamenFisico(mIdHistorialMedico);
            //fillDoctorsInfo(mIdHistorialMedico);


        }



        void LoopVisualTree(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                if (obj is TextBox)
                    ((TextBox)obj).Text = null;
                if (obj is ComboBox)
                {
                    ((ComboBox)obj).SelectedIndex = 0;
                    ((ComboBox)obj).ItemsSource = null;
                }
                LoopVisualTree(VisualTreeHelper.GetChild(obj, i));
            }
        }

        private void txtIdPacienteSearch_KeyDown(object sender, KeyEventArgs e)
        {
            LoopVisualTree(expDetail);

            DataTable lDtRegistroMedico = new DataTable();

            ControlDePacientes.Dal.HistorialMedico lHistorialMedico = new HistorialMedico();
            lDtRegistroMedico = lHistorialMedico.HistorialMedicoByIdPaciente(txtIdPacienteSearch.Text);
            dtgRegistroMedico.ItemsSource = lDtRegistroMedico.DefaultView;
        }

    }
}
