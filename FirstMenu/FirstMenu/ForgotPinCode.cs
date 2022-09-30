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
    public partial class ForgotPinCode : Form
    {
        private String bankingDatabase; //the full string to connect to database
        private String passwordHash; //saved hashed password
        private int routing; //saved routing number
        private bool ifClicked = false; //if user clicked button
        private Security security = new Security(); //salting and hashing program
        private OleDbConnection bankingDatabaseConnection = new OleDbConnection(); //connection to database
        
        public ForgotPinCode(String databaseFile)
        {
            this.bankingDatabase = databaseFile; //sets connection string

            InitializeComponent();
            
            //sets the connection to the database string
            bankingDatabaseConnection.ConnectionString = databaseFile;
        }

        // =============================================================================================================================
        //                                                    FORM LOAD
        // =============================================================================================================================

        private void ForgotPinCode_Load(object sender, EventArgs e)
        {
            lblPinCode.Visible = false; //hides the pin code label
            txtPinCode.Visible = false; //hides the pin code textbox
            cboxPincode.Visible = false; //hides the pin code checkbox
            btnOK.Visible = false; //hides the ok button
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
        //                                                    PASSWORD VALIDATING
        // =============================================================================================================================

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

        // =============================================================================================================================
        //                                                    SHOW PASSWORD CHECKED CHANGED
        // =============================================================================================================================

        private void cboxShow_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxShow.Checked == true) //if checked
            {
                txtPassword.PasswordChar = '\0'; //gets "rid" of password char
            }
            else
            {
                txtPassword.PasswordChar = '*'; //resets the password char
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
            errorProvider.SetError(btnConfirm, "");

            String passwordSalt = ""; //local password salt string

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
                    //sets password salt to the one found on database
                    passwordSalt = readSalt["Password(Salt)"].ToString();
                }
            }
            bankingDatabaseConnection.Close();

            if (count == 0) //if no account was found
            {
                e.Cancel = true;
                errorProvider.SetError(btnConfirm, "Username not found");
                return;
            }

            passwordHash = security.Hash(txtPassword.Text, passwordSalt);

            bankingDatabaseConnection.Open();
            OleDbCommand checkPassword = new OleDbCommand();
            checkPassword.Connection = bankingDatabaseConnection;
            checkPassword.CommandText =
                "SELECT * from Banking where [Username]='" + txtUsername.Text + "' and [Password(Hash)]='" + passwordHash + "'";
            //finds username and password

            OleDbDataReader reader = checkPassword.ExecuteReader();
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

            if (count == 0) //if password didnt match found username
            {
                e.Cancel = true;
                errorProvider.SetError(btnConfirm, "Incorrect password");
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
                    lblPinCode.Visible = true; //shows the pin code label
                    txtPinCode.Visible = true; //shows the pin code textbox
                    cboxPincode.Visible = true; //shows the pin code checkbox
                    btnOK.Visible = true; //shows the ok button
                    txtUsername.Enabled = false; //disables the username textbox
                    txtPassword.Enabled = false; //disables the password textbox
                    btnConfirm.Enabled = false; //disables the confirm button
                    cboxShow.Checked = false; //sets the show password checkbox to false
                    cboxShow.Enabled = false; //disables the password checkbox
                    lblConfirm.Text = "Accepted!"; //sets the confirm label text
                    this.AcceptButton = this.btnOK; //sets enter key to press ok button
                    ifClicked = false; //changes the clicked boolean to false
                }
            }
        }

        // =============================================================================================================================
        //                                                    PIN CODE VALIDATING
        // =============================================================================================================================

        private void txtPinCode_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtPinCode, "");

            if (txtPinCode.Text == "" && !ifClicked) //if empty and button has been clicked
            {
                e.Cancel = true;
                errorProvider.SetError(txtPinCode, "Pin code is required");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    PIN CODE CHECKED CHANGED
        // =============================================================================================================================

        private void cboxPincode_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxPincode.Checked == true) //if checked
            {
                txtPinCode.PasswordChar = '\0'; //gets "rid" of password char
            }
            else
            {
                txtPinCode.PasswordChar = '*'; //resets password char
            }
        }

        // =============================================================================================================================
        //                                                    OK BUTTON CLICKED
        // =============================================================================================================================

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                String pinCodeSalt, pinCodeHash;

                pinCodeSalt = security.Salt();
                pinCodeHash = security.Hash(txtPinCode.Text, pinCodeSalt);

                bankingDatabaseConnection.Open();
                OleDbCommand updatePinCode = new OleDbCommand();
                updatePinCode.Connection = bankingDatabaseConnection;
                updatePinCode.CommandText =
                    "UPDATE Banking set [Account Pin Code(Salt)]='" + pinCodeSalt + "',[Account Pin Code(Hash)]='" + pinCodeHash +
                    "' where [Username]='" + txtUsername.Text + "' and [Password(Hash)]='" + passwordHash + "'";
                //sets the pin code salt and pin code hash

                updatePinCode.ExecuteNonQuery(); //updates pin code salt and hash
                bankingDatabaseConnection.Close();

                MessageBox.Show("Pin-Code changed");
                this.DialogResult = DialogResult.OK;
                    
            }
        }
    }
}