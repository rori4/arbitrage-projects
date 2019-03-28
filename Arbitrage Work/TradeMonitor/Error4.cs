// Decompiled with JetBrains decompiler
// Type: TradeMonitor.Error4
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
  public class Error4 : Form
  {
    private Point mouseOffset;
    private IContainer components;
    private Panel panel1;
    private Label label2;
    private PictureBox pictureBox2;
    private Button button6;
    private Label label1;
    private Panel panel7;
    private Panel panel6;
    private Panel panel5;
    private Panel panel4;

    public Error4()
    {
      this.InitializeComponent();
    }

    private void panel1_MouseDown(object sender, MouseEventArgs e)
    {
      this.mouseOffset = new Point(-e.X, -e.Y);
    }

    private void panel1_MouseMove(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      Point mousePosition = Control.MousePosition;
      mousePosition.Offset(this.mouseOffset.X, this.mouseOffset.Y);
      this.Location = mousePosition;
    }

    private void button6_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void button6_MouseMove(object sender, MouseEventArgs e)
    {
      this.button6.BackgroundImage = (Image) Resources.zak2;
    }

    private void button6_MouseLeave(object sender, EventArgs e)
    {
      this.button6.BackgroundImage = (Image) Resources.zak1;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Error4));
      this.panel1 = new Panel();
      this.panel7 = new Panel();
      this.panel6 = new Panel();
      this.panel5 = new Panel();
      this.panel4 = new Panel();
      this.label1 = new Label();
      this.label2 = new Label();
      this.pictureBox2 = new PictureBox();
      this.button6 = new Button();
      this.panel1.SuspendLayout();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      this.SuspendLayout();
      this.panel1.BackColor = Color.FromArgb(20, 20, 20);
      this.panel1.BackgroundImage = (Image) Resources.errr2016222;
      this.panel1.Controls.Add((Control) this.panel7);
      this.panel1.Controls.Add((Control) this.panel6);
      this.panel1.Controls.Add((Control) this.panel5);
      this.panel1.Controls.Add((Control) this.panel4);
      this.panel1.Controls.Add((Control) this.label1);
      this.panel1.Controls.Add((Control) this.label2);
      this.panel1.Controls.Add((Control) this.pictureBox2);
      this.panel1.Controls.Add((Control) this.button6);
      this.panel1.Cursor = Cursors.Default;
      this.panel1.Location = new Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(362, 229);
      this.panel1.TabIndex = 22;
      this.panel1.MouseDown += new MouseEventHandler(this.panel1_MouseDown);
      this.panel1.MouseMove += new MouseEventHandler(this.panel1_MouseMove);
      this.panel7.BackColor = Color.Gold;
      this.panel7.BackgroundImage = (Image) Resources._22;
      this.panel7.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel7.Location = new Point(-19, 228);
      this.panel7.Name = "panel7";
      this.panel7.Size = new Size(391, 10);
      this.panel7.TabIndex = 30;
      this.panel6.BackColor = Color.Gold;
      this.panel6.BackgroundImage = (Image) Resources._22;
      this.panel6.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel6.Location = new Point(-13, -9);
      this.panel6.Name = "panel6";
      this.panel6.Size = new Size(391, 10);
      this.panel6.TabIndex = 29;
      this.panel5.BackColor = Color.Gold;
      this.panel5.BackgroundImage = (Image) Resources.aa;
      this.panel5.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel5.Location = new Point(-30, -8);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(31, 241);
      this.panel5.TabIndex = 32;
      this.panel4.BackColor = Color.Gold;
      this.panel4.BackgroundImage = (Image) Resources.aa;
      this.panel4.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel4.Location = new Point(361, -4);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(31, 241);
      this.panel4.TabIndex = 31;
      this.label1.AutoSize = true;
      this.label1.BackColor = Color.Transparent;
      this.label1.Font = new Font("Arial", 12.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.label1.ForeColor = Color.White;
      this.label1.Location = new Point(108, 115);
      this.label1.Name = "label1";
      this.label1.Size = new Size(144, 19);
      this.label1.TabIndex = 28;
      this.label1.Text = "An error occured";
      this.label2.AutoSize = true;
      this.label2.BackColor = Color.Transparent;
      this.label2.Font = new Font("Arial", 9.75f);
      this.label2.ForeColor = Color.FromArgb(251, 143, 0);
      this.label2.Location = new Point(32, 5);
      this.label2.Name = "label2";
      this.label2.Size = new Size(61, 16);
      this.label2.TabIndex = 12;
      this.label2.Text = "Message";
      this.pictureBox2.BackColor = Color.Transparent;
      this.pictureBox2.BackgroundImage = (Image) Resources.User_blue3;
      this.pictureBox2.Location = new Point(1, 1);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(31, 31);
      this.pictureBox2.TabIndex = 11;
      this.pictureBox2.TabStop = false;
      this.button6.BackColor = Color.Transparent;
      this.button6.BackgroundImage = (Image) Resources.zak1;
      this.button6.BackgroundImageLayout = ImageLayout.Center;
      this.button6.Cursor = Cursors.Hand;
      this.button6.FlatAppearance.BorderSize = 0;
      this.button6.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button6.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button6.FlatStyle = FlatStyle.Flat;
      this.button6.ForeColor = Color.Transparent;
      this.button6.Location = new Point(327, 0);
      this.button6.Name = "button6";
      this.button6.Size = new Size(35, 35);
      this.button6.TabIndex = 10;
      this.button6.UseVisualStyleBackColor = false;
      this.button6.Click += new EventHandler(this.button6_Click);
      this.button6.MouseLeave += new EventHandler(this.button6_MouseLeave);
      this.button6.MouseMove += new MouseEventHandler(this.button6_MouseMove);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(45, 45, 48);
      this.ClientSize = new Size(362, 229);
      this.Controls.Add((Control) this.panel1);
      this.Cursor = Cursors.Hand;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (Error4);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "An error occured";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((ISupportInitialize) this.pictureBox2).EndInit();
      this.ResumeLayout(false);
    }
  }
}
