// Decompiled with JetBrains decompiler
// Type: TradeMonitor.TM
// Assembly: TradeMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CEE2865B-9294-47DF-879B-0AFC01A708B6
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\TradeMonitor.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TradeMonitor.Properties;

namespace TradeMonitor
{
  public class TM : Form
  {
    private IContainer components;
    private Timer timer1;
    private Panel panel9;
    private Panel panel1;
    private Panel panel3;
    private Panel panel2;

    public TM()
    {
      this.InitializeComponent();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.timer1.Enabled = false;
      this.Close();
    }

    private void TM_Load(object sender, EventArgs e)
    {
      this.timer1.Enabled = true;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TM));
      this.timer1 = new Timer(this.components);
      this.panel9 = new Panel();
      this.panel1 = new Panel();
      this.panel3 = new Panel();
      this.panel2 = new Panel();
      this.SuspendLayout();
      this.timer1.Interval = 5000;
      this.timer1.Tick += new EventHandler(this.timer1_Tick);
      this.panel9.BackColor = Color.Gold;
      this.panel9.BackgroundImage = (Image) Resources._22;
      this.panel9.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel9.Location = new Point(-7, 316);
      this.panel9.Name = "panel9";
      this.panel9.Size = new Size(475, 10);
      this.panel9.TabIndex = 11;
      this.panel1.BackColor = Color.Gold;
      this.panel1.BackgroundImage = (Image) Resources._22;
      this.panel1.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel1.Location = new Point(-3, -9);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(475, 10);
      this.panel1.TabIndex = 12;
      this.panel3.BackColor = Color.Gold;
      this.panel3.BackgroundImage = (Image) Resources.aa;
      this.panel3.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel3.Location = new Point(436, -4);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(31, 329);
      this.panel3.TabIndex = 9;
      this.panel2.BackColor = Color.Gold;
      this.panel2.BackgroundImage = (Image) Resources.aa;
      this.panel2.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel2.Location = new Point(-30, -7);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(31, 343);
      this.panel2.TabIndex = 10;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackgroundImage = (Image) Resources.welcome2016;
      this.BackgroundImageLayout = ImageLayout.Center;
      this.ClientSize = new Size(437, 317);
      this.Controls.Add((Control) this.panel2);
      this.Controls.Add((Control) this.panel3);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.panel9);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (TM);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Trade Monitor 3.7";
      this.Load += new EventHandler(this.TM_Load);
      this.ResumeLayout(false);
    }
  }
}
