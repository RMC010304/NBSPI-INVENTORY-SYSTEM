using Org.BouncyCastle.Crmf;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;
using DGVPrinterHelper;
using System.Drawing.Printing;
using RJCodeAdvance.RJControls;

namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class TRANSACTION : UserControl
    {

        string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";
        DataSet ds = new DataSet();

        public string BorrowerId { get; set; }
        public string BorrowerName { get; set; }
        public string DamagedItem { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; }

        private BORROW borrowForm;

        bool isDescending = true;

        public TRANSACTION()
        {
            InitializeComponent();

            GetItems();
            GetItems2();
            GetItems3();

            dataGridView3.Hide();
            dataGridView2.Hide();
            dataGridView6.Hide();

            doubleBufferedPanel2.Hide();
            doubleBufferedPanel3.Hide();
            doubleBufferedPanel6.Hide();
            doubleBufferedPanel5.Hide();
            doubleBufferedPanel8.Hide();
            doubleBufferedPanel9.Hide();

            borrowForm = new BORROW(this);
            borrowForm.OnStatusUpdated += BorrowForm_OnStatusUpdated;

        }

        private void BorrowForm_OnStatusUpdated()
        {
            // This method is called when the status of any item is updated in the BORROW form
            // Update your transaction form UI here to reflect the changes

            // Example: You can refresh the list of borrow records
       
            GetItems();
        }

        private void TRANSACTION_Load(object sender, EventArgs e)
        {
            dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 7, FontStyle.Bold);
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(93, 79, 162);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Disable visual styles
            dataGridView3.EnableHeadersVisualStyles = false;

            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 7, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(93, 79, 162);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Disable visual styles
            dataGridView2.EnableHeadersVisualStyles = false;

            dataGridView6.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 7, FontStyle.Bold);
            dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(93, 79, 162);
            dataGridView6.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Disable visual styles
            dataGridView6.EnableHeadersVisualStyles = false;

            dataGridView5.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 10, FontStyle.Bold);
            dataGridView5.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView5.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            // Disable visual styles
            dataGridView5.EnableHeadersVisualStyles = false;

            dataGridView4.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 10, FontStyle.Bold);
            dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView4.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            // Disable visual styles
            dataGridView4.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            // Disable visual styles
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        public void GetItems()
        {

            string orderBy = isDescending ? "DESC" : "ASC";

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string query = ($"SELECT * FROM dbo.BORROW ORDER BY DATE {orderBy}");
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView5.DataSource = dt;
                dataGridView3.DataSource = dt;
            }
        }

        public void GetItems2()
        {

            string orderBy = isDescending ? "DESC" : "ASC";

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string query = ($"SELECT * FROM dbo.DAMAGE ORDER BY DATE {orderBy}");
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView4.DataSource = dt;
                dataGridView6.DataSource = dt;
            }
        }

        public void GetItems3()
        {

            string orderBy = isDescending ? "DESC" : "ASC";

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string query = ($"SELECT * FROM dbo.[RETURN] ORDER BY DATE {orderBy}");
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView2.DataSource = dt;
            }
        }

        //returned
        private void rjButton1_Click(object sender, EventArgs e)
        {
            rjButton1.BackColor = Color.FromArgb(93, 79, 162);
            rjButton1.ForeColor = Color.White;
            rjButton22.BackColor = Color.White;
            rjButton22.ForeColor = Color.FromArgb(96, 96, 96);
            rjButton3.BackColor = Color.White;
            rjButton3.ForeColor = Color.FromArgb(96, 96, 96);

            doubleBufferedPanel4.BringToFront();
            doubleBufferedPanel4.Show();

            GetItems();
            GetItems2();
            GetItems3();

            doubleBufferedPanel2.Hide();
            doubleBufferedPanel3.Hide();
            doubleBufferedPanel5.Hide();
            doubleBufferedPanel6.Hide();
            doubleBufferedPanel8.Hide();
            doubleBufferedPanel9.Hide();
        }

        //variance
        private void rjButton3_Click(object sender, EventArgs e)
        {
            rjButton3.BackColor = Color.FromArgb(93, 79, 162);
            rjButton3.ForeColor = Color.White;
            rjButton22.BackColor = Color.White;
            rjButton22.ForeColor = Color.FromArgb(96, 96, 96);
            rjButton1.BackColor = Color.White;
            rjButton1.ForeColor = Color.FromArgb(96, 96, 96);

            doubleBufferedPanel7.BringToFront();
            doubleBufferedPanel7.Show();

            GetItems();
            GetItems2();
            GetItems3();

            doubleBufferedPanel2.Hide();
            doubleBufferedPanel3.Hide();
            doubleBufferedPanel5.Hide();
            doubleBufferedPanel6.Hide();
            doubleBufferedPanel8.Hide();
            doubleBufferedPanel9.Hide();
        }

        //borrow
        private void rjButton22_Click(object sender, EventArgs e)
        {
            rjButton22.BackColor = Color.FromArgb(93, 79, 162);
            rjButton22.ForeColor = Color.White;
            rjButton1.BackColor = Color.White;
            rjButton1.ForeColor = Color.FromArgb(96, 96, 96);
            rjButton3.BackColor = Color.White;
            rjButton3.ForeColor = Color.FromArgb(96, 96, 96);

            doubleBufferedPanel1.BringToFront();
            doubleBufferedPanel1.Show(); 

            GetItems();
            GetItems2();
            GetItems3();

            doubleBufferedPanel2.Hide();
            doubleBufferedPanel3.Hide();
            doubleBufferedPanel5.Hide();
            doubleBufferedPanel6.Hide();
            doubleBufferedPanel8.Hide();
            doubleBufferedPanel9.Hide();
        }

        private void rjButton7_Click(object sender, EventArgs e)
        {

            BORROW borrow = new BORROW(this);
            borrow.Show();
        }

        //borrow

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView5.Columns[e.ColumnIndex].HeaderText == " ")
            {
                DataGridViewRow selectedRow = dataGridView5.Rows[e.RowIndex];

                int label2Text = 0;
                DateTime label6Text = DateTime.MinValue;
                DateTime label7Text = DateTime.MinValue;

                string label1Text = selectedRow.Cells["nAMEDataGridViewTextBoxColumn1"].Value?.ToString() ?? "";
                int.TryParse(selectedRow.Cells["qUANTITYDataGridViewTextBoxColumn1"].Value?.ToString(), out label2Text);
                string label3Text = selectedRow.Cells["iDDataGridViewTextBoxColumn1"].Value?.ToString();
                string label12Text = selectedRow.Cells["iTEMIDDataGridViewTextBoxColumn1"].Value?.ToString();
                string label13Text = selectedRow.Cells["iTEMDataGridViewTextBoxColumn1"].Value?.ToString();
                string label14Text = selectedRow.Cells["bRANDDataGridViewTextBoxColumn1"].Value?.ToString();
                string label15Text = selectedRow.Cells["mODELDataGridViewTextBoxColumn1"].Value?.ToString();
                string label4Text = selectedRow.Cells["cATEGORYDataGridViewTextBoxColumn1"].Value?.ToString();
                string label5Text = selectedRow.Cells["sTATUSDataGridViewTextBoxColumn1"].Value?.ToString();
                DateTime.TryParse(selectedRow.Cells["dATEDataGridViewTextBoxColumn1"].Value?.ToString(), out label6Text);
                DateTime.TryParse(selectedRow.Cells["dATE2DataGridViewTextBoxColumn1"].Value?.ToString(), out label7Text);

                Color statusColor = Color.FromArgb(255, 194, 111);

                if (label5Text == "UNRETURNED")
                {
                    statusColor = Color.FromArgb(255, 194, 111);
                }
                else if (label5Text == "OVERDUE")
                {
                    statusColor = Color.FromArgb(207, 74, 74);
                }

                BORROWDETAILS borrowDetails = new BORROWDETAILS(
                    label1Text, label2Text, label3Text, label12Text, label13Text,
                    label14Text, label15Text, label4Text, label5Text, label6Text, label7Text, statusColor);

                borrowDetails.ShowDialog();
            }

            if(dataGridView5.Columns[e.ColumnIndex].HeaderText == "  ")
            {
                DataGridViewRow selectedRow = dataGridView5.Rows[e.RowIndex];
                string borrowId = selectedRow.Cells["iDDataGridViewTextBoxColumn1"].Value.ToString();
                string itemId = selectedRow.Cells["iTEMIDDataGridViewTextBoxColumn1"].Value.ToString();

                // Check if the item belongs to the IT category based on its ID
                if (itemId.StartsWith("HM") || itemId.StartsWith("SL") || itemId.StartsWith("SE")) // Assuming IT items have IDs starting with "IT"
                {
                    // Handle direct return for IT items
                    // For non-IT items, show the BORROWRETURN form
                    int borrowedQuantity = 0;
                    object quantityValue = selectedRow.Cells["qUANTITYDataGridViewTextBoxColumn1"].Value;

                    if (quantityValue != DBNull.Value)
                    {
                        borrowedQuantity = Convert.ToInt32(quantityValue);
                    }

                    BORROWRETURN bORROWRETURN = new BORROWRETURN(this, borrowId, itemId, borrowedQuantity);
                    bORROWRETURN.Show();

                }
                else
                {

                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        con.Open();

                        // Retrieve the borrowed item details
                        string getBorrowDetailsQuery = "SELECT [ITEM], BRAND, MODEL, DESCRIPTION, PHOTO FROM BORROW WHERE ID = @BorrowId";
                        SqlCommand getBorrowDetailsCmd = new SqlCommand(getBorrowDetailsQuery, con);
                        getBorrowDetailsCmd.Parameters.AddWithValue("@BorrowId", borrowId);

                        SqlDataReader reader = getBorrowDetailsCmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string itemName = reader["ITEM"].ToString();
                            string brand = reader["BRAND"].ToString();
                            string model = reader["MODEL"].ToString();
                            string description = reader["DESCRIPTION"].ToString();
                            byte[] photo = reader["PHOTO"] as byte[];

                            // Insert the item back into the IT table
                            string insertToITQuery = "INSERT INTO IT (ID, ITEM, BRAND, MODEL,STATUS, DESCRIPTION, PHOTO,DATE) VALUES (@ItemId, @Item, @Brand, @Model,@status, @Description, @Photo,@date)";
                            SqlCommand insertToITCmd = new SqlCommand(insertToITQuery, con);
                            insertToITCmd.Parameters.AddWithValue("@ItemId", itemId);
                            insertToITCmd.Parameters.AddWithValue("@Item", itemName);
                            insertToITCmd.Parameters.AddWithValue("@Brand", brand);
                            insertToITCmd.Parameters.AddWithValue("@Model", model);
                            insertToITCmd.Parameters.AddWithValue("@status", "AVAILABLE");
                            insertToITCmd.Parameters.AddWithValue("@Description", description);
                            insertToITCmd.Parameters.AddWithValue("@Photo", (object)photo ?? DBNull.Value);
                            insertToITCmd.Parameters.AddWithValue("@date", DateTime.Now);



                            reader.Close();
                            insertToITCmd.ExecuteNonQuery();

                            // Remove the item from the BORROW table
                            string deleteFromBorrowQuery = "DELETE FROM BORROW WHERE ID = @BorrowId";
                            SqlCommand deleteFromBorrowCmd = new SqlCommand(deleteFromBorrowQuery, con);
                            deleteFromBorrowCmd.Parameters.AddWithValue("@BorrowId", borrowId);
                            deleteFromBorrowCmd.ExecuteNonQuery();

                            GetItems();
                            NOTIFRETURNED nOTIFSUCCESS = new NOTIFRETURNED();
                            nOTIFSUCCESS.Show();
                        }
                    }
                }
            }
            





        }


        //

        private void doubleBufferedPanel2_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void rjButton9_Click(object sender, EventArgs e)
        {

        }


        //borrow

        private void rjButton4_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel3.Visible = !doubleBufferedPanel3.Visible;

            doubleBufferedPanel2.Hide();

            GetItems();
            GetItems2();
            GetItems3();

        }

        private void rjButton5_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel2.Visible = !doubleBufferedPanel2.Visible;

            doubleBufferedPanel3.Hide();

            GetItems();
            GetItems2();
            GetItems3();

        }

        private void rjComboBox1_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            Sort(rjComboBox1.SelectedItem.ToString());

            doubleBufferedPanel3.Hide();

        }

        private void Sort(string value)
        {

            SqlConnection con = new SqlConnection(conn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd;
            DataView dv;

            string sql = "SELECT * FROM dbo.BORROW";
            cmd = new SqlCommand(sql, con);
            adapter.SelectCommand = cmd;

            ds.Clear();
            adapter.Fill(ds);
            dv = new DataView(ds.Tables[0]);
            dv.Sort = value;

            dataGridView5.DataSource = dv;
            dataGridView3.DataSource = dv;



        }

        private void rjButton13_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.BORROW WHERE DATE BETWEEN @date1-1 AND @date2", con);
            cmd.Parameters.AddWithValue("date1", SqlDbType.DateTime).Value = rjDatePicker1.Value;
            cmd.Parameters.AddWithValue("date2", SqlDbType.DateTime).Value = rjDatePicker2.Value;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView5.DataSource = dt;
            dataGridView3.DataSource = dt;

            doubleBufferedPanel2.Hide();
        }

        private void rjButton9_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.BORROW WHERE DATE2 BETWEEN @date1-1 AND @date2", con);
            cmd.Parameters.AddWithValue("date1", SqlDbType.DateTime).Value = rjDatePicker4.Value;
            cmd.Parameters.AddWithValue("date2", SqlDbType.DateTime).Value = rjDatePicker3.Value;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView5.DataSource = dt;
            dataGridView3.DataSource = dt;

            doubleBufferedPanel2.Hide();
        }


        private void rjComboBox3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Sort2(rjComboBox3.SelectedItem.ToString());

            doubleBufferedPanel9.Hide();
        }

        private void Sort2(string value)
        {
            SqlConnection con = new SqlConnection(conn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd;
            DataView dv;

            string sql = "SELECT * FROM dbo.DAMAGE";
            cmd = new SqlCommand(sql, con);
            adapter.SelectCommand = cmd;

            ds.Clear();
            adapter.Fill(ds);
            dv = new DataView(ds.Tables[0]);
            dv.Sort = value;

            dataGridView1.DataSource = dv;
            dataGridView4.DataSource = dv;


        }

        //

        //return

        private void rjComboBox4_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Sort3(rjComboBox4.SelectedItem.ToString());

            doubleBufferedPanel6.Hide();
        }

        private void Sort3(string value)
        {
            SqlConnection con = new SqlConnection(conn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd;
            DataView dv;

            string sql = "SELECT * FROM dbo.[RETURN]";
            cmd = new SqlCommand(sql, con);
            adapter.SelectCommand = cmd;

            ds.Clear();
            adapter.Fill(ds);
            dv = new DataView(ds.Tables[0]);
            dv.Sort = value;

            dataGridView1.DataSource = dv;
            dataGridView2.DataSource = dv;


        }

        private void rjButton16_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(@"
            SELECT * FROM dbo.[RETURN] 
              WHERE DATE >= @date1 AND DATE < DATEADD(DAY, 1, @date2)", con);

            // Set parameters with truncated time (only dates)
            cmd.Parameters.AddWithValue("@date1", rjDatePicker8.Value.Date);
            cmd.Parameters.AddWithValue("@date2", rjDatePicker7.Value.Date);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Bind data to DataGridView
            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt;

            doubleBufferedPanel5.Hide();
        }

        private void rjButton14_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel6.Visible = !doubleBufferedPanel6.Visible;

            doubleBufferedPanel5.Hide();

            GetItems();
            GetItems2();
            GetItems3();
        }

        private void rjButton12_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel5.Visible = !doubleBufferedPanel5.Visible;

            doubleBufferedPanel6.Hide();

            GetItems();
            GetItems2();
            GetItems3();
        }

        private bool hasErrorOccurred = false;


        //
        private void rjTextBox1__TextChanged(object sender, EventArgs e)
        {
            if (rjComboBox2.SelectedIndex == -1)
            {
                if (!hasErrorOccurred)
                {
                    NOTIFSELECT nOTIFSELECT = new NOTIFSELECT();
                    nOTIFSELECT.Show();
                    hasErrorOccurred = true; // Set the flag to prevent multiple messages
                }
                return;

            }

            string searchTerm = rjTextBox1.Texts.Trim();

            string prefix = rjComboBox2.SelectedItem.ToString();
            string selectedTable;

            if (prefix == "BORROW")
                selectedTable = "BORROW";
            else if (prefix == "RETURNED")
                selectedTable = "[RETURN]";
            else if (prefix == "VARIANCE")
                selectedTable = "DAMAGE";
            else
                return;

            string currentTab = GetCurrentTab(); // Implement GetCurrentTab() to return the active tab or button state
            if ((currentTab == "BORROW" && selectedTable != "BORROW") ||
                (currentTab == "RETURNED" && selectedTable != "[RETURN]") ||
                (currentTab == "VARIANCE" && selectedTable != "DAMAGE"))
            {

                if (!hasErrorOccurred)
                {
                    NOTIFCORRECT nOTIFCORRECT = new NOTIFCORRECT();
                    nOTIFCORRECT.Show();
                    hasErrorOccurred = true; // Set the flag to prevent multiple messages
                }
                return;
            }

            hasErrorOccurred = false;

            if (string.IsNullOrEmpty(searchTerm))
            {
                GetItems();
                GetItems2();
                GetItems3();
                return;
            }


            SearchDatabase(selectedTable, searchTerm);

        }

        private string GetCurrentTab()
        {
            if (rjButton22.BackColor == Color.FromArgb(93, 79, 162))
                return "BORROW";
            else if (rjButton1.BackColor == Color.FromArgb(93, 79, 162))
                return "RETURNED";
            else if (rjButton3.BackColor == Color.FromArgb(93, 79, 162))
                return "VARIANCE";
            else
                return string.Empty; 
        }

        private void SearchDatabase(string tableName, string searchTerm)
        {
            using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678"))
            {
              
                    con.Open();

                    string query = $@"
                SELECT * 
                FROM {tableName} 
                WHERE 
                    NAME LIKE @searchTerm OR 
                    ITEM LIKE @searchTerm OR 
                    BRAND LIKE @searchTerm OR 
                    MODEL LIKE @searchTerm OR 
                    CATEGORY LIKE @searchTerm OR 
                    QUANTITY LIKE @searchTerm";

                    SqlCommand cmd = new SqlCommand(query, con);

            
                    cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                if (tableName == "BORROW")
                {
                    dataGridView5.DataSource = dt;
                }
                else if (tableName == "[RETURN]")
                {
                    dataGridView1.DataSource = dt;
                }
                else if (tableName == "DAMAGE")
                {
                    dataGridView4.DataSource = dt;
                }

           
                if (dt.Rows.Count == 0)
                    {
                        NOTIFNOTFOUND2 nOTIFNOTFOUND2 = new NOTIFNOTFOUND2();
                         nOTIFNOTFOUND2.Show();
                    }
                else
                {
           
                    hasErrorOccurred = false;
                }

            }
        }

        private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        //return

        private void rjButton15_Click(object sender, EventArgs e)
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

            printer.Title = "RETURNED ITEMS";
            printer.TitleSpacing = 29;
            printer.TitleFont = extraBoldFont;
            printer.TitleColor = Color.White;
            printer.TitleBackground = new SolidBrush(Color.FromArgb(93, 79, 162));


            printer.SubTitle = string.Format("Current Returned Items Overview Report\n({0})", DateTime.Now.Date.ToLongDateString());
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


            printer.PrintPreviewDataGridView(dataGridView2);
            GetItems3();

        }


        //borrow
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

            printer.Title = "BORROWED ITEMS";
            printer.TitleSpacing = 29;
            printer.TitleFont = extraBoldFont;
            printer.TitleColor = Color.White;
            printer.TitleBackground = new SolidBrush(Color.FromArgb(93, 79, 162));


            printer.SubTitle = string.Format("Current Borrowed Items Overview Report\n({0})", DateTime.Now.Date.ToLongDateString());
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

        private void rjButton12_Click_1(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == " ")
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                int label2Text = 0;

                DateTime label6Text = DateTime.MinValue;

                string label1Text = selectedRow.Cells["nAMEDataGridViewTextBoxColumn2"].Value?.ToString() ?? "";
                int.TryParse(selectedRow.Cells["qUANTITYDataGridViewTextBoxColumn2"].Value?.ToString(), out label2Text);
                string label3Text = selectedRow.Cells["bORROWIDDataGridViewTextBoxColumn"].Value?.ToString();
                string label12Text = selectedRow.Cells["iTEMIDDataGridViewTextBoxColumn2"].Value?.ToString();
                string label13Text = selectedRow.Cells["iTEMDataGridViewTextBoxColumn2"].Value?.ToString();
                string label14Text = selectedRow.Cells["bRANDDataGridViewTextBoxColumn2"].Value?.ToString();
                string label15Text = selectedRow.Cells["mODELDataGridViewTextBoxColumn2"].Value?.ToString();
                string label4Text = selectedRow.Cells["cATEGORYDataGridViewTextBoxColumn2"].Value?.ToString();
                string label5Text = selectedRow.Cells["sTATUSDataGridViewTextBoxColumn2"].Value?.ToString();
                DateTime.TryParse(selectedRow.Cells["dATEDataGridViewTextBoxColumn2"].Value?.ToString(), out label6Text);



                BORROWDETAILS3 borrowDetails = new BORROWDETAILS3(
                    label1Text, label2Text, label3Text, label12Text, label13Text,
                    label14Text, label15Text, label4Text, label5Text, label6Text);

                borrowDetails.ShowDialog();

            }
        }

        private void rjButton17_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel9.Visible = !doubleBufferedPanel9.Visible;

            doubleBufferedPanel8.Hide();

            GetItems();
            GetItems2();
            GetItems3();
        }

        private void rjButton11_Click_1(object sender, EventArgs e)
        {
            doubleBufferedPanel8.Visible = !doubleBufferedPanel8.Visible;

            doubleBufferedPanel9.Hide();

            GetItems();
            GetItems2();
            GetItems3();
        }

        private void rjButton8_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(@"
              SELECT * FROM dbo.DAMAGE 
              WHERE DATE >= @date1 AND DATE < DATEADD(DAY, 1, @date2)", con);

            // Set parameters with truncated time (only dates)
            cmd.Parameters.AddWithValue("@date1", rjDatePicker6.Value.Date);
            cmd.Parameters.AddWithValue("@date2", rjDatePicker5.Value.Date);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Bind data to DataGridView
            dataGridView4.DataSource = dt;
            dataGridView6.DataSource = dt;

            doubleBufferedPanel8.Hide();

        }

        private void rjButton10_Click(object sender, EventArgs e)
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

            printer.Title = "DAMAGED ITEMS";
            printer.TitleSpacing = 29;
            printer.TitleFont = extraBoldFont;
            printer.TitleColor = Color.White;
            printer.TitleBackground = new SolidBrush(Color.FromArgb(93, 79, 162));


            printer.SubTitle = string.Format("Current Damaged Items Overview Report\n({0})", DateTime.Now.Date.ToLongDateString());
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


            printer.PrintPreviewDataGridView(dataGridView6);
            GetItems2();

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4.Columns[e.ColumnIndex].HeaderText == " ")
            {
                DataGridViewRow selectedRow = dataGridView4.Rows[e.RowIndex];

                int label2Text = 0;

                DateTime label7Text = DateTime.MinValue;

                string label1Text = selectedRow.Cells["nAMEDataGridViewTextBoxColumn4"].Value?.ToString() ?? "";
                int.TryParse(selectedRow.Cells["qUANTITYDataGridViewTextBoxColumn4"].Value?.ToString(), out label2Text);
                string label3Text = selectedRow.Cells["bORROWIDDataGridViewTextBoxColumn2"].Value?.ToString();
                string label12Text = selectedRow.Cells["iTEMIDDataGridViewTextBoxColumn4"].Value?.ToString();
                string label13Text = selectedRow.Cells["iTEMDataGridViewTextBoxColumn4"].Value?.ToString();
                string label14Text = selectedRow.Cells["bRANDDataGridViewTextBoxColumn4"].Value?.ToString();
                string label15Text = selectedRow.Cells["mODELDataGridViewTextBoxColumn4"].Value?.ToString();
                string label4Text = selectedRow.Cells["cATEGORYDataGridViewTextBoxColumn4"].Value?.ToString();
                string label5Text = selectedRow.Cells["sTATUSDataGridViewTextBoxColumn4"].Value?.ToString();
                string label6Text = selectedRow.Cells["rEASONDataGridViewTextBoxColumn"].Value?.ToString();
                DateTime.TryParse(selectedRow.Cells["dATEDataGridViewTextBoxColumn4"].Value?.ToString(), out label7Text);



                BORROWDETAILS2 borrowDetails = new BORROWDETAILS2(
                    label1Text, label2Text, label3Text, label12Text, label13Text,
                    label14Text, label15Text, label4Text, label5Text, label6Text, label7Text);

                borrowDetails.ShowDialog();

            }

            if (dataGridView4.Columns[e.ColumnIndex].HeaderText == "  ")
            {

                string damageId = dataGridView4.Rows[e.RowIndex].Cells["iDDataGridViewTextBoxColumn4"].Value.ToString(); // Assuming you have a column for ID
                string itemId = dataGridView4.Rows[e.RowIndex].Cells["iTEMIDDataGridViewTextBoxColumn4"].Value.ToString(); // Assuming you have a column for Item ID
                int damageQuantity = Convert.ToInt32(dataGridView4.Rows[e.RowIndex].Cells["qUANTITYDataGridViewTextBoxColumn4"].Value); // Assuming you have a column for Quantity

                // Pass the data to DAMAGEFIXED form
                DAMAGEFIXED damageFixedForm = new DAMAGEFIXED(this, damageId, itemId, damageQuantity);
                damageFixedForm.Show();

            }
        }

        private void dataGridView5_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView5.Columns[e.ColumnIndex].Name == "sTATUSDataGridViewTextBoxColumn1")
            {

                string status = e.Value.ToString();


                if (status == "OVERDUE")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(207, 74, 74);
                }
                else if (status == "UNRETURNED")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(255, 194, 111);
                }

            }
        }

        private void rjButton18_Click(object sender, EventArgs e)
        {
            isDescending = !isDescending;

            // Load the items with the new sorting order
            GetItems();
         
        }

        private void rjButton19_Click(object sender, EventArgs e)
        {
            isDescending = !isDescending;

            // Load the items with the new sorting order
     
            GetItems2();
        }

        private void rjButton20_Click(object sender, EventArgs e)
        {
            isDescending = !isDescending;

            // Load the items with the new sorting order
    
            GetItems3();
        }
    }
}
