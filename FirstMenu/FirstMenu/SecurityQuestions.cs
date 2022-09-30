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
    public partial class SecurityQuestions : Form
    {
        private String bankingDatabase;
        private int routing;
        private OleDbConnection bankingDatabaseConnection = new OleDbConnection();
        private Security security = new Security();

        public SecurityQuestions(String databaseFile, int routingNum)
        {
            this.bankingDatabase = databaseFile; //database file
            this.routing = routingNum; //routing number of account

            InitializeComponent();

            //sets up the connection to the database
            bankingDatabaseConnection.ConnectionString = databaseFile;
        }

        // =============================================================================================================================
        //                                                    SECURITY QUESTIONS FORM LOAD
        // =============================================================================================================================

        private void SecurityQuestions_Load(object sender, EventArgs e)
        {
            bankingDatabaseConnection.Open();
            OleDbCommand getQuestions = new OleDbCommand();
            getQuestions.Connection = bankingDatabaseConnection;
            getQuestions.CommandText =
                "SELECT * from Banking where [Routing Number]=" + routing + "";
            //finds account with routing number

            OleDbDataReader reader = getQuestions.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count++; //if found

                if (count == 1)
                {
                    //sets the questions to what is chosen by the user at account creation
                    lblQuestion1Text.Text = reader["Security Question 1"].ToString();
                    lblQuestion2Text.Text = reader["Security Question 2"].ToString();
                    lblQuestion3Text.Text = reader["Security Question 3"].ToString();
                }
            }
            bankingDatabaseConnection.Close();
        }

        // =============================================================================================================================
        //                                                    ANSWER 1 VALIDATING
        // =============================================================================================================================

        private void txtAnswer1_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtAnswer1, "");

            if (txtAnswer1.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtAnswer1, "Answer required");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    ANSWER 2 VALIDATING
        // =============================================================================================================================

        private void txtAnswer2_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtAnswer2, "");

            if (txtAnswer2.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtAnswer2, "Answer required");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    ANSWER 3 VALIDATING
        // =============================================================================================================================

        private void txtAnswer3_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtAnswer3, "");

            if (txtAnswer3.Text == "") //if empty
            {
                e.Cancel = true;
                errorProvider.SetError(txtAnswer3, "Answer rquired");
                return;
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
        //                                                    OK BUTTON VALIDATING
        // =============================================================================================================================

        private void btnOK_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(btnOK, "");

            String answer1Salt = "", answer2Salt = "", answer3Salt = "";
            String answer1Hash, answer2Hash, answer3Hash;

            bankingDatabaseConnection.Open();
            OleDbCommand getSalts = new OleDbCommand();
            getSalts.Connection = bankingDatabaseConnection;
            getSalts.CommandText =
                "SELECT * from Banking where [Routing Number]=" + routing + "";
            //gets account with routing number

            OleDbDataReader readSalts = getSalts.ExecuteReader();
            int count = 0;
            while (readSalts.Read())
            {
                count++;
                if (count == 1)
                {
                    //gets the salts for each answer
                    answer1Salt = readSalts["Security Answer 1(Salt)"].ToString();
                    answer2Salt = readSalts["Security Answer 2(Salt)"].ToString();
                    answer3Salt = readSalts["Security Answer 3(Salt)"].ToString();
                }
            }

            //hashes each answer with the found salt
            answer1Hash = security.Hash(txtAnswer1.Text, answer1Salt);
            answer2Hash = security.Hash(txtAnswer2.Text, answer2Salt);
            answer3Hash = security.Hash(txtAnswer3.Text, answer3Salt);

            OleDbCommand checkAnswers = new OleDbCommand();
            checkAnswers.Connection = bankingDatabaseConnection;
            checkAnswers.CommandText =
                "SELECT * from Banking where [Routing Number]=" + routing +
                " and [Security Answer 1(Hash)]='" + answer1Hash +
                "' and [Security Answer 2(Hash)]='" + answer2Hash +
                "' and [Security Answer 3(Hash)]='" + answer3Hash + "'";
            //find where the routing number and all answers are correct

            OleDbDataReader readCheck = checkAnswers.ExecuteReader();
            count = 0;
            while (readCheck.Read())
            {
                count++; //if found
            }
            bankingDatabaseConnection.Close();

            if (count == 0) //if answers are wrong
            {
                e.Cancel = true;
                errorProvider.SetError(btnOK, "Answer(s) are incorrect");
                return;
            }
        }

        // =============================================================================================================================
        //                                                    OK BUTTON CLICKED
        // =============================================================================================================================

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren()) //if all is correct
            {
                MessageBox.Show("Authorized!");
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
