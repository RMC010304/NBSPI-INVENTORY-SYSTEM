using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class HMADD : Form
    {
        string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";

       // private CustomIdGenerator _generator;

        int no;
        string thing, br, mod, cat,description;
        byte[] photo;

        private IT hm;
        
        private void label1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(comboBox1.Text) ||
          !string.IsNullOrWhiteSpace(rjComboBox1.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox2.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox3.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox4.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox5.Texts) ||
            photo != null)
            {

                using (NOTIFNOTICE1 nOTIFNOTICE = new NOTIFNOTICE1(this))
                {
                    nOTIFNOTICE.ShowDialog();
                }
            }
            else
            {

                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(comboBox1.Text) ||
          !string.IsNullOrWhiteSpace(rjComboBox1.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox2.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox3.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox4.Texts) ||
          !string.IsNullOrWhiteSpace(rjTextBox5.Texts) ||
            photo != null)
            {

                using (NOTIFNOTICE1 nOTIFNOTICE = new NOTIFNOTICE1(this))
                {
                    nOTIFNOTICE.ShowDialog();
                }
            }
            else
            {

                this.Close();
            }
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            string newItem = comboBox1.Text.Trim();

            // Add the item to the ComboBox regardless of its content
            comboBox1.Items.Add(newItem);

            // Save the item to the database even if it's empty
            SaveComboBoxItemToDatabase(newItem);

            // Clear the ComboBox text for new input
            comboBox1.Text = "";
        }

        private void SaveComboBoxItemToDatabase(string item)
        {
            // Database connection string
            string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";

            try
            {
                // Open a connection to the database
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // SQL query to insert the new item into the ComboBoxItems table
                    string query = "INSERT INTO COMBOBOX (ITEMS) VALUES (@ItemText)";

                    // Create a command to execute the query
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add the item as a parameter to prevent SQL injection
                        cmd.Parameters.AddWithValue("@ItemText", item);

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Silently handle any errors without showing messages
                // You can add logging here if needed for debugging purposes
            }
        }

        private void LoadComboBoxItemsFromDatabase()
        {
            // Database connection string
            string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";

            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // SQL query to retrieve items from the ComboBox table
                    string query = "SELECT ITEMS FROM COMBOBOX";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Clear existing items in the ComboBox
                            comboBox1.Items.Clear();

                            // Add each item from the database to the ComboBox
                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader["ITEMS"].ToString());
                            }
                        }
                    }
                }
            }
            catch
            {
                // Silently handle any errors without showing messages
                // You can add logging here if needed for debugging purposes
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Text = comboBox1.SelectedItem?.ToString();
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.Text.Trim();

            if (!string.IsNullOrWhiteSpace(selectedItem))
            {
                // Remove the selected item from the ComboBox
                comboBox1.Items.Remove(selectedItem);

                // Delete the item from the database
                DeleteComboBoxItemFromDatabase(selectedItem);

                // Clear the ComboBox field
                comboBox1.Text = "";
            }
        }

        private void DeleteComboBoxItemFromDatabase(string item)
        {
            // Database connection string
            string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";

            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // SQL query to delete the item from the ComboBox table
                    string query = "DELETE FROM COMBOBOX WHERE ITEMS = @ItemText";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add the item as a parameter to prevent SQL injection
                        cmd.Parameters.AddWithValue("@ItemText", item);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Silently handle any errors
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            String imageLocation = "";
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Get the image file path.
                        imageLocation = openFileDialog.FileName;

                        // Display the image in the PictureBox.
                        pictureBox2.ImageLocation = imageLocation;

                        // Convert the selected image into a byte array.
                        using (FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                fs.CopyTo(ms);
                                photo = ms.ToArray();
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {

            }
        }

        private void HMADD_Load(object sender, EventArgs e)
        {
            rjDatePicker1.Hide();
            LoadComboBoxItemsFromDatabase();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        public HMADD(IT control, string item = "", string category = "", string brand = "", string model = "", string desc = "", byte[] photoData = null)
        {
            InitializeComponent();

            hm = control;
       

          // no = quantity;
            thing = item;
            cat = category;
            br = brand;
            mod = model;
            description = desc;
            photo = photoData;

          //  _generator = new CustomIdGenerator("IT");
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {

            thing = comboBox1.Text;
            br = rjTextBox2.Texts;
            mod = rjTextBox3.Texts;
            cat = rjComboBox1.Texts;
            description = rjTextBox4.Texts;
            byte[] photoData = null;

            if (pictureBox2.Image != null) // Convert PictureBox image to byte array.
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox2.Image.Save(ms, pictureBox2.Image.RawFormat);
                    photoData = ms.ToArray();
                }
            }

            DateTime day = DateTime.Now;

            using (SqlConnection con = new SqlConnection(conn))
            {
                if (string.IsNullOrWhiteSpace(comboBox1.Text) || string.IsNullOrWhiteSpace(rjTextBox5.Texts) || string.IsNullOrWhiteSpace(rjTextBox2.Texts) || string.IsNullOrWhiteSpace(rjTextBox3.Texts))
                {
                    NOTIFFILLED2 nOTIFFILLED = new NOTIFFILLED2();
                    nOTIFFILLED.Show();
                    return;
                }

              //  if (!int.TryParse(rjTextBox4.Texts, out int no))
              //  {
              //      NOTIFNUMERIC nOTIFNUMERIC = new NOTIFNUMERIC();
              //      nOTIFNUMERIC.Show();
              //      return;
              //  }

                con.Open();

                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM IT WHERE BRAND = @BRAND OR MODEL = @MODEL", con);
                checkCmd.Parameters.Add("@BRAND", SqlDbType.VarChar).Value = rjTextBox2.Texts;
                checkCmd.Parameters.Add("@MODEL", SqlDbType.VarChar).Value = rjTextBox3.Texts;

                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    NOTIFEXIST nOTIFEXIST = new NOTIFEXIST();
                    nOTIFEXIST.Show();
                    return;
                }

             //   string customId = _generator.GenerateId();

                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.IT ([ID],[ITEM],[BRAND],[MODEL],[CATEGORY],[STATUS],[DATE],[DESCRIPTION],[PHOTO]) Values (@ID,@ITEM,@BRAND,@MODEL,@CATEGORY,@STATUS,@DATE,@DESCRIPTION,@PHOTO)", con);
                cmd.Parameters.AddWithValue("@ID", rjTextBox5.Texts);
                cmd.Parameters.AddWithValue("@ITEM", thing);
                cmd.Parameters.AddWithValue("@BRAND", br);
                cmd.Parameters.AddWithValue("@MODEL", mod);
                cmd.Parameters.AddWithValue("@CATEGORY", cat);
                //cmd.Parameters.AddWithValue("@QUANTITY", no);
                cmd.Parameters.AddWithValue("@STATUS", "AVAILABLE");
                cmd.Parameters.AddWithValue("@DATE", DateTime.Now);
                cmd.Parameters.AddWithValue("@DESCRIPTION", description);


                if (photoData != null) // If photo data is available
                {
                    cmd.Parameters.Add("@PHOTO", SqlDbType.VarBinary).Value = photoData;
                }
                else // If no photo is uploaded
                {
                    cmd.Parameters.Add("@PHOTO", SqlDbType.VarBinary).Value = DBNull.Value;
                }

                cmd.ExecuteNonQuery();
            }

            comboBox1.Text = string.Empty;
            rjTextBox2.Texts = string.Empty;
            rjTextBox3.Texts = string.Empty;
            rjComboBox1.SelectedIndex = -1; // Clear combo box selection
            rjTextBox4.Texts = string.Empty;
            rjTextBox5.Texts = string.Empty;
            pictureBox2.Image = null;


            hm.GetItems();
            NOTIFADD nOTIFADD = new NOTIFADD();
            nOTIFADD.Show();
            this.Close();

          


        }

     /**   public class CustomIdGenerator
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
                    SqlCommand command = new SqlCommand("SELECT COALESCE(MAX(CAST(SUBSTRING(ID, LEN(@prefix) + 1, LEN(ID) - LEN(@prefix)) AS INT)), 0) FROM IT WHERE ID LIKE @prefix + '%'", connection);
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
        } **/
    }
}
