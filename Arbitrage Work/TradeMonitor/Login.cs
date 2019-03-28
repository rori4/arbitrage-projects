// Decompiled with JetBrains decompiler
// Type: TradeMonitor.Login
// Assembly: TradeMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CEE2865B-9294-47DF-879B-0AFC01A708B6
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\TradeMonitor.exe

using SettingsProvider;
using SettingsProvider.WesternPipes;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TradeMonitor.Properties;

namespace TradeMonitor
{
  public class Login : Form
  {
    private AutoResetEvent resetEvent = new AutoResetEvent(false);
    private int pingsSent;
    private Point mouseOffset;
    private IContainer components;
    private Button button1;
    private Panel panel1;
    private Button button5;
    private Button button6;
    private Label label2;
    private Button button7;
    private Button button3;
    private Button button4;
    private Button button8;
    private Panel panel7;
    private LinkLabel linkLabel1;
    private Label label3;
    private Button button9;
    private Panel panel8;
    private Panel panel10;
    private Panel panel11;
    private LinkLabel linkLabel2;
    private LinkLabel linkLabel3;
    private Panel panel12;
    private TextBox txtResponse;
    private Button btnPing;
    private WPTextBox UserName;
    private PictureBox pictureBox3;
    private PictureBox pictureBox7;
    private PictureBox pictureBox8;
    private PictureBox pictureBox9;
    private Label label1;
    private Panel panel2;
    private Label label15;
    private Label label12;
    private Label label14;
    private Label label8;
    private Label label13;
    private Label label6;
    private Label label11;
    private Label label10;
    private Label label9;
    private Label label7;
    private WPTextBox txtIP;
    private Panel panel4;
    private Label label16;
    private Label label5;
    private Panel panel5;
    private PictureBox pictureBox4;
    private Button button2;
    private ToolTip toolTip1;
    private Panel panel3;
    private Panel panel9;
    private Panel panel6;
    private Panel panel13;
    private PictureBox pictureBox1;
    private LinkLabel linkLabel9;
    private LinkLabel linkLabel8;
    private LinkLabel linkLabel7;
    private LinkLabel linkLabel6;
    private LinkLabel linkLabel5;
    private LinkLabel linkLabel4;

    public Login()
    {
      this.InitializeComponent();
      this.panel2.Focus();
    }

    private void saveUser()
    {
      string str = this.UserName.Text.Trim();
      if (!(SettingsProvider.Settings.LocalSetting.User != str))
        return;
      SettingsProvider.Settings.LocalSetting.User = str;
      SettingsProvider.Settings.LocalSetting.storeSettings();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.panel2.Focus();
      this.saveUser();
      Error1 error1 = new Error1();
      SettingsProvider.Settings.LocalSetting.User = this.UserName.Text;
      SettingsProvider.Settings.LocalSetting.storeSettings();
      try
      {
        if (new LicenseServiceClient().checkUser(new Trader()
        {
          Account = SettingsProvider.Settings.LocalSetting.User,
          Signature = SettingsProvider.Settings.LocalSetting.Config
        }) == 0)
        {
          this.DialogResult = DialogResult.OK;
          this.Close();
        }
        else
        {
//          int num = (int) error1.ShowDialog();
          this.DialogResult = DialogResult.OK;
          this.Close();
        }
      }
      catch
      {
//        int num = (int) error1.ShowDialog();
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
    }

    private void Login_Load(object sender, EventArgs e)
    {
      SettingsProvider.Settings.LocalSetting.initLocalSettings();
      this.UserName.Text = SettingsProvider.Settings.LocalSetting.User;
      this.toolTip1.SetToolTip((Control) this.button2, "Please visit our web page http://westernpips.com");
      this.toolTip1.SetToolTip((Control) this.UserName, "Enter your email");
      this.toolTip1.SetToolTip((Control) this.txtIP, "Enter IP adress or web page");
      this.toolTip1.SetToolTip((Control) this.button9, "For registration of the new user enter client name and click Send Application");
      this.panel7.Visible = true;
      this.panel8.Visible = false;
      this.panel10.Visible = false;
      this.panel12.Visible = false;
    }

    private void reloaduser()
    {
      this.UserName.Text = SettingsProvider.Settings.LocalSetting.User;
    }

    private void button6_Click(object sender, EventArgs e)
    {
      this.panel2.Focus();
      Application.Exit();
    }

    private void button6_MouseLeave(object sender, EventArgs e)
    {
      this.button6.BackgroundImage = (Image) Resources.zak1;
    }

    private void button6_MouseMove(object sender, MouseEventArgs e)
    {
      this.button6.BackgroundImage = (Image) Resources.zak2;
    }

    private void button5_MouseLeave(object sender, EventArgs e)
    {
      this.button5.BackgroundImage = (Image) Resources.sv1;
    }

    private void button5_MouseMove(object sender, MouseEventArgs e)
    {
      this.button5.BackgroundImage = (Image) Resources.sv2;
    }

    private void button8_MouseLeave(object sender, EventArgs e)
    {
      this.button8.BackgroundImage = (Image) Resources.button1x;
    }

    private void button8_MouseMove(object sender, MouseEventArgs e)
    {
      this.button8.BackgroundImage = (Image) Resources.button3x;
    }

    private void button4_MouseLeave(object sender, EventArgs e)
    {
      this.button4.BackgroundImage = (Image) Resources.button1x;
    }

    private void button4_MouseMove(object sender, MouseEventArgs e)
    {
      this.button4.BackgroundImage = (Image) Resources.button3x;
    }

    private void button3_MouseLeave(object sender, EventArgs e)
    {
      this.button3.BackgroundImage = (Image) Resources.button1x;
    }

    private void button3_MouseMove(object sender, MouseEventArgs e)
    {
      this.button3.BackgroundImage = (Image) Resources.button3x;
    }

    private void button7_MouseLeave(object sender, EventArgs e)
    {
      this.button7.BackgroundImage = (Image) Resources.button1x;
    }

    private void button7_MouseMove(object sender, MouseEventArgs e)
    {
      this.button7.BackgroundImage = (Image) Resources.button3x;
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

    private void button8_Click(object sender, EventArgs e)
    {
      this.panel2.Focus();
      this.panel7.Visible = true;
      this.panel8.Visible = false;
      this.panel10.Visible = false;
      this.panel12.Visible = false;
    }

    private void button4_Click_1(object sender, EventArgs e)
    {
      this.panel2.Focus();
      this.panel12.Visible = true;
      this.panel10.Visible = false;
      this.panel7.Visible = false;
      this.panel8.Visible = false;
    }

    private void button3_Click(object sender, EventArgs e)
    {
      this.panel2.Focus();
      this.panel7.Visible = false;
      this.panel8.Visible = true;
      this.panel10.Visible = false;
      this.panel12.Visible = false;
    }

    private void button7_Click(object sender, EventArgs e)
    {
      this.panel2.Focus();
      this.panel10.Visible = true;
      this.panel12.Visible = false;
      this.panel7.Visible = false;
      this.panel8.Visible = false;
    }

    private void button1_MouseMove(object sender, MouseEventArgs e)
    {
      this.button1.BackgroundImage = (Image) Resources.button3x;
    }

    private void button1_MouseLeave(object sender, EventArgs e)
    {
      this.button1.BackgroundImage = (Image) Resources.button1x;
    }

    private void button9_Click(object sender, EventArgs e)
    {
      this.panel2.Focus();
      this.saveUser();
      int num1 = Security.requestRegistration();
      Error2 error2 = new Error2();
      Error3 error3 = new Error3();
      Error4 error4 = new Error4();
      Error5 error5 = new Error5();
      switch (num1)
      {
        case 0:
          int num2 = (int) error3.ShowDialog();
          break;
        case 6:
          int num3 = (int) error2.ShowDialog();
          break;
        case 100:
          int num4 = (int) error5.ShowDialog();
          break;
        default:
          int num5 = (int) error4.ShowDialog();
          break;
      }
    }

    private void button9_MouseLeave(object sender, EventArgs e)
    {
      this.button9.BackgroundImage = (Image) Resources.button1x;
    }

    private void button9_MouseMove(object sender, MouseEventArgs e)
    {
      this.button9.BackgroundImage = (Image) Resources.button3x;
    }

    private void button10_Click(object sender, EventArgs e)
    {
      this.panel2.Focus();
      this.panel8.Visible = true;
      this.panel10.Visible = false;
    }

    private void SendPing()
    {
      Ping ping = new Ping();
      ping.PingCompleted += new PingCompletedEventHandler(this.pingSender_Complete);
      ping.SendAsync(this.txtIP.Text, 5000, Encoding.ASCII.GetBytes("................................"), new PingOptions(50, true), (object) this.resetEvent);
    }

    private void pingSender_Complete(object sender, PingCompletedEventArgs e)
    {
      if (e.Cancelled)
      {
        this.txtResponse.Text += "Ping was canceled...\r\n";
        ((EventWaitHandle) e.UserState).Set();
      }
      else if (e.Error != null)
      {
        TextBox txtResponse = this.txtResponse;
        txtResponse.Text = txtResponse.Text + "An error occured: " + (object) e.Error + "\r\n";
        ((EventWaitHandle) e.UserState).Set();
      }
      else
        this.ShowPingResults(e.Reply);
    }

    public void ShowPingResults(PingReply pingResponse)
    {
      if (pingResponse == null)
      {
        this.txtResponse.Text += "There was no response.\r\n\r\n";
      }
      else
      {
        if (pingResponse.Status == IPStatus.Success)
        {
          TextBox txtResponse = this.txtResponse;
          txtResponse.Text = txtResponse.Text + "Reply from " + pingResponse.Address.ToString() + ":  bytes=" + (object) pingResponse.Buffer.Length + "  TTL=" + (object) pingResponse.Options.Ttl + "  time=" + (object) pingResponse.RoundtripTime + " ms\r\n";
        }
        else
        {
          TextBox txtResponse = this.txtResponse;
          txtResponse.Text = txtResponse.Text + "Ping was unsuccessful: " + (object) pingResponse.Status + "\r\n\r\n";
        }
        ++this.pingsSent;
        if (this.pingsSent >= 7)
          return;
        this.SendPing();
      }
    }

    private void btnPing_Click(object sender, EventArgs e)
    {
      this.panel2.Focus();
      this.pingsSent = 0;
      this.txtResponse.Clear();
      TextBox txtResponse = this.txtResponse;
      txtResponse.Text = txtResponse.Text + "Pinging " + this.txtIP.Text + " with 32 bytes of data:\r\n\r\n";
      this.SendPing();
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.com/Riskwarning.html");
    }

    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.com");
    }

    private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.ru");
    }

    private void btnPing_MouseLeave(object sender, EventArgs e)
    {
      this.btnPing.BackgroundImage = (Image) Resources.button1x;
    }

    private void btnPing_MouseMove(object sender, MouseEventArgs e)
    {
      this.btnPing.BackgroundImage = (Image) Resources.button3x;
    }

    private void button5_Click(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
      this.reloaduser();
    }

    private void panel7_Paint(object sender, PaintEventArgs e)
    {
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.panel2.Focus();
      Process.Start("http://westernpips.com");
    }

    private void toolTip1_Popup(object sender, PopupEventArgs e)
    {
    }

    private void label2_Click(object sender, EventArgs e)
    {
    }

    private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.eu");
    }

    private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.us");
    }

    private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.pro");
    }

    private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.cn");
    }

    private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.center");
    }

    private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.name");
    }

    private void linkLabel2_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel2.LinkColor = ColorTranslator.FromHtml("#498af3");
    }

    private void linkLabel2_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel2.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel3_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel3.LinkColor = ColorTranslator.FromHtml("#498af3");
    }

    private void linkLabel3_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel3.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel4_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel4.LinkColor = ColorTranslator.FromHtml("#498af3");
    }

    private void linkLabel4_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel4.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel5_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel5.LinkColor = ColorTranslator.FromHtml("#498af3");
    }

    private void linkLabel5_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel5.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel6_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel6.LinkColor = ColorTranslator.FromHtml("#498af3");
    }

    private void linkLabel6_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel6.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel7_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel7.LinkColor = ColorTranslator.FromHtml("#498af3");
    }

    private void linkLabel7_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel7.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel8_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel8.LinkColor = ColorTranslator.FromHtml("#498af3");
    }

    private void linkLabel8_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel8.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel9_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel9.LinkColor = ColorTranslator.FromHtml("#498af3");
    }

    private void linkLabel9_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel9.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void Login_Shown(object sender, EventArgs e)
    {
      this.panel2.Focus();
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Login));
      this.toolTip1 = new ToolTip(this.components);
      this.panel1 = new Panel();
      this.pictureBox1 = new PictureBox();
      this.panel13 = new Panel();
      this.panel9 = new Panel();
      this.panel6 = new Panel();
      this.panel3 = new Panel();
      this.label2 = new Label();
      this.button2 = new Button();
      this.label15 = new Label();
      this.label12 = new Label();
      this.button7 = new Button();
      this.button8 = new Button();
      this.button4 = new Button();
      this.button3 = new Button();
      this.button5 = new Button();
      this.button6 = new Button();
      this.panel10 = new Panel();
      this.pictureBox7 = new PictureBox();
      this.panel11 = new Panel();
      this.linkLabel9 = new LinkLabel();
      this.linkLabel8 = new LinkLabel();
      this.linkLabel7 = new LinkLabel();
      this.linkLabel6 = new LinkLabel();
      this.linkLabel5 = new LinkLabel();
      this.linkLabel4 = new LinkLabel();
      this.label14 = new Label();
      this.label8 = new Label();
      this.label13 = new Label();
      this.label6 = new Label();
      this.label11 = new Label();
      this.label10 = new Label();
      this.label9 = new Label();
      this.label7 = new Label();
      this.linkLabel3 = new LinkLabel();
      this.linkLabel2 = new LinkLabel();
      this.panel8 = new Panel();
      this.pictureBox4 = new PictureBox();
      this.label16 = new Label();
      this.label5 = new Label();
      this.panel7 = new Panel();
      this.label1 = new Label();
      this.panel2 = new Panel();
      this.UserName = new WPTextBox();
      this.pictureBox9 = new PictureBox();
      this.pictureBox8 = new PictureBox();
      this.button9 = new Button();
      this.linkLabel1 = new LinkLabel();
      this.label3 = new Label();
      this.button1 = new Button();
      this.panel12 = new Panel();
      this.panel5 = new Panel();
      this.txtResponse = new TextBox();
      this.panel4 = new Panel();
      this.txtIP = new WPTextBox();
      this.pictureBox3 = new PictureBox();
      this.btnPing = new Button();
      this.panel1.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.panel10.SuspendLayout();
      ((ISupportInitialize) this.pictureBox7).BeginInit();
      this.panel11.SuspendLayout();
      this.panel8.SuspendLayout();
      ((ISupportInitialize) this.pictureBox4).BeginInit();
      this.panel7.SuspendLayout();
      this.panel2.SuspendLayout();
      ((ISupportInitialize) this.pictureBox9).BeginInit();
      ((ISupportInitialize) this.pictureBox8).BeginInit();
      this.panel12.SuspendLayout();
      this.panel5.SuspendLayout();
      this.panel4.SuspendLayout();
      ((ISupportInitialize) this.pictureBox3).BeginInit();
      this.SuspendLayout();
      this.toolTip1.BackColor = Color.FromArgb(0, 80, 109);
      this.toolTip1.ForeColor = Color.White;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.Popup += new PopupEventHandler(this.toolTip1_Popup);
      this.panel1.BackColor = Color.FromArgb(20, 20, 20);
      this.panel1.BackgroundImage = (Image) Resources.LoginForm2016;
      this.panel1.Controls.Add((Control) this.pictureBox1);
      this.panel1.Controls.Add((Control) this.panel13);
      this.panel1.Controls.Add((Control) this.panel9);
      this.panel1.Controls.Add((Control) this.panel6);
      this.panel1.Controls.Add((Control) this.panel3);
      this.panel1.Controls.Add((Control) this.label2);
      this.panel1.Controls.Add((Control) this.button2);
      this.panel1.Controls.Add((Control) this.label15);
      this.panel1.Controls.Add((Control) this.label12);
      this.panel1.Controls.Add((Control) this.button7);
      this.panel1.Controls.Add((Control) this.button8);
      this.panel1.Controls.Add((Control) this.button4);
      this.panel1.Controls.Add((Control) this.button3);
      this.panel1.Controls.Add((Control) this.button5);
      this.panel1.Controls.Add((Control) this.button6);
      this.panel1.Controls.Add((Control) this.panel10);
      this.panel1.Controls.Add((Control) this.panel8);
      this.panel1.Controls.Add((Control) this.panel7);
      this.panel1.Controls.Add((Control) this.panel12);
      this.panel1.Location = new Point(0, -20);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(693, 592);
      this.panel1.TabIndex = 3;
      this.panel1.Paint += new PaintEventHandler(this.panel1_Paint);
      this.panel1.MouseDown += new MouseEventHandler(this.panel1_MouseDown);
      this.panel1.MouseMove += new MouseEventHandler(this.panel1_MouseMove);
      this.pictureBox1.BackColor = Color.Transparent;
      this.pictureBox1.BackgroundImage = (Image) Resources.User_blue3;
      this.pictureBox1.BackgroundImageLayout = ImageLayout.Center;
      this.pictureBox1.Location = new Point(3, 22);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(31, 31);
      this.pictureBox1.TabIndex = 28;
      this.pictureBox1.TabStop = false;
      this.panel13.BackColor = Color.Gold;
      this.panel13.BackgroundImage = (Image) Resources._22;
      this.panel13.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel13.Location = new Point(-7, 11);
      this.panel13.Name = "panel13";
      this.panel13.Size = new Size(755, 10);
      this.panel13.TabIndex = 11;
      this.panel9.BackColor = Color.Gold;
      this.panel9.BackgroundImage = (Image) Resources._22;
      this.panel9.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel9.Location = new Point(-12, 590);
      this.panel9.Name = "panel9";
      this.panel9.Size = new Size(755, 10);
      this.panel9.TabIndex = 10;
      this.panel6.BackColor = Color.Gold;
      this.panel6.BackgroundImage = (Image) Resources.aa;
      this.panel6.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel6.Location = new Point(-30, 10);
      this.panel6.Name = "panel6";
      this.panel6.Size = new Size(31, 614);
      this.panel6.TabIndex = 9;
      this.panel3.BackColor = Color.Gold;
      this.panel3.BackgroundImage = (Image) Resources.aa;
      this.panel3.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel3.Location = new Point(690, 12);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(31, 614);
      this.panel3.TabIndex = 8;
      this.label2.AutoSize = true;
      this.label2.BackColor = Color.Transparent;
      this.label2.Font = new Font("Arial", 9.75f);
      this.label2.ForeColor = Color.FromArgb(73, 138, 243);
      this.label2.Location = new Point(38, 30);
      this.label2.Name = "label2";
      this.label2.Size = new Size(76, 16);
      this.label2.TabIndex = 12;
      this.label2.Text = "Client Login";
      this.label2.Click += new EventHandler(this.label2_Click);
      this.button2.BackColor = Color.Transparent;
      this.button2.BackgroundImage = (Image) Resources.logotip4;
      this.button2.Cursor = Cursors.Hand;
      this.button2.FlatAppearance.BorderSize = 0;
      this.button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button2.FlatStyle = FlatStyle.Flat;
      this.button2.Location = new Point(209, 48);
      this.button2.Name = "button2";
      this.button2.Size = new Size(264, 61);
      this.button2.TabIndex = 8;
      this.button2.Tag = (object) "";
      this.button2.UseVisualStyleBackColor = false;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.label15.AutoSize = true;
      this.label15.BackColor = Color.Transparent;
      this.label15.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label15.ForeColor = Color.White;
      this.label15.Location = new Point(124, 569);
      this.label15.Name = "label15";
      this.label15.Size = new Size(209, 14);
      this.label15.TabIndex = 18;
      this.label15.Text = "Software for EA Newest PRO Version 3.7";
      this.label12.AutoSize = true;
      this.label12.BackColor = Color.Transparent;
      this.label12.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label12.ForeColor = Color.White;
      this.label12.Location = new Point(121, 555);
      this.label12.Name = "label12";
      this.label12.Size = new Size(224, 14);
      this.label12.TabIndex = 17;
      this.label12.Text = "© 2007-2017 Westernpips all rights reserved";
      this.button7.BackColor = Color.Transparent;
      this.button7.BackgroundImage = (Image) componentResourceManager.GetObject("button7.BackgroundImage");
      this.button7.BackgroundImageLayout = ImageLayout.Stretch;
      this.button7.Cursor = Cursors.Hand;
      this.button7.FlatAppearance.BorderColor = Color.FromArgb(37, 37, 38);
      this.button7.FlatAppearance.BorderSize = 0;
      this.button7.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button7.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button7.FlatStyle = FlatStyle.Flat;
      this.button7.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.button7.ForeColor = Color.White;
      this.button7.ImageAlign = ContentAlignment.TopRight;
      this.button7.Location = new Point(464, 554);
      this.button7.Name = "button7";
      this.button7.Size = new Size(101, 28);
      this.button7.TabIndex = 12;
      this.button7.Text = "Contact Us";
      this.button7.UseVisualStyleBackColor = false;
      this.button7.Click += new EventHandler(this.button7_Click);
      this.button7.MouseLeave += new EventHandler(this.button7_MouseLeave);
      this.button7.MouseMove += new MouseEventHandler(this.button7_MouseMove);
      this.button8.BackColor = Color.Transparent;
      this.button8.BackgroundImage = (Image) componentResourceManager.GetObject("button8.BackgroundImage");
      this.button8.BackgroundImageLayout = ImageLayout.Stretch;
      this.button8.Cursor = Cursors.Hand;
      this.button8.FlatAppearance.BorderColor = Color.FromArgb(37, 37, 38);
      this.button8.FlatAppearance.BorderSize = 0;
      this.button8.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button8.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button8.FlatStyle = FlatStyle.Flat;
      this.button8.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.button8.ForeColor = Color.White;
      this.button8.Location = new Point(12, 554);
      this.button8.Name = "button8";
      this.button8.Size = new Size(101, 28);
      this.button8.TabIndex = 12;
      this.button8.Text = nameof (Login);
      this.button8.UseVisualStyleBackColor = false;
      this.button8.Click += new EventHandler(this.button8_Click);
      this.button8.MouseLeave += new EventHandler(this.button8_MouseLeave);
      this.button8.MouseMove += new MouseEventHandler(this.button8_MouseMove);
      this.button4.BackColor = Color.Transparent;
      this.button4.BackgroundImage = (Image) componentResourceManager.GetObject("button4.BackgroundImage");
      this.button4.BackgroundImageLayout = ImageLayout.Stretch;
      this.button4.Cursor = Cursors.Hand;
      this.button4.FlatAppearance.BorderColor = Color.FromArgb(37, 37, 38);
      this.button4.FlatAppearance.BorderSize = 0;
      this.button4.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button4.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button4.FlatStyle = FlatStyle.Flat;
      this.button4.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.button4.ForeColor = Color.White;
      this.button4.Location = new Point(580, 554);
      this.button4.Name = "button4";
      this.button4.Size = new Size(101, 28);
      this.button4.TabIndex = 12;
      this.button4.Text = "Ping Ip";
      this.button4.UseVisualStyleBackColor = false;
      this.button4.Click += new EventHandler(this.button4_Click_1);
      this.button4.MouseLeave += new EventHandler(this.button4_MouseLeave);
      this.button4.MouseMove += new MouseEventHandler(this.button4_MouseMove);
      this.button3.BackColor = Color.Transparent;
      this.button3.BackgroundImage = (Image) componentResourceManager.GetObject("button3.BackgroundImage");
      this.button3.BackgroundImageLayout = ImageLayout.Stretch;
      this.button3.Cursor = Cursors.Hand;
      this.button3.FlatAppearance.BorderColor = Color.FromArgb(37, 37, 38);
      this.button3.FlatAppearance.BorderSize = 0;
      this.button3.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button3.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button3.FlatStyle = FlatStyle.Flat;
      this.button3.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.button3.ForeColor = Color.White;
      this.button3.Location = new Point(349, 554);
      this.button3.Name = "button3";
      this.button3.Size = new Size(101, 28);
      this.button3.TabIndex = 12;
      this.button3.Text = "About Us";
      this.button3.UseVisualStyleBackColor = false;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.button3.MouseLeave += new EventHandler(this.button3_MouseLeave);
      this.button3.MouseMove += new MouseEventHandler(this.button3_MouseMove);
      this.button5.BackColor = Color.Transparent;
      this.button5.BackgroundImage = (Image) Resources.sv1;
      this.button5.BackgroundImageLayout = ImageLayout.Center;
      this.button5.Cursor = Cursors.Hand;
      this.button5.FlatAppearance.BorderSize = 0;
      this.button5.FlatAppearance.CheckedBackColor = Color.Transparent;
      this.button5.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button5.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button5.FlatStyle = FlatStyle.Flat;
      this.button5.Location = new Point(618, 22);
      this.button5.Name = "button5";
      this.button5.Size = new Size(35, 35);
      this.button5.TabIndex = 9;
      this.button5.UseVisualStyleBackColor = false;
      this.button5.Click += new EventHandler(this.button5_Click);
      this.button5.MouseLeave += new EventHandler(this.button5_MouseLeave);
      this.button5.MouseMove += new MouseEventHandler(this.button5_MouseMove);
      this.button6.BackColor = Color.Transparent;
      this.button6.BackgroundImage = (Image) Resources.zak1;
      this.button6.BackgroundImageLayout = ImageLayout.Center;
      this.button6.Cursor = Cursors.Hand;
      this.button6.FlatAppearance.BorderSize = 0;
      this.button6.FlatAppearance.CheckedBackColor = Color.Transparent;
      this.button6.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button6.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button6.FlatStyle = FlatStyle.Flat;
      this.button6.Location = new Point(654, 22);
      this.button6.Name = "button6";
      this.button6.Size = new Size(35, 35);
      this.button6.TabIndex = 10;
      this.button6.UseVisualStyleBackColor = false;
      this.button6.Click += new EventHandler(this.button6_Click);
      this.button6.MouseLeave += new EventHandler(this.button6_MouseLeave);
      this.button6.MouseMove += new MouseEventHandler(this.button6_MouseMove);
      this.panel10.BackColor = Color.Transparent;
      this.panel10.Controls.Add((Control) this.pictureBox7);
      this.panel10.Controls.Add((Control) this.panel11);
      this.panel10.Location = new Point(36, 184);
      this.panel10.Name = "panel10";
      this.panel10.Size = new Size(237, 304);
      this.panel10.TabIndex = 27;
      this.pictureBox7.Image = (Image) Resources.CONTACT_US2016;
      this.pictureBox7.Location = new Point(50, 4);
      this.pictureBox7.Name = "pictureBox7";
      this.pictureBox7.Size = new Size(117, 25);
      this.pictureBox7.TabIndex = 5;
      this.pictureBox7.TabStop = false;
      this.panel11.BackColor = Color.FromArgb(37, 37, 38);
      this.panel11.BackgroundImage = (Image) Resources.panel2016111;
      this.panel11.BackgroundImageLayout = ImageLayout.Center;
      this.panel11.Controls.Add((Control) this.linkLabel9);
      this.panel11.Controls.Add((Control) this.linkLabel8);
      this.panel11.Controls.Add((Control) this.linkLabel7);
      this.panel11.Controls.Add((Control) this.linkLabel6);
      this.panel11.Controls.Add((Control) this.linkLabel5);
      this.panel11.Controls.Add((Control) this.linkLabel4);
      this.panel11.Controls.Add((Control) this.label14);
      this.panel11.Controls.Add((Control) this.label8);
      this.panel11.Controls.Add((Control) this.label13);
      this.panel11.Controls.Add((Control) this.label6);
      this.panel11.Controls.Add((Control) this.label11);
      this.panel11.Controls.Add((Control) this.label10);
      this.panel11.Controls.Add((Control) this.label9);
      this.panel11.Controls.Add((Control) this.label7);
      this.panel11.Controls.Add((Control) this.linkLabel3);
      this.panel11.Controls.Add((Control) this.linkLabel2);
      this.panel11.Location = new Point(-2, 38);
      this.panel11.Name = "panel11";
      this.panel11.Size = new Size(227, 249);
      this.panel11.TabIndex = 1;
      this.linkLabel9.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel9.AutoSize = true;
      this.linkLabel9.BackColor = Color.Transparent;
      this.linkLabel9.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.linkLabel9.LinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel9.Location = new Point(78, 218);
      this.linkLabel9.Name = "linkLabel9";
      this.linkLabel9.Size = new Size(139, 15);
      this.linkLabel9.TabIndex = 24;
      this.linkLabel9.TabStop = true;
      this.linkLabel9.Text = "www.westernpips.name";
      this.linkLabel9.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel9_LinkClicked);
      this.linkLabel9.MouseLeave += new EventHandler(this.linkLabel9_MouseLeave);
      this.linkLabel9.MouseMove += new MouseEventHandler(this.linkLabel9_MouseMove);
      this.linkLabel8.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel8.AutoSize = true;
      this.linkLabel8.BackColor = Color.Transparent;
      this.linkLabel8.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.linkLabel8.LinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel8.Location = new Point(78, 203);
      this.linkLabel8.Name = "linkLabel8";
      this.linkLabel8.Size = new Size(141, 15);
      this.linkLabel8.TabIndex = 23;
      this.linkLabel8.TabStop = true;
      this.linkLabel8.Text = "www.westernpips.center";
      this.linkLabel8.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel8_LinkClicked);
      this.linkLabel8.MouseLeave += new EventHandler(this.linkLabel8_MouseLeave);
      this.linkLabel8.MouseMove += new MouseEventHandler(this.linkLabel8_MouseMove);
      this.linkLabel7.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel7.AutoSize = true;
      this.linkLabel7.BackColor = Color.Transparent;
      this.linkLabel7.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.linkLabel7.LinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel7.Location = new Point(78, 188);
      this.linkLabel7.Name = "linkLabel7";
      this.linkLabel7.Size = new Size(120, 15);
      this.linkLabel7.TabIndex = 22;
      this.linkLabel7.TabStop = true;
      this.linkLabel7.Text = "www.westernpips.cn";
      this.linkLabel7.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel7_LinkClicked);
      this.linkLabel7.MouseLeave += new EventHandler(this.linkLabel7_MouseLeave);
      this.linkLabel7.MouseMove += new MouseEventHandler(this.linkLabel7_MouseMove);
      this.linkLabel6.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel6.AutoSize = true;
      this.linkLabel6.BackColor = Color.Transparent;
      this.linkLabel6.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.linkLabel6.LinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel6.Location = new Point(78, 173);
      this.linkLabel6.Name = "linkLabel6";
      this.linkLabel6.Size = new Size(125, 15);
      this.linkLabel6.TabIndex = 21;
      this.linkLabel6.TabStop = true;
      this.linkLabel6.Text = "www.westernpips.pro";
      this.linkLabel6.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel6_LinkClicked);
      this.linkLabel6.MouseLeave += new EventHandler(this.linkLabel6_MouseLeave);
      this.linkLabel6.MouseMove += new MouseEventHandler(this.linkLabel6_MouseMove);
      this.linkLabel5.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel5.AutoSize = true;
      this.linkLabel5.BackColor = Color.Transparent;
      this.linkLabel5.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.linkLabel5.LinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel5.Location = new Point(78, 158);
      this.linkLabel5.Name = "linkLabel5";
      this.linkLabel5.Size = new Size(121, 15);
      this.linkLabel5.TabIndex = 20;
      this.linkLabel5.TabStop = true;
      this.linkLabel5.Text = "www.westernpips.us";
      this.linkLabel5.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
      this.linkLabel5.MouseLeave += new EventHandler(this.linkLabel5_MouseLeave);
      this.linkLabel5.MouseMove += new MouseEventHandler(this.linkLabel5_MouseMove);
      this.linkLabel4.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel4.AutoSize = true;
      this.linkLabel4.BackColor = Color.Transparent;
      this.linkLabel4.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.linkLabel4.LinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel4.Location = new Point(78, 143);
      this.linkLabel4.Name = "linkLabel4";
      this.linkLabel4.Size = new Size(121, 15);
      this.linkLabel4.TabIndex = 19;
      this.linkLabel4.TabStop = true;
      this.linkLabel4.Text = "www.westernpips.eu";
      this.linkLabel4.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
      this.linkLabel4.MouseLeave += new EventHandler(this.linkLabel4_MouseLeave);
      this.linkLabel4.MouseMove += new MouseEventHandler(this.linkLabel4_MouseMove);
      this.label14.AutoSize = true;
      this.label14.BackColor = Color.Transparent;
      this.label14.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label14.ForeColor = Color.Black;
      this.label14.Location = new Point(78, 48);
      this.label14.Name = "label14";
      this.label14.Size = new Size(110, 15);
      this.label14.TabIndex = 18;
      this.label14.Text = "group.westernpips";
      this.label8.AutoSize = true;
      this.label8.BackColor = Color.Transparent;
      this.label8.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label8.ForeColor = Color.Black;
      this.label8.Location = new Point(78, 33);
      this.label8.Name = "label8";
      this.label8.Size = new Size(75, 15);
      this.label8.TabIndex = 17;
      this.label8.Text = "westernpips";
      this.label13.AutoSize = true;
      this.label13.BackColor = Color.Transparent;
      this.label13.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.label13.ForeColor = Color.Black;
      this.label13.Location = new Point(6, 21);
      this.label13.Name = "label13";
      this.label13.Size = new Size(45, 15);
      this.label13.TabIndex = 16;
      this.label13.Text = "Skype:";
      this.label6.AutoSize = true;
      this.label6.BackColor = Color.Transparent;
      this.label6.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.label6.ForeColor = Color.Black;
      this.label6.Location = new Point(6, 73);
      this.label6.Name = "label6";
      this.label6.Size = new Size(44, 15);
      this.label6.TabIndex = 15;
      this.label6.Text = "E-Mail:";
      this.label11.AutoSize = true;
      this.label11.BackColor = Color.Transparent;
      this.label11.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.label11.ForeColor = Color.Black;
      this.label11.Location = new Point(8, 113);
      this.label11.Name = "label11";
      this.label11.Size = new Size(36, 15);
      this.label11.TabIndex = 14;
      this.label11.Text = "Web:";
      this.label10.AutoSize = true;
      this.label10.BackColor = Color.Transparent;
      this.label10.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label10.ForeColor = Color.Black;
      this.label10.Location = new Point(78, 88);
      this.label10.Name = "label10";
      this.label10.Size = new Size(134, 15);
      this.label10.TabIndex = 13;
      this.label10.Text = "info@westernpips.com";
      this.label9.AutoSize = true;
      this.label9.BackColor = Color.Transparent;
      this.label9.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label9.ForeColor = Color.Black;
      this.label9.Location = new Point(78, 73);
      this.label9.Name = "label9";
      this.label9.Size = new Size(145, 15);
      this.label9.TabIndex = 12;
      this.label9.Text = "westernpips@gmail.com";
      this.label7.AutoSize = true;
      this.label7.BackColor = Color.Transparent;
      this.label7.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label7.ForeColor = Color.Black;
      this.label7.Location = new Point(78, 18);
      this.label7.Name = "label7";
      this.label7.Size = new Size(102, 15);
      this.label7.TabIndex = 11;
      this.label7.Text = "westernpips.com";
      this.linkLabel3.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel3.AutoSize = true;
      this.linkLabel3.BackColor = Color.Transparent;
      this.linkLabel3.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.linkLabel3.LinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel3.Location = new Point(78, 128);
      this.linkLabel3.Name = "linkLabel3";
      this.linkLabel3.Size = new Size(118, 15);
      this.linkLabel3.TabIndex = 6;
      this.linkLabel3.TabStop = true;
      this.linkLabel3.Text = "www.westernpips.ru";
      this.linkLabel3.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
      this.linkLabel3.MouseLeave += new EventHandler(this.linkLabel3_MouseLeave);
      this.linkLabel3.MouseMove += new MouseEventHandler(this.linkLabel3_MouseMove);
      this.linkLabel2.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel2.AutoSize = true;
      this.linkLabel2.BackColor = Color.Transparent;
      this.linkLabel2.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.linkLabel2.LinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel2.Location = new Point(78, 113);
      this.linkLabel2.Name = "linkLabel2";
      this.linkLabel2.Size = new Size(131, 15);
      this.linkLabel2.TabIndex = 5;
      this.linkLabel2.TabStop = true;
      this.linkLabel2.Text = "www.westernpips.com";
      this.linkLabel2.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
      this.linkLabel2.MouseLeave += new EventHandler(this.linkLabel2_MouseLeave);
      this.linkLabel2.MouseMove += new MouseEventHandler(this.linkLabel2_MouseMove);
      this.panel8.BackColor = Color.Transparent;
      this.panel8.BackgroundImage = (Image) Resources.Loginsova2016;
      this.panel8.BackgroundImageLayout = ImageLayout.Center;
      this.panel8.Controls.Add((Control) this.pictureBox4);
      this.panel8.Controls.Add((Control) this.label16);
      this.panel8.Controls.Add((Control) this.label5);
      this.panel8.Location = new Point(0, 85);
      this.panel8.Name = "panel8";
      this.panel8.Size = new Size(692, 456);
      this.panel8.TabIndex = 22;
      this.pictureBox4.BackgroundImage = (Image) Resources.HELL2016;
      this.pictureBox4.BackgroundImageLayout = ImageLayout.Center;
      this.pictureBox4.Location = new Point(17, 62);
      this.pictureBox4.Name = "pictureBox4";
      this.pictureBox4.Size = new Size(190, 34);
      this.pictureBox4.TabIndex = 7;
      this.pictureBox4.TabStop = false;
      this.label16.AutoSize = true;
      this.label16.BackColor = Color.Transparent;
      this.label16.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label16.ForeColor = Color.White;
      this.label16.Location = new Point(25, 212);
      this.label16.Name = "label16";
      this.label16.Size = new Size(406, 180);
      this.label16.TabIndex = 5;
      this.label16.Text = componentResourceManager.GetString("label16.Text");
      this.label5.AutoSize = true;
      this.label5.BackColor = Color.Transparent;
      this.label5.Font = new Font("Arial", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.label5.ForeColor = Color.FromArgb(73, 138, 243);
      this.label5.Location = new Point(25, 111);
      this.label5.Name = "label5";
      this.label5.Size = new Size(365, 80);
      this.label5.TabIndex = 6;
      this.label5.Text = componentResourceManager.GetString("label5.Text");
      this.panel7.BackColor = Color.Transparent;
      this.panel7.Controls.Add((Control) this.label1);
      this.panel7.Controls.Add((Control) this.panel2);
      this.panel7.Controls.Add((Control) this.pictureBox9);
      this.panel7.Controls.Add((Control) this.pictureBox8);
      this.panel7.Controls.Add((Control) this.button9);
      this.panel7.Controls.Add((Control) this.linkLabel1);
      this.panel7.Controls.Add((Control) this.label3);
      this.panel7.Controls.Add((Control) this.button1);
      this.panel7.ForeColor = Color.Black;
      this.panel7.Location = new Point(36, 184);
      this.panel7.Name = "panel7";
      this.panel7.Size = new Size(239, 344);
      this.panel7.TabIndex = 15;
      this.panel7.Paint += new PaintEventHandler(this.panel7_Paint);
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label1.ForeColor = Color.White;
      this.label1.Location = new Point(32, 290);
      this.label1.Name = "label1";
      this.label1.Size = new Size(63, 14);
      this.label1.TabIndex = 27;
      this.label1.Text = "I accept the";
      this.panel2.BackColor = Color.Transparent;
      this.panel2.BackgroundImage = (Image) Resources.loginennewesta2;
      this.panel2.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel2.Controls.Add((Control) this.UserName);
      this.panel2.Location = new Point(6, 38);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(214, 76);
      this.panel2.TabIndex = 16;
      this.UserName.BackColor = Color.Transparent;
      this.UserName.CharIndex = 0;
      this.UserName.Cursor = Cursors.IBeam;
      this.UserName.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.UserName.ForeColor = Color.White;
      this.UserName.ImeMode = ImeMode.On;
      this.UserName.Location = new Point(6, 29);
      this.UserName.Name = "UserName";
      this.UserName.Selecting = false;
      this.UserName.SelectionLength = 0;
      this.UserName.SelectionStart = -1;
      this.UserName.SelectText = "";
      this.UserName.Size = new Size(205, 19);
      this.UserName.TabIndex = 1;
      this.UserName.TabStop = false;
      this.UserName.Text = "westernpips@mail.ru";
      this.pictureBox9.Image = (Image) Resources.REGISTER_NEW_CLIENT2016;
      this.pictureBox9.Location = new Point(17, 183);
      this.pictureBox9.Name = "pictureBox9";
      this.pictureBox9.Size = new Size(201, 25);
      this.pictureBox9.TabIndex = 25;
      this.pictureBox9.TabStop = false;
      this.pictureBox8.Image = (Image) Resources.CLIENT_NAME2016;
      this.pictureBox8.Location = new Point(48, 4);
      this.pictureBox8.Name = "pictureBox8";
      this.pictureBox8.Size = new Size(129, 26);
      this.pictureBox8.TabIndex = 24;
      this.pictureBox8.TabStop = false;
      this.button9.BackColor = Color.Transparent;
      this.button9.BackgroundImage = (Image) componentResourceManager.GetObject("button9.BackgroundImage");
      this.button9.BackgroundImageLayout = ImageLayout.Stretch;
      this.button9.Cursor = Cursors.Hand;
      this.button9.FlatAppearance.BorderSize = 0;
      this.button9.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button9.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button9.FlatStyle = FlatStyle.Flat;
      this.button9.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.button9.ForeColor = Color.White;
      this.button9.Location = new Point(2, 218);
      this.button9.Name = "button9";
      this.button9.Size = new Size(221, 42);
      this.button9.TabIndex = 21;
      this.button9.Text = "Send Application";
      this.button9.UseVisualStyleBackColor = false;
      this.button9.Click += new EventHandler(this.button9_Click);
      this.button9.MouseLeave += new EventHandler(this.button9_MouseLeave);
      this.button9.MouseMove += new MouseEventHandler(this.button9_MouseMove);
      this.linkLabel1.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.DisabledLinkColor = Color.FromArgb(0, 22, 54);
      this.linkLabel1.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.linkLabel1.ForeColor = Color.White;
      this.linkLabel1.LinkColor = Color.FromArgb(0, 22, 54);
      this.linkLabel1.Location = new Point(101, 290);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(79, 14);
      this.linkLabel1.TabIndex = 17;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Risk Warning";
      this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label3.ForeColor = Color.White;
      this.label3.Location = new Point(24, 275);
      this.label3.Name = "label3";
      this.label3.Size = new Size(178, 14);
      this.label3.TabIndex = 16;
      this.label3.Text = "By clicking \"Login To Trade Monitor\"";
      this.button1.BackColor = Color.Transparent;
      this.button1.BackgroundImage = (Image) componentResourceManager.GetObject("button1.BackgroundImage");
      this.button1.BackgroundImageLayout = ImageLayout.Stretch;
      this.button1.Cursor = Cursors.Hand;
      this.button1.FlatAppearance.BorderSize = 0;
      this.button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button1.FlatStyle = FlatStyle.Flat;
      this.button1.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.button1.ForeColor = Color.White;
      this.button1.Location = new Point(2, 125);
      this.button1.Name = "button1";
      this.button1.Size = new Size(221, 42);
      this.button1.TabIndex = 0;
      this.button1.Text = "Login to Trade Monitor";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.button1.MouseLeave += new EventHandler(this.button1_MouseLeave);
      this.button1.MouseMove += new MouseEventHandler(this.button1_MouseMove);
      this.panel12.BackColor = Color.Transparent;
      this.panel12.Controls.Add((Control) this.panel5);
      this.panel12.Controls.Add((Control) this.panel4);
      this.panel12.Controls.Add((Control) this.pictureBox3);
      this.panel12.Controls.Add((Control) this.btnPing);
      this.panel12.Location = new Point(36, 184);
      this.panel12.Name = "panel12";
      this.panel12.Size = new Size(237, 330);
      this.panel12.TabIndex = 3;
      this.panel5.BackgroundImage = (Image) Resources.rantping2016;
      this.panel5.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel5.Controls.Add((Control) this.txtResponse);
      this.panel5.Location = new Point(5, 129);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(214, 141);
      this.panel5.TabIndex = 10;
      this.txtResponse.BackColor = Color.FromArgb(0, 80, 109);
      this.txtResponse.BorderStyle = BorderStyle.None;
      this.txtResponse.ForeColor = Color.White;
      this.txtResponse.Location = new Point(3, 3);
      this.txtResponse.Multiline = true;
      this.txtResponse.Name = "txtResponse";
      this.txtResponse.ReadOnly = true;
      this.txtResponse.Size = new Size(208, 135);
      this.txtResponse.TabIndex = 4;
      this.panel4.BackgroundImage = (Image) Resources.pingip2016;
      this.panel4.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel4.Controls.Add((Control) this.txtIP);
      this.panel4.Location = new Point(5, 27);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(214, 99);
      this.panel4.TabIndex = 9;
      this.txtIP.BackColor = Color.Transparent;
      this.txtIP.CharIndex = 0;
      this.txtIP.Cursor = Cursors.IBeam;
      this.txtIP.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.txtIP.ForeColor = Color.White;
      this.txtIP.ImeMode = ImeMode.On;
      this.txtIP.Location = new Point(5, 40);
      this.txtIP.Name = "txtIP";
      this.txtIP.Selecting = false;
      this.txtIP.SelectionLength = 0;
      this.txtIP.SelectionStart = -1;
      this.txtIP.SelectText = "";
      this.txtIP.Size = new Size(212, 19);
      this.txtIP.TabIndex = 8;
      this.txtIP.TabStop = false;
      this.txtIP.Text = "ping.lmaxtrader.com";
      this.pictureBox3.BackColor = Color.Transparent;
      this.pictureBox3.Image = (Image) Resources.PING_A_SERVER_OR_WEB_SITE20161;
      this.pictureBox3.Location = new Point(48, 4);
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.Size = new Size(135, 30);
      this.pictureBox3.TabIndex = 5;
      this.pictureBox3.TabStop = false;
      this.btnPing.BackColor = Color.Transparent;
      this.btnPing.BackgroundImage = (Image) Resources.button1x;
      this.btnPing.BackgroundImageLayout = ImageLayout.Stretch;
      this.btnPing.Cursor = Cursors.Hand;
      this.btnPing.FlatAppearance.BorderSize = 0;
      this.btnPing.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.btnPing.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.btnPing.FlatStyle = FlatStyle.Flat;
      this.btnPing.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.btnPing.ForeColor = Color.Transparent;
      this.btnPing.Location = new Point(1, 280);
      this.btnPing.Name = "btnPing";
      this.btnPing.Size = new Size(222, 42);
      this.btnPing.TabIndex = 2;
      this.btnPing.Text = "Check Ping";
      this.btnPing.UseVisualStyleBackColor = false;
      this.btnPing.Click += new EventHandler(this.btnPing_Click);
      this.btnPing.MouseLeave += new EventHandler(this.btnPing_MouseLeave);
      this.btnPing.MouseMove += new MouseEventHandler(this.btnPing_MouseMove);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.ActiveCaption;
      this.ClientSize = new Size(691, 571);
      this.Controls.Add((Control) this.panel1);
      this.ForeColor = Color.FromArgb(74, 139, 243);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (Login);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Client Login";
      this.Load += new EventHandler(this.Login_Load);
      this.Shown += new EventHandler(this.Login_Shown);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.panel10.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox7).EndInit();
      this.panel11.ResumeLayout(false);
      this.panel11.PerformLayout();
      this.panel8.ResumeLayout(false);
      this.panel8.PerformLayout();
      ((ISupportInitialize) this.pictureBox4).EndInit();
      this.panel7.ResumeLayout(false);
      this.panel7.PerformLayout();
      this.panel2.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox9).EndInit();
      ((ISupportInitialize) this.pictureBox8).EndInit();
      this.panel12.ResumeLayout(false);
      this.panel5.ResumeLayout(false);
      this.panel5.PerformLayout();
      this.panel4.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox3).EndInit();
      this.ResumeLayout(false);
    }
  }
}
