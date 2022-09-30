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
    public partial class ForgotPassword : Form
    {
        private String bankingDatabase; //the full string to connect to database
        private String SSNHash; //saved hashed SSN
        private int routing; //saved routing number
        private bool ifClicked = false; //if user clicked button
        private Security security = new Security(); //salting and hashing program
        private OleDbConnection bankingDatabaseConnection = new OleDbConnection(); //connection to database

        public ForgotPassword(String databaseFile)
        {
            this.bankingDatabase = databaseFile; //sets connection string

            InitializeComponent();
            
            //sets the connection to the database string
            bankingDatabaseConnection.ConnectionString = databaseFile;
        }

        // =============================================================================================================================
        //                                                    FORM LOAD
        // =============================================================================================================================

        private void ForgotPassword_Load(object sender, EventArgs e)
        {
            lblPassword.Visible = false; //hides the password label
            txtPassword.Visible = false; //hides the password textbox
            cboxPassword.Visible = false; //hides the password checkbox
            btnOK.Visible = false; //hides ok button
        }

        // =============================================================================================================================
        //                                                    USERNAME VALIDATING
        // =============================================================================================================================

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtUsername, "");

            if (txtUsername.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtUsername, "Username required");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    SSN VALIDATING
        // =============================================================================================================================

        private void txtSSN_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtSSN, "");

            if (txtSSN.Text == "   -  -") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtSSN, "SSN required");
                return;
            }

            if (txtSSN.Text.Length != 11) //if not fully filled in
            {
                e.Cancel = true;
                errorProvider.SetError(txtSSN, "SSN is not valid");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    SHOW SSN CHECKED CHANGED
        // =============================================================================================================================

        private void cboxShow_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxShow.Checked == true) //if checked
            {
                txtSSN.PasswordChar = '\0'; //gets "rid" of password char
            }
            else
            {
                txtSSN.PasswordChar = '*'; //resets the password char
            }
        }

        // =============================================================================================================================
        //                                                    CANCEL BUTTON CLICKED
        // =============================================================================================================================

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); //closes current form
        }

        // =============================================================================================================================
        //                                                    CONFIRM BUTTON VALIDATING
        // =============================================================================================================================

        private void btnConfirm_Validating(object sender, CancelEventArgs e)
        {
            String SSNsalt = ""; //local SSN salt string

            bankingDatabaseConnection.Open();
            OleDbCommand getSalt = new OleDbCommand();
            getSalt.Connection = bankingDatabaseConnection;
            getSalt.CommandText =
                "SELECT * from Banking where [Username]='" + txtUsername.Text + "'";
            //finds username in database

            OleDbDataReader readSalt = getSalt.ExecuteReader();
            int count = 0;
            while (readSalt.Read())
            {
                count++;
                if (count == 1)
                {
                    //sets ssn salt to the one found on database
                    SSNsalt = readSalt["SSN(Salt)"].ToString();
                }
            }
            bankingDatabaseConnection.Close();

            if (count == 0) //if no account was found
            {
                e.Cancel = true;
                errorProvider.SetError(btnConfirm, "Username not found");
                return;
            }

            SSNHash = security.Hash(txtSSN.Text, SSNsalt);
            
            bankingDatabaseConnection.Open();
            OleDbCommand checkSSID = new OleDbCommand();
            checkSSID.Connection = bankingDatabaseConnection;
            checkSSID.CommandText =
                "SELECT * from Banking where [Username]='" + txtUsername.Text + "' and [SSN(Hash)]='" + SSNHash + "'";
            //finds username and SSN

            OleDbDataReader reader = checkSSID.ExecuteReader();
            count = 0;
            while (reader.Read())
            {
                count++;
                if (count == 1)
                {
                    routing = int.Parse(reader["Routing Number"].ToString());
                }
            }
            bankingDatabaseConnection.Close();

            if (count == 0) //if SSN didnt match found username
            {
                e.Cancel = true;
                errorProvider.SetError(btnConfirm, "Incorrect SSN");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    CONFIRM BUTTON CLICKED
        // =============================================================================================================================

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ifClicked = true; //clicked the button

            if (this.ValidateChildren())
            {
                SecurityQuestions questions = new SecurityQuestions(bankingDatabase, routing);

                if (questions.ShowDialog() == DialogResult.OK) //if all answers are correct
                {
                    lblPassword.Visible = true; //shows the password label
                    txtPassword.Visible = true; //shows the password textbox
                    cboxPassword.Visible = true; //shows the password checkbox
                    btnOK.Visible = true; //shows the ok button
                    txtUsername.Enabled = false; //disables the username textbox
                    txtSSN.Enabled = false; //disables the ssn textbox
                    btnConfirm.Enabled = false; //disables the confirm button
                    cboxShow.Checked = false; //sets the show SSN checkbox to false
                    cboxShow.Enabled = false; //disables the ssn checkbox
                    lblConfirm.Text = "Accepted!"; //sets the coffirm label text
                    this.AcceptButton = this.btnOK; //sets enter key to press ok button
                    ifClicked = false; //changes the clicked boolean to false
                }
            }
        }

        // =============================================================================================================================
        //                                                    PASSWORD VALIDATING
        // =============================================================================================================================

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtPassword, "");

            if (txtPassword.Text == "" && !ifClicked) //if empty and button has been clicked
            {
                e.Cancel = true;
                errorProvider.SetError(txtPassword, "Password required");
                return;
            }

            bool digit = false;
            bool upper = false;
            bool lower = false;
            bool symbol = false;

            //checks for a valid password with (number, uppercase, lowercase, and symbol)
            for (int x = 0; x < txtPassword.Text.Length; x++)
            {
                if (Char.IsDigit(txtPassword.Text, x) == true)
                {
                    digit = true;
                }
                else if (Char.IsUpper(txtPassword.Text, x) == true)
                {
                    upper = true;
                }
                //mix of both counts
                else if (Char.IsSymbol(txtPassword.Text, x) == true || Char.IsPunctuation(txtPassword.Text, x) == true)
                {
                    symbol = true;
                }
                else if (Char.IsLower(txtPassword.Text, x) == true)
                {
                    lower = true;
                }
            }

            if ((!(digit && upper && symbol && lower)) && !ifClicked) //if not valid and button has been clicked
            {
                e.Cancel = true;
                errorProvider.SetError(txtPassword, "Password needs one of each: digit, uppercase, lowercase, symbol");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    SHOW PASSWORD CHECKED CHANGED
        // =============================================================================================================================

        private void cboxPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxPassword.Checked == true)  //if checked
            {
                txtPassword.PasswordChar = '\0'; //gets "rid" of password char
            }
            else
            {
                txtPassword.PasswordChar = '*'; //resets password char
            }
        }

        // =============================================================================================================================
        //                                                    OK BUTTON CLICKED
        // =============================================================================================================================

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                String passwordSalt, passwordHash;

                passwordSalt = security.Salt();
                passwordHash = security.Hash(txtPassword.Text, passwordSalt);

                bankingDatabaseConnection.Open();
                OleDbCommand updatePassword = new OleDbCommand();
                updatePassword.Connection = bankingDatabaseConnection;
                updatePassword.CommandText =
                    "UPDATE Banking set [Password(Salt)]='" + passwordSalt + "',[Password(Hash)]='" + passwordHash +
                    "' where [Username]='" + txtUsername.Text + "' and [SSN(Hash)]='" + SSNHash + "'";
                //sets the password salt and password hash

                updatePassword.ExecuteNonQuery(); //updates password salt and hash
                bankingDatabaseConnection.Close();

                MessageBox.Show("Password changed");
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
