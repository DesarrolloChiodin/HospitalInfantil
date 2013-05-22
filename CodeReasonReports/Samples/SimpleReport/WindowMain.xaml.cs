/************************************************************************
 * Copyright: Hans Wolff
 *
 * License:  This software abides by the LGPL license terms. For further
 *           licensing information please see the top level LICENSE.txt 
 *           file found in the root directory of CodeReason Reports.
 *
 * Author:   Hans Wolff
 *
 ************************************************************************/

using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using CodeReason.Reports;

namespace SimpleReport
{
    /// <summary>
    /// Application's main form
    /// </summary>
    public partial class WindowMain : Window
    {
        private bool _firstActivated = true;

        /// <summary>
        /// Constructor
        /// </summary>
        public WindowMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Window has been activated
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event details</param>
        private void Window_Activated(object sender, EventArgs e)
        {
            if (!_firstActivated) return;

            _firstActivated = false;

            try
            {
                ReportDocument reportDocument = new ReportDocument();

                StreamReader reader = new StreamReader(new FileStream(@"Templates\SimpleReport.xaml", FileMode.Open, FileAccess.Read));
                reportDocument.XamlData = reader.ReadToEnd();
                reportDocument.XamlImagePath = Path.Combine(Environment.CurrentDirectory, @"Templates\");
                reader.Close();

                ReportData data = new ReportData();

                // set constant document values
                data.ReportDocumentValues.Add("PrintDate", DateTime.Now); // print date is now


                //*************PRUEBAS**************
                Run Run1 = new Run("Registro Médico No. ");
                Run Run2 = new Run("Fecha" + DateTime.Today.Date);

                Label lblRegistro = new Label();
                lblRegistro.Content = "99999";

                InlineUIContainer lInlineUiContainer = new InlineUIContainer();
                lInlineUiContainer.BaselineAlignment = BaselineAlignment.Bottom;
                lInlineUiContainer.Child = lblRegistro;

                Paragraph lParagraph = new Paragraph();
                lParagraph.Inlines.Add(Run1);
                lParagraph.Inlines.Add(lInlineUiContainer);
                lParagraph.Inlines.Add(Run2);
                //***********************************
                data.ReportDocumentValues.Add("label", lParagraph); // print date is now


                // sample table "Ean"
                DataTable table = new DataTable("Ean");
                table.Columns.Add("Position", typeof(string));
                table.Columns.Add("Item", typeof(string));
                table.Columns.Add("EAN", typeof(string));
                table.Columns.Add("Count", typeof(int));
                Random rnd = new Random(1234);
                for (int i = 1; i <= 100; i++)
                {
                    // randomly create some articles
                    table.Rows.Add(new object[] { i, "Item " + i.ToString("0000"), "123456790123", rnd.Next(9) + 1 });
                }
                data.DataTables.Add(table);

                DateTime dateTimeStart = DateTime.Now; // start time measure here

                XpsDocument xps = reportDocument.CreateXpsDocument(data);
                documentViewer.Document = xps.GetFixedDocumentSequence();

                // show the elapsed time in window title
                Title += " - generated in " + (DateTime.Now - dateTimeStart).TotalMilliseconds + "ms";
            }
            catch (Exception ex)
            {
                // show exception
                MessageBox.Show(ex.Message + "\r\n\r\n" + ex.GetType() + "\r\n" + ex.StackTrace, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
    }
}
