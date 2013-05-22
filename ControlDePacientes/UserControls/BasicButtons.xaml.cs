using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlDePacientes.UserControls
{
    /// <summary>
    /// Interaction logic for BasicButtons.xaml
    /// </summary>
    public partial class BasicButtons : UserControl
    {
        public BasicButtons()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public static readonly RoutedEvent BtnSaveClick = EventManager.RegisterRoutedEvent("BtnSaveClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BasicButtons));
        
        public static readonly RoutedEvent BtnCancelClick = EventManager.RegisterRoutedEvent("BtnCancelClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BasicButtons));
        
        public static readonly RoutedEvent BtnDeleteClick = EventManager.RegisterRoutedEvent("BtnDeleteClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BasicButtons));
        
        public event RoutedEventHandler ButtonSaveClick
        {
            add { AddHandler(BtnSaveClick, value); }
            remove { RemoveHandler(BtnSaveClick, value); }
        }

        public event RoutedEventHandler ButtonCancelClick
        {
            add { AddHandler(BtnCancelClick, value); }
            remove { RemoveHandler(BtnCancelClick, value); }
        }

        public event RoutedEventHandler ButtonDeleteClick
        {
            add { AddHandler(BtnDeleteClick, value); }
            remove { RemoveHandler(BtnDeleteClick, value); }
        }

        void RaiseBtnSaveClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(BasicButtons.BtnSaveClick);
            RaiseEvent(newEventArgs);
        }
        
        void RaiseBtnCancelClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(BasicButtons.BtnCancelClick);
            RaiseEvent(newEventArgs);
        }
        
        void RaiseBtnDeleteClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(BasicButtons.BtnDeleteClick);
            RaiseEvent(newEventArgs);
        }


        /// <summary>
        /// Metodo que muestra u oculta los botónes básicos
        /// </summary>
        /// <Author>Omar Chiodin</Author>
        /// <Date>21/10/2011</Date>
        /// <param name="pBtnSave">Para ocultar el botón guardar</param>
        /// <param name="pBtnCancel">Para ocultar el botón Cancelar</param>
        /// <param name="pBtnDelete">Para ocultar el botón Eliminar</param>
        public void ShowHideButtons(bool pBtnSave, bool pBtnCancel, bool pBtnDelete)
        {
            if (!pBtnSave)
                btnGuardar.Visibility = Visibility.Collapsed;

            if (!pBtnCancel)
                btnCancelar.Visibility = Visibility.Collapsed;

            if (!pBtnDelete)
                btnEliminar.Visibility = Visibility.Collapsed;

            if (pBtnSave)
                btnGuardar.Visibility = Visibility.Visible;

            if (pBtnCancel)
                btnCancelar.Visibility = Visibility.Visible;

            if (pBtnDelete)
                btnEliminar.Visibility = Visibility.Visible;
        }
        
        /// <summary>
        /// Metodo que guarda a la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            
            RaiseBtnSaveClickEvent();
        }

        /// <summary>
        /// Limpia contolres como ComboBox y TextBox
        /// </summary>
        /// <Author>Omar Chiodin</Author>
        /// <Date>21/10/2011</Date>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="pObj">Objeto que contiene los controles a limpiar</param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
           // LoopVisualTree(pObj);
            RaiseBtnCancelClickEvent();
        }
        
        /// <summary>
        /// metodo que limpia textbox y combobox
        /// </summary>
        /// <param name="obj"></param>
        void LoopVisualTree(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                if (obj is TextBox)
                    ((TextBox)obj).Text = null;
                if (obj is ComboBox)
                {
                    ((ComboBox)obj).SelectedIndex = 0;
                    ((ComboBox)obj).ItemsSource = null;
                }
                LoopVisualTree(VisualTreeHelper.GetChild(obj, i));
            }
        }

        /// <summary>
        /// Metodo que Elimina a la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            
            RaiseBtnDeleteClickEvent();
        }





    }
}
