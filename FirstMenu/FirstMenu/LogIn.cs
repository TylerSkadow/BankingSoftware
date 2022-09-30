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
    public partial class LogIn : Form
    {
        private BankingSoftware parentForm;
        private String bankingDataBase;
        private List<Account> loggedInAccountsList = new List<Account>();
        private String hashPassword;
        private OleDbConnection bankingDatabaseConnection = new OleDbConnection();
        private Security security = new Security();

        public LogIn(BankingSoftware parentForm, String databaseFile)
        {
            this.parentForm = parentForm;
            this.bankingDataBase = databaseFile;
            InitializeComponent();
            parentForm.Hide(); //closes previous form
            timer.Enabled = true; //sets timer up and makes it change every minute
            timer.Interval = 1000;
            bankingDatabaseConnection.ConnectionString = databaseFile; //sets the connection to the database
        }

        // ==========================================================================================================================
        //                                                 USERNAME/EMAIL VALIDATE
        // ==========================================================================================================================

        private void txtLogInName_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtLogInName, "");

            if (txtLogInName.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtLogInName, "Username/Email required");
                return;
            }
        }

        // ==========================================================================================================================
        //                                                 PASSWORD VALIDATE
        // ==========================================================================================================================

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtPassword, "");

            if (txtPassword.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtPassword, "Password required");
                return;
            }
        }

        // ==========================================================================================================================
        //                                                 LOG IN BUTTON VALIDATE
        // ==========================================================================================================================

        private void btnLogIn_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(btnLogIn, "");

            bankingDatabaseConnection.Open();
            OleDbCommand getPassword = new OleDbCommand();
            getPassword.Connection = bankingDatabaseConnection;
            getPassword.CommandText =
                "SELECT * from Banking where [Username]='" + txtLogInName.Text + "' or [Email]='" + txtLogInName.Text + "'";
            //using the username or email to get the password salt

            OleDbDataReader readPassword = getPassword.ExecuteReader();

            String salt = "";
            int count = 0;

            while (readPassword.Read())
            {
                count++;

                if (count == 1)
                {
                    salt = readPassword["Password(Salt)"].ToString(); 
                    //reads in the salt if there is a correct username or email
                }
            }
            bankingDatabaseConnection.Close();

            //if username/email is not found
            if (count == 0)
            {
                e.Cancel = true;
                errorProvider.SetError(txtLogInName, "Incorrect username/email");
                return;
            }

            hashPassword = security.Hash(txtPassword.Text, salt);
            //creates the hash of the entered password with the salt

            bankingDatabaseConnection.Open();
            OleDbCommand logIn = new OleDbCommand();
            logIn.Connection = bankingDatabaseConnection;
            logIn.CommandText =
                "SELECT * from Banking where ([Username]='" + txtLogInName.Text + "' or [Email]='" + txtLogInName.Text +
                "') and [Password(Hash)]= '" + hashPassword + "'";
            //checks if the password along with the username/email is correct

            OleDbDataReader reader = logIn.ExecuteReader();
            count = 0;
            while(reader.Read())
            {
                count++;
            }
            bankingDatabaseConnection.Close();

            //if password does not match the one on file
            if (count == 0)
            {
                e.Cancel = true;
                errorProvider.SetError(txtPassword, "Incorrect password");
                return;
            }
        }

        // ==========================================================================================================================
        //                                                 LOG IN BUTTON CLICKED
        // ==========================================================================================================================

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            //if all other controls are valid
            if(this.ValidateChildren())
            {
                loggedInAccountsList.Clear();
                int count = 0;
                int pinCode = 0;
                String pinSalt = "";

                PinCodeForm openPinCodeForm = new PinCodeForm();
                //creates and shows the pin code pad for user to enter the correct pin code

                //if they click ok
                if (openPinCodeForm.ShowDialog() == DialogResult.OK)
                {
                    pinCode = openPinCodeForm.InputPinCode;
                    //gets the entered pin code

                    bankingDatabaseConnection.Open();
                    OleDbCommand getPinSalt = new OleDbCommand();
                    getPinSalt.Connection = bankingDatabaseConnection;
                    getPinSalt.CommandText =
                        "SELECT * from Banking where ([Username]='" + txtLogInName.Text + "' or [Email]='" + txtLogInName.Text + 
                        "') and [Password(Hash)]='" + hashPassword + "'";
                    //using username or email with password to get the pin code salt

                    OleDbDataReader readPinSalt = getPinSalt.ExecuteReader();

                    while (readPinSalt.Read())
                    {
                        count++;

                        //if found
                        if (count == 1)
                        {
                            pinSalt = readPinSalt["Account Pin Code(Salt)"].ToString();
                            //reads in the salt
                        }
                    }

                    String pinHash = security.Hash(pinCode.ToString(), pinSalt);
                    //creates the hash of the entered pin code with the salt

                    OleDbCommand logInPin = new OleDbCommand();
                    logInPin.Connection = bankingDatabaseConnection;
                    logInPin.CommandText =
                        "SELECT * from Banking where ([Username]='" + txtLogInName.Text + "' or [Email]='" + txtLogInName.Text + 
                        "') and [Password(Hash)]= '" + hashPassword + "' and [Account Pin Code(Hash)]='" + pinHash + "'";
                    //using username or email along with the password and pincode to make sure the pincode is correct

                    OleDbDataReader readerPin = logInPin.ExecuteReader();

                    count = 0;

                    while (readerPin.Read())
                    {
                        count++;

                        //if found
                        if (count == 1)
                        {
                            //creates account object with the on file data
                            Account newAccount = new Account(
                                readerPin["First Name"].ToString(),
                                readerPin["Middle Initial"].ToString(),
                                readerPin["Last Name"].ToString(),
                                readerPin["Suffix"].ToString(),
                                readerPin["Email"].ToString(),
                                readerPin["Phone Number"].ToString(),
                                readerPin["SSN(Salt)"].ToString(),
                                readerPin["SSN(Hash)"].ToString(),
                                readerPin["Date of Birth"].ToString(),
                                readerPin["Street"].ToString(),
                                readerPin["State"].ToString(),
                                readerPin["County"].ToString(),
                                readerPin["Zip Code"].ToString(),
                                readerPin["Username"].ToString(),
                                readerPin["Password(Salt)"].ToString(),
                                readerPin["Password(Hash)"].ToString(),
                                readerPin["Account Type"].ToString(),
                                readerPin["Account Pin Code(Salt)"].ToString(),
                                readerPin["Account Pin Code(Hash)"].ToString(),
                                readerPin["Routing Number"].ToString(),
                                readerPin["Security Question 1"].ToString(),
                                readerPin["Security Answer 1(Salt)"].ToString(),
                                readerPin["Security Answer 1(Hash)"].ToString(),
                                readerPin["Security Question 2"].ToString(),
                                readerPin["Security Answer 2(Salt)"].ToString(),
                                readerPin["Security Answer 2(Hash)"].ToString(),
                                readerPin["Security Question 3"].ToString(),
                                readerPin["Security Answer 3(Salt)"].ToString(),
                                readerPin["Security Answer 3(Hash)"].ToString());

                            //adds newAccount to accounts list in case of more than one account on file
                            loggedInAccountsList.Add(newAccount);

                            //decreases count to 0 in order to allow for more accounts
                            count--;
                        }
                    }
                    bankingDatabaseConnection.Close();

                    //if an account is found
                    if (loggedInAccountsList.Count != 0)
                    {
                        txtLogInName.Text = ""; //resets log in and password textboxs
                        txtPassword.Text = "";

                        //opens the account homepage
                        HomePage homepage = new HomePage(this, bankingDataBase, loggedInAccountsList);
                        homepage.Show();
                    }

                    //if the pin code entered was incorrect
                    else
                    {
                        MessageBox.Show("Incorrect Pin Code");
                    }
                }

                //if user does not click ok on pinCode form
                else
                {
                    MessageBox.Show("Pin-code required");
                }
            }
        }

        // ==========================================================================================================================
        //                                                 FORGOT LOG IN BUTTON
        // ==========================================================================================================================

        private void btnForgot_Click(object sender, EventArgs e)
        {
            //opens forgotLogin form
            ForgotLogin forgot = new ForgotLogin(this, bankingDataBase);
            forgot.Show();
        }

        // ==========================================================================================================================
        //                                                 BACK BUTTON CLICKED
        // ==========================================================================================================================

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ==========================================================================================================================
        //                                                 FORM CLOSED
        // ==========================================================================================================================

        private void LogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            //when form closed shows the previous form
            parentForm.Show();
        }

        // ==========================================================================================================================
        //                                                 TIMER TICK
        // ==========================================================================================================================

        private void timer_Tick(object sender, EventArgs e)
        {
            //sets the label to the current time
            lblTime.Text = DateTime.Now.ToString("g");
        }
    }
}
