
namespace FirstMenu
{
    partial class SecurityQuestions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblQuestion1Title = new System.Windows.Forms.Label();
            this.lblQuestion2Title = new System.Windows.Forms.Label();
            this.lblQuestion3Title = new System.Windows.Forms.Label();
            this.lblQuestion1Text = new System.Windows.Forms.Label();
            this.lblQuestion2Text = new System.Windows.Forms.Label();
            this.lblQuestion3Text = new System.Windows.Forms.Label();
            this.txtAnswer1 = new System.Windows.Forms.TextBox();
            this.txtAnswer2 = new System.Windows.Forms.TextBox();
            this.txtAnswer3 = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnOK.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(435, 250);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 50);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnOK.Validating += new System.ComponentModel.CancelEventHandler(this.btnOK_Validating);
            // 
            // lblQuestion1Title
            // 
            this.lblQuestion1Title.AutoSize = true;
            this.lblQuestion1Title.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion1Title.Location = new System.Drawing.Point(20, 20);
            this.lblQuestion1Title.Name = "lblQuestion1Title";
            this.lblQuestion1Title.Size = new System.Drawing.Size(101, 23);
            this.lblQuestion1Title.TabIndex = 1;
            this.lblQuestion1Title.Text = "Question 1:";
            // 
            // lblQuestion2Title
            // 
            this.lblQuestion2Title.AutoSize = true;
            this.lblQuestion2Title.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion2Title.Location = new System.Drawing.Point(20, 100);
            this.lblQuestion2Title.Name = "lblQuestion2Title";
            this.lblQuestion2Title.Size = new System.Drawing.Size(101, 23);
            this.lblQuestion2Title.TabIndex = 2;
            this.lblQuestion2Title.Text = "Question 2:";
            // 
            // lblQuestion3Title
            // 
            this.lblQuestion3Title.AutoSize = true;
            this.lblQuestion3Title.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion3Title.Location = new System.Drawing.Point(20, 180);
            this.lblQuestion3Title.Name = "lblQuestion3Title";
            this.lblQuestion3Title.Size = new System.Drawing.Size(101, 23);
            this.lblQuestion3Title.TabIndex = 3;
            this.lblQuestion3Title.Text = "Question 3:";
            // 
            // lblQuestion1Text
            // 
            this.lblQuestion1Text.AutoSize = true;
            this.lblQuestion1Text.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion1Text.Location = new System.Drawing.Point(130, 20);
            this.lblQuestion1Text.Name = "lblQuestion1Text";
            this.lblQuestion1Text.Size = new System.Drawing.Size(0, 23);
            this.lblQuestion1Text.TabIndex = 4;
            // 
            // lblQuestion2Text
            // 
            this.lblQuestion2Text.AutoSize = true;
            this.lblQuestion2Text.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion2Text.Location = new System.Drawing.Point(130, 100);
            this.lblQuestion2Text.Name = "lblQuestion2Text";
            this.lblQuestion2Text.Size = new System.Drawing.Size(0, 23);
            this.lblQuestion2Text.TabIndex = 5;
            // 
            // lblQuestion3Text
            // 
            this.lblQuestion3Text.AutoSize = true;
            this.lblQuestion3Text.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion3Text.Location = new System.Drawing.Point(130, 180);
            this.lblQuestion3Text.Name = "lblQuestion3Text";
            this.lblQuestion3Text.Size = new System.Drawing.Size(0, 23);
            this.lblQuestion3Text.TabIndex = 6;
            // 
            // txtAnswer1
            // 
            this.txtAnswer1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnswer1.Location = new System.Drawing.Point(130, 50);
            this.txtAnswer1.Name = "txtAnswer1";
            this.txtAnswer1.Size = new System.Drawing.Size(455, 30);
            this.txtAnswer1.TabIndex = 0;
            this.txtAnswer1.Validating += new System.ComponentModel.CancelEventHandler(this.txtAnswer1_Validating);
            // 
            // txtAnswer2
            // 
            this.txtAnswer2.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnswer2.Location = new System.Drawing.Point(130, 130);
            this.txtAnswer2.Name = "txtAnswer2";
            this.txtAnswer2.Size = new System.Drawing.Size(455, 30);
            this.txtAnswer2.TabIndex = 8;
            this.txtAnswer2.Validating += new System.ComponentModel.CancelEventHandler(this.txtAnswer2_Validating);
            // 
            // txtAnswer3
            // 
            this.txtAnswer3.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnswer3.Location = new System.Drawing.Point(130, 210);
            this.txtAnswer3.Name = "txtAnswer3";
            this.txtAnswer3.Size = new System.Drawing.Size(455, 30);
            this.txtAnswer3.TabIndex = 9;
            this.txtAnswer3.Validating += new System.ComponentModel.CancelEventHandler(this.txtAnswer3_Validating);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(20, 250);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 50);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // SecurityQuestions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(609, 311);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtAnswer3);
            this.Controls.Add(this.txtAnswer2);
            this.Controls.Add(this.txtAnswer1);
            this.Controls.Add(this.lblQuestion3Text);
            this.Controls.Add(this.lblQuestion2Text);
            this.Controls.Add(this.lblQuestion1Text);
            this.Controls.Add(this.lblQuestion3Title);
            this.Controls.Add(this.lblQuestion2Title);
            this.Controls.Add(this.lblQuestion1Title);
            this.Controls.Add(this.btnOK);
            this.Name = "SecurityQuestions";
            this.Text = "SecurityQuestions";
            this.Load += new System.EventHandler(this.SecurityQuestions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblQuestion1Title;
        private System.Windows.Forms.Label lblQuestion2Title;
        private System.Windows.Forms.Label lblQuestion3Title;
        private System.Windows.Forms.Label lblQuestion1Text;
        private System.Windows.Forms.Label lblQuestion2Text;
        private System.Windows.Forms.Label lblQuestion3Text;
        private System.Windows.Forms.TextBox txtAnswer1;
        private System.Windows.Forms.TextBox txtAnswer2;
        private System.Windows.Forms.TextBox txtAnswer3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}