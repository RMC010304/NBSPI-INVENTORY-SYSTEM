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
    public partial class BRD : Form
    {
        string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";
        bool isDescending = true;
        bool isDescending1 = true;
        bool isDescending2 = true;
        public BRD()
        {
            InitializeComponent();
        }

        private void BRD_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'iT_RESDataSet29.DAMAGE' table. You can move, or remove it, as needed.
            this.dAMAGETableAdapter.Fill(this.iT_RESDataSet29.DAMAGE);
            // TODO: This line of code loads data into the 'iT_RESDataSet28.RETURN' table. You can move, or remove it, as needed.
            this.rETURNTableAdapter.Fill(this.iT_RESDataSet28.RETURN);
            // TODO: This line of code loads data into the 'iT_RESDataSet27.BORROW' table. You can move, or remove it, as needed.
            this.bORROWTableAdapter.Fill(this.iT_RESDataSet27.BORROW);

            GetItems();
            GetItems2();
            GetItems3();
            dataGridView5.Columns["pHOTODataGridViewImageColumn"].DefaultCellStyle.NullValue = null;
            dataGridView1.Columns["pHOTODataGridViewImageColumn1"].DefaultCellStyle.NullValue = null;
            dataGridView2.Columns["pHOTODataGridViewImageColumn2"].DefaultCellStyle.NullValue = null;
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
            
            }
        }
        public void GetItems2()
        {

            string orderBy = isDescending1 ? "DESC" : "ASC";

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string query = ($"SELECT * FROM dbo.[RETURN] ORDER BY DATE {orderBy}");
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
        }

        public void GetItems3()
        {

            string orderBy = isDescending2 ? "DESC" : "ASC";

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string query = ($"SELECT * FROM dbo.DAMAGE ORDER BY DATE {orderBy}");
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;

            }
        }

        public void RefreshData()
        {
            GetItems();
            GetItems2();
            GetItems3();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
