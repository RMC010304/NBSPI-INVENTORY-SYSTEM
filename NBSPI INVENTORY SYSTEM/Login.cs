using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Reflection.Emit;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class Login : Form
    {
        private SerialPortManager serialPortManager;
        private string LastScannedUID = "";
        private string connectionString = ("Data Source=localhost;Initial Catalog=IT_RES;Integrated Security=True");

        private HashSet<string> validUIDs = new HashSet<string> { "49 F0 61 9F", "19 85 01 9F", "39 2E 1B 9F", "29 85 1D 9F", "69 36 47 9F" , "39 B5 3A 9F" };

        private bool isRegistrationComplete = false;
        private bool allowScanning = true;
        private bool isNotifDeniedShown = false;

   
        public Login()
        {
            InitializeComponent();
            serialPortManager = new SerialPortManager("COM5");
            serialPortManager.OnDataReceived += DisplayRFIDUID;
            serialPortManager.OpenPort();
            timer3.Stop();

            checkBox5.CheckedChanged += ShowPasswordCheckBox_CheckedChanged;
            checkBox6.CheckedChanged += ShowPasswordCheckBox_CheckedChanged;
        }

        private void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            rjTextBox2.PasswordChar = !checkBox5.Checked;
            rjTextBox3.PasswordChar = !checkBox6.Checked;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }

        public class SerialPortManager
        {
            private SerialPort serialPort;
            public event Action<string> OnDataReceived;


            public SerialPortManager(string COM5)
            {
                serialPort = new SerialPort
                {
                    PortName = COM5,
                    BaudRate = 9600,
                    Parity = Parity.None,
                    StopBits = StopBits.One,
                    DataBits = 8,
                    Handshake = Handshake.None,
                    Encoding = System.Text.Encoding.ASCII
                };

                serialPort.DataReceived += SerialPort_DataReceived;
            }

            public void OpenPort()
            {
            try { 
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }
            }
            catch(Exception ex)
                {
                MessageBox.Show("Error opening the serial port: " + ex.Message);
                 }
            }

            public void ClosePort()
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
            }

            private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
            {
                try
                {
                    string data = serialPort.ReadLine().Trim();
                    OnDataReceived?.Invoke(data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading data: " + ex.Message);
                }
            }
        }

        private bool canRescan = true;

        private void DisplayRFIDUID(string uid)
        {
            if (!string.IsNullOrEmpty (uid))
            {
                if (uid == LastScannedUID && !canRescan)
                {
                    return;
                }

                LastScannedUID = uid;

                this.Invoke(new MethodInvoker(() =>
                {
                    textBox1.Text = uid;


                    if (!allowScanning)
                    {
                        MessageBox.Show("Please complete the registration process.");
                        return;
                    }

                    if (isRegistrationComplete)
                    {
                        ProcessValidation(uid);
                    }
                    else
                    {
                        ProcessRegistration(uid);
                    }

                    canRescan = true;
                }));
            }
        }

        private void ProcessValidation(string uid)
        {
            label1.Text = $"Scanned UID: {uid}";

            if (scan.Visible)
            {

                ValidateRFID(uid);
            }
            else
            {
                MessageBox.Show("Validation mode not active.");
            }
        }

        private void ProcessRegistration(string uid)
        {
            // Call the new validation method to handle checks
            if (!IsUIDValidForRegistration(uid))
            {
              
                return; // Exit if invalid UID or already registered
            }

            if (!isNotifDeniedShown)
            {
               
                NOTIFDENIED nOTIFDENIED = new NOTIFDENIED();
                nOTIFDENIED.Show();
            }
            else
            {
                // Optional: Handle the case where the NOTIFDENIED form is disabled
                Console.WriteLine("NOTIFDENIED form is permanently disabled.");
            }

            StartPanelAnimation();




        }

        private void StartPanelAnimation()
        {

            timer3.Start();
        }

        private bool IsUIDValidForRegistration(string uid)
        {
            // Check if UID is in the valid list
            if (!validUIDs.Contains(uid))
            {
                MessageBox.Show("This UID is not valid and cannot be registered.");
                return false; // Invalid UID
            }

            // Check if the UID is already registered
            if (IsUIDRegistered(uid))
            {

                ValidateRFID(uid);
                return false;// Already registered
            }

            // UID is valid and not yet registered
            return true;
        }


        private void RegisterUID(string uid, string name)
        {

            if (!IsUIDValidForRegistration(uid))
            {
                return;
            }

            if (isRegistrationComplete)
            {
                return;
            }

            int it = checkBox1.Checked ? 1 : 0;
            int hm = checkBox2.Checked ? 1 : 0;
            int science = checkBox3.Checked ? 1 : 0;
            int sports = checkBox4.Checked ? 1 : 0;

            using (var connection = CreateConnection())
            {
                // Insert into the MySQL database
                string query = @"INSERT INTO dbo.register 
                         (UID, Name, password, IT, HM, SCIENCE, SPORTS)
                         VALUES(@uid, @name, @password, @it, @hm, @science, @sports)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@uid", uid);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@password", rjTextBox2.Texts);
                    cmd.Parameters.AddWithValue("@it", it);
                    cmd.Parameters.AddWithValue("@hm", hm);
                    cmd.Parameters.AddWithValue("@science", science);
                    cmd.Parameters.AddWithValue("@sports", sports);

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        NOTIFREGISTER nOTIFREGISTER = new NOTIFREGISTER();
                        nOTIFREGISTER.Show();
                        ResetForm();
                        rjButton2.BackColor = Color.White;
                        rjButton2.TextColor = Color.FromArgb(93, 79, 162);
                        rjButton1.BackColor = Color.Transparent;
                        rjButton1.TextColor = Color.White;
                        scan.BringToFront();
                        isRegistrationComplete = true;
                        allowScanning = true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error inserting into database: " + ex.Message);
                    }
                }
            }
        }

        private bool IsUIDRegistered(string uid)
        {
            using (var connection = CreateConnection())
            {

                string query = "SELECT COUNT(*) FROM dbo.register WHERE UID = @uid";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@uid", uid);
                    try
                    {
                        connection.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error checking UID registration: " + ex.Message);
                        return false;
                    }
                }

            }
        }


        private void ValidateRFID(string uid)
        {
            using (var connection = CreateConnection())
            {
                string query = "SELECT Name FROM register WHERE UID = @uid";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@uid", uid);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string username = reader.GetString(0); // Fetch the username from the database

                                // Pass the username to the Main form
                                NOTIFGRANTED nOTIFGRANTED = new NOTIFGRANTED();
                                Main mainForm = new Main();
                                EnableButtonsBasedOnPermissions2(mainForm,  uid);
                                mainForm.UpdateDashboardUsername(username);
                                mainForm.Show();
                             
                                nOTIFGRANTED.Show();
                                this.Hide(); // Hide the Login form
                            }
                            else
                            {
                                MessageBox.Show("UID not found. Access denied.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error validating UID: " + ex.Message);
                    }
                }
            }
        }


        private void formrfidconfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPortManager.ClosePort();
        }

      

        private void ResetForm()
        {
            textBox1.Text = String.Empty;
            rjTextBox1.Text = String.Empty;
        }







        private void rjButton1_Click(object sender, EventArgs e)
        {
            rjButton2.BackColor = Color.Transparent;
            rjButton2.TextColor = Color.White;
            rjButton1.BackColor = Color.White;
            rjButton1.TextColor = Color.FromArgb(93, 79, 162);
            create.BringToFront();
            doubleBufferedPanel4.Width = 0;
          


        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            rjButton1.BackColor = Color.Transparent;
            rjButton1.TextColor = Color.White;
            rjButton2.BackColor = Color.White;
            rjButton2.TextColor = Color.FromArgb(93, 79, 162);
            scan.BringToFront();
            
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            card.Top += 3;

            if (card.Top >= 330)
            {
                timer1.Start();
                timer2.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            card.Top -= 3;

            if (card.Top <= 303)
            {
                timer1.Stop();
                timer2.Start();
            }
        }

        private void rjButton1_MouseHover(object sender, EventArgs e)
        {

        }


        private void rjButton3_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel5.BringToFront();
            doubleBufferedPanel4.Width = 0;
            isNotifDeniedShown = true;
        }

        private void rjButton4_Click(object sender, EventArgs e)
        {
            string uid = textBox1.Text.Trim();  // Get the UID from the TextBox
            string name = rjTextBox1.Texts.Trim();  // Get the name from the TextBox

            // Check if UID is not empty and if a valid UID has been scanned
            if (string.IsNullOrEmpty(uid))
            {
                MessageBox.Show("Please scan a valid UID.");
                return;  // Exit if UID is not scanned
            }

            // Check if the name field is not empty
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name.");
                return;  // Exit if name is not entered
            }

            // If both UID and name are provided, call RegisterUID
            RegisterUID(uid, name);
          
        }

        private void label2_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel5.BringToFront();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            doubleBufferedPanel4.Width += 20;

            if (doubleBufferedPanel4.Width >= 488)
            {
                timer3.Stop();
            
            }
        }

        private void doubleBufferedPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            create.BringToFront();
        }

        private void rjButton5_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel2.BringToFront();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            scan.BringToFront();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            doubleBufferedPanel6.BringToFront();
        }

        private void create_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rjButton6_Click(object sender, EventArgs e)
        {
            string username = rjTextBox4.Texts.Trim();
            string password = rjTextBox3.Texts.Trim();

            if (ValidateUsernamePassword(username, password))
            {
                // Create and show the Main form
                NOTIFGRANTED nOTIFGRANTED = new NOTIFGRANTED();
                Main mainForm = new Main();

                EnableButtonsBasedOnPermissions(mainForm,username);

                // Update the dashboard with the username
                mainForm.UpdateDashboardUsername(username);

                // Show Main form and hide the Login form
                mainForm.Show();
                nOTIFGRANTED.Show();
                this.Hide();

                // Display a notification for successful login
              
                
            }
            else
            {
                // Show notification for invalid credentials
                NOTIFINVALID nOTIFINVALID = new NOTIFINVALID();
                nOTIFINVALID.Show();
            }
        }

        private bool ValidateUsernamePassword(string username, string password)
        {
            using (var connection = CreateConnection())
            {
                string query = "SELECT COUNT(*) FROM dbo.register WHERE Name = @name AND password = @password";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@name", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    connection.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private void EnableButtonsBasedOnPermissions(Main mainForm, string username)
        {
            using (var connection = CreateConnection())
            {
                string query = "SELECT IT, HM, SCIENCE, SPORTS FROM dbo.register WHERE Name = @name";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@name", username);

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool it = !reader.IsDBNull(0) && Convert.ToInt32(reader[0]) == 1;
                            bool hm = !reader.IsDBNull(1) && Convert.ToInt32(reader[1]) == 1;
                            bool science = !reader.IsDBNull(2) && Convert.ToInt32(reader[2]) == 1;
                            bool sports = !reader.IsDBNull(3) && Convert.ToInt32(reader[3]) == 1;

                            // Enable or disable buttons based on the user's permissions
                            mainForm.SetButtonPermissions(
                           it, hm, science, sports,
                           Properties.Resources.it1111, Properties.Resources.it111,
                           Properties.Resources._21, Properties.Resources.hm111,
                          Properties.Resources._31, Properties.Resources.sc111,
                          Properties.Resources._41, Properties.Resources.sp111);

                            mainForm.UpdateAdditionalButtons(
                        it ? Properties.Resources._1 : Properties.Resources.total2,
                        hm ? Properties.Resources._2 : Properties.Resources.total2,
                        science ? Properties.Resources._3 : Properties.Resources.total2,
                        sports ? Properties.Resources._4 : Properties.Resources.total2);

                            mainForm.UpdateLabelVisibility(it, hm, science, sports);

                        }
                    }
                }
            }
        }

        private void UpdateButtonState(Button button, bool isEnabled, Image enabledImage, Image disabledImage)
        {
            button.Enabled = isEnabled;

            // Change the button image based on the enabled/disabled state
            button.Image = isEnabled ? enabledImage : disabledImage;
        }

        private void EnableButtonsBasedOnPermissions2(Main mainForm, string uid)
        {
            using (var connection = CreateConnection())
            {
                string query = "SELECT IT, HM, SCIENCE, SPORTS FROM dbo.register WHERE UID = @uid";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@uid", uid);

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool it = !reader.IsDBNull(0) && Convert.ToInt32(reader[0]) == 1;
                            bool hm = !reader.IsDBNull(1) && Convert.ToInt32(reader[1]) == 1;
                            bool science = !reader.IsDBNull(2) && Convert.ToInt32(reader[2]) == 1;
                            bool sports = !reader.IsDBNull(3) && Convert.ToInt32(reader[3]) == 1;

                            // Enable or disable buttons based on the user's permissions
                            mainForm.SetButtonPermissions(
                                it, hm, science, sports,
                                Properties.Resources.it1111, Properties.Resources.it111,
                                Properties.Resources._21, Properties.Resources.hm111,
                                Properties.Resources._31, Properties.Resources.sc111,
                                Properties.Resources._41, Properties.Resources.sp111);

                            mainForm.UpdateAdditionalButtons(
                                it ? Properties.Resources._1 : Properties.Resources.total2,
                                hm ? Properties.Resources._2 : Properties.Resources.total2,
                                science ? Properties.Resources._3 : Properties.Resources.total2,
                                sports ? Properties.Resources._4 : Properties.Resources.total2);

                            mainForm.UpdateLabelVisibility(it, hm, science, sports);
                        }
                    }
                }
            }
        }

        private void scan_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
