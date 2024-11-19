﻿using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class SPORTS : UserControl
    {
        SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;Integrated Security=True");
        DataSet ds = new DataSet();

        public SPORTS()
        {
            InitializeComponent();
            doubleBufferedPanel1.Hide();
            doubleBufferedPanel2.Hide();
            dataGridView1.Hide();
            GetItems();
            GetItems2();
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void label5_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void label10_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void label10_MouseLeave(object sender, EventArgs e)
        {
           
        }


        public void GetItems()
        {

            SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.SPORTS", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView5.DataSource = dt;
        }

        public void GetItems2()
        {


            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.SPORTS", con))
            {
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
            }

            dataGridView1.DataSource = dt;

        }

        private void rjButton1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            doubleBufferedPanel1.Hide();
            doubleBufferedPanel2.Hide();
            GetItems();
        }

        private void SPORTS_Load(object sender, EventArgs e)
        {

        }

        private void rjButton22_Click(object sender, EventArgs e)
        {
            SPORTSADD iTADD = new SPORTSADD(this, "", 0, "", "", "");
            iTADD.Show();
        }

        private void rjButton4_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel1.Visible = !doubleBufferedPanel1.Visible;

            doubleBufferedPanel2.Hide();

            GetItems();
            GetItems2();
        }

        private void rjButton5_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel2.Visible = !doubleBufferedPanel2.Visible;

            doubleBufferedPanel1.Hide();

            GetItems();
            GetItems2();
        }

        private void rjComboBox1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Sort(rjComboBox1.SelectedItem.ToString());

            doubleBufferedPanel1.Hide();
        }
        private void Sort(string value)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd;
            DataView dv;

            string sql = "SELECT * FROM dbo.SPORTS";
            cmd = new SqlCommand(sql, con);
            adapter.SelectCommand = cmd;

            ds.Clear();
            adapter.Fill(ds);
            dv = new DataView(ds.Tables[0]);
            dv.Sort = value;

            dataGridView5.DataSource = dv;
            dataGridView1.DataSource = dv;


        }

        private void rjButton6_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.SPORTS WHERE DATE BETWEEN @date1-1 AND @date2", con);
            cmd.Parameters.AddWithValue("date1", SqlDbType.DateTime).Value = rjDatePicker1.Value;
            cmd.Parameters.AddWithValue("date2", SqlDbType.DateTime).Value = rjDatePicker2.Value;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView5.DataSource = dt;
            dataGridView1.DataSource = dt;

            doubleBufferedPanel2.Hide();
        }

        private void rjButton7_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.SPORTS WHERE DATE2 BETWEEN @date1-1 AND @date2", con);
            cmd.Parameters.AddWithValue("date1", SqlDbType.DateTime).Value = rjDatePicker4.Value;
            cmd.Parameters.AddWithValue("date2", SqlDbType.DateTime).Value = rjDatePicker3.Value;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView5.DataSource = dt;
            dataGridView1.DataSource = dt;

            doubleBufferedPanel2.Hide();
        }

        private void rjTextBox1__TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da;
            DataTable dt;

            using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678"))
            {
                con.Open();


                string query = @"SELECT * FROM dbo.SPORTS  WHERE CONCAT(ID, ITEM, BRAND, MODEL, CATEGORY, QUANTITY) LIKE @searchText";


                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@searchText", "%" + rjTextBox1.Texts + "%");

                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                dataGridView5.DataSource = dt;
            }
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView5.Columns[e.ColumnIndex].HeaderText == "  ")
            {
                DataGridViewRow row = dataGridView5.Rows[e.RowIndex];
                string itemId = Convert.ToString(row.Cells["iDDataGridViewTextBoxColumn"].Value);

                SPORTSDELETE deleteForm = new SPORTSDELETE(this, itemId);


                deleteForm.Show();

                GetItems();


            }
            if (dataGridView5.Columns[e.ColumnIndex].HeaderText == " ")
            {
                int quantity;
                string id, item, brand, model, category, date;

                id = Convert.ToString(dataGridView5.Rows[e.RowIndex].Cells["iDDataGridViewTextBoxColumn"].Value);
                quantity = Convert.ToInt32(dataGridView5.Rows[e.RowIndex].Cells["qUANTITYDataGridViewTextBoxColumn"].Value);
                item = Convert.ToString(dataGridView5.Rows[e.RowIndex].Cells["iTEMDataGridViewTextBoxColumn"].Value);
                brand = Convert.ToString(dataGridView5.Rows[e.RowIndex].Cells["bRANDDataGridViewTextBoxColumn"].Value);
                model = Convert.ToString(dataGridView5.Rows[e.RowIndex].Cells["mODELDataGridViewTextBoxColumn"].Value);
                category = Convert.ToString(dataGridView5.Rows[e.RowIndex].Cells["cATEGORYDataGridViewTextBoxColumn"].Value);
                date = Convert.ToString(dataGridView5.Rows[e.RowIndex].Cells["dATEDataGridViewTextBoxColumn"].Value);

                SPORTSUPDATE iTUPDATE = new SPORTSUPDATE(this,item, id, brand, model, category, date, quantity);
                iTUPDATE.Show();

                GetItems();
            }
        }

        private void rjButton3_Click(object sender, EventArgs e)
        {
            Font extraBoldFont = new Font("Montserrat ExtraBold", 36);

            PrintDialog printDialog = new PrintDialog
            {
                AllowSomePages = true,
                AllowCurrentPage = true
            };

            PageSettings pageSettings = new PageSettings
            {
                Margins = new Margins(60, 60, 60, 60),
                Landscape = true,
                PaperSize = new PaperSize("A4", 827, 1169)

            };


            DGVPrinter printer = new DGVPrinter();

            printer.Title = "SPORTS EQUIPMENTS";
            printer.TitleSpacing = 29;
            printer.TitleFont = extraBoldFont;
            printer.TitleColor = Color.White;
            printer.TitleBackground = new SolidBrush(Color.FromArgb(93, 79, 162));


            printer.SubTitle = string.Format("Current Stock Overview and Management Report\n({0})", DateTime.Now.Date.ToLongDateString());
            printer.SubTitleFont = new Font("Segoe UI", 8, FontStyle.Bold);
            printer.SubTitleAlignment = StringAlignment.Center;
            printer.SubTitleColor = Color.DimGray;
            printer.SubTitleSpacing = 7;


            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PageNumberAlignment = StringAlignment.Far;
            printer.PageNumberFont = new Font("Segoe UI", 7, FontStyle.Bold);
            printer.PageNumberColor = Color.DimGray;
            printer.ShowTotalPageNumber = true;
            printer.PageNumberOnSeparateLine = true;


            printer.PorportionalColumns = true;
            printer.Footer = "   NBSPI INVENTORY MANAGEMENT SYSTEM";
            printer.FooterFont = new Font("Segoe UI", 8, FontStyle.Bold);
            printer.FooterSpacing = 16;
            printer.FooterAlignment = StringAlignment.Near;
            printer.FooterBackground = new SolidBrush(Color.Gray);
            printer.FooterColor = Color.White;
            printer.HeaderCellAlignment = StringAlignment.Center;

            printer.PrintMargins = new System.Drawing.Printing.Margins(60, 60, 60, 60);


            printer.PrintPreviewDataGridView(dataGridView1);
            GetItems2();
        }

        private void rjButton8_Click(object sender, EventArgs e)
        {
           
        }

        private void rjButton9_Click(object sender, EventArgs e)
        {
            GetItems();
            NOTIFRELOAD nOTIFRELOAD = new NOTIFRELOAD();
            nOTIFRELOAD.Show();
        }
    }
}