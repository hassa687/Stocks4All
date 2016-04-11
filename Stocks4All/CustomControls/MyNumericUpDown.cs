using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yolo.CustomControls
{
  public class CustomNumericUpDown : NumericUpDown
  {
    protected override void OnEnter(EventArgs e)
    {
      this.Select(0, this.Text.Length);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      //base.OnMouseDown(e);
      //this.BeginInvoke((Action) delegate
      //{
      //this.Select(0, this.Text.Length);
      //});
      this.Select(0, this.Text.Length);
    }

    private void InitializeComponent()
    {
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // CustomNumericUpDown
      // 
      this.Enter += new System.EventHandler(this.CustomNumericUpDown_Enter);
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);

      this.Increment = 0.10m;
    }

    private void CustomNumericUpDown_Enter(object sender, EventArgs e)
    {
      //this.BeginInvoke((Action) delegate
      //{
      //this.Select(0, this.Text.Length);
      //});
    }
   
  }
}
