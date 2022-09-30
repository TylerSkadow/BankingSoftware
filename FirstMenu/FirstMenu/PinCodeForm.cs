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
    public partial class PinCodeForm : Form
    {
        private Random random = new Random(); //random number generator
        private List<Button> pinCodeBtns = new List<Button>(); //list of buttons to press
        public PinCodeForm()
        {
            InitializeComponent();
        }

        // =============================================================================================================================
        //                                                    FORM LOAD
        // =============================================================================================================================

        private void PinCodeForm_Load(object sender, EventArgs e)
        {
            int counter = 0; //counter of how many buttons have been made
            List<int> pinCode = new List<int>(); //list of possible numbers
            
            //while there is not 10 in counter
            while(counter != 10)
            {
                int randomNum = random.Next(0, 10); //makes a random number from 1 to 10

                //if the list of numbers doesnt contain the random number
                if (!pinCode.Contains(randomNum))
                {
                    pinCode.Add(randomNum); //adds the random number to the list
                    counter++; //adds to the counter
                }
            }

            counter = 0; //resets to 0

            //where the buttons are located
            int buttonY = 100; 
            int buttonX = 0;
            
            bool ifOK = false; //if the ok button is put down
            bool ifCancel = false; //if the cancel button is put down

            //a loop for vertical buttons
            for (int x = 0; x < 4; x++)
            {
                buttonX = 30; //where each row is is

                //a loop for horizontal buttons
                for (int y = 0; y < 3; y++)
                {
                    Button randomPinCode = new Button(); //makes a new button

                    //if the counter is 9 and the ok button has not been placed
                    if (counter == 9 && ifOK == false)
                    {
                        randomPinCode.Text = "O";
                        randomPinCode.BackColor = System.Drawing.Color.Green;
                        randomPinCode.Click += new System.EventHandler(this.buttonO_Click);
                        ifOK = true;
                        counter--;
                    }

                    //if the counter is 10 and the cancel button has not been placed
                    else if (counter == 10 && ifCancel == false)
                    {
                        randomPinCode.Text = "X";
                        randomPinCode.BackColor = System.Drawing.Color.Red;
                        randomPinCode.Click += new System.EventHandler(this.buttonX_Click);
                        ifCancel = true;
                    }

                    //if either one doesnt apply then place the random number on the button
                    else
                    {
                        randomPinCode.Text = pinCode[counter].ToString();
                        randomPinCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
                        randomPinCode.Click += new System.EventHandler(this.buttonRandom_Click);
                    }

                    //sets the style for each button
                    randomPinCode.Location = new System.Drawing.Point(buttonX, buttonY);
                    randomPinCode.Size = new System.Drawing.Size(50, 50);
                    randomPinCode.Font = new System.Drawing.Font("Comic Sans MS", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    
                    //adds button to list of buttons and to the form
                    pinCodeBtns.Add(randomPinCode);
                    this.Controls.Add(randomPinCode);
                    
                    buttonX += 60; //moves the button over the right
                    counter++; //adds to the counter
                }
                buttonY += 60; //moves the button down
            }
        }

        // =============================================================================================================================
        //                                                    RANDOM BUTTON CLICK
        // =============================================================================================================================

        private void buttonRandom_Click(object sender, EventArgs e)
        {
            Button clickedBtn = (Button)sender; //the clicked button
            int num = int.Parse(clickedBtn.Text); //gets the text from the button

            //if the length of the pin code is less than 4 then add to string
            if (txtPinCode.Text.Length < 4)
            {
                txtPinCode.Text += num;
            }
        }

        // =============================================================================================================================
        //                                                    OK BUTTON CLICK
        // =============================================================================================================================

        private void buttonO_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        // =============================================================================================================================
        //                                                    CANCEL BUTTON CLICK
        // =============================================================================================================================

        private void buttonX_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            
        }

        // =============================================================================================================================
        //                                                    BACKSPACE BUTTON CLICK
        // =============================================================================================================================

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            String text = txtPinCode.Text;
            if(text != null && text.Length > 0)
            {
                text = text.Substring(0, text.Length - 1);
            }
            txtPinCode.Text = text;
        }

        // =============================================================================================================================
        //                                                    PIN CODE FUNCTION
        // =============================================================================================================================

        public int InputPinCode
        {
            get
            {
                return int.Parse(txtPinCode.Text);
            }
            set
            {
                txtPinCode.Text = value.ToString();
            }
        }
       
    }
}
