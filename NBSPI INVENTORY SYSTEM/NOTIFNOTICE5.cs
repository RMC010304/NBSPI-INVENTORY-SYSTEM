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
    public partial class NOTIFNOTICE5 : Form
    {
        private DAMAGEFORM _dAMAGEFORM;
        public NOTIFNOTICE5(DAMAGEFORM dAMAGEFORM)
        {
            InitializeComponent();
            _dAMAGEFORM = dAMAGEFORM;
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {
            _dAMAGEFORM.Close();
            this.Close();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
