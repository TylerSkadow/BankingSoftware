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
    public partial class OpenAccount : Form
    {
        private BankingSoftware parentForm;
        private OleDbConnection bankingDatabaseConnection = new OleDbConnection();

        public OpenAccount(BankingSoftware parentForm, String databaseFile)
        {
            this.parentForm = parentForm;
            InitializeComponent();
            parentForm.Hide(); //Hides previous form
            timer.Enabled = true; //sets timer up and makes it change every minute
            timer.Interval = 1000;
            dtpDateOfBirth.Value = DateTime.Now; //sets the DOB value to today's date
            bankingDatabaseConnection.ConnectionString = databaseFile; //sets the connection to the database
        }

        // =============================================================================================================================
        //                                                    FIRST NAME VALIDATE
        // =============================================================================================================================

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtFirstName, "");

            if (txtFirstName.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtFirstName, "First Name is required");
                return;
            }

            for (int x = 0; x < txtFirstName.Text.Length; x++)
            {
                if (Char.IsLetter(txtFirstName.Text, x) == false && txtFirstName.Text[x] != '\'') //if not a letter
                {
                    e.Cancel = true;
                    errorProvider.SetError(txtFirstName, "Only letters allowed");
                    return;
                }
            }
        }

        // =============================================================================================================================
        //                                                    LAST NAME VALIDATE
        // =============================================================================================================================

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtLastName, "");

            if (txtLastName.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtLastName, "Last Name is required");
                return;
            }

            for (int x = 0; x < txtLastName.Text.Length; x++)
            {
                if (Char.IsLetter(txtLastName.Text, x) == false && txtLastName.Text[x] != '\'') //if not a letter
                {
                    e.Cancel = true;
                    errorProvider.SetError(txtLastName, "Only letters allowed");
                    return;
                }
            }
        }

        // =============================================================================================================================
        //                                                    MIDDLE INITIAL VALIDATE
        // =============================================================================================================================

        private void txtMiddleName_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtMiddleName, "");

            for (int x = 0; x < txtMiddleName.Text.Length; x++)
            {
                if (Char.IsLetter(txtMiddleName.Text, x) == false) //if not a letter
                {
                    e.Cancel = true;
                    errorProvider.SetError(txtMiddleName, "Only letters allowed");
                    return;
                }
            }
            return;
        }

        // =============================================================================================================================
        //                                                    SUFFIX VALIDATE
        // =============================================================================================================================

        private void txtSuffix_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtSuffix, "");

            for (int x = 0; x < txtSuffix.Text.Length; x++)
            {
                if (Char.IsLetter(txtSuffix.Text, x) == false && txtSuffix.Text.Length != 0) //if not a letter
                {
                    e.Cancel = true;
                    errorProvider.SetError(txtSuffix, "Only letters allowed");
                    return;
                }
            }
        }

        // =============================================================================================================================
        //                                                    EMAIL ADDRESS VALIDATE
        // =============================================================================================================================

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtEmail, "");

            bool ifValid = false;
            if (txtEmail.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtEmail, "Email is required");
                return;
            }

            for (int x = 0; x < txtEmail.Text.Length; x++)
            {
                if (txtEmail.Text[x].Equals('@')) //sees if @ symbol is ever used in email
                {
                    ifValid = true; //makes the valid bool true
                }
            }

            if (ifValid == false) //if the @ symbol wasn't found then its invalid
            {
                e.Cancel = true;
                errorProvider.SetError(txtEmail, "Email not valid");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    PHONE NUMBER VALIDATE
        // =============================================================================================================================

        private void txtPhoneNumber_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtPhoneNumber, "");

            if (txtPhoneNumber.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtPhoneNumber, "Phone Number is required");
                return;
            }

            for (int x = 0; x < txtPhoneNumber.Text.Length; x++)
            {
                //if they did not fill in all spots or didnt fully complete
                if (txtPhoneNumber.Text[x] == ' ' || txtPhoneNumber.Text.Length != 14)
                {
                    e.Cancel = true;
                    errorProvider.SetError(txtPhoneNumber, "Phone number isn't valid");
                    return;
                }
            }
        }

        // =============================================================================================================================
        //                                                    SOCIAL SECURITY VALIDATE
        // =============================================================================================================================

        private void txtSSN_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtSSN, "");

            if (txtSSN.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtSSN, "SSID is required");
                return;
            }

            for (int x = 0; x < txtSSN.Text.Length; x++)
            {
                //if they didnt not fill in all spots or didnt fully complete
                if (txtSSN.Text[x] == ' ' || txtSSN.Text.Length != 11) 
                {
                    e.Cancel = true;
                    errorProvider.SetError(txtSSN, "SSN isn't valid");
                    return;
                }
            }
        }

        // =============================================================================================================================
        //                                                    SOCIAL SECURITY CHECKED CHANGED
        // =============================================================================================================================

        private void checkBoxSSN_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSSN.Checked == true)
            {
                txtSSN.PasswordChar = '\0'; //shows text
            }
            else
            {
                txtSSN.PasswordChar = '*'; //shows passwordChar
            }
        }

        // =============================================================================================================================
        //                                                    DATE OF BIRTH VALIDATE
        // =============================================================================================================================

        private void dtpDateOfBirth_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(dtpDateOfBirth, "");

            // adds 18 years to the date entered to make sure that they are old enough
            if (dtpDateOfBirth.Value.AddYears(18) >= DateTime.Now.Date)    
            {
                e.Cancel = true;
                errorProvider.SetError(dtpDateOfBirth, "Too young to open an account");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    ADDRESS STREET VALIDATE
        // =============================================================================================================================

        private void txtAddressStreet_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtAddressStreet, "");

            if (txtAddressStreet.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtAddressStreet, "Please enter an address");
                return;
            }

            for (int x = 0; x < txtAddressStreet.Text.Length; x++)
            {
                if (Char.IsSymbol(txtAddressStreet.Text, x) == true) // checks if there is any symbols
                {
                    e.Cancel = true;
                    errorProvider.SetError(txtAddressStreet, "Only letters and digits allowed");
                    return;
                }
            }
        }

        // =============================================================================================================================
        //                                                    ADDRESS STATE VALIDATE
        // =============================================================================================================================

        private void cboxAddressState_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(cboxAddressState, "");

            if (cboxAddressState.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(cboxAddressState, "Please select a state");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    ADDRESS COUNTY VALIDATE
        // =============================================================================================================================

        private void txtAddressCounty_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtAddressCounty, "");

            if (txtAddressCounty.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtAddressCounty, "Please enter a county");
                return;
            }

            for (int x = 0; x < txtAddressCounty.Text.Length; x++)
            {
                //checks to see if it is anything but a letter
                if (Char.IsLetter(txtAddressCounty.Text, x) == false && Char.IsWhiteSpace(txtAddressCounty.Text, x) == false) 
                {
                    e.Cancel = true;
                    errorProvider.SetError(txtAddressCounty, "Only letters allowed");
                    return;
                }
            }
        }

        // =============================================================================================================================
        //                                                    ADDRESS ZIP CODE VALIDATE
        // =============================================================================================================================

        private void txtAddressZipCode_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtAddressZipCode, "");

            if (txtAddressZipCode.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtAddressZipCode, "Please enter a zip code");
                return;
            }

            for (int x = 0; x < txtAddressZipCode.Text.Length; x++)
            {
                //if missing a digit or not long enough
                if (txtAddressZipCode.Text[x] == ' ' || txtAddressZipCode.Text.Length != 5) 
                {
                    e.Cancel = true;
                    errorProvider.SetError(txtAddressZipCode, "Zip code is wrong length");
                    return;
                }
            }
        }

        // =============================================================================================================================
        //                                                    USERNAME VALIDATE
        // =============================================================================================================================

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtUsername, "");

            if (txtUsername.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtUsername, "Username is required");
                return;
            }

            for (int x = 0; x < txtUsername.Text.Length; x++)
            {
                // if not a letter or digit
                if (Char.IsLetter(txtUsername.Text, x) == false && Char.IsDigit(txtUsername.Text, x) == false)
                {
                    e.Cancel = true;
                    errorProvider.SetError(txtUsername, "Only letters and numbers allowed");
                    return;
                }
            }
        }

        // =============================================================================================================================
        //                                                    ACCOUNT TYPE VALIDATE
        // =============================================================================================================================

        private void cboxAccountType_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(cboxAccountType, "");

            if (cboxAccountType.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(cboxAccountType, "Please select an account type");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    PASSWORD VALIDATE
        // =============================================================================================================================

        private void txtAccountPassword_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtAccountPassword, "");

            bool digit = false;
            bool upper = false;
            bool symbol = false;
            bool lower = false;

            if (txtAccountPassword.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtAccountPassword, "Password is required");
                return;
            }

            for (int x = 0; x < txtAccountPassword.Text.Length; x++)
            {
                // if digit
                if (Char.IsDigit(txtAccountPassword.Text, x) == true)
                {
                    digit = true;
                }
                //if upper
                else if (Char.IsUpper(txtAccountPassword.Text, x) == true)
                {
                    upper = true;
                }
                //if symbol/punctuation
                else if (Char.IsSymbol(txtAccountPassword.Text, x) == true || Char.IsPunctuation(txtAccountPassword.Text, x) == true)
                {
                    symbol = true;
                }
                //if lower
                else if (Char.IsLower(txtAccountPassword.Text, x) == true)
                {
                    lower = true;
                }
            }

            //not all needs were meet
            if (!(digit && upper && symbol && lower))
            {
                e.Cancel = true;
                errorProvider.SetError(txtAccountPassword, "Password needs one of each: digit, uppercase, lowercase, symbol");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    PASSWORD TEXT CHANGED
        // =============================================================================================================================

        private void txtAccountPassword_TextChanged(object sender, EventArgs e)
        {
            //"resets" each time text is changed
            lblPasswordUpper.ForeColor = Color.Red;
            lblPasswordLower.ForeColor = Color.Red;
            lblPasswordNum.ForeColor = Color.Red;
            lblPasswordSymbol.ForeColor = Color.Red;
            lblPasswordUpper.Text = "✘ Uppercase Letter";
            lblPasswordLower.Text = "✘ Lowercase Letter";
            lblPasswordNum.Text = "✘ Number";
            lblPasswordSymbol.Text = "✘ Symbol";

            //loop to find if upper, lower, number, and symbol is used
            for (int x = 0; x < txtAccountPassword.Text.Length; x++)
            {
                if (Char.IsUpper(txtAccountPassword.Text[x]) == true)
                {
                    lblPasswordUpper.Text = "✔ Uppercase Letter";
                    lblPasswordUpper.ForeColor = Color.Green;
                }
                else if (Char.IsLower(txtAccountPassword.Text[x]) == true)
                {
                    lblPasswordLower.Text = "✔ Lowercase Letter";
                    lblPasswordLower.ForeColor = Color.Green;
                }
                else if (Char.IsDigit(txtAccountPassword.Text[x]) == true)
                {
                    lblPasswordNum.Text = "✔ Number";
                    lblPasswordNum.ForeColor = Color.Green;
                }
                else if (Char.IsSymbol(txtAccountPassword.Text[x]) == true || Char.IsPunctuation(txtAccountPassword.Text[x]) == true)
                {
                    lblPasswordSymbol.Text = "✔ Symbol";
                    lblPasswordSymbol.ForeColor = Color.Green;
                }
            }
        }

        // =============================================================================================================================
        //                                                    CONFIRM PASSWORD VALIDATE
        // =============================================================================================================================

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtConfirmPassword, "");

            //if confirm password and password are not the same
            if (txtConfirmPassword.Text != txtAccountPassword.Text)
            {
                e.Cancel = true;
                errorProvider.SetError(txtConfirmPassword, "Passwords do not match");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    PASSWORD CHECKED CHANGED
        // =============================================================================================================================

        private void checkBoxPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPassword.Checked == true)
            {
                txtAccountPassword.PasswordChar = '\0'; //shows text
                txtConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                txtAccountPassword.PasswordChar = '*'; //shows passwordChar
                txtConfirmPassword.PasswordChar = '*';
            }
        }

        // =============================================================================================================================
        //                                                    PIN CODE VALIDATE
        // =============================================================================================================================

        private void txtPinCode_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtPinCode, "");

            if (txtPinCode.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtPinCode, "Pin code is required");
                return;
            }

            //if pin code is not 4 digits
            else if (txtPinCode.Text.Length != 4)
            {
                e.Cancel = true;
                errorProvider.SetError(txtPinCode, "Pin code needs 4 digits");
                return;
            }

            for (int x = 0; x < txtPinCode.Text.Length; x++)
            {
                //if pin code does not have only digits
                if (Char.IsDigit(txtPinCode.Text, x) == false)
                {
                    e.Cancel = true;
                    errorProvider.SetError(txtPinCode, "Pin code only allows digits");
                    return;
                }
            }
        }

        // =============================================================================================================================
        //                                                    PIN CODE CHECKED CHANGED
        // =============================================================================================================================

        private void checkBoxPinCode_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPinCode.Checked == true)
            {
                txtPinCode.PasswordChar = '\0'; //shows text
            }
            else
            {
                txtPinCode.PasswordChar = '*'; //shows passwordChar
            }
        }

        // =============================================================================================================================
        //                                                    DUPLICATE USERNAME FUNCTION
        // =============================================================================================================================

        private bool checkDuplicate()
        {
            //scans through database to see if there is an already exisiting username
            bankingDatabaseConnection.Open();
            OleDbCommand duplicate = new OleDbCommand();
            duplicate.Connection = bankingDatabaseConnection;
            duplicate.CommandText = "SELECT * from Banking where [Username] = '" + txtUsername.Text + "'";
            OleDbDataReader reader = duplicate.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count++;
            }
            bankingDatabaseConnection.Close();

            if (count == 0)
            {
                return false; //no duplicates
            }
            else
            {
                return true; //one or more
            }
        }

        // =============================================================================================================================
        //                                                    GENERATE ROUTING NUMBER FUNCTION
        // =============================================================================================================================

        private int routingNumber()
        {
            Random random = new Random();
            bool notFound = false;
            int routingNumber = 0;

            //while the routing number is invalid
            while(notFound == false)
            {
                int randomNum = random.Next(100000000, 999999999); //generates a random 9 digit code

                bankingDatabaseConnection.Open();
                OleDbCommand routing = new OleDbCommand();
                routing.Connection = bankingDatabaseConnection;

                routing.CommandText = "SELECT * from Banking where [Routing Number]= " + randomNum + "";
                OleDbDataReader reader = routing.ExecuteReader();

                int count = 0;
                while(reader.Read())
                {
                    count++;
                }
                bankingDatabaseConnection.Close();

                if (count == 0)
                {
                    routingNumber = randomNum;
                    notFound = true;
                }
            }

            return routingNumber;
        }

        // =============================================================================================================================
        //                                                    QUESTION 1 VALIDATE
        // =============================================================================================================================

        private void cboxQuestion1_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(cboxQuestion1, "");

            if (cboxQuestion1.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(cboxQuestion1, "Please select a question");
                return;
            }

            //if repeating question
            if (cboxQuestion1.SelectedItem == cboxQuestion2.SelectedItem || cboxQuestion1.SelectedItem == cboxQuestion3.SelectedItem)
            {
                e.Cancel = true;
                errorProvider.SetError(cboxQuestion1, "Can not have repeating questions");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    ANSWER 1 VALIDATE
        // =============================================================================================================================

        private void txtAnswer1_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtAnswer1, "");

            if (txtAnswer1.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtAnswer1, "Cannot be blank");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    QUESTION 2 VALIDATE
        // =============================================================================================================================

        private void cboxQuestion2_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(cboxQuestion2, "");

            if (cboxQuestion2.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(cboxQuestion2, "Please select a question");
                return;
            }

            //if repeating question
            if (cboxQuestion2.SelectedItem == cboxQuestion1.SelectedItem || cboxQuestion2.SelectedItem == cboxQuestion3.SelectedItem)
            {
                e.Cancel = true;
                errorProvider.SetError(cboxQuestion2, "Can not have repeating questions");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    ANSWER 2 VALIDATE
        // =============================================================================================================================

        private void txtAnswer2_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtAnswer2, "");

            if (txtAnswer2.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtAnswer2, "Cannot be blank");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    QUESTION 3 VALIDATE
        // =============================================================================================================================

        private void cboxQuestion3_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(cboxQuestion3, "");

            if (cboxQuestion3.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(cboxQuestion3, "Please select a question");
                return;
            }

            //if repeating question
            if (cboxQuestion3.SelectedItem == cboxQuestion1.SelectedItem || cboxQuestion3.SelectedItem == cboxQuestion2.SelectedItem)
            {
                e.Cancel = true;
                errorProvider.SetError(cboxQuestion3, "Can not have repeating questions");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    ANSWER 3 VALIDATE
        // =============================================================================================================================

        private void txtAnswer3_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtAnswer3, "");

            if (txtAnswer3.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtAnswer3, "Cannot be blank");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    CREATE BUTTON VALIDATE
        // =============================================================================================================================

        private void btnCreate_Validating(object sender, CancelEventArgs e)
        {
            bool duplicate = checkDuplicate(); //if there is another account with same username

            errorProvider.SetError(btnCreate, "");

            //if the username is aleady in use
            if (duplicate == true)
            {
                e.Cancel = true;
                errorProvider.SetError(btnCreate, "Username is taken");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    CREATE BUTTON CLICKED
        // =============================================================================================================================

        private void btnCreate_Click(object sender, EventArgs e)
        {
            bool created = false;

            //if all fields are valid and account has not been created yet
            if (this.ValidateChildren() && !created)
            {
                int routing = routingNumber();

                Security security = new Security();

                String saltPassword = security.Salt();
                String saltPinCode = security.Salt();
                String saltSSN = security.Salt();
                String saltAnswer1 = security.Salt();
                String saltAnswer2 = security.Salt();
                String saltAnswer3 = security.Salt();
                
                Account newAccount = new Account(
                    txtFirstName.Text, 
                    txtMiddleName.Text, 
                    txtLastName.Text, 
                    txtSuffix.Text, 
                    txtEmail.Text, 
                    txtPhoneNumber.Text,
                    saltSSN,
                    security.Hash(txtSSN.Text,saltSSN), 
                    dtpDateOfBirth.Text, 
                    txtAddressStreet.Text, 
                    cboxAddressState.Text, 
                    txtAddressCounty.Text, 
                    txtAddressZipCode.Text,
                    txtUsername.Text,
                    saltPassword,
                    security.Hash(txtAccountPassword.Text, saltPassword),
                    cboxAccountType.Text, 
                    saltPinCode,
                    security.Hash(txtPinCode.Text, saltPinCode),
                    routing.ToString(),
                    cboxQuestion1.Text,
                    saltAnswer1,
                    security.Hash(txtAnswer1.Text, saltAnswer1),
                    cboxQuestion2.Text,
                    saltAnswer2,
                    security.Hash(txtAnswer2.Text, saltAnswer2),
                    cboxQuestion3.Text,
                    saltAnswer3,
                    security.Hash(txtAnswer3.Text, saltAnswer3));

                bankingDatabaseConnection.Open(); //connects to database
                OleDbCommand createAccount = new OleDbCommand(); //command line creation
                createAccount.Connection = bankingDatabaseConnection; //sets the commands connection to the database

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

                    "VALUES('"
                    + newAccount.Username + "', '"
                    + newAccount.PasswordSalt + "', '"
                    + newAccount.PasswordHash + "', '"
                    + newAccount.AccountType + "', '"
                    + newAccount.PinCodeSalt + "', '"
                    + newAccount.PinCodeHash + "', '"
                    + newAccount.FirstName + "', '"
                    + newAccount.LastName + "', '"
                    + newAccount.MiddleInitial + "', '"
                    + newAccount.Suffix + "', '"
                    + newAccount.Email + "', '"
                    + newAccount.PhoneNumber + "', '"
                    + newAccount.SocialSecuritySalt + "', '"
                    + newAccount.SocialSecurityHash + "', '"
                    + newAccount.DateOfBirth + "', '"
                    + newAccount.Street + "', '"
                    + newAccount.State + "', '"
                    + newAccount.County + "', '"
                    + newAccount.ZipCode + "', '"
                    + newAccount.RoutingNum + "', '"
                    + newAccount.Question1 + "', '"
                    + newAccount.Answer1Salt + "', '"
                    + newAccount.Answer1Hash + "', '"
                    + newAccount.Question2 + "', '"
                    + newAccount.Answer2Salt + "', '"
                    + newAccount.Answer2Hash + "', '"
                    + newAccount.Question3 + "', '"
                    + newAccount.Answer3Salt + "', '"
                    + newAccount.Answer3Hash + "')";

                createAccount.ExecuteNonQuery(); //executes without anything returning aka like void?
                MessageBox.Show("Account Created!");
                bankingDatabaseConnection.Close();
                created = true;
            }
            else if (created)
            {
                this.Close();
            }
        }

        // =============================================================================================================================
        //                                                    BACK OR CANCEL BUTTON CLICKED
        // =============================================================================================================================

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //closes the form
            this.Close();
        }

        // =============================================================================================================================
        //                                                    FORM CLOSED
        // =============================================================================================================================

        private void OpenAccount_FormClosed(object sender, FormClosedEventArgs e)
        {
            //shows previous form after close
            parentForm.Show();
        }

        // =============================================================================================================================
        //                                                    TIMER TICK
        // =============================================================================================================================

        private void timer1_Tick(object sender, EventArgs e)
        {
            //sets the label to the current time
            lblTime.Text = DateTime.Now.ToString("g");
        }
    }
}