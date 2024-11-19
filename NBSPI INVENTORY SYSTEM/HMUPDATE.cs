﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class HMUPDATE : Form
    {

        string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";

        int no;
        string number, thing, br, mod, cat;

        IT hm;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HMUPDATE_Load(object sender, EventArgs e)
        {
            textBox1.Text = number.ToString();
            rjTextBox1.Texts = thing;
            // rjTextBox4.Texts = no.ToString();
            // rjDatePicker1.Text = day;
            rjTextBox2.Texts = br;
            rjTextBox3.Texts = mod;
            rjComboBox1.Texts = cat;
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

            DateTime day = DateTime.Now;


            SqlConnection con = new SqlConnection(conn);
            con.Open();

            string query = "UPDATE IT SET ITEM=@ITEM, BRAND=@BRAND, MODEL=@MODEL, CATEGORY=@CATEGORY, DATE2=@DATE2 WHERE ID =@ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", number);
            cmd.Parameters.AddWithValue("@ITEM", thing);
            cmd.Parameters.AddWithValue("@BRAND", br);
            cmd.Parameters.AddWithValue("@MODEL", mod);
            // cmd.Parameters.AddWithValue("@QUANTITY", no);
            cmd.Parameters.AddWithValue("@DATE2", rjDatePicker1.Value);
            cmd.Parameters.AddWithValue("@CATEGORY", cat);
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

        public HMUPDATE(IT control,string item, string id, string brand, string model, string category, string date, int quantity)
        {
            InitializeComponent();

            hm = control;

            number = id;
            // no = quantity;
            thing = item;
            br = brand;
            mod = model;
            cat = category;

            label2.Text = quantity.ToString();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            string id = number;
            int quantity = no;

            HMCHANGE iTCHANGE = new HMCHANGE(id, quantity);
            iTCHANGE.Show();
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            string id = number;
            int quantity = no;

            HMADDSTOCK iSTOCK = new HMADDSTOCK(id, quantity);
            iSTOCK.Show();
        }

        private void rjButton3_Click(object sender, EventArgs e)
        {
            string id = number;
            int quantity = no;

            HMREDUCE iREDUCE = new HMREDUCE(id, quantity);
            iREDUCE.Show();
        }

        private void GetItems()
        {

            SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.IT", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();


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
