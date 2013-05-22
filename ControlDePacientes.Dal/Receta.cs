using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ControlDePacientes.Dal
{
    public class Receta
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;

        public int AddReceta(ControlDePacientes.Entities.receta pReceta)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("receta_add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pRecetaName", MySqlDbType.String).Value = pReceta.Recetaname;
                    cmd.Parameters.Add("@pCodPaciente", MySqlDbType.Int32).Value = pReceta.Codpaciente;
                    cmd.Parameters.Add("@pFechaReceta", MySqlDbType.DateTime).Value = pReceta.Fechareceta;
                    cmd.Parameters.Add("@pCodEstado", MySqlDbType.Int32).Value = pReceta.Codestado;


                    conn.Open();
                    int lIdReceta = Convert.ToInt32(cmd.ExecuteScalar());
                    return lIdReceta;

                }
            }
        }


        public void DeleteRecetaAndRecetaDetalle(int pIdReceta)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("receta_deleteRecetaAndRecetaDetailByIdreceta", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdReceta", MySqlDbType.Int32).Value = pIdReceta;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }

        }

        public DataTable GetRecetaByParameter(string pRegistroMedico)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("receta_getbyparameters", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (pRegistroMedico == "")
                        cmd.Parameters.Add("@pRegistroMedico", MySqlDbType.String).Value = -1;
                    else
                        cmd.Parameters.Add("@pRegistroMedico", MySqlDbType.String).Value = pRegistroMedico;
                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public void UpdReceta(int pIdReceta, int pCodUsuarioEntrega)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("receta_updateEstado", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdReceta", MySqlDbType.String).Value = pIdReceta;
                    cmd.Parameters.Add("@pCodUsuarioEntrega", MySqlDbType.String).Value = pCodUsuarioEntrega;
                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }


        }
    }
}
