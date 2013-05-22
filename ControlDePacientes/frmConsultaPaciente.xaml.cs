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
using ControlDePacientes.Entities;
using ControlDePacientes.Logic;

namespace ControlDePacientes.BackEnd.Paciente
{
    /// <summary>
    /// Interaction logic for frmConsultaPaciente.xaml
    /// </summary>
    public partial class frmConsultaPaciente : Page
    {
        public frmConsultaPaciente()
        {
            InitializeComponent();
        }

        public DataTable mDtPaciente;
        public int mIdPaciente { get; set; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ControlDePacientes.Dal.Paciente lPaciente = new Dal.Paciente();


            mDtPaciente = new DataTable();
            if (txtNombrePaciente.Text == "")
                mDtPaciente = lPaciente.GetPacientePorParametros("");

            else if (txtNombrePaciente.Text != "")
                mDtPaciente = lPaciente.GetPacientePorParametros(txtNombrePaciente.Text);

            dtgPaciente.ItemsSource = mDtPaciente.DefaultView;
        }

        private void dtgPaciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SELECCIONA LA FILA
            DataRowView lCurrentDataRowView = ((DataRowView)((DataGrid)sender).SelectedItem);

            if (lCurrentDataRowView == null)
                return;

            ControlDePacientes.Entities.paciente lPaciente = new paciente();

            lPaciente = new paciente();
            lPaciente.Idpaciente = (int)lCurrentDataRowView.Row["IdPaciente"];
            lPaciente.Nombre = (string)lCurrentDataRowView.Row["Nombre"];
            lPaciente.Apellido = lCurrentDataRowView.Row["Apellido"].ToString() != "" ? (string)lCurrentDataRowView.Row["Apellido"] : "";
            lPaciente.Segundoapellido = lCurrentDataRowView.Row["SegundoApellido"].ToString() != "" ? (string)lCurrentDataRowView.Row["SegundoApellido"] : "";
            lPaciente.Codsexo = lCurrentDataRowView.Row["CodSexo"].ToString() != "" ? (int)lCurrentDataRowView.Row["CodSexo"] : 0;
            lPaciente.Fechanacimiento = lCurrentDataRowView.Row["FechaNacimiento"].ToString() != "" ? (DateTime)lCurrentDataRowView.Row["FechaNac"] : new DateTime();
            lPaciente.Lugarnacimiento = lCurrentDataRowView.Row["LugarNacimiento"].ToString() != "" ? (int)lCurrentDataRowView.Row["LugarNacimiento"] : 0;
            lPaciente.Codalergias = lCurrentDataRowView.Row["CodAlergias"].ToString() != "" ? (int)lCurrentDataRowView.Row["CodAlergias"] : 0;
            lPaciente.Codtiposangre = lCurrentDataRowView.Row["CodTipoSangre"].ToString() != "" ? (int)lCurrentDataRowView.Row["CodTipoSangre"] : 0;
            lPaciente.Nombrepadre = lCurrentDataRowView.Row["NombrePadre"].ToString() != "" ? (string)lCurrentDataRowView.Row["NombrePadre"] : "";
            lPaciente.Codocupacionpadre = lCurrentDataRowView.Row["CodOcupacionPadre"].ToString() != "" ? (int)lCurrentDataRowView.Row["CodOcupacionPadre"] : 0;
            lPaciente.Nombremadre = lCurrentDataRowView.Row["NombreMadre"].ToString() != "" ? (string)lCurrentDataRowView.Row["NombreMadre"] : "";
            lPaciente.Codocupacion = lCurrentDataRowView.Row["CodOcupacion"].ToString() != "" ? (int)lCurrentDataRowView.Row["CodOcupacion"] : 0;
            //lPaciente.NombreAcompanante = lCurrentDataRowView.Row["NombreAcompanante"].ToString() != "" ? lCurrentDataRowView.Row["NombreAcompanante"].ToString() : "";
            //lPaciente.CodOcupacionAcompanante = lCurrentDataRowView.Row["CodOcupacionAcompanante"].ToString() != "" ? (int)lCurrentDataRowView.Row["CodOcupacionAcompanante"] : 0;
            lPaciente.Direccion = lCurrentDataRowView.Row["Direccion"].ToString() != "" ? (string)lCurrentDataRowView.Row["Direccion"] : "";
            lPaciente.Coddepartamento = lCurrentDataRowView.Row["CodDepartamento"].ToString() != "" ? (int)lCurrentDataRowView.Row["CodDepartamento"] : 0;
            lPaciente.Codmunicipio = lCurrentDataRowView.Row["CodMunicipio"].ToString() != "" ? (int)lCurrentDataRowView.Row["CodMunicipio"] : 0;
            lPaciente.Aldea = lCurrentDataRowView.Row["Aldea"].ToString() != "" ? (string)lCurrentDataRowView.Row["Aldea"] : "";
            lPaciente.Telefonocasa = lCurrentDataRowView.Row["TelefonoCasa"].ToString() != "" ? (string)lCurrentDataRowView.Row["TelefonoCasa"] : "";
            lPaciente.Telefonoemergencia = lCurrentDataRowView.Row["TelefonoEmergencia"].ToString() != "" ? (string)lCurrentDataRowView.Row["TelefonoEmergencia"] : "";
            lPaciente.Celularprincipal = lCurrentDataRowView.Row["CelularPrincipal"].ToString() != "" ? (string)lCurrentDataRowView.Row["CelularPrincipal"] : "";
            lPaciente.Celularsecundario = lCurrentDataRowView.Row["CelularSecundario"].ToString() != "" ? (string)lCurrentDataRowView.Row["CelularSecundario"] : "";
            lPaciente.Correoelectronico = lCurrentDataRowView.Row["CorreoElectronico"].ToString() != "" ? (string)lCurrentDataRowView.Row["CorreoElectronico"] : "";
            lPaciente.Observacion = lCurrentDataRowView.Row["Observacion"].ToString() != "" ? (string)lCurrentDataRowView.Row["Observacion"] : "";
            lPaciente.Fotopath = lCurrentDataRowView.Row["Fotopath"] == DBNull.Value ? "" : (string)lCurrentDataRowView.Row["Fotopath"];
            lPaciente.Registromedico = lCurrentDataRowView.Row["RegistroMedico"].ToString() != "" ? (string)lCurrentDataRowView.Row["RegistroMedico"] : "";
            lPaciente.Codpais = lCurrentDataRowView.Row["CodPais"].ToString() != "" ? (int)lCurrentDataRowView.Row["CodPais"] : 0;
            lPaciente.FotoPaciente = lCurrentDataRowView.Row["FotoPaciente"].ToString() != "" ? (byte[])lCurrentDataRowView.Row["FotoPaciente"] : null;
            lPaciente.FotoNombre = lCurrentDataRowView.Row["FotoNombre"].ToString() != "" ? (string)lCurrentDataRowView.Row["FotoNombre"] : "";
            lPaciente.CodGrupoEtnico = lCurrentDataRowView.Row["CodGrupoEtnico"].ToString() != "" ? (int)lCurrentDataRowView.Row["CodGrupoEtnico"] : 0;
            lPaciente.CodComunidadLinguistica = lCurrentDataRowView.Row["CodComunidadLinguistica"].ToString() != "" ? (int)lCurrentDataRowView.Row["CodComunidadLinguistica"] : 0;
            lPaciente.CodEscolaridad = lCurrentDataRowView.Row["CodEscolaridad"].ToString() != "" ? (int)lCurrentDataRowView.Row["CodEscolaridad"] : 0;
            lPaciente.CodDiscapacidad = lCurrentDataRowView.Row["CodDiscapacidad"].ToString() != "" ? (int)lCurrentDataRowView.Row["CodDiscapacidad"] : 0;
            lPaciente.Igss = Convert.ToBoolean(lCurrentDataRowView.Row["Igss"]);
            lPaciente.AgricolaMigrante = Convert.ToBoolean(lCurrentDataRowView.Row["AgricolaMigrante"]);

            lPaciente.PacienteDpi = lCurrentDataRowView.Row["PacienteDpi"].ToString() != "" ? (string)lCurrentDataRowView.Row["PacienteDpi"] : "";
            lPaciente.PadreDpi = lCurrentDataRowView.Row["PadreDpi"].ToString() != "" ? (string)lCurrentDataRowView.Row["PadreDpi"] : "";
            lPaciente.MadreDpi = lCurrentDataRowView.Row["MadreDpi"].ToString() != "" ? (string)lCurrentDataRowView.Row["MadreDpi"] : "";


            frmRegistroPaciente lFrmRegistroPaciente = new frmRegistroPaciente();
            lFrmRegistroPaciente.lPaciente = lPaciente;
            lFrmRegistroPaciente.isEdit = true;


            this.NavigationService.Navigate(lFrmRegistroPaciente);

            //Frame lFrame = (Frame)Application.Current.MainWindow.FindName("MainFrame");

            //lFrame.NavigationService.Navigate(new frmRegistroPaciente());


        }

        private void txtNombrePaciente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {

                    ControlDePacientes.Dal.Paciente lPaciente = new Dal.Paciente();


                    mDtPaciente = new DataTable();
                    if (txtNombrePaciente.Text == "")
                        mDtPaciente = lPaciente.GetPacientePorParametros("");

                    else if (txtNombrePaciente.Text != "")
                        mDtPaciente = lPaciente.GetPacientePorParametros(txtNombrePaciente.Text);

                    dtgPaciente.ItemsSource = mDtPaciente.DefaultView;
                  
                }
                catch (Exception ex)
                {

                    ControlOperation.alertWarning("Error:--> " + ex);
                }
            }
        }
    }
}
