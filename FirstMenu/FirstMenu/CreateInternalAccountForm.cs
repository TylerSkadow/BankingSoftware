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
    public partial class CreateInternalAccountForm : Form
    {
        private String bankingDataBase;
        private List<Account> accounts = new List<Account>();
        private OleDbConnection bankingDatabaseConnection = new OleDbConnection();

        public CreateInternalAccountForm(String databaseFile, List<Account> loggedInAccounts)
        {
            this.bankingDataBase = databaseFile; //string of database connection
            this.accounts = loggedInAccounts; //list of connected accounts

            InitializeComponent();
            
            bankingDatabaseConnection.ConnectionString = databaseFile; //sets the database connection
        }

        // =============================================================================================================================
        //                                                    ACCOUNT TYPE VALIDATE
        // =============================================================================================================================

        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(comboBox1, "");

            //if the combo box is empty
            if (comboBox1.Text == "")
            {
                e.Cancel = true;
                errorProvider.SetError(comboBox1, "Please select an account type");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    ROUTING NUMBER FUNTCTION
        // =============================================================================================================================

        private int routingNumber()
        {
            Random random = new Random();

            bool notFound = false; //boolean for if the routing number is used or not
            int routingNumber = 0;
            
            //keep going until the routing number is not a duplicate
            while(notFound == false)
            {
                int randomNum = random.Next(100000000, 999999999); //any routing number with 9 digits

                bankingDatabaseConnection.Open();
                OleDbCommand routing = new OleDbCommand();
                routing.Connection = bankingDatabaseConnection;
                routing.CommandText = "SELECT * from Banking where [Routing Number]=" + randomNum + "";

                OleDbDataReader reader = routing.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count++;
                }
                reader.Close();
                bankingDatabaseConnection.Close();
                
                //if there was no other routing number that was the same
                if (count == 0)
                {
                    routingNumber = randomNum;
                    notFound = true;
                }
            }
            return routingNumber;
        }

        // =============================================================================================================================
        //                                                    OK BUTTON CLICKED
        // =============================================================================================================================

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                DialogResult confirm = MessageBox.Show
                    ("Are you sure you want to create a new " + comboBox1.Text + "?", "Confirm", MessageBoxButtons.YesNoCancel);

                //if user clicks ok
                if (confirm == DialogResult.Yes)
                {
                    int routing = routingNumber(); //calls routing number

                    Account newAccount = new Account(
                        accounts[0].FirstName,
                        accounts[0].MiddleInitial,
                        accounts[0].LastName,
                        accounts[0].Suffix,
                        accounts[0].Email,
                        accounts[0].PhoneNumber,
                        accounts[0].SocialSecuritySalt,
                        accounts[0].SocialSecurityHash,
                        accounts[0].DateOfBirth,
                        accounts[0].Street,
                        accounts[0].State,
                        accounts[0].County,
                        accounts[0].ZipCode,
                        accounts[0].Username,
                        accounts[0].PasswordSalt,
                        accounts[0].PasswordHash,
                        comboBox1.Text,
                        accounts[0].PinCodeSalt,
                        accounts[0].PinCodeHash,
                        routing.ToString(),
                        accounts[0].Question1,
                        accounts[0].Answer1Salt,
                        accounts[0].Answer1Hash,
                        accounts[0].Question2,
                        accounts[0].Answer2Salt,
                        accounts[0].Answer2Hash,
                        accounts[0].Question3,
                        accounts[0].Answer3Salt,
                        accounts[0].Answer3Hash);


                    bankingDatabaseConnection.Open();
                    OleDbCommand createAccount = new OleDbCommand();
                    createAccount.Connection = bankingDatabaseConnection;

                    createAccount.CommandText = //long line of code that bascially just sets the table values to the info collected
                        "INSERT INTO Banking (" +
                        "[Username]," +
                        "[Password(Salt)]," +
                        "[Password(Hash)]," +
                        "[Account Type]," +
                        "[Account Pin Code(Salt)]," +
                        "[Account Pin Code(Hash)]," +
                        "[First Name]," +
                        "[Last Name]," +
                        "[Middle Initial]," +
                        "[Suffix]," +
                        "[Email]," +
                        "[Phone Number]," +
                        "[SSN(Salt)]," +
                        "[SSN(Hash)]," +
                        "[Date of Birth]," +
                        "[Street]," +
                        "[State]," +
                        "[County]," +
                        "[Zip Code]," +
                        "[Routing Number]," +
                        "[Security Question 1]," +
                        "[Security Answer 1(Salt)]," +
                        "[Security Answer 1(Hash)]," +
                        "[Security Question 2]," +
                        "[Security Answer 2(Salt)]," +
                        "[Security Answer 2(Hash)]," +
                        "[Security Question 3]," +
                        "[Security Answer 3(Salt)]," +
                        "[Security Answer 3(Hash)])" +
                        //names of columns ^^ inputs vv
                        "values('" + 
                        newAccount.Username + "', '" + 
                        newAccount.PasswordSalt + "', '" +
                        newAccount.PasswordHash + "', '" + 
                        newAccount.AccountType + "', '" + 
                        newAccount.PinCodeSalt + "', '" +
                        newAccount.PinCodeHash + "', '" +
                        newAccount.FirstName + "', '" + 
                        newAccount.LastName + "', '" + 
                        newAccount.MiddleInitial + "', '" + 
                        newAccount.Suffix + "', '" + 
                        newAccount.Email + "', '" +
                        newAccount.PhoneNumber + "', '" + 
                        newAccount.SocialSecuritySalt + "', '" +
                        newAccount.SocialSecurityHash + "', '" + 
                        newAccount.DateOfBirth + "', '" + 
                        newAccount.Street + "', '" +
                        newAccount.State + "', '" + 
                        newAccount.County + "', '" + 
                        newAccount.ZipCode + "', '" + 
                        newAccount.RoutingNum + "', '" +
                        newAccount.Question1 + "', '" +
                        newAccount.Answer1Salt + "', '" +
                        newAccount.Answer1Hash + "', '" +
                        newAccount.Question2 + "', '" +
                        newAccount.Answer2Salt + "', '" +
                        newAccount.Answer2Hash + "', '" + 
                        newAccount.Question3 + "', '" +
                        newAccount.Answer3Salt + "', '" +
                        newAccount.Answer3Hash + "')";

                    createAccount.ExecuteNonQuery(); //adds the new account to the database
                    accounts.Add(newAccount); //adds the new account to the list of connected accounts

                    bankingDatabaseConnection.Close();
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        // =============================================================================================================================
        //                                                    ACCOUNTS LIST GETTER
        // =============================================================================================================================
        
        //function to call and get the updated list with the new account added
        public List<Account> AccountsList
        {
            get
            {
                return accounts;
            }
        }
    }
}
