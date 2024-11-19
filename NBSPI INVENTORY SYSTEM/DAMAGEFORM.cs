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

namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class DAMAGEFORM : Form
    {

        string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";

        private TRANSACTION transaction;
        private string borrowerId;
        private string borrowerName;
        private string damagedItem;
        private string category;
        private int quantity;
        private string reason;

        private string _borrowId;

        private int _remainingQuantity;
        private int _returnQuantity;

        private CustomIdGenerator _generator;
        private CustomIdGenerator _generator2;

        public DAMAGEFORM(TRANSACTION control, string borrowerId, string borrowerName, string damagedItem, string category, int _remainingQuantity, int returnQuantity, string reason, string borrowId)
        {
            InitializeComponent();

            transaction = control;

            this.borrowerId = borrowerId;
            this.borrowerName = borrowerName;
            this.damagedItem = damagedItem;
            this.category = category;
            this.quantity = _remainingQuantity;
            this.reason = reason;
            _borrowId = borrowId;
            _returnQuantity = returnQuantity;

            _generator = new CustomIdGenerator("R", "DL");



            rjTextBox4.Texts = quantity.ToString();
            LoadBorrowData();

            
        }

        private void LoadBorrowData()
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string query = "SELECT ID, NAME, ITEM, [ITEM ID], BRAND, MODEL, CATEGORY, QUANTITY FROM BORROW WHERE ID = @BorrowID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BorrowID", _borrowId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    borrowerId = reader["ID"].ToString();
                    borrowerName = reader["NAME"].ToString();
                    damagedItem = reader["ITEM"].ToString();
                    category = reader["CATEGORY"].ToString();
                    quantity = Convert.ToInt32(reader["QUANTITY"]);
      

                    string itemId = reader["ITEM ID"].ToString();
                    string brand = reader["BRAND"].ToString();
                    string model = reader["MODEL"].ToString();


                    rjTextBox1.Texts = borrowerId;
                    rjTextBox2.Texts = borrowerName;
                    rjTextBox3.Texts = damagedItem;
                    rjTextBox6.Texts = category;
                    textBox1.Text = itemId;   
                    textBox2.Text = brand;   
                    textBox3.Text = model;

                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rjTextBox1.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox6.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox2.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox3.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox4.Texts))
            {

                using (NOTIFNOTICE5 nOTIFNOTICE = new NOTIFNOTICE5(this))
                {
                    nOTIFNOTICE.ShowDialog();
                }
            }
            else
            {

                this.Close();
            }
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {
            string borrowerName = rjTextBox1.Texts;
            string borrowerId = rjTextBox2.Texts;
            string damagedItem = rjTextBox3.Texts;
            string category = rjTextBox6.Texts;
            int quantity = int.TryParse(rjTextBox4.Texts, out int qty) ? qty : 0;
            DateTime dateLoss = rjDatePicker1.Value;
            string reason = rjTextBox5.Texts;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(borrowerName) || string.IsNullOrWhiteSpace(borrowerId) ||
                string.IsNullOrWhiteSpace(damagedItem) || quantity <= 0 || string.IsNullOrWhiteSpace(reason))
            {
               NOTIFFILLED2 nOTIFFILLED2 = new NOTIFFILLED2();
               nOTIFFILLED2.Show();

                return;
            }


            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                string customId2 = _generator.GenerateId2();

                // Insert into DAMAGE table
                string insertQuery = "INSERT INTO DAMAGE (ID, [BORROW ID], [ITEM ID], NAME, ITEM, BRAND, MODEL, CATEGORY, QUANTITY, STATUS, REASON, DATE) " +
                                     "VALUES (@Id, @BorrowID, @ItemID, @Name, @Item, @Brand, @Model, @Category, @Quantity, @Status, @Reason, @Date)";

                SqlCommand cmd = new SqlCommand(insertQuery, con);
                cmd.Parameters.AddWithValue("@Id", customId2);
                cmd.Parameters.AddWithValue("@BorrowID", borrowerId);
                cmd.Parameters.AddWithValue("@ItemID", textBox1.Text);
                cmd.Parameters.AddWithValue("@Name", borrowerName);
                cmd.Parameters.AddWithValue("@Item", damagedItem);
                cmd.Parameters.AddWithValue("@Brand", textBox2.Text);
                cmd.Parameters.AddWithValue("@Model", textBox3.Text);
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@Quantity", quantity);  // Quantity for DAMAGE
                cmd.Parameters.AddWithValue("@Status", "PENDING");
                cmd.Parameters.AddWithValue("@Reason", reason);
                cmd.Parameters.AddWithValue("@Date", dateLoss);
                cmd.ExecuteNonQuery();

                string customId = _generator.GenerateId();

                // Insert into RETURN table
                string insertReturnQuery = "INSERT INTO [RETURN] (ID, [BORROW ID], [ITEM ID], NAME, ITEM, BRAND, MODEL, CATEGORY, QUANTITY, STATUS, DATE) " +
                                           "VALUES (@Id, @BorrowID, @ItemID, @Name, @Item, @Brand, @Model, @Category, @ReturnQuantity, @ReturnStatus, @ReturnDate)";

                SqlCommand cmdreturn = new SqlCommand(insertReturnQuery, con);
                cmdreturn.Parameters.AddWithValue("@Id", customId);
                cmdreturn.Parameters.AddWithValue("@BorrowID", borrowerId);
                cmdreturn.Parameters.AddWithValue("@ItemID", textBox1.Text);
                cmdreturn.Parameters.AddWithValue("@Name", borrowerName);
                cmdreturn.Parameters.AddWithValue("@Item", damagedItem);
                cmdreturn.Parameters.AddWithValue("@Brand", textBox2.Text);
                cmdreturn.Parameters.AddWithValue("@Model", textBox3.Text);
                cmdreturn.Parameters.AddWithValue("@Category", category);
                cmdreturn.Parameters.AddWithValue("@ReturnQuantity", _returnQuantity); // Calculate the return quantity
                cmdreturn.Parameters.AddWithValue("@ReturnStatus", "RETURNED");
                cmdreturn.Parameters.AddWithValue("@ReturnDate", dateLoss);


                cmdreturn.ExecuteNonQuery(); // Execut

                UpdateQuantityInTable(con, "IT", textBox1.Text, _returnQuantity);
                UpdateQuantityInTable(con, "ITEMS", textBox1.Text, _returnQuantity);
                UpdateQuantityInTable(con, "SCIENCE", textBox1.Text, _returnQuantity);
                UpdateQuantityInTable(con, "SPORTS", textBox1.Text, _returnQuantity);

                DeleteBorrowRecord(con, borrowerId);

            }

            transaction.GetItems();
            transaction.GetItems2();
            transaction.GetItems3();

            // Show confirmation message
            NOTIFREPORTED nOTIFREPORTED = new NOTIFREPORTED();
            nOTIFREPORTED.ShowDialog();
            this.Close();


        }

        private void UpdateQuantityInTable(SqlConnection con, string tableName, string itemId, int returnQuantity)
        {
            string updateQuery = $"UPDATE {tableName} SET QUANTITY = QUANTITY + @ReturnQuantity WHERE ID = @ItemID";
            SqlCommand cmdUpdate = new SqlCommand(updateQuery, con);
            cmdUpdate.Parameters.AddWithValue("@ReturnQuantity", returnQuantity);
            cmdUpdate.Parameters.AddWithValue("@ItemID", itemId);
            cmdUpdate.ExecuteNonQuery();
        }

        private void DeleteBorrowRecord(SqlConnection con, string borrowerId)
        {
            string deleteQuery = "DELETE FROM BORROW WHERE ID = @BorrowID";
            SqlCommand cmdDelete = new SqlCommand(deleteQuery, con);
            cmdDelete.Parameters.AddWithValue("@BorrowID", borrowerId);
            cmdDelete.ExecuteNonQuery();
        }

        private void DAMAGEFORM_Load(object sender, EventArgs e)
        {
            rjTextBox2.Texts = borrowerId;
            rjTextBox1.Texts = borrowerName;
            rjTextBox3.Texts = damagedItem;
            rjTextBox6.Texts = category;
            rjTextBox5.Texts = reason;

        }

        public class CustomIdGenerator
        {
            private string _prefix;
            private int _currentNumber;
            private string _prefix2;
            private int _currentNumber2;

            public CustomIdGenerator(string prefix, string prefix2)
            {
                _prefix = prefix;
                _prefix2 = prefix2;
                _currentNumber = GetLastUsedNumberFromReturn() + 1;
                _currentNumber2 = GetLastUsedNumberFromDamage() + 1;
            }

            // Separate method for [RETURN] table
            private int GetLastUsedNumberFromReturn()
            {
                using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        "SELECT COALESCE(MAX(CAST(SUBSTRING(ID, LEN(@prefix) + 1, LEN(ID) - LEN(@prefix)) AS INT)), 0) FROM [RETURN] WHERE ID LIKE @prefix + '%'",
                        connection);
                    command.Parameters.AddWithValue("@prefix", _prefix);

                    var result = command.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToInt32(result) : 0;
                }
            }

            // Separate method for DAMAGE table
            private int GetLastUsedNumberFromDamage()
            {
                using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678"))
                {
                    connection.Open();
                    SqlCommand command2 = new SqlCommand(
                        "SELECT COALESCE(MAX(CAST(SUBSTRING(ID, LEN(@prefix2) + 1, LEN(ID) - LEN(@prefix2)) AS INT)), 0) FROM DAMAGE WHERE ID LIKE @prefix2 + '%'",
                        connection);
                    command2.Parameters.AddWithValue("@prefix2", _prefix2);

                    var result2 = command2.ExecuteScalar();
                    return result2 != DBNull.Value ? Convert.ToInt32(result2) : 0;
                }
            }

            public string GenerateId()
            {
                string id = $"{_prefix}{_currentNumber:D3}";
                _currentNumber++;
                return id;
            }



            public string GenerateId2()
            {
                string id = $"{_prefix2}{_currentNumber2:D3}";
                _currentNumber2++;
                return id;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rjTextBox1.Texts) ||
         !string.IsNullOrWhiteSpace(rjTextBox6.Texts) ||
         !string.IsNullOrWhiteSpace(rjTextBox2.Texts) ||
         !string.IsNullOrWhiteSpace(rjTextBox3.Texts) ||
         !string.IsNullOrWhiteSpace(rjTextBox4.Texts))
            {

                using (NOTIFNOTICE5 nOTIFNOTICE = new NOTIFNOTICE5(this))
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
