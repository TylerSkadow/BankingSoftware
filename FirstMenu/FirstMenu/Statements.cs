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
using Microsoft.Office.Interop.Excel;

namespace FirstMenu
{
    public partial class Statements : Form
    {
        private String bankingDatabase; //the full string to connect to database
        private List<Account> accountList; //list of accounts
        private OleDbConnection bankingDatabaseConnection = new OleDbConnection(); //connection to database
        public Statements(String databaseFile, List<Account> selectedAccounts)
        {
            this.bankingDatabase = databaseFile; //sets connection string
            this.accountList = selectedAccounts; //sets the selected accounts

            InitializeComponent();

            //sets the connection to the databasse string
            bankingDatabaseConnection.ConnectionString = databaseFile;
        }

        // =============================================================================================================================
        //                                                    FORM LOAD
        // =============================================================================================================================

        private void Statements_Load(object sender, EventArgs e)
        {
            foreach (Account account in accountList) //a loop through all accounts
            {
                cboxAccounts.Items.Add(account.AccountType + " (..." + account.RoutingNum.Substring(5) + ")");
            }
        }

        // =============================================================================================================================
        //                                                    ACCOUNTS COMBOBOX VALIDATING
        // =============================================================================================================================

        private void cboxAccounts_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(cboxAccounts, "");

            if (cboxAccounts.Text == "")
            {
                e.Cancel = true;
                errorProvider.SetError(cboxAccounts, "Please select an account");
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
                int counter = 1;

                Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
                application.Visible = true;

                Workbook workbook = application.Workbooks.Add();
                Worksheet sheet = workbook.Worksheets[1];

                sheet.PageSetup.CenterHeader = "&18TYLANNI BANKING SOFTWARE";
                sheet.PageSetup.LeftFooter = "Statement for account: " + accountList[cboxAccounts.SelectedIndex].RoutingNum;
                sheet.PageSetup.RightFooter = DateTime.Now.ToString("f");
                sheet.PageSetup.PrintHeadings = true;
                sheet.PageSetup.PrintGridlines = false;

                sheet.Cells[1, 1] = "DATE";
                sheet.Cells[1, 2] = "TYPE";
                sheet.Cells[1, 3] = "DETAILS";
                sheet.Cells[1, 4] = "AMOUNT";
                sheet.Cells[1, 5] = "BALANCE";

                sheet.Cells[1, 1].EntireRow.Font.Bold = true;

                bankingDatabaseConnection.Open();
                OleDbCommand getTransactions = new OleDbCommand();
                getTransactions.Connection = bankingDatabaseConnection;
                getTransactions.CommandText =
                    "SELECT * from Banking where [Routing Number]=" + int.Parse(accountList[cboxAccounts.SelectedIndex].RoutingNum) + "";
                OleDbDataReader reader = getTransactions.ExecuteReader();

                int count = 0;
                string transactions = "";

                while (reader.Read())
                {
                    count++;

                    if (count == 1)
                    {
                        transactions = reader["Transactions"].ToString(); //sets transactions list
                    }
                }
                reader.Close();

                String[] transactionList = transactions.Split(';');
                
                for (int x = 0; x < transactionList.Length - 1; x++)
                {
                    String[] arr = transactionList[x].Split('|');
                    String date = arr[0];
                    String type = arr[1];
                    String detail = arr[2];
                    String amount = arr[3];
                    String balance = arr[4];

                    if (amount[0] != '-')
                    {
                        sheet.Cells[1 + counter, 4].Font.Bold = true;
                    }
                    else
                    {
                        amount = amount.Remove(0, 1);
                    }

                    sheet.Cells[1 + counter, 1] = date;
                    sheet.Cells[1 + counter, 2] = type;
                    sheet.Cells[1 + counter, 3] = detail;
                    sheet.Cells[1 + counter, 4] = amount;
                    sheet.Cells[1 + counter, 5] = balance;

                    counter++;
                }

                sheet.Range["A:E"].Columns.AutoFit();
                this.Close();
            }
        }

        // =============================================================================================================================
        //                                                    CANCEL BUTTON CLICKED
        // =============================================================================================================================

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
