using Stocks4All.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Stocks4All.Forms
{
  public partial class LoginForm : BaseForm
  {
    public bool cancelFlag;
    public bool loginFlag;
    private static SHA1 sha = new SHA1CryptoServiceProvider();
    public LoginForm()
    {
      InitializeComponent();
      loginFlag = cancelFlag = false;
      
    }

    private void loginButton_Click(object sender, EventArgs e)
    {
      resultLabel.Visible = false;
      resultLabel.Text = "";

      if (!loginBackgroundWorker.IsBusy)
      {
        loginBackgroundWorker.RunWorkerAsync(
          new KeyValuePair<string, string>
            (this.usernameTextBox.Text, this.passwordtextBox.Text));
      }
     
    }

    private void cancalButton_Click(object sender, EventArgs e)
    {
      cancelFlag = true;
      this.Close();
    }

    private void loginBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      KeyValuePair<string, string> creden = (KeyValuePair<string, string>)e.Argument;
      try
      {
        Robinhood.Init(creden.Key, creden.Value);
        e.Result = true;
      }
      catch(Exception ex)
      {
        resultLabel.BeginInvoke((Action)delegate
        {
          resultLabel.Text = "Login Failed, Please confirm username  password";
        });
        MessageBox.Show(ex.ToString());
        e.Result = false;
      }
    }

    private void loginBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      bool success = (bool)e.Result;
      if (success)
      {
        loginFlag = true;
        this.Close();
      }
      else
      {
        resultLabel.Visible = true;
      }
    }

    private void usernameTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(usernameTextBox.Text)
        && !string.IsNullOrEmpty(passwordtextBox.Text))
        loginButton.Enabled = true;
      else
        loginButton.Enabled = false;

      resultLabel.Visible = false;
    }

    private void passwordtextBox_TextChanged(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(usernameTextBox.Text)
        && !string.IsNullOrEmpty(passwordtextBox.Text))
        loginButton.Enabled = true;
      else
        loginButton.Enabled = false;
      
      resultLabel.Visible = false;
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("http://meethassan.net/contact-me/");
    }

    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("https://support.robinhood.com/hc/en-us/articles/203585575-Forgot-Username-or-Password");
    }
  }
}
