using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ControlDePacientes.Dal
{
    public class LoginDal
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;


        public DataTable GetUser(string pUser, string pPassword)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("GetUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pUser", MySqlDbType.String).Value = pUser;
                    cmd.Parameters.Add("@pPassword", MySqlDbType.String).Value = pPassword;
                    
                    DataTable dt = new DataTable();
                    conn.Open();
                    
                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    
                    dt.Load(myDataReader);
                   
                    return dt;
                }
            }


        }




    }

}
