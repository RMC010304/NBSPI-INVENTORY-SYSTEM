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
    public partial class NOTIFNOTICE2 : Form
    {
        private SCIENCEADD _sADD;
        public NOTIFNOTICE2(SCIENCEADD sADD)
        {
            InitializeComponent();
            _sADD = sADD;
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {
            _sADD.Close();
            this.Close();
        }
    }
}
