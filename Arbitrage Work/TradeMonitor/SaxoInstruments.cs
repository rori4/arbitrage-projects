// Decompiled with JetBrains decompiler
// Type: TradeMonitor.SaxoInstruments
// Assembly: TradeMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CEE2865B-9294-47DF-879B-0AFC01A708B6
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\TradeMonitor.exe

using SettingsProvider.WesternPipes;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TradeMonitor.Properties;

namespace TradeMonitor
{
  public class SaxoInstruments : Form
  {
    private Point mouseOffset;
    private int direction;
    private bool scrollPresed;
    private int lastPos;
    private int maxPos;
    private IContainer components;
    private Panel panel2;
    private Button button1;
    private Button button2;
    private Label label7;
    private LinkLabel linkLabel1;
    private Label label6;
    private Label label3;
    private PictureBox pictureBox2;
    private Button button5;
    private Button button6;
    private Button button8;
    private DataGridView lwInstruments;
    private DataGridViewTextBoxColumn InstrumentId;
    private DataGridViewTextBoxColumn InstrumentName;
    private DataGridViewCheckBoxColumn Active;
    private Button bSubmit;
    private TextBox tbInstrumentId;
    private Label lInstumentId;
    private Button button3;
    private Button button4;
    private Label label4;
    private Panel panel4;
    private Panel panel6;
    private Panel panel7;
    private Panel panel5;
    private Panel scrollPanel;
    private Button bDown;
    private Button btScroll;
    private Button bUp;
    private Timer scrollTimer;

    public SaxoInstruments()
    {
      this.InitializeComponent();
      this.lwInstruments.Rows.Clear();
      lock (SettingsProvider.Settings.LocalSetting.SaxoInstruments)
      {
        int num = 1;
        foreach (SettingsProvider.Settings.SaxoValues saxoInstrument in SettingsProvider.Settings.LocalSetting.SaxoInstruments)
        {
          if (num > this.lwInstruments.Rows.Count)
            this.lwInstruments.Rows.Add();
          ++num;
          DataGridViewRow row = this.lwInstruments.Rows[this.lwInstruments.Rows.Count - 1];
          row.Cells[nameof (InstrumentId)].Value = (object) saxoInstrument.InstumentId.ToString();
          row.Cells[nameof (InstrumentName)].Value = (object) saxoInstrument.Name;
          row.Cells[nameof (Active)].Value = (object) saxoInstrument.Enabled;
        }
      }
      this.lwInstruments.FirstDisplayedScrollingRowIndex = this.lwInstruments.RowCount - 1;
      this.maxPos = this.lwInstruments.FirstDisplayedScrollingRowIndex;
      this.lwInstruments.FirstDisplayedScrollingRowIndex = 0;
      this.lwInstruments.MouseWheel += new MouseEventHandler(this.LwInstruments_MouseWheel);
    }

    private void LwInstruments_MouseWheel(object sender, MouseEventArgs e)
    {
      this.scrollContent(e.Delta < 0 ? 1 : -1);
    }

    private void button8_Click(object sender, EventArgs e)
    {
      this.panel2.Focus();
      LicenseServiceClient licenseServiceClient = new LicenseServiceClient();
      foreach (DataGridViewRow row in (IEnumerable) this.lwInstruments.Rows)
        licenseServiceClient.setInstumentState(long.Parse(row.Cells["InstrumentId"].Value.ToString()), (bool) row.Cells["Active"].Value);
      licenseServiceClient.Close();
      SettingsProvider.Settings.LocalSetting.GetInstruments();
      MainWindow.stopProcess("Saxo");
      MainWindow.startProcess("Saxo", "");
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void bSubmit_Click(object sender, EventArgs e)
    {
      this.panel2.Focus();
      Error1.showError(SettingsProvider.Settings.LocalSetting.addNewInstrument(this.tbInstrumentId.Text, this.tbInstrumentId.Text, "", 2, ""));
    }

    private void panel2_MouseDown(object sender, MouseEventArgs e)
    {
      this.mouseOffset = new Point(-e.X, -e.Y);
    }

    private void panel2_MouseMove(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      Point mousePosition = Control.MousePosition;
      mousePosition.Offset(this.mouseOffset.X, this.mouseOffset.Y);
      this.Location = mousePosition;
    }

    private void button8_MouseMove(object sender, MouseEventArgs e)
    {
      this.button8.BackgroundImage = (Image) Resources.button3x;
    }

    private void button8_MouseLeave(object sender, EventArgs e)
    {
      this.button8.BackgroundImage = (Image) Resources.button1x;
    }

    private void button4_MouseMove(object sender, MouseEventArgs e)
    {
      this.button4.BackgroundImage = (Image) Resources.button3x;
    }

    private void button4_MouseLeave(object sender, EventArgs e)
    {
      this.button4.BackgroundImage = (Image) Resources.button1x;
    }

    private void button3_MouseMove(object sender, MouseEventArgs e)
    {
      this.button3.BackgroundImage = (Image) Resources.button3x;
    }

    private void button3_MouseLeave(object sender, EventArgs e)
    {
      this.button3.BackgroundImage = (Image) Resources.button1x;
    }

    private void bSubmit_MouseMove(object sender, MouseEventArgs e)
    {
      this.bSubmit.BackgroundImage = (Image) Resources.button3x;
    }

    private void bSubmit_MouseLeave(object sender, EventArgs e)
    {
      this.bSubmit.BackgroundImage = (Image) Resources.button1x;
    }

    private void button1_MouseMove(object sender, MouseEventArgs e)
    {
      this.button1.BackgroundImage = (Image) Resources.sv2;
    }

    private void button1_MouseLeave(object sender, EventArgs e)
    {
      this.button1.BackgroundImage = (Image) Resources.sv1;
    }

    private void button2_MouseMove(object sender, MouseEventArgs e)
    {
      this.button2.BackgroundImage = (Image) Resources.zak2;
    }

    private void button2_MouseLeave(object sender, EventArgs e)
    {
      this.button2.BackgroundImage = (Image) Resources.zak1;
    }

    private void button4_Click(object sender, EventArgs e)
    {
      this.panel2.Focus();
      foreach (DataGridViewRow row in (IEnumerable) this.lwInstruments.Rows)
        row.Cells["Active"].Value = (object) false;
    }

    private void button3_Click(object sender, EventArgs e)
    {
      this.panel2.Focus();
      foreach (DataGridViewRow row in (IEnumerable) this.lwInstruments.Rows)
        row.Cells["Active"].Value = (object) true;
    }

    private void btScroll_MouseUp(object sender, MouseEventArgs e)
    {
      this.scrollPresed = false;
      this.panel2.Focus();
    }

    private void btScroll_MouseDown(object sender, MouseEventArgs e)
    {
      this.scrollPresed = true;
      this.lastPos = e.Y;
    }

    private void btScroll_MouseMove(object sender, MouseEventArgs e)
    {
      this.btScroll.BackgroundImage = (Image) Resources.Scroll2;
      if (!this.scrollPresed || Math.Abs(this.lastPos - e.Y) <= 50)
        return;
      this.lastPos = e.Y;
      if (e.Y < 0)
        this.scrollContent(-1);
      else
        this.scrollContent(1);
    }

    private void bUp_MouseDown(object sender, MouseEventArgs e)
    {
      this.direction = -1;
      this.scrollTimer.Enabled = true;
    }

    private void bUp_MouseUp(object sender, MouseEventArgs e)
    {
      this.scrollTimer.Enabled = false;
      this.panel2.Focus();
    }

    private void bDown_MouseDown(object sender, MouseEventArgs e)
    {
      this.direction = 1;
      this.scrollTimer.Enabled = true;
    }

    private void bDown_MouseUp(object sender, MouseEventArgs e)
    {
      this.scrollTimer.Enabled = false;
      this.panel2.Focus();
    }

    private void scrollContent(int scrollValue)
    {
      if (this.lwInstruments.RowCount == 0)
        return;
      int num1 = this.lwInstruments.FirstDisplayedScrollingRowIndex + scrollValue;
      if (num1 > this.lwInstruments.Rows.Count - 1)
        num1 = this.lwInstruments.FirstDisplayedScrollingRowIndex - 1;
      if (num1 < 0)
        num1 = 0;
      this.lwInstruments.FirstDisplayedScrollingRowIndex = num1;
      int num2 = this.bUp.Bottom + 1;
      int num3 = this.bDown.Top - 1 - this.btScroll.Height - num2;
      double num4 = 1.0 * (double) this.lwInstruments.FirstDisplayedScrollingRowIndex / (double) this.maxPos;
      this.btScroll.Top = num2 + (int) ((double) num3 * num4);
    }

    private void scrollTimer_Tick(object sender, EventArgs e)
    {
      this.scrollContent(this.direction);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.panel2.Focus();
      this.WindowState = FormWindowState.Minimized;
    }

    private void bUp_Click(object sender, EventArgs e)
    {
      this.scrollContent(-2);
      this.panel2.Focus();
    }

    private void bDown_Click(object sender, EventArgs e)
    {
      this.scrollContent(2);
      this.panel2.Focus();
    }

    private void bUp_MouseMove(object sender, MouseEventArgs e)
    {
      this.bUp.BackgroundImage = (Image) Resources.ScrollUp2;
    }

    private void bUp_MouseLeave(object sender, EventArgs e)
    {
      this.bUp.BackgroundImage = (Image) Resources.ScrollUp;
    }

    private void btScroll_MouseLeave(object sender, EventArgs e)
    {
      this.btScroll.BackgroundImage = (Image) Resources.Scroll;
    }

    private void bDown_MouseMove(object sender, MouseEventArgs e)
    {
      this.bDown.BackgroundImage = (Image) Resources.ScrollDn2;
    }

    private void bDown_MouseLeave(object sender, EventArgs e)
    {
      this.bDown.BackgroundImage = (Image) Resources.ScrollDn;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SaxoInstruments));
      this.lwInstruments = new DataGridView();
      this.InstrumentId = new DataGridViewTextBoxColumn();
      this.InstrumentName = new DataGridViewTextBoxColumn();
      this.Active = new DataGridViewCheckBoxColumn();
      this.scrollTimer = new Timer(this.components);
      this.panel2 = new Panel();
      this.scrollPanel = new Panel();
      this.bDown = new Button();
      this.btScroll = new Button();
      this.bUp = new Button();
      this.panel4 = new Panel();
      this.panel6 = new Panel();
      this.panel7 = new Panel();
      this.panel5 = new Panel();
      this.label4 = new Label();
      this.bSubmit = new Button();
      this.tbInstrumentId = new TextBox();
      this.button3 = new Button();
      this.lInstumentId = new Label();
      this.button4 = new Button();
      this.button1 = new Button();
      this.button2 = new Button();
      this.label7 = new Label();
      this.linkLabel1 = new LinkLabel();
      this.label6 = new Label();
      this.button8 = new Button();
      this.label3 = new Label();
      this.pictureBox2 = new PictureBox();
      this.button5 = new Button();
      this.button6 = new Button();
      ((ISupportInitialize) this.lwInstruments).BeginInit();
      this.panel2.SuspendLayout();
      this.scrollPanel.SuspendLayout();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      this.SuspendLayout();
      this.lwInstruments.AllowUserToAddRows = false;
      this.lwInstruments.BackgroundColor = Color.FromArgb(0, 22, 54);
      this.lwInstruments.BorderStyle = BorderStyle.None;
      this.lwInstruments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.lwInstruments.ColumnHeadersVisible = false;
      this.lwInstruments.Columns.AddRange((DataGridViewColumn) this.InstrumentId, (DataGridViewColumn) this.InstrumentName, (DataGridViewColumn) this.Active);
      this.lwInstruments.GridColor = Color.FromArgb(0, 43, 73);
      this.lwInstruments.Location = new Point(6, 154);
      this.lwInstruments.Name = "lwInstruments";
      this.lwInstruments.RowHeadersVisible = false;
      this.lwInstruments.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(0, 22, 54);
      this.lwInstruments.RowTemplate.DefaultCellStyle.ForeColor = Color.White;
      this.lwInstruments.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(73, 138, 243);
      this.lwInstruments.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.White;
      this.lwInstruments.ScrollBars = ScrollBars.None;
      this.lwInstruments.Size = new Size(293, 372);
      this.lwInstruments.TabIndex = 72;
      this.InstrumentId.HeaderText = "Id";
      this.InstrumentId.Name = "InstrumentId";
      this.InstrumentId.Visible = false;
      this.InstrumentName.HeaderText = "Instrument Name";
      this.InstrumentName.Name = "InstrumentName";
      this.InstrumentName.ReadOnly = true;
      this.InstrumentName.Width = 200;
      this.Active.HeaderText = "Active";
      this.Active.Name = "Active";
      this.Active.Width = 80;
      this.scrollTimer.Tick += new EventHandler(this.scrollTimer_Tick);
      this.panel2.BackColor = Color.Black;
      this.panel2.BackgroundImage = (Image) Resources.lm1655;
      this.panel2.BackgroundImageLayout = ImageLayout.Center;
      this.panel2.Controls.Add((Control) this.scrollPanel);
      this.panel2.Controls.Add((Control) this.panel4);
      this.panel2.Controls.Add((Control) this.panel6);
      this.panel2.Controls.Add((Control) this.panel7);
      this.panel2.Controls.Add((Control) this.panel5);
      this.panel2.Controls.Add((Control) this.label4);
      this.panel2.Controls.Add((Control) this.bSubmit);
      this.panel2.Controls.Add((Control) this.tbInstrumentId);
      this.panel2.Controls.Add((Control) this.button3);
      this.panel2.Controls.Add((Control) this.lInstumentId);
      this.panel2.Controls.Add((Control) this.button4);
      this.panel2.Controls.Add((Control) this.button1);
      this.panel2.Controls.Add((Control) this.button2);
      this.panel2.Controls.Add((Control) this.label7);
      this.panel2.Controls.Add((Control) this.linkLabel1);
      this.panel2.Controls.Add((Control) this.label6);
      this.panel2.Controls.Add((Control) this.button8);
      this.panel2.Controls.Add((Control) this.label3);
      this.panel2.Controls.Add((Control) this.pictureBox2);
      this.panel2.Controls.Add((Control) this.button5);
      this.panel2.Controls.Add((Control) this.button6);
      this.panel2.Location = new Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(330, 678);
      this.panel2.TabIndex = 66;
      this.panel2.MouseDown += new MouseEventHandler(this.panel2_MouseDown);
      this.panel2.MouseMove += new MouseEventHandler(this.panel2_MouseMove);
      this.scrollPanel.BackColor = Color.FromArgb(0, 22, 54);
      this.scrollPanel.Controls.Add((Control) this.bDown);
      this.scrollPanel.Controls.Add((Control) this.btScroll);
      this.scrollPanel.Controls.Add((Control) this.bUp);
      this.scrollPanel.Location = new Point(302, 135);
      this.scrollPanel.Name = "scrollPanel";
      this.scrollPanel.Size = new Size(25, 397);
      this.scrollPanel.TabIndex = 94;
      this.bDown.BackColor = Color.FromArgb(0, 77, 111);
      this.bDown.BackgroundImage = (Image) Resources.ScrollDn;
      this.bDown.Cursor = Cursors.Hand;
      this.bDown.FlatAppearance.BorderSize = 0;
      this.bDown.FlatStyle = FlatStyle.Flat;
      this.bDown.ForeColor = Color.White;
      this.bDown.Location = new Point(3, 374);
      this.bDown.Name = "bDown";
      this.bDown.Size = new Size(20, 21);
      this.bDown.TabIndex = 4;
      this.bDown.UseVisualStyleBackColor = false;
      this.bDown.Click += new EventHandler(this.bDown_Click);
      this.bDown.MouseDown += new MouseEventHandler(this.bDown_MouseDown);
      this.bDown.MouseLeave += new EventHandler(this.bDown_MouseLeave);
      this.bDown.MouseMove += new MouseEventHandler(this.bDown_MouseMove);
      this.bDown.MouseUp += new MouseEventHandler(this.bDown_MouseUp);
      this.btScroll.BackColor = Color.FromArgb(0, 77, 111);
      this.btScroll.BackgroundImage = (Image) Resources.Scroll;
      this.btScroll.Cursor = Cursors.Hand;
      this.btScroll.FlatAppearance.BorderSize = 0;
      this.btScroll.FlatStyle = FlatStyle.Flat;
      this.btScroll.Location = new Point(3, 25);
      this.btScroll.Name = "btScroll";
      this.btScroll.Size = new Size(20, 42);
      this.btScroll.TabIndex = 3;
      this.btScroll.UseVisualStyleBackColor = false;
      this.btScroll.MouseDown += new MouseEventHandler(this.btScroll_MouseDown);
      this.btScroll.MouseLeave += new EventHandler(this.btScroll_MouseLeave);
      this.btScroll.MouseMove += new MouseEventHandler(this.btScroll_MouseMove);
      this.btScroll.MouseUp += new MouseEventHandler(this.btScroll_MouseUp);
      this.bUp.BackColor = Color.FromArgb(0, 77, 111);
      this.bUp.BackgroundImage = (Image) Resources.ScrollUp;
      this.bUp.Cursor = Cursors.Hand;
      this.bUp.FlatAppearance.BorderSize = 0;
      this.bUp.FlatStyle = FlatStyle.Flat;
      this.bUp.ForeColor = Color.White;
      this.bUp.Location = new Point(3, 2);
      this.bUp.Name = "bUp";
      this.bUp.Size = new Size(20, 21);
      this.bUp.TabIndex = 1;
      this.bUp.UseVisualStyleBackColor = false;
      this.bUp.Click += new EventHandler(this.bUp_Click);
      this.bUp.MouseDown += new MouseEventHandler(this.bUp_MouseDown);
      this.bUp.MouseLeave += new EventHandler(this.bUp_MouseLeave);
      this.bUp.MouseMove += new MouseEventHandler(this.bUp_MouseMove);
      this.bUp.MouseUp += new MouseEventHandler(this.bUp_MouseUp);
      this.panel4.BackColor = Color.Gold;
      this.panel4.BackgroundImage = (Image) Resources.aa;
      this.panel4.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel4.Location = new Point(329, -8);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(31, 692);
      this.panel4.TabIndex = 90;
      this.panel6.BackColor = Color.Gold;
      this.panel6.BackgroundImage = (Image) Resources._22;
      this.panel6.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel6.Location = new Point(-6, 677);
      this.panel6.Name = "panel6";
      this.panel6.Size = new Size(362, 10);
      this.panel6.TabIndex = 88;
      this.panel7.BackColor = Color.Gold;
      this.panel7.BackgroundImage = (Image) Resources._22;
      this.panel7.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel7.Location = new Point(-10, -9);
      this.panel7.Name = "panel7";
      this.panel7.Size = new Size(362, 10);
      this.panel7.TabIndex = 89;
      this.panel5.BackColor = Color.Gold;
      this.panel5.BackgroundImage = (Image) Resources.aa;
      this.panel5.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel5.Location = new Point(-30, -5);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(31, 692);
      this.panel5.TabIndex = 91;
      this.label4.AutoSize = true;
      this.label4.BackColor = Color.Transparent;
      this.label4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label4.ForeColor = Color.Gold;
      this.label4.Location = new Point(11, 632);
      this.label4.Name = "label4";
      this.label4.Size = new Size(239, 39);
      this.label4.TabIndex = 84;
      this.label4.Text = "For add new instrument in list, enter Instrument ID\r\nNext Click \"Send\". \r\nFor Example: Instrument ID: EURUSD";
      this.bSubmit.BackColor = Color.Transparent;
      this.bSubmit.BackgroundImage = (Image) Resources.button1x;
      this.bSubmit.BackgroundImageLayout = ImageLayout.Stretch;
      this.bSubmit.Cursor = Cursors.Hand;
      this.bSubmit.FlatAppearance.BorderSize = 0;
      this.bSubmit.FlatAppearance.CheckedBackColor = Color.Transparent;
      this.bSubmit.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.bSubmit.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.bSubmit.FlatStyle = FlatStyle.Flat;
      this.bSubmit.Font = new Font("Arial", 12f);
      this.bSubmit.ForeColor = Color.White;
      this.bSubmit.Location = new Point(222, 596);
      this.bSubmit.Name = "bSubmit";
      this.bSubmit.Size = new Size(101, 28);
      this.bSubmit.TabIndex = 64;
      this.bSubmit.Text = "Send request";
      this.bSubmit.UseVisualStyleBackColor = false;
      this.bSubmit.Click += new EventHandler(this.bSubmit_Click);
      this.bSubmit.MouseLeave += new EventHandler(this.bSubmit_MouseLeave);
      this.bSubmit.MouseMove += new MouseEventHandler(this.bSubmit_MouseMove);
      this.tbInstrumentId.BackColor = Color.FromArgb(0, 22, 54);
      this.tbInstrumentId.BorderStyle = BorderStyle.None;
      this.tbInstrumentId.ForeColor = Color.White;
      this.tbInstrumentId.Location = new Point(102, 605);
      this.tbInstrumentId.Name = "tbInstrumentId";
      this.tbInstrumentId.Size = new Size(110, 13);
      this.tbInstrumentId.TabIndex = 63;
      this.tbInstrumentId.Text = "EURUSD";
      this.button3.BackColor = Color.Transparent;
      this.button3.BackgroundImage = (Image) Resources.button1x;
      this.button3.BackgroundImageLayout = ImageLayout.Stretch;
      this.button3.Cursor = Cursors.Hand;
      this.button3.FlatAppearance.BorderSize = 0;
      this.button3.FlatAppearance.CheckedBackColor = Color.Transparent;
      this.button3.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button3.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button3.FlatStyle = FlatStyle.Flat;
      this.button3.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.button3.ForeColor = Color.White;
      this.button3.Location = new Point(8, 552);
      this.button3.Name = "button3";
      this.button3.Size = new Size(101, 28);
      this.button3.TabIndex = 75;
      this.button3.Text = "Select All";
      this.button3.UseVisualStyleBackColor = false;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.button3.MouseLeave += new EventHandler(this.button3_MouseLeave);
      this.button3.MouseMove += new MouseEventHandler(this.button3_MouseMove);
      this.lInstumentId.AutoSize = true;
      this.lInstumentId.BackColor = Color.Transparent;
      this.lInstumentId.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.lInstumentId.ForeColor = Color.White;
      this.lInstumentId.Location = new Point(11, 603);
      this.lInstumentId.Name = "lInstumentId";
      this.lInstumentId.Size = new Size(81, 16);
      this.lInstumentId.TabIndex = 62;
      this.lInstumentId.Text = "Instument ID";
      this.button4.BackColor = Color.Transparent;
      this.button4.BackgroundImage = (Image) Resources.button1x;
      this.button4.BackgroundImageLayout = ImageLayout.Stretch;
      this.button4.Cursor = Cursors.Hand;
      this.button4.FlatAppearance.BorderSize = 0;
      this.button4.FlatAppearance.CheckedBackColor = Color.Transparent;
      this.button4.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button4.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button4.FlatStyle = FlatStyle.Flat;
      this.button4.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.button4.ForeColor = Color.White;
      this.button4.Location = new Point(115, 552);
      this.button4.Name = "button4";
      this.button4.Size = new Size(101, 28);
      this.button4.TabIndex = 76;
      this.button4.Text = "Clear";
      this.button4.UseVisualStyleBackColor = false;
      this.button4.Click += new EventHandler(this.button4_Click);
      this.button4.MouseLeave += new EventHandler(this.button4_MouseLeave);
      this.button4.MouseMove += new MouseEventHandler(this.button4_MouseMove);
      this.button1.BackColor = Color.Transparent;
      this.button1.BackgroundImage = (Image) Resources.sv1;
      this.button1.BackgroundImageLayout = ImageLayout.Center;
      this.button1.Cursor = Cursors.Hand;
      this.button1.FlatAppearance.BorderSize = 0;
      this.button1.FlatAppearance.CheckedBackColor = Color.Transparent;
      this.button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button1.FlatStyle = FlatStyle.Flat;
      this.button1.Location = new Point(259, 2);
      this.button1.Name = "button1";
      this.button1.Size = new Size(35, 35);
      this.button1.TabIndex = 16;
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.button1.MouseLeave += new EventHandler(this.button1_MouseLeave);
      this.button1.MouseMove += new MouseEventHandler(this.button1_MouseMove);
      this.button2.BackColor = Color.Transparent;
      this.button2.BackgroundImage = (Image) Resources.zak1;
      this.button2.BackgroundImageLayout = ImageLayout.Center;
      this.button2.Cursor = Cursors.Hand;
      this.button2.FlatAppearance.BorderSize = 0;
      this.button2.FlatAppearance.CheckedBackColor = Color.Transparent;
      this.button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button2.FlatStyle = FlatStyle.Flat;
      this.button2.Location = new Point(295, 2);
      this.button2.Name = "button2";
      this.button2.Size = new Size(35, 35);
      this.button2.TabIndex = 17;
      this.button2.UseVisualStyleBackColor = false;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.button2.MouseLeave += new EventHandler(this.button2_MouseLeave);
      this.button2.MouseMove += new MouseEventHandler(this.button2_MouseMove);
      this.label7.AutoSize = true;
      this.label7.Font = new Font("Arial", 9.75f);
      this.label7.ForeColor = Color.White;
      this.label7.Location = new Point(282, 13);
      this.label7.Name = "label7";
      this.label7.Size = new Size(0, 16);
      this.label7.TabIndex = 15;
      this.linkLabel1.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.linkLabel1.LinkColor = Color.White;
      this.linkLabel1.Location = new Point(282, 14);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(0, 15);
      this.linkLabel1.TabIndex = 14;
      this.label6.AutoSize = true;
      this.label6.Font = new Font("Arial", 9.75f);
      this.label6.ForeColor = Color.White;
      this.label6.Location = new Point(277, 13);
      this.label6.Name = "label6";
      this.label6.Size = new Size(0, 16);
      this.label6.TabIndex = 13;
      this.button8.BackColor = Color.Transparent;
      this.button8.BackgroundImage = (Image) Resources.button1x;
      this.button8.BackgroundImageLayout = ImageLayout.Stretch;
      this.button8.Cursor = Cursors.Hand;
      this.button8.FlatAppearance.BorderSize = 0;
      this.button8.FlatAppearance.CheckedBackColor = Color.Transparent;
      this.button8.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button8.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button8.FlatStyle = FlatStyle.Flat;
      this.button8.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.button8.ForeColor = Color.White;
      this.button8.Location = new Point(222, 552);
      this.button8.Name = "button8";
      this.button8.Size = new Size(101, 28);
      this.button8.TabIndex = 61;
      this.button8.Text = "Save";
      this.button8.UseVisualStyleBackColor = false;
      this.button8.Click += new EventHandler(this.button8_Click);
      this.button8.MouseLeave += new EventHandler(this.button8_MouseLeave);
      this.button8.MouseMove += new MouseEventHandler(this.button8_MouseMove);
      this.label3.AutoSize = true;
      this.label3.BackColor = Color.Transparent;
      this.label3.Font = new Font("Arial", 9.75f);
      this.label3.ForeColor = Color.FromArgb(73, 138, 243);
      this.label3.Location = new Point(37, 12);
      this.label3.Name = "label3";
      this.label3.Size = new Size(110, 16);
      this.label3.TabIndex = 12;
      this.label3.Text = "Saxo Instruments";
      this.pictureBox2.BackColor = Color.Transparent;
      this.pictureBox2.BackgroundImage = (Image) Resources.User_blue3;
      this.pictureBox2.BackgroundImageLayout = ImageLayout.Center;
      this.pictureBox2.Location = new Point(2, 2);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(35, 35);
      this.pictureBox2.TabIndex = 11;
      this.pictureBox2.TabStop = false;
      this.button5.BackgroundImage = (Image) Resources.sv1;
      this.button5.FlatAppearance.BorderSize = 0;
      this.button5.FlatStyle = FlatStyle.Flat;
      this.button5.Location = new Point(896, 7);
      this.button5.Name = "button5";
      this.button5.Size = new Size(26, 26);
      this.button5.TabIndex = 9;
      this.button5.UseVisualStyleBackColor = true;
      this.button6.BackgroundImage = (Image) Resources.zak1;
      this.button6.FlatAppearance.BorderSize = 0;
      this.button6.FlatStyle = FlatStyle.Flat;
      this.button6.Location = new Point(923, 7);
      this.button6.Name = "button6";
      this.button6.Size = new Size(26, 26);
      this.button6.TabIndex = 10;
      this.button6.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackgroundImageLayout = ImageLayout.Center;
      this.ClientSize = new Size(330, 678);
      this.Controls.Add((Control) this.lwInstruments);
      this.Controls.Add((Control) this.panel2);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Location = new Point(620, 100);
      this.Name = nameof (SaxoInstruments);
      this.Text = "Saxo Instruments";
      ((ISupportInitialize) this.lwInstruments).EndInit();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.scrollPanel.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox2).EndInit();
      this.ResumeLayout(false);
    }
  }
}
