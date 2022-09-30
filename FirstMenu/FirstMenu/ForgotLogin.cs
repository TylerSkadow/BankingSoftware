using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstMenu
{
    public partial class ForgotLogin : Form
    {
        private String bankingDatabase; //the full string to connect to database
        private LogIn logInForm; //previous form
        public ForgotLogin(LogIn parentform, String databaseFile)
        {
            this.logInForm = parentform; //sets previous form
            this.bankingDatabase = databaseFile; //sets connection string
            parentform.Hide(); //hides the previous form
            InitializeComponent();
        }

        // =============================================================================================================================
        //                                                    USERNAME BUTTON CLICKED
        // =============================================================================================================================

        private void btnUsername_Click(object sender, EventArgs e)
        {
            ForgotUsername username = new ForgotUsername(bankingDatabase);
            username.ShowDialog(); //shows ForgotUsername form
        }

        // =============================================================================================================================
        //                                                    EMAIL BUTTON CLICKED
        // =============================================================================================================================

        private void btnEmail_Click(object sender, EventArgs e)
        {
            ForgotEmail email = new ForgotEmail(bankingDatabase);
            email.ShowDialog(); //shows ForgotEmail form
        }

        // =============================================================================================================================
        //                                                    PASSWORD BUTTON CLICKED
        // =============================================================================================================================

        private void btnPassword_Click(object sender, EventArgs e)
        {
            ForgotPassword password = new ForgotPassword(bankingDatabase);
            password.ShowDialog(); //shows ForgotPassword form
        }

        // =============================================================================================================================
        //                                                    PIN CODE BUTTON CLICKED
        // =============================================================================================================================

        private void btnPinCode_Click(object sender, EventArgs e)
        {
            ForgotPinCode pinCode = new ForgotPinCode(bankingDatabase);
            pinCode.ShowDialog(); //shows ForgotPinCode form
        }

        // =============================================================================================================================
        //                                                    CANCEL BUTTON CLICKED
        // =============================================================================================================================

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); //closes current form
        }

        // =============================================================================================================================
        //                                                    FORM CLOSED
        // =============================================================================================================================
        
        private void ForgotLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInForm.Show(); //when current form is closed show previous form
        }
    }
}
