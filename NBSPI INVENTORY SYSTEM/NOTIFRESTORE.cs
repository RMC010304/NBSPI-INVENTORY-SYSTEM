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
    public partial class NOTIFRESTORE : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=IT_RES;Integrated Security=True");

        private string archiveId;

        private ARCHIVE archive;
       
        public NOTIFRESTORE(ARCHIVE control,string id)
        {
            InitializeComponent();

            archiveId = id;

            archive = control;
          
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conn.ConnectionString))
            {
                con.Open();

                string insertArchiveQuery = @"
             INSERT INTO {0} ([ID], [ITEM], [BRAND], [MODEL], [CATEGORY], [QUANTITY], [STATUS],[DESCRIPTION], [DATE],[PHOTO])
            SELECT [ITEM ID], [ITEM], [BRAND], [MODEL], [CATEGORY], [QUANTITY], @Status, [DESCRIPTION], GETDATE() , [PHOTO]
            FROM ARCHIVE WHERE [ITEM ID] = @itemId";


                string targetTable = GetInventoryTable(archiveId);

                string formattedQuery = string.Format(insertArchiveQuery, targetTable);

                SqlCommand insertArchiveCmd = new SqlCommand(formattedQuery, con);

  
                insertArchiveCmd.Parameters.AddWithValue("@itemId", archiveId);
                insertArchiveCmd.Parameters.AddWithValue("@Status", "AVAILABLE");
                insertArchiveCmd.ExecuteNonQuery();           
                

                string deleteArchiveQuery = "DELETE FROM ARCHIVE WHERE [ITEM ID] = @itemId";
                SqlCommand deleteArchiveCmd = new SqlCommand(deleteArchiveQuery, con);
                deleteArchiveCmd.Parameters.AddWithValue("@itemId", archiveId);
                deleteArchiveCmd.ExecuteNonQuery();
            }

            NOTIFRESTORED nOTIFRESTORED = new NOTIFRESTORED();
            nOTIFRESTORED.Show();
            archive.GetItems();
            this.Close();
        }


        private string GetInventoryTable(string itemId)
        {
            if (itemId.StartsWith("HM")) return "ITEMS";
            else if (itemId.StartsWith("SL")) return "SCIENCE";
            else if (itemId.StartsWith("SE")) return "SPORTS";
            else return "IT";
        }

    }

       
    
}
