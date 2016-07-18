using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stocks4All.Model;
using BasicallyMe.RobinhoodNet;

namespace Stocks4All.CustomControls
{
  public partial class PricePointControl : UserControl
  {
    public event EventHandler ExecutionSpreadSelected;
    public event EventHandler TriggerSelected;
    public event EventHandler FollowSelected;
    public event EventHandler FollowSharesSelected;
    public event EventHandler PlaceOrder;

    [Description("Text displayed in the groupbox"), Category("Data")]
    public string DisplayName
    {
      get { return groupBoxName.Text; }
      set { groupBoxName.Text = value; }
    }

    //public PricePoint pricePoint
    //{
    //  get { return pricePoint; }
    //  set { pricePoint = value; }
    //}

    public enum OrderType
    {
      Market = 1,
      Limit = 2
    }

    public enum Execution
    {
      Spread = 1,
      Limit = 2,
      Trailing = 3
    }

    public enum FollowPrice
    {
      AtBid = 1,
      AtAsk = 2,
      AtLastTradedPrice = 3,
      AtLastTradedPrice_Off_1 = 6,
      AtLastTradedPrice_Off_2 = 7,
      AtLastTradedPrice_Off_5 = 8,
      AtLastTradedPrice_Off_10 = 9,
      AtLastTradedPrice_Off_20 = 10,
      AtValue = 4,
      AtCost = 5,
      
    }
    public enum FollowShare
    {
      FullPosition = 1,
      HalfPosition = 2,
      Value = 3,
    }

    public PricePointControl()
    {
      InitializeComponent();
      comboBoxOrderType.DataSource = Enum.GetValues(typeof(OrderType));
      comboBoxExecution.DataSource = Enum.GetValues(typeof(Execution));
      triggerComboBox.DataSource = Enum.GetValues(typeof(TriggerType));
      toFollowComboBox.DataSource = Enum.GetValues(typeof(FollowPrice));
      toFollowSharesComboBox.DataSource = Enum.GetValues(typeof(FollowShare));
      numericUpDownValue.TextChanged += NumericUpDownValue_TextChanged;
      numericUpDownNoOfShares.TextChanged += NumericUpDownNoOfShares_TextChanged;
    }

    private void NumericUpDownNoOfShares_TextChanged(object sender, EventArgs e)
    {
      try
      {
        totalLabel.Text = "$" + (decimal.Parse(numericUpDownNoOfShares.Text) * decimal.Parse(numericUpDownValue.Text)).ToString();
      }
      catch
      {
        totalLabel.Text = "$0";
      }
    }

    private void NumericUpDownValue_TextChanged(object sender, EventArgs e)
    {
      try
      {
        totalLabel.Text = "$" + (decimal.Parse(numericUpDownNoOfShares.Text) * decimal.Parse(numericUpDownValue.Text)).ToString();
      }
      catch
      {
        totalLabel.Text = "$0";
      }
    }

    public void Set(PricePoint pp)
    {
      this.numericUpDownValue.Value = pp.Price;
      this.stopPriceCustomNumericUpDown.Value = pp.StopOffset;
      this.trailPrcntgNumericUpDown.Value = pp.TrailPrcntg;
      this.comboBoxOrderType.SelectedItem = pp.Type;
      this.comboBoxExecution.SelectedItem = pp.Execution;
      if (pp.NoOfShares < 0)
        pp.NoOfShares *= -1;
      this.numericUpDownNoOfShares.Value = pp.NoOfShares;
      this.triggerComboBox.SelectedItem = pp.Trigger;
      this.toFollowComboBox.SelectedItem = pp.FollowPrice;
      this.toFollowSharesComboBox.SelectedItem = pp.FollowShare;
    }
    
    public PricePoint Get()
    {
      PricePoint pp = new PricePoint();

      pp.Price = this.numericUpDownValue.Value;
      pp.StopOffset = this.stopPriceCustomNumericUpDown.Value;
      pp.TrailPrcntg = this.trailPrcntgNumericUpDown.Value;
      pp.Type = (PricePointControl.OrderType)this.comboBoxOrderType.SelectedValue;
      pp.Execution = (PricePointControl.Execution)this.comboBoxExecution.SelectedValue;
      pp.Trigger = (TriggerType)this.triggerComboBox.SelectedValue;
      pp.FollowPrice = (PricePointControl.FollowPrice)this.toFollowComboBox.SelectedValue;
      pp.FollowShare = (PricePointControl.FollowShare)this.toFollowSharesComboBox.SelectedValue;
      pp.NoOfShares = (int)this.numericUpDownNoOfShares.Value;
      return pp;
    }

    private void PricePointControl_Load(object sender, EventArgs e)
    {

    }

    private void numericUpDownValue_ValueChanged(object sender, EventArgs e)
    {
      //if (numericUpDownValue.Value > 0)
      //  numericUpDownNoOfShares.Enabled = comboBoxExecution.Enabled = comboBoxOrderType.Enabled = true;
      //else
      //  numericUpDownNoOfShares.Enabled = comboBoxExecution.Enabled = comboBoxOrderType.Enabled = false;

      if (comboBoxExecution.SelectedValue.ToString() == Execution.Spread.ToString())
      {
        if (this.ExecutionSpreadSelected != null)
          this.ExecutionSpreadSelected(new object(), new EventArgs());
      }
    }

    private void comboBoxExecution_SelectedValueChanged(object sender, EventArgs e)
    {
      spreadValuesPanel.Visible = false;
      trailPrcntgPanel.Visible = false;
      if (comboBoxExecution.SelectedValue.ToString() == Execution.Spread.ToString())
      {
        //labelExecution.Text = "Spread Value: $";
        //labelExecution.Visible = comboBoxSpreadValues.Visible = true;
        spreadValuesPanel.Visible = true;
        spreadValuesPanel.Refresh();
        if (this.ExecutionSpreadSelected != null)
          this.ExecutionSpreadSelected(new object(), new EventArgs());
      }
      else if (comboBoxExecution.SelectedValue.ToString() == Execution.Trailing.ToString())
      {
        trailPrcntgPanel.Visible = true;
        //labelExecution.Text = "Trail %:";
        //labelExecution.Visible = comboBoxSpreadValues.Visible = true;
      }
    }

    private void numericUpDownNoOfShares_ValueChanged(object sender, EventArgs e)
    {
      if (comboBoxExecution.SelectedValue.ToString() == Execution.Spread.ToString())
      {
        if (this.ExecutionSpreadSelected != null)
          this.ExecutionSpreadSelected(new object(), new EventArgs());
      }
    }

    private void toFollowComboBox_SelectedValueChanged(object sender, EventArgs e)
    {
      //if (toFollowComboBox.SelectedValue.ToString() == Execution.Spread.ToString())
      //{
        if (this.FollowSelected != null)
          this.FollowSelected(new object(), new EventArgs());
      //}
    }

    private void placeOrderButton_Click(object sender, EventArgs e)
    {
      if (this.PlaceOrder != null)
        this.PlaceOrder(new object(), new EventArgs());
    }

    private void triggerComboBox_SelectedValueChanged(object sender, EventArgs e)
    {
      if (triggerComboBox.SelectedValue.ToString() == TriggerType.Stop.ToString())
      {
        panelStopPrice.Visible = true;
      }
      else
      {
        panelStopPrice.Visible = false;
      }

      if (this.TriggerSelected != null)
        this.TriggerSelected(new object(), new EventArgs());
    }

    private void groupBoxName_Enter(object sender, EventArgs e)
    {

    }

    private void toFollowSharesComboBox_SelectedValueChanged(object sender, EventArgs e)
    {
      if (this.FollowSharesSelected != null)
        this.FollowSharesSelected(new object(), new EventArgs());
    }
  }
}
