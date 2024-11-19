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
    public partial class NOTIFNOTICE4 : Form
    {
        private BORROW _bORROW;
        public NOTIFNOTICE4(BORROW bORROW)
        {
            InitializeComponent();
            _bORROW = bORROW;
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {
            _bORROW.Close();
            this.Close();
        }

        private void NOTIFNOTICE4_Load(object sender, EventArgs e)
        {

        }
    }
}
