using Yolo.CustomControls;
namespace Stocks4All.CustomControls
{
  partial class StopLossControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
      this.numericUpDownValue = new Yolo.CustomControls.CustomNumericUpDown();
      this.comboBoxOrderType = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.groupBoxName = new System.Windows.Forms.GroupBox();
      this.label4 = new System.Windows.Forms.Label();
      this.numericUpDownNoOfShares = new Yolo.CustomControls.CustomNumericUpDown();
      this.label3 = new System.Windows.Forms.Label();
      this.comboBoxExecution = new System.Windows.Forms.ComboBox();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValue)).BeginInit();
      this.groupBoxName.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoOfShares)).BeginInit();
      this.SuspendLayout();
      // 
      // numericUpDownValue
      // 
      this.numericUpDownValue.DecimalPlaces = 2;
      this.numericUpDownValue.Location = new System.Drawing.Point(49, 14);
      this.numericUpDownValue.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
      this.numericUpDownValue.Name = "numericUpDownValue";
      this.numericUpDownValue.Size = new System.Drawing.Size(86, 20);
      this.numericUpDownValue.TabIndex = 0;
      this.numericUpDownValue.ValueChanged += new System.EventHandler(this.numericUpDownValue_ValueChanged);
      // 
      // comboBoxOrderType
      // 
      this.comboBoxOrderType.Enabled = false;
      this.comboBoxOrderType.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.comboBoxOrderType.FormattingEnabled = true;
      this.comboBoxOrderType.Location = new System.Drawing.Point(192, 13);
      this.comboBoxOrderType.Name = "comboBoxOrderType";
      this.comboBoxOrderType.Size = new System.Drawing.Size(105, 21);
      this.comboBoxOrderType.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label1.Location = new System.Drawing.Point(6, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(37, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Value:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(151, 16);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(34, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Type:";
      // 
      // groupBoxName
      // 
      this.groupBoxName.AutoSize = true;
      this.groupBoxName.Controls.Add(this.label4);
      this.groupBoxName.Controls.Add(this.numericUpDownNoOfShares);
      this.groupBoxName.Controls.Add(this.label3);
      this.groupBoxName.Controls.Add(this.comboBoxExecution);
      this.groupBoxName.Controls.Add(this.label1);
      this.groupBoxName.Controls.Add(this.label2);
      this.groupBoxName.Controls.Add(this.numericUpDownValue);
      this.groupBoxName.Controls.Add(this.comboBoxOrderType);
      this.groupBoxName.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.groupBoxName.Location = new System.Drawing.Point(3, 3);
      this.groupBoxName.Name = "groupBoxName";
      this.groupBoxName.Size = new System.Drawing.Size(791, 53);
      this.groupBoxName.TabIndex = 4;
      this.groupBoxName.TabStop = false;
      this.groupBoxName.Text = "Name";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label4.Location = new System.Drawing.Point(477, 16);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(72, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "No of Shares:";
      // 
      // numericUpDownNoOfShares
      // 
      this.numericUpDownNoOfShares.Enabled = false;
      this.numericUpDownNoOfShares.Location = new System.Drawing.Point(555, 13);
      this.numericUpDownNoOfShares.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
      this.numericUpDownNoOfShares.Name = "numericUpDownNoOfShares";
      this.numericUpDownNoOfShares.Size = new System.Drawing.Size(86, 20);
      this.numericUpDownNoOfShares.TabIndex = 6;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(303, 16);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(57, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Execution:";
      // 
      // comboBoxExecution
      // 
      this.comboBoxExecution.Enabled = false;
      this.comboBoxExecution.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.comboBoxExecution.FormattingEnabled = true;
      this.comboBoxExecution.Location = new System.Drawing.Point(366, 13);
      this.comboBoxExecution.Name = "comboBoxExecution";
      this.comboBoxExecution.Size = new System.Drawing.Size(105, 21);
      this.comboBoxExecution.TabIndex = 4;
      // 
      // StopLossControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Controls.Add(this.groupBoxName);
      this.Name = "StopLossControl";
      this.Size = new System.Drawing.Size(797, 59);
      this.Load += new System.EventHandler(this.PricePointControl_Load);
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValue)).EndInit();
      this.groupBoxName.ResumeLayout(false);
      this.groupBoxName.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoOfShares)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.ComponentModel.BackgroundWorker backgroundWorker1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.GroupBox groupBoxName;
    public CustomNumericUpDown numericUpDownValue;
    public System.Windows.Forms.ComboBox comboBoxOrderType;
    private System.Windows.Forms.Label label3;
    public System.Windows.Forms.ComboBox comboBoxExecution;
    private System.Windows.Forms.Label label4;
    public CustomNumericUpDown numericUpDownNoOfShares;
  }
}
