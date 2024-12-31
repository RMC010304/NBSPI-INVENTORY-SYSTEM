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

   
        public DASHBOARD()
        {
            InitializeComponent();
            
        }

        public void SetUsername(string username)
        {
            label3.Text = $"Hello, {username}"; // Update the label to display the username
        }

        void FillChart()
        {
            SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678");
            DataTable dt = new DataTable();
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT ITEM, QUANTITY FROM dbo.BORROW", con);
            da.Fill(dt);
            chart6.DataSource = dt;
            con.Close();



            chart6.Series["QUANTITY"].XValueMember = "ITEM";
            chart6.Series["QUANTITY"].YValueMembers = "QUANTITY";



        }

        void FillChart2()
        {
            SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678");
            DataTable dt = new DataTable();
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT ITEM, QUANTITY FROM dbo.[RETURN]", con);
            da.Fill(dt);
            chart2.DataSource = dt;
            con.Close();



            chart2.Series["QUANTITY"].XValueMember = "QUANTITY";
            chart2.Series["QUANTITY"].YValueMembers = "QUANTITY";

            foreach (var point in chart6.Series["QUANTITY"].Points)
            {

                point.Label = string.Empty;
            }


        }

        void FillChart3()
        {
            SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678");
            DataTable dt = new DataTable();
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT ITEM, QUANTITY FROM dbo. DAMAGE", con);
            da.Fill(dt);
            chart7.DataSource = dt;
            con.Close();



            chart7.Series["QUANTITY"].XValueMember = "ITEM";
            chart7.Series["QUANTITY"].YValueMembers = "QUANTITY";



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

        void FillChart5()
        {
            SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678");
            DataTable dt = new DataTable();
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("SELECT NAME, QUANTITY,DATE FROM dbo. BORROW", con);
            da.Fill(dt);
            chart5.DataSource = dt;
            con.Close();



            chart5.Series["QUANTITY"].XValueMember = "NAME";
            chart5.Series["QUANTITY"].YValueMembers = "QUANTITY";



        }

        private void DASHBOARD_Load(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString("MMMM dd, yyyy").ToUpper();
            DisplayItemQuantities();
            FillChart();
            FillChart2();
            FillChart3();
            FillChart4();
            FillChart5();
            LoadBorrowedPercentage();
            LoadReturnedPercentage2();
            LoadDamagePercentage3();


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
                    label7.Text = itQuantity.ToString();
                }

                // ITEMS table item quantities
                using (SqlCommand itemsCommand = new SqlCommand(itemsQuery, connection))
                {
                    object itemsResult = itemsCommand.ExecuteScalar();
                    int itemsQuantity = itemsResult != DBNull.Value ? Convert.ToInt32(itemsResult) : 0;
                    label8.Text = itemsQuantity.ToString();
                }

                // SCIENCE table item quantities
                using (SqlCommand scienceCommand = new SqlCommand(scienceQuery, connection))
                {
                    object scienceResult = scienceCommand.ExecuteScalar();
                    int scienceQuantity = scienceResult != DBNull.Value ? Convert.ToInt32(scienceResult) : 0;
                    label9.Text = scienceQuantity.ToString();
                }

                // SPORTS table item quantities
                using (SqlCommand sportsCommand = new SqlCommand(sportsQuery, connection))
                {
                    object sportsResult = sportsCommand.ExecuteScalar();
                    int sportsQuantity = sportsResult != DBNull.Value ? Convert.ToInt32(sportsResult) : 0;
                    label10.Text = sportsQuantity.ToString();
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
                        double percentage = (double)totalBorrowed / totalItems * 100;
                        label11.Text = $"{percentage:F2}%"; // Display percentage with 2 decimal places
                    }
                    else
                    {
                        label11.Text = "00%";
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
                        double percentage = (double)totalReturned / totalItems * 100;
                        label12.Text = $"{percentage:F2}%"; // Display percentage with 2 decimal places
                    }
                    else
                    {
                        label12.Text = "00%";
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
                        double percentage = (double)totalDamaged / totalItems * 100;
                        label13.Text = $"{percentage:F2}%"; // Display percentage with 2 decimal places
                    }
                    else
                    {
                        label13.Text = "00%";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading damage percentage: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }    
}
