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
    /// Interaction logic for frmModulo.xaml
    /// </summary>
    public partial class frmModulo : Page
    {
        public frmModulo()
        {
            InitializeComponent();
        }

        public ControlDePacientes.Entities.modulo mEntityModulo;
        public ControlDePacientes.Entities.modulohijo mEntityModuloHijo;
        public int mIdModulo;
        public int mIdModuloHijo;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            fillCmbTipoUsuario();
            fillCmbModule();
            fillDtgModulo();
            fillDtgModuloHijo();

        }

        private void fillCmbTipoUsuario()
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

        private void fillModulo()
        {
            mEntityModulo = new ControlDePacientes.Entities.modulo();
            mEntityModulo.Moduloname = txtModuloName.Text;
            mEntityModulo.Codtipousuario = (int)cmbTipoUsuario.SelectedValue;
        }

        private bool Validation()
        {
            if (txtModuloName.Text == "")
            {
                ControlOperation.alertWarning("Ingrese el nombre del modulo");
                return false;
            }

            if (cmbTipoUsuario.SelectedIndex == 0)
            {
                ControlOperation.alertWarning("Seleccione un tipo de usuario");
                return false;

            }

            return true;

        }

        private void AddRowComboBox(DataTable pDt)
        {
            DataRow lDr = pDt.NewRow();
            lDr[0] = 0;
            lDr[1] = "-- Seleccione --";
            pDt.Rows.InsertAt(lDr, 0);
        }

        private void AddRowComboBox2(DataTable pDt2)
        {
            DataRow lDr2 = pDt2.NewRow();
            lDr2[0] = 0;
            lDr2[1] = "-- Seleccione --";
            pDt2.Rows.InsertAt(lDr2, 0);
        }

        private void fillDtgModulo()
        {
            ControlDePacientes.Dal.Modulo lModulo = new ControlDePacientes.Dal.Modulo();
            DataTable lDtModulo = lModulo.GetModulo();
            dtgModulo.ItemsSource = lDtModulo.DefaultView;

        }

        private void btnGuardarModulo_Click(object sender, RoutedEventArgs e)
        {

            if (Validation())
            {
                try
                {
                    fillModulo();
                    ControlDePacientes.Dal.Modulo lModulo = new Modulo();

                    if (mIdModulo == 0)
                    {
                        lModulo.AddModulo(mEntityModulo);
                        ControlOperation.alertInformation("Módulo guardado éxitosamente");
                    }
                    else
                    {
                        mEntityModulo.Idmodulo = mIdModulo;
                        lModulo.UpdateModulo(mEntityModulo);
                        ControlOperation.alertInformation("Módulo actualizado éxitosamente");
                    }
                    txtModuloName.Text = "";
                    mIdModulo = 0;
                    cmbTipoUsuario.SelectedIndex = 0;


                    fillDtgModulo();

                }
                catch (Exception ex)
                {

                    ControlOperation.alertWarning(ex + "");
                }

            }


        }

        private void btnCancelModulo_Click(object sender, RoutedEventArgs e)
        {
            txtModuloName.Text = "";
            cmbTipoUsuario.SelectedIndex = 0;
            mIdModulo = 0;
        }

        private void btnDeleteModulo_Click(object sender, RoutedEventArgs e)
        {
            if (mIdModulo > 0)
            {
                if (ControlOperation.alertConfirm("Desea eliminar el módulo  seleccionado?") == MessageBoxResult.Yes)
                {
                    fillModulo();
                    ControlDePacientes.Dal.Modulo lModulo = new Modulo();
                    lModulo.DeleteModulo(mIdModulo);
                    ControlOperation.alertInformation("Módulo eliminado éxitosamente");

                    txtModuloName.Text = "";
                    mIdModulo = 0;
                    cmbTipoUsuario.SelectedIndex = 0;

                    fillDtgModulo();
                }
            }
            else
            {
                ControlOperation.alertWarning("Seleccione un módulo para eliminar");
            }


        }

        private void dtgModulo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SELECCIONA LA FILA
            DataRowView lCurrentDataRowView = ((DataRowView)((DataGrid)sender).SelectedItem);

            if (lCurrentDataRowView == null)
                return;

            mIdModulo = (int)lCurrentDataRowView.Row["IdModulo"];
            txtModuloName.Text = (string)lCurrentDataRowView.Row["ModuloName"];
            cmbTipoUsuario.SelectedValue = (int)lCurrentDataRowView.Row["CodTipoUsuario"];
        }

        private void btnGuardarModuloHijo_Click(object sender, RoutedEventArgs e)
        {
            if (validationModuloHijo())
            {
                try
                {
                    fillModuloHijo();
                    ControlDePacientes.Dal.ModuloHijo lModuloHijo = new ModuloHijo();

                    if (mIdModuloHijo == 0)
                    {
                        lModuloHijo.AddModuloHijo(mEntityModuloHijo);
                        ControlOperation.alertInformation("Módulo Hijo guardado éxitosamente");
                    }
                    else
                    {
                        mEntityModuloHijo.Idmodulohijo = mIdModuloHijo;
                        lModuloHijo.UpdateModuloHijo(mEntityModuloHijo);
                        ControlOperation.alertInformation("Módulo Hijo actualizado éxitosamente");
                    }
                    txtModuloHijoPage.Text = "";
                    txtModuloHijoName.Text = "";
                    txtModuloHijoImagen.Text = "";
                    cmbModulo.SelectedIndex = 0;
                    mIdModuloHijo = 0;

                    fillDtgModuloHijo();

                }
                catch (Exception ex)
                {

                    ControlOperation.alertWarning(ex + "");
                }
            }
        }

        private void btnCancelModuloHijo_Click(object sender, RoutedEventArgs e)
        {
            txtModuloHijoPage.Text = "";
            txtModuloHijoName.Text = "";
            txtModuloHijoImagen.Text = "";
            cmbModulo.SelectedIndex = 0;
        }

        private void btnDeleteModuloHijo_Click(object sender, RoutedEventArgs e)
        {
            if (mIdModuloHijo > 0)
            {
                if (ControlOperation.alertConfirm("Desea eliminar el módulo hijo seleccionado?") == MessageBoxResult.Yes)
                {
                    fillModuloHijo();
                    ControlDePacientes.Dal.ModuloHijo lModuloHijo = new ModuloHijo();
                    lModuloHijo.DeleteModuloHijo(mIdModuloHijo);
                    ControlOperation.alertInformation("Módulo Hijo eliminado éxitosamente");

                    txtModuloHijoPage.Text = "";
                    txtModuloHijoName.Text = "";
                    txtModuloHijoImagen.Text = "";
                    cmbModulo.SelectedIndex = 0;
                    mIdModuloHijo = 0;

                    fillDtgModuloHijo();
                }
            }
            else
            {
                ControlOperation.alertWarning("Seleccione un módulo Hijo para eliminar");
            }


          
        }

        private void fillCmbModule()
        {
            DataTable lDtModule = new DataTable();
            ControlDePacientes.Dal.Modulo lModulo = new Modulo();

            lDtModule = lModulo.GetModulo();

            AddRowComboBox2(lDtModule);

            cmbModulo.ItemsSource = lDtModule.DefaultView;
            cmbModulo.SelectedValuePath = "IdModulo";
            cmbModulo.DisplayMemberPath = "ModuloName";
            cmbModulo.SelectedIndex = 0;

        }

        private bool validationModuloHijo()
        {
            if (txtModuloHijoName.Text == "")
            {
                ControlOperation.alertWarning("Escriba el nombre del módulo hijo");
                return false;
            }

            if (txtModuloHijoPage.Text == "")
            {
                ControlOperation.alertWarning("Escriba el nombre del page del módulo hijo");
                return false;

            }

            if (cmbModulo.SelectedIndex == 0)
            {
                ControlOperation.alertWarning("Seleccione el módulo para el módulo hijo");
                return false;
            }

            return true;
        }

        private void fillModuloHijo()
        {
            mEntityModuloHijo = new modulohijo();

            mEntityModuloHijo.Modulohijoname = txtModuloHijoName.Text;
            mEntityModuloHijo.Modulohijoimagen = txtModuloHijoImagen.Text;
            mEntityModuloHijo.Modulohijopage = txtModuloHijoPage.Text;
            mEntityModuloHijo.Codmodulo = (int) cmbModulo.SelectedValue;
        }

        private void fillDtgModuloHijo()
        {
            ControlDePacientes.Dal.ModuloHijo lModuloHijo = new ControlDePacientes.Dal.ModuloHijo();
            DataTable lDtModuloHijo = lModuloHijo.GetModuloHijo();
            dtgModuloHijo.ItemsSource = lDtModuloHijo.DefaultView;
        }

        private void dtgModuloHijo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SELECCIONA LA FILA
            DataRowView lCurrentDataRowView = ((DataRowView)((DataGrid)sender).SelectedItem);

            if (lCurrentDataRowView == null)
                return;

            mIdModuloHijo = (int)lCurrentDataRowView.Row["IdModuloHijo"];
            txtModuloHijoName.Text = (string)lCurrentDataRowView.Row["ModuloHijoName"];
            txtModuloHijoImagen.Text = (string)lCurrentDataRowView.Row["ModuloHijoImagen"];
            txtModuloHijoPage.Text = (string)lCurrentDataRowView.Row["ModuloHijoPage"];
            cmbModulo.SelectedValue = (int)lCurrentDataRowView.Row["CodModulo"];
        }
    }
}
