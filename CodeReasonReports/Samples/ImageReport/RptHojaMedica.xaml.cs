using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;
using CodeReason.Reports;
using System.Windows;


namespace ImageReport
{
    /// <summary>
    /// Interaction logic for RptHojaMedica.xaml
    /// </summary>
    public partial class RptHojaMedica : Window
    {
        private bool _firstActivated = true;

        public RptHojaMedica()
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

                //FlowDocument lFlowDocument = new FlowDocument();
                //lFlowDocument.Blocks.Add(lParagraph);
                //this.Content = lFlowDocument;
                //PrintDialog printDlg = new PrintDialog();
                //IDocumentPaginatorSource idpSource = lFlowDocument;

                //printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");

               
                
                ReportDocument reportDocument = new ReportDocument();
                reportDocument.ImageProcessing += reportDocument_ImageProcessing;
                reportDocument.ImageError += reportDocument_ImageError;
               

                StreamReader reader = new StreamReader(new FileStream(@"Templates\ImageReport.xaml", FileMode.Open, FileAccess.Read));
                reportDocument.XamlData = reader.ReadToEnd();
                reportDocument.XamlImagePath = Path.Combine(Environment.CurrentDirectory, @"Templates\");
                reader.Close();

                ReportData data = new ReportData();

                // set constant document values
                data.ReportDocumentValues.Add("PrintDate", DateTime.Now); // print date is now

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

        /// <summary>
        /// Event occurs for each image before it is processed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">image error event details</param>
        private void reportDocument_ImageError(object sender, ImageErrorEventArgs e)
        {
            e.Handled = true; // just suppress exceptions
        }

        /// <summary>
        /// Event occurs for each image before it is processed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">image event details</param>
        private void reportDocument_ImageProcessing(object sender, ImageEventArgs e)
        {
            System.Drawing.Bitmap bitmap = null;

            if (e.Image.Name == "imageDynamic1")
            {
                // create image dynamically
                bitmap = new System.Drawing.Bitmap(100, 100);
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb((byte)(x + y), (byte)(x + y), 0));
                    }
                }
            }

            if (e.Image.Name == "imageDynamic2")
            {
                // create image dynamically
                bitmap = new System.Drawing.Bitmap(100, 100);
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb((byte)(x), (byte)(y), 0));
                    }
                }
            }

            if (bitmap != null)
            {
                // save this image into a memory stream
                MemoryStream mem = new MemoryStream();
                bitmap.Save(mem, System.Drawing.Imaging.ImageFormat.Bmp);
                mem.Position = 0;

                // load new media image into report
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = mem;
                image.EndInit();
                e.Image.Source = image;
            }
        }
    }
}
