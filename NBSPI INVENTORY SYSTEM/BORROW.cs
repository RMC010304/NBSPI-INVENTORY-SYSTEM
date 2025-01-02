using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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

        public event Action OnStatusUpdated;

        public BORROW(TRANSACTION control)
        {
            InitializeComponent();
            transaction = control;

            _generator = new CustomIdGenerator("B");

       
            timer1.Tick += Timer_Tick;

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
                string[] tables = { "IT", "ITEMS", "SCIENCE", "SPORTS" };

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
                                // Populate the fields
                                rjTextBox3.Texts = reader["ITEM"].ToString();
                                textBox1.Text = reader["BRAND"].ToString();
                                textBox2.Text = reader["MODEL"].ToString();
                                rjTextBox5.Texts = reader["DESCRIPTION"]?.ToString();

                                // Check if the table is "IT"
                                if (table == "IT")
                                {
                                    rjTextBox4.Texts = "1"; // Set quantity to "1"
                                    rjTextBox4.Enabled = false; // Disable rjTextBox4
                                }
                                else
                                {
                                    rjTextBox4.Texts = reader["QUANTITY"].ToString(); // Use the actual quantity
                                    rjTextBox4.Enabled = true; // Enable rjTextBox4
                                }

                                // Populate photo
                                byte[] photoBytes = reader["PHOTO"] as byte[];
                                pictureBox2.Image = photoBytes != null ? Image.FromStream(new MemoryStream(photoBytes)) : null;

                                return; // Exit after finding a match
                            }
                        }
                    }
                }

                // Enable rjTextBox4 if no match is found in any table
                rjTextBox4.Enabled = true;
            }
        }

        private void FetchItemDetails2(string itemName)
        {
            using (SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678"))
            {
                con.Open();

                string queryTemplate = "SELECT * FROM {0} WHERE ITEM = @ItemName";
                string[] tables = { "ITEMS", "SCIENCE", "SPORTS" };

                foreach (string table in tables)
                {
                    string query = string.Format(queryTemplate, table);
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ItemName", itemName);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate fields with the retrieved data
                                textBox1.Text = reader["BRAND"].ToString();
                                textBox2.Text = reader["MODEL"].ToString();
                                rjTextBox2.Texts = reader["ID"]?.ToString();
                                rjTextBox4.Texts = reader["QUANTITY"].ToString();
                                rjTextBox5.Texts = reader["DESCRIPTION"]?.ToString();

                                // Populate photo
                                byte[] photoBytes = reader["PHOTO"] as byte[];
                                pictureBox2.Image = photoBytes != null ? Image.FromStream(new MemoryStream(photoBytes)) : null;

                                return; // Exit after finding the first match
                            }
                        }
                    }
                }
            }
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {

            string itemId = rjTextBox2.Texts.Trim();
            int quantityToBorrow = 0; // Initialize to avoid uninitialized variable error

            // Determine the table name based on the itemId prefix
            string tableName;
            if (itemId.StartsWith("HM"))
            {
                tableName = "ITEMS";
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
                tableName = "IT";
            }

            // Validate quantity only if the table is NOT "IT"
            bool isITItem = tableName == "IT";
            if (!isITItem)
            {
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
            }

            string borrowerName = rjTextBox1.Texts.Trim();
            DateTime borrowedDate = rjDatePicker2.Value;
            DateTime returnDate = rjDatePicker1.Value;

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

                // Get current stock if the table is NOT "IT"
                int currentStock = 0;
                if (!isITItem)
                {
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

                    currentStock = Convert.ToInt32(stockResult);

                    if (quantityToBorrow > currentStock)
                    {
                        NOTIFENOUGH nOTIFENOUGH = new NOTIFENOUGH();
                        nOTIFENOUGH.Show();
                        return;
                    }
                }

                // Generate custom ID
                string customId = _generator.GenerateId();

                // Insert borrow record into BORROW table
                string insertBorrowQuery = isITItem
                    ? "INSERT INTO BORROW (ID, [ITEM ID], NAME, ITEM, BRAND, MODEL, CATEGORY, QUANTITY, STATUS, DATE, DATE2, DESCRIPTION, PHOTO) " +
                      "VALUES (@Id, @ItemId, @Name, @Item, @Brand, @Model, @Category, 1, @Status, @Date, @Date2, @Description, @Photo)"
                    : "INSERT INTO BORROW (ID, [ITEM ID], NAME, ITEM, BRAND, MODEL, CATEGORY, QUANTITY, STATUS, DATE, DATE2, DESCRIPTION, PHOTO) " +
                      "VALUES (@Id, @ItemId, @Name, @Item, @Brand, @Model, @Category, @Quantity, @Status, @Date, @Date2, @Description, @Photo)";

                SqlCommand insertBorrowCmd = new SqlCommand(insertBorrowQuery, con);
                insertBorrowCmd.Parameters.AddWithValue("@Id", customId);
                insertBorrowCmd.Parameters.AddWithValue("@ItemId", itemId);
                insertBorrowCmd.Parameters.AddWithValue("@Name", borrowerName);
                insertBorrowCmd.Parameters.AddWithValue("@Item", rjTextBox3.Texts.Trim());
                insertBorrowCmd.Parameters.AddWithValue("@Brand", textBox1.Text.Trim());
                insertBorrowCmd.Parameters.AddWithValue("@Model", textBox2.Text.Trim());
                insertBorrowCmd.Parameters.AddWithValue("@Category", rjComboBox1.Texts.Trim());
                if (!isITItem)
                {
                    insertBorrowCmd.Parameters.AddWithValue("@Quantity", quantityToBorrow);
                }
                insertBorrowCmd.Parameters.AddWithValue("@Status", "UNRETURNED");
                insertBorrowCmd.Parameters.AddWithValue("@Date", borrowedDate);
                insertBorrowCmd.Parameters.AddWithValue("@Date2", returnDate);
                insertBorrowCmd.Parameters.AddWithValue("@Description", rjTextBox5.Texts.Trim());

                // Handle the PHOTO column
                if (pictureBox2.Image != null)
                {
                    // Convert the image to a byte array
                    using (var ms = new MemoryStream())
                    {
                        pictureBox2.Image.Save(ms, pictureBox2.Image.RawFormat);
                        byte[] photo = ms.ToArray();
                        insertBorrowCmd.Parameters.AddWithValue("@Photo", photo); // Add as byte[]
                    }
                }
                else
                {
                    // Set DBNull.Value if no image is provided
                    SqlParameter photoParam = new SqlParameter("@Photo", SqlDbType.VarBinary);
                    photoParam.Value = DBNull.Value;
                    insertBorrowCmd.Parameters.Add(photoParam);
                }

                insertBorrowCmd.ExecuteNonQuery();

                // Remove the item from the IT table if it is from IT
                if (isITItem)
                {
                    string deleteItemQuery = "DELETE FROM IT WHERE ID = @ItemId";
                    SqlCommand deleteItemCmd = new SqlCommand(deleteItemQuery, con);
                    deleteItemCmd.Parameters.AddWithValue("@ItemId", itemId);
                    deleteItemCmd.ExecuteNonQuery();
                }

                // If the current date is past the return date, update status to OVERDUE
                if (DateTime.Now > returnDate)
                {
                    string updateStatusQuery = "UPDATE BORROW SET STATUS = 'OVERDUE' WHERE ID = @Id";
                    SqlCommand updateStatusCmd = new SqlCommand(updateStatusQuery, con);
                    updateStatusCmd.Parameters.AddWithValue("@Id", customId);
                    updateStatusCmd.ExecuteNonQuery();
                }

                // Update stock in the specified table
                if (!isITItem)
                {
                    string updateStockQuery = $"UPDATE {tableName} SET QUANTITY = QUANTITY - @Quantity WHERE ID = @ItemId";
                    SqlCommand updateStockCmd = new SqlCommand(updateStockQuery, con);
                    updateStockCmd.Parameters.AddWithValue("@Quantity", quantityToBorrow);
                    updateStockCmd.Parameters.AddWithValue("@ItemId", itemId);
                    updateStockCmd.ExecuteNonQuery();
                }

                NOTIFSUCCESS nOTIFSUCCESS = new NOTIFSUCCESS();
                nOTIFSUCCESS.Show();
                this.Close();
            }

            // Clear inputs
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
            string itemName = rjTextBox3.Texts.Trim(); // Get the text from rjTextBox3
            if (!string.IsNullOrEmpty(itemName))
            {
                FetchItemDetails2(itemName);
            }
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
            timer1.Start();
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

        private void rjDatePicker1_ValueChanged(object sender, EventArgs e)
        {
         
        }

        // Event handler for Timer Tick
        private void Timer_Tick(object sender, EventArgs e)
        {
            CheckAndUpdateOverdueStatus(); // Check for overdue items and update their status
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckAndUpdateOverdueStatus();
        }

        private void CheckAndUpdateOverdueStatus()
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                // Query to find all borrow records where the return date has passed and status is still UNRETURNED
                string query = "SELECT ID, DATE2 FROM BORROW WHERE STATUS = 'UNRETURNED' AND DATE2 < @CurrentDate";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CurrentDate", DateTime.Now);

                SqlDataReader reader = cmd.ExecuteReader();

                bool statusUpdated = false;  // To track if any status was updated

                List<string> overdueBorrowIds = new List<string>();

                while (reader.Read())
                {
                    string borrowId = reader["ID"].ToString();
                    overdueBorrowIds.Add(borrowId);  // Add the borrow ID to the list of overdue records
                }

                reader.Close();  // Close the DataReader before executing the update

                // If there are overdue borrow IDs, update their status
                if (overdueBorrowIds.Count > 0)
                {
                    using (SqlConnection updateCon = new SqlConnection(conn))  // Use a new connection for updates
                    {
                        updateCon.Open();

                        foreach (string borrowId in overdueBorrowIds)
                        {
                            string updateQuery = "UPDATE BORROW SET STATUS = 'OVERDUE' WHERE ID = @BorrowId";
                            SqlCommand updateCmd = new SqlCommand(updateQuery, updateCon);
                            updateCmd.Parameters.AddWithValue("@BorrowId", borrowId);
                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                statusUpdated = true;  // Mark that a status update occurred
                            }
                        }
                    }
                }

                // Trigger the event to notify the TRANSACTION form if any status was updated
                if (statusUpdated && OnStatusUpdated != null)
                {
                    OnStatusUpdated.Invoke();
                }
            }
        }
    }
    
}
