using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using Microsoft.Win32;

namespace ControlDePacientes.Logic
{
    /// <summary>
    /// Clase que sirve para realizar validaciones en los controles de la interfaz gráfica
    /// </summary>
    public static class ControlOperation
    {
        /// <summary>
        /// Descripcion: Valida un control de tipo combo que es obligatorio
        /// Autor:Omar Chiodin
        /// Fecha:19/10/2011
        /// </summary>        
        /// <param name="pComboBox">Cadena con el nombre del combo o contenido de la etiqueta que lo identifica</param>
        /// <returns>Verdadero sino existe activación de validación</returns>
        public static bool validateComboBox(ComboBox pComboBox)
        {
            if (pComboBox == null)
                return false;

            if (pComboBox.Items.Count == 0)
                return false;

            if (pComboBox.SelectedItem == null)
                return false;

            if (pComboBox.SelectedValue.ToString() == "-1")
                return false;

            return true;
        }

        /// <summary>
        /// Presenta el mensaje de confirmación en pantalla
        /// </summary>
        /// <param name="pMessage">Mensaje que se va a presentar en el sistema</param>
        /// <param name="pTitle">Título del mensaje</param>
        /// <returns>Retorna resultado de la pregunta</returns>
        /// Autor:Omar Chiodin
        /// Fecha:19/10/2011
        public static MessageBoxResult alertConfirm(string pMessage)
        {
            MessageBoxResult lResult = Microsoft.Windows.Controls.MessageBox.Show(pMessage, "Alerta del Sistema", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return lResult;
        }

        /// <summary>
        /// Presenta el mensaje de información en pantalla
        /// </summary>
        /// <param name="pMessage">Mensaje que se va a presentar en el sistema</param>
        /// <param name="pTitle">Título del mensaje</param>
        /// Autor:Omar Chiodin
        /// Fecha:19/10/2011
        public static void alertInformation(string pMessage)
        {
            Microsoft.Windows.Controls.MessageBox.Show(pMessage, "Información del Sistema", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Presenta el mensaje de warning o alerta en pantalla
        /// </summary>
        /// <param name="pMessage">Mensaje de validación >Excepción presentada en el sistema</param>
        /// <param name="pTitle">Título del mensaje</param>
        /// Autor:Omar Chiodin
        /// Fecha:19/10/2011
        public static void alertWarning(string pMessage)
        {
            Microsoft.Windows.Controls.MessageBox.Show(pMessage, "Validación del Sistema", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Presenta el mensaje de pregunta en pantalla
        /// </summary>
        /// <param name="pMessage">Mensaje de validación >Excepción presentada en el sistema</param>
        /// <param name="pTitle">Título del mensaje</param>
        /// Autor:Omar Chiodin
        /// Fecha:19/10/2011
        public static void QuestionWarning(string pMessage)
        {
            Microsoft.Windows.Controls.MessageBox.Show(pMessage, "Pregunta del Sistema", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        /// <summary>
        /// Presenta el mensaje de error en pantalla
        /// </summary>
        /// <param name="pIdLog">Identificador del error producido</param>
        /// Autor:Omar Chiodin
        /// Fecha:19/10/2011
        private static void alertError(int pIdLog)
        {
            Microsoft.Windows.Controls.MessageBox.Show(String.Format("Se ha presentado un error por favor consulte con el Administrador. El Id es: {0}", pIdLog.ToString()), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private static void AddRowComboBox(DataTable pDt)
        {
            DataRow lDr = pDt.NewRow();
            lDr[0] = 0;
            lDr[1] = "-- Seleccione --";
            pDt.Rows.InsertAt(lDr, 0);


        }

        /// <summary>
        /// Funcion que carga imagen de la base de datos
        /// </summary>
        /// <param name="pFotoByte">Byte que contiene la foto</param>
        /// <param name="pImageControl">Control donde se quiere cargar la foto</param>
        /// <returns></returns>
        public static bool LoadImageFromDB(byte[] pFotoByte, System.Windows.Controls.Image pImageControl)
        {
            try
            {
                BitmapImage bi = new BitmapImage();
                bi = (BitmapImage)ByteToImage(pFotoByte);
                pImageControl.Source = bi;

                return true;
            }
            catch (Exception ex)
            {
                alertInformation(ex.Message);
                return false;
            }

        }

        public static ImageSource ByteToImage(byte[] imageData)
        {

            byte[] blob = (byte[])imageData;
            MemoryStream stream = new MemoryStream();
            stream.Write(blob, 0, blob.Length);
            stream.Position = 0;

            System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms;
            bi.EndInit();
           
            return bi;
        }
    }
}
