using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;
using Control = System.Windows.Forms.Control;


namespace ControlDePacientes.Logic
{
    public static class ExtensionControl
    {
        public static IEnumerable<Control> GetAllControls(this Control Control)
        {
            List<Control> ctl = new List<Control>();

            Action<Control.ControlCollection> recursive = null;
            recursive = new Action<Control.ControlCollection>((a) =>
            {

                foreach (Control c in a)
                {
                    ctl.Add(c);
                    recursive(c.Controls);
                }

            });

            recursive(Control.Controls);
            return ctl;
        }
    }
 
}
