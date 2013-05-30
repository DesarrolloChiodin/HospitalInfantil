﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ControlDePacientes.Dal
{
   public class Alergias
    {
       public string connStr = ConfigurationManager.ConnectionStrings["ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;

       public DataTable GetAlergias()
       {
           using (MySqlConnection conn = new MySqlConnection(connStr))
           {
               {
                   //ejecuto sp para la consulta de Usuarios
                   //linea agregada
                   MySqlCommand cmd = new MySqlCommand("alergias_getAll", conn);
                   cmd.CommandType = CommandType.StoredProcedure;

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
