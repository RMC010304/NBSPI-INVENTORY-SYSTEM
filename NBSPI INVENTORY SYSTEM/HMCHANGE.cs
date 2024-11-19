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
    public partial class HMCHANGE : Form
    {

        string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";

        int no;
        string number;

        public HMCHANGE(string id, int quantity)
        {
            InitializeComponent();

            number = id;
            no = quantity;

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {
            number = textBox1.Text;

            SqlConnection con = new SqlConnection(conn);
            con.Open();

            if (!int.TryParse(rjTextBox1.Texts, out int no))
            {
                NOTIFNUMERIC nOTIFNUMERIC = new NOTIFNUMERIC();
                nOTIFNUMERIC.Show();
                return;
            }

            string query = "UPDATE IT SET QUANTITY=@QUANTITY,DATE2=@DATE2 WHERE ID =@ID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", number);
            cmd.Parameters.AddWithValue("@QUANTITY", no);
            cmd.Parameters.AddWithValue("@DATE2", DateTime.Now);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {

                GetItems();
                NOTIFUPDATE nOTIFUPDATE = new NOTIFUPDATE();
                nOTIFUPDATE.Show();
                this.Close();


                HMUPDATE updateForm = Application.OpenForms.OfType<HMUPDATE>().FirstOrDefault();
                if (updateForm != null)
                {

                    updateForm.UpdateQuantity(no);
                    updateForm.UpdateDate(DateTime.Now);

                }

            }

            con.Close();
        }

        private void HMCHANGE_Load(object sender, EventArgs e)
        {
            textBox1.Text = number.ToString();
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
    }
}
