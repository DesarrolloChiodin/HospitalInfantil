using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ControlDePacientes.Dal
{
    public class RecetaDetalle
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;

        public int AddRecetaDetalle(ControlDePacientes.Entities.recetadetalle pRecetaDetalle)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("recetadetalle_add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pRecetaDetalleName", MySqlDbType.String).Value = pRecetaDetalle.Recetadetallename;
                    cmd.Parameters.Add("@pCodReceta", MySqlDbType.Int32).Value = pRecetaDetalle.Codreceta;
                    cmd.Parameters.Add("@pCantidad", MySqlDbType.Int32).Value = pRecetaDetalle.Cantidad;
                    cmd.Parameters.Add("@pCodMedicina", MySqlDbType.Int32).Value = pRecetaDetalle.Codmedicina;
                    cmd.Parameters.Add("@pIndicaciones", MySqlDbType.String).Value = pRecetaDetalle.Indicaciones;
                    


                    conn.Open();
                    int lIdRecetaDetalle = Convert.ToInt32(cmd.ExecuteScalar());
                    return lIdRecetaDetalle;

                }
            }
        }

        public DataTable GetRecetaDetalleByIdReceta(int pIdReceta)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("recetadetalle_getbyidreceta", conn);
                    cmd.Parameters.Add("@pCodReceta", MySqlDbType.Int32).Value = pIdReceta;
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }


        public void DeleteRecetaDetalle(int pIdRecetaDetalle)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("recetadetalle_deletebyidrecetadetalle", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdRecetaDetalle", MySqlDbType.Int32).Value = pIdRecetaDetalle;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }

        }

    }
}
