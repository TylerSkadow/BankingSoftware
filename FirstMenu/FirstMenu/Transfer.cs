using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace FirstMenu
{
    public partial class Transfer : Form
    {
        private AccountForm accountForm;
        private String bankingDatabase;
        private List<Account> accountList;
        private int accountIndex;
        private OleDbConnection bankingDatabaseConnection = new OleDbConnection();

        public Transfer(AccountForm parentForm, String databaseFile, List<Account> selectedAccounts, int index)
        {
            this.accountForm = parentForm; //account form set as parent fomr
            this.bankingDatabase = databaseFile; //database file
            this.accountList = selectedAccounts; //list of connected accounts
            this.accountIndex = index; //spot where selected account is in list

            InitializeComponent();
            parentForm.Hide();
            
            //current time timer
            timer1.Enabled = true;
            timer1.Interval = 1000;

            //sets up the connect to the database
            bankingDatabaseConnection.ConnectionString = databaseFile;
        }

        // =============================================================================================================================
        //                                                    TRANSFER FORM LOAD
        // =============================================================================================================================
        private void Transfer_Load(object sender, EventArgs e)
        {
            //sets text to ex. Checking(...1234)
            lblAccount.Text = accountList[accountIndex].AccountType + "(..." + accountList[accountIndex].RoutingNum.Substring(5) + ")";
        }
        

        // =============================================================================================================================
        //                                                    EXTERNAL BUTTON CLICKED
        // =============================================================================================================================

        private void btnExternal_Click(object sender, EventArgs e)
        {
            //makes new external transfer form
            TransferExternalForm externalForm = new TransferExternalForm(bankingDatabase, accountList, accountIndex);

            if (externalForm.ShowDialog() == DialogResult.OK)
            {
                //asks if wants to transfer again
                DialogResult request = MessageBox.Show("Would you like to do another transfer?", "Transfer Request", MessageBoxButtons.YesNo);
                if (request == DialogResult.No)
                {
                    this.Close();
                }
            }
        }

        // =============================================================================================================================
        //                                                    INTERNAL BUTTON CLICKED
        // =============================================================================================================================

        private void btnInternal_Click(object sender, EventArgs e)
        {
            //makes new internal transfer form
            TransferInternalForm internalForm = new TransferInternalForm(bankingDatabase, accountList, accountIndex);
            if (internalForm.ShowDialog() == DialogResult.OK)
            {
                //asks if wants to tranfer again
                DialogResult request = MessageBox.Show("Would you like to do another transfer?", "Transfer Request", MessageBoxButtons.YesNo);
                if (request == DialogResult.No)
                {
                    this.Close();
                }
            }
        }

        // =============================================================================================================================
        //                                                    EXIT BUTTON CLICKED
        // =============================================================================================================================

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); //closes current form
        }

        // =============================================================================================================================
        //                                                    TIMER TICK
        // =============================================================================================================================

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("g"); //sets the text to current date and time
        }

        // =============================================================================================================================
        //                                                    TRANSFER FORM CLOSED
        // =============================================================================================================================

        private void Transfer_FormClosed(object sender, FormClosedEventArgs e)
        {
            accountForm.update(); //updates the account in case of changes
            accountForm.Show(); //shows account form
        }
    }
}
