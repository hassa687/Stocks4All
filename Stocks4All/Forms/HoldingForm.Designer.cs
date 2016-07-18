using Yolo.CustomControls;
namespace Stocks4All
{
  partial class HoldingForm
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
      this.buttonCancel = new System.Windows.Forms.Button();
      this.PriceTarget = new Stocks4All.CustomControls.PricePointControl();
      this.Entry = new Stocks4All.CustomControls.PricePointControl();
      this.StopLoss = new Stocks4All.CustomControls.PricePointControl();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.labelVolatitlity = new System.Windows.Forms.Label();
      this.Volatility = new Yolo.CustomControls.CustomNumericUpDown();
      this.labelMarketValue = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.CostBasis = new Yolo.CustomControls.CustomNumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      this.NoOfShares = new Yolo.CustomControls.CustomNumericUpDown();
      this.groupBoxTradingFields = new System.Windows.Forms.GroupBox();
      this.checkManageTrade = new System.Windows.Forms.CheckBox();
      this.label9 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.ProfitTargPrctg = new Yolo.CustomControls.CustomNumericUpDown();
      this.label11 = new System.Windows.Forms.Label();
      this.ProfitTargAmnt = new Yolo.CustomControls.CustomNumericUpDown();
      this.label8 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.checkBoxDayTrade = new System.Windows.Forms.CheckBox();
      this.MaxLossPrcntg = new Yolo.CustomControls.CustomNumericUpDown();
      this.label6 = new System.Windows.Forms.Label();
      this.MaxLossAmnt = new Yolo.CustomControls.CustomNumericUpDown();
      this.labelQuote = new System.Windows.Forms.Label();
      this.textBoxTicker = new Stocks4All.CustomControls.DelayedTextBox();
      this.buttonSave = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.labelBidPrice = new System.Windows.Forms.Label();
      this.labelAskPrice = new System.Windows.Forms.Label();
      this.groupBoxPendingOrders = new System.Windows.Forms.GroupBox();
      this.buttonCancelPendingOrders = new System.Windows.Forms.Button();
      this.dataGridViewPendingOrders = new System.Windows.Forms.DataGridView();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.Volatility)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.CostBasis)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.NoOfShares)).BeginInit();
      this.groupBoxTradingFields.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ProfitTargPrctg)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ProfitTargAmnt)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MaxLossPrcntg)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MaxLossAmnt)).BeginInit();
      this.groupBoxPendingOrders.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPendingOrders)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonCancel
      // 
      this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.buttonCancel.Location = new System.Drawing.Point(1098, 588);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(188, 30);
      this.buttonCancel.TabIndex = 10;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // PriceTarget
      // 
      this.PriceTarget.AutoSize = true;
      this.PriceTarget.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.PriceTarget.DisplayName = "Price Target";
      this.PriceTarget.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.PriceTarget.Location = new System.Drawing.Point(13, 435);
      this.PriceTarget.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.PriceTarget.Name = "PriceTarget";
      this.PriceTarget.Size = new System.Drawing.Size(762, 191);
      this.PriceTarget.TabIndex = 21;
      this.PriceTarget.Load += new System.EventHandler(this.PriceTarget_Load);
      // 
      // Entry
      // 
      this.Entry.AutoSize = true;
      this.Entry.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Entry.DisplayName = "Entry";
      this.Entry.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Entry.Location = new System.Drawing.Point(13, 50);
      this.Entry.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Entry.Name = "Entry";
      this.Entry.Size = new System.Drawing.Size(762, 191);
      this.Entry.TabIndex = 22;
      this.Entry.Load += new System.EventHandler(this.Entry_Load);
      this.Entry.VisibleChanged += new System.EventHandler(this.Entry_VisibleChanged);
      // 
      // StopLoss
      // 
      this.StopLoss.AutoSize = true;
      this.StopLoss.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.StopLoss.DisplayName = "Stop";
      this.StopLoss.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.StopLoss.Location = new System.Drawing.Point(13, 245);
      this.StopLoss.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.StopLoss.Name = "StopLoss";
      this.StopLoss.Size = new System.Drawing.Size(762, 191);
      this.StopLoss.TabIndex = 23;
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label4.AutoSize = true;
      this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label4.Location = new System.Drawing.Point(667, 18);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(37, 19);
      this.label4.TabIndex = 27;
      this.label4.Text = "Bid:";
      // 
      // label5
      // 
      this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label5.AutoSize = true;
      this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label5.Location = new System.Drawing.Point(468, 18);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(41, 19);
      this.label5.TabIndex = 26;
      this.label5.Text = "Ask:";
      // 
      // labelVolatitlity
      // 
      this.labelVolatitlity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.labelVolatitlity.AutoSize = true;
      this.labelVolatitlity.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.labelVolatitlity.Location = new System.Drawing.Point(909, 106);
      this.labelVolatitlity.Name = "labelVolatitlity";
      this.labelVolatitlity.Size = new System.Drawing.Size(97, 19);
      this.labelVolatitlity.TabIndex = 25;
      this.labelVolatitlity.Text = "Volatility %:";
      // 
      // Volatility
      // 
      this.Volatility.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.Volatility.DecimalPlaces = 2;
      this.Volatility.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
      this.Volatility.Location = new System.Drawing.Point(1012, 104);
      this.Volatility.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
      this.Volatility.Name = "Volatility";
      this.Volatility.Size = new System.Drawing.Size(92, 27);
      this.Volatility.TabIndex = 24;
      this.Volatility.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.Volatility.ValueChanged += new System.EventHandler(this.numericUpDownVolatility_ValueChanged);
      this.Volatility.Leave += new System.EventHandler(this.numericUpDownVolatility_Leave);
      // 
      // labelMarketValue
      // 
      this.labelMarketValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.labelMarketValue.AutoSize = true;
      this.labelMarketValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.labelMarketValue.Location = new System.Drawing.Point(1110, 18);
      this.labelMarketValue.Name = "labelMarketValue";
      this.labelMarketValue.Size = new System.Drawing.Size(95, 19);
      this.labelMarketValue.TabIndex = 20;
      this.labelMarketValue.Text = "MarketValue";
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label1.Location = new System.Drawing.Point(909, 18);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(100, 19);
      this.label1.TabIndex = 18;
      this.label1.Text = "Cost Basis: $";
      // 
      // CostBasis
      // 
      this.CostBasis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.CostBasis.DecimalPlaces = 2;
      this.CostBasis.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
      this.CostBasis.Location = new System.Drawing.Point(1012, 16);
      this.CostBasis.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
      this.CostBasis.Name = "CostBasis";
      this.CostBasis.Size = new System.Drawing.Size(92, 27);
      this.CostBasis.TabIndex = 16;
      this.CostBasis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label2.AutoSize = true;
      this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label2.Location = new System.Drawing.Point(900, 62);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(106, 19);
      this.label2.TabIndex = 19;
      this.label2.Text = "No of Shares:";
      // 
      // NoOfShares
      // 
      this.NoOfShares.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.NoOfShares.Location = new System.Drawing.Point(1012, 60);
      this.NoOfShares.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
      this.NoOfShares.Name = "NoOfShares";
      this.NoOfShares.Size = new System.Drawing.Size(92, 27);
      this.NoOfShares.TabIndex = 17;
      this.NoOfShares.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // groupBoxTradingFields
      // 
      this.groupBoxTradingFields.Controls.Add(this.checkManageTrade);
      this.groupBoxTradingFields.Controls.Add(this.label9);
      this.groupBoxTradingFields.Controls.Add(this.label10);
      this.groupBoxTradingFields.Controls.Add(this.ProfitTargPrctg);
      this.groupBoxTradingFields.Controls.Add(this.label11);
      this.groupBoxTradingFields.Controls.Add(this.ProfitTargAmnt);
      this.groupBoxTradingFields.Controls.Add(this.label8);
      this.groupBoxTradingFields.Controls.Add(this.label7);
      this.groupBoxTradingFields.Controls.Add(this.checkBoxDayTrade);
      this.groupBoxTradingFields.Controls.Add(this.MaxLossPrcntg);
      this.groupBoxTradingFields.Controls.Add(this.label6);
      this.groupBoxTradingFields.Controls.Add(this.MaxLossAmnt);
      this.groupBoxTradingFields.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.groupBoxTradingFields.Location = new System.Drawing.Point(846, 137);
      this.groupBoxTradingFields.Name = "groupBoxTradingFields";
      this.groupBoxTradingFields.Size = new System.Drawing.Size(464, 142);
      this.groupBoxTradingFields.TabIndex = 15;
      this.groupBoxTradingFields.TabStop = false;
      this.groupBoxTradingFields.Text = "Trading Fields";
      // 
      // checkManageTrade
      // 
      this.checkManageTrade.AutoSize = true;
      this.checkManageTrade.Location = new System.Drawing.Point(203, 26);
      this.checkManageTrade.Name = "checkManageTrade";
      this.checkManageTrade.Size = new System.Drawing.Size(128, 23);
      this.checkManageTrade.TabIndex = 40;
      this.checkManageTrade.Text = "Manage Trade";
      this.checkManageTrade.UseVisualStyleBackColor = true;
      this.checkManageTrade.CheckedChanged += new System.EventHandler(this.checkManageTrade_CheckedChanged);
      // 
      // label9
      // 
      this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label9.AutoSize = true;
      this.label9.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label9.Location = new System.Drawing.Point(320, 95);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(24, 19);
      this.label9.TabIndex = 39;
      this.label9.Text = "or";
      this.label9.Visible = false;
      // 
      // label10
      // 
      this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label10.AutoSize = true;
      this.label10.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label10.Location = new System.Drawing.Point(449, 95);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(25, 19);
      this.label10.TabIndex = 38;
      this.label10.Text = "%";
      this.label10.Visible = false;
      // 
      // ProfitTargPrctg
      // 
      this.ProfitTargPrctg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ProfitTargPrctg.DecimalPlaces = 2;
      this.ProfitTargPrctg.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
      this.ProfitTargPrctg.Location = new System.Drawing.Point(344, 93);
      this.ProfitTargPrctg.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
      this.ProfitTargPrctg.Name = "ProfitTargPrctg";
      this.ProfitTargPrctg.Size = new System.Drawing.Size(99, 27);
      this.ProfitTargPrctg.TabIndex = 37;
      this.ProfitTargPrctg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.ProfitTargPrctg.Visible = false;
      this.ProfitTargPrctg.ValueChanged += new System.EventHandler(this.ProfitTargPrctg_ValueChanged);
      // 
      // label11
      // 
      this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label11.AutoSize = true;
      this.label11.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label11.Location = new System.Drawing.Point(96, 95);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(118, 19);
      this.label11.TabIndex = 36;
      this.label11.Text = "Profit Target: $";
      this.label11.Visible = false;
      // 
      // ProfitTargAmnt
      // 
      this.ProfitTargAmnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ProfitTargAmnt.DecimalPlaces = 2;
      this.ProfitTargAmnt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
      this.ProfitTargAmnt.Location = new System.Drawing.Point(220, 93);
      this.ProfitTargAmnt.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
      this.ProfitTargAmnt.Name = "ProfitTargAmnt";
      this.ProfitTargAmnt.Size = new System.Drawing.Size(94, 27);
      this.ProfitTargAmnt.TabIndex = 35;
      this.ProfitTargAmnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.ProfitTargAmnt.Visible = false;
      this.ProfitTargAmnt.ValueChanged += new System.EventHandler(this.ProfitTargAmnt_ValueChanged);
      // 
      // label8
      // 
      this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label8.AutoSize = true;
      this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label8.Location = new System.Drawing.Point(369, 35);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(24, 19);
      this.label8.TabIndex = 34;
      this.label8.Text = "or";
      this.label8.Visible = false;
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label7.AutoSize = true;
      this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label7.Location = new System.Drawing.Point(290, 57);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(25, 19);
      this.label7.TabIndex = 33;
      this.label7.Text = "%";
      // 
      // checkBoxDayTrade
      // 
      this.checkBoxDayTrade.AutoSize = true;
      this.checkBoxDayTrade.Location = new System.Drawing.Point(83, 31);
      this.checkBoxDayTrade.Name = "checkBoxDayTrade";
      this.checkBoxDayTrade.Size = new System.Drawing.Size(101, 23);
      this.checkBoxDayTrade.TabIndex = 15;
      this.checkBoxDayTrade.Text = "Day Trade";
      this.checkBoxDayTrade.UseVisualStyleBackColor = true;
      this.checkBoxDayTrade.Visible = false;
      // 
      // MaxLossPrcntg
      // 
      this.MaxLossPrcntg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.MaxLossPrcntg.DecimalPlaces = 2;
      this.MaxLossPrcntg.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
      this.MaxLossPrcntg.Location = new System.Drawing.Point(190, 55);
      this.MaxLossPrcntg.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
      this.MaxLossPrcntg.Name = "MaxLossPrcntg";
      this.MaxLossPrcntg.Size = new System.Drawing.Size(94, 27);
      this.MaxLossPrcntg.TabIndex = 32;
      this.MaxLossPrcntg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.MaxLossPrcntg.ValueChanged += new System.EventHandler(this.MaxLossPrcntg_ValueChanged);
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label6.AutoSize = true;
      this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label6.Location = new System.Drawing.Point(109, 57);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(75, 19);
      this.label6.TabIndex = 31;
      this.label6.Text = "Max loss:";
      // 
      // MaxLossAmnt
      // 
      this.MaxLossAmnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.MaxLossAmnt.DecimalPlaces = 2;
      this.MaxLossAmnt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
      this.MaxLossAmnt.Location = new System.Drawing.Point(341, 55);
      this.MaxLossAmnt.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
      this.MaxLossAmnt.Name = "MaxLossAmnt";
      this.MaxLossAmnt.Size = new System.Drawing.Size(94, 27);
      this.MaxLossAmnt.TabIndex = 30;
      this.MaxLossAmnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.MaxLossAmnt.Visible = false;
      this.MaxLossAmnt.ValueChanged += new System.EventHandler(this.customNumericUpDownMaxLossAmnt_ValueChanged);
      // 
      // labelQuote
      // 
      this.labelQuote.AutoSize = true;
      this.labelQuote.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.labelQuote.Location = new System.Drawing.Point(205, 18);
      this.labelQuote.Name = "labelQuote";
      this.labelQuote.Size = new System.Drawing.Size(52, 19);
      this.labelQuote.TabIndex = 12;
      this.labelQuote.Text = "Quote";
      // 
      // textBoxTicker
      // 
      this.textBoxTicker.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
      this.textBoxTicker.DelayedTextChangedTimeout = 10000;
      this.textBoxTicker.Location = new System.Drawing.Point(79, 15);
      this.textBoxTicker.Name = "textBoxTicker";
      this.textBoxTicker.Size = new System.Drawing.Size(120, 27);
      this.textBoxTicker.TabIndex = 0;
      // 
      // buttonSave
      // 
      this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.buttonSave.Location = new System.Drawing.Point(1016, 588);
      this.buttonSave.Name = "buttonSave";
      this.buttonSave.Size = new System.Drawing.Size(76, 30);
      this.buttonSave.TabIndex = 9;
      this.buttonSave.Text = "Save";
      this.buttonSave.UseVisualStyleBackColor = true;
      this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label3.Location = new System.Drawing.Point(19, 18);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(58, 19);
      this.label3.TabIndex = 6;
      this.label3.Text = "Ticker:";
      // 
      // labelBidPrice
      // 
      this.labelBidPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.labelBidPrice.AutoSize = true;
      this.labelBidPrice.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.labelBidPrice.Location = new System.Drawing.Point(709, 18);
      this.labelBidPrice.Name = "labelBidPrice";
      this.labelBidPrice.Size = new System.Drawing.Size(31, 19);
      this.labelBidPrice.TabIndex = 2;
      this.labelBidPrice.Text = "Bid";
      // 
      // labelAskPrice
      // 
      this.labelAskPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.labelAskPrice.AutoSize = true;
      this.labelAskPrice.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.labelAskPrice.Location = new System.Drawing.Point(515, 18);
      this.labelAskPrice.Name = "labelAskPrice";
      this.labelAskPrice.Size = new System.Drawing.Size(35, 19);
      this.labelAskPrice.TabIndex = 1;
      this.labelAskPrice.Text = "Ask";
      // 
      // groupBoxPendingOrders
      // 
      this.groupBoxPendingOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBoxPendingOrders.Controls.Add(this.buttonCancelPendingOrders);
      this.groupBoxPendingOrders.Controls.Add(this.dataGridViewPendingOrders);
      this.groupBoxPendingOrders.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.groupBoxPendingOrders.Location = new System.Drawing.Point(846, 300);
      this.groupBoxPendingOrders.Name = "groupBoxPendingOrders";
      this.groupBoxPendingOrders.Size = new System.Drawing.Size(440, 282);
      this.groupBoxPendingOrders.TabIndex = 29;
      this.groupBoxPendingOrders.TabStop = false;
      this.groupBoxPendingOrders.Text = "Pending Orders:";
      // 
      // buttonCancelPendingOrders
      // 
      this.buttonCancelPendingOrders.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this.buttonCancelPendingOrders.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.buttonCancelPendingOrders.Location = new System.Drawing.Point(252, 13);
      this.buttonCancelPendingOrders.Name = "buttonCancelPendingOrders";
      this.buttonCancelPendingOrders.Size = new System.Drawing.Size(182, 30);
      this.buttonCancelPendingOrders.TabIndex = 30;
      this.buttonCancelPendingOrders.Text = "Cancel Pending Orders";
      this.buttonCancelPendingOrders.UseVisualStyleBackColor = true;
      this.buttonCancelPendingOrders.Click += new System.EventHandler(this.buttonCancelPendingOrders_Click);
      // 
      // dataGridViewPendingOrders
      // 
      this.dataGridViewPendingOrders.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.dataGridViewPendingOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewPendingOrders.Location = new System.Drawing.Point(16, 49);
      this.dataGridViewPendingOrders.Name = "dataGridViewPendingOrders";
      this.dataGridViewPendingOrders.Size = new System.Drawing.Size(408, 277);
      this.dataGridViewPendingOrders.TabIndex = 28;
      // 
      // HoldingForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.buttonCancel;
      this.ClientSize = new System.Drawing.Size(1298, 629);
      this.Controls.Add(this.PriceTarget);
      this.Controls.Add(this.Entry);
      this.Controls.Add(this.StopLoss);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.labelVolatitlity);
      this.Controls.Add(this.Volatility);
      this.Controls.Add(this.labelMarketValue);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.CostBasis);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.NoOfShares);
      this.Controls.Add(this.groupBoxTradingFields);
      this.Controls.Add(this.labelQuote);
      this.Controls.Add(this.textBoxTicker);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonSave);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.labelBidPrice);
      this.Controls.Add(this.labelAskPrice);
      this.Controls.Add(this.groupBoxPendingOrders);
      this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.Name = "HoldingForm";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HoldingForm_FormClosing);
      this.Load += new System.EventHandler(this.HoldingForm_Load);
      this.Shown += new System.EventHandler(this.HoldingForm_Shown);
      ((System.ComponentModel.ISupportInitialize)(this.Volatility)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.CostBasis)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.NoOfShares)).EndInit();
      this.groupBoxTradingFields.ResumeLayout(false);
      this.groupBoxTradingFields.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ProfitTargPrctg)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ProfitTargAmnt)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MaxLossPrcntg)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MaxLossAmnt)).EndInit();
      this.groupBoxPendingOrders.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPendingOrders)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.Button buttonCancel;
    private Stocks4All.CustomControls.DelayedTextBox textBoxTicker;
    public System.Windows.Forms.Label labelAskPrice;
    public System.Windows.Forms.Label labelBidPrice;
    public System.Windows.Forms.Label labelQuote;
    private System.Windows.Forms.GroupBox groupBoxTradingFields;
    private System.Windows.Forms.CheckBox checkBoxDayTrade;
    private System.Windows.Forms.Label label1;
    private CustomNumericUpDown CostBasis;
    private System.Windows.Forms.Label label2;
    private CustomNumericUpDown NoOfShares;
    public System.Windows.Forms.Label labelMarketValue;
    private CustomControls.PricePointControl PriceTarget;
    private CustomControls.PricePointControl Entry;
    private CustomControls.PricePointControl StopLoss;
    private System.Windows.Forms.Label labelVolatitlity;
    private CustomNumericUpDown Volatility;
    public System.Windows.Forms.Label label4;
    public System.Windows.Forms.Label label5;
    private System.Windows.Forms.DataGridView dataGridViewPendingOrders;
    private System.Windows.Forms.GroupBox groupBoxPendingOrders;
    private System.Windows.Forms.Button buttonCancelPendingOrders;
    private System.Windows.Forms.Label label6;
    private CustomNumericUpDown MaxLossAmnt;
    private System.Windows.Forms.Label label7;
    private CustomNumericUpDown MaxLossPrcntg;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private CustomNumericUpDown ProfitTargPrctg;
    private System.Windows.Forms.Label label11;
    private CustomNumericUpDown ProfitTargAmnt;
    private System.Windows.Forms.CheckBox checkManageTrade;
    private System.Windows.Forms.Timer timer1;
  }
}