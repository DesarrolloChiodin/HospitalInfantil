using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ControlDePacientes.Dal
{
    public class Ocupacion
    {

        public string connStr = ConfigurationManager.ConnectionStrings["ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;

        public DataTable GetOcupacion()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("ocupacion_getall", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public int AddOcupacion(ControlDePacientes.Entities.ocupacion pOcupacion)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("ocupacion_add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pOcupacionName", MySqlDbType.String).Value = pOcupacion.Ocupacionname;
                    
                    conn.Open();
                    int lResult = cmd.ExecuteNonQuery();
                    return lResult;

                }
            }


        }

        public void DeleteOcupacion(int pIdOcupacion)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("ocupacion_delete_by_idocupacion", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdOcupacion", MySqlDbType.Int32).Value = pIdOcupacion;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }

        }

        public void UpdateOcupacion(ControlDePacientes.Entities.ocupacion pOcupacion)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("ocupacion_update_by_idocupacion", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdOcupacion", MySqlDbType.Int32).Value = pOcupacion.Idocupacion;
                    cmd.Parameters.Add("@pOcupacionName", MySqlDbType.String).Value = pOcupacion.Ocupacionname;
                    

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }



    
    }
}
