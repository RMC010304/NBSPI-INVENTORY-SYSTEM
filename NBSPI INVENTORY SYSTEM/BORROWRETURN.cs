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
    public partial class BORROWRETURN : Form
    {
       
        private string _borrowId;
        private string _itemId;
        private int _borrowedQuantity;
        private string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";
        private TRANSACTION transaction;
        private CustomIdGenerator _generator;

        public BORROWRETURN(TRANSACTION control ,string borrowId, string itemId, int borrowedQuantity)
        {
            InitializeComponent();

            transaction = control;


            _borrowId = borrowId;
            _itemId = itemId;
            _borrowedQuantity = borrowedQuantity;

            _generator = new CustomIdGenerator("R");

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(rjTextBox1.Texts.Trim()))
            {
                NOTIFFILLED2 nOTIFFILLED2 = new NOTIFFILLED2();
                nOTIFFILLED2.Show();
                return;
            }

            // Check if the input is a numeric value greater than zero
            if (!int.TryParse(rjTextBox1.Texts.Trim(), out int returnQuantity) || returnQuantity <= 0)
            {
                NOTIFNUMERIC2 nOTIFNUMERIC2 = new NOTIFNUMERIC2();
                nOTIFNUMERIC2.Show();
                return;
            }

            if (returnQuantity > _borrowedQuantity)
            {
                NOTIFEXCEED nOTIFEXCEED = new NOTIFEXCEED();
                nOTIFEXCEED.Show();
                return;
            }

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                string customId = _generator.GenerateId();

                // Transfer returned quantity to RETURN table including DESCRIPTION and PHOTO
                string insertReturnQuery = "INSERT INTO [RETURN] (ID, [BORROW ID], [ITEM ID], NAME, ITEM, BRAND, MODEL, CATEGORY, QUANTITY, STATUS, DATE, DESCRIPTION, PHOTO) " +
                                           "SELECT @Id, ID, [ITEM ID], NAME, ITEM, BRAND, MODEL, CATEGORY, @Quantity, @Status, GETDATE(), DESCRIPTION, PHOTO " +
                                           "FROM BORROW WHERE ID = @BorrowId";
                SqlCommand insertReturnCmd = new SqlCommand(insertReturnQuery, con);
                insertReturnCmd.Parameters.AddWithValue("@Id", customId);
                insertReturnCmd.Parameters.AddWithValue("@Quantity", returnQuantity);
                insertReturnCmd.Parameters.AddWithValue("@Status", "RETURNED");
                insertReturnCmd.Parameters.AddWithValue("@BorrowId", _borrowId);
                insertReturnCmd.ExecuteNonQuery();

                if (returnQuantity == _borrowedQuantity)
                {
                    // Delete the row if all items are returned
                    string deleteBorrowQuery = "DELETE FROM BORROW WHERE ID = @BorrowId";
                    SqlCommand deleteBorrowCmd = new SqlCommand(deleteBorrowQuery, con);
                    deleteBorrowCmd.Parameters.AddWithValue("@BorrowId", _borrowId);
                    deleteBorrowCmd.ExecuteNonQuery();
                }
                else
                {
                    // Update the remaining quantity in the BORROW table
                    int remainingQuantity = _borrowedQuantity - returnQuantity;
                    string updateBorrowQuery = "UPDATE BORROW SET QUANTITY = @RemainingQuantity WHERE ID = @BorrowId";
                    SqlCommand updateBorrowCmd = new SqlCommand(updateBorrowQuery, con);
                    updateBorrowCmd.Parameters.AddWithValue("@RemainingQuantity", remainingQuantity);
                    updateBorrowCmd.Parameters.AddWithValue("@BorrowId", _borrowId);
                    updateBorrowCmd.ExecuteNonQuery();
                }

                // Determine the relevant inventory table based on the item ID prefix
                string inventoryTable = GetInventoryTable(_itemId);

                // Update inventory quantity in the relevant table
                string updateInventoryQuery = $"UPDATE {inventoryTable} SET QUANTITY = QUANTITY + @Quantity WHERE ID = @ItemId";
                SqlCommand updateInventoryCmd = new SqlCommand(updateInventoryQuery, con);
                updateInventoryCmd.Parameters.AddWithValue("@Quantity", returnQuantity);
                updateInventoryCmd.Parameters.AddWithValue("@ItemId", _itemId);
                updateInventoryCmd.ExecuteNonQuery();

                // Refresh transaction data
                transaction.GetItems();
                transaction.GetItems2();
                transaction.GetItems3();

                // Show notification
                NOTIFRETURNED nOTIFRETURNED = new NOTIFRETURNED();
                nOTIFRETURNED.Show();
                this.Close();
            }
        }
            
        private string GetInventoryTable(string itemId)
        {
            if (itemId.StartsWith("HM")) return "ITEMS";
            else if (itemId.StartsWith("SL")) return "SCIENCE";
            else if (itemId.StartsWith("SE")) return "SPORTS";
            else return "IT";
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
                    SqlCommand command = new SqlCommand("SELECT COALESCE(MAX(CAST(SUBSTRING(ID, LEN(@prefix) + 1, LEN(ID) - LEN(@prefix)) AS INT)), 0) FROM [RETURN] WHERE ID LIKE @prefix + '%'", connection);
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

        private void BORROWRETURN_Load(object sender, EventArgs e)
        {

        }
    }
    
}
