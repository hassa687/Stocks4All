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

namespace Stocks4All.CustomControls
{
  public partial class StopLossControl : UserControl
  {
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
      Limit = 2
    }

    public StopLossControl()
    {
      InitializeComponent();
      comboBoxOrderType.DataSource = Enum.GetValues(typeof(OrderType));
      comboBoxExecution.DataSource = Enum.GetValues(typeof(Execution));
    }

    private void PricePointControl_Load(object sender, EventArgs e)
    {

    }

    private void numericUpDownValue_ValueChanged(object sender, EventArgs e)
    {
      if (numericUpDownValue.Value > 0)
        numericUpDownNoOfShares.Enabled = comboBoxExecution.Enabled = comboBoxOrderType.Enabled = true;
      else
        numericUpDownNoOfShares.Enabled = comboBoxExecution.Enabled = comboBoxOrderType.Enabled = false;
    }
  }
}
