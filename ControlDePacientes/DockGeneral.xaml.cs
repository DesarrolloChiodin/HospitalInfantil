using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.ComponentModel;
using ControlDePacientes.Dal;
using ControlDePacientes.Logic;
using MySql.Data.MySqlClient;
using MySql;


using WinFormCharpWebCam;
using DockPanel = ControlDePacientes.Dal.DockPanel;

namespace ControlDePacientes
{
    /// <summary>
    /// Interaction logic for DockGeneral.xaml
    /// </summary>
    public partial class DockGeneral : Window
    {
        public DockGeneral()
        {
            InitializeComponent();
        }

        #region Public Properties
        //public string connStr = ConfigurationManager.ConnectionStrings["ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;
        public string mUserName { get; set; }
        public int mIdTipoUsuario { get; set; }
        public DataTable mDtUsuario;
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //linea agregada
            //using (MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr))
            //{
            //    {

            //        MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("GetAlergias", conn);
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //        //cmd.Parameters.Add("@MyParam1", MySql.Data.MySqlClient.MySqlDbType.Int32).Value = 123;
            //        //cmd.Parameters.Add("@MyParam2", MySql.Data.MySqlClient.MySqlDbType.String).Value = "blah";
            //        DataTable dt = new DataTable();
            //        conn.Open();
            //        MySqlDataReader myDataReader = cmd.ExecuteReader();
            //        dt.Load(myDataReader);
            //        //cmd.ExecuteNonQuery();
            //        dataGrid1.ItemsSource = dt.DefaultView;
            //    }

            //}


            //   MessageBox.Show("lineas: " + mDtUsuario.Rows.Count, "info");

            lblUsernName.Content = "Usuario logeado: " + mUserName + " a las " + DateTime.Now.ToShortTimeString();
            CreateMenu();
        }

        private void CreateMenu()
        {
            int lModuleCount = 0;
            //BUSCO LOS MODULOS POR TIPO DE USUARIO
            ControlDePacientes.Dal.DockPanel lDockPanel = new DockPanel();
            mDtUsuario = new DataTable();
            mDtUsuario = lDockPanel.GetModuloByTipoUsuario(mIdTipoUsuario);

            lModuleCount = mDtUsuario.Rows.Count;



            for (int x = 0; x < lModuleCount; x++)
            {
                Expander lExpander = new Expander();
                lExpander.Name = "exp" + x + 1;
                lExpander.Tag = (int)mDtUsuario.Rows[x]["IdModulo"];//x + 1;
                lExpander.Header = mDtUsuario.Rows[x]["ModuloName"]; // +"" + lBulletDecorator;
                lExpander.Width = 175;

                stpMenu.Children.Add(lExpander);

                CreateSubMenu((int)lExpander.Tag, lExpander);

                lExpander.IsExpanded = true;
            }

        //    scrMenu.Content = stpMenu;


        }

        private void CreateSubMenu(int pIdModulo, Expander pExapnder)
        {
            try
            {
                //busco los menu hijo
                DockPanel lDockPanel = new DockPanel();
                DataTable lDtModuloHijo = lDockPanel.GetModuloHijoByModulo(pIdModulo);

                StackPanel lStackPanel = new StackPanel();

                if (lDtModuloHijo.Rows.Count != 0)
                {
                    for (int i = 0; i < lDtModuloHijo.Rows.Count; i++)
                    {
                        Button lBtnMenuSon = new Button();

                        if (lDtModuloHijo.Rows[i]["ModuloHijoImagen"] != null)
                        {
                            Image lImage = new Image();
                            lImage.Source = new BitmapImage(new Uri((string)lDtModuloHijo.Rows[i]["ModuloHijoImagen"], UriKind.Relative));
                            lImage.StretchDirection = StretchDirection.Both;
                            lImage.HorizontalAlignment = HorizontalAlignment.Right;


                            TextBlock lTextBlock = new TextBlock();
                            lTextBlock.Text = (string)lDtModuloHijo.Rows[i]["ModuloHijoName"];
                            lTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
                            lTextBlock.Width = 100;

                            WrapPanel lWrapPanel = new WrapPanel();
                            lWrapPanel.Width = 150;

                            lWrapPanel.Children.Add(lTextBlock);

                            lWrapPanel.Children.Add(lImage);


                            Grid lGrid = new Grid();
                            lGrid.Width = 150;
                            lGrid.Children.Add(lWrapPanel);


                            lBtnMenuSon.Content = lGrid; //lDtModuloHijo.Rows[i]["ModuloHijoName"];

                            lBtnMenuSon.HorizontalContentAlignment = HorizontalAlignment.Left;
                            lBtnMenuSon.Tag = i + 1;
                            lBtnMenuSon.Height = 40;
                            lBtnMenuSon.Width = 150;
                            lBtnMenuSon.Click += new RoutedEventHandler(BtnClick);
                            if (lDtModuloHijo.Rows[i]["ModuloHijoPage"] != DBNull.Value)
                                lBtnMenuSon.Name = (String)lDtModuloHijo.Rows[i]["ModuloHijoPage"];
                            lStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
                            lStackPanel.Children.Add(lBtnMenuSon);
                            pExapnder.Content = lStackPanel;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

               ControlOperation.alertWarning("" + ex);
            }


        }

        void BtnClick(object sender, RoutedEventArgs e)
        {

            if (((Button)sender).Name != "")
                MainFrame.NavigationService.Navigate(new Uri(((Button)sender).Name + ".xaml", UriKind.Relative));
            //MainFrame.NavigationService.Navigate(new frmRegistroPaciente());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            AcercaDe lAcercaDe = new AcercaDe();
            lAcercaDe.ShowDialog();

        }

        private void btnHideLeft_Click(object sender, RoutedEventArgs e)
        {
            GridLeft.Visibility = Visibility.Collapsed;
            btnHideLeft.Visibility = Visibility.Collapsed;
            btnHideRight.Visibility = Visibility.Visible;
            lblShowHide.Content = "Mostrar";
        }

        private void btnHideRight_Click(object sender, RoutedEventArgs e)
        {
            GridLeft.Visibility = Visibility.Visible;
            btnHideRight.Visibility = Visibility.Collapsed;
            btnHideLeft.Visibility = Visibility.Visible;
            lblShowHide.Content = "Ocultar";
        }

        
    }
}
