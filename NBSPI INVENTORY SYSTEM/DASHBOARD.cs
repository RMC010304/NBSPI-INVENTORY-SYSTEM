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

namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class DASHBOARD : UserControl
    {

         string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";

        bool isDescending = true;
        public DASHBOARD()
        {
            InitializeComponent();
            
        }

        public void SetUsername(string username)
        {
            label3.Text = $"Hello, {username}"; // Update the label to display the username
        }

        void FillChart1()
        {
            SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678");
            DataTable dt = new DataTable();
            con.Open();

            // Query to retrieve data from the 'Return' table
            string query = "SELECT ITEM, QUANTITY, DATE FROM dbo.DAMAGE";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.Fill(dt);
            con.Close();

            chart3.DataSource = dt;

            chart3.Series["QUANTITY"].XValueMember = "ITEM";
            chart3.Series["QUANTITY"].YValueMembers = "QUANTITY";

            // Remove the legend
            chart3.Legends.Clear();

            // Hide X-axis labels
            chart3.ChartAreas[0].AxisX.LabelStyle.Enabled = false;

            // Hide Y-axis labels
            chart3.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
        }

        void FillChart2()
        {
            SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678");
            DataTable dt = new DataTable();
            con.Open();

            // Query to retrieve data from the 'Return' table
            string query = "SELECT ITEM, QUANTITY, DATE FROM dbo.BORROW";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.Fill(dt);
            con.Close();

            chart2.DataSource = dt;

            chart2.Series["QUANTITY"].XValueMember = "ITEM";
            chart2.Series["QUANTITY"].YValueMembers = "QUANTITY";

            // Remove the legend
            chart2.Legends.Clear();

            // Hide X-axis labels
            chart2.ChartAreas[0].AxisX.LabelStyle.Enabled = false;

            // Hide Y-axis labels
            chart2.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
        }

        void FillChart3()
        {
            SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678");
            DataTable dt = new DataTable();
            con.Open();

            // Query to retrieve data from the 'Return' table
            string query = "SELECT ITEM, QUANTITY, DATE FROM dbo.[RETURN]";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.Fill(dt);
            con.Close();

            chart1.DataSource = dt;

            chart1.Series["QUANTITY"].XValueMember = "ITEM";
            chart1.Series["QUANTITY"].YValueMembers = "QUANTITY";

            // Remove the legend
            chart1.Legends.Clear();

            // Hide X-axis labels
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;

            // Hide Y-axis labels
            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
        }


        void FillChart4()
        {
            SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678");
            DataTable dt = new DataTable();
            con.Open();

            // Combined SQL query using UNION ALL to retrieve data from all tables
            string query = @"
              SELECT ITEM, QUANTITY, DATE FROM dbo.IT
              UNION ALL
             SELECT ITEM, QUANTITY, DATE FROM dbo.ITEMS
             UNION ALL
             SELECT ITEM, QUANTITY, DATE FROM dbo.SCIENCE
             UNION ALL
             SELECT ITEM, QUANTITY, DATE FROM dbo.SPORTS";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.Fill(dt); 
            con.Close();

       
            chart4.DataSource = dt;

    
            chart4.Series["QUANTITY"].XValueMember = "ITEM";
            chart4.Series["QUANTITY"].YValueMembers = "QUANTITY";


        }

        public void GetItems()
        {

            string orderBy = isDescending ? "DESC" : "ASC";

            SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;Integrated Security=True");
            SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.BORROW ORDER BY DATE {orderBy}", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView5.DataSource = dt;

        }

        private void DASHBOARD_Load(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("MMMM dd, yyyy").ToUpper();
            DisplayItemQuantities();
            LoadOverallTotalItems();
            FillChart4();
            FillChart3();
            FillChart2();
            FillChart1();
            LoadBorrowedPercentage();
            LoadReturnedPercentage2();
            LoadDamagePercentage3();
            GetItems();


        }

        public void RefreshDashboard()
        {
            DisplayItemQuantities();
            LoadOverallTotalItems();
            FillChart1();
            FillChart2();
            FillChart3();
            FillChart4();
            LoadBorrowedPercentage();
            LoadReturnedPercentage2();
            LoadDamagePercentage3();
            GetItems();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("hh:mm:ss tt").ToUpper();
        }

        private void DisplayItemQuantities()
        {

            // SQL queries to sum item quantities in each table
            string borrowQuery = "SELECT SUM(QUANTITY) FROM BORROW";
            string damageQuery = "SELECT SUM(QUANTITY) FROM DAMAGE";
            string returnQuery = "SELECT SUM(QUANTITY) FROM [RETURN]";
            string itQuery = "SELECT SUM(QUANTITY) FROM IT";
            string itemsQuery = "SELECT SUM(QUANTITY) FROM ITEMS";
            string scienceQuery = "SELECT SUM(QUANTITY) FROM SCIENCE";
            string sportsQuery = "SELECT SUM(QUANTITY) FROM SPORTS";

            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                // Borrow table item quantities
                using (SqlCommand borrowCommand = new SqlCommand(borrowQuery, connection))
                {
                    object borrowResult = borrowCommand.ExecuteScalar();
                    int borrowQuantity = borrowResult != DBNull.Value ? Convert.ToInt32(borrowResult) : 0;
                    label4.Text = borrowQuantity.ToString();
                }

                // Damage table item quantities
                using (SqlCommand damageCommand = new SqlCommand(damageQuery, connection))
                {
                    object damageResult = damageCommand.ExecuteScalar();
                    int damageQuantity = damageResult != DBNull.Value ? Convert.ToInt32(damageResult) : 0;
                    label6.Text = damageQuantity.ToString();
                }

                // Return table item quantities
                using (SqlCommand returnCommand = new SqlCommand(returnQuery, connection))
                {
                    object returnResult = returnCommand.ExecuteScalar();
                    int returnQuantity = returnResult != DBNull.Value ? Convert.ToInt32(returnResult) : 0;
                    label5.Text = returnQuantity.ToString();
                }

                // IT table item quantities
                using (SqlCommand itCommand = new SqlCommand(itQuery, connection))
                {
                    object itResult = itCommand.ExecuteScalar();
                    int itQuantity = itResult != DBNull.Value ? Convert.ToInt32(itResult) : 0;
                 
                }

                // ITEMS table item quantities
                using (SqlCommand itemsCommand = new SqlCommand(itemsQuery, connection))
                {
                    object itemsResult = itemsCommand.ExecuteScalar();
                    int itemsQuantity = itemsResult != DBNull.Value ? Convert.ToInt32(itemsResult) : 0;
                   
                }

                // SCIENCE table item quantities
                using (SqlCommand scienceCommand = new SqlCommand(scienceQuery, connection))
                {
                    object scienceResult = scienceCommand.ExecuteScalar();
                    int scienceQuantity = scienceResult != DBNull.Value ? Convert.ToInt32(scienceResult) : 0;
                    
                }

                // SPORTS table item quantities
                using (SqlCommand sportsCommand = new SqlCommand(sportsQuery, connection))
                {
                    object sportsResult = sportsCommand.ExecuteScalar();
                    int sportsQuantity = sportsResult != DBNull.Value ? Convert.ToInt32(sportsResult) : 0;
                   
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void LoadBorrowedPercentage()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn)) // Use the connection string
                {
                    connection.Open();

                    // Query to calculate total borrowed quantity and total available quantity
                    string totalBorrowedQuery = "SELECT SUM(QUANTITY) AS TotalBorrowedQuantity FROM BORROW";
                    string totalItemsQuery = @"
            SELECT SUM(QUANTITY) AS TotalItemsQuantity FROM (
                SELECT QUANTITY FROM BORROW
                UNION ALL
                SELECT QUANTITY FROM IT
                UNION ALL
                SELECT QUANTITY FROM ITEMS
                UNION ALL
                SELECT QUANTITY FROM SCIENCE
                UNION ALL
                SELECT QUANTITY FROM SPORTS
            ) AS TotalItems";

                    // Execute queries
                    SqlCommand borrowedCommand = new SqlCommand(totalBorrowedQuery, connection);
                    SqlCommand totalItemsCommand = new SqlCommand(totalItemsQuery, connection);

                    object borrowedResult = borrowedCommand.ExecuteScalar();
                    object totalItemsResult = totalItemsCommand.ExecuteScalar();

                    // Ensure valid results
                    int totalBorrowed = borrowedResult != DBNull.Value && borrowedResult != null
                        ? Convert.ToInt32(borrowedResult)
                        : 0;
                    int totalItems = totalItemsResult != DBNull.Value && totalItemsResult != null
                        ? Convert.ToInt32(totalItemsResult)
                        : 0;

                    // Calculate percentage
                    if (totalItems > 0)
                    {
                        int percentage = (int)Math.Round((double)totalBorrowed / totalItems * 100); // Round to nearest whole number
                        circularProgressBar1.Text = $"{percentage}"; // Display percentage as a whole number
                        circularProgressBar1.Value = percentage; // Reflect the percentage in circularProgressBar1
                    }
                    else
                    {
                        circularProgressBar1.Text = "0%";
                        circularProgressBar1.Value = 0; // Set progress bar to 0% if no items
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading borrowed percentage: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadReturnedPercentage2()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn)) // Use the connection string
                {
                    connection.Open();

                    // Query to calculate total returned quantity and total available quantity
                    string totalReturnedQuery = "SELECT SUM(QUANTITY) AS TotalReturnedQuantity FROM [RETURN]"; // Modify as per your return table
                    string totalItemsQuery = @"
            SELECT SUM(QUANTITY) AS TotalItemsQuantity FROM (
                SELECT QUANTITY FROM [RETURN]
                UNION ALL
                SELECT QUANTITY FROM IT
                UNION ALL
                SELECT QUANTITY FROM ITEMS
                UNION ALL
                SELECT QUANTITY FROM SCIENCE
                UNION ALL
                SELECT QUANTITY FROM SPORTS
            ) AS TotalItems";

                    // Execute queries
                    SqlCommand returnedCommand = new SqlCommand(totalReturnedQuery, connection);
                    SqlCommand totalItemsCommand = new SqlCommand(totalItemsQuery, connection);

                    object returnedResult = returnedCommand.ExecuteScalar();
                    object totalItemsResult = totalItemsCommand.ExecuteScalar();

                    // Ensure valid results
                    int totalReturned = returnedResult != DBNull.Value && returnedResult != null
                        ? Convert.ToInt32(returnedResult)
                        : 0;
                    int totalItems = totalItemsResult != DBNull.Value && totalItemsResult != null
                        ? Convert.ToInt32(totalItemsResult)
                        : 0;

                    // Calculate percentage
                    if (totalItems > 0)
                    {
                        int percentage = (int)Math.Round((double)totalReturned / totalItems * 100); // Round to nearest whole number
                        circularProgressBar2.Text = $"{percentage}"; // Display percentage as a whole number
                        circularProgressBar2.Value = percentage; // Reflect the percentage in circularProgressBar2
                    }
                    else
                    {
                        circularProgressBar2.Text = "0%";
                        circularProgressBar2.Value = 0; // Set progress bar to 0% if no items
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading returned percentage: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void label12_Click(object sender, EventArgs e)
        {

        }
        private void LoadDamagePercentage3()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn)) // Use the connection string
                {
                    connection.Open();

                    // Query to calculate total damaged quantity and total available quantity
                    string totalDamagedQuery = "SELECT SUM(QUANTITY) AS TotalDamagedQuantity FROM DAMAGE"; // Modify as per your damage table
                    string totalItemsQuery = @"
            SELECT SUM(QUANTITY) AS TotalItemsQuantity FROM (
                SELECT QUANTITY FROM DAMAGE
                UNION ALL
                SELECT QUANTITY FROM IT
                UNION ALL
                SELECT QUANTITY FROM ITEMS
                UNION ALL
                SELECT QUANTITY FROM SCIENCE
                UNION ALL
                SELECT QUANTITY FROM SPORTS
            ) AS TotalItems";

                    // Execute queries
                    SqlCommand damagedCommand = new SqlCommand(totalDamagedQuery, connection);
                    SqlCommand totalItemsCommand = new SqlCommand(totalItemsQuery, connection);

                    object damagedResult = damagedCommand.ExecuteScalar();
                    object totalItemsResult = totalItemsCommand.ExecuteScalar();

                    // Ensure valid results
                    int totalDamaged = damagedResult != DBNull.Value && damagedResult != null
                        ? Convert.ToInt32(damagedResult)
                        : 0;
                    int totalItems = totalItemsResult != DBNull.Value && totalItemsResult != null
                        ? Convert.ToInt32(totalItemsResult)
                        : 0;

                    // Calculate percentage
                    if (totalItems > 0)
                    {
                        int percentage = (int)Math.Round((double)totalDamaged / totalItems * 100); // Round to nearest whole number
                        circularProgressBar3.Text = $"{percentage}"; // Display percentage as a whole number
                        circularProgressBar3.Value = percentage; // Reflect the percentage in circularProgressBar3
                    }
                    else
                    {
                        circularProgressBar3.Text = "0%";
                        circularProgressBar3.Value = 0; // Set progress bar to 0% if no items
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading damage percentage: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOverallTotalItems()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn)) // Use the connection string
                {
                    connection.Open();

                    // Query to calculate total items from IT, ITEMS, SCIENCE, and SPORTS
                    string totalQuery = @"
            SELECT SUM(QUANTITY) AS TotalItems FROM (
                SELECT QUANTITY FROM IT
                UNION ALL
                SELECT QUANTITY FROM ITEMS
                UNION ALL
                SELECT QUANTITY FROM SCIENCE
                UNION ALL
                SELECT QUANTITY FROM SPORTS
            ) AS TotalItems";

                    // Execute query
                    SqlCommand totalCommand = new SqlCommand(totalQuery, connection);
                    object totalResult = totalCommand.ExecuteScalar();

                    // Ensure valid result
                    int total = totalResult != DBNull.Value && totalResult != null
                        ? Convert.ToInt32(totalResult)
                        : 0;

                    // Update label7 with the total
                    label7.Text = $"{total}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading overall total items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void chart6_Click(object sender, EventArgs e)
        {

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            OVERALL oVERALL = new OVERALL();
            oVERALL.Show();
        }

        private void rjButton4_Click(object sender, EventArgs e)
        {
            BRD bRD = new BRD();
            bRD.Show(); 
        }

        private void rjButton9_Click(object sender, EventArgs e)
        {
            RefreshDashboard();
            NOTIFRELOAD nOTIFRELOAD = new NOTIFRELOAD();
            nOTIFRELOAD.Show();
        }
    }    
}
