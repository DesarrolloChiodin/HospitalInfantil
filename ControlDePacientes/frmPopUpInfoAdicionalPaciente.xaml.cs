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
using System.Windows.Shapes;
using ControlDePacientes.Dal;
using PacienteInfoAdicional = ControlDePacientes.Entities.PacienteInfoAdicional;

namespace ControlDePacientes
{
    /// <summary>
    /// Interaction logic for frmPopUpInfoAdicionalPaciente.xaml
    /// </summary>
    public partial class frmPopUpInfoAdicionalPaciente : Window
    {
        public frmPopUpInfoAdicionalPaciente()
        {
            InitializeComponent();
        }

        public int mCodHistorial { get; set; }
        public int mCodPaciente { get; set; }
        public bool mIsEdit { get; set; }
        private bool mSave;
        private int mIdInfoAdicional;
        private ControlDePacientes.Entities.PacienteInfoAdicional mPacienteInfoAdicional;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fillConsulta();
            fillReferencia();
            chkNuevoPaciente.IsEnabled = false;
            txtPersonaRetiraPaciente.IsEnabled = false;

            if (mIsEdit)
            {
                usrBotones.btnCancelar.Visibility = Visibility.Collapsed;
                var lDtPacienteInfoAdicional = new DataTable();

                lDtPacienteInfoAdicional = new ControlDePacientes.Dal.PacienteInfoAdicional().GetPacienteInfoAdicionalPorHistorial(mCodHistorial);
                if (lDtPacienteInfoAdicional.Rows.Count > 0)
                {
                    mIdInfoAdicional =
                        System.Convert.ToInt32(lDtPacienteInfoAdicional.Rows[0]["idpacienteinfoadicional"].ToString());
                    txtNombreAcompanante.Text = lDtPacienteInfoAdicional.Rows[0]["NombreAcompanante"].ToString();
                    txtCedulaDpiAcompanante.Text = lDtPacienteInfoAdicional.Rows[0]["CedulaDpiAcompanante"].ToString();
                    txtTelefonoAcompanante.Text = lDtPacienteInfoAdicional.Rows[0]["TelefonoAcompanante"].ToString();
                    txtCelularAcompanante.Text = lDtPacienteInfoAdicional.Rows[0]["CelularAcompanante"].ToString();
                    txtDireccion.Text = lDtPacienteInfoAdicional.Rows[0]["Direccion"].ToString();
                    txtPersonaRetiraPaciente.Text = lDtPacienteInfoAdicional.Rows[0]["PersonaRetiraPaciente"].ToString();
                    cmbConsulta.SelectedValue =
                        System.Convert.ToInt32(lDtPacienteInfoAdicional.Rows[0]["CodConsulta"].ToString());
                    cmbReferencia.SelectedValue =
                        System.Convert.ToInt32(lDtPacienteInfoAdicional.Rows[0]["CodReferido"].ToString());
                    chkNuevoPaciente.IsChecked =
                        System.Convert.ToBoolean(lDtPacienteInfoAdicional.Rows[0]["NuevoPaciente"]);
                    chkRetiraPaciente.IsChecked =
                        System.Convert.ToBoolean(lDtPacienteInfoAdicional.Rows[0]["RetiraPaciente"]);
                    txtHistoriaAcompanante.Text = lDtPacienteInfoAdicional.Rows[0]["HistoriaAcompanante"].ToString();
                }
                else
                {
                    mIsEdit = false;
                }
            }


        }



        private void fillConsulta()
        {
            DataTable lDtConsulta = new DataTable();
            ControlDePacientes.Dal.Consulta lConsulta = new Consulta();
            lDtConsulta = lConsulta.GetConsultaAll();
            AddRowComboBox(lDtConsulta);

            cmbConsulta.ItemsSource = lDtConsulta.DefaultView;
            cmbConsulta.DisplayMemberPath = "Descripcion";
            cmbConsulta.SelectedValuePath = "IdConsulta";
            cmbConsulta.SelectedIndex = 0;
        }

        private void fillReferencia()
        {
            DataTable lDtReferencia = new DataTable();
            ControlDePacientes.Dal.Referido lReferencia = new Dal.Referido();
            lDtReferencia = lReferencia.ReferidoGetAll();
            AddRowComboBox(lDtReferencia);

            cmbReferencia.ItemsSource = lDtReferencia.DefaultView;
            cmbReferencia.DisplayMemberPath = "Descripcion";
            cmbReferencia.SelectedValuePath = "IdReferido";
            cmbReferencia.SelectedIndex = 0;
        }

        private void AddRowComboBox(DataTable pDt)
        {
            DataRow lDr = pDt.NewRow();
            lDr[0] = 0;
            lDr[1] = "-- Seleccione --";
            pDt.Rows.InsertAt(lDr, 0);
        }

        private void usrBotones_ButtonSaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((chkInfoAdicional.IsChecked == false))
                {
                    string lMessage;
                    lMessage = validacion();

                    if (lMessage == "")
                    {
                        mPacienteInfoAdicional = new PacienteInfoAdicional();
                        mPacienteInfoAdicional = fillPacienteInfoAdicional();

                        ControlDePacientes.Dal.PacienteInfoAdicional lDalPacienteInfoAdicional = new Dal.PacienteInfoAdicional();

                        if (!mIsEdit)
                        {

                            lDalPacienteInfoAdicional.AddPacienteInfoAdicional(mPacienteInfoAdicional);
                            Logic.ControlOperation.alertInformation("Información guardada con éxito");
                            mSave = true;
                            Close();
                        }
                        else
                        {
                            mPacienteInfoAdicional.Idpacienteinfoadicional = mIdInfoAdicional;
                            lDalPacienteInfoAdicional.UpdPacienteInfoAdicional(mPacienteInfoAdicional);
                            Logic.ControlOperation.alertInformation("Información Actualizada con éxito");
                            mSave = true;
                            Close();
                        }
                    }
                    else
                    {
                        ControlDePacientes.Logic.ControlOperation.alertWarning(lMessage);
                    }
                }
                else
                {
                    Close();
                }
            }
            catch (Exception ex)
            {

                ControlDePacientes.Logic.ControlOperation.alertWarning(ex.Message);
            }

        }

        private void usrBotones_ButtonDeleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void usrBotones_ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            txtCedulaDpiAcompanante.Text = "";
            txtDireccion.Text = "";
            txtHistoriaAcompanante.Text = "";
            txtNombreAcompanante.Text = "";
            chkRetiraPaciente.IsChecked = false;
            txtPersonaRetiraPaciente.Text = "";
            cmbConsulta.SelectedIndex = 0;
            cmbReferencia.SelectedIndex = 0;


        }

        private void usrBotones_Loaded(object sender, RoutedEventArgs e)
        {
            usrBotones.btnEliminar.Visibility = Visibility.Collapsed;
        }

        private ControlDePacientes.Entities.PacienteInfoAdicional fillPacienteInfoAdicional()
        {
            ControlDePacientes.Entities.PacienteInfoAdicional lPacienteInfoAdicional = new ControlDePacientes.Entities.PacienteInfoAdicional();

            //lPacienteInfoAdicional.idpacienteinfoadicional = txtidpacienteinfoadicional;
            lPacienteInfoAdicional.Nombreacompanante = txtNombreAcompanante.Text;
            lPacienteInfoAdicional.Ceduladpiacompanante = txtCedulaDpiAcompanante.Text;
            lPacienteInfoAdicional.Telefonoacompanante = txtTelefonoAcompanante.Text;
            lPacienteInfoAdicional.Celularacompanante = txtCelularAcompanante.Text;
            lPacienteInfoAdicional.Direccion = txtDireccion.Text;
            lPacienteInfoAdicional.Personaretirapaciente = txtPersonaRetiraPaciente.Text;
            lPacienteInfoAdicional.Retirapaciente = chkRetiraPaciente.IsChecked.Value;
            lPacienteInfoAdicional.Historiaacompanante = txtHistoriaAcompanante.Text;
            lPacienteInfoAdicional.Fechaacompanante = DateTime.Now;
            lPacienteInfoAdicional.Codhistorialmedico = mCodHistorial;
            lPacienteInfoAdicional.NuevoPaciente = chkNuevoPaciente.IsChecked.Value;
            lPacienteInfoAdicional.Codpaciente = mCodPaciente;


            if ((int)cmbConsulta.SelectedIndex == 0)
                lPacienteInfoAdicional.CodConsulta = 0;
            else lPacienteInfoAdicional.CodConsulta = (int)cmbConsulta.SelectedValue;

            if ((int)cmbReferencia.SelectedIndex == 0)
                lPacienteInfoAdicional.CodReferencia = 0;
            else lPacienteInfoAdicional.CodReferencia = (int)cmbReferencia.SelectedValue;



            return lPacienteInfoAdicional;

        }

        private string validacion()
        {
            if (txtCedulaDpiAcompanante.Text == "")
                return "Ingrese Cedula o Dpi del Acompañante";

            if (txtDireccion.Text == "")
                return "Ingrese Dirección del Acompañante";

            if (txtHistoriaAcompanante.Text == "")
                return "ingrese la Historia del Acompañante";

            if (txtNombreAcompanante.Text == "")
                return "Ingrese el Nombre del Acompañante";

            if ((bool)chkRetiraPaciente.IsChecked)
            {
                if (txtPersonaRetiraPaciente.Text == "")
                    return "Ingrese el nombre de la persona que retira al Paciente";
            }

            if ((int)cmbConsulta.SelectedValue == 0)
                return "Seleccione la consulta";

            if ((int)cmbReferencia.SelectedValue == 0)
                return "Seleccione la referencia";


            return "";
        }

        private void chkInfoAdicional_Checked(object sender, RoutedEventArgs e)
        {
            txtCedulaDpiAcompanante.IsEnabled = false;
            txtCelularAcompanante.IsEnabled = false;
            txtDireccion.IsEnabled = false;
            txtHistoriaAcompanante.IsEnabled = false;
            txtNombreAcompanante.IsEnabled = false;
            txtPersonaRetiraPaciente.IsEnabled = false;
            txtTelefonoAcompanante.IsEnabled = false;
            chkRetiraPaciente.IsEnabled = false;
        }

        private void chkInfoAdicional_Unchecked(object sender, RoutedEventArgs e)
        {
            txtCedulaDpiAcompanante.IsEnabled = true;
            txtCelularAcompanante.IsEnabled = true;
            txtDireccion.IsEnabled = true;
            txtHistoriaAcompanante.IsEnabled = true;
            txtNombreAcompanante.IsEnabled = true;
            txtPersonaRetiraPaciente.IsEnabled = true;
            txtTelefonoAcompanante.IsEnabled = true;
            chkRetiraPaciente.IsEnabled = true;
        }

        private void chkRetiraPaciente_Checked(object sender, RoutedEventArgs e)
        {
            txtPersonaRetiraPaciente.IsEnabled = true;
        }

        private void chkRetiraPaciente_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPersonaRetiraPaciente.IsEnabled = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!mSave)
            {
                if (Logic.ControlOperation.alertConfirm("no se guardara la información, desea cerrar esta ventana? ") ==
                    MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
