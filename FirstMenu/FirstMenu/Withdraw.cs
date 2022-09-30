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
    public partial class Withdraw : Form
    {
        private String bankingDataBase; //the full string to connect to database
        private Account account; //the selected account
        private String amount = ""; //amount to transfer
        private OleDbConnection bankingDatabaseConnection = new OleDbConnection(); //connection to database
        
        public Withdraw(String databaseFile, Account selectedAccount)
        {
            this.bankingDataBase = databaseFile; //sets connectiopn string
            this.account = selectedAccount; //sets the selected account

            InitializeComponent();

            //sets the connection to the database string
            bankingDatabaseConnection.ConnectionString = databaseFile;
        }

        // =============================================================================================================================
        //                                                    TEXT BOX 1 CHANGED
        // =============================================================================================================================
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //gets rid of the currency portions of the string
            string value = textBox1.Text.Replace(",", "").Replace("$", "").Replace(".", "").TrimStart('0');
            decimal entered; //only allows for 2 places

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
                //if amount is empty
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
        }

        // =============================================================================================================================
        //                                                    OK BUTTON CLICKED
        // =============================================================================================================================

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                bankingDatabaseConnection.Open();
                OleDbCommand withdraw = new OleDbCommand();
                withdraw.Connection = bankingDatabaseConnection;
                withdraw.CommandText =
                    "SELECT * from Banking where [Routing Number]=" + int.Parse(account.RoutingNum) + "";
                OleDbDataReader reader = withdraw.ExecuteReader();

                int count = 0;
                double balance = 0;
                string replace = textBox1.Text.Replace(",", "").Replace("$", "").TrimStart('0'); //removes currency formating
                double amountTaken = double.Parse(replace); //converts to double
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
                balance = balance - amountTaken; //adds amount to balance;

                //date, from, transaction, amount, balance
                transaction +=
                    DateTime.Now.ToString("f") +
                    "|Withdraw" +
                    "|Withdraw from Tylanni Banking Software|" +
                    "-" + textBox1.Text + "|" +
                    balance.ToString("C") + ";";

                reader.Close();

                withdraw.CommandText =
                    "UPDATE Banking set [Balance]='" + balance.ToString() + "', [Transactions]='" + transaction +
                    "' where [Routing Number]=" + int.Parse(account.RoutingNum) + "";
               
                withdraw.ExecuteNonQuery(); //updates balance
                bankingDatabaseConnection.Close();
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}