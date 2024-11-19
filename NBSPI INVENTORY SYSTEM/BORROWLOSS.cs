using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace NBSPI_INVENTORY_SYSTEM
{
    public partial class BORROWLOSS : Form
    {

        string conn = "Data Source=localhost;Initial Catalog=IT_RES;User ID=sa;Password=12345678";
        private TRANSACTION transaction;
        string _borrowId;

        private int _remainingQuantity;
        private int _returnQuantity;

        public BORROWLOSS(TRANSACTION transactionUserControl, string borrowId, int remainingQuantity, int returnQuantity)
        {
            InitializeComponent();

            transaction = transactionUserControl;

            _borrowId = borrowId;

            _remainingQuantity = remainingQuantity;

            _returnQuantity = returnQuantity;

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rjButton22_Click(object sender, EventArgs e)
        {

            

            DAMAGEFORM dAMAGEFORM = new DAMAGEFORM(transaction,transaction.BorrowerId,
                                                   transaction.BorrowerName,
                                                   transaction.DamagedItem,
                                                   transaction.Category,
                                                     _remainingQuantity,
                                                     _returnQuantity,
                                                   transaction.Reason,_borrowId);
            dAMAGEFORM.ShowDialog();


            this.Close();

        }

        private void BORROWLOSS_Load(object sender, EventArgs e)
        {

        }
    }
}
