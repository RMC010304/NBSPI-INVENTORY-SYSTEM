using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class BORROWDETAILS3 : Form
    {
        public BORROWDETAILS3(string label1Text, int label2Text, string label3Text, string label12Text, string label13Text,
                             string label14Text, string label15Text, string label4Text, string label5Text, DateTime label6Text, string label7Text, byte[] photoData)
        {
            InitializeComponent();


            label1.Text = label1Text;
            label2.Text = label2Text.ToString();
            label3.Text = label3Text;
            label12.Text = label12Text;
            label13.Text = label13Text;
            label14.Text = label14Text;
            label15.Text = label15Text;
            label4.Text = label4Text;
            label5.Text = label5Text;
            label6.Text = label6Text.ToString("MM/dd/yyyy h:mm tt");

            label7.Text = label7Text;  // Assuming label7 is the Label control for Description

            // Display the photo in a PictureBox if photoData is not null
            if (photoData != null && photoData.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(photoData))
                {
                    pictureBox2.Image = Image.FromStream(ms);  // Assuming pictureBox1 is the PictureBox control
                }
            }
            else
            {
                pictureBox2.Image = null;  // No photo available
            }

        }

        private void BORROWDETAILS3_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
