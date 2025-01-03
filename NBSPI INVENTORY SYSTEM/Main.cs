using NBSPI_INVENTORY_SYSTEM.Properties;
using Org.BouncyCastle.Crmf;
using RJCodeAdvance.RJControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using static Google.Protobuf.Reflection.FieldDescriptorProto.Types;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class Main : Form
    {

        public DASHBOARD dashboard;

        public Main()
        {
            InitializeComponent();
            dashboard = new DASHBOARD();
        }

        private void Main_Load(object sender, EventArgs e)
        {
         
        }

        public void UpdateDashboardUsername(string username)
        {
            dashboard4.SetUsername(username); // Call the method on the Dashboard user control
        }

        private void dashbutton_Click(object sender, EventArgs e)
        {
            dashbutton.BackColor = Color.White;
            dashbutton.ForeColor = Color.FromArgb(93, 79, 162);
            itbutton.BackColor = Color.Transparent;
            itbutton.ForeColor = Color.White;
            trbutton.BackColor = Color.Transparent;
            trbutton.ForeColor = Color.White;
      
            archbutton.BackColor = Color.Transparent;
            archbutton.ForeColor = Color.White;
            abbutton.BackColor = Color.Transparent;
            abbutton.ForeColor = Color.White;


            dashbutton.Image = Resources.db;
            itbutton.Image = Resources.it2;
            trbutton.Image = Resources.tr2;
   
            archbutton.Image = Resources.ar2;
            abbutton.Image = Resources.ab2;

            dashboard4.BringToFront();

            dashboard.RefreshDashboard();

        }

        private void itbutton_Click(object sender, EventArgs e)
        {
            itbutton.BackColor = Color.White;
            itbutton.ForeColor = Color.FromArgb(93, 79, 162);
            dashbutton.BackColor = Color.Transparent;
            dashbutton.ForeColor = Color.White;
            trbutton.BackColor = Color.Transparent;
            trbutton.ForeColor = Color.White;
           
            archbutton.BackColor = Color.Transparent;
            archbutton.ForeColor = Color.White;
            abbutton.BackColor = Color.Transparent;
            abbutton.ForeColor = Color.White;

            dashbutton.Image = Resources.db2;
            itbutton.Image = Resources.it;
            trbutton.Image = Resources.tr2;
       
            archbutton.Image = Resources.ar2;
            abbutton.Image = Resources.ab2;

            ipanel.BringToFront();




        }

        private void archbutton_Click(object sender, EventArgs e)
        {
            archbutton.BackColor = Color.White;
            archbutton.ForeColor = Color.FromArgb(93, 79, 162);
            itbutton.BackColor = Color.Transparent;
            itbutton.ForeColor = Color.White;
            dashbutton.BackColor = Color.Transparent;
            dashbutton.ForeColor = Color.White;
            abbutton.BackColor = Color.Transparent;
            abbutton.ForeColor = Color.White;
            trbutton.BackColor = Color.Transparent;
            trbutton.ForeColor = Color.White;
         

            dashbutton.Image = Resources.db2;
            itbutton.Image = Resources.it2;
            archbutton.Image = Resources.ar;
            trbutton.Image = Resources.tr2;
     
            abbutton.Image = Resources.ab2;

            archive2.BringToFront();





        }

        private void abbutton_Click(object sender, EventArgs e)
        {
            abbutton.BackColor = Color.White;
            abbutton.ForeColor = Color.FromArgb(93, 79, 162);
            trbutton.BackColor = Color.Transparent;
            trbutton.ForeColor = Color.White;
         
            archbutton.BackColor = Color.Transparent;
            archbutton.ForeColor = Color.White;
            itbutton.BackColor = Color.Transparent;
            itbutton.ForeColor = Color.White;
            dashbutton.BackColor = Color.Transparent;
            dashbutton.ForeColor = Color.White;

            dashbutton.Image = Resources.db2;
            itbutton.Image = Resources.it2;
            archbutton.Image = Resources.ar2;
            trbutton.Image = Resources.tr2;
    
            abbutton.Image = Resources.ab;

          
            abpanel.BringToFront();
        }


        private void rjButton4_MouseCaptureChanged(object sender, EventArgs e)
        {
          
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            hm2.BringToFront();
            hm2.Show();
          

        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            science2.BringToFront();
            science2.Show();
        
        }

        private void rjButton3_Click(object sender, EventArgs e)
        {
            sports2.BringToFront();
            sports2.Show();
        
        }

        private void rjButton1_MouseHover(object sender, EventArgs e)
        {
            rjButton1.Size = new Size(1183, 191);
            rjButton1.Location = new Point(71, 426);
        }

        private void rjButton1_MouseLeave(object sender, EventArgs e)
        {
            rjButton1.Size = new Size(1129, 166);
            rjButton1.Location = new Point(115, 439);
        }

        private void rjButton2_MouseHover(object sender, EventArgs e)
        {
            rjButton2.Size = new Size(1183, 191);
            rjButton2.Location = new Point(71, 610);
        }

        private void rjButton2_MouseLeave_1(object sender, EventArgs e)
        {
            rjButton2.Size = new Size(1129, 166);
            rjButton2.Location = new Point(115, 621);
        }

        private void rjButton3_MouseHover(object sender, EventArgs e)
        {
            rjButton3.Size = new Size(1183, 191);
             rjButton3.Location = new Point(71, 791);
        }

        private void rjButton3_MouseLeave(object sender, EventArgs e)
        {
            rjButton3.Size = new Size(1129, 166);
            rjButton3.Location = new Point(115, 805);
        }

        private void rjButton5_MouseHover(object sender, EventArgs e)
        {
            rjButton5.Size = new Size(1183, 191);
            rjButton5.Location = new Point(71, 244);
        }

        private void rjButton5_MouseLeave(object sender, EventArgs e)
        {
            rjButton5.Size = new Size(1129, 166);
            rjButton5.Location = new Point(115, 257);
        }

      

        private void rjButton5_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void trbutton_Click(object sender, EventArgs e)
        {
            trbutton.BackColor = Color.White;
            trbutton.ForeColor = Color.FromArgb(93, 79, 162);
            abbutton.BackColor = Color.Transparent;
            abbutton.ForeColor = Color.White;      
        
            archbutton.BackColor = Color.Transparent;
            archbutton.ForeColor = Color.White;
            itbutton.BackColor = Color.Transparent;
            itbutton.ForeColor = Color.White;
            dashbutton.BackColor = Color.Transparent;
            dashbutton.ForeColor = Color.White;

            dashbutton.Image = Resources.db2;
            itbutton.Image = Resources.it2;
            archbutton.Image = Resources.ar2;
            trbutton.Image = Resources.tr;
      
            abbutton.Image = Resources.ab2;

            transaction2.BringToFront();

           
        }

        private void rbutton_Click(object sender, EventArgs e)
        {
     

        }

        private void dashbutton_MouseHover(object sender, EventArgs e)
        {
            dashbutton.Size = new Size(284, 73);
            dashbutton.Location = new Point(43, 275);
        }

        private void dashbutton_MouseLeave(object sender, EventArgs e)
        {
            dashbutton.Size = new Size(238, 67);
            dashbutton.Location = new Point(67, 282);
        }

        private void itbutton_MouseHover(object sender, EventArgs e)
        {
            itbutton.Size = new Size(284, 73);
            itbutton.Location = new Point(43, 350);
        }

        private void itbutton_MouseLeave(object sender, EventArgs e)
        {
            itbutton.Size = new Size(238, 67);
            itbutton.Location = new Point(67, 357);
        }

        private void trbutton_MouseHover(object sender, EventArgs e)
        {
            trbutton.Size = new Size(284, 73);
            trbutton.Location = new Point(43, 426);
        }

        private void trbutton_MouseLeave(object sender, EventArgs e)
        {
            trbutton.Size = new Size(238, 67);
            trbutton.Location = new Point(67, 432);
        }

        private void rbutton_MouseHover(object sender, EventArgs e)
        {
          
        }

        private void rbutton_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void archbutton_MouseHover(object sender, EventArgs e)
        {
            archbutton.Size = new Size(284, 73);
            archbutton.Location = new Point(43, 501);
        }

        private void archbutton_MouseLeave(object sender, EventArgs e)
        {
            archbutton.Size = new Size(238, 67);
            archbutton.Location = new Point(67, 509);
        }

        private void abbutton_MouseHover(object sender, EventArgs e)
        {
            abbutton.Size = new Size(284, 73);
            abbutton.Location = new Point(43, 845);
        }

        private void abbutton_MouseLeave(object sender, EventArgs e)
        {
            abbutton.Size = new Size(238, 67);
            abbutton.Location = new Point(67, 851);
        }

        private void rjButton5_Click(object sender, EventArgs e)
        {
            it2.BringToFront();
            it2.Show();
        }

        private void ipanel_Paint(object sender, PaintEventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT SUM (QUANTITY) FROM dbo.IT", conn);
            SqlCommand cmd2 = new SqlCommand("SELECT SUM (QUANTITY) FROM dbo.ITEMS", conn);
            SqlCommand cmd3 = new SqlCommand("SELECT SUM (QUANTITY)  FROM dbo.SCIENCE", conn);
            SqlCommand cmd4 = new SqlCommand("SELECT SUM (QUANTITY) FROM dbo.SPORTS", conn);

            var count1 = cmd.ExecuteScalar();
            var count2 = cmd2.ExecuteScalar();
            var count3 = cmd3.ExecuteScalar();
            var count4 = cmd4.ExecuteScalar();

            label1.Text = count1.ToString();
            label2.Text = count2.ToString();
            label3.Text = count3.ToString();
            label4.Text = count4.ToString();


            conn.Close();
        }

        public void SetButtonPermissions(
     bool it, bool hm, bool science, bool sports,
     Image itEnabledImage, Image itDisabledImage,
     Image hmEnabledImage, Image hmDisabledImage,
     Image scienceEnabledImage, Image scienceDisabledImage,
     Image sportsEnabledImage, Image sportsDisabledImage)
        {
            // IT button
            rjButton5.Enabled = it;
            rjButton5.BackgroundImage = it ? itEnabledImage : itDisabledImage;
            rjButton5.BackgroundImageLayout = ImageLayout.Stretch;
         

            // HM button
            rjButton1.Enabled = hm;
            rjButton1.BackgroundImage = hm ? hmEnabledImage : hmDisabledImage;
            rjButton1.BackgroundImageLayout = ImageLayout.Stretch;
          

            // Science button
            rjButton2.Enabled = science;
            rjButton2.BackgroundImage = science ? scienceEnabledImage : scienceDisabledImage;
            rjButton2.BackgroundImageLayout = ImageLayout.Stretch;
     
            // Sports button
            rjButton3.Enabled = sports;
            rjButton3.BackgroundImage = sports ? sportsEnabledImage : sportsDisabledImage;
            rjButton3.BackgroundImageLayout = ImageLayout.Stretch;
  

            // Refresh UI
            rjButton5.Refresh();
            rjButton1.Refresh();
            rjButton2.Refresh();
            rjButton3.Refresh();
        }
        public void UpdateAdditionalButtons(Image itImage, Image hmImage, Image scienceImage, Image sportsImage)
        {
            // Update images for the additional buttons
            rjButton4.BackgroundImage = itImage;
            rjButton4.BackgroundImageLayout = ImageLayout.Stretch;

            rjButton6.BackgroundImage = hmImage;
            rjButton6.BackgroundImageLayout = ImageLayout.Stretch;

            rjButton7.BackgroundImage = scienceImage;
            rjButton7.BackgroundImageLayout = ImageLayout.Stretch;

            rjButton8.BackgroundImage = sportsImage;
            rjButton8.BackgroundImageLayout = ImageLayout.Stretch;

            // Refresh the UI
            rjButton4.Refresh();
            rjButton6.Refresh();
            rjButton7.Refresh();
            rjButton8.Refresh();
        }

        public void UpdateLabelVisibility(bool it, bool hm, bool science, bool sports)
        {
            // Update label visibility based on permissions
            label1.Visible = it;
            label2.Visible = hm;
            label3.Visible = science;
            label4.Visible = sports;
        }

        private void Main_Load_1(object sender, EventArgs e)
        {
         
        }


        private void rjButton15_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void rjButton16_Click(object sender, EventArgs e)
        {
            LOGOUT logoutForm = new LOGOUT();
            logoutForm.ShowDialog();

            if (logoutForm.IsConfirmed)
            {
                this.Close(); // Close the MainForm
                Login loginForm = new Login();
                loginForm.Show(); // Show the LoginForm
            }
            // Optionally, disable the Main form while LOGOUT form is open

        }


    }
}
