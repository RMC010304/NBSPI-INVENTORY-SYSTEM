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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class BORROW : Form
    {
        string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";

        private CustomIdGenerator _generator;

        private TRANSACTION transaction;

        public BORROW(TRANSACTION control)
        {
            InitializeComponent();
            transaction = control;

            _generator = new CustomIdGenerator("B");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rjTextBox1.Texts) ||
           !string.IsNullOrWhiteSpace(rjComboBox1.Texts) ||
           !string.IsNullOrWhiteSpace(rjTextBox2.Texts) ||
           !string.IsNullOrWhiteSpace(rjTextBox3.Texts) ||
           !string.IsNullOrWhiteSpace(rjTextBox4.Texts))
            {

                using (NOTIFNOTICE4 nOTIFNOTICE = new NOTIFNOTICE4(this))
                {
                    nOTIFNOTICE.ShowDialog();
                }
            }
            else
            {

                this.Close();
            }
        }

        private void FetchItemDetails(string itemId)
        {
            using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678"))
            {
                con.Open();

                string queryTemplate = "SELECT * FROM {0} WHERE ID = @ItemId";
                string[] tables = { "ITEMS", "IT", "SCIENCE", "SPORTS" };

                foreach (string table in tables)
                {

                    string query = string.Format(queryTemplate, table);
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ItemId", itemId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                rjTextBox3.Texts = reader["ITEM"].ToString();
                                textBox1.Text = reader["BRAND"].ToString();
                                textBox2.Text = reader["MODEL"].ToString();
                                rjTextBox4.Texts = reader["QUANTITY"].ToString();

                                return;

                            }
                        }
                    }
                }
            }
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {

            string itemId = rjTextBox2.Texts.Trim().ToUpper();
            int quantityToBorrow;


            if (string.IsNullOrWhiteSpace(rjTextBox4.Texts.Trim()))
            {
                NOTIFFILLED2 nOTIFFILLED2 = new NOTIFFILLED2();
                nOTIFFILLED2.Show();
                return;
            }

            if (!int.TryParse(rjTextBox4.Texts.Trim(), out quantityToBorrow))
            {
               NOTIFNUMERIC2 nOTIFNUMERIC2 = new NOTIFNUMERIC2();
                nOTIFNUMERIC2.Show();
                return;

            }

            string borrowerName = rjTextBox1.Texts.Trim();
            DateTime borrowedDate = rjDatePicker2.Value;
            DateTime returnDate = rjDatePicker1.Value;

            string tableName;
            if (itemId.StartsWith("IT"))
            {
                tableName = "IT";
            }
            else if (itemId.StartsWith("SL"))
            {
                tableName = "SCIENCE";
            }
            else if (itemId.StartsWith("SE"))
            {
                tableName = "SPORTS";
            }
            else
            {
                tableName = "ITEMS"; 
            }

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                // Check if the item exists in the specified table
                string checkItemExistsQuery = $"SELECT COUNT(1) FROM {tableName} WHERE ID = @ItemId";
                SqlCommand checkItemExistsCmd = new SqlCommand(checkItemExistsQuery, con);
                checkItemExistsCmd.Parameters.AddWithValue("@ItemId", itemId);

                int itemCount = (int)checkItemExistsCmd.ExecuteScalar();

                if (itemCount == 0)
                {
                    NOTIFNOTFOUND nOTIFNOTFOUND = new NOTIFNOTFOUND();
                    nOTIFNOTFOUND.Show();

                    return;
                }

                if (string.IsNullOrWhiteSpace(borrowerName))
                {
                   NOTIFFILLED2 nOTIFFILLED2 = new NOTIFFILLED2();
                    nOTIFFILLED2.Show();
                    return;
                }

                if (string.IsNullOrWhiteSpace(rjComboBox1.Texts.Trim()))
                {
                    NOTIFFILLED2 nOTIFFILLED2 = new NOTIFFILLED2();
                    nOTIFFILLED2.Show();
                    return;
                }

                // Get current stock
                string getStockQuery = $"SELECT QUANTITY FROM {tableName} WHERE ID = @ItemId";
                SqlCommand getStockCmd = new SqlCommand(getStockQuery, con);
                getStockCmd.Parameters.AddWithValue("@ItemId", itemId);

                object stockResult = getStockCmd.ExecuteScalar();

                if (stockResult == null || stockResult == DBNull.Value)
                {
                    NOTIFNOTFOUND nOTIFNOTFOUND = new NOTIFNOTFOUND();
                    nOTIFNOTFOUND.Show();   
                    return;
                }

                int currentStock = Convert.ToInt32(stockResult);

                if (quantityToBorrow > currentStock)
                {
                  NOTIFENOUGH nOTIFENOUGH = new NOTIFENOUGH();
                    nOTIFENOUGH.Show();
                    return;
                }

                string customId = _generator.GenerateId();
                // Insert borrow record into BORROW table
                string insertBorrowQuery = "INSERT INTO BORROW (ID, [ITEM ID], NAME, ITEM, BRAND, MODEL, CATEGORY, QUANTITY, STATUS, DATE, DATE2) " +
                                           "VALUES (@Id, @ItemId, @Name, @Item, @Brand, @Model, @Category, @Quantity, @Status, @Date, @Date2)";
                SqlCommand insertBorrowCmd = new SqlCommand(insertBorrowQuery, con);
                insertBorrowCmd.Parameters.AddWithValue("@Id", customId);
                insertBorrowCmd.Parameters.AddWithValue("@ItemId", itemId);
                insertBorrowCmd.Parameters.AddWithValue("@Name", borrowerName);
                insertBorrowCmd.Parameters.AddWithValue("@Item", rjTextBox3.Texts.Trim());
                insertBorrowCmd.Parameters.AddWithValue("@Brand", textBox1.Text.Trim());
                insertBorrowCmd.Parameters.AddWithValue("@Model", textBox2.Text.Trim());
                insertBorrowCmd.Parameters.AddWithValue("@Category", rjComboBox1.Texts.Trim()); // Using the tableName as category
                insertBorrowCmd.Parameters.AddWithValue("@Quantity", quantityToBorrow);
                insertBorrowCmd.Parameters.AddWithValue("@Status", "UNRETURNED");
                insertBorrowCmd.Parameters.AddWithValue("@Date", borrowedDate);
                insertBorrowCmd.Parameters.AddWithValue("@Date2", returnDate);

                insertBorrowCmd.ExecuteNonQuery();

                // If the current date is past the return date, update status to OVERDUE
                if (DateTime.Now > returnDate)
                {
                    // Update the status to OVERDUE in the BORROW table
                    string updateStatusQuery = "UPDATE BORROW SET STATUS = 'OVERDUE' WHERE ID = @Id";
                    SqlCommand updateStatusCmd = new SqlCommand(updateStatusQuery, con);
                    updateStatusCmd.Parameters.AddWithValue("@Id", customId);
                    updateStatusCmd.ExecuteNonQuery();
                }

                // Update stock in the specified table
                string updateStockQuery = $"UPDATE {tableName} SET QUANTITY = QUANTITY - @Quantity WHERE ID = @ItemId";
                SqlCommand updateStockCmd = new SqlCommand(updateStockQuery, con);
                updateStockCmd.Parameters.AddWithValue("@Quantity", quantityToBorrow);
                updateStockCmd.Parameters.AddWithValue("@ItemId", itemId);

                updateStockCmd.ExecuteNonQuery();

                NOTIFSUCCESS nOTIFSUCCESS = new NOTIFSUCCESS();
                nOTIFSUCCESS.Show();
                this.Close();
                
            }


            rjTextBox1.Texts = string.Empty;
            rjTextBox2.Texts = string.Empty;
            rjTextBox3.Texts = string.Empty;
            rjComboBox1.SelectedIndex = -1;
            rjTextBox4.Texts = string.Empty;

            transaction.GetItems();


        }

        public class CustomIdGenerator
        {
            private string _prefix;
            private int _currentNumber;

            public CustomIdGenerator(string prefix)
            {
                _prefix = prefix;
                _currentNumber = GetLastUsedNumber() + 1; 
            }

            private int GetLastUsedNumber()
            {
                using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT COALESCE(MAX(CAST(SUBSTRING(ID, LEN(@prefix) + 1, LEN(ID) - LEN(@prefix)) AS INT)), 0) FROM BORROW WHERE ID LIKE @prefix + '%'", connection);
                    command.Parameters.AddWithValue("@prefix", _prefix);

                    var result = command.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
            }

            public string GenerateId()
            {
                string id = $"{_prefix}{_currentNumber:D3}";
                _currentNumber++;
                return id;
            }
        }

        private void rjTextBox3__TextChanged(object sender, EventArgs e)
        {
        
        }

        private void rjTextBox2__TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rjTextBox2.Texts))
            {
                FetchItemDetails(rjTextBox2.Texts.Trim()); 
            }
        }

        private void BORROW_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rjTextBox1.Texts) ||
          !string.IsNullOrWhiteSpace(rjComboBox1.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox2.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox3.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox4.Texts))
            {

                using (NOTIFNOTICE4 nOTIFNOTICE = new NOTIFNOTICE4(this))
                {
                    nOTIFNOTICE.ShowDialog();
                }
            }
            else
            {

                this.Close();
            }
        }
    }
    
}
