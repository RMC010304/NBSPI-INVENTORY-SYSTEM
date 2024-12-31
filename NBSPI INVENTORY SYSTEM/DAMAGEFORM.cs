using RJCodeAdvance.RJControls;
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
        private string Description;
        private string PhotoPath;

        private string _borrowId;

        private int _remainingQuantity;
        private int _returnQuantity;

        private CustomIdGenerator _generator;
        private CustomIdGenerator _generator2;

        public DAMAGEFORM(TRANSACTION transaction, // Accept transaction as a parameter
    string borrowerId,
    string borrowerName,
    string damagedItem,
    string category,
    
    string reason,
    string borrowId,
    string description,  // New parameter for description
    string photoPath)
            // New parameter for photo path or URL)
        {
            InitializeComponent();

            this.transaction = transaction;

            this.borrowerId = borrowerId;
            this.borrowerName = borrowerName;
            this.damagedItem = damagedItem;
            this.category = category;
            this.quantity = _remainingQuantity;
            this.reason = reason;
            this.Description = description;  // Store description
            this.PhotoPath = photoPath;
            _borrowId = borrowId;
         

            _generator = new CustomIdGenerator("R", "DL");



            rjTextBox4.Texts = quantity.ToString();
            LoadBorrowData();

            
        }

        private void LoadBorrowData()
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string query = "SELECT ID, NAME, ITEM, [ITEM ID], BRAND, MODEL, CATEGORY, QUANTITY, PHOTO, DESCRIPTION FROM BORROW WHERE ID = @BorrowID";
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
                    Description = reader["DESCRIPTION"].ToString();

                    // Read the byte array from the database
                    byte[] photoData = reader["PHOTO"] as byte[];

                    string itemId = reader["ITEM ID"].ToString();
                    string brand = reader["BRAND"].ToString();
                    string model = reader["MODEL"].ToString();

                    rjTextBox1.Texts = borrowerId;
                    rjTextBox2.Texts = borrowerName;
                    rjTextBox3.Texts = damagedItem;
                    rjTextBox6.Texts = category;
                    rjTextBox7.Texts = Description;
                    rjTextBox4.Texts = quantity.ToString();

                    textBox1.Text = itemId;
                    textBox2.Text = brand;
                    textBox3.Text = model;

                    // Check if photoData is not null and convert it to an image
                    if (photoData != null && photoData.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(photoData))
                        {
                            pictureBox2.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        pictureBox2.Image = null;  // Clear the picture if no image data exists
                    }

                    // Determine the source table based on ITEM ID prefix
                    if (!itemId.StartsWith("HM") && !itemId.StartsWith("SL") && !itemId.StartsWith("SE"))
                    {
                        // If the ITEM ID does not start with a known prefix, it belongs to the IT table
                        rjTextBox4.Enabled = false;
                    }
                    else
                    {
                        // Otherwise, it belongs to ITEMS, SCIENCE, or SPORTS
                        rjTextBox4.Enabled = true;
                    }
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
            string description = rjTextBox7.Texts;

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

                // Get the borrowed quantity from the BORROW table
                string selectQuery = "SELECT QUANTITY FROM BORROW WHERE [ID] = @BorrowID AND ITEM = @Item";
                SqlCommand selectCmd = new SqlCommand(selectQuery, con);
                selectCmd.Parameters.AddWithValue("@BorrowID", borrowerId);
                selectCmd.Parameters.AddWithValue("@Item", damagedItem);

                int borrowedQuantity = 0;
                object result = selectCmd.ExecuteScalar();
                if (result != null)
                {
                    borrowedQuantity = Convert.ToInt32(result);
                }
                else
                {
                   NOTIFNOTFOUND2 nOTIFNOTFOUND2 = new NOTIFNOTFOUND2();
                    nOTIFNOTFOUND2.Show();
                }

                // Ensure the damage quantity is not greater than borrowed quantity
                if (quantity > borrowedQuantity)
                {
                    NOTIFEXCEED nOTIFEXCEED = new NOTIFEXCEED();
                    nOTIFEXCEED.Show();
                }

                string customId2 = _generator.GenerateId2();

                // Insert into DAMAGE table for the damaged items
                string insertQuery = "INSERT INTO DAMAGE (ID, [BORROW ID], [ITEM ID], NAME, ITEM, BRAND, MODEL, CATEGORY, QUANTITY, STATUS, REASON, DATE, DESCRIPTION, PHOTO) " +
                                     "VALUES (@Id, @BorrowID, @ItemID, @Name, @Item, @Brand, @Model, @Category, @Quantity, @Status, @Reason, @Date, @Description, @Photo)";

                using (SqlCommand insertCmd = new SqlCommand(insertQuery, con))
                {
                    insertCmd.Parameters.AddWithValue("@Id", customId2);
                    insertCmd.Parameters.AddWithValue("@BorrowID", borrowerId);
                    insertCmd.Parameters.AddWithValue("@ItemID", textBox1.Text);
                    insertCmd.Parameters.AddWithValue("@Name", borrowerName);
                    insertCmd.Parameters.AddWithValue("@Item", damagedItem);
                    insertCmd.Parameters.AddWithValue("@Brand", textBox2.Text);
                    insertCmd.Parameters.AddWithValue("@Model", textBox3.Text);
                    insertCmd.Parameters.AddWithValue("@Category", category);
                    insertCmd.Parameters.AddWithValue("@Quantity", quantity);
                    insertCmd.Parameters.AddWithValue("@Status", "PENDING");
                    insertCmd.Parameters.AddWithValue("@Reason", reason);
                    insertCmd.Parameters.AddWithValue("@Date", dateLoss);
                    insertCmd.Parameters.AddWithValue("@Description", description);

                    // Convert the image in PictureBox to a byte array for storage
                    if (pictureBox2.Image != null)
                    {
                        using (var ms = new MemoryStream())
                        {
                            // Clone the image to avoid locking issues
                            using (var imageClone = new Bitmap(pictureBox2.Image))
                            {
                                imageClone.Save(ms, pictureBox2.Image.RawFormat);
                            }
                            byte[] photo = ms.ToArray();
                            insertCmd.Parameters.AddWithValue("@Photo", photo);
                        }
                    }
                    else
                    {
                        insertCmd.Parameters.AddWithValue("@Photo", DBNull.Value);
                    }

                    int rowsAffected = insertCmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Failed to insert data into the DAMAGE table.");
                        return;
                    }
                }

                // Update or delete the BORROW table based on the damage quantity
                if (quantity < borrowedQuantity)
                {
                    string updateQuery = "UPDATE BORROW SET QUANTITY = @RemainingQuantity WHERE [ID] = @BorrowID AND ITEM = @Item";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@RemainingQuantity", borrowedQuantity - quantity);
                    updateCmd.Parameters.AddWithValue("@BorrowID", borrowerId);
                    updateCmd.Parameters.AddWithValue("@Item", damagedItem);
                    updateCmd.ExecuteNonQuery();
                }
                else if (quantity == borrowedQuantity)
                {
                    string deleteQuery = "DELETE FROM BORROW WHERE [ID] = @BorrowID AND ITEM = @Item";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                    deleteCmd.Parameters.AddWithValue("@BorrowID", borrowerId);
                    deleteCmd.Parameters.AddWithValue("@Item", damagedItem);
                    deleteCmd.ExecuteNonQuery();
                }
            }

            transaction.GetItems();
            transaction.GetItems2();
            transaction.GetItems3();

            NOTIFREPORTED nOTIFREPORTED = new NOTIFREPORTED();
            nOTIFREPORTED.Show();
            this.Close();


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
