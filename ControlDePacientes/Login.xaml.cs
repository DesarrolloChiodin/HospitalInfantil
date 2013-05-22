using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
using ControlDePacientes.Logic;
using MySql.Data.MySqlClient;


namespace ControlDePacientes
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            new Introduccion().ShowDialog();
        }
        
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string lUser = "";
            string lPassword = "";

            lUser = txtUser.Text;
            lPassword = txtPassword.Password;

            var lLoginDal = new LoginDal();

            DataTable lDt = lLoginDal.GetUser(lUser, lPassword);

            //DESENCRYPTA LA CONTRASEÑA
            byte[] data = Encoding.UTF8.GetBytes(lPassword);
            HashAlgorithm hash = new SHA1Managed();
            byte[] hashBytes = hash.ComputeHash(data);
            String hashValue = BitConverter.ToString(hashBytes).Replace("-", "");
           

            if (lDt.Rows.Count >= 1)
            {
                if ((string)lDt.Rows[0]["Usuario"] == lUser &&  lDt.Rows[0]["Password"].ToString().ToUpper() == hashValue)
                {
                    ControlDePacientes.Entities.GlobalUser.Idusuario = (int) lDt.Rows[0]["IdUsuario"];


                   // MessageBox.Show("Usuario existe en la base de Datos", "Mensaje");
                    DockGeneral lDockGeneral = new DockGeneral();
                    this.Close();
                    lDockGeneral.mIdTipoUsuario = (int) lDt.Rows[0]["CodTipoUsuario"];
                    lDockGeneral.mUserName = (string)lDt.Rows[0]["Usuario"];
                    lDockGeneral.ShowDialog();

                }
                else

                {
                    ControlOperation.alertWarning("Usuario no existe ");
                    txtUser.Text = "";
                    txtPassword.Password = "";
                    txtUser.Focus();
                    
                }
            }
            else
            {
                ControlOperation.alertWarning("Usuario no existe ");
                txtUser.Text = "";
                txtPassword.Password= "";
                txtUser.Focus();
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

         
            
            
            txtUser.Focus();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           
        }
    }
}
