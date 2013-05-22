using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ControlDePacientes.Dal
{

    public class Estados
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;

        public void  AddEstado(ControlDePacientes.Entities.Estado pEstado)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("estado_add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pEstadoName", MySqlDbType.String).Value = pEstado.Estadoname;
                    cmd.Parameters.Add("@pCodTipoEstado", MySqlDbType.String).Value = pEstado.Codtipoestado;
                    conn.Open();

                    cmd.ExecuteNonQuery();
                    
                }
            }


        }

        public void AddTipoEstado(ControlDePacientes.Entities.tipoestado ptipoestado)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("tipoestado_add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pTipoEstadoName", MySqlDbType.String).Value = ptipoestado.Tipoestadoname;
                    
                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }


        }

        public DataTable GetTipodEstado()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("tipoestado_getall", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    DataTable dt = new DataTable();
                    conn.Open();
                    
                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public DataTable GetEstado()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("estado_getall", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public void DeleteTipoEstado(int pIdTipoEstado)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("tipoestado_delete_by_idtipoestado", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdTipoEstado", MySqlDbType.Int32).Value = pIdTipoEstado;
                   
                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }

        }

        public void DeleteEstado(int pIdEstado)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("estado_delete_by_idestado", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdEstado", MySqlDbType.Int32).Value = pIdEstado;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }

        }

        public DataTable GetEstadoByTipoEstado(int pIdTipoEstado)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("estado_getby_tipoestado", conn);
                    cmd.Parameters.Add("@pIdTipoEstado", MySqlDbType.Int32).Value = pIdTipoEstado;

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
