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
using System.Text.RegularExpressions;

namespace FirstMenu
{
    public partial class TransferExternalForm : Form
    {
        private String bankingDatabase; //the full string to connect to database
        private List<Account> accountList; //list of accounts
        private int accountIndex; //index of the selected account in the accounts list
        private int transferRoutingNum; //the routing number that will be transfered to
        private String amount; //the amount to be transfered
        private OleDbConnection bankingDatabaseConnection = new OleDbConnection(); //connection to database

        public TransferExternalForm(String databaseFile, List<Account> selectedAccounts, int index)
        {
            this.bankingDatabase = databaseFile; //sets connection string
            this.accountList = selectedAccounts; //sets the selected accounts
            this.accountIndex = index; //index of selected account in the list

            InitializeComponent();
            
            //sets the connection to the database string
            bankingDatabaseConnection.ConnectionString = databaseFile;
        }

        // =============================================================================================================================
        //                                                    ROUTING NUMBER VALIDATING
        // =============================================================================================================================

        private void txtRoutingNumber_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtRoutingNumber, "");

            if (txtRoutingNumber.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtRoutingNumber, "Please enter a routing number");
                return;
            }
            else if (txtRoutingNumber.Text.Length != 9) //if the rotuing number is the correct length
            {
                e.Cancel = true;
                errorProvider.SetError(txtRoutingNumber, "Routing Number incorrect length");
                return;
            }

            //checks if routing number is only digits
            for (int x = 0; x < txtRoutingNumber.Text.Length; x++)
            {
                if (Char.IsDigit(txtRoutingNumber.Text, x) == false)
                {
                    e.Cancel = true;
                    errorProvider.SetError(txtRoutingNumber, "Only digits allowed");
                    return;
                }
            }

            bankingDatabaseConnection.Open();
            OleDbCommand findRouting = new OleDbCommand();
            findRouting.Connection = bankingDatabaseConnection;
            findRouting.CommandText =
                "SELECT * from Banking where [Routing Number]=" + int.Parse(txtRoutingNumber.Text) + "";
            OleDbDataReader readRouting = findRouting.ExecuteReader();

            int count = 0;

            while (readRouting.Read())
            {
                count++; //if found

                if (count == 1)
                {
                    //sets the found routing number to the transfer routing
                    transferRoutingNum = int.Parse(readRouting["Routing Number"].ToString());
                }
            }
            readRouting.Close();
            bankingDatabaseConnection.Close();

            if (count == 0) //if routing number not found
            {
                e.Cancel = true;
                errorProvider.SetError(txtRoutingNumber, "Cannot find routing number");
                return;
            }
            
        }

        // =============================================================================================================================
        //                                                    AMOUNT VALIDATING
        // =============================================================================================================================

        private void txtAmount_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtAmount, "");

            if (txtAmount.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtAmount, "Please enter an amount");
                return;
            }

            bankingDatabaseConnection.Open();
            OleDbCommand checkBalance = new OleDbCommand();
            checkBalance.Connection = bankingDatabaseConnection;
            checkBalance.CommandText =
                "SELECT * from Banking where [Routing Number]=" + int.Parse(accountList[accountIndex].RoutingNum) + "";
            OleDbDataReader readBal = checkBalance.ExecuteReader();

            int count = 0;

            while (readBal.Read())
            {
                count++; //if found

                if (count == 1)
                {
                    double balance = double.Parse(readBal["Balance"].ToString()); //gets the balance from account
                    string replace = amount.Replace(",", "").Replace("$", "").TrimStart('0'); //removes all but numbers
                    double transfer = double.Parse(replace); //sets string to double

                    if (transfer > balance) //if transfer amount is larger than current balance
                    {
                        bankingDatabaseConnection.Close();
                        e.Cancel = true;
                        errorProvider.SetError(txtAmount, "Not enough in account");
                        return;
                    }
                }
            }
            readBal.Close();
            bankingDatabaseConnection.Close();

            if (count == 0) //if routing number is not found
            {
                e.Cancel = true;
                errorProvider.SetError(txtRoutingNumber, "Cannot find routing number");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    AMOUNT TEXT CHANGED
        // =============================================================================================================================

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            string value = txtAmount.Text.Replace(",", "").Replace("$", "").Replace(".", "").TrimStart('0'); //removes all but numbers
            decimal entered;

            //converts the string to decimal
            if (decimal.TryParse(value, out entered))
            {
                entered /= 100; //divides the number by 100
                txtAmount.TextChanged -= txtAmount_TextChanged; //gets rid of text changed event
                txtAmount.Text = string.Format("{0:C2}", entered); //formats the text
                txtAmount.TextChanged += txtAmount_TextChanged; //adds the text changed event back
                txtAmount.Select(txtAmount.Text.Length, 0); //sets cursor back to spot 0 aka the back
            }

            //bool to see if text is valid
            bool correct = isValid(txtAmount.Text);

            //if false
            if (!correct)
            {
                //if amount is not empty
                if (amount != "")
                {
                    txtAmount.Text = amount; //sets the textbox to the last valid amount
                    txtAmount.Select(txtAmount.Text.Length, 0); //sets back to spot 0 aka the back
                }
                else
                {
                    txtAmount.Text = "$0.00"; //sets to 0 dollars if theres no amount yet
                    txtAmount.Select(txtAmount.Text.Length, 0); //sets back to spot 0 aka the back
                }
            }
            //if true
            else
            {
                //sets amount to the current textbox text
                amount = txtAmount.Text;
            }
        }

        //function to see if textbox is valid
        private bool isValid(string text)
        {
            //honestly i dont know what this is
            //I think it allows for only doubles in each spot ex. $9,999.99
            Regex money = new Regex(@"^\$(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$");
            return money.IsMatch(text);
        }

        // =============================================================================================================================
        //                                                    OK BUTTON CLICKED
        // =============================================================================================================================

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                bankingDatabaseConnection.Open();
                OleDbCommand transferFrom = new OleDbCommand();
                transferFrom.Connection = bankingDatabaseConnection;
                transferFrom.CommandText =
                    "SELECT * from Banking where [Routing Number]=" + int.Parse(accountList[accountIndex].RoutingNum) + "";
                OleDbDataReader reader = transferFrom.ExecuteReader();

                int count = 0;
                double balance = 0;
                string replace = amount.Replace(",", "").Replace("$", "").TrimStart('0'); //removes currency formatting
                double amountTransfer = double.Parse(replace); //converts to double
                string transaction = "";

                while (reader.Read())
                {
                    count++; //if found

                    if (count == 1)
                    {
                        balance = double.Parse(reader["Balance"].ToString()); //sets balance
                        transaction = reader["Transactions"].ToString(); //sets transactions list
                    }
                }
                reader.Close();

                balance = balance - amountTransfer;

                //date, from, transaction, amount, balance;
                transaction +=
                    DateTime.Now.ToString("f") +
                    "|Transfer" +
                    "|External Transfer to Routing Number: " + transferRoutingNum + "|" +
                    "-" + amount + "|" +
                    balance.ToString("C") + ";";

                transferFrom.CommandText =
                    "UPDATE Banking set [Balance]='" + balance.ToString() +
                    "', [Transactions]='" + transaction + 
                    "' where [Routing Number]=" + int.Parse(accountList[accountIndex].RoutingNum) + "";
                transferFrom.ExecuteNonQuery(); //sets the user account balance lower

                balance = 0;
                count = 0;

                OleDbCommand transferTo = new OleDbCommand();
                transferTo.Connection = bankingDatabaseConnection;
                transferTo.CommandText =
                    "SELECT * from Banking where [Routing Number]=" + transferRoutingNum + "";
                reader = transferTo.ExecuteReader();

                while (reader.Read())
                {
                    count++; //if found

                    if (count == 1)
                    {
                        balance = double.Parse(reader["Balance"].ToString()); //sets balance
                        transaction = reader["Transactions"].ToString(); //sets transactions list
                    }
                }
                reader.Close();

                balance = balance + amountTransfer;

                transaction +=
                    DateTime.Now.ToString("f") +
                    "|Transfer" +
                    "|External Transfer from " + accountList[accountIndex].RoutingNum + "|" +
                    amount + "|" +
                    balance.ToString("C") + ";";

                transferTo.CommandText =
                    "UPDATE Banking set [Balance]='" + balance.ToString() + "', [Transactions]='" + transaction +
                    "' where [Routing Number]=" + transferRoutingNum + "";
                transferTo.ExecuteNonQuery(); //sets the transfer to account balance higher

                bankingDatabaseConnection.Close();
                MessageBox.Show(amount + " was transfered from " + accountList[accountIndex].AccountType + "(..." + 
                    accountList[accountIndex].RoutingNum.Substring(5) + ") to " + transferRoutingNum, "Transfer Success");
                this.DialogResult = DialogResult.OK;
            }
        }

        // =============================================================================================================================
        //                                                    CANCEL BUTTON CLICKED
        // =============================================================================================================================

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); //closes current form
        }
    }
}
