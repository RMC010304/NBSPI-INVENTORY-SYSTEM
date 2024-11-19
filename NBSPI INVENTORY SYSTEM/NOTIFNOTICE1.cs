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
    public partial class NOTIFNOTICE1 : Form
    {

        private HMADD _hMADD;

        public NOTIFNOTICE1(HMADD hMADD)
        {
            InitializeComponent();

            _hMADD = hMADD;
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {
            _hMADD.Close();
            this.Close();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
