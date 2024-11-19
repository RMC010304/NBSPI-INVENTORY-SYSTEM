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
    public partial class NOTIFDENIED : Form
    {
        int x, y;
        public NOTIFDENIED()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            y -= 5;
            this.Location = new Point(x, y);
            if (y <= 960)
            {
                timer1.Stop();
                timer2.Start();

            }
        }
        int yy = 180;

        private void NOTIFDENIED_Load(object sender, EventArgs e)
        {
            Position();
        }

        private void Position()
        {
            int ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int ScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            x = ScreenWidth - this.Width - 25;
            y = ScreenHeight - this.Height;

            this.Location = new Point(x, y);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            yy--;
            if (yy <= 0)
            {
                y += 1;
                this.Location = new Point(x, y += 10);
                if (y > 1300)
                {
                    timer2.Stop();
                    yy = 100;

                    this.Close();


                }

            }
        }

       
    }
}
