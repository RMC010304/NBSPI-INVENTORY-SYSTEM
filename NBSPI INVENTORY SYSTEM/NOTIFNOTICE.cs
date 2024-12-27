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
    public partial class NOTIFNOTICE : Form
    {
        private ITADD _iTADD;

        public NOTIFNOTICE(ITADD iTADD)
        {
            InitializeComponent();

            _iTADD = iTADD;

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {
            
            _iTADD.Close(); 
            this.Close();


        }
    }
}
