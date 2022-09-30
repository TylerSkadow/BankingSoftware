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
    public partial class TransferInternalForm : Form
    {
        private String bankingDatabase; //the full string to connect to database
        private List<Account> accountList; //list of accounts
        private int accountIndex; //index of the selectd account in the accounts list
        private String amount; //the amount to be transfered
        private OleDbConnection bankingDatabaseConnection = new OleDbConnection(); //connection to database

        public TransferInternalForm(String databaseFile, List<Account> selectedAccounts, int index)
        {
            this.bankingDatabase = databaseFile; //sets connection string
            this.accountList = selectedAccounts; //sets the selected accounts
            this.accountIndex = index; //index of selected account in the list

            InitializeComponent();

            //sets the connection to the database string
            bankingDatabaseConnection.ConnectionString = databaseFile;
        }

        // =============================================================================================================================
        //                                                    FORM LOAD
        // =============================================================================================================================

        private void TransferInternalForm_Load(object sender, EventArgs e)
        {
            foreach (Account account in accountList) //a loop through all accounts
            {
                if (account.RoutingNum != accountList[accountIndex].RoutingNum) //if not the selected account
                {
                    comboBox1.Items.Add(account.AccountType + " (..." + account.RoutingNum.Substring(5) + ")");
                }
            }
        }

        // =============================================================================================================================
        //                                                    TEXTBOX 1 VALIDATING
        // =============================================================================================================================

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(textBox1, "");

            if (textBox1.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(textBox1, "Please enter an amount");
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
                        errorProvider.SetError(textBox1, "Not enough in account");
                        return;
                    }
                }
            }
            readBal.Close();
            bankingDatabaseConnection.Close();

            if (count == 0) //if routing number is not found
            {
                e.Cancel = true;
                errorProvider.SetError(comboBox1, "Account not found");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    TEXTBOX 1 TEXT CHANGED
        // =============================================================================================================================

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            string value = textBox1.Text.Replace(",", "").Replace("$", "").Replace(".", "").TrimStart('0'); //removes all but numbers
            decimal entered;

            //converts the string to decimal
            if (decimal.TryParse(value, out entered))
            {
                entered /= 100; //divides the number by 100
                textBox1.TextChanged -= textBox1_TextChanged; //gets rid of text changed event
                textBox1.Text = string.Format("{0:C2}", entered); //formats the text
                textBox1.TextChanged += textBox1_TextChanged; //adds the text changed event back
                textBox1.Select(textBox1.Text.Length, 0); //sets cursor back to spot 0 aka the back
            }

            //bool to see if text is valid
            bool correct = isValid(textBox1.Text);

            //if false
            if (!correct)
            {
                //if amount is not empty
                if (amount != "")
                {
                    textBox1.Text = amount; //sets the textbox to the last valid amount
                    textBox1.Select(textBox1.Text.Length, 0); //sets back to spot 0 aka the back
                }
                else
                {
                    textBox1.Text = "$0.00"; //sets to 0 dollars if theres no amount yet
                    textBox1.Select(textBox1.Text.Length, 0); //sets back to spot 0 aka the back
                }
            }
            //if true
            else
            {
                //sets amount to the current textbox text
                amount = textBox1.Text;
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
        //                                                    COMBOBOX 1 VALIDATING
        // =============================================================================================================================

        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(comboBox1, "");

            if (comboBox1.Text == "") //if none selected
            {
                e.Cancel = true;
                errorProvider.SetError(comboBox1, "Please select an account");
                return;
            }
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
                double amountTranfer = double.Parse(replace); //converts to double
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

                balance = balance - amountTranfer;
                int tranferIndex = 0;

                //find where transfer to account is at in list
                if (comboBox1.SelectedIndex >= accountIndex) 
                {
                    tranferIndex = comboBox1.SelectedIndex + 1;
                }
                else
                {
                    tranferIndex = comboBox1.SelectedIndex;
                }

                //date, from, transaction, amount, balance;
                transaction +=
                    DateTime.Now.ToString("f") +
                    "|Transfer" +
                    "|Internal Transfer to " +
                    accountList[tranferIndex].AccountType + " (..." + accountList[tranferIndex].RoutingNum.Substring(5) + ")|" +
                    "-" + amount + "|" +
                    balance.ToString("C") + ";";

                transferFrom.CommandText =
                    "UPDATE Banking set [Balance]='" + balance.ToString() + "', [Transactions]='" + transaction +
                    "' where [Routing Number]= " + int.Parse(accountList[accountIndex].RoutingNum) + "";
                transferFrom.ExecuteNonQuery(); //sets the user account balance lower

                balance = 0;
                count = 0;
                
                OleDbCommand tranferTo = new OleDbCommand();
                tranferTo.Connection = bankingDatabaseConnection;
                tranferTo.CommandText =
                    "SELECT * from Banking where [Routing Number]=" + int.Parse(accountList[tranferIndex].RoutingNum) + "";
                reader = tranferTo.ExecuteReader();

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

                balance = balance + amountTranfer;

                //date, from, transaction, amount, balance;
                transaction +=
                    DateTime.Now.ToString("f") +
                    "|Transfer" +
                    "|Internal Transfer from " +
                    accountList[accountIndex].AccountType + " (..." + accountList[accountIndex].RoutingNum.Substring(5) + ")|" +
                    amount + "|" +
                    balance.ToString("C") + ";";

                tranferTo.CommandText =
                    "UPDATE Banking set [Balance]='" + balance.ToString() + "', [Transactions]='" + transaction +
                    "' where [Routing Number]= " + int.Parse(accountList[tranferIndex].RoutingNum) + "";
                tranferTo.ExecuteNonQuery(); //sets the transfer to account balance higher

                bankingDatabaseConnection.Close();
                MessageBox.Show(amount + " was transfered from " + accountList[accountIndex].AccountType + "(..." + 
                    accountList[accountIndex].RoutingNum.Substring(5) + ") to " + accountList[tranferIndex].AccountType + 
                    "(..." + accountList[tranferIndex].RoutingNum.Substring(5) + ")", "Transfer Success");
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
