using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class alergias
    {
        private System.Int32 idalergias;
        public System.Int32 Idalergias
        {
            get
            {
                return idalergias;
            }
            set
            {
                idalergias = value;
            }
        }
        private System.String alergiasname;
        public System.String Alergiasname
        {
            get
            {
                return alergiasname;
            }
            set
            {
                alergiasname = value;
            }
        }
    }
}

