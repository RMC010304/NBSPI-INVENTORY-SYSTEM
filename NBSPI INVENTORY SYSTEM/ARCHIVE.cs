using RJCodeAdvance.RJControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGVPrinterHelper;
using System.Drawing.Printing;

namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class ARCHIVE : UserControl
    {
        SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;Integrated Security=True");
        DataSet ds = new DataSet();

        bool isDescending = true;
        public ARCHIVE()
        {
            InitializeComponent();

            doubleBufferedPanel6.Hide();
            doubleBufferedPanel7.Hide();
            doubleBufferedPanel1.Hide();
            dataGridView3.Hide();
            GetItems();
        }

        private void ARCHIVE_Load(object sender, EventArgs e)
        {
            dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 7, FontStyle.Bold);
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(93, 79, 162);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Disable visual styles
            dataGridView3.EnableHeadersVisualStyles = false;

            dataGridView5.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 10, FontStyle.Bold);
            dataGridView5.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView5.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dataGridView5.Columns["pHOTODataGridViewImageColumn"].DefaultCellStyle.NullValue = null;

            // Disable visual styles
            dataGridView5.EnableHeadersVisualStyles = false;
        }

        public void GetItems()
        {

            DataTable dt = new DataTable();
            string orderBy = isDescending ? "DESC" : "ASC";

            using (SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.ARCHIVE ORDER BY DATE {orderBy}", conn))
            {
                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                conn.Close();
            }

            dataGridView5.DataSource = dt;
            dataGridView3.DataSource = dt;

        }

        private void rjButton9_Click(object sender, EventArgs e)
        {
            GetItems();
            NOTIFRELOAD nOTIFRELOAD = new NOTIFRELOAD();
            nOTIFRELOAD.Show();
        }

        private void rjTextBox1__TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da;
            DataTable dt;

            using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678"))
            {
                con.Open();


                string query = @"SELECT * FROM dbo.ARCHIVE  WHERE CONCAT([ITEM ID], ITEM, BRAND, MODEL, CATEGORY, QUANTITY) LIKE @searchText";


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
            if (dataGridView5.Columns[e.ColumnIndex].HeaderText == " ")
            {

                string archiveId = Convert.ToString(dataGridView5.Rows[e.RowIndex].Cells["iTEMIDDataGridViewTextBoxColumn"].Value);

                NOTIFRESTORE nOTIFRESTORE = new NOTIFRESTORE(this,archiveId);
                nOTIFRESTORE.Show();

            }
        }

        private void rjButton4_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel7.Visible = !doubleBufferedPanel7.Visible;

            doubleBufferedPanel6.Hide();
            doubleBufferedPanel1.Hide();

            GetItems();
        }

        private void rjButton5_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel6.Visible = !doubleBufferedPanel6.Visible;

            doubleBufferedPanel7.Hide();
            doubleBufferedPanel1.Hide();

            GetItems();
        }

        private void rjComboBox3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Sort(rjComboBox3.SelectedItem.ToString());

            doubleBufferedPanel7.Hide();
        }

        private void Sort(string value)
        {

            SqlConnection con = new SqlConnection(conn.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd;
            DataView dv;

            string sql = "SELECT * FROM dbo.ARCHIVE";
            cmd = new SqlCommand(sql, con);
            adapter.SelectCommand = cmd;

            ds.Clear();
            adapter.Fill(ds);
            dv = new DataView(ds.Tables[0]);
            dv.Sort = value;

            dataGridView5.DataSource = dv;
            dataGridView3.DataSource = dv;



        }

        private void rjButton17_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn.ConnectionString);
       
            SqlCommand cmd = new SqlCommand(@"
              SELECT * FROM dbo.ARCHIVE
              WHERE DATE >= @date1 AND DATE < DATEADD(DAY, 1, @date2)", con);

            // Set parameters with truncated time (only dates)
            cmd.Parameters.AddWithValue("@date1", rjDatePicker8.Value.Date);
            cmd.Parameters.AddWithValue("@date2", rjDatePicker7.Value.Date);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Bind data to DataGridView
            dataGridView5.DataSource = dt;
            dataGridView3.DataSource = dt;

            doubleBufferedPanel6.Hide();

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel1.Visible = !doubleBufferedPanel1.Visible;

            doubleBufferedPanel6.Hide();
            doubleBufferedPanel7.Hide();

            GetItems();
        }

        private void rjComboBox1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = rjComboBox1.SelectedItem.ToString();
            string prefix = "";

            // Map category to prefix
            switch (selectedCategory)
            {
                case "IT RESOURCES":
                    prefix = "IT";
                    break;
                case "HM LABORATORY":
                    prefix = "HM";
                    break;
                case "SCIENCE LABORATORY":
                    prefix = "SL";
                    break;
                case "SPORTS EQUIPMENT":
                    prefix = "SE";
                    break;
                default:
                    prefix = "";
                    break;
            }

            doubleBufferedPanel1.Hide();

            FilterDataGridViewByPrefix(prefix);
        }

        private void FilterDataGridViewByPrefix(string prefix)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;Integrated Security=True"))
            {
                string query = "SELECT * FROM ARCHIVE WHERE [ITEM ID] LIKE @prefix + '%'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);

                adapter.SelectCommand.Parameters.AddWithValue("@prefix", prefix);

                DataTable filteredTable = new DataTable();
                adapter.Fill(filteredTable);

                // Bind to DataGridView
                dataGridView5.DataSource = filteredTable;
                dataGridView3.DataSource = filteredTable;
            }
        }

        private void rjButton6_Click(object sender, EventArgs e)
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

            printer.Title = "ARCHIVE";
            printer.TitleSpacing = 29;
            printer.TitleFont = extraBoldFont;
            printer.TitleColor = Color.White;
            printer.TitleBackground = new SolidBrush(Color.FromArgb(93, 79, 162));


            printer.SubTitle = string.Format("Current Archive Items Overview Report\n({0})", DateTime.Now.Date.ToLongDateString());
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


            printer.PrintPreviewDataGridView(dataGridView3);
            GetItems();
        }

        private void rjButton8_Click(object sender, EventArgs e)
        {
            isDescending = !isDescending;

            // Load the items with the new sorting order
            GetItems();
          
        }
    }
}
