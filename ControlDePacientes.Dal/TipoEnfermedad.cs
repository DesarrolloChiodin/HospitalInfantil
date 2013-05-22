using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ControlDePacientes.Dal
{
   public class TipoEnfermedad
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;

        public DataTable TipoEnfermedadGetAll()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("tipoenfermedad_getall", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public int AddTipoEnfermedad(ControlDePacientes.Entities.TipoEnfermedad pTipoEnfermedad)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("tipoenfermedad_add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pTipoEnfermedadName", MySqlDbType.String).Value = pTipoEnfermedad.Tipoenfermedadname;
                   
                    conn.Open();
                    int lResult;
                    lResult = cmd.ExecuteNonQuery();
                    return lResult;
                }
            }


        }

        public void DeleteTipoEnfermedad(int pIdTipoEnfermedad)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("tipoenfermedad_delete_by_idtipoenfermedad", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdTipoEnfermedad", MySqlDbType.Int32).Value = pIdTipoEnfermedad;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }

        }

       public void UpdateTipoEnfermedad(ControlDePacientes.Entities.TipoEnfermedad pTipoEnfermedad)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("tipoenfermedad_update_by_idtipoenfermedad", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdTipoEnfermedad", MySqlDbType.Int32).Value = pTipoEnfermedad.Idtipoenfermedad;
                    cmd.Parameters.Add("@pTipoEnfermedadName", MySqlDbType.String).Value = pTipoEnfermedad.Tipoenfermedadname;
                   

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }




    }
}
