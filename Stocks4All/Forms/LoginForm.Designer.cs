namespace Stocks4All.Forms
{
  partial class LoginForm
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
      this.loginBackgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.loginButton = new System.Windows.Forms.Button();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.label3 = new System.Windows.Forms.Label();
      this.resultLabel = new System.Windows.Forms.Label();
      this.cancalButton = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.passwordtextBox = new System.Windows.Forms.TextBox();
      this.usernameTextBox = new System.Windows.Forms.TextBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.linkLabel2 = new System.Windows.Forms.LinkLabel();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // loginBackgroundWorker
      // 
      this.loginBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.loginBackgroundWorker_DoWork);
      this.loginBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.loginBackgroundWorker_RunWorkerCompleted);
      // 
      // loginButton
      // 
      this.loginButton.Enabled = false;
      this.loginButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.loginButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.loginButton.Location = new System.Drawing.Point(363, 266);
      this.loginButton.Name = "loginButton";
      this.loginButton.Size = new System.Drawing.Size(66, 32);
      this.loginButton.TabIndex = 5;
      this.loginButton.Text = "Login";
      this.loginButton.UseVisualStyleBackColor = true;
      this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.linkLabel1.Location = new System.Drawing.Point(376, 101);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(123, 19);
      this.linkLabel1.TabIndex = 11;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Contact Support";
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(189, 2);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(185, 19);
      this.label3.TabIndex = 8;
      this.label3.Text = "Stocks4All v1.2(Beta)";
      // 
      // resultLabel
      // 
      this.resultLabel.AutoSize = true;
      this.resultLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.resultLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.resultLabel.ForeColor = System.Drawing.Color.Maroon;
      this.resultLabel.Location = new System.Drawing.Point(12, 276);
      this.resultLabel.Name = "resultLabel";
      this.resultLabel.Size = new System.Drawing.Size(292, 16);
      this.resultLabel.TabIndex = 7;
      this.resultLabel.Text = "Login Failed, Please confirm username  password";
      this.resultLabel.Visible = false;
      // 
      // cancalButton
      // 
      this.cancalButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cancalButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cancalButton.Location = new System.Drawing.Point(435, 266);
      this.cancalButton.Name = "cancalButton";
      this.cancalButton.Size = new System.Drawing.Size(78, 32);
      this.cancalButton.TabIndex = 6;
      this.cancalButton.Text = "Cancel";
      this.cancalButton.UseVisualStyleBackColor = true;
      this.cancalButton.Click += new System.EventHandler(this.cancalButton_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(13, 215);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(227, 19);
      this.label2.TabIndex = 3;
      this.label2.Text = "Robinhood User Password:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(39, 159);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(196, 19);
      this.label1.TabIndex = 2;
      this.label1.Text = "Robinhood User Name:";
      // 
      // passwordtextBox
      // 
      this.passwordtextBox.Location = new System.Drawing.Point(241, 212);
      this.passwordtextBox.Name = "passwordtextBox";
      this.passwordtextBox.PasswordChar = '*';
      this.passwordtextBox.Size = new System.Drawing.Size(258, 27);
      this.passwordtextBox.TabIndex = 1;
      this.passwordtextBox.TextChanged += new System.EventHandler(this.passwordtextBox_TextChanged);
      // 
      // usernameTextBox
      // 
      this.usernameTextBox.Location = new System.Drawing.Point(241, 156);
      this.usernameTextBox.Name = "usernameTextBox";
      this.usernameTextBox.Size = new System.Drawing.Size(258, 27);
      this.usernameTextBox.TabIndex = 0;
      this.usernameTextBox.TextChanged += new System.EventHandler(this.usernameTextBox_TextChanged);
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::Stocks4All.Properties.Resources.sfa_logo_sml;
      this.pictureBox1.InitialImage = global::Stocks4All.Properties.Resources.sfa_logo_lrg;
      this.pictureBox1.Location = new System.Drawing.Point(209, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(117, 108);
      this.pictureBox1.TabIndex = 4;
      this.pictureBox1.TabStop = false;
      // 
      // linkLabel2
      // 
      this.linkLabel2.AutoSize = true;
      this.linkLabel2.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.linkLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.linkLabel2.Location = new System.Drawing.Point(384, 186);
      this.linkLabel2.Name = "linkLabel2";
      this.linkLabel2.Size = new System.Drawing.Size(115, 13);
      this.linkLabel2.TabIndex = 12;
      this.linkLabel2.TabStop = true;
      this.linkLabel2.Text = "What is my username?";
      this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
      // 
      // LoginForm
      // 
      this.AcceptButton = this.loginButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(525, 310);
      this.Controls.Add(this.linkLabel2);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.resultLabel);
      this.Controls.Add(this.cancalButton);
      this.Controls.Add(this.loginButton);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.passwordtextBox);
      this.Controls.Add(this.usernameTextBox);
      this.Controls.Add(this.pictureBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.Name = "LoginForm";
      this.Text = "Login";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.PictureBox pictureBox1;
    public System.Windows.Forms.TextBox usernameTextBox;
    public System.Windows.Forms.TextBox passwordtextBox;
    private System.Windows.Forms.Button loginButton;
    private System.Windows.Forms.Button cancalButton;
    private System.ComponentModel.BackgroundWorker loginBackgroundWorker;
    private System.Windows.Forms.Label resultLabel;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.LinkLabel linkLabel2;
  }
}