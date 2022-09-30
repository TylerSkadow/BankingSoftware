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
    public partial class BankingSoftware : Form
    {
        OpenFileDialog databaseFile;
        String database;

        public BankingSoftware()
        {
            InitializeComponent();

            timer.Enabled = true; //sets timer up and makes it change every minute
            timer.Interval = 1000;
        }

        // =============================================================================================================================
        //                                                    FORM LOAD
        // =============================================================================================================================

        private void FirstForm_Load(object sender, EventArgs e)
        {
            OpenFileDialog openDatabase = new OpenFileDialog();
            openDatabase.Filter = "Access files (*.accdb)|*.accdb|All files (*.*)|*.*";
            //allows user to select the correct database

            bool exit = false;
            do
            {
                if (openDatabase.ShowDialog() == DialogResult.OK)
                {
                    if (openDatabase.FileName.Contains("Banking.accdb"))
                    {
                        databaseFile = openDatabase;
                        database = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + databaseFile.FileName + ";Persist Security Info=False;";
                        //sets the string to the full line to connect to the database to make it easier to input later
                    }
                    else
                    {
                        MessageBox.Show("Wrong database, must choose 'Banking.accdb'");
                    }
                }
                else
                {
                    //if they dont open a database
                    exit = true;
                }
            }
            while (!openDatabase.FileName.Contains("Banking.accdb") && !exit);
            
            if (exit)
            {
                //closes if they do not open a database
                this.Close();
            }
        }

        // =============================================================================================================================
        //                                                    OPEN ACCOUNT BUTTON CLICKED
        // =============================================================================================================================

        private void btnOpenAccount_Click(object sender, EventArgs e)
        {
            OpenAccount openAccountForm = new OpenAccount(this, database);
            openAccountForm.Show();
        }

        // =============================================================================================================================
        //                                                    LOG IN BUTTON CLICKED
        // =============================================================================================================================

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LogIn logInForm = new LogIn(this, database);
            logInForm.Show();
        }

        // =============================================================================================================================
        //                                                    EXIT BUTTON CLICKED
        // =============================================================================================================================

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =============================================================================================================================
        //                                                    TIMER TICK
        // =============================================================================================================================

        private void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("g");
        }
    }
}
