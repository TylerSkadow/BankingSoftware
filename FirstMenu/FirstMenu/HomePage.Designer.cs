
namespace FirstMenu
{
    partial class HomePage
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnStatement = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.panelOverviewTotal = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblGreetings = new System.Windows.Forms.Label();
            this.panelOverview = new System.Windows.Forms.Panel();
            this.lblOverview = new System.Windows.Forms.Label();
            this.panelAccountHeader = new System.Windows.Forms.Panel();
            this.lblAccountHeader = new System.Windows.Forms.Label();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.panelFooter.SuspendLayout();
            this.panelOverviewTotal.SuspendLayout();
            this.panelOverview.SuspendLayout();
            this.panelAccountHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Comic Sans MS", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(484, 96);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Tylanni Bank";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(280, 32);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(0, 23);
            this.lblTime.TabIndex = 0;
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("Comic Sans MS", 6.25F);
            this.lblCopyright.Location = new System.Drawing.Point(10, 10);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(142, 13);
            this.lblCopyright.TabIndex = 39;
            this.lblCopyright.Text = "©Copyright 2022 Tyler Skadow";
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.btnStatement);
            this.panelFooter.Controls.Add(this.btnCreate);
            this.panelFooter.Controls.Add(this.btnRefresh);
            this.panelFooter.Controls.Add(this.btnBack);
            this.panelFooter.Controls.Add(this.lblTime);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 301);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(484, 60);
            this.panelFooter.TabIndex = 40;
            // 
            // btnStatement
            // 
            this.btnStatement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnStatement.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatement.Location = new System.Drawing.Point(118, 34);
            this.btnStatement.Name = "btnStatement";
            this.btnStatement.Size = new System.Drawing.Size(100, 23);
            this.btnStatement.TabIndex = 4;
            this.btnStatement.Text = "Statements";
            this.btnStatement.UseVisualStyleBackColor = false;
            this.btnStatement.Click += new System.EventHandler(this.btnStatement_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnCreate.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(12, 34);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(100, 23);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Create Account";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnRefresh.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(118, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBack.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(12, 9);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // panelOverviewTotal
            // 
            this.panelOverviewTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panelOverviewTotal.Controls.Add(this.lblTotal);
            this.panelOverviewTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelOverviewTotal.Location = new System.Drawing.Point(10, 180);
            this.panelOverviewTotal.Name = "panelOverviewTotal";
            this.panelOverviewTotal.Size = new System.Drawing.Size(460, 50);
            this.panelOverviewTotal.TabIndex = 41;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(20, 12);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(70, 26);
            this.lblTotal.TabIndex = 43;
            this.lblTotal.Text = "Total: ";
            // 
            // lblGreetings
            // 
            this.lblGreetings.AutoSize = true;
            this.lblGreetings.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreetings.Location = new System.Drawing.Point(8, 96);
            this.lblGreetings.Name = "lblGreetings";
            this.lblGreetings.Size = new System.Drawing.Size(64, 30);
            this.lblGreetings.TabIndex = 42;
            this.lblGreetings.Text = "Hello";
            // 
            // panelOverview
            // 
            this.panelOverview.Controls.Add(this.lblOverview);
            this.panelOverview.Location = new System.Drawing.Point(10, 150);
            this.panelOverview.Name = "panelOverview";
            this.panelOverview.Size = new System.Drawing.Size(300, 30);
            this.panelOverview.TabIndex = 44;
            // 
            // lblOverview
            // 
            this.lblOverview.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.lblOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOverview.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverview.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOverview.Location = new System.Drawing.Point(0, 0);
            this.lblOverview.Name = "lblOverview";
            this.lblOverview.Size = new System.Drawing.Size(300, 30);
            this.lblOverview.TabIndex = 0;
            this.lblOverview.Text = "Overview";
            this.lblOverview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelAccountHeader
            // 
            this.panelAccountHeader.Controls.Add(this.lblAccountHeader);
            this.panelAccountHeader.Location = new System.Drawing.Point(10, 250);
            this.panelAccountHeader.Name = "panelAccountHeader";
            this.panelAccountHeader.Size = new System.Drawing.Size(300, 30);
            this.panelAccountHeader.TabIndex = 45;
            // 
            // lblAccountHeader
            // 
            this.lblAccountHeader.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.lblAccountHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAccountHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountHeader.Location = new System.Drawing.Point(0, 0);
            this.lblAccountHeader.Name = "lblAccountHeader";
            this.lblAccountHeader.Size = new System.Drawing.Size(300, 30);
            this.lblAccountHeader.TabIndex = 0;
            this.lblAccountHeader.Text = "Accounts";
            // 
            // timerUpdate
            // 
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.CancelButton = this.btnBack;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.panelOverviewTotal);
            this.Controls.Add(this.panelAccountHeader);
            this.Controls.Add(this.panelOverview);
            this.Controls.Add(this.lblGreetings);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblTitle);
            this.Name = "HomePage";
            this.Text = "HomePage";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HomePage_FormClosed);
            this.Load += new System.EventHandler(this.HomePage_Load);
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.panelOverviewTotal.ResumeLayout(false);
            this.panelOverviewTotal.PerformLayout();
            this.panelOverview.ResumeLayout(false);
            this.panelAccountHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Panel panelOverviewTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblGreetings;
        private System.Windows.Forms.Panel panelOverview;
        private System.Windows.Forms.Label lblOverview;
        private System.Windows.Forms.Panel panelAccountHeader;
        private System.Windows.Forms.Label lblAccountHeader;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnStatement;
    }
}