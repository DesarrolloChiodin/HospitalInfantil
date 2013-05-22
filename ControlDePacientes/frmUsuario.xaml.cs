using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ControlDePacientes.Dal;
using ControlDePacientes.Entities;
using ControlDePacientes.Logic;

namespace ControlDePacientes.Administracion
{
    /// <summary>
    /// Interaction logic for frmUsuario.xaml
    /// </summary>
    public partial class frmUsuario : Page
    {
        public frmUsuario()
        {
            InitializeComponent();
        }

        public int mIdTipoUsuario;
        public int mIdUsuario;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            fillDtgTipoUsuario();
            fillDtgUsuario();

            fillcmbTipoUsuario();
            fillcmbCargo();
            fillcmbEstadoUsuario();

        }

        #region Tipo Usuario
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtTipoUsuario.Text != "")
                {

                    if (mIdTipoUsuario == 0)
                    {
                        ControlDePacientes.Entities.tipousuario lTipousuario = new tipousuario();
                        lTipousuario.Tipousuarioname = txtTipoUsuario.Text;

                        ControlDePacientes.Dal.TipoUsuario lTipoUsuarioDal = new TipoUsuario();
                        mIdTipoUsuario = lTipoUsuarioDal.AddTipoUsuario(lTipousuario);


                        ControlOperation.alertInformation("Tipo de Usuario guardado éxitosamente");
                        fillDtgTipoUsuario();
                        txtTipoUsuario.Text = "";

                    }
                    else
                    {
                        ControlDePacientes.Entities.tipousuario lTipousuario = new tipousuario();
                        lTipousuario.Idtipousuario = mIdTipoUsuario;
                        lTipousuario.Tipousuarioname = txtTipoUsuario.Text;

                        ControlDePacientes.Dal.TipoUsuario lTipoUsuarioDal = new TipoUsuario();
                        lTipoUsuarioDal.UpdateTipoUsuario(lTipousuario);

                        ControlOperation.alertInformation("Tipo de Usuario Actualizado éxitosamente");
                        fillDtgTipoUsuario();
                        txtTipoUsuario.Text = "";

                        mIdTipoUsuario = 0;

                    }
                }
                else ControlOperation.alertWarning("Escriba un Tipo de Usuario");

            }
            catch (Exception ex)
            {

                ControlOperation.alertWarning("" + ex);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            LoopVisualTree(expTipoUsuario);
        }

        void LoopVisualTree(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                if (obj is TextBox)
                    ((TextBox)obj).Text = null;
                if (obj is ComboBox)
                    ((ComboBox)obj).SelectedIndex = 0;
                //if (obj is DataGrid)
                //    ((DataGrid)obj).ItemsSource = null;
                LoopVisualTree(VisualTreeHelper.GetChild(obj, i));
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mIdTipoUsuario != 0)
                {
                    if (ControlOperation.alertConfirm("Desea eliminar el Tipo de Usuario seleccionado?") == MessageBoxResult.Yes)
                    {
                        ControlDePacientes.Dal.TipoUsuario lTipoUsuario = new TipoUsuario();
                        lTipoUsuario.DeleteTipoUsuario(mIdTipoUsuario);
                        ControlOperation.alertInformation("Tipo de Usuario eliminado éxitosamente");
                        fillDtgTipoUsuario();
                        mIdTipoUsuario = 0;
                        txtTipoUsuario.Text = "";
                    }
                }
                else ControlOperation.alertWarning("Elija un Tipo de Usuario a Eliminar");

            }
            catch (Exception ex)
            {

                ControlOperation.alertWarning("" + ex);
            }

        }

        private void fillDtgTipoUsuario()
        {
            DataTable lDtTipoUsuario = new DataTable();
            ControlDePacientes.Dal.TipoUsuario lTipoUsuario = new TipoUsuario();

            lDtTipoUsuario = lTipoUsuario.GetTipoUsuario();

            dtgTipoUsuario.ItemsSource = lDtTipoUsuario.DefaultView;

        }

        private void dtgTipoUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SELECCIONA LA FILA
            DataRowView lCurrentDataRowView = ((DataRowView)((DataGrid)sender).SelectedItem);

            if (lCurrentDataRowView == null)
                return;

            ControlDePacientes.Entities.tipousuario lTipoUsuario = new tipousuario();

            lTipoUsuario.Idtipousuario = (int)lCurrentDataRowView.Row["IdTipoUsuario"];
            lTipoUsuario.Tipousuarioname = (string)lCurrentDataRowView.Row["TipoUsuarioName"];

            mIdTipoUsuario = (int)lCurrentDataRowView.Row["IdTipoUsuario"];

            txtTipoUsuario.Text = (string)lCurrentDataRowView.Row["TipoUsuarioName"];

        }
        #endregion

        #region Usuario

        private ControlDePacientes.Entities.usuario FillUsuario()
        {
            ControlDePacientes.Entities.usuario lUsuario = new usuario();

            lUsuario.Usuarioname = txtNombre.Text + " " + txtApellido.Text;
            lUsuario.Nombreusuario = txtNombre.Text;
            lUsuario.Apellidousuario = txtApellido.Text;
            lUsuario.Usuario1 = txtUsuario.Text;
            lUsuario.Password = txtPassword.Password;
            lUsuario.Codtipousuario = (int)cmbTipoUsuario.SelectedValue;
            lUsuario.Telefonocasa = txtTelefono.Text;
            lUsuario.Celular = txtCelular.Text;
            lUsuario.Codestado = (int)cmbEstado.SelectedValue;
            lUsuario.Codcargo = (int)cmbCargo.SelectedValue;

            return lUsuario;

        }

        private string validation()
        {

            if (txtNombre.Text == "")
                return "Ingrese el nombre del usuario";
            if (txtApellido.Text == "")
                return "Ingrese el apellido del usuario";
            if (txtUsuario.Text == "")
                return "Ingrese el usuario";
            if (txtPassword.Password == "")
                return "Ingrese la contraseña del usuario";
            if (cmbTipoUsuario.SelectedIndex == 0)
                return "Seleccione un tipo de usuario";
            if (txtTelefono.Text == "")
                return "Ingrese el número de teléfono del usuario";
            if (txtCelular.Text == "")
                return "Ingrese el número de celular del usuario";
            if (cmbEstado.SelectedIndex == 0)
                return "Seleccione el estado del usuario";
            if (cmbCargo.SelectedIndex == 0)
                return "Seleccione el cargo del usuario";

            return "";
        }

        private void btnGuardarUsuario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mIdUsuario == 0)
                {
                    string lValidate;
                    lValidate = validation();
                    if (lValidate == "")
                    {
                        ControlDePacientes.Dal.Usuario lUsuario = new Usuario();
                        ControlDePacientes.Entities.usuario lUsuarioEntity = new usuario();
                        lUsuarioEntity = FillUsuario();
                        mIdUsuario = lUsuario.AddUsuario(lUsuarioEntity);
                        ControlOperation.alertInformation("Usuario creado éxitosamente");
                        mIdUsuario = 0;
                        fillDtgUsuario();
                        txtNombre.Text = "";
                        txtApellido.Text = "";
                        txtCelular.Text = "";
                        txtTelefono.Text = "";
                        txtUsuario.Text = "";
                        txtPassword.Password = "";
                        cmbTipoUsuario.SelectedIndex = 0;
                        cmbCargo.SelectedIndex = 0;
                        cmbEstado.SelectedIndex = 0;

                    }
                    else ControlOperation.alertWarning(lValidate);

                }
                else if (mIdUsuario != 0)
                {
                    ControlDePacientes.Dal.Usuario lUsuario = new Usuario();
                    ControlDePacientes.Entities.usuario lUsuarioEntity = new usuario();
                    lUsuarioEntity = FillUsuario();
                    lUsuarioEntity.Idusuario = mIdUsuario;
                    lUsuario.UpdateUsuario(lUsuarioEntity);
                    ControlOperation.alertInformation("Usuario actualizado éxitosamente");
                    mIdUsuario = 0;
                    fillDtgUsuario();

                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtCelular.Text = "";
                    txtTelefono.Text = "";
                    txtUsuario.Text = "";
                    txtPassword.Password = "";
                    cmbTipoUsuario.SelectedIndex = 0;
                    cmbCargo.SelectedIndex = 0;
                    cmbEstado.SelectedIndex = 0;

                }

            }
            catch (Exception ex)
            {

                ControlOperation.alertWarning("" + ex);
            }
        }

        private void btnCancelUsuario_Click(object sender, RoutedEventArgs e)
        {
            LoopVisualTree(expUsuario);
        }

        private void btnDeleteUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (mIdUsuario != 0)
            {
                if (ControlOperation.alertConfirm("Desea eliminar al usuario") == MessageBoxResult.Yes)
                {
                    ControlDePacientes.Dal.Usuario lUsuario = new Usuario();
                    lUsuario.DeleteUsuario(mIdUsuario);
                    ControlOperation.alertInformation("Usuario eliminado con éxito");
                    fillDtgUsuario();
                    mIdUsuario = 0;
                }


            }
            else
            {
                ControlOperation.alertWarning("Elija un usuario para eliminar");
            }
        }

        private void fillcmbTipoUsuario()
        {
            DataTable lDtTipoUsuario = new DataTable();
            ControlDePacientes.Dal.TipoUsuario lTipoUsuario = new TipoUsuario();

            lDtTipoUsuario = lTipoUsuario.GetTipoUsuario();

            AddRowComboBox(lDtTipoUsuario);

            cmbTipoUsuario.ItemsSource = lDtTipoUsuario.DefaultView;
            cmbTipoUsuario.SelectedValuePath = "IdTipoUsuario";
            cmbTipoUsuario.DisplayMemberPath = "TipoUsuarioName";
            cmbTipoUsuario.SelectedIndex = 0;


        }

        private void fillcmbCargo()
        {
            DataTable lDtCargo = new DataTable();
            ControlDePacientes.Dal.Cargo lCargo = new Cargo();

            lDtCargo = lCargo.GetCargo();

            AddRowComboBox(lDtCargo);

            cmbCargo.ItemsSource = lDtCargo.DefaultView;
            cmbCargo.SelectedValuePath = "IdCargo";
            cmbCargo.DisplayMemberPath = "CargoName";
            cmbCargo.SelectedIndex = 0;

        }

        private void fillcmbEstadoUsuario()
        {
            DataTable lDtEstado = new DataTable();
            ControlDePacientes.Dal.Estados lEstados = new Estados();
            lDtEstado = lEstados.GetEstadoByTipoEstado(1);

            AddRowComboBox(lDtEstado);

            cmbEstado.ItemsSource = lDtEstado.DefaultView;
            cmbEstado.SelectedValuePath = "IdEstado";
            cmbEstado.DisplayMemberPath = "EstadoName";
            cmbEstado.SelectedIndex = 0;
        }

        private void AddRowComboBox(DataTable pDt)
        {
            DataRow lDr = pDt.NewRow();
            lDr[0] = 0;
            lDr[1] = "-- Seleccione --";
            pDt.Rows.InsertAt(lDr, 0);
        }

        private void fillDtgUsuario()
        {
            DataTable lDtUsuario = new DataTable();
            ControlDePacientes.Dal.Usuario lUsuario = new Usuario();
            lDtUsuario = lUsuario.GetUsuario();
            dtgUsuario.ItemsSource = lDtUsuario.DefaultView;


        }

        private void dtgUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SELECCIONA LA FILA
            DataRowView lCurrentDataRowView = ((DataRowView)((DataGrid)sender).SelectedItem);

            if (lCurrentDataRowView == null)
                return;


            mIdUsuario = (int)lCurrentDataRowView.Row["IdUsuario"];
            txtNombre.Text = (string)lCurrentDataRowView.Row["NombreUsuario"];
            txtApellido.Text = (string)lCurrentDataRowView.Row["ApellidoUsuario"];
            txtUsuario.Text = (string)lCurrentDataRowView.Row["Usuario"];
            //txtPassword.IsEnabled = false;// Password = (string)lCurrentDataRowView.Row["Password"];
            cmbTipoUsuario.SelectedValue = (int)lCurrentDataRowView.Row["CodTipoUsuario"];
            txtTelefono.Text = (string)lCurrentDataRowView.Row["TelefonoCasa"];
            txtCelular.Text = (string)lCurrentDataRowView.Row["Celular"];
            cmbEstado.SelectedValue = (int)lCurrentDataRowView.Row["CodEstado"];
            cmbCargo.SelectedValue = (int)lCurrentDataRowView.Row["CodCargo"];



        }
        #endregion







    }
}
