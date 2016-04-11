using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stocks4All
{
  public partial class BaseForm : Form
  {
    public BaseForm()
    {
      InitializeComponent();
      //this.BackColor = Color.Black;
      //this.ForeColor = Color.LightGray;
    }

    bool isNew = false;

    public BaseForm(bool isNew)
      : this()
    {

      this.isNew = isNew;
    }

    private void BaseForm_Load(object sender, EventArgs e)
    {

    }

    private void BaseForm_ControlAdded(object sender, ControlEventArgs e)
    {
      var button = e.Control as Button;
      if (button != null)
      {
        button.FlatStyle = FlatStyle.System;
        //button.BackColor = Color.FromArgb(20, 19, 19);
        //button.ForeColor = Color.FromArgb(238, 238, 238);
      }

      var num = e.Control as NumericUpDown;
      if (num != null)
      {
        num.TextAlign = HorizontalAlignment.Right;
      }

      var label = e.Control as Label;
      if (label != null)
      {
        label.FlatStyle = FlatStyle.System;
      }

      var comboBox = e.Control as ComboBox;
      if (comboBox != null)
      {
        comboBox.FlatStyle = FlatStyle.System;
      }


      var groupBox = e.Control as GroupBox;
      if (groupBox != null)
      {
        groupBox.FlatStyle = FlatStyle.System;
      }
    }
  }
}
