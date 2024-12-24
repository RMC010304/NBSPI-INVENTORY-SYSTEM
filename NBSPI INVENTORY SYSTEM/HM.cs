using RJCodeAdvance.RJControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using DGVPrinterHelper;
using System.Drawing.Printing;
using Mysqlx.Crud;

namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class HM : UserControl
    {

        SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;Integrated Security=True");
        DataSet ds = new DataSet();

        bool isDescending = true;
        public HM()
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

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            doubleBufferedPanel1.Hide();
            doubleBufferedPanel2.Hide();
            GetItems();
        }

        public void GetItems()
        {

            DataTable dt = new DataTable();
            string orderBy = isDescending ? "DESC" : "ASC";

            // Modify query to order by DATE column in descending order
            using (SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.ITEMS ORDER BY DATE {orderBy}", con))
            {
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
            }

            dataGridView5.DataSource = dt;

        }     

        public void GetItems2()
        {


            DataTable dt = new DataTable();
            string orderBy = isDescending ? "DESC" : "ASC";

            using (SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.ITEMS ORDER BY DATE {orderBy}", con))
            {
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
            }

            dataGridView1.DataSource = dt;
       

        }

        private void rjButton22_Click(object sender, EventArgs e)
        {

            ITADD iTADD = new ITADD(this, "", 0, "", "", "");
            iTADD.Show();

        }

        private void HM_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 7, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(93, 79, 162);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Disable visual styles
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView5.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 10, FontStyle.Bold);
            dataGridView5.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView5.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            // Disable visual styles
            dataGridView5.EnableHeadersVisualStyles = false;
        }


        private void rjButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void Sort(string value)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd;
            DataView dv;

            string sql = "SELECT * FROM dbo.ITEMS";
            cmd = new SqlCommand(sql, con);
            adapter.SelectCommand = cmd;

            ds.Clear();
            adapter.Fill(ds);
            dv = new DataView(ds.Tables[0]);
            dv.Sort = value;

            dataGridView5.DataSource = dv;
            dataGridView1.DataSource = dv;


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

        private void rjButton5_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel2.Visible = !doubleBufferedPanel2.Visible;

            doubleBufferedPanel1.Hide();

            GetItems();
            GetItems2();
        }

        private void rjButton4_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel1.Visible = !doubleBufferedPanel1.Visible;

            doubleBufferedPanel2.Hide();

            GetItems();
            GetItems2();
        }

        private void rjButton6_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand(@"
              SELECT * FROM dbo.ITEMS
              WHERE DATE >= @date1 AND DATE < DATEADD(DAY, 1, @date2)", con);

            // Set parameters with truncated time (only dates)
            cmd.Parameters.AddWithValue("@date1", rjDatePicker1.Value.Date);
            cmd.Parameters.AddWithValue("@date2", rjDatePicker2.Value.Date);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Bind data to DataGridView
            dataGridView5.DataSource = dt;
            dataGridView1.DataSource = dt;

            doubleBufferedPanel2.Hide();
        }

        private void rjButton7_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(@"
              SELECT * FROM dbo.ITEMS
              WHERE DATE2 >= @date1 AND DATE2 < DATEADD(DAY, 1, @date2)", con);

            // Set parameters with truncated time (only dates)
            cmd.Parameters.AddWithValue("@date1", rjDatePicker4.Value.Date);
            cmd.Parameters.AddWithValue("@date2", rjDatePicker3.Value.Date);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Bind data to DataGridView
            dataGridView5.DataSource = dt;
            dataGridView1.DataSource = dt;

            doubleBufferedPanel2.Hide();
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

            printer.Title = "HM LABORATORY";
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

        private void rjTextBox1__TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da;
            DataTable dt;

            using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678"))
            {
                con.Open();


                string query = @"SELECT * FROM dbo.ITEMS  WHERE CONCAT(ID, ITEM, BRAND, MODEL, CATEGORY, QUANTITY) LIKE @searchText";


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

                ITDELETE deleteForm = new ITDELETE(this, itemId);


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

                ITUPDATE iTUPDATE = new ITUPDATE(this, item, id, brand, model, category, date, quantity);
                iTUPDATE.Show();

                GetItems();
            }
        }

        private void rjComboBox1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Sort(rjComboBox1.SelectedItem.ToString());

            doubleBufferedPanel1.Hide();
        }

        private void rjButton8_Click_1(object sender, EventArgs e)
        {
            isDescending = !isDescending;

            // Load the items with the new sorting order
            GetItems();
            GetItems2();
        }
    }
}
