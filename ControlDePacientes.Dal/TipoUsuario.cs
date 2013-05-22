using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ControlDePacientes.Dal
{
    public class TipoUsuario
    {
        public string connStr =
            ConfigurationManager.ConnectionStrings[
                "ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;


        public int AddTipoUsuario(ControlDePacientes.Entities.tipousuario pTipoUsuarioName)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("tipoUsuario_add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pTipoUsuarioName", MySqlDbType.String).Value = pTipoUsuarioName.Tipousuarioname;

                    conn.Open();
                    int lResult;
                    lResult = cmd.ExecuteNonQuery();
                    return lResult;
                }
            }


        }

        public DataTable GetTipoUsuario()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("tipousuario_getAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public void DeleteTipoUsuario(int pIdTipoUsuario)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("tipousuario_delete_by_idtipousuario", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdTipoUsuario", MySqlDbType.Int32).Value = pIdTipoUsuario;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }

        }

        public void UpdateTipoUsuario(ControlDePacientes.Entities.tipousuario pTipoUsuario)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("tipousuario_update_by_idtipousuario", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdTipoUsuario", MySqlDbType.Int32).Value = pTipoUsuario.Idtipousuario;
                    cmd.Parameters.Add("@pTipoUsuarioName", MySqlDbType.String).Value = pTipoUsuario.Tipousuarioname;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
