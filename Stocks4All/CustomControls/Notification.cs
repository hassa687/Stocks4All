using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Abhinaba.TransDlg;

namespace DiffuseDlgDemo
{
	public class Notification : TransDialog
	{
        #region Ctor, init code and dispose
		public Notification()
            : base(true)
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
        #endregion // Ctor and init code

        #region Event handler
        private void Notification_Load(object sender, System.EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Left = screenWidth - this.Width;
            this.Top = screenHeight - this.Height;

            timer1.Enabled = true;

            string link = "http://www.geocities.com/basuabhinaba";
            //linkLabel1.Links.Add(0, link.Length, link);
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            string link = e.Link.LinkData.ToString();
            if (link != null && link.Length > 0)
                System.Diagnostics.Process.Start(link);
        }
        #endregion // Event handler
        
        #region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      this.label1 = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(82, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(168, 52);
      this.label1.TabIndex = 0;
      this.label1.Text = "You\'ve got email";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::Stocks4All.Properties.Resources.sfa_logo_sml;
      this.pictureBox1.Location = new System.Drawing.Point(-37, -35);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(113, 109);
      this.pictureBox1.TabIndex = 1;
      this.pictureBox1.TabStop = false;
      // 
      // timer1
      // 
      this.timer1.Interval = 3000;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // Notification
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(262, 70);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "Notification";
      this.Text = "Stocks4All Notification";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.Notification_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);

        }
		#endregion

    public Label label1;

        #region Designer generated variables

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.IContainer components;
        #endregion

	}
}
