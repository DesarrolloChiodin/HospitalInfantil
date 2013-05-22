using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ControlDePacientes.Dal
{
    public class Modulo
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;

        public DataTable GetModulo()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("modulo_getAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }
        public int AddModulo(ControlDePacientes.Entities.modulo pModulo)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("modulo_add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pModuloName", MySqlDbType.String).Value = pModulo.Moduloname;
                    cmd.Parameters.Add("@pCodTipoUsuario", MySqlDbType.Int32).Value = pModulo.Codtipousuario;

                    conn.Open();
                    int lResult;
                    lResult = cmd.ExecuteNonQuery();
                    return lResult;
                }
            }


        }
        public void DeleteModulo(int pIdModulo)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("modulo_delete_by_idmodulo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdModulo", MySqlDbType.Int32).Value = pIdModulo;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }

        }
        public void UpdateModulo(ControlDePacientes.Entities.modulo pModulo)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("modulo_update_by_idmodulo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdModulo", MySqlDbType.Int32).Value = pModulo.Idmodulo;
                    cmd.Parameters.Add("@pModuloName", MySqlDbType.String).Value = pModulo.Moduloname;
                    cmd.Parameters.Add("@pCodTipoUsuario", MySqlDbType.Int32).Value = pModulo.Codtipousuario;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }



    }
}
