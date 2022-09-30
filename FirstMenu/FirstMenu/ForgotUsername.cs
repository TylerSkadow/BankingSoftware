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
    public partial class ForgotUsername : Form
    {
        private String bankingDatabase; //the full string to connect to database
        private String passwordHash; //saved hashed password
        private int routing; //saved routing number
        private bool ifClicked = false; //if user clicked button
        private Security security = new Security(); //salting and hashing program
        private OleDbConnection bankingDatabaseConnection = new OleDbConnection(); //connection to database

        public ForgotUsername(String databaseFile)
        {
            this.bankingDatabase = databaseFile; //sets connection string

            InitializeComponent();

            //sets the connection to the database string
            bankingDatabaseConnection.ConnectionString = databaseFile;
        }

        // =============================================================================================================================
        //                                                    FORM LOAD
        // =============================================================================================================================

        private void ForgotUsername_Load(object sender, EventArgs e)
        {
            lblUsername.Visible = false; //hides the username label
            txtUsername.Visible = false; //hides the username textbox
            btnOK.Visible = false; //hides the ok button
        }

        // =============================================================================================================================
        //                                                    EMAIL VALIDATING
        // =============================================================================================================================

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtEmail, "");

            bool ifValid = false; //boolean to see if email is valid
            if (txtEmail.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtEmail, "Email is required");
                return;
            }

            //goes through string to find if there is an '@'
            for (int x = 0; x < txtEmail.Text.Length; x++)
            {
                if (txtEmail.Text[x].Equals('@')) //if found
                {
                    ifValid = true;
                }
            }

            if (ifValid == false) //if not valid
            {
                e.Cancel = true;
                errorProvider.SetError(txtEmail, "Email not valid");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    PASSWORD VALIDATING
        // =============================================================================================================================

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtPassword, "");

            if (txtPassword.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtPassword, "Password is required");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    SHOW PASSWORD CHECKED CHANGED
        // =============================================================================================================================

        private void cboxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxShowPassword.Checked == true) //if checked
            {
                txtPassword.PasswordChar = '\0'; //gets "rid" of password char
            }
            else
            {
                txtPassword.PasswordChar = '*'; //resets password char
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

        private void btnConfim_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(btnConfim, "");

            String passwordSalt = ""; //local password salt string

            bankingDatabaseConnection.Open();
            OleDbCommand getSalt = new OleDbCommand();
            getSalt.Connection = bankingDatabaseConnection;
            getSalt.CommandText =
                "SELECT * from Banking where [Email]='" + txtEmail.Text + "'";
            //finds email in database

            OleDbDataReader readSalt = getSalt.ExecuteReader();
            int count = 0;
            while (readSalt.Read())
            {
                count++;
                if (count == 1)
                {
                    //sets password salt to the one found on database
                    passwordSalt = readSalt["Password(Salt)"].ToString();
                }
            }
            bankingDatabaseConnection.Close();

            if (count == 0) //if no account was found
            {
                e.Cancel = true;
                errorProvider.SetError(btnConfim, "No account found using email");
                return;
            }

            bankingDatabaseConnection.Open();
            OleDbCommand check = new OleDbCommand();
            check.Connection = bankingDatabaseConnection;
            passwordHash = security.Hash(txtPassword.Text, passwordSalt);
            check.CommandText = 
                "SELECT * from Banking where [Email]='" + txtEmail.Text + "' and [Password(Hash)]='" + passwordHash + "'";
            //infds email and password

            OleDbDataReader readCheck = check.ExecuteReader();
            count = 0;
            while (readCheck.Read())
            {
                count++;
                if (count == 1)
                {
                    routing = int.Parse(readCheck["Routing Number"].ToString());
                }
            }
            bankingDatabaseConnection.Close();

            if (count == 0) //if password didnt match found email
            {
                e.Cancel = true;
                errorProvider.SetError(btnConfim, "Incorrect Password");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    CONFIRM BUTTON CLICKED
        // =============================================================================================================================

        private void btnConfim_Click(object sender, EventArgs e)
        {
            ifClicked = true; //clicked the button

            if (this.ValidateChildren())
            {
                SecurityQuestions questions = new SecurityQuestions(bankingDatabase, routing);

                if (questions.ShowDialog() == DialogResult.OK) //if all answers are correct
                {
                    lblUsername.Visible = true; //shows the username label
                    txtUsername.Visible = true; //shows the username textbox
                    btnOK.Visible = true; //shows the ok button
                    btnConfim.Enabled = false; //disables the confirm button
                    txtEmail.Enabled = false; //disables the email textbox
                    txtPassword.Enabled = false; //disables the password textbox
                    cboxShowPassword.Checked = false; //sets the show password checkbox to false
                    cboxShowPassword.Enabled = false; //disables the password checkbox
                    lblConfirm.Text = "Accepted!"; //sets the confirm label text
                    this.AcceptButton = this.btnOK; //sets enter key to press ok button
                    ifClicked = false; //changes teh clikced boolean to false
                }
            }
        }

        // =============================================================================================================================
        //                                                    USERNAME VALIDATING
        // =============================================================================================================================

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtUsername, "");

            if (txtUsername.Text == "" && !ifClicked) //if empty and button has been clicked
            {
                e.Cancel = true;
                errorProvider.SetError(txtUsername, "Username is required");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    OK BUTTON VALIDATING
        // =============================================================================================================================

        private void btnOK_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(btnOK, "");

            bankingDatabaseConnection.Open();
            OleDbCommand duplicate = new OleDbCommand();
            duplicate.Connection = bankingDatabaseConnection;
            duplicate.CommandText = "SELECT * from Banking where [Username] = '" + txtUsername.Text + "'";
            //finds if username is in use

            OleDbDataReader reader = duplicate.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count++;
            }
            bankingDatabaseConnection.Close();

            if (count != 0 && !ifClicked) //if username is found and button has been clicked
            {
                e.Cancel = true;
                errorProvider.SetError(txtUsername, "Username is already taken");
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
                OleDbCommand usernameUpdate = new OleDbCommand();
                usernameUpdate.Connection = bankingDatabaseConnection;
                usernameUpdate.CommandText =
                    "UPDATE Banking set [Username]='" + txtUsername.Text +
                    "'where [Email]='" + txtEmail.Text + "' and [Password(Hash)]='" + passwordHash + "'";
                //sets the username

                usernameUpdate.ExecuteNonQuery(); //updates username
                bankingDatabaseConnection.Close();

                MessageBox.Show("Username changed to " + txtUsername.Text);
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
