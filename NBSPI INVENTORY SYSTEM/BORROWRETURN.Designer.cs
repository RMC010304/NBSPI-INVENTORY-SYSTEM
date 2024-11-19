namespace NBSPI_INVENTORY_SYSTEM
{
    partial class BORROWRETURN
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
            this.rjButton1 = new RJCodeAdvance.RJControls.RJButton();
            this.rjButton22 = new RJCodeAdvance.RJControls.RJButton();
            this.rjTextBox1 = new RJCodeAdvance.RJControls.RJTextBox();
            this.SuspendLayout();
            // 
            // rjButton1
            // 
            this.rjButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rjButton1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rjButton1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.rjButton1.BorderColor = System.Drawing.Color.Transparent;
            this.rjButton1.BorderRadius = 20;
            this.rjButton1.BorderSize = 0;
            this.rjButton1.FlatAppearance.BorderSize = 0;
            this.rjButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton1.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.rjButton1.Location = new System.Drawing.Point(55, 207);
            this.rjButton1.Name = "rjButton1";
            this.rjButton1.Size = new System.Drawing.Size(146, 40);
            this.rjButton1.TabIndex = 98;
            this.rjButton1.Text = "Cancel";
            this.rjButton1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.rjButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rjButton1.UseVisualStyleBackColor = false;
            this.rjButton1.Click += new System.EventHandler(this.rjButton1_Click);
            // 
            // rjButton22
            // 
            this.rjButton22.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rjButton22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(79)))), ((int)(((byte)(162)))));
            this.rjButton22.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(79)))), ((int)(((byte)(162)))));
            this.rjButton22.BorderColor = System.Drawing.Color.Transparent;
            this.rjButton22.BorderRadius = 20;
            this.rjButton22.BorderSize = 0;
            this.rjButton22.FlatAppearance.BorderSize = 0;
            this.rjButton22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton22.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton22.ForeColor = System.Drawing.Color.White;
            this.rjButton22.Location = new System.Drawing.Point(216, 207);
            this.rjButton22.Name = "rjButton22";
            this.rjButton22.Size = new System.Drawing.Size(146, 40);
            this.rjButton22.TabIndex = 97;
            this.rjButton22.Text = "Return";
            this.rjButton22.TextColor = System.Drawing.Color.White;
            this.rjButton22.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rjButton22.UseVisualStyleBackColor = false;
            this.rjButton22.Click += new System.EventHandler(this.rjButton22_Click);
            // 
            // rjTextBox1
            // 
            this.rjTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rjTextBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.rjTextBox1.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.rjTextBox1.BorderRadius = 15;
            this.rjTextBox1.BorderSize = 1;
            this.rjTextBox1.Font = new System.Drawing.Font("Montserrat SemiBold", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjTextBox1.ForeColor = System.Drawing.Color.DimGray;
            this.rjTextBox1.Location = new System.Drawing.Point(55, 140);
            this.rjTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.rjTextBox1.Multiline = false;
            this.rjTextBox1.Name = "rjTextBox1";
            this.rjTextBox1.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rjTextBox1.PasswordChar = false;
            this.rjTextBox1.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rjTextBox1.PlaceholderText = "";
            this.rjTextBox1.Size = new System.Drawing.Size(307, 31);
            this.rjTextBox1.TabIndex = 96;
            this.rjTextBox1.Texts = "";
            this.rjTextBox1.UnderlinedStyle = false;
            // 
            // BORROWRETURN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::NBSPI_INVENTORY_SYSTEM.Properties.Resources.retform;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(417, 274);
            this.Controls.Add(this.rjButton1);
            this.Controls.Add(this.rjButton22);
            this.Controls.Add(this.rjTextBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BORROWRETURN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BORROWRETURN";
            this.Load += new System.EventHandler(this.BORROWRETURN_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RJCodeAdvance.RJControls.RJButton rjButton1;
        private RJCodeAdvance.RJControls.RJButton rjButton22;
        private RJCodeAdvance.RJControls.RJTextBox rjTextBox1;
    }
}