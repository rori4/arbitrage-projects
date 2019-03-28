// Decompiled with JetBrains decompiler
// Type: TradeMonitor.GOOD2
// Assembly: TradeMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CEE2865B-9294-47DF-879B-0AFC01A708B6
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\TradeMonitor.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TradeMonitor
{
  public class GOOD2 : Form
  {
    private IContainer components;
    private Panel panel1;
    private TextBox textBox1;

    public GOOD2()
    {
      this.InitializeComponent();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (GOOD2));
      this.panel1 = new Panel();
      this.textBox1 = new TextBox();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.panel1.Controls.Add((Control) this.textBox1);
      this.panel1.Location = new Point(5, 5);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(385, 8774);
      this.panel1.TabIndex = 0;
      this.textBox1.BackColor = Color.FromArgb(0, 77, 111);
      this.textBox1.BorderStyle = BorderStyle.None;
      this.textBox1.Enabled = false;
      this.textBox1.ForeColor = Color.White;
      this.textBox1.HideSelection = false;
      this.textBox1.Location = new Point(0, 0);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(382, 8774);
      this.textBox1.TabIndex = 0;
      this.textBox1.Text = componentResourceManager.GetString("textBox1.Text");
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoScroll = true;
      this.BackColor = Color.FromArgb(0, 77, 111);
      this.ClientSize = new Size(410, 400);
      this.Controls.Add((Control) this.panel1);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Location = new Point(5, 65);
      this.Name = nameof (GOOD2);
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (GOOD2);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
