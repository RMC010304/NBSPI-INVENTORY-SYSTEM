﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class SPORTSUPDATE : Form
    {
        string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";

        int no;
        string number, thing, br, mod, cat,description;
        byte[] photo;

        SPORTS hm;
        public SPORTSUPDATE(SPORTS control,string item, string id, string brand, string model, string category, string date, int quantity, string desc, byte[] img)
        {
            InitializeComponent();

            hm = control;

            number = id;
            // no = quantity;
            thing = item;
            br = brand;
            mod = model;
            cat = category;
            description = desc;
            photo = img;

            label2.Text = quantity.ToString();

            rjTextBox4.Texts = description;
            if (photo != null)
            {
                using (var ms = new MemoryStream(photo))
                {
                    pictureBox2.Image = Image.FromStream(ms);
                }
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            //string id = number;
           // int quantity = no;

           // SPORTSCHANGE iTCHANGE = new SPORTSCHANGE(id, quantity);
           // iTCHANGE.Show();
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            string id = number;
            int quantity = no;

            SPORTSADDSTOCK iSTOCK = new SPORTSADDSTOCK(id, quantity);
            iSTOCK.Show();
        }

        private void rjButton3_Click(object sender, EventArgs e)
        {
            //string id = number;
            //int quantity = no;

           // SPORTSREDUCE iREDUCE = new SPORTSREDUCE(id, quantity);
           // iREDUCE.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            String imageLocation = "";
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Get the image file path.
                        imageLocation = openFileDialog.FileName;

                        // Display the image in the PictureBox.
                        pictureBox2.ImageLocation = imageLocation;

                        // Convert the selected image into a byte array.
                        using (FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                fs.CopyTo(ms);
                                photo = ms.ToArray();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {
            number = textBox1.Text;
            thing = rjTextBox1.Texts;
            br = rjTextBox2.Texts;
            mod = rjTextBox3.Texts;
            //no = Convert.ToInt32(rjTextBox4.Texts);
            //day = rjDatePicker1.Text;          
            cat = rjComboBox1.Texts;
            description = rjTextBox4.Texts;

            // Convert the image in PictureBox to byte[]
            if (pictureBox2.Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    pictureBox2.Image.Save(ms, pictureBox2.Image.RawFormat);
                    photo = ms.ToArray();
                }
            }
            else
            {
                photo = null;
            }

            DateTime day = DateTime.Now;


            SqlConnection con = new SqlConnection(conn);
            con.Open();

            string query = "UPDATE SPORTS SET ITEM=@ITEM, BRAND=@BRAND, MODEL=@MODEL, CATEGORY=@CATEGORY, DATE2=@DATE2, DESCRIPTION=@DESCRIPTION, PHOTO=@PHOTO WHERE ID =@ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", number);
            cmd.Parameters.AddWithValue("@ITEM", thing);
            cmd.Parameters.AddWithValue("@BRAND", br);
            cmd.Parameters.AddWithValue("@MODEL", mod);
            // cmd.Parameters.AddWithValue("@QUANTITY", no);
            cmd.Parameters.AddWithValue("@DATE2", rjDatePicker1.Value);
            cmd.Parameters.AddWithValue("@CATEGORY", cat);
            cmd.Parameters.AddWithValue("@DESCRIPTION", description);
            cmd.Parameters.AddWithValue("@PHOTO", (object)photo ?? DBNull.Value);

            int result = cmd.ExecuteNonQuery();

            if (result > 0)
            {
                hm.GetItems();
                NOTIFUPDATE nOTIFUPDATE = new NOTIFUPDATE();
                nOTIFUPDATE.Show();
                this.Close();
              
            }

            con.Close();
        }


        private void SPORTSUPDATE_Load(object sender, EventArgs e)
        {
            textBox1.Text = number.ToString();
            rjTextBox1.Texts = thing;
            // rjTextBox4.Texts = no.ToString();
            // rjDatePicker1.Text = day;
            rjTextBox2.Texts = br;
            rjTextBox3.Texts = mod;
            rjComboBox1.Texts = cat;
            rjTextBox4.Texts = description;

            if (photo != null)
            {
                using (var ms = new MemoryStream(photo))
                {
                    pictureBox2.Image = Image.FromStream(ms);
                }
            }


            rjButton1.Hide();
            rjButton3.Hide();
            rjDatePicker1.Hide();
        }

        public void UpdateQuantity(int newQuantity)
        {
            label2.Text = newQuantity.ToString();
        }

        public void UpdateDate(DateTime newDate)
        {
            rjDatePicker1.Value = newDate;
        }
    }
}
