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
    public partial class LOGOUT : Form
    {
        public bool IsConfirmed { get; private set; } = false;
        public LOGOUT()
        {
            InitializeComponent();
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {
            IsConfirmed = true;
            this.Close();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            IsConfirmed = false;
            this.Close();
        }
    }
}
