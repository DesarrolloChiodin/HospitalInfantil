using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
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
using Microsoft.Win32;
using WinFormCharpWebCam;
using Image = System.Drawing.Image;


namespace ControlDePacientes
{
    /// <summary>
    /// Regitro de Pacientes y generacion de Historial Médico
    /// </summary>
    public partial class frmRegistroPaciente : Page
    {
        #region Public Members

        public ControlDePacientes.Entities.paciente lPaciente { get; set; }
        public bool isEdit;
        public int mIdPaciente;
        public string mRegistroMedico;
        public string mFileName = "";
        public bool mAgeBlock;
        public string mIdHistorial = "";
        public int mIdHistorialForDelete;
        public string mIdHistorialForPopup;

        private string mFotoPath;
        private string mFotoNombre;
        private byte[] mFotoPaciente;
        #endregion

        #region Constructor
        public frmRegistroPaciente()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Methods
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            fillComboBox();
            btnCancel.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Collapsed;
            txtRegistroMedico.Visibility = Visibility.Visible;
            lblRegistroMedico.Visibility = Visibility.Visible;


            if (isEdit)
            {
                mIdPaciente = lPaciente.Idpaciente;
                mIdHistorialForPopup = lPaciente.Registromedico;
                txtNombrePaciente.Text = lPaciente.Nombre;
                txtPrimerApellidoPaciente.Text = lPaciente.Apellido;
                txtSegundoApellidoPaciente.Text = lPaciente.Segundoapellido;
                cmbSexo.SelectedValue = lPaciente.Codsexo;


                if (lPaciente.Fechanacimiento == new DateTime())
                { }
                else
                {
                    dtpFechaNacimiento.SelectedDate = lPaciente.Fechanacimiento;
                    CalcularEdad(DateTime.Parse(dtpFechaNacimiento.Text));
                }

                CmbLugarNacimiento.SelectedValue = lPaciente.Lugarnacimiento;
                cmbAlergias.SelectedValue = lPaciente.Codalergias;
                cmbTipoSangre.SelectedValue = lPaciente.Codtiposangre;
                txtNombrePadre.Text = lPaciente.Nombrepadre;
                cmbOcuapcionPadre.SelectedValue = lPaciente.Codocupacionpadre;
                txtNombreMadre.Text = lPaciente.Nombremadre;
                cmbOcuapcionMadre.SelectedValue = lPaciente.Codocupacion;
                txtDireccion.Text = lPaciente.Direccion;

                FillDepartamento();

                if (lPaciente.Coddepartamento != 0)
                    cmbDepartamento.SelectedValue = lPaciente.Coddepartamento;
                else cmbDepartamento.SelectedIndex = 0;

                FillMunicipio(lPaciente.Coddepartamento);
                if (lPaciente.Codmunicipio != 0)
                    cmbMunicipio.SelectedValue = lPaciente.Codmunicipio;
                else cmbMunicipio.SelectedIndex = 0;

                FillPais();
                cmbPais.SelectedValue = lPaciente.Codpais;
                if ((int)cmbPais.SelectedValue != 26)//es disitno a guatemala
                {
                    cmbDepartamento.IsEnabled = false;
                    cmbMunicipio.IsEnabled = false;

                }


                FillGrupoEtnico();
                if (lPaciente.CodGrupoEtnico != 0)
                    cmbGrupoEtnico.SelectedValue = lPaciente.CodGrupoEtnico;
                else cmbGrupoEtnico.SelectedIndex = 0;

                fillCmbComunidadLinguistica(0);
                if (lPaciente.CodComunidadLinguistica != 0)
                    cmbComunidadLinguistica.SelectedValue = lPaciente.CodComunidadLinguistica;
                else cmbComunidadLinguistica.SelectedIndex = 0;

                fillCmbEscolaridad();
                if (lPaciente.CodEscolaridad != 0)
                    cmbEscolaridad.SelectedValue = lPaciente.CodEscolaridad;
                else cmbEscolaridad.SelectedIndex = 0;

                fillCmbDiscapacidad();
                if (lPaciente.CodDiscapacidad != 0)
                    cmbDiscapacidad.SelectedValue = lPaciente.CodDiscapacidad;
                else cmbDiscapacidad.SelectedIndex = 0;


                chkAgricolaMigrante.IsChecked = lPaciente.AgricolaMigrante;
                chkIgss.IsChecked = lPaciente.Igss;


                txtAldea.Text = lPaciente.Aldea;
                txtTelCasa.Text = lPaciente.Telefonocasa;
                txtTelEmergencia.Text = lPaciente.Telefonoemergencia;
                txtCelular.Text = lPaciente.Celularprincipal;
                txtCorreo.Text = lPaciente.Correoelectronico;
                txtObservaciones.Text = lPaciente.Observacion;
                txtPacienteId.Text = lPaciente.Registromedico;
                txtDpiPaciente.Text = lPaciente.PacienteDpi;
                txtDPIPapa.Text = lPaciente.PadreDpi;
                txtDPImama.Text = lPaciente.MadreDpi;


                //mFileName = lPaciente.Fotopath;
                mFotoPaciente = lPaciente.FotoPaciente;
                mFotoNombre = lPaciente.FotoNombre;
                //mFotoPath = lPaciente.Fotopath;

                if (lPaciente.FotoPaciente != null)
                    ControlDePacientes.Logic.ControlOperation.LoadImageFromDB(lPaciente.FotoPaciente, imgPaciente);

                if (mAgeBlock)
                {
                    CleanBlockControls(1);
                    wrpControls.Visibility = Visibility.Collapsed;
                }

                dtgLoadHistorialPaciente(lPaciente.Registromedico.ToString());

            }

        }


        public bool getImage(byte[] pFotoByte, System.Windows.Controls.Image pImageControl)
        {
            try
            {

                byte[] buffer = (byte[])pFotoByte;

                MemoryStream stream = new MemoryStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;

                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                pImageControl.Source = bi;

                return true;
            }
            catch { return false; }

        }

        public static BitmapImage ToWpfImage(System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();  // no using here! BitmapImage will dispose the stream after loading
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            BitmapImage ix = new BitmapImage();
            ix.BeginInit();
            ix.CacheOption = BitmapCacheOption.OnLoad;
            ix.StreamSource = ms;
            //ix.EndInit();
            return ix;
        }

        /// <summary>
        /// One-way converter from System.Drawing.Image to System.Windows.Media.ImageSource
        /// </summary>

        public object Convert(object value)
        {
            // empty images are empty...
            if (value == null) { return null; }

            var image = (System.Drawing.Image)value;
            // Winforms Image we want to get the WPF Image from...
            var bitmap = new System.Windows.Media.Imaging.BitmapImage();
            bitmap.BeginInit();
            MemoryStream memoryStream = new MemoryStream();
            // Save to a memory stream...
            image.Save(memoryStream, image.RawFormat);
            // Rewind the stream...
            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();
            return bitmap;
        }

        private void AddRowComboBox(DataTable pDt)
        {
            DataRow lDr = pDt.NewRow();
            lDr[0] = 0;
            lDr[1] = "-- Seleccione --";
            pDt.Rows.InsertAt(lDr, 0);
        }

        private void fillComboBox()
        {
            FillSexo();
            FillAlergias();
            FillTipoSangre();
            FillOcupacion();

            FillLugarNacimiento();
            FillPais();
            FillDepartamento();
            FillMunicipio(0);

            FillGrupoEtnico();
            fillCmbComunidadLinguistica(0);
            fillCmbEscolaridad();
            fillCmbDiscapacidad();




        }

        private void FillPais()
        {
            DataTable lDtPais = new DataTable();
            ControlDePacientes.Dal.Pais lPais = new Dal.Pais();
            lDtPais = lPais.GetPais();
            AddRowComboBox(lDtPais);
            cmbPais.ItemsSource = lDtPais.DefaultView;
            cmbPais.DisplayMemberPath = "PaisName";
            cmbPais.SelectedValuePath = "IdPais";
            cmbPais.SelectedValue = 26;
        }

        private void FillSexo()
        {
            DataTable lDtSexo = new DataTable();
            ControlDePacientes.Dal.Sexo lSexo = new Dal.Sexo();
            lDtSexo = lSexo.GetSexo();
            AddRowComboBox(lDtSexo);
            cmbSexo.ItemsSource = lDtSexo.DefaultView;
            cmbSexo.DisplayMemberPath = "SexoName";
            cmbSexo.SelectedValuePath = "IdSexo";
            cmbSexo.SelectedIndex = 0;


        }

        private void FillAlergias()
        {
            DataTable lDtAlergias = new DataTable();
            ControlDePacientes.Dal.Alergias lAlergias = new Alergias();
            lDtAlergias = lAlergias.GetAlergias();
            AddRowComboBox(lDtAlergias);

            cmbAlergias.ItemsSource = lDtAlergias.DefaultView;
            cmbAlergias.DisplayMemberPath = "AlergiasName";
            cmbAlergias.SelectedValuePath = "IdAlergias";
            cmbAlergias.SelectedIndex = 0;
        }

        private void FillTipoSangre()
        {
            DataTable lDtTipoSangre = new DataTable();
            ControlDePacientes.Dal.TipoSangre lTipoSangre = new TipoSangre();
            lDtTipoSangre = lTipoSangre.GetTipoSangre();

            AddRowComboBox(lDtTipoSangre);

            cmbTipoSangre.ItemsSource = lDtTipoSangre.DefaultView;
            cmbTipoSangre.DisplayMemberPath = "TipoSangreName";
            cmbTipoSangre.SelectedValuePath = "IdTipoSangre";
            cmbTipoSangre.SelectedIndex = 0;

        }

        private void FillOcupacion()
        {
            DataTable lDtOcupacion = new DataTable();
            ControlDePacientes.Dal.Ocupacion lOcupacion = new Ocupacion();
            lDtOcupacion = lOcupacion.GetOcupacion();

            AddRowComboBox(lDtOcupacion);

            cmbOcuapcionPadre.ItemsSource = cmbOcuapcionMadre.ItemsSource = lDtOcupacion.DefaultView;
            cmbOcuapcionPadre.DisplayMemberPath = cmbOcuapcionMadre.DisplayMemberPath = "OcupacionName";
            cmbOcuapcionPadre.SelectedValuePath = cmbOcuapcionMadre.SelectedValuePath = "IdOcupacion";
            cmbOcuapcionPadre.SelectedIndex = cmbOcuapcionMadre.SelectedIndex = 0;


            //cmbOcuapcionAcompanante.ItemsSource = cmbOcuapcionMadre.ItemsSource = lDtOcupacion.DefaultView;
            //cmbOcuapcionAcompanante.DisplayMemberPath = cmbOcuapcionMadre.DisplayMemberPath = "OcupacionName";
            //cmbOcuapcionAcompanante.SelectedValuePath = cmbOcuapcionMadre.SelectedValuePath = "IdOcupacion";
            //cmbOcuapcionAcompanante.SelectedIndex = cmbOcuapcionMadre.SelectedIndex = 0;

        }

        private void FillMunicipio(int pIdDepartamento)
        {


            DataTable lDtMunicipio = new DataTable();
            ControlDePacientes.Dal.Municipio lMunicipio = new Municipio();
            lDtMunicipio = lMunicipio.GetMunicipioByDepartamento(pIdDepartamento);

            AddRowComboBox(lDtMunicipio);

            cmbMunicipio.ItemsSource = lDtMunicipio.DefaultView;
            cmbMunicipio.DisplayMemberPath = "MunicipioName";
            cmbMunicipio.SelectedValuePath = "IdMunicipio";
            cmbMunicipio.SelectedIndex = 0;

        }

        private void FillLugarNacimiento()
        {
            DataTable lDtMunicipio = new DataTable();
            ControlDePacientes.Dal.Municipio lMunicipio = new Municipio();
            lDtMunicipio = lMunicipio.GetMunicipio();

            AddRowComboBox(lDtMunicipio);


            CmbLugarNacimiento.ItemsSource = lDtMunicipio.DefaultView;
            CmbLugarNacimiento.DisplayMemberPath = "MunicipioName";
            CmbLugarNacimiento.SelectedValuePath = "IdMunicipio";
            CmbLugarNacimiento.SelectedIndex = 0;
        }

        private void FillDepartamento()
        {
            DataTable lDtDepartamento = new DataTable();
            ControlDePacientes.Dal.Departamento lDepartamento = new Departamento();
            lDtDepartamento = lDepartamento.GetDepartamento();

            AddRowComboBox(lDtDepartamento);


            cmbDepartamento.ItemsSource = lDtDepartamento.DefaultView;
            cmbDepartamento.DisplayMemberPath = "DepartamentoName";
            cmbDepartamento.SelectedValuePath = "IdDepartamento";
            cmbDepartamento.SelectedIndex = 0;
        }

        private void FillGrupoEtnico()
        {
            DataTable lDtGrupoEtnico = new DataTable();
            ControlDePacientes.Dal.GrupoEtnico lGrupoEtnico = new Dal.GrupoEtnico();
            lDtGrupoEtnico = lGrupoEtnico.GetGrupoEtnico();
            AddRowComboBox(lDtGrupoEtnico);
            cmbGrupoEtnico.ItemsSource = lDtGrupoEtnico.DefaultView;
            cmbGrupoEtnico.DisplayMemberPath = "Descripcion";
            cmbGrupoEtnico.SelectedValuePath = "Idgrupoetnico";
            cmbGrupoEtnico.SelectedIndex = 0;
        }

        private void fillCmbComunidadLinguistica(int pGrupoEtnico)
        {

            DataTable lDtComunidad = new DataTable();
            ControlDePacientes.Dal.ComunidadLinguistica lComunidadLinguistica = new Dal.ComunidadLinguistica();

            if (pGrupoEtnico == 0)
            {
                cmbComunidadLinguistica.IsEnabled = true;
                lDtComunidad = lComunidadLinguistica.GetComunidadLinguisticaAll();
                AddRowComboBox(lDtComunidad);
                cmbComunidadLinguistica.ItemsSource = lDtComunidad.DefaultView;
                cmbComunidadLinguistica.DisplayMemberPath = "Descripcion";
                cmbComunidadLinguistica.SelectedValuePath = "IdComunidadLinguistica";
                cmbComunidadLinguistica.SelectedIndex = 0;
            }
            else
            {
                lDtComunidad = lComunidadLinguistica.GetComunidadLinguisticaByGrupoEtnico(pGrupoEtnico);
                AddRowComboBox(lDtComunidad);
                if (lDtComunidad.Rows.Count > 0)
                {

                    cmbComunidadLinguistica.ItemsSource = lDtComunidad.DefaultView;
                    cmbComunidadLinguistica.DisplayMemberPath = "Descripcion";
                    cmbComunidadLinguistica.SelectedValuePath = "IdComunidadLinguistica";
                    cmbComunidadLinguistica.SelectedIndex = 0;
                }
                else
                {
                    cmbComunidadLinguistica.SelectedIndex = 0;
                }
            }
        }

        private void fillCmbEscolaridad()
        {
            ControlDePacientes.Dal.Escolaridad lEscolaridad = new Dal.Escolaridad();
            DataTable lDtEscolaridad = lEscolaridad.GetEscolaridad();
            AddRowComboBox(lDtEscolaridad);
            cmbEscolaridad.ItemsSource = lDtEscolaridad.DefaultView;
            cmbEscolaridad.DisplayMemberPath = "Descripcion";
            cmbEscolaridad.SelectedValuePath = "IdEscolaridad";
            cmbEscolaridad.SelectedIndex = 0;
        }

        private void fillCmbDiscapacidad()
        {
            ControlDePacientes.Dal.Discapacidad lDiscapacidad = new Dal.Discapacidad();
            DataTable lDtDiscapacidad = lDiscapacidad.GetDiscapacidad();
            AddRowComboBox(lDtDiscapacidad);
            cmbDiscapacidad.ItemsSource = lDtDiscapacidad.DefaultView;
            cmbDiscapacidad.DisplayMemberPath = "Descripcion";
            cmbDiscapacidad.SelectedValuePath = "IdDiscapacidad";
            cmbDiscapacidad.SelectedIndex = 0;
        }

        private void cmbGrupoEtnico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (((ComboBox)sender).SelectedValue != null)
            //{
            //    fillCmbComunidadLinguistica((int)((ComboBox)sender).SelectedValue);
            //}

            if (cmbGrupoEtnico.SelectedValue != null)
                fillCmbComunidadLinguistica((int)(cmbGrupoEtnico.SelectedValue));
        }

        private void cmbPais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbPais.SelectedValue != null)
            {
                if ((int)cmbPais.SelectedValue != 26)
                {
                    cmbDepartamento.SelectedIndex = 0;
                    cmbMunicipio.SelectedIndex = 0;
                    cmbDepartamento.IsEnabled = false;
                    cmbMunicipio.IsEnabled = false;
                }
                else if ((int)cmbPais.SelectedValue == 26)
                {
                    cmbDepartamento.SelectedIndex = 0;
                    cmbMunicipio.SelectedIndex = 0;
                    cmbDepartamento.IsEnabled = true;
                    cmbMunicipio.IsEnabled = true;
                }
            }


        }

        private void cmbDepartamento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (!isEdit)
            if (cmbDepartamento.SelectedValue != null)
                FillMunicipio((int)cmbDepartamento.SelectedValue);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPacienteId.Text == "")
                {
                    string lStringValidation = Validation();
                    if (lStringValidation == "")
                    {


                        FillPaciente();

                        ControlDePacientes.Dal.Paciente lPacienteAdd = new Paciente();

                        mRegistroMedico = lPacienteAdd.AddPaciente(lPaciente);

                        txtPacienteId.Text = mRegistroMedico; //.ToString() + "-" + DateTime.Now.Year.ToString();

                        CleanBlockControls(1);

                        //SaveRegistroMedico();
                        DataTable lDtIdPaciente = lPacienteAdd.GetPacienteByRegistroMedico(mRegistroMedico);
                        mIdPaciente = (int)lDtIdPaciente.Rows[0]["IdPaciente"];

                        ControlOperation.alertInformation("Registro Médico agregado Exitosamente, porfavor copie el Registro Médico");

                        btnCancel.Visibility = Visibility.Collapsed;
                        btnDelete.Visibility = Visibility.Visible;


                    }
                    else
                    {
                        ControlOperation.alertWarning(lStringValidation);
                    }
                }
                else
                    if (isEdit)
                    {
                        FillPaciente();
                        ControlDePacientes.Dal.Paciente lPacienteAdd = new Paciente();
                        lPacienteAdd.UpdPaciente(lPaciente, mFotoPath);
                        CleanBlockControls(1);
                        ControlOperation.alertInformation("Registro Médico fué actulizado éxitosamente");
                        //SaveRegistroMedico();

                        btnCancel.Visibility = Visibility.Collapsed;
                        btnDelete.Visibility = Visibility.Visible;
                    }
                    else
                        ControlOperation.alertWarning("El Registro Médico ya exíste");
            }
            catch (Exception ex)
            {

                ControlOperation.alertWarning("" + ex);
            }
        }

        private void FillPaciente()
        {
            lPaciente = new paciente();
            if (isEdit)
                lPaciente.Idpaciente = mIdPaciente;

            //Cuenta los pacientes de la base de datos en total

            ControlDePacientes.Dal.Paciente lPacienteDal = new Paciente();
            int lTotalPacientes = (lPacienteDal.CountPaciente() + 1);
            lPaciente.Pacientename = "";
            lPaciente.Nombre = txtNombrePaciente.Text;
            lPaciente.Apellido = txtPrimerApellidoPaciente.Text;
            lPaciente.Segundoapellido = txtSegundoApellidoPaciente.Text;
            lPaciente.Codsexo = cmbSexo.SelectedIndex == 0 ? 0 : (int)cmbSexo.SelectedValue;
            lPaciente.Fechanacimiento = dtpFechaNacimiento.SelectedDate != null ? (DateTime)dtpFechaNacimiento.SelectedDate : new DateTime();
            lPaciente.Lugarnacimiento = CmbLugarNacimiento.SelectedIndex == 0 ? 0 : (int)CmbLugarNacimiento.SelectedValue;
            lPaciente.Codalergias = cmbAlergias.SelectedIndex == 0 ? 0 : (int)cmbAlergias.SelectedValue;
            lPaciente.Codtiposangre = cmbTipoSangre.SelectedIndex == 0 ? 0 : (int)cmbTipoSangre.SelectedValue;
            lPaciente.Nombrepadre = txtNombrePadre.Text == "" ? "" : txtNombrePadre.Text;
            lPaciente.Codocupacionpadre = cmbOcuapcionPadre.SelectedIndex == 0 ? 0 : (int)cmbOcuapcionPadre.SelectedValue;
            lPaciente.Nombremadre = txtNombreMadre.Text == "" ? "" : txtNombreMadre.Text;
            lPaciente.Codocupacion = cmbOcuapcionMadre.SelectedIndex == 0 ? 0 : (int)cmbOcuapcionMadre.SelectedValue;
            lPaciente.Direccion = txtDireccion.Text == "" ? "" : txtDireccion.Text;
            lPaciente.Coddepartamento = cmbDepartamento.SelectedIndex == 0 ? 0 : (int)cmbDepartamento.SelectedValue;
            lPaciente.Codmunicipio = cmbMunicipio.SelectedIndex == 0 ? 0 : (int)cmbMunicipio.SelectedValue;
            lPaciente.Aldea = txtAldea.Text == "" ? "" : txtAldea.Text;
            lPaciente.Telefonocasa = txtTelCasa.Text == "" ? "" : txtTelCasa.Text;
            lPaciente.Telefonoemergencia = txtTelEmergencia.Text == "" ? "" : txtTelEmergencia.Text;
            lPaciente.Celularprincipal = txtCelular.Text == "" ? "" : txtCelular.Text;
            lPaciente.Celularsecundario = txtCelular.Text == "" ? "" : txtCelular.Text;
            lPaciente.Correoelectronico = txtCorreo.Text == "" ? "" : txtCorreo.Text;
            lPaciente.Observacion = txtObservaciones.Text == "" ? "" : txtObservaciones.Text;
            lPaciente.Codpais = cmbPais.SelectedIndex == 0 ? 0 : (int)cmbPais.SelectedValue;
            lPaciente.Fotopath = mFileName; //mFotoPath;
            lPaciente.Registromedico = lTotalPacientes.ToString() + "-" + DateTime.Today.Year;
            lPaciente.Edad = lblEdad.Content.ToString();
            lPaciente.CodGrupoEtnico = cmbGrupoEtnico.SelectedIndex <= 0 ? 0 : (int)cmbGrupoEtnico.SelectedValue;
            lPaciente.CodComunidadLinguistica = cmbComunidadLinguistica.SelectedIndex <= 0 ? 0 : (int)cmbComunidadLinguistica.SelectedValue;
            lPaciente.CodEscolaridad = cmbEscolaridad.SelectedIndex == 0 ? 0 : (int)cmbEscolaridad.SelectedValue;
            lPaciente.CodDiscapacidad = cmbDiscapacidad.SelectedIndex == 0 ? 0 : (int)cmbDiscapacidad.SelectedValue;
            lPaciente.Igss = (bool)chkIgss.IsChecked.Value;
            lPaciente.AgricolaMigrante = (bool)chkAgricolaMigrante.IsChecked.Value;
            lPaciente.PacienteDpi = txtDpiPaciente.Text == "" ? "" : txtDpiPaciente.Text;
            lPaciente.PadreDpi = txtDPIPapa.Text == "" ? "" : txtDPIPapa.Text;
            lPaciente.MadreDpi = txtDPImama.Text == "" ? "" : txtDPImama.Text;
            // lPaciente.CodOcupacionAcompanante = cmbOcuapcionAcompanante.SelectedIndex == 0 ? 0 : (int)cmbOcuapcionAcompanante.SelectedValue;
            // lPaciente.NombreAcompanante = txtNombreAcompanante.Text == "" ? "" : txtNombreAcompanante.Text;


            if (mFileName != "")
            {
                string filePath = mFileName;   //A stream of bytes that represnts the binary file
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                //The reader reads the binary data from the file stream
                BinaryReader reader = new BinaryReader(fs);

                //Bytes from the binary reader stored in BlobValue array
                byte[] BlobValue = reader.ReadBytes((int)fs.Length);

                fs.Close();
                reader.Close();

                lPaciente.FotoPaciente = BlobValue;
            }







            //if (isEdit)
            //{
            //    if (mFotoPaciente == null)
            //    {
            //        if (mFotoPath != "")
            //        {
            //            string filePath = mFileName;   //A stream of bytes that represnts the binary file
            //            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            //            //The reader reads the binary data from the file stream
            //            BinaryReader reader = new BinaryReader(fs);

            //            //Bytes from the binary reader stored in BlobValue array
            //            byte[] BlobValue = reader.ReadBytes((int)fs.Length);

            //            fs.Close();
            //            reader.Close();

            //            lPaciente.FotoPaciente = BlobValue;
            //        }
            //    }
            //    else
            //    {
            //        lPaciente.FotoPaciente = mFotoPaciente;
            //    }
            //}

            //CONVIERTE FOTOGRAFIA

            //    string filePath = mFileName;
            //if (mFileName != "")
            //{

            //    //A stream of bytes that represnts the binary file
            //    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            //    //The reader reads the binary data from the file stream
            //    BinaryReader reader = new BinaryReader(fs);

            //    //Bytes from the binary reader stored in BlobValue array
            //    byte[] BlobValue = reader.ReadBytes((int) fs.Length);

            //    fs.Close();
            //    reader.Close();


            //    lPaciente.FotoPaciente = BlobValue;
            //}
            //else
            //{
            //    lPaciente.FotoPaciente = null;
            //}

        }

        private void CleanBlockControls(int pBlock)
        {
            if (pBlock == 1)
            {
                txtNombrePaciente.IsEnabled = false;
                txtPrimerApellidoPaciente.IsEnabled = false;
                txtSegundoApellidoPaciente.IsEnabled = false;
                cmbSexo.IsEnabled = false;
                cmbPais.IsEnabled = false;
                dtpFechaNacimiento.IsEnabled = false;
                CmbLugarNacimiento.IsEnabled = false;
                cmbAlergias.IsEnabled = false;
                cmbTipoSangre.IsEnabled = false;
                txtNombrePadre.IsEnabled = false;
                cmbOcuapcionPadre.IsEnabled = false;
                txtNombreMadre.IsEnabled = false;
                cmbOcuapcionMadre.IsEnabled = false;
                txtDireccion.IsEnabled = false;
                cmbDepartamento.IsEnabled = false;
                cmbMunicipio.IsEnabled = false;
                txtAldea.IsEnabled = false;
                txtTelCasa.IsEnabled = false;
                txtTelEmergencia.IsEnabled = false;
                txtCelular.IsEnabled = false;
                txtCorreo.IsEnabled = false;
                txtObservaciones.IsEnabled = false;
                txtDpiPaciente.IsEnabled = false;
                txtDPIPapa.IsEnabled = false;
                txtDPImama.IsEnabled = false;

                cmbGrupoEtnico.IsEnabled = false;
                cmbComunidadLinguistica.IsEnabled = false;
                cmbEscolaridad.IsEnabled = false;
                cmbDiscapacidad.IsEnabled = false;
                chkAgricolaMigrante.IsEnabled = false;
                chkIgss.IsEnabled = false;


            }
            else if (pBlock == 0)
            {
                txtNombrePaciente.IsEnabled = true;
                txtPrimerApellidoPaciente.IsEnabled = true;
                txtSegundoApellidoPaciente.IsEnabled = true;
                cmbSexo.IsEnabled = true;
                cmbPais.IsEnabled = true;
                dtpFechaNacimiento.IsEnabled = true;
                CmbLugarNacimiento.IsEnabled = true;
                cmbAlergias.IsEnabled = true;
                cmbTipoSangre.IsEnabled = true;
                txtNombrePadre.IsEnabled = true;
                cmbOcuapcionPadre.IsEnabled = true;
                txtNombreMadre.IsEnabled = true;
                cmbOcuapcionMadre.IsEnabled = true;
                txtDireccion.IsEnabled = true;
                cmbDepartamento.IsEnabled = true;
                cmbMunicipio.IsEnabled = true;
                txtAldea.IsEnabled = true;
                txtTelCasa.IsEnabled = true;
                txtTelEmergencia.IsEnabled = true;
                txtCelular.IsEnabled = true;
                txtCorreo.IsEnabled = true;
                txtObservaciones.IsEnabled = true;

                txtDpiPaciente.IsEnabled = true;
                txtDPIPapa.IsEnabled = true;
                txtDPImama.IsEnabled = true;

                cmbGrupoEtnico.IsEnabled = true;
                cmbComunidadLinguistica.IsEnabled = true;
                cmbEscolaridad.IsEnabled = true;
                cmbDiscapacidad.IsEnabled = true;
                chkAgricolaMigrante.IsEnabled = true;
                chkIgss.IsEnabled = true;
            }
            else if (pBlock == 2)
            {
                txtNombrePaciente.Text = "";
                txtPrimerApellidoPaciente.Text = "";
                txtSegundoApellidoPaciente.Text = "";
                cmbSexo.SelectedIndex = 0;
                cmbPais.SelectedIndex = 0;
                dtpFechaNacimiento.Text = "";
                CmbLugarNacimiento.SelectedIndex = 0;
                cmbAlergias.SelectedIndex = 0;
                cmbTipoSangre.SelectedIndex = 0;
                txtNombrePadre.Text = "";
                cmbOcuapcionPadre.SelectedIndex = 0;
                txtNombreMadre.Text = "";
                cmbOcuapcionMadre.SelectedIndex = 0;
                txtDireccion.Text = "";
                cmbDepartamento.SelectedIndex = 0;
                cmbMunicipio.SelectedIndex = 0;
                txtAldea.Text = "";
                txtTelCasa.Text = "";
                txtTelEmergencia.Text = "";
                txtCelular.Text = "";
                txtCorreo.Text = "";
                txtObservaciones.Text = "";
                txtRegistroMedico.Text = "";
                cmbDepartamento.IsEnabled = true;
                cmbDepartamento.IsEnabled = true;
                txtRegistroMedico.Text = "";
                imgPaciente.Source = new BitmapImage(new Uri("/ControlDePacientes;component/Images/nophoto.png", UriKind.Relative));
                txtDpiPaciente.Text = "";
                txtDPIPapa.Text = "";
                txtDPImama.Text = "";

                cmbGrupoEtnico.SelectedIndex = 0;
                cmbComunidadLinguistica.SelectedIndex = 0;
                cmbEscolaridad.SelectedIndex = 0;
                cmbDiscapacidad.SelectedIndex = 0;
                chkAgricolaMigrante.IsChecked = false;
                chkIgss.IsChecked = false;
            }
        }

        private string Validation()
        {
            if (txtNombrePaciente.Text == "")
                return "Ingrese el Nombre del Paciente";
            //else if (txtPrimerApellidoPaciente.Text == "")
            //    return "Ingrese el Apellido del Paciente";
            else if (cmbSexo.SelectedIndex == 0)
                return "Seleccione el Sexo del Paciente";

            //else if (dtpFechaNacimiento.Text == "")
            //    return "Ingrese fecha de nacimiento del cliente";
            //else if (CmbLugarNacimiento.SelectedIndex == 0)
            //    return "Ingrese el lugar de nacimiento del Paciente";
            //else if (txtNombrePadre.Text == "")
            //    return "Ingrese el Nombre del Padre del Paciente";
            //else if (txtNombreMadre.Text == "")
            //    return "Ingrese el nombre de la Mamá del Paciente";
            //else if (cmbOcuapcionPadre.SelectedIndex == 0)
            //    return "Seleccione OCupacion del Padre";
            //else if (cmbOcuapcionMadre.SelectedIndex == 0)
            //    return "Seleccione OCupacion de la Madre";
            //else if (txtTelCasa.Text == "")
            //    return "Ingrese un Numero de telefono de casa del Paciente";
            //else if (cmbPais.SelectedIndex == 0)
            //    return "Seleccione un País";
            //else
            //    if ((int)cmbPais.SelectedValue != 26)
            //    { }
            //    else
            //    {
            //        if (cmbDepartamento.SelectedIndex == 0)
            //            return "Seleccione un departamento";
            //        else if (cmbMunicipio.SelectedIndex == 0)
            //            return "Seleccione la municipalidad de la direccion del Paciente";
            //    }

            return "";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CleanBlockControls(2);
            txtPacienteId.Text = "";
            mIdPaciente = 0;

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ControlOperation.alertConfirm("Desea Eliminar el Registro Médico?") == MessageBoxResult.Yes)
            {
                try
                {
                    ControlDePacientes.Dal.Paciente lPaciente = new Paciente();
                    lPaciente.DeletePaciente(mIdPaciente);
                    ControlOperation.alertInformation("El Registro Médico se ha eliminado exitosamente");

                    txtPacienteId.Text = "";
                    btnCancel.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Collapsed;
                    CleanBlockControls(2);
                }
                catch (Exception ex)
                {

                    ControlOperation.alertWarning("No se puede Elimiar el registro, porque tiene un Historial, consulte con el adminsitrador");
                }




            }
        }

        private void btnFoto_Click(object sender, RoutedEventArgs e)
        {
            if (ControlOperation.alertConfirm("Desea tomar una nueva foto?") == MessageBoxResult.No)
            {


                OpenFileDialog lOpenFileDialog = new OpenFileDialog();
                lOpenFileDialog.InitialDirectory = @"C:\";
                lOpenFileDialog.Filter = "Archivos de Imagen (.jpg)|*.jpg|All Files (*.*)|*.*";
                lOpenFileDialog.FilterIndex = 1;
                lOpenFileDialog.Multiselect = false;



                bool? lChecarOK = lOpenFileDialog.ShowDialog();
                if (lChecarOK == true)
                {

                    imgPaciente.Source = new BitmapImage(new Uri(lOpenFileDialog.FileName));
                    mFileName = (string)(lOpenFileDialog.FileName);
                }
            }
            else
            {
                WinFormCharpWebCam.mainWinForm lMainWinForm = new mainWinForm();
                lMainWinForm.ShowDialog();
            }

        }

        private void dtpFechaNacimiento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //Calcula la edad
            if (dtpFechaNacimiento.Text != "")
            {
                int lEdad = CalcularEdad(DateTime.Parse(dtpFechaNacimiento.Text));

                if (lEdad == -1)
                {
                    ControlOperation.alertWarning("La fecha de nacimiento es mayor a la fecha de hoy, Ingrese correctamente la fecha de nacimiento");
                    dtpFechaNacimiento.Text = "";

                }
                else if (lEdad == -2)
                {
                    ControlOperation.alertWarning(
                        "La edad del niño es igual o mayor a 13 años, no se le puede registrar en el sistema");

                    mAgeBlock = true;
                }
                else
                {

                    DateDifference dateDifference = new DateDifference(dtpFechaNacimiento.SelectedDate.Value,
                                                                       DateTime.Now.Date);

                    if (dtpFechaNacimiento.SelectedDate != null)
                        lblEdad.Content = dateDifference.ToString();
                    //lblEdad.Content = lEdad.ToString();
                }
            }

        }

        public int CalcularEdad(DateTime birthDate)
        {

            if (birthDate.Year > DateTime.Now.Year)
            {
                return -1;
            }
            else
            {
                int age = DateTime.Now.Year - birthDate.Year;

                if (DateTime.Now.Month < birthDate.Month || (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day))
                    age--;
                if (mAgeBlock)
                {
                    return age;
                }
                else
                    if (age >= 13)
                        return -2;

                return age;
            }

        }

        // private void SaveRegistroMedico()
        //{
        //    if (mIdPaciente != 0)
        //    {
        //        if (mIdHistorial == "")
        //        {
        //            // Cuenta los historiales para el correlativo
        //            ControlDePacientes.Dal.HistorialMedico lHistorialMedico = new HistorialMedico();
        //            int lCountHistory = lHistorialMedico.HistorialMedicoCountByYear(mIdPaciente);
        //            lCountHistory += 1;

        //            ControlDePacientes.Entities.historialmedico lHistorialmedicoEntity = new historialmedico();
        //            lHistorialmedicoEntity.Historialmediconame = "Historial No. " + lCountHistory + " del paciente No. " + mIdPaciente;
        //            lHistorialmedicoEntity.Codestado = 1;
        //            lHistorialmedicoEntity.Codpaciente = mIdPaciente;
        //            //lHistorialmedicoEntity.Registromedico = lCountHistory.ToString() + "-" + DateTime.Today.Date.Year;
        //            lHistorialmedicoEntity.Fecha = DateTime.Today;
        //            mIdHistorialForDelete = lHistorialMedico.AddNewHistorialMedico(lHistorialmedicoEntity);

        //           // mIdHistorial = lHistorialmedicoEntity.Registromedico;

        //           // txtRegistroMedico.Text = lHistorialmedicoEntity.Registromedico;//mIdHistorial.ToString() + "-" + DateTime.Today.Date.Year;
        //            txtRegistroMedico.Visibility = Visibility.Visible;
        //            lblRegistroMedico.Visibility = Visibility.Visible;

        //            // Microsoft.Windows.Controls.MessageBox.Show("Registro Médico no: " + lHistorialmedicoEntity.Registromedico + " Guardado éxitosamente del paciente No. " + mIdPaciente, "Validación del Sistema");
        //        }
        //        else
        //        {
        //            ControlOperation.alertWarning("Ya se genero el Registro Médico no: " + mIdHistorial);
        //        }
        //    }
        //    else
        //    {
        //        ControlOperation.alertWarning("Debe registrar el paciente para generar un registro médico nuevo");
        //    }

        //}

        private void btnHistorial_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (mIdPaciente != 0)
                {
                    if (mIdHistorial == "")
                    {
                        //verifica si no tiene un historial ya generado
                        ControlDePacientes.Dal.HistorialMedico lHistorialMedico = new HistorialMedico();
                        DataTable lDtCheck = lHistorialMedico.HistorialMedicoPendienteCheck(mIdPaciente);

                        if (lDtCheck.Rows.Count > 0)
                        {
                            ControlOperation.alertWarning("El Paciente tiene un historial pendiente de terminar, debe realizarse el exámen físico");
                        }
                        else
                        {
                            // Cuenta los historiales para el correlativo
                            //ControlDePacientes.Dal.HistorialMedico lHistorialMedico = new HistorialMedico();
                            int lCountHistory = lHistorialMedico.HistorialMedicoCountByYear(mIdPaciente);
                            lCountHistory += 1;

                            ControlDePacientes.Entities.historialmedico lHistorialmedicoEntity = new historialmedico();
                            lHistorialmedicoEntity.Historialmediconame = "Historial No. " + lCountHistory +
                                                                         " del paciente No. " + mIdPaciente;
                            lHistorialmedicoEntity.Codestado = 10; //ESTADO INICIADO PARA HISTORIAL
                            lHistorialmedicoEntity.Codpaciente = mIdPaciente;
                            lHistorialmedicoEntity.Correlativo = lCountHistory.ToString() + "-" + DateTime.Today.Date.Year;
                            lHistorialmedicoEntity.Fecha = DateTime.Today;

                            mIdHistorialForDelete = lHistorialMedico.AddNewHistorialMedico(lHistorialmedicoEntity);

                            mIdHistorial = lHistorialmedicoEntity.Correlativo;

                            txtRegistroMedico.Text = lHistorialmedicoEntity.Correlativo;
                            //mIdHistorial.ToString() + "-" + DateTime.Today.Date.Year;
                            txtRegistroMedico.Visibility = Visibility.Visible;
                            lblRegistroMedico.Visibility = Visibility.Visible;

                            loadInfoAdicional(lCountHistory);

                            ControlOperation.alertInformation("Historial Médico Generado con éxito");



                        }
                    }
                    else
                    {
                        ControlOperation.alertWarning("Ya se genero el Historial Médico para el paciente");
                    }
                }
                else
                {
                    ControlOperation.alertWarning("Debe registrar el paciente para generar un Historial Médico nuevo");
                }
            }
            catch (Exception ex)
            {

                ControlOperation.alertWarning(ex.Message);
            }
        }

        private void dtgLoadHistorialPaciente(string pRegistroMedico)
        {
            ControlDePacientes.Dal.Paciente lPaciente = new Dal.Paciente();


            DataTable lDtPaciente = new DataTable();

            lDtPaciente = lPaciente.GetPacientePorParametros(pRegistroMedico);

            dtgPaciente.ItemsSource = lDtPaciente.DefaultView;
        }
        
        private void dtgPaciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAcompanante_Click(object sender, RoutedEventArgs e)
        {
            loadInfoAdicional(0);
        }

        private void loadInfoAdicional(int pCountHistory)
        {
            //ABRE VENTANA  DE INFORMACIÓN ADICIONAL
            try
            {
                ControlDePacientes.Dal.Paciente lPaciente = new ControlDePacientes.Dal.Paciente();
                DataTable ldtPacienteInfo = lPaciente.GetPacienteHistorialByEstado(mIdPaciente);
                string lContador = ldtPacienteInfo.Rows[0]["Correlativo"].ToString();
                if (isEdit)
                {




                    frmPopUpInfoAdicionalPaciente lFrmPopUpInfoAdicionalPaciente = new frmPopUpInfoAdicionalPaciente
                        {
                            HorizontalAlignment = HorizontalAlignment.Center,
                            mCodHistorial = System.Convert.ToInt32(ldtPacienteInfo.Rows[0]["IdHistorialMedico"].ToString()),
                            mCodPaciente = mIdPaciente,
                            mIsEdit = isEdit,
                            VerticalAlignment = VerticalAlignment.Center,
                            chkNuevoPaciente = { IsChecked = System.Convert.ToInt32((lContador).Substring(0, lContador.IndexOf(@"-"))) == 1 }
                        };

                    lFrmPopUpInfoAdicionalPaciente.ShowDialog();
                }
                else
                {
                    frmPopUpInfoAdicionalPaciente lFrmPopUpInfoAdicionalPaciente = new frmPopUpInfoAdicionalPaciente
                        {
                            HorizontalAlignment = HorizontalAlignment.Center,
                            mCodHistorial = System.Convert.ToInt32(ldtPacienteInfo.Rows[0]["IdHistorialMedico"].ToString()),
                            mCodPaciente = mIdPaciente,
                            mIsEdit = isEdit,
                            VerticalAlignment = VerticalAlignment.Center,
                            chkNuevoPaciente = { IsChecked = pCountHistory == 1 }
                        };
                    lFrmPopUpInfoAdicionalPaciente.ShowDialog();
                }
            }
            catch (Exception ex) { ControlOperation.alertWarning(ex.Message); }

        }

        #endregion

    }
}