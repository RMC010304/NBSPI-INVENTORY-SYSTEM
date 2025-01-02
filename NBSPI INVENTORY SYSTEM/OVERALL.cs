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
    public partial class OVERALL : Form
    {
        public OVERALL()
        {
            InitializeComponent();
        }

        private void OVERALL_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'iT_RESDataSet26.IT' table. You can move, or remove it, as needed.
            this.iTTableAdapter.Fill(this.iT_RESDataSet26.IT);
            LoadDataGridView();
            dataGridView5.Columns["pHOTODataGridViewImageColumn"].DefaultCellStyle.NullValue = null;
        }
        void LoadDataGridView()
        {
            // Define the connection string
            SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678");

            try
            {
                // Open the connection
                con.Open();

                // Define a combined query to retrieve data from all tables
                string query = @"
            SELECT 'IT' AS [Table], ITEM, QUANTITY, STATUS, PHOTO FROM dbo.IT
            UNION ALL
            SELECT 'ITEMS' AS [Table], ITEM, QUANTITY, STATUS, PHOTO FROM dbo.ITEMS
            UNION ALL
            SELECT 'SCIENCE' AS [Table], ITEM, QUANTITY, STATUS, PHOTO FROM dbo.SCIENCE
            UNION ALL
            SELECT 'SPORTS' AS [Table], ITEM, QUANTITY, STATUS, PHOTO FROM dbo.SPORTS";

                // Execute the query and fill the data into a DataTable
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind the DataTable to the DataGridView
                dataGridView5.DataSource = dt;

            }
            catch (Exception ex)
            {
                // Display an error message if something goes wrong
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection
                con.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
