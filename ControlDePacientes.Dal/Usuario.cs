using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ControlDePacientes.Dal
{
    public class Usuario
    {
        public string connStr =
            ConfigurationManager.ConnectionStrings[
                "ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;


      

        public DataTable GetUsuario()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("usuario_getAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }
        public int AddUsuario(ControlDePacientes.Entities.usuario pUsuario)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("usuario_add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pUsuarioName", MySqlDbType.String).Value = pUsuario.Usuarioname;
                    cmd.Parameters.Add("@pNombreUsuario", MySqlDbType.String).Value = pUsuario.Nombreusuario;
                    cmd.Parameters.Add("@pApellidoUsuario", MySqlDbType.String).Value = pUsuario.Apellidousuario;
                    cmd.Parameters.Add("@pUsuario", MySqlDbType.String).Value = pUsuario.Usuario1;
                    cmd.Parameters.Add("@pPassword", MySqlDbType.String).Value = pUsuario.Password;
                    cmd.Parameters.Add("@pCodTipoUsuario", MySqlDbType.String).Value = pUsuario.Codtipousuario;
                    cmd.Parameters.Add("@pTelefonoCasa", MySqlDbType.String).Value = pUsuario.Telefonocasa;
                    cmd.Parameters.Add("@pCelular", MySqlDbType.String).Value = pUsuario.Celular;
                    cmd.Parameters.Add("@pCodEstado", MySqlDbType.String).Value = pUsuario.Codestado;
                    cmd.Parameters.Add("@pCodCargo", MySqlDbType.String).Value = pUsuario.Codcargo;


                    conn.Open();
                    int lResult;
                    lResult = cmd.ExecuteNonQuery();
                    return lResult;
                }
            }


        }
        public void DeleteUsuario(int pIdUsuario)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("usuario_delete_by_idusuario", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdUsuario", MySqlDbType.Int32).Value = pIdUsuario;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }

        }

        public void UpdateUsuario(ControlDePacientes.Entities.usuario pUsuario)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("usuario_updatebyidusuario", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdUsuario", MySqlDbType.Int32).Value = pUsuario.Idusuario;
                    cmd.Parameters.Add("@pUsuarioName", MySqlDbType.String).Value = pUsuario.Usuarioname;
                    cmd.Parameters.Add("@pNombreUsuario", MySqlDbType.String).Value = pUsuario.Nombreusuario;
                    cmd.Parameters.Add("@pApellidoUsuario", MySqlDbType.String).Value = pUsuario.Apellidousuario;
                    cmd.Parameters.Add("@pUsuario", MySqlDbType.String).Value = pUsuario.Usuario1;
                    cmd.Parameters.Add("@pPassword", MySqlDbType.String).Value = pUsuario.Password;
                    cmd.Parameters.Add("@pCodTipoUsuario", MySqlDbType.String).Value = pUsuario.Codtipousuario;
                    cmd.Parameters.Add("@pTelefonoCasa", MySqlDbType.String).Value = pUsuario.Telefonocasa;
                    cmd.Parameters.Add("@pCelular", MySqlDbType.String).Value = pUsuario.Celular;
                    cmd.Parameters.Add("@pCodEstado", MySqlDbType.String).Value = pUsuario.Codestado;
                    cmd.Parameters.Add("@pCodCargo", MySqlDbType.String).Value = pUsuario.Codcargo;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

    }
}
