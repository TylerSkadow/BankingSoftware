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
    public partial class HomePage : Form
    {
        private LogIn LogInForm;
        private String bankingDataBase;
        private List<Account> accounts = new List<Account>();
        private Account selectedAccount;
        private OleDbConnection bankingDatabaseConnection = new OleDbConnection();
        private List<Panel> panels = new List<Panel>();

        public HomePage(LogIn parentForm, String databaseFile, List<Account> loggedInAccounts)
        {
            this.LogInForm = parentForm; //log in form set as parent form
            this.bankingDataBase = databaseFile; //database file
            this.accounts = loggedInAccounts; //list of accounts

            InitializeComponent();

            parentForm.Hide(); //hides the log in form

            //current time timer
            timer.Enabled = true;
            timer.Interval = 1000;
            
            //refresh timer
            timerUpdate.Enabled = true;
            timerUpdate.Interval = 60000;

            //sets up the connection to the database
            bankingDatabaseConnection.ConnectionString = databaseFile;

            //sets first name to the FirstName in the accounts list
            String firstName = accounts[0].FirstName;
            String greetingMessage = "";
            
            //current time set as hours and minutes
            String currentTime = DateTime.Now.ToString("HH:mm");

            //set times for when each time period starts
            String morning = "04:00"; //4:00am - 11:59am
            String afternoon = "12:00"; //12:00pm - 5:59pm
            String evening = "18:00"; //6:00pm - 9:59pm
            String night = "22:00"; //10:00pm - 3:59am
            
            //if current time is greater than morning but less than afternoon
            if (currentTime.CompareTo(morning) >= 0 && currentTime.CompareTo(afternoon) == -1) 
            {
                greetingMessage = "Good Morning, " + firstName;
            }
            //if current time is greater than afternoon but less than evening
            else if (currentTime.CompareTo(afternoon) >= 0 && currentTime.CompareTo(evening) == -1)
            {
                greetingMessage = "Good Afternoon, " + firstName;
            }
            //if current time is greater than evening but less than night
            else if (currentTime.CompareTo(evening) >= 0 && currentTime.CompareTo(night) == -1)
            {
                greetingMessage = "Good Evening, " + firstName;
            }
            //if none other are true then it is night
            else
            {
                greetingMessage = "Goodnight, " + firstName;
            }

            //sets the label to what is the current time period with first name
            lblGreetings.Text = greetingMessage;
        }

        // =============================================================================================================================
        //                                                    HOME PAGE LOAD
        // =============================================================================================================================
        
        private void HomePage_Load(object sender, EventArgs e)
        {
            update(); //calls update function
        }

        // =============================================================================================================================
        //                                                    UPDATE FUNCTION
        // =============================================================================================================================

        private void update()
        {
            //deletes panels on form
            foreach (Panel delpanel in panels)
            {
                this.Controls.Remove(delpanel);
            }
            panels.Clear(); //clears the panels list
            int size = 360; //starting size with no panels
            int location = 280; //where first panel starts
            double total = 0; //total balance

            //loop for each account
            foreach (Account account in accounts)
            {
                //new panel with attributes along with it
                Panel accountPanel = new Panel();
                accountPanel.BackColor = System.Drawing.Color.FromArgb
                    (((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
                accountPanel.Location = new System.Drawing.Point(10, location);
                accountPanel.Size = new System.Drawing.Size(460, 75);
                accountPanel.Name = account.RoutingNum; //sets the routing number as the panel name
                accountPanel.Click += new System.EventHandler(this.accountPanel_Click);

                //new account title label with attributes along with it
                Label accountLabel = new Label();
                accountLabel.Font = new System.Drawing.Font
                    ("Comic Sans MS", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                accountLabel.Location = new System.Drawing.Point(20, 12);
                accountLabel.AutoSize = true;
                String routing = account.RoutingNum;
                accountLabel.Text = account.AccountType + " (..." + account.RoutingNum.Substring(5) + ")";

                //new balance label with attributes along with it
                Label accountBalance = new Label();
                accountBalance.Font = new System.Drawing.Font
                    ("Comic Sans MS", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                accountBalance.Location = new System.Drawing.Point(300, 35);


                bankingDatabaseConnection.Open();
                OleDbCommand showBalance = new OleDbCommand();
                showBalance.Connection = bankingDatabaseConnection;
                showBalance.CommandText = "SELECT * from Banking where [Routing Number]=" + account.RoutingNum + "";
                //finds routing number in database

                OleDbDataReader reader = showBalance.ExecuteReader();
                int count = 0;
                double balance = 0.00;
                while (reader.Read())
                {
                    count++; //if found
                    if (count == 1)
                    {
                        balance = double.Parse(reader["Balance"].ToString()); //gets balance
                        total += balance; //adds balance to total
                        count--; //resets count to 0
                        lblTotal.Text = "Total: " + total.ToString("C"); //sets label to total in currency form
                    }
                }
                accountBalance.Text = balance.ToString("C"); //balance found into currency form
                bankingDatabaseConnection.Close();

                //sets avalible label with attributes along with it
                Label avaliable = new Label();
                avaliable.Font = new System.Drawing.Font
                    ("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                avaliable.Location = new System.Drawing.Point(300, 20);
                avaliable.Text = "Avaliable Balance";

                accountPanel.Controls.Add(accountLabel); //adds account title label to panel
                accountPanel.Controls.Add(accountBalance); //adds account balance label to panel
                accountPanel.Controls.Add(avaliable); //adds avalible text label to panel
                panels.Add(accountPanel); //adds current panel to the list of panels
                this.Controls.Add(accountPanel); //adds current panel to form
                location += 75; //moves the location
                size += 75; //makes form bigger
            }
            this.ClientSize = new System.Drawing.Size(485, size); //sets form size
        }

        // =============================================================================================================================
        //                                                    ACCOUNT PANEL CLICKED
        // =============================================================================================================================

        private void accountPanel_Click(object sender, EventArgs e)
        {
            bankingDatabaseConnection.Open();
            Panel account = (Panel)sender; //panel clicked on
            OleDbCommand clickedAccount = new OleDbCommand();
            clickedAccount.Connection = bankingDatabaseConnection;
            clickedAccount.CommandText =
                "SELECT * from Banking where [Routing Number]= " + int.Parse(account.Name) + "";
            //finds routing number in database

            OleDbDataReader reader = clickedAccount.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count++; //if found

                if (count == 1)
                {
                    //makes new account with info found
                    Account newAccount = new Account(
                            reader["First Name"].ToString(),
                            reader["Middle Initial"].ToString(),
                            reader["Last Name"].ToString(),
                            reader["Suffix"].ToString(),
                            reader["Email"].ToString(),
                            reader["Phone Number"].ToString(),
                            reader["SSN(Salt)"].ToString(),
                            reader["SSN(Hash)"].ToString(),
                            reader["Date of Birth"].ToString(),
                            reader["Street"].ToString(),
                            reader["State"].ToString(),
                            reader["County"].ToString(),
                            reader["Zip Code"].ToString(),
                            reader["Username"].ToString(),
                            reader["Password(Salt)"].ToString(),
                            reader["Password(Hash)"].ToString(),
                            reader["Account Type"].ToString(),
                            reader["Account Pin Code(Salt)"].ToString(),
                            reader["Account Pin Code(Hash)"].ToString(),
                            reader["Routing Number"].ToString(),
                            reader["Security Question 1"].ToString(),
                            reader["Security Answer 1(Salt)"].ToString(),
                            reader["Security Answer 1(Hash)"].ToString(),
                            reader["Security Question 2"].ToString(),
                            reader["Security Answer 2(Salt)"].ToString(),
                            reader["Security Answer 2(Hash)"].ToString(),
                            reader["Security Question 3"].ToString(),
                            reader["Security Answer 3(Salt)"].ToString(),
                            reader["Security Answer 3(Hash)"].ToString());

                    selectedAccount = newAccount; //sets new account to the selected account
                }
            }
            bankingDatabaseConnection.Close();

            //shows account form
            AccountForm accountform = new AccountForm(this, bankingDataBase, selectedAccount, accounts);
            accountform.Show();
        }

        // =============================================================================================================================
        //                                                    TIMER TICK
        // =============================================================================================================================

        private void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("g");
        }

        // =============================================================================================================================
        //                                                    TIMER UPDATE TICK
        // =============================================================================================================================

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            update();
        }

        // =============================================================================================================================
        //                                                    BACK BUTTON CLICKED
        // =============================================================================================================================

        private void btnBack_Click(object sender, EventArgs e)
        {
            bankingDatabaseConnection.Close();
            this.Close();
        }

        // =============================================================================================================================
        //                                                    REFRESH BUTTON CLICKED
        // =============================================================================================================================

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            update();
        }

        // =============================================================================================================================
        //                                                    CREATE ACCONT BUTTON CLICKED
        // =============================================================================================================================

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateInternalAccountForm createInternalAccount = new CreateInternalAccountForm(bankingDataBase, accounts);
            if (createInternalAccount.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Account Created!");
                accounts = createInternalAccount.AccountsList;
            }
        }
        
        // =============================================================================================================================
        //                                                    STATEMENT BUTTON CLICKED
        // =============================================================================================================================

        private void btnStatement_Click(object sender, EventArgs e)
        {
            Statements statements = new Statements(bankingDataBase, accounts);
            statements.ShowDialog();
        }

        // =============================================================================================================================
        //                                                    HOME PAGE FORM CLOSED
        // =============================================================================================================================

        private void HomePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            LogInForm.Show();
        }
    }
}