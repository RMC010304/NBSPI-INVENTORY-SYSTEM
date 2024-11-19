namespace NBSPI_INVENTORY_SYSTEM
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.doubleBufferedPanel1 = new NBSPI_INVENTORY_SYSTEM.DoubleBufferedPanel();
            this.scan = new NBSPI_INVENTORY_SYSTEM.DoubleBufferedPanel();
            this.card = new RJCodeAdvance.RJControls.RJCircularPictureBox();
            this.rjCircularPictureBox2 = new RJCodeAdvance.RJControls.RJCircularPictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.create = new NBSPI_INVENTORY_SYSTEM.DoubleBufferedPanel();
            this.rjButton3 = new RJCodeAdvance.RJControls.RJButton();
            this.rjTextBox1 = new RJCodeAdvance.RJControls.RJTextBox();
            this.doubleBufferedPanel2 = new NBSPI_INVENTORY_SYSTEM.DoubleBufferedPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.doubleBufferedPanel3 = new NBSPI_INVENTORY_SYSTEM.DoubleBufferedPanel();
            this.doubleBufferedPanel4 = new NBSPI_INVENTORY_SYSTEM.DoubleBufferedPanel();
            this.rjButton4 = new RJCodeAdvance.RJControls.RJButton();
            this.rjButton1 = new RJCodeAdvance.RJControls.RJButton();
            this.rjButton2 = new RJCodeAdvance.RJControls.RJButton();
            this.Character = new System.Windows.Forms.PictureBox();
            this.doubleBufferedPanel1.SuspendLayout();
            this.scan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.card)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rjCircularPictureBox2)).BeginInit();
            this.create.SuspendLayout();
            this.doubleBufferedPanel2.SuspendLayout();
            this.doubleBufferedPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Character)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Interval = 10;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // doubleBufferedPanel1
            // 
            this.doubleBufferedPanel1.BackColor = System.Drawing.Color.Transparent;
            this.doubleBufferedPanel1.BackgroundImage = global::NBSPI_INVENTORY_SYSTEM.Properties.Resources.b;
            this.doubleBufferedPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.doubleBufferedPanel1.Controls.Add(this.scan);
            this.doubleBufferedPanel1.Controls.Add(this.create);
            this.doubleBufferedPanel1.Controls.Add(this.doubleBufferedPanel2);
            this.doubleBufferedPanel1.Controls.Add(this.rjButton1);
            this.doubleBufferedPanel1.Controls.Add(this.rjButton2);
            this.doubleBufferedPanel1.Controls.Add(this.Character);
            this.doubleBufferedPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doubleBufferedPanel1.Location = new System.Drawing.Point(0, 0);
            this.doubleBufferedPanel1.Name = "doubleBufferedPanel1";
            this.doubleBufferedPanel1.Size = new System.Drawing.Size(1363, 814);
            this.doubleBufferedPanel1.TabIndex = 0;
            this.doubleBufferedPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.doubleBufferedPanel1_Paint);
            // 
            // scan
            // 
            this.scan.BackColor = System.Drawing.Color.White;
            this.scan.BackgroundImage = global::NBSPI_INVENTORY_SYSTEM.Properties.Resources.bg;
            this.scan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.scan.Controls.Add(this.card);
            this.scan.Controls.Add(this.rjCircularPictureBox2);
            this.scan.Controls.Add(this.label1);
            this.scan.Location = new System.Drawing.Point(616, 0);
            this.scan.Name = "scan";
            this.scan.Size = new System.Drawing.Size(747, 814);
            this.scan.TabIndex = 0;
            // 
            // card
            // 
            this.card.BackColor = System.Drawing.Color.White;
            this.card.BorderCapStyle = System.Drawing.Drawing2D.DashCap.Flat;
            this.card.BorderColor = System.Drawing.Color.Transparent;
            this.card.BorderColor2 = System.Drawing.Color.Transparent;
            this.card.BorderLineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.card.BorderSize = 0;
            this.card.GradientAngle = 50F;
            this.card.Image = global::NBSPI_INVENTORY_SYSTEM.Properties.Resources.ca;
            this.card.Location = new System.Drawing.Point(259, 330);
            this.card.Name = "card";
            this.card.Size = new System.Drawing.Size(259, 259);
            this.card.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.card.TabIndex = 13;
            this.card.TabStop = false;
            // 
            // rjCircularPictureBox2
            // 
            this.rjCircularPictureBox2.BackColor = System.Drawing.Color.White;
            this.rjCircularPictureBox2.BorderCapStyle = System.Drawing.Drawing2D.DashCap.Flat;
            this.rjCircularPictureBox2.BorderColor = System.Drawing.Color.Transparent;
            this.rjCircularPictureBox2.BorderColor2 = System.Drawing.Color.Transparent;
            this.rjCircularPictureBox2.BorderLineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.rjCircularPictureBox2.BorderSize = 0;
            this.rjCircularPictureBox2.GradientAngle = 50F;
            this.rjCircularPictureBox2.Image = global::NBSPI_INVENTORY_SYSTEM.Properties.Resources.tech;
            this.rjCircularPictureBox2.Location = new System.Drawing.Point(143, 202);
            this.rjCircularPictureBox2.Name = "rjCircularPictureBox2";
            this.rjCircularPictureBox2.Size = new System.Drawing.Size(498, 498);
            this.rjCircularPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.rjCircularPictureBox2.TabIndex = 12;
            this.rjCircularPictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Montserrat", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(79)))), ((int)(((byte)(162)))));
            this.label1.Location = new System.Drawing.Point(212, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 48);
            this.label1.TabIndex = 11;
            this.label1.Text = "SCAN YOUR CARD";
            // 
            // create
            // 
            this.create.BackColor = System.Drawing.Color.White;
            this.create.BackgroundImage = global::NBSPI_INVENTORY_SYSTEM.Properties.Resources._16;
            this.create.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.create.Controls.Add(this.rjButton3);
            this.create.Controls.Add(this.rjTextBox1);
            this.create.Location = new System.Drawing.Point(616, 0);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(747, 814);
            this.create.TabIndex = 16;
            // 
            // rjButton3
            // 
            this.rjButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(79)))), ((int)(((byte)(162)))));
            this.rjButton3.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(79)))), ((int)(((byte)(162)))));
            this.rjButton3.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rjButton3.BorderRadius = 22;
            this.rjButton3.BorderSize = 0;
            this.rjButton3.FlatAppearance.BorderSize = 0;
            this.rjButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton3.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton3.ForeColor = System.Drawing.Color.White;
            this.rjButton3.Image = global::NBSPI_INVENTORY_SYSTEM.Properties.Resources.arrow_circle_right1;
            this.rjButton3.Location = new System.Drawing.Point(506, 437);
            this.rjButton3.Name = "rjButton3";
            this.rjButton3.Size = new System.Drawing.Size(117, 42);
            this.rjButton3.TabIndex = 13;
            this.rjButton3.Text = "Next";
            this.rjButton3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rjButton3.TextColor = System.Drawing.Color.White;
            this.rjButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.rjButton3.UseVisualStyleBackColor = false;
            this.rjButton3.Click += new System.EventHandler(this.rjButton3_Click);
            // 
            // rjTextBox1
            // 
            this.rjTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.rjTextBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.rjTextBox1.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(79)))), ((int)(((byte)(162)))));
            this.rjTextBox1.BorderRadius = 18;
            this.rjTextBox1.BorderSize = 2;
            this.rjTextBox1.Font = new System.Drawing.Font("Montserrat SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjTextBox1.ForeColor = System.Drawing.Color.DimGray;
            this.rjTextBox1.Location = new System.Drawing.Point(131, 382);
            this.rjTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.rjTextBox1.Multiline = true;
            this.rjTextBox1.Name = "rjTextBox1";
            this.rjTextBox1.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rjTextBox1.PasswordChar = false;
            this.rjTextBox1.PlaceholderColor = System.Drawing.Color.Silver;
            this.rjTextBox1.PlaceholderText = "Type your username";
            this.rjTextBox1.Size = new System.Drawing.Size(492, 39);
            this.rjTextBox1.TabIndex = 11;
            this.rjTextBox1.Texts = "";
            this.rjTextBox1.UnderlinedStyle = false;
            // 
            // doubleBufferedPanel2
            // 
            this.doubleBufferedPanel2.BackColor = System.Drawing.Color.White;
            this.doubleBufferedPanel2.BackgroundImage = global::NBSPI_INVENTORY_SYSTEM.Properties.Resources._26;
            this.doubleBufferedPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.doubleBufferedPanel2.Controls.Add(this.textBox1);
            this.doubleBufferedPanel2.Controls.Add(this.label2);
            this.doubleBufferedPanel2.Controls.Add(this.doubleBufferedPanel3);
            this.doubleBufferedPanel2.Controls.Add(this.rjButton4);
            this.doubleBufferedPanel2.Location = new System.Drawing.Point(616, 0);
            this.doubleBufferedPanel2.Name = "doubleBufferedPanel2";
            this.doubleBufferedPanel2.Size = new System.Drawing.Size(747, 814);
            this.doubleBufferedPanel2.TabIndex = 17;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(770, 202);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkGray;
            this.label2.Location = new System.Drawing.Point(429, 448);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 21);
            this.label2.TabIndex = 16;
            this.label2.Text = "Back";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // doubleBufferedPanel3
            // 
            this.doubleBufferedPanel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.doubleBufferedPanel3.Controls.Add(this.doubleBufferedPanel4);
            this.doubleBufferedPanel3.Location = new System.Drawing.Point(137, 404);
            this.doubleBufferedPanel3.Name = "doubleBufferedPanel3";
            this.doubleBufferedPanel3.Size = new System.Drawing.Size(488, 10);
            this.doubleBufferedPanel3.TabIndex = 14;
            // 
            // doubleBufferedPanel4
            // 
            this.doubleBufferedPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(79)))), ((int)(((byte)(162)))));
            this.doubleBufferedPanel4.Location = new System.Drawing.Point(-13, 0);
            this.doubleBufferedPanel4.Name = "doubleBufferedPanel4";
            this.doubleBufferedPanel4.Size = new System.Drawing.Size(10, 10);
            this.doubleBufferedPanel4.TabIndex = 15;
            // 
            // rjButton4
            // 
            this.rjButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(79)))), ((int)(((byte)(162)))));
            this.rjButton4.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(79)))), ((int)(((byte)(162)))));
            this.rjButton4.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rjButton4.BorderRadius = 22;
            this.rjButton4.BorderSize = 0;
            this.rjButton4.FlatAppearance.BorderSize = 0;
            this.rjButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton4.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton4.ForeColor = System.Drawing.Color.White;
            this.rjButton4.Location = new System.Drawing.Point(506, 437);
            this.rjButton4.Name = "rjButton4";
            this.rjButton4.Size = new System.Drawing.Size(117, 42);
            this.rjButton4.TabIndex = 13;
            this.rjButton4.Text = "Sign in";
            this.rjButton4.TextColor = System.Drawing.Color.White;
            this.rjButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.rjButton4.UseVisualStyleBackColor = false;
            this.rjButton4.Click += new System.EventHandler(this.rjButton4_Click);
            // 
            // rjButton1
            // 
            this.rjButton1.BackColor = System.Drawing.Color.Transparent;
            this.rjButton1.BackgroundColor = System.Drawing.Color.Transparent;
            this.rjButton1.BorderColor = System.Drawing.Color.Transparent;
            this.rjButton1.BorderRadius = 25;
            this.rjButton1.BorderSize = 0;
            this.rjButton1.FlatAppearance.BorderSize = 0;
            this.rjButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(218)))), ((int)(((byte)(246)))));
            this.rjButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(179)))), ((int)(((byte)(237)))));
            this.rjButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton1.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton1.ForeColor = System.Drawing.Color.White;
            this.rjButton1.Location = new System.Drawing.Point(482, 469);
            this.rjButton1.Name = "rjButton1";
            this.rjButton1.Size = new System.Drawing.Size(162, 54);
            this.rjButton1.TabIndex = 13;
            this.rjButton1.Text = "    REGISTER";
            this.rjButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rjButton1.TextColor = System.Drawing.Color.White;
            this.rjButton1.UseVisualStyleBackColor = false;
            this.rjButton1.Click += new System.EventHandler(this.rjButton1_Click);
            this.rjButton1.MouseHover += new System.EventHandler(this.rjButton1_MouseHover);
            // 
            // rjButton2
            // 
            this.rjButton2.BackColor = System.Drawing.Color.White;
            this.rjButton2.BackgroundColor = System.Drawing.Color.White;
            this.rjButton2.BorderColor = System.Drawing.Color.Transparent;
            this.rjButton2.BorderRadius = 25;
            this.rjButton2.BorderSize = 0;
            this.rjButton2.FlatAppearance.BorderSize = 0;
            this.rjButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(218)))), ((int)(((byte)(246)))));
            this.rjButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(179)))), ((int)(((byte)(237)))));
            this.rjButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton2.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(79)))), ((int)(((byte)(162)))));
            this.rjButton2.Location = new System.Drawing.Point(482, 415);
            this.rjButton2.Name = "rjButton2";
            this.rjButton2.Size = new System.Drawing.Size(162, 54);
            this.rjButton2.TabIndex = 15;
            this.rjButton2.Text = "    SCAN";
            this.rjButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rjButton2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(79)))), ((int)(((byte)(162)))));
            this.rjButton2.UseVisualStyleBackColor = false;
            this.rjButton2.Click += new System.EventHandler(this.rjButton2_Click);
            // 
            // Character
            // 
            this.Character.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Character.Image = global::NBSPI_INVENTORY_SYSTEM.Properties.Resources.c;
            this.Character.Location = new System.Drawing.Point(-169, 294);
            this.Character.Name = "Character";
            this.Character.Size = new System.Drawing.Size(728, 621);
            this.Character.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Character.TabIndex = 14;
            this.Character.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1363, 814);
            this.Controls.Add(this.doubleBufferedPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.doubleBufferedPanel1.ResumeLayout(false);
            this.scan.ResumeLayout(false);
            this.scan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.card)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rjCircularPictureBox2)).EndInit();
            this.create.ResumeLayout(false);
            this.doubleBufferedPanel2.ResumeLayout(false);
            this.doubleBufferedPanel2.PerformLayout();
            this.doubleBufferedPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Character)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBufferedPanel doubleBufferedPanel1;
        private DoubleBufferedPanel scan;
        private System.Windows.Forms.PictureBox Character;
        private RJCodeAdvance.RJControls.RJButton rjButton1;
        private RJCodeAdvance.RJControls.RJButton rjButton2;
        private DoubleBufferedPanel create;
        private RJCodeAdvance.RJControls.RJButton rjButton3;
        private RJCodeAdvance.RJControls.RJTextBox rjTextBox1;
        private RJCodeAdvance.RJControls.RJCircularPictureBox card;
        private RJCodeAdvance.RJControls.RJCircularPictureBox rjCircularPictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private DoubleBufferedPanel doubleBufferedPanel2;
        private RJCodeAdvance.RJControls.RJButton rjButton4;
        private System.Windows.Forms.Label label2;
        private DoubleBufferedPanel doubleBufferedPanel4;
        private DoubleBufferedPanel doubleBufferedPanel3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer3;
    }
}