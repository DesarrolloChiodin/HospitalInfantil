using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ControlDePacientes.Dal
{
    public class ModuloHijo
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;

        public DataTable GetModuloHijo()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("modulohijo_getAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }
        public int AddModuloHijo(ControlDePacientes.Entities.modulohijo pModuloHijo)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("modulohijo_add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pModuloHijoName", MySqlDbType.String).Value = pModuloHijo.Modulohijoname;
                    cmd.Parameters.Add("@pModuloHijoImagen", MySqlDbType.String).Value = pModuloHijo.Modulohijoimagen;
                    cmd.Parameters.Add("@pCodModulo", MySqlDbType.Int32).Value = pModuloHijo.Codmodulo;
                    cmd.Parameters.Add("@pModuloHijoPage", MySqlDbType.String).Value = pModuloHijo.Modulohijopage;


                    conn.Open();
                    int lResult;
                    lResult = cmd.ExecuteNonQuery();
                    return lResult;
                }
            }


        }
        public void DeleteModuloHijo(int pIdModuloHijo)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("modulohijo_delete", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdModuloHijo", MySqlDbType.Int32).Value = pIdModuloHijo;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }

        }
        public void UpdateModuloHijo(ControlDePacientes.Entities.modulohijo pModuloHijo)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("modulohijo_update", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdModuloHijo", MySqlDbType.Int32).Value = pModuloHijo.Idmodulohijo;
                    cmd.Parameters.Add("@pModuloHijoName", MySqlDbType.String).Value = pModuloHijo.Modulohijoname;
                    cmd.Parameters.Add("@pModuloHijoImagen", MySqlDbType.String).Value = pModuloHijo.Modulohijoimagen;
                    cmd.Parameters.Add("@pCodModulo", MySqlDbType.Int32).Value = pModuloHijo.Codmodulo;
                    cmd.Parameters.Add("@pModuloHijoPage", MySqlDbType.String).Value = pModuloHijo.Modulohijopage;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
