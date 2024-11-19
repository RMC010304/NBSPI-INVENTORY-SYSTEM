using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class NOTIFNOTICE3 : Form
    {
        private SPORTSADD _spADD;

        public NOTIFNOTICE3(SPORTSADD sPADD)
        {
            InitializeComponent();

            _spADD = sPADD;
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {
            _spADD.Close();
            this.Close();
        }
    }
}
