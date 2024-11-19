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
    public partial class DAMAGEFIXED : Form
    {
        private string _damageId;
        private string _itemId;
        private int _damageQuantity;
        private string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";
        private CustomIdGenerator _generator;
        private TRANSACTION transaction;
        public DAMAGEFIXED(TRANSACTION control,string damageId, string itemId, int damageQuantity)
        {
            InitializeComponent();

            transaction = control;

            _damageId = damageId;
            _itemId = itemId;
            _damageQuantity = damageQuantity;


            _generator = new CustomIdGenerator("R");
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                string customId = _generator.GenerateId();
                // Insert into RETURN table
                string insertReturnQuery = "INSERT INTO [RETURN] (ID, [BORROW ID], [ITEM ID], NAME, ITEM, BRAND, MODEL, CATEGORY, QUANTITY,STATUS, DATE) " +
                              "SELECT @id , ID, [ITEM ID], NAME, ITEM, BRAND, MODEL, CATEGORY, QUANTITY,@Status, GETDATE() " +
                              "FROM DAMAGE WHERE ID = @damageId";

                SqlCommand insertReturnCmd = new SqlCommand(insertReturnQuery, con);
                insertReturnCmd.Parameters.AddWithValue("@id", customId);
                insertReturnCmd.Parameters.AddWithValue("@Status", "RETURNED");
                insertReturnCmd.Parameters.AddWithValue("@damageId", _damageId);
                insertReturnCmd.ExecuteNonQuery();

               

                // Delete from DAMAGE table
                string deleteDamageQuery = "DELETE FROM DAMAGE WHERE ID = @DamageId";
                SqlCommand deleteDamageCmd = new SqlCommand(deleteDamageQuery, con);
                deleteDamageCmd.Parameters.AddWithValue("@damageId", _damageId);
                deleteDamageCmd.ExecuteNonQuery();

                string inventoryTable = GetInventoryTable(_itemId);

                // Update the quantity in the respective inventory table
                string updateInventoryQuery = $"UPDATE {inventoryTable} SET QUANTITY = QUANTITY + @Quantity WHERE ID = @ItemId";
                SqlCommand updateInventoryCmd = new SqlCommand(updateInventoryQuery, con);
                updateInventoryCmd.Parameters.AddWithValue("@Quantity", _damageQuantity); // Ensure _damageQuantity is defined with the correct quantity
                updateInventoryCmd.Parameters.AddWithValue("@ItemId", _itemId);
                updateInventoryCmd.ExecuteNonQuery();


                transaction.GetItems();
                transaction.GetItems2();
                transaction.GetItems3();

           

                this.Close();
                NOTIFRETURNED nOTIFRETURNED = new NOTIFRETURNED();
                nOTIFRETURNED.Show();


            }
        }

        private string GetInventoryTable(string itemId)
        {
            if (itemId.StartsWith("IT")) return "IT";
            else if (itemId.StartsWith("SL")) return "SCIENCE";
            else if (itemId.StartsWith("SE")) return "SPORTS";
            else return "ITEMS";
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

    }
}
