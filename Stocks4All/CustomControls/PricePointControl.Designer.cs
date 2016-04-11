using Yolo.CustomControls;
namespace Stocks4All.CustomControls
{
  partial class PricePointControl
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
      this.comboBoxOrderType = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.groupBoxName = new System.Windows.Forms.GroupBox();
      this.panelStopPrice = new System.Windows.Forms.Panel();
      this.label8 = new System.Windows.Forms.Label();
      this.trailPrcntgPanel = new System.Windows.Forms.Panel();
      this.label7 = new System.Windows.Forms.Label();
      this.spreadValuesPanel = new System.Windows.Forms.Panel();
      this.labelExecution = new System.Windows.Forms.Label();
      this.comboBoxSpreadValues = new System.Windows.Forms.ComboBox();
      this.placeOrderButton = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.toFollowComboBox = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.triggerComboBox = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.comboBoxExecution = new System.Windows.Forms.ComboBox();
      this.totalLabel = new System.Windows.Forms.Label();
      this.stopPriceCustomNumericUpDown = new Yolo.CustomControls.CustomNumericUpDown();
      this.trailPrcntgNumericUpDown = new Yolo.CustomControls.CustomNumericUpDown();
      this.numericUpDownNoOfShares = new Yolo.CustomControls.CustomNumericUpDown();
      this.numericUpDownValue = new Yolo.CustomControls.CustomNumericUpDown();
      this.groupBoxName.SuspendLayout();
      this.panelStopPrice.SuspendLayout();
      this.trailPrcntgPanel.SuspendLayout();
      this.spreadValuesPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.stopPriceCustomNumericUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trailPrcntgNumericUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoOfShares)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValue)).BeginInit();
      this.SuspendLayout();
      // 
      // comboBoxOrderType
      // 
      this.comboBoxOrderType.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.comboBoxOrderType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.comboBoxOrderType.FormattingEnabled = true;
      this.comboBoxOrderType.Location = new System.Drawing.Point(216, 12);
      this.comboBoxOrderType.Name = "comboBoxOrderType";
      this.comboBoxOrderType.Size = new System.Drawing.Size(75, 21);
      this.comboBoxOrderType.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(23, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(43, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Price: $";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(176, 15);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(34, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Type:";
      // 
      // groupBoxName
      // 
      this.groupBoxName.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.groupBoxName.Controls.Add(this.totalLabel);
      this.groupBoxName.Controls.Add(this.panelStopPrice);
      this.groupBoxName.Controls.Add(this.trailPrcntgPanel);
      this.groupBoxName.Controls.Add(this.spreadValuesPanel);
      this.groupBoxName.Controls.Add(this.placeOrderButton);
      this.groupBoxName.Controls.Add(this.label6);
      this.groupBoxName.Controls.Add(this.toFollowComboBox);
      this.groupBoxName.Controls.Add(this.label5);
      this.groupBoxName.Controls.Add(this.triggerComboBox);
      this.groupBoxName.Controls.Add(this.label4);
      this.groupBoxName.Controls.Add(this.numericUpDownNoOfShares);
      this.groupBoxName.Controls.Add(this.label3);
      this.groupBoxName.Controls.Add(this.comboBoxExecution);
      this.groupBoxName.Controls.Add(this.label1);
      this.groupBoxName.Controls.Add(this.label2);
      this.groupBoxName.Controls.Add(this.numericUpDownValue);
      this.groupBoxName.Controls.Add(this.comboBoxOrderType);
      this.groupBoxName.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.groupBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBoxName.Location = new System.Drawing.Point(3, 3);
      this.groupBoxName.Name = "groupBoxName";
      this.groupBoxName.Size = new System.Drawing.Size(648, 131);
      this.groupBoxName.TabIndex = 4;
      this.groupBoxName.TabStop = false;
      this.groupBoxName.Text = "SSs";
      this.groupBoxName.Enter += new System.EventHandler(this.groupBoxName_Enter);
      // 
      // panelStopPrice
      // 
      this.panelStopPrice.Controls.Add(this.label8);
      this.panelStopPrice.Controls.Add(this.stopPriceCustomNumericUpDown);
      this.panelStopPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.panelStopPrice.Location = new System.Drawing.Point(255, 80);
      this.panelStopPrice.Name = "panelStopPrice";
      this.panelStopPrice.Size = new System.Drawing.Size(206, 29);
      this.panelStopPrice.TabIndex = 19;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(3, 8);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(116, 13);
      this.label8.TabIndex = 11;
      this.label8.Text = "Stop Offset From Price:";
      // 
      // trailPrcntgPanel
      // 
      this.trailPrcntgPanel.Controls.Add(this.trailPrcntgNumericUpDown);
      this.trailPrcntgPanel.Controls.Add(this.label7);
      this.trailPrcntgPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.trailPrcntgPanel.Location = new System.Drawing.Point(479, 79);
      this.trailPrcntgPanel.Name = "trailPrcntgPanel";
      this.trailPrcntgPanel.Size = new System.Drawing.Size(134, 29);
      this.trailPrcntgPanel.TabIndex = 16;
      this.trailPrcntgPanel.Visible = false;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(3, 9);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(41, 13);
      this.label7.TabIndex = 9;
      this.label7.Text = "Trail %:";
      // 
      // spreadValuesPanel
      // 
      this.spreadValuesPanel.Controls.Add(this.labelExecution);
      this.spreadValuesPanel.Controls.Add(this.comboBoxSpreadValues);
      this.spreadValuesPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.spreadValuesPanel.Location = new System.Drawing.Point(443, 45);
      this.spreadValuesPanel.Name = "spreadValuesPanel";
      this.spreadValuesPanel.Size = new System.Drawing.Size(178, 29);
      this.spreadValuesPanel.TabIndex = 15;
      this.spreadValuesPanel.Visible = false;
      // 
      // labelExecution
      // 
      this.labelExecution.AutoSize = true;
      this.labelExecution.Location = new System.Drawing.Point(3, 5);
      this.labelExecution.Name = "labelExecution";
      this.labelExecution.Size = new System.Drawing.Size(88, 13);
      this.labelExecution.TabIndex = 9;
      this.labelExecution.Text = "Spread Values: $";
      // 
      // comboBoxSpreadValues
      // 
      this.comboBoxSpreadValues.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.comboBoxSpreadValues.FormattingEnabled = true;
      this.comboBoxSpreadValues.Location = new System.Drawing.Point(97, 2);
      this.comboBoxSpreadValues.Name = "comboBoxSpreadValues";
      this.comboBoxSpreadValues.Size = new System.Drawing.Size(73, 21);
      this.comboBoxSpreadValues.TabIndex = 4;
      // 
      // placeOrderButton
      // 
      this.placeOrderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.placeOrderButton.Location = new System.Drawing.Point(545, 12);
      this.placeOrderButton.Name = "placeOrderButton";
      this.placeOrderButton.Size = new System.Drawing.Size(97, 23);
      this.placeOrderButton.TabIndex = 14;
      this.placeOrderButton.Text = "Place Order";
      this.placeOrderButton.UseVisualStyleBackColor = true;
      this.placeOrderButton.Click += new System.EventHandler(this.placeOrderButton_Click);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(13, 50);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(53, 13);
      this.label6.TabIndex = 13;
      this.label6.Text = "To follow:";
      // 
      // toFollowComboBox
      // 
      this.toFollowComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.toFollowComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toFollowComboBox.FormattingEnabled = true;
      this.toFollowComboBox.Location = new System.Drawing.Point(75, 45);
      this.toFollowComboBox.Name = "toFollowComboBox";
      this.toFollowComboBox.Size = new System.Drawing.Size(75, 21);
      this.toFollowComboBox.TabIndex = 12;
      this.toFollowComboBox.SelectedValueChanged += new System.EventHandler(this.toFollowComboBox_SelectedValueChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(314, 16);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(43, 13);
      this.label5.TabIndex = 11;
      this.label5.Text = "Trigger:";
      // 
      // triggerComboBox
      // 
      this.triggerComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.triggerComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.triggerComboBox.FormattingEnabled = true;
      this.triggerComboBox.Location = new System.Drawing.Point(363, 11);
      this.triggerComboBox.Name = "triggerComboBox";
      this.triggerComboBox.Size = new System.Drawing.Size(75, 21);
      this.triggerComboBox.TabIndex = 10;
      this.triggerComboBox.SelectedValueChanged += new System.EventHandler(this.triggerComboBox_SelectedValueChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(6, 87);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(72, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "No of Shares:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(220, 50);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(57, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Execution:";
      // 
      // comboBoxExecution
      // 
      this.comboBoxExecution.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.comboBoxExecution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.comboBoxExecution.FormattingEnabled = true;
      this.comboBoxExecution.Location = new System.Drawing.Point(283, 47);
      this.comboBoxExecution.Name = "comboBoxExecution";
      this.comboBoxExecution.Size = new System.Drawing.Size(75, 21);
      this.comboBoxExecution.TabIndex = 3;
      this.comboBoxExecution.SelectedValueChanged += new System.EventHandler(this.comboBoxExecution_SelectedValueChanged);
      // 
      // totalLabel
      // 
      this.totalLabel.AutoSize = true;
      this.totalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.totalLabel.Location = new System.Drawing.Point(175, 88);
      this.totalLabel.Name = "totalLabel";
      this.totalLabel.Size = new System.Drawing.Size(34, 13);
      this.totalLabel.TabIndex = 20;
      this.totalLabel.Text = "$0.00";
      // 
      // stopPriceCustomNumericUpDown
      // 
      this.stopPriceCustomNumericUpDown.DecimalPlaces = 2;
      this.stopPriceCustomNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.stopPriceCustomNumericUpDown.Location = new System.Drawing.Point(110, 6);
      this.stopPriceCustomNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.stopPriceCustomNumericUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
      this.stopPriceCustomNumericUpDown.Name = "stopPriceCustomNumericUpDown";
      this.stopPriceCustomNumericUpDown.Size = new System.Drawing.Size(75, 20);
      this.stopPriceCustomNumericUpDown.TabIndex = 10;
      this.stopPriceCustomNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // trailPrcntgNumericUpDown
      // 
      this.trailPrcntgNumericUpDown.DecimalPlaces = 2;
      this.trailPrcntgNumericUpDown.Location = new System.Drawing.Point(50, 5);
      this.trailPrcntgNumericUpDown.Name = "trailPrcntgNumericUpDown";
      this.trailPrcntgNumericUpDown.Size = new System.Drawing.Size(81, 20);
      this.trailPrcntgNumericUpDown.TabIndex = 10;
      this.trailPrcntgNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // numericUpDownNoOfShares
      // 
      this.numericUpDownNoOfShares.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.numericUpDownNoOfShares.Location = new System.Drawing.Point(75, 85);
      this.numericUpDownNoOfShares.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
      this.numericUpDownNoOfShares.Name = "numericUpDownNoOfShares";
      this.numericUpDownNoOfShares.Size = new System.Drawing.Size(97, 20);
      this.numericUpDownNoOfShares.TabIndex = 2;
      this.numericUpDownNoOfShares.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.numericUpDownNoOfShares.ValueChanged += new System.EventHandler(this.numericUpDownNoOfShares_ValueChanged);
      // 
      // numericUpDownValue
      // 
      this.numericUpDownValue.DecimalPlaces = 2;
      this.numericUpDownValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.numericUpDownValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
      this.numericUpDownValue.Location = new System.Drawing.Point(75, 12);
      this.numericUpDownValue.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
      this.numericUpDownValue.Name = "numericUpDownValue";
      this.numericUpDownValue.Size = new System.Drawing.Size(75, 20);
      this.numericUpDownValue.TabIndex = 0;
      this.numericUpDownValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.numericUpDownValue.ValueChanged += new System.EventHandler(this.numericUpDownValue_ValueChanged);
      // 
      // PricePointControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Controls.Add(this.groupBoxName);
      this.Name = "PricePointControl";
      this.Size = new System.Drawing.Size(654, 137);
      this.Load += new System.EventHandler(this.PricePointControl_Load);
      this.groupBoxName.ResumeLayout(false);
      this.groupBoxName.PerformLayout();
      this.panelStopPrice.ResumeLayout(false);
      this.panelStopPrice.PerformLayout();
      this.trailPrcntgPanel.ResumeLayout(false);
      this.trailPrcntgPanel.PerformLayout();
      this.spreadValuesPanel.ResumeLayout(false);
      this.spreadValuesPanel.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.stopPriceCustomNumericUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trailPrcntgNumericUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNoOfShares)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValue)).EndInit();
      this.ResumeLayout(false);

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
    private System.Windows.Forms.Label labelExecution;
    public System.Windows.Forms.ComboBox comboBoxSpreadValues;
    private System.Windows.Forms.Label label5;
    public System.Windows.Forms.ComboBox triggerComboBox;
    private System.Windows.Forms.Label label6;
    public System.Windows.Forms.ComboBox toFollowComboBox;
    private System.Windows.Forms.Panel trailPrcntgPanel;
    public CustomNumericUpDown trailPrcntgNumericUpDown;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Panel spreadValuesPanel;
    private System.Windows.Forms.Panel panelStopPrice;
    public CustomNumericUpDown stopPriceCustomNumericUpDown;
    private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Button placeOrderButton;
    private System.Windows.Forms.Label totalLabel;
  }
}
