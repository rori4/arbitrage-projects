// Decompiled with JetBrains decompiler
// Type: TradeMonitor.GOOD
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
  public class GOOD : Form
  {
    private int direction;
    private bool scrollPresed;
    private int lastPos;
    private Point mouseOffset;
    private IContainer components;
    private Panel panel1;
    private Label label2;
    private PictureBox pictureBox2;
    private Button button6;
    private Button button1;
    private PictureBox pictureBox9;
    private RichTextBox richTextBox1;
    private Panel panel8;
    private Panel panel9;
    private Panel scrollPanel;
    private Button bUp;
    private Button bDown;
    private Timer scrollTimer;
    private Button btScroll;
    private Panel panel2;
    private Panel panel4;

    public GOOD()
    {
      this.InitializeComponent();
      this.MouseWheel += new MouseEventHandler(this.GOOD_MouseWheel);
      this.richTextBox1.MouseWheel += new MouseEventHandler(this.GOOD_MouseWheel);
    }

    private void GOOD_MouseWheel(object sender, MouseEventArgs e)
    {
      this.scrollContent(-e.Delta);
    }

    private void scrollContent(int scrollValue)
    {
      int num1 = this.richTextBox1.SelectionStart + scrollValue;
      if (num1 > this.richTextBox1.TextLength)
        num1 = this.richTextBox1.TextLength;
      if (num1 < 0)
        num1 = 0;
      this.richTextBox1.SelectionStart = num1;
      this.richTextBox1.ScrollToCaret();
      int num2 = this.bUp.Bottom + 1;
      int num3 = this.bDown.Top - 1 - this.btScroll.Height - num2;
      double num4 = 1.0 * (double) this.richTextBox1.SelectionStart / (double) this.richTextBox1.TextLength;
      this.btScroll.Top = num2 + (int) ((double) num3 * num4);
    }

    private void button6_MouseLeave(object sender, EventArgs e)
    {
      this.button6.BackgroundImage = (Image) Resources.zak1;
    }

    private void button6_MouseMove(object sender, MouseEventArgs e)
    {
      this.button6.BackgroundImage = (Image) Resources.zak2;
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
      Application.Exit();
    }

    private void GOOD_Load(object sender, EventArgs e)
    {
    }

    private void button1_MouseLeave(object sender, EventArgs e)
    {
      this.button1.BackgroundImage = (Image) Resources.button1x;
    }

    private void button1_MouseMove(object sender, MouseEventArgs e)
    {
      this.button1.BackgroundImage = (Image) Resources.button3x;
    }

    private void button1_Click_1(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
    }

    private void bUp_Click(object sender, EventArgs e)
    {
      this.scrollContent(-100);
    }

    private void bDown_Click(object sender, EventArgs e)
    {
      this.scrollContent(100);
    }

    private void bDown_MouseDown(object sender, MouseEventArgs e)
    {
      this.direction = 1;
      this.scrollTimer.Enabled = true;
    }

    private void bUp_MouseDown(object sender, MouseEventArgs e)
    {
      this.direction = -1;
      this.scrollTimer.Enabled = true;
    }

    private void scrollTimer_Tick(object sender, EventArgs e)
    {
      this.scrollContent(300 * this.direction);
    }

    private void bUp_MouseUp(object sender, MouseEventArgs e)
    {
      this.scrollTimer.Enabled = false;
    }

    private void bDown_MouseUp(object sender, MouseEventArgs e)
    {
      this.scrollTimer.Enabled = false;
    }

    private void btScroll_MouseDown(object sender, MouseEventArgs e)
    {
      this.scrollPresed = true;
      this.lastPos = e.Y;
    }

    private void btScroll_MouseUp(object sender, MouseEventArgs e)
    {
      this.scrollPresed = false;
    }

    private void btScroll_MouseMove(object sender, MouseEventArgs e)
    {
      this.btScroll.BackgroundImage = (Image) Resources.Scroll2;
      if (!this.scrollPresed)
        return;
      this.scrollContent(e.Y - this.lastPos);
    }

    private void bUp_MouseMove(object sender, MouseEventArgs e)
    {
      this.bUp.BackgroundImage = (Image) Resources.ScrollUp2;
    }

    private void bUp_MouseLeave(object sender, EventArgs e)
    {
      this.bUp.BackgroundImage = (Image) Resources.ScrollUp;
    }

    private void bDown_MouseMove(object sender, MouseEventArgs e)
    {
      this.bDown.BackgroundImage = (Image) Resources.ScrollDn2;
    }

    private void bDown_MouseLeave(object sender, EventArgs e)
    {
      this.bDown.BackgroundImage = (Image) Resources.ScrollDn;
    }

    private void btScroll_MouseLeave(object sender, EventArgs e)
    {
      this.btScroll.BackgroundImage = (Image) Resources.Scroll;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (GOOD));
      this.panel1 = new Panel();
      this.panel2 = new Panel();
      this.panel4 = new Panel();
      this.scrollPanel = new Panel();
      this.btScroll = new Button();
      this.bDown = new Button();
      this.bUp = new Button();
      this.button1 = new Button();
      this.panel9 = new Panel();
      this.panel8 = new Panel();
      this.pictureBox9 = new PictureBox();
      this.richTextBox1 = new RichTextBox();
      this.label2 = new Label();
      this.pictureBox2 = new PictureBox();
      this.button6 = new Button();
      this.scrollTimer = new Timer(this.components);
      this.panel1.SuspendLayout();
      this.scrollPanel.SuspendLayout();
      ((ISupportInitialize) this.pictureBox9).BeginInit();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      this.SuspendLayout();
      this.panel1.BackColor = Color.FromArgb(20, 20, 20);
      this.panel1.BackgroundImage = (Image) Resources._321dddd3;
      this.panel1.BackgroundImageLayout = ImageLayout.Center;
      this.panel1.Controls.Add((Control) this.panel2);
      this.panel1.Controls.Add((Control) this.panel4);
      this.panel1.Controls.Add((Control) this.scrollPanel);
      this.panel1.Controls.Add((Control) this.button1);
      this.panel1.Controls.Add((Control) this.panel9);
      this.panel1.Controls.Add((Control) this.panel8);
      this.panel1.Controls.Add((Control) this.pictureBox9);
      this.panel1.Controls.Add((Control) this.richTextBox1);
      this.panel1.Controls.Add((Control) this.label2);
      this.panel1.Controls.Add((Control) this.pictureBox2);
      this.panel1.Controls.Add((Control) this.button6);
      this.panel1.Location = new Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(429, 550);
      this.panel1.TabIndex = 30;
      this.panel1.MouseDown += new MouseEventHandler(this.panel1_MouseDown);
      this.panel1.MouseMove += new MouseEventHandler(this.panel1_MouseMove);
      this.panel2.BackColor = Color.Gold;
      this.panel2.BackgroundImage = (Image) Resources.aa;
      this.panel2.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel2.Location = new Point(428, -22);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(10, 593);
      this.panel2.TabIndex = 88;
      this.panel4.BackColor = Color.Gold;
      this.panel4.BackgroundImage = (Image) Resources.aa;
      this.panel4.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel4.Location = new Point(-9, -10);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(10, 593);
      this.panel4.TabIndex = 87;
      this.scrollPanel.AllowDrop = true;
      this.scrollPanel.BackColor = Color.FromArgb(0, 28, 56);
      this.scrollPanel.Controls.Add((Control) this.btScroll);
      this.scrollPanel.Controls.Add((Control) this.bDown);
      this.scrollPanel.Controls.Add((Control) this.bUp);
      this.scrollPanel.Location = new Point(398, 80);
      this.scrollPanel.Name = "scrollPanel";
      this.scrollPanel.Size = new Size(22, 410);
      this.scrollPanel.TabIndex = 54;
      this.btScroll.BackColor = Color.FromArgb(0, 77, 111);
      this.btScroll.BackgroundImage = (Image) Resources.Scroll;
      this.btScroll.Cursor = Cursors.Hand;
      this.btScroll.FlatAppearance.BorderColor = Color.FromArgb(0, 77, 111);
      this.btScroll.FlatAppearance.BorderSize = 0;
      this.btScroll.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 77, 111);
      this.btScroll.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 77, 111);
      this.btScroll.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 77, 111);
      this.btScroll.FlatStyle = FlatStyle.Flat;
      this.btScroll.Location = new Point(1, 26);
      this.btScroll.Name = "btScroll";
      this.btScroll.Size = new Size(20, 42);
      this.btScroll.TabIndex = 2;
      this.btScroll.UseVisualStyleBackColor = false;
      this.btScroll.MouseDown += new MouseEventHandler(this.btScroll_MouseDown);
      this.btScroll.MouseLeave += new EventHandler(this.btScroll_MouseLeave);
      this.btScroll.MouseMove += new MouseEventHandler(this.btScroll_MouseMove);
      this.btScroll.MouseUp += new MouseEventHandler(this.btScroll_MouseUp);
      this.bDown.BackColor = Color.FromArgb(0, 77, 111);
      this.bDown.BackgroundImage = (Image) Resources.ScrollDn;
      this.bDown.BackgroundImageLayout = ImageLayout.Center;
      this.bDown.Cursor = Cursors.Hand;
      this.bDown.FlatAppearance.BorderColor = Color.FromArgb(0, 77, 111);
      this.bDown.FlatAppearance.BorderSize = 0;
      this.bDown.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 77, 111);
      this.bDown.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 77, 111);
      this.bDown.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 77, 111);
      this.bDown.FlatStyle = FlatStyle.Flat;
      this.bDown.Font = new Font("Microsoft Sans Serif", 6f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.bDown.ForeColor = Color.White;
      this.bDown.Location = new Point(1, 389);
      this.bDown.Name = "bDown";
      this.bDown.Size = new Size(20, 21);
      this.bDown.TabIndex = 1;
      this.bDown.UseVisualStyleBackColor = false;
      this.bDown.Click += new EventHandler(this.bDown_Click);
      this.bDown.MouseDown += new MouseEventHandler(this.bDown_MouseDown);
      this.bDown.MouseLeave += new EventHandler(this.bDown_MouseLeave);
      this.bDown.MouseMove += new MouseEventHandler(this.bDown_MouseMove);
      this.bDown.MouseUp += new MouseEventHandler(this.bDown_MouseUp);
      this.bUp.BackColor = Color.FromArgb(0, 77, 111);
      this.bUp.BackgroundImage = (Image) Resources.ScrollUp;
      this.bUp.BackgroundImageLayout = ImageLayout.Center;
      this.bUp.Cursor = Cursors.Hand;
      this.bUp.FlatAppearance.BorderColor = Color.FromArgb(0, 77, 111);
      this.bUp.FlatAppearance.BorderSize = 0;
      this.bUp.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 77, 111);
      this.bUp.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 77, 111);
      this.bUp.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 77, 111);
      this.bUp.FlatStyle = FlatStyle.Flat;
      this.bUp.Font = new Font("Microsoft Sans Serif", 6f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.bUp.ForeColor = Color.White;
      this.bUp.Location = new Point(1, 0);
      this.bUp.Name = "bUp";
      this.bUp.Size = new Size(20, 21);
      this.bUp.TabIndex = 0;
      this.bUp.UseVisualStyleBackColor = false;
      this.bUp.Click += new EventHandler(this.bUp_Click);
      this.bUp.MouseDown += new MouseEventHandler(this.bUp_MouseDown);
      this.bUp.MouseLeave += new EventHandler(this.bUp_MouseLeave);
      this.bUp.MouseMove += new MouseEventHandler(this.bUp_MouseMove);
      this.bUp.MouseUp += new MouseEventHandler(this.bUp_MouseUp);
      this.button1.BackColor = Color.Transparent;
      this.button1.BackgroundImage = (Image) Resources.button1x;
      this.button1.BackgroundImageLayout = ImageLayout.Stretch;
      this.button1.Cursor = Cursors.Hand;
      this.button1.FlatAppearance.BorderSize = 0;
      this.button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button1.FlatStyle = FlatStyle.Flat;
      this.button1.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.button1.ForeColor = Color.White;
      this.button1.Location = new Point(164, 506);
      this.button1.Name = "button1";
      this.button1.Size = new Size(101, 28);
      this.button1.TabIndex = 41;
      this.button1.Text = "I agree";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new EventHandler(this.button1_Click_1);
      this.button1.MouseLeave += new EventHandler(this.button1_MouseLeave);
      this.button1.MouseMove += new MouseEventHandler(this.button1_MouseMove);
      this.panel9.BackColor = Color.Gold;
      this.panel9.BackgroundImage = (Image) Resources._22;
      this.panel9.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel9.Location = new Point(-8, 548);
      this.panel9.Name = "panel9";
      this.panel9.Size = new Size(471, 10);
      this.panel9.TabIndex = 55;
      this.panel8.BackColor = Color.Gold;
      this.panel8.BackgroundImage = (Image) Resources._22;
      this.panel8.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel8.Location = new Point(-35, -9);
      this.panel8.Name = "panel8";
      this.panel8.Size = new Size(471, 10);
      this.panel8.TabIndex = 54;
      this.pictureBox9.BackColor = Color.Transparent;
      this.pictureBox9.Image = (Image) Resources.END_USER_LICENSE_AGREEMENT2016;
      this.pictureBox9.Location = new Point(88, 42);
      this.pictureBox9.Name = "pictureBox9";
      this.pictureBox9.Size = new Size(283, 32);
      this.pictureBox9.TabIndex = 48;
      this.pictureBox9.TabStop = false;
      this.richTextBox1.BackColor = Color.FromArgb(0, 77, 111);
      this.richTextBox1.BorderStyle = BorderStyle.None;
      this.richTextBox1.ForeColor = Color.White;
      this.richTextBox1.Location = new Point(12, 80);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.ScrollBars = RichTextBoxScrollBars.None;
      this.richTextBox1.Size = new Size(387, 410);
      this.richTextBox1.TabIndex = 52;
      this.richTextBox1.Text = componentResourceManager.GetString("richTextBox1.Text");
      this.label2.AutoSize = true;
      this.label2.BackColor = Color.Transparent;
      this.label2.Font = new Font("Arial", 9.75f);
      this.label2.ForeColor = Color.FromArgb(73, 138, 243);
      this.label2.Location = new Point(36, 10);
      this.label2.Name = "label2";
      this.label2.Size = new Size(122, 16);
      this.label2.TabIndex = 12;
      this.label2.Text = "License agreement ";
      this.pictureBox2.BackColor = Color.Transparent;
      this.pictureBox2.BackgroundImage = (Image) Resources.User_blue3;
      this.pictureBox2.Location = new Point(3, 3);
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
      this.button6.Location = new Point(393, 1);
      this.button6.Name = "button6";
      this.button6.Size = new Size(35, 35);
      this.button6.TabIndex = 10;
      this.button6.UseVisualStyleBackColor = false;
      this.button6.Click += new EventHandler(this.button6_Click);
      this.button6.MouseLeave += new EventHandler(this.button6_MouseLeave);
      this.button6.MouseMove += new MouseEventHandler(this.button6_MouseMove);
      this.scrollTimer.Tick += new EventHandler(this.scrollTimer_Tick);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(0, 77, 111);
      this.ClientSize = new Size(429, 549);
      this.Controls.Add((Control) this.panel1);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.IsMdiContainer = true;
      this.Name = nameof (GOOD);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "End user license agreement";
      this.Load += new EventHandler(this.GOOD_Load);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.scrollPanel.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox9).EndInit();
      ((ISupportInitialize) this.pictureBox2).EndInit();
      this.ResumeLayout(false);
    }
  }
}
