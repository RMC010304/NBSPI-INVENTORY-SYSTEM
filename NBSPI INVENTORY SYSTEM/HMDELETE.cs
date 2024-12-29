﻿using System;
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
    public partial class HMDELETE : Form
    {
        private string itemId;
        private string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";

        private IT it;
        public HMDELETE(IT control, string id)
        {
            InitializeComponent();

            it = control;
            itemId = id;
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

                // Transfer data to RETURN table
                string insertReturnQuery = @"
                INSERT INTO ARCHIVE ([ITEM ID], [ITEM], [BRAND], [MODEL], [CATEGORY], [QUANTITY], [STATUS],[DESCRIPTION], [PHOTO], [DATE])
            SELECT [ID], [ITEM], [BRAND], [MODEL], [CATEGORY], [QUANTITY], 'UNAVAILABLE', [DESCRIPTION], [PHOTO], GETDATE()
            FROM IT WHERE [ID] = @ID";

                SqlCommand insertReturnCmd = new SqlCommand(insertReturnQuery, con);
                insertReturnCmd.Parameters.AddWithValue("@ID", itemId);      
                insertReturnCmd.ExecuteNonQuery();

                using (SqlCommand cmd = new SqlCommand("DELETE FROM IT WHERE [ID] = @ID", con))
                {
                    cmd.Parameters.AddWithValue("@ID", itemId);             
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }


            it.GetItems();
            NOTIFDELETE nOTIFDELETE = new NOTIFDELETE();
            nOTIFDELETE.Show();
            this.Hide();
        }
    }
}
