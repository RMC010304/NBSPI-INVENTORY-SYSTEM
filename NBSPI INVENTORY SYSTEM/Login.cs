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

            using (var connection = CreateConnection())
            {
                // Insert into the MySQL database
                string query = "INSERT INTO dbo.register (UID, Name) VALUES (@uid, @name)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@uid", uid);
                    cmd.Parameters.AddWithValue("@name", name);

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
                                NOTIFGRANTED nOTIFGRANTED = new NOTIFGRANTED();
                                Main main = new Main();                            
                                main.Show();
                                nOTIFGRANTED.Show();

                                this.Hide();


                                // Perform further actions like granting access
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
            doubleBufferedPanel2.BringToFront();
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
            create.BringToFront();
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
    }
}
