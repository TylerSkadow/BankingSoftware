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
    public partial class AccountForm : Form
    {
        private HomePage homePage; //previous form
        private String bankingDataBase; //the full string to connect to database
        private Account account; //the selected account
        private List<Account> accountList; //list of connected accounts
        private int accountIndex; //spot in the accounts list where selected account is
        private OleDbConnection bankingDatabaseConnection = new OleDbConnection(); //connection to database
        public AccountForm(HomePage parentForm, String databaseFile, Account selectedAccount, List<Account> selectedAccounts)
        {
            this.homePage = parentForm; //sets previous form
            this.bankingDataBase = databaseFile; //sets connection string
            this.account = selectedAccount; //sets the account selected
            this.accountList = selectedAccounts; //sets the connected accounts

            InitializeComponent();

            //hides the HomePage form
            parentForm.Hide();

            //sets up the timer for clock
            timer.Enabled = true;
            timer.Interval = 1000;

            //sets the connection to the database string
            bankingDatabaseConnection.ConnectionString = databaseFile;
        }

        // =============================================================================================================================
        //                                                    ACCOUNT FORM LOAD
        // =============================================================================================================================

        private void AccountForm_Load(object sender, EventArgs e)
        {
            //sets the label ex. 'Checking (...1234)'
            lblAcount.Text = account.AccountType + " (..." + account.RoutingNum.Substring(5) + ")";

            accountIndex = 0; //sets to 0 to find the spot in the list

            //scans through list of accounts to find where selected account is
            for (int x = 0; x < accountList.Count; x++)
            {
                if (accountList[x].RoutingNum == account.RoutingNum)
                {
                    accountIndex = x;
                }
            }

            update(); //calls update() function
            lblBalance.Size = new System.Drawing.Size(190, 30); //sets the size of the balance label
        }

        // =============================================================================================================================
        //                                                    UPDATE FUNCTION
        // =============================================================================================================================

        public void update()
        {
            bankingDatabaseConnection.Open();
            OleDbCommand getBalance = new OleDbCommand();
            getBalance.Connection = bankingDatabaseConnection;

            //finds the account with the selected accounts routing number
            getBalance.CommandText =
                "SELECT * from Banking where [Routing Number]= " + int.Parse(account.RoutingNum) + "";

            OleDbDataReader reader = getBalance.ExecuteReader();
            int count = 0;
            double balance = 0;

            while (reader.Read())
            {
                count++; //if found

                if (count == 1)
                {
                    balance = double.Parse(reader["Balance"].ToString()); //sets the balance
                }
            }
            reader.Close();
            bankingDatabaseConnection.Close();

            //sets the label to balance found
            lblBalance.Text = string.Format("{0, 15:C}", balance);
        }

        // =============================================================================================================================
        //                                                    DEPOSIT BUTTON CLICKED
        // =============================================================================================================================

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            //makes new deposit form
            Deposit depositForm = new Deposit(bankingDataBase, account);

            //if clicked OK then update the form
            if (depositForm.ShowDialog() == DialogResult.OK)
            {
                update();
            }
        }

        // =============================================================================================================================
        //                                                    WITHDRAW BUTTON CLICKED
        // =============================================================================================================================

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            //makes new withdraw form
            Withdraw withdrawForm = new Withdraw(bankingDataBase, account);

            //if clicked OK then update the form
            if (withdrawForm.ShowDialog() == DialogResult.OK)
            {
                update();
            }
        }

        // =============================================================================================================================
        //                                                    TRANSFER BUTTON CLICKED
        // =============================================================================================================================

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            //makes new transfer form
            Transfer transferForm = new Transfer(this, bankingDataBase, accountList, accountIndex);
            transferForm.Show();
        }
        
        // =============================================================================================================================
        //                                                    BACK BUTTON CLICKED
        // =============================================================================================================================

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); //closes current form
        }

        // =============================================================================================================================
        //                                                    TIMER TICK
        // =============================================================================================================================

        private void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("g"); //sets time ex. (12/25/2000 4:50 pm)
        }

        // =============================================================================================================================
        //                                                    ACCOUNT FORM CLOSED
        // =============================================================================================================================

        private void AccountForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            homePage.Show(); //shows HomePage form when closed
        }
    }
}
