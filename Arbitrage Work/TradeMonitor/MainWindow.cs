// Decompiled with JetBrains decompiler
// Type: TradeMonitor.MainWindow
// Assembly: TradeMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CEE2865B-9294-47DF-879B-0AFC01A708B6
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\TradeMonitor.exe

using MemoryData;
using SettingsProvider.WesternPipes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TradeMonitor.Properties;

namespace TradeMonitor
{
  public class MainWindow : Form
  {
    public static Dictionary<string, Process> processes = new Dictionary<string, Process>();
    private Thread sendThread;
    public Dictionary<string, Form> openForms;
    private Point mouseOffset;
    private MainWindow.startMmode mode;
    private IContainer components;
    private Panel panel2;
    private Label label3;
    private PictureBox pictureBox2;
    private Button button5;
    private Button button6;
    private Label label7;
    private LinkLabel linkLabel1;
    private Label label6;
    private Button button3;
    private Panel pRithmic;
    private TextBox tbRitmicsPass;
    private TextBox tbRitmicsUser;
    private Label label15;
    private Label label14;
    private Panel pLmax;
    private Panel pCQG;
    private Panel pSaxo;
    private TextBox LmaxPass;
    private TextBox LmaxUser;
    private Label label5;
    private Label label4;
    private Panel panel16;
    private LinkLabel linkLabel2;
    private Button btSaxo;
    private Button btLmax;
    private Button btCQG;
    private Button btRitthmic;
    private LinkLabel linkLabel3;
    private LinkLabel linkLabel9;
    private LinkLabel linkLabel8;
    private LinkLabel linkLabel7;
    private LinkLabel linkLabel6;
    private LinkLabel linkLabel5;
    private LinkLabel linkLabel4;
    private Label label17;
    private Label label18;
    private CheckBox cbLmax;
    private RadioButton rbEurope;
    private RadioButton rbUSA;
    private RadioButton rbDemo;
    private Button bRInstrumetns;
    private Button bSInstrumetns;
    private Button bCQGInstruments;
    private Button bLmaxInstruments;
    private Panel panel3;
    private Label label2;
    private Panel panel1;
    private Panel panel5;
    private Panel panel6;
    private ToolTip toolTip1;
    private Panel panel4;
    private Panel panel7;
    private Panel panel9;
    private Panel panel11;
    private Panel panel12;
    private CheckBox cbServerFeed;
    private Label label8;
    private WPTextBox wpTextBox1;
    private Label lUserName;
    private CheckBox cbRithmicFeed;
    private TextBox tbSaxoTimeOut;
    private Label label9;
    private CheckBox cbFixLmax;
    private LinkLabel llNews;
    private Button btStart;
    private Button btTWSStart;
    private Panel pTWS;
    private Panel panel10;
    private Button twsInstruments;
    private Label label10;
    private Button multiBut;
    private Panel panelmulti;
    private Label label1;
    private Button btIQFeed;
    private Panel pIQFeed;
    private Panel panel13;
    private Button btIqInstruments;
    private TextBox tbIQPass;
    private TextBox tbIQLogin;
    private Label lIQPass;
    private Label lIQLogin;

    public MainWindow()
    {
      this.InitializeComponent();
      SettingsProvider.Settings.LocalSetting.initLocalSettings();
      SettingsProvider.Settings.LocalSetting.GetInstruments();
      this.rbUSA.Select();
      MemoryDataProvider.CreateMTFile();
    }

    private void showSaxoMonitor()
    {
      MainWindow.startProcess("Saxo", "");
      this.panel2.Focus();
    }

    private void showTWSMonitor()
    {
      MainWindow.startProcess("TWS", "");
      this.panel2.Focus();
    }

    private void showIQFeedMonitor()
    {
      MainWindow.startProcess("IQ", "");
      this.panel2.Focus();
    }

    private void showRithmicsMonitor()
    {
      if (SettingsProvider.Settings.LocalSetting.rithmicSettings.UseServerFeed)
        MainWindow.startProcess("Feed", "");
      else
        MainWindow.startProcess("Rithmic", "");
      this.panel2.Focus();
    }

    private void showLmaxMonitor()
    {
      if (SettingsProvider.Settings.LocalSetting.Lmax.UseServerFeed)
        MainWindow.startProcess("Feed", "-lmax");
      else if (SettingsProvider.Settings.LocalSetting.LmaxMode > 1)
        MainWindow.startProcess("Fix", "");
      else
        MainWindow.startProcess("Lmax", "");
      this.panel2.Focus();
    }

    private void StartAll()
    {
      SettingsProvider.Settings.LocalSetting.initLocalSettings();
      this.initSettingPane();
      try
      {
        LicenseServiceClient licenseServiceClient = new LicenseServiceClient();
        int num = licenseServiceClient.checkUser(new Trader()
        {
          Account = SettingsProvider.Settings.LocalSetting.User,
          Signature = SettingsProvider.Settings.LocalSetting.Config
        });
        licenseServiceClient.Close();
        if (num == 0)
          Application.Exit();
        SettingsProvider.Settings.LocalSetting.sendLogin();
      }
      catch
      {
        Error1.showError("Service unavailable.");
        Application.Exit();
      }
      this.openForms = new Dictionary<string, Form>();
      this.LayoutMdi(MdiLayout.TileVertical);
      this.hideAllPanels();
    }

    private void TradeMonitor_Load(object sender, EventArgs e)
    {
      this.toolTip1.SetToolTip((Control) this.button3, "Read User Guide");
      this.lUserName.Text = SettingsProvider.Settings.LocalSetting.User;
      this.StartAll();
      this.sendThread = new Thread(new ThreadStart(this.sendData));
      this.sendThread.IsBackground = true;
      this.sendThread.Start();
      this.llNews.Links.Add(new LinkLabel.Link()
      {
        LinkData = (object) "http://westernpips.com/arbitrage_news.html"
      });
    }

    public void sendData()
    {
      while (true)
      {
        this.sendClientData();
        Thread.Sleep(TimeSpan.FromDays(1.0));
      }
    }

    private void restartAll()
    {
      this.StopAll();
      this.StartAll();
    }

    public static void stopProcess(string _process)
    {
      if (!MainWindow.processes.ContainsKey(_process))
        return;
      try
      {
        MainWindow.processes[_process].Kill();
      }
      catch
      {
      }
      MainWindow.processes.Remove(_process);
    }

    public static void startProcess(string _process, string args = "")
    {
      try
      {
        if (Process.GetProcessesByName(_process + "Monitor").Length != 0 && MainWindow.processes.ContainsKey(_process + args))
          return;
        Process process = new Process();
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.FileName = _process + "Monitor.exe";
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.Arguments = args;
        process.Start();
        if (MainWindow.processes.ContainsKey(_process + args))
          MainWindow.processes.Remove(_process + args);
        MainWindow.processes.Add(_process + args, process);
      }
      catch
      {
        Error1.showError(_process + " is not installed");
      }
    }

    private void StopAll()
    {
      foreach (Form mdiChild in this.MdiChildren)
        mdiChild.Close();
      foreach (Process process in MainWindow.processes.Values)
      {
        try
        {
          process.Kill();
        }
        catch
        {
        }
      }
      MainWindow.processes.Clear();
    }

    private void initSettingPane()
    {
      if (SettingsProvider.Settings.LocalSetting.LmaxMode < 2)
      {
        SettingsProvider.Settings.LmaxSettings lmax = SettingsProvider.Settings.LocalSetting.Lmax;
        this.LmaxUser.Text = lmax.User;
        this.LmaxPass.Text = lmax.Password;
        this.cbFixLmax.Checked = false;
      }
      else
      {
        this.LmaxUser.Text = SettingsProvider.Settings.LocalSetting.FixUser;
        this.LmaxPass.Text = SettingsProvider.Settings.LocalSetting.FixPassword;
        this.cbLmax.Checked = SettingsProvider.Settings.LocalSetting.FixDemo;
        this.cbFixLmax.Checked = true;
      }
      this.tbRitmicsUser.Text = SettingsProvider.Settings.LocalSetting.RUser;
      this.tbRitmicsPass.Text = SettingsProvider.Settings.LocalSetting.RPass;
      SettingsProvider.Settings.LocalSetting.testConnection = true;
      switch (SettingsProvider.Settings.LocalSetting.RithmicMode)
      {
        case 0:
          this.rbDemo.Checked = true;
          this.rbEurope.Checked = false;
          this.rbUSA.Checked = false;
          break;
        case 1:
          this.rbDemo.Checked = false;
          this.rbEurope.Checked = false;
          this.rbUSA.Checked = true;
          break;
        case 2:
          this.rbDemo.Checked = false;
          this.rbEurope.Checked = true;
          this.rbUSA.Checked = false;
          break;
      }
      this.cbServerFeed.Checked = SettingsProvider.Settings.LocalSetting.Lmax.UseServerFeed;
      this.cbRithmicFeed.Checked = SettingsProvider.Settings.LocalSetting.rithmicSettings.UseServerFeed;
      if (this.rbDemo.Checked)
        this.cbRithmicFeed.Enabled = false;
      else
        this.cbRithmicFeed.Enabled = true;
      if (this.cbRithmicFeed.Checked)
        this.rbDemo.Enabled = false;
      else
        this.rbDemo.Enabled = true;
      this.tbSaxoTimeOut.Text = SettingsProvider.Settings.LocalSetting.SaxoTimeout.ToString();
      if (this.tbSaxoTimeOut.Text == "")
        this.tbSaxoTimeOut.Text = "2";
      this.tbIQLogin.Text = SettingsProvider.Settings.LocalSetting.IQFeedParams.Login;
      this.tbIQPass.Text = SettingsProvider.Settings.LocalSetting.IQFeedParams.Password;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (SettingsProvider.Settings.LocalSetting != null)
      {
        SettingsProvider.Settings.LocalSetting.Lmax = new SettingsProvider.Settings.LmaxSettings()
        {
          User = this.LmaxUser.Text,
          Password = this.LmaxPass.Text
        };
        SettingsProvider.Settings.LocalSetting.RUser = this.tbRitmicsUser.Text;
        SettingsProvider.Settings.LocalSetting.RPass = this.tbRitmicsPass.Text;
        SettingsProvider.Settings.LocalSetting.IQFeedParams.Login = this.tbIQLogin.Text;
        SettingsProvider.Settings.LocalSetting.IQFeedParams.Password = this.tbIQPass.Text;
        SettingsProvider.Settings.LocalSetting.storeSettings();
      }
      this.restartAll();
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

    private void button5_MouseLeave(object sender, EventArgs e)
    {
      this.button5.BackgroundImage = (Image) Resources.sv1;
    }

    private void button5_MouseMove(object sender, MouseEventArgs e)
    {
      this.button5.BackgroundImage = (Image) Resources.sv2;
    }

    private void button6_MouseLeave(object sender, EventArgs e)
    {
      this.button6.BackgroundImage = (Image) Resources.zak1;
    }

    private void button6_MouseMove(object sender, MouseEventArgs e)
    {
      this.button6.BackgroundImage = (Image) Resources.zak2;
    }

    private void button6_Click(object sender, EventArgs e)
    {
      this.StopAll();
      Application.Exit();
    }

    private void button5_Click(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }

    private void button13_MouseMove(object sender, MouseEventArgs e)
    {
      this.btSaxo.BackgroundImage = (Image) Resources.saxo20162;
    }

    private void button13_MouseLeave(object sender, EventArgs e)
    {
      this.btSaxo.BackgroundImage = (Image) Resources.saxo20161;
    }

    private void button12_MouseMove(object sender, MouseEventArgs e)
    {
      this.btLmax.BackgroundImage = (Image) Resources.lmax20162;
    }

    private void button12_MouseLeave(object sender, EventArgs e)
    {
      this.btLmax.BackgroundImage = (Image) Resources.lmax20161;
    }

    private void button7_MouseMove(object sender, MouseEventArgs e)
    {
      this.btRitthmic.BackgroundImage = (Image) Resources.r01bbb;
    }

    private void button7_MouseLeave(object sender, EventArgs e)
    {
      this.btRitthmic.BackgroundImage = (Image) Resources.r01a;
    }

    private void button11_MouseMove(object sender, MouseEventArgs e)
    {
      this.btCQG.BackgroundImage = (Image) Resources.cqg20162;
    }

    private void button11_MouseLeave(object sender, EventArgs e)
    {
      this.btCQG.BackgroundImage = (Image) Resources.cqg20161;
    }

    private void button8_MouseMove(object sender, MouseEventArgs e)
    {
      this.btStart.BackgroundImage = (Image) Resources.start2;
    }

    private void button8_MouseLeave(object sender, EventArgs e)
    {
      this.btStart.BackgroundImage = (Image) Resources.start1;
    }

    private void button3_Click(object sender, EventArgs e)
    {
      Process.Start("http://westernpips.com/arbitrage_guide.html");
    }

    private void button2_Click(object sender, EventArgs e)
    {
      MainWindow.stopProcess("Rithmic");
      this.showRithmicsMonitor();
    }

    private void bLmaxStart_Click(object sender, EventArgs e)
    {
      this.multiBut.Visible = true;
      this.btStart.Visible = false;
      try
      {
        SettingsProvider.Settings.LocalSetting.RUser = this.tbRitmicsUser.Text;
        SettingsProvider.Settings.LocalSetting.RPass = this.tbRitmicsPass.Text;
        int result = 0;
        SettingsProvider.Settings.LocalSetting.SaxoTimeout = !int.TryParse(this.tbSaxoTimeOut.Text, out result) ? 2 : result;
        if (this.cbFixLmax.Checked)
        {
          SettingsProvider.Settings.LocalSetting.FixDemo = this.cbLmax.Checked;
          SettingsProvider.Settings.LocalSetting.FixUser = this.LmaxUser.Text;
          SettingsProvider.Settings.LocalSetting.FixPassword = this.LmaxPass.Text;
          SettingsProvider.Settings.LocalSetting.storeFixSettings();
        }
        else
        {
          SettingsProvider.Settings.LmaxSettings lmax = SettingsProvider.Settings.LocalSetting.Lmax;
          lmax.User = this.LmaxUser.Text;
          lmax.Password = this.LmaxPass.Text;
          lmax.UseServerFeed = this.cbServerFeed.Checked;
          SettingsProvider.Settings.LocalSetting.Lmax = lmax;
        }
        SettingsProvider.Settings.LocalSetting.IQFeedParams.Login = this.tbIQLogin.Text;
        SettingsProvider.Settings.LocalSetting.IQFeedParams.Password = this.tbIQPass.Text;
        SettingsProvider.Settings.LocalSetting.storeSettings();
        this.hideAllPanels();
        switch (this.mode)
        {
          case MainWindow.startMmode.CQG:
            this.showCQGMonitor();
            break;
          case MainWindow.startMmode.Rithmics:
            this.showRithmicsMonitor();
            break;
          case MainWindow.startMmode.Saxo:
            this.showSaxoMonitor();
            break;
          case MainWindow.startMmode.Lmax:
            this.showLmaxMonitor();
            break;
          case MainWindow.startMmode.TWS:
            this.showTWSMonitor();
            break;
          case MainWindow.startMmode.IQFeed:
            this.showIQFeedMonitor();
            break;
        }
        this.panel3.Visible = true;
      }
      catch
      {
      }
    }

    private void hideAllPanels()
    {
      this.pTWS.Visible = false;
      this.pSaxo.Visible = false;
      this.pLmax.Visible = false;
      this.pRithmic.Visible = false;
      this.pCQG.Visible = false;
      this.panelmulti.Visible = false;
      this.pIQFeed.Visible = false;
      this.panel3.Visible = true;
      this.panel16.Visible = true;
    }

    private void button7_Click(object sender, EventArgs e)
    {
      this.pRithmic.Visible = true;
      this.btStart.Visible = true;
      this.multiBut.Visible = false;
      this.panel3.Visible = false;
      this.pCQG.Visible = false;
      this.pLmax.Visible = false;
      this.pIQFeed.Visible = false;
      this.pSaxo.Visible = false;
      this.pTWS.Visible = false;
      this.panelmulti.Visible = false;
      this.panel2.Focus();
      this.mode = MainWindow.startMmode.Rithmics;
    }

    private void bCQG_Click(object sender, EventArgs e)
    {
      this.pCQG.Visible = true;
      this.multiBut.Visible = false;
      this.btStart.Visible = true;
      this.panel3.Visible = false;
      this.pRithmic.Visible = false;
      this.pLmax.Visible = false;
      this.pIQFeed.Visible = false;
      this.pSaxo.Visible = false;
      this.pTWS.Visible = false;
      this.panelmulti.Visible = false;
      this.panel2.Focus();
      this.mode = MainWindow.startMmode.CQG;
    }

    private void bLamx_Click(object sender, EventArgs e)
    {
      this.pLmax.Visible = true;
      this.btStart.Visible = true;
      this.multiBut.Visible = false;
      this.panel3.Visible = false;
      this.pRithmic.Visible = false;
      this.pCQG.Visible = false;
      this.pSaxo.Visible = false;
      this.pIQFeed.Visible = false;
      this.pTWS.Visible = false;
      this.panelmulti.Visible = false;
      this.panel2.Focus();
      this.mode = MainWindow.startMmode.Lmax;
    }

    private void button13_Click(object sender, EventArgs e)
    {
      this.pSaxo.Visible = true;
      this.multiBut.Visible = false;
      this.btStart.Visible = true;
      this.panel3.Visible = false;
      this.pRithmic.Visible = false;
      this.pCQG.Visible = false;
      this.pLmax.Visible = false;
      this.pIQFeed.Visible = false;
      this.pTWS.Visible = false;
      this.panelmulti.Visible = false;
      this.panel2.Focus();
      this.mode = MainWindow.startMmode.Saxo;
    }

    private void button4_Click(object sender, EventArgs e)
    {
      SettingsProvider.Settings.LocalSetting.RUser = this.tbRitmicsUser.Text;
      SettingsProvider.Settings.LocalSetting.RPass = this.tbRitmicsPass.Text;
      SettingsProvider.Settings.LocalSetting.storeSettings();
      this.showRithmicsMonitor();
      this.pRithmic.Visible = false;
      this.panelmulti.Visible = false;
      this.panel3.Visible = true;
    }

    private void button9_Click(object sender, EventArgs e)
    {
      int result = 0;
      SettingsProvider.Settings.LocalSetting.SaxoTimeout = !int.TryParse(this.tbSaxoTimeOut.Text, out result) ? 2 : result;
      SettingsProvider.Settings.LocalSetting.storeSettings();
      MainWindow.startProcess("Saxo", "");
      this.showSaxoMonitor();
      this.pSaxo.Visible = false;
      this.panel3.Visible = true;
      this.panelmulti.Visible = false;
      this.panel2.Focus();
    }

    private void showCQGMonitor()
    {
      MainWindow.startProcess("CQG", "");
      this.panel2.Focus();
      this.pCQG.Visible = true;
    }

    private void btCQGStart_Click(object sender, EventArgs e)
    {
      this.showCQGMonitor();
      this.panel3.Visible = true;
      this.panel2.Focus();
    }

    private void linkLabel2_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel2.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel2_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel2.LinkColor = ColorTranslator.FromHtml("#FFFFFF");
    }

    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.pro");
    }

    private void linkLabel3_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel3.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel3_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel3.LinkColor = ColorTranslator.FromHtml("#FFFFFF");
    }

    private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.us");
    }

    private void linkLabel4_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel4.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel4_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel4.LinkColor = ColorTranslator.FromHtml("#FFFFFF");
    }

    private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.com");
    }

    private void linkLabel5_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel5.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel5_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel5.LinkColor = ColorTranslator.FromHtml("#FFFFFF");
    }

    private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.eu");
    }

    private void linkLabel6_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel6.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel6_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel6.LinkColor = ColorTranslator.FromHtml("#FFFFFF");
    }

    private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.center");
    }

    private void linkLabel7_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel7.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel7_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel7.LinkColor = ColorTranslator.FromHtml("#FFFFFF");
    }

    private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.name");
    }

    private void linkLabel8_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel8.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel8_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel8.LinkColor = ColorTranslator.FromHtml("#FFFFFF");
    }

    private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.cn");
    }

    private void linkLabel9_MouseMove(object sender, MouseEventArgs e)
    {
      this.linkLabel9.LinkColor = ColorTranslator.FromHtml("#001636");
    }

    private void linkLabel9_MouseLeave(object sender, EventArgs e)
    {
      this.linkLabel9.LinkColor = ColorTranslator.FromHtml("#FFFFFF");
    }

    private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://westernpips.ru");
    }

    private void cbDemo_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void button2_Click_2(object sender, EventArgs e)
    {
      Process.Start("http://westernpips.com");
    }

    private void TradeMonitor_FormClosing(object sender, FormClosingEventArgs e)
    {
      MemoryDataProvider.closeMTFile();
      if (this.sendThread == null)
        return;
      this.sendThread.Abort();
    }

    private void cbRithmic_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.initSettingPane();
    }

    private void cbLmax_CheckedChanged(object sender, EventArgs e)
    {
      int mode = !this.cbFixLmax.Checked ? (this.cbLmax.Checked ? 1 : 0) : (this.cbLmax.Checked ? 3 : 2);
      SettingsProvider.Settings.LocalSetting.setModeLmax(mode);
      SettingsProvider.Settings.LocalSetting.initLamxSettings();
      this.initSettingPane();
      this.cbServerFeed.Enabled = !this.cbLmax.Checked && !this.cbFixLmax.Checked;
    }

    private void rbDemo_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.rbDemo.Checked)
        return;
      SettingsProvider.Settings.LocalSetting.setMode(0);
      this.initSettingPane();
    }

    private void rbUSA_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.rbUSA.Checked)
        return;
      SettingsProvider.Settings.LocalSetting.setMode(1);
      this.initSettingPane();
    }

    private void rbEurope_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.rbEurope.Checked)
        return;
      SettingsProvider.Settings.LocalSetting.setMode(2);
      this.initSettingPane();
    }

    private void bRInstrumetns_Click(object sender, EventArgs e)
    {
      RInstruments rinstruments = new RInstruments();
      this.panel2.Focus();
      rinstruments.Show();
    }

    private void bSInstrumetns_Click(object sender, EventArgs e)
    {
      SaxoInstruments saxoInstruments = new SaxoInstruments();
      this.panel2.Focus();
      saxoInstruments.Show();
    }

    private void bCQGInstruments_Click(object sender, EventArgs e)
    {
      CQGInstruments cqgInstruments = new CQGInstruments();
      this.panel2.Focus();
      cqgInstruments.Show();
    }

    private void bLmaxInstruments_Click(object sender, EventArgs e)
    {
      LmaxInstrumetns lmaxInstrumetns = new LmaxInstrumetns();
      this.panel2.Focus();
      lmaxInstrumetns.Show();
    }

    private void button3_MouseMove(object sender, MouseEventArgs e)
    {
      this.button3.BackgroundImage = (Image) Resources.vop2;
    }

    private void button3_MouseLeave(object sender, EventArgs e)
    {
      this.button3.BackgroundImage = (Image) Resources.vop1;
    }

    private void bRInstrumetns_MouseLeave(object sender, EventArgs e)
    {
      this.bRInstrumetns.BackgroundImage = (Image) Resources.button1x;
    }

    private void bRInstrumetns_MouseMove(object sender, MouseEventArgs e)
    {
      this.bRInstrumetns.BackgroundImage = (Image) Resources.button3x;
    }

    private void bLmaxInstruments_MouseMove(object sender, MouseEventArgs e)
    {
      this.bLmaxInstruments.BackgroundImage = (Image) Resources.button3x;
    }

    private void bLmaxInstruments_MouseLeave(object sender, EventArgs e)
    {
      this.bLmaxInstruments.BackgroundImage = (Image) Resources.button1x;
    }

    private void bCQGInstruments_MouseMove(object sender, MouseEventArgs e)
    {
      this.bCQGInstruments.BackgroundImage = (Image) Resources.button3x;
    }

    private void bCQGInstruments_MouseLeave(object sender, EventArgs e)
    {
      this.bCQGInstruments.BackgroundImage = (Image) Resources.button1x;
    }

    private void bSInstrumetns_MouseMove(object sender, MouseEventArgs e)
    {
      this.bSInstrumetns.BackgroundImage = (Image) Resources.button3x;
    }

    private void bSInstrumetns_MouseLeave(object sender, EventArgs e)
    {
      this.bSInstrumetns.BackgroundImage = (Image) Resources.button1x;
    }

    private void cbRithmicFeed_CheckedChanged(object sender, EventArgs e)
    {
      SettingsProvider.Settings.RithmicSetting rithmicSettings = SettingsProvider.Settings.LocalSetting.rithmicSettings;
      rithmicSettings.UseServerFeed = this.cbRithmicFeed.Checked;
      SettingsProvider.Settings.LocalSetting.rithmicSettings = rithmicSettings;
      SettingsProvider.Settings.LocalSetting.storeRithmicSettings();
      this.initSettingPane();
    }

    private void panel13_Paint(object sender, PaintEventArgs e)
    {
    }

    private void tbSaxoTimeOut_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
        return;
      e.Handled = true;
    }

    private void bNewInstrument_Click(object sender, EventArgs e)
    {
      NewInstrumentRequest instrumentRequest = new NewInstrumentRequest();
      this.panel2.Focus();
      instrumentRequest.Show();
    }

    private void cbServerFeed_CheckedChanged(object sender, EventArgs e)
    {
      if (this.cbServerFeed.Checked)
        this.cbLmax.Enabled = false;
      else
        this.cbLmax.Enabled = true;
    }

    private void sendTimes_Tick(object sender, EventArgs e)
    {
      this.sendClientData();
    }

    public void sendClientData()
    {
      if (!Directory.Exists("C:\\ProgramData\\Westernpips"))
        return;
      string logServerAddr = new LicenseServiceClient().getLogServerAddr();
      foreach (string file in Directory.GetFiles("C:\\ProgramData\\Westernpips"))
      {
        try
        {
          Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
          socket.Connect(logServerAddr, 10187);
          string str1 = SettingsProvider.Settings.LocalSetting.User + "\\" + Path.GetFileNameWithoutExtension(file);
          string str2 = File.ReadAllText(file);
          string str3 = "Westernpips\\" + str1;
          string str4 = str3.Length.ToString();
          while (str4.Length < 3)
            str4 = "0" + str4;
          string s = str4 + str3 + str2;
          socket.Send(Encoding.ASCII.GetBytes(s));
          socket.Shutdown(SocketShutdown.Both);
          socket.Close();
        }
        catch (Exception ex)
        {
        }
      }
    }

    private void cbFixLmax_CheckedChanged(object sender, EventArgs e)
    {
      int mode = !this.cbFixLmax.Checked ? (this.cbLmax.Checked ? 1 : 0) : (this.cbLmax.Checked ? 3 : 2);
      SettingsProvider.Settings.LocalSetting.setModeLmax(mode);
      this.initSettingPane();
      this.cbServerFeed.Enabled = !this.cbLmax.Checked && !this.cbFixLmax.Checked;
    }

    private void LmaxUser_TextChanged(object sender, EventArgs e)
    {
    }

    private void llNews_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start(e.Link.LinkData as string);
    }

    private void btTWSStart_Click(object sender, EventArgs e)
    {
      this.multiBut.Visible = false;
      this.pLmax.Visible = false;
      this.panelmulti.Visible = false;
      this.btStart.Visible = true;
      this.panel3.Visible = false;
      this.pRithmic.Visible = false;
      this.pCQG.Visible = false;
      this.pSaxo.Visible = false;
      this.pIQFeed.Visible = false;
      this.pTWS.Visible = true;
      this.panel2.Focus();
      this.mode = MainWindow.startMmode.TWS;
    }

    private void twsInstruments_Click(object sender, EventArgs e)
    {
      TWSInstrumetns twsInstrumetns = new TWSInstrumetns();
      this.panel2.Focus();
      twsInstrumetns.Show();
    }

    private void multiBut_MouseMove(object sender, MouseEventArgs e)
    {
      this.multiBut.BackgroundImage = (Image) Resources.multi2;
    }

    private void multiBut_MouseLeave(object sender, EventArgs e)
    {
      this.multiBut.BackgroundImage = (Image) Resources.multi1;
    }

    private void multiBut_Click(object sender, EventArgs e)
    {
      this.panel16.Visible = false;
      this.panel3.Visible = false;
      this.panelmulti.Visible = true;
      this.panel2.Focus();
    }

    private void panel2_Paint(object sender, PaintEventArgs e)
    {
    }

    private void panel3_Paint(object sender, PaintEventArgs e)
    {
    }

    private void panel2_Click(object sender, EventArgs e)
    {
      this.multiBut.Visible = true;
      this.btStart.Visible = false;
      this.hideAllPanels();
      this.panel3.Visible = true;
      this.panel16.Visible = true;
    }

    private void lUserName_Click(object sender, EventArgs e)
    {
      this.multiBut.Visible = true;
      this.btStart.Visible = false;
      this.hideAllPanels();
      this.panel3.Visible = true;
      this.panel16.Visible = true;
    }

    private void btTWSStart_MouseMove(object sender, MouseEventArgs e)
    {
      this.btTWSStart.BackgroundImage = (Image) Resources.button3x;
    }

    private void btTWSStart_MouseLeave(object sender, EventArgs e)
    {
      this.btTWSStart.BackgroundImage = (Image) Resources.button1x;
    }

    private void button1_MouseLeave(object sender, EventArgs e)
    {
      this.btIQFeed.BackgroundImage = (Image) Resources.button1x;
    }

    private void button1_MouseMove(object sender, MouseEventArgs e)
    {
      this.btIQFeed.BackgroundImage = (Image) Resources.button3x;
    }

    private void twsInstruments_MouseMove(object sender, MouseEventArgs e)
    {
      this.twsInstruments.BackgroundImage = (Image) Resources.button3x;
    }

    private void twsInstruments_MouseLeave(object sender, EventArgs e)
    {
      this.twsInstruments.BackgroundImage = (Image) Resources.button1x;
    }

    private void btIqInstruments_Click(object sender, EventArgs e)
    {
      IQInstruments iqInstruments = new IQInstruments();
      this.panel1.Focus();
      iqInstruments.Show();
    }

    private void btIQFeed_Click(object sender, EventArgs e)
    {
      this.multiBut.Visible = false;
      this.pLmax.Visible = false;
      this.panelmulti.Visible = false;
      this.btStart.Visible = true;
      this.panel3.Visible = false;
      this.pRithmic.Visible = false;
      this.pCQG.Visible = false;
      this.pSaxo.Visible = false;
      this.pIQFeed.Visible = true;
      this.pTWS.Visible = false;
      this.mode = MainWindow.startMmode.IQFeed;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MainWindow));
      this.toolTip1 = new ToolTip(this.components);
      this.panel5 = new Panel();
      this.panel2 = new Panel();
      this.llNews = new LinkLabel();
      this.lUserName = new Label();
      this.label8 = new Label();
      this.panel4 = new Panel();
      this.panel6 = new Panel();
      this.panel1 = new Panel();
      this.label2 = new Label();
      this.label7 = new Label();
      this.linkLabel1 = new LinkLabel();
      this.btLmax = new Button();
      this.btCQG = new Button();
      this.button3 = new Button();
      this.label6 = new Label();
      this.btSaxo = new Button();
      this.label3 = new Label();
      this.btRitthmic = new Button();
      this.pictureBox2 = new PictureBox();
      this.button5 = new Button();
      this.button6 = new Button();
      this.wpTextBox1 = new WPTextBox();
      this.multiBut = new Button();
      this.btStart = new Button();
      this.pIQFeed = new Panel();
      this.tbIQPass = new TextBox();
      this.tbIQLogin = new TextBox();
      this.lIQPass = new Label();
      this.lIQLogin = new Label();
      this.panel13 = new Panel();
      this.btIqInstruments = new Button();
      this.pTWS = new Panel();
      this.panel10 = new Panel();
      this.twsInstruments = new Button();
      this.label10 = new Label();
      this.panel3 = new Panel();
      this.panel16 = new Panel();
      this.linkLabel9 = new LinkLabel();
      this.linkLabel8 = new LinkLabel();
      this.linkLabel7 = new LinkLabel();
      this.linkLabel6 = new LinkLabel();
      this.linkLabel5 = new LinkLabel();
      this.linkLabel4 = new LinkLabel();
      this.linkLabel3 = new LinkLabel();
      this.linkLabel2 = new LinkLabel();
      this.pCQG = new Panel();
      this.panel7 = new Panel();
      this.bCQGInstruments = new Button();
      this.label17 = new Label();
      this.pRithmic = new Panel();
      this.cbRithmicFeed = new CheckBox();
      this.panel9 = new Panel();
      this.bRInstrumetns = new Button();
      this.rbEurope = new RadioButton();
      this.rbUSA = new RadioButton();
      this.rbDemo = new RadioButton();
      this.tbRitmicsPass = new TextBox();
      this.tbRitmicsUser = new TextBox();
      this.label15 = new Label();
      this.label14 = new Label();
      this.pSaxo = new Panel();
      this.label9 = new Label();
      this.tbSaxoTimeOut = new TextBox();
      this.panel11 = new Panel();
      this.bSInstrumetns = new Button();
      this.label18 = new Label();
      this.pLmax = new Panel();
      this.cbFixLmax = new CheckBox();
      this.cbServerFeed = new CheckBox();
      this.panel12 = new Panel();
      this.bLmaxInstruments = new Button();
      this.cbLmax = new CheckBox();
      this.LmaxPass = new TextBox();
      this.LmaxUser = new TextBox();
      this.label5 = new Label();
      this.label4 = new Label();
      this.panelmulti = new Panel();
      this.btIQFeed = new Button();
      this.label1 = new Label();
      this.btTWSStart = new Button();
      this.panel2.SuspendLayout();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      this.pIQFeed.SuspendLayout();
      this.pTWS.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel16.SuspendLayout();
      this.pCQG.SuspendLayout();
      this.pRithmic.SuspendLayout();
      this.pSaxo.SuspendLayout();
      this.pLmax.SuspendLayout();
      this.panelmulti.SuspendLayout();
      this.SuspendLayout();
      this.toolTip1.BackColor = Color.FromArgb(0, 80, 109);
      this.toolTip1.ForeColor = Color.White;
      this.toolTip1.IsBalloon = true;
      this.panel5.BackColor = Color.Gold;
      this.panel5.BackgroundImage = (Image) Resources.aa;
      this.panel5.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel5.Location = new Point(702, -7);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(31, 787);
      this.panel5.TabIndex = 11;
      this.panel2.BackColor = Color.FromArgb(20, 20, 20);
      this.panel2.BackgroundImage = (Image) Resources.MainForm20162;
      this.panel2.Controls.Add((Control) this.llNews);
      this.panel2.Controls.Add((Control) this.lUserName);
      this.panel2.Controls.Add((Control) this.label8);
      this.panel2.Controls.Add((Control) this.panel4);
      this.panel2.Controls.Add((Control) this.panel6);
      this.panel2.Controls.Add((Control) this.panel1);
      this.panel2.Controls.Add((Control) this.label2);
      this.panel2.Controls.Add((Control) this.label7);
      this.panel2.Controls.Add((Control) this.linkLabel1);
      this.panel2.Controls.Add((Control) this.btLmax);
      this.panel2.Controls.Add((Control) this.btCQG);
      this.panel2.Controls.Add((Control) this.button3);
      this.panel2.Controls.Add((Control) this.label6);
      this.panel2.Controls.Add((Control) this.btSaxo);
      this.panel2.Controls.Add((Control) this.label3);
      this.panel2.Controls.Add((Control) this.btRitthmic);
      this.panel2.Controls.Add((Control) this.pictureBox2);
      this.panel2.Controls.Add((Control) this.button5);
      this.panel2.Controls.Add((Control) this.button6);
      this.panel2.Controls.Add((Control) this.wpTextBox1);
      this.panel2.Controls.Add((Control) this.multiBut);
      this.panel2.Controls.Add((Control) this.btStart);
      this.panel2.Controls.Add((Control) this.pIQFeed);
      this.panel2.Controls.Add((Control) this.pTWS);
      this.panel2.Controls.Add((Control) this.panel3);
      this.panel2.Controls.Add((Control) this.pCQG);
      this.panel2.Controls.Add((Control) this.pRithmic);
      this.panel2.Controls.Add((Control) this.pSaxo);
      this.panel2.Controls.Add((Control) this.pLmax);
      this.panel2.Controls.Add((Control) this.panelmulti);
      this.panel2.Location = new Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(711, 726);
      this.panel2.TabIndex = 4;
      this.panel2.Click += new EventHandler(this.panel2_Click);
      this.panel2.Paint += new PaintEventHandler(this.panel2_Paint);
      this.panel2.MouseDown += new MouseEventHandler(this.panel2_MouseDown);
      this.panel2.MouseMove += new MouseEventHandler(this.panel2_MouseMove);
      this.llNews.ActiveLinkColor = Color.Red;
      this.llNews.AutoSize = true;
      this.llNews.BackColor = Color.Transparent;
      this.llNews.Font = new Font("Arial", 8.25f);
      this.llNews.ForeColor = Color.FromArgb(73, 138, 243);
      this.llNews.LinkColor = Color.Red;
      this.llNews.Location = new Point(620, 74);
      this.llNews.Name = "llNews";
      this.llNews.Size = new Size(80, 14);
      this.llNews.TabIndex = 111;
      this.llNews.TabStop = true;
      this.llNews.Text = "Live News >>>";
      this.llNews.VisitedLinkColor = Color.Red;
      this.llNews.LinkClicked += new LinkLabelLinkClickedEventHandler(this.llNews_LinkClicked);
      this.lUserName.BackColor = Color.Transparent;
      this.lUserName.ForeColor = Color.White;
      this.lUserName.Location = new Point(288, 540);
      this.lUserName.Name = "lUserName";
      this.lUserName.Size = new Size(137, 13);
      this.lUserName.TabIndex = 110;
      this.lUserName.Text = "UserName";
      this.lUserName.TextAlign = ContentAlignment.MiddleCenter;
      this.lUserName.Click += new EventHandler(this.lUserName_Click);
      this.label8.AutoSize = true;
      this.label8.BackColor = Color.Transparent;
      this.label8.Font = new Font("Arial", 6.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label8.ForeColor = Color.Gold;
      this.label8.Location = new Point(50, 22);
      this.label8.Name = "label8";
      this.label8.Size = new Size(80, 12);
      this.label8.TabIndex = 108;
      this.label8.Text = "E X C L U S I V E";
      this.panel4.BackColor = Color.Gold;
      this.panel4.BackgroundImage = (Image) Resources._22;
      this.panel4.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel4.Location = new Point(-22, 724);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(755, 10);
      this.panel4.TabIndex = 13;
      this.panel6.BackColor = Color.Gold;
      this.panel6.BackgroundImage = (Image) Resources.aa;
      this.panel6.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel6.Location = new Point(-30, -7);
      this.panel6.Name = "panel6";
      this.panel6.Size = new Size(31, 787);
      this.panel6.TabIndex = 10;
      this.panel1.BackColor = Color.Gold;
      this.panel1.BackgroundImage = (Image) Resources._22;
      this.panel1.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel1.Location = new Point(-8, -9);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(755, 10);
      this.panel1.TabIndex = 12;
      this.label2.AutoSize = true;
      this.label2.BackColor = Color.Transparent;
      this.label2.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label2.ForeColor = Color.FromArgb(73, 138, 243);
      this.label2.Location = new Point(547, 43);
      this.label2.Name = "label2";
      this.label2.Size = new Size(153, 14);
      this.label2.TabIndex = 105;
      this.label2.Text = "Software For EA Newest PRO";
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
      this.btLmax.BackColor = Color.Transparent;
      this.btLmax.BackgroundImage = (Image) Resources.lmax20161;
      this.btLmax.BackgroundImageLayout = ImageLayout.Center;
      this.btLmax.Cursor = Cursors.Hand;
      this.btLmax.FlatAppearance.BorderSize = 0;
      this.btLmax.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.btLmax.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.btLmax.FlatStyle = FlatStyle.Flat;
      this.btLmax.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.btLmax.ForeColor = Color.FromArgb(112, 110, 111);
      this.btLmax.Location = new Point(160, 611);
      this.btLmax.Name = "btLmax";
      this.btLmax.Size = new Size(110, 110);
      this.btLmax.TabIndex = 96;
      this.btLmax.UseVisualStyleBackColor = false;
      this.btLmax.Click += new EventHandler(this.bLamx_Click);
      this.btLmax.MouseLeave += new EventHandler(this.button12_MouseLeave);
      this.btLmax.MouseMove += new MouseEventHandler(this.button12_MouseMove);
      this.btCQG.BackColor = Color.Transparent;
      this.btCQG.BackgroundImage = (Image) Resources.cqg20161;
      this.btCQG.BackgroundImageLayout = ImageLayout.Center;
      this.btCQG.Cursor = Cursors.Hand;
      this.btCQG.FlatAppearance.BorderSize = 0;
      this.btCQG.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.btCQG.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.btCQG.FlatStyle = FlatStyle.Flat;
      this.btCQG.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.btCQG.ForeColor = Color.FromArgb(112, 110, 111);
      this.btCQG.Location = new Point(21, 598);
      this.btCQG.Name = "btCQG";
      this.btCQG.Size = new Size(110, 110);
      this.btCQG.TabIndex = 95;
      this.btCQG.UseVisualStyleBackColor = false;
      this.btCQG.Click += new EventHandler(this.bCQG_Click);
      this.btCQG.MouseLeave += new EventHandler(this.button11_MouseLeave);
      this.btCQG.MouseMove += new MouseEventHandler(this.button11_MouseMove);
      this.button3.BackColor = Color.Transparent;
      this.button3.BackgroundImage = (Image) Resources.vop1;
      this.button3.BackgroundImageLayout = ImageLayout.Center;
      this.button3.Cursor = Cursors.Hand;
      this.button3.FlatAppearance.BorderSize = 0;
      this.button3.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button3.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button3.FlatStyle = FlatStyle.Flat;
      this.button3.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.button3.ForeColor = Color.Transparent;
      this.button3.Location = new Point(593, 3);
      this.button3.Name = "button3";
      this.button3.Size = new Size(35, 35);
      this.button3.TabIndex = 40;
      this.button3.UseVisualStyleBackColor = false;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.button3.MouseLeave += new EventHandler(this.button3_MouseLeave);
      this.button3.MouseMove += new MouseEventHandler(this.button3_MouseMove);
      this.label6.AutoSize = true;
      this.label6.Font = new Font("Arial", 9.75f);
      this.label6.ForeColor = Color.White;
      this.label6.Location = new Point(277, 13);
      this.label6.Name = "label6";
      this.label6.Size = new Size(0, 16);
      this.label6.TabIndex = 13;
      this.btSaxo.BackColor = Color.Transparent;
      this.btSaxo.BackgroundImage = (Image) Resources.saxo20161;
      this.btSaxo.BackgroundImageLayout = ImageLayout.Center;
      this.btSaxo.Cursor = Cursors.Hand;
      this.btSaxo.FlatAppearance.BorderSize = 0;
      this.btSaxo.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.btSaxo.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.btSaxo.FlatStyle = FlatStyle.Flat;
      this.btSaxo.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.btSaxo.ForeColor = Color.FromArgb(112, 110, 111);
      this.btSaxo.Location = new Point(577, 598);
      this.btSaxo.Name = "btSaxo";
      this.btSaxo.Size = new Size(110, 110);
      this.btSaxo.TabIndex = 97;
      this.btSaxo.UseVisualStyleBackColor = false;
      this.btSaxo.Click += new EventHandler(this.button13_Click);
      this.btSaxo.MouseLeave += new EventHandler(this.button13_MouseLeave);
      this.btSaxo.MouseMove += new MouseEventHandler(this.button13_MouseMove);
      this.label3.AutoSize = true;
      this.label3.BackColor = Color.Transparent;
      this.label3.Font = new Font("Arial", 9.75f);
      this.label3.ForeColor = Color.FromArgb(73, 138, 243);
      this.label3.Location = new Point(38, 5);
      this.label3.Name = "label3";
      this.label3.Size = new Size(109, 16);
      this.label3.TabIndex = 12;
      this.label3.Text = "Trade Monitor 3.7";
      this.btRitthmic.BackColor = Color.Transparent;
      this.btRitthmic.BackgroundImage = (Image) Resources.r01a;
      this.btRitthmic.BackgroundImageLayout = ImageLayout.Center;
      this.btRitthmic.Cursor = Cursors.Hand;
      this.btRitthmic.FlatAppearance.BorderSize = 0;
      this.btRitthmic.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.btRitthmic.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.btRitthmic.FlatStyle = FlatStyle.Flat;
      this.btRitthmic.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.btRitthmic.ForeColor = Color.FromArgb(112, 110, 111);
      this.btRitthmic.Location = new Point(438, 611);
      this.btRitthmic.Name = "btRitthmic";
      this.btRitthmic.Size = new Size(110, 110);
      this.btRitthmic.TabIndex = 94;
      this.btRitthmic.UseVisualStyleBackColor = false;
      this.btRitthmic.Click += new EventHandler(this.button7_Click);
      this.btRitthmic.MouseLeave += new EventHandler(this.button7_MouseLeave);
      this.btRitthmic.MouseMove += new MouseEventHandler(this.button7_MouseMove);
      this.pictureBox2.BackColor = Color.Transparent;
      this.pictureBox2.BackgroundImage = (Image) Resources.User_blue3;
      this.pictureBox2.Location = new Point(3, 3);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(31, 31);
      this.pictureBox2.TabIndex = 11;
      this.pictureBox2.TabStop = false;
      this.button5.BackColor = Color.Transparent;
      this.button5.BackgroundImage = (Image) Resources.sv1;
      this.button5.BackgroundImageLayout = ImageLayout.Center;
      this.button5.Cursor = Cursors.Hand;
      this.button5.FlatAppearance.BorderSize = 0;
      this.button5.FlatAppearance.CheckedBackColor = Color.Transparent;
      this.button5.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.button5.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.button5.FlatStyle = FlatStyle.Flat;
      this.button5.Location = new Point(629, 3);
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
      this.button6.Location = new Point(665, 3);
      this.button6.Name = "button6";
      this.button6.Size = new Size(35, 35);
      this.button6.TabIndex = 10;
      this.button6.UseVisualStyleBackColor = false;
      this.button6.Click += new EventHandler(this.button6_Click);
      this.button6.MouseLeave += new EventHandler(this.button6_MouseLeave);
      this.button6.MouseMove += new MouseEventHandler(this.button6_MouseMove);
      this.wpTextBox1.BackColor = Color.Transparent;
      this.wpTextBox1.CharIndex = 0;
      this.wpTextBox1.Cursor = Cursors.IBeam;
      this.wpTextBox1.Enabled = false;
      this.wpTextBox1.Font = new Font("Arial", 8.25f);
      this.wpTextBox1.ForeColor = Color.FromArgb(73, 138, 243);
      this.wpTextBox1.ImeMode = ImeMode.On;
      this.wpTextBox1.Location = new Point(526, 58);
      this.wpTextBox1.Name = "wpTextBox1";
      this.wpTextBox1.Selecting = false;
      this.wpTextBox1.SelectionLength = 0;
      this.wpTextBox1.SelectionStart = -1;
      this.wpTextBox1.SelectText = "";
      this.wpTextBox1.Size = new Size(209, 19);
      this.wpTextBox1.TabIndex = 109;
      this.wpTextBox1.TabStop = false;
      this.wpTextBox1.Text = "Developers: Sergey & WP Group";
      this.multiBut.BackColor = Color.Transparent;
      this.multiBut.BackgroundImage = (Image) Resources.multi13;
      this.multiBut.Cursor = Cursors.Hand;
      this.multiBut.FlatAppearance.BorderSize = 0;
      this.multiBut.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.multiBut.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.multiBut.FlatStyle = FlatStyle.Flat;
      this.multiBut.ForeColor = Color.Transparent;
      this.multiBut.Location = new Point(292, 601);
      this.multiBut.Name = "multiBut";
      this.multiBut.Size = new Size(120, 120);
      this.multiBut.TabIndex = 114;
      this.multiBut.UseVisualStyleBackColor = false;
      this.multiBut.Click += new EventHandler(this.multiBut_Click);
      this.multiBut.MouseLeave += new EventHandler(this.multiBut_MouseLeave);
      this.multiBut.MouseMove += new MouseEventHandler(this.multiBut_MouseMove);
      this.btStart.BackColor = Color.Transparent;
      this.btStart.BackgroundImage = (Image) Resources.start1;
      this.btStart.BackgroundImageLayout = ImageLayout.Center;
      this.btStart.Cursor = Cursors.Hand;
      this.btStart.FlatAppearance.BorderSize = 0;
      this.btStart.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.btStart.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.btStart.FlatStyle = FlatStyle.Flat;
      this.btStart.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.btStart.ForeColor = Color.FromArgb(112, 110, 111);
      this.btStart.Location = new Point(292, 601);
      this.btStart.Name = "btStart";
      this.btStart.Size = new Size(120, 120);
      this.btStart.TabIndex = 87;
      this.btStart.UseVisualStyleBackColor = false;
      this.btStart.Click += new EventHandler(this.bLmaxStart_Click);
      this.btStart.MouseLeave += new EventHandler(this.button8_MouseLeave);
      this.btStart.MouseMove += new MouseEventHandler(this.button8_MouseMove);
      this.pIQFeed.BackColor = Color.Transparent;
      this.pIQFeed.Controls.Add((Control) this.tbIQPass);
      this.pIQFeed.Controls.Add((Control) this.tbIQLogin);
      this.pIQFeed.Controls.Add((Control) this.lIQPass);
      this.pIQFeed.Controls.Add((Control) this.lIQLogin);
      this.pIQFeed.Controls.Add((Control) this.panel13);
      this.pIQFeed.Controls.Add((Control) this.btIqInstruments);
      this.pIQFeed.Location = new Point(231, 181);
      this.pIQFeed.Name = "pIQFeed";
      this.pIQFeed.Size = new Size(271, 323);
      this.pIQFeed.TabIndex = 114;
      this.tbIQPass.BackColor = Color.FromArgb(0, 77, 111);
      this.tbIQPass.BorderStyle = BorderStyle.None;
      this.tbIQPass.ForeColor = Color.White;
      this.tbIQPass.Location = new Point(103, 215);
      this.tbIQPass.Name = "tbIQPass";
      this.tbIQPass.Size = new Size(110, 13);
      this.tbIQPass.TabIndex = 113;
      this.tbIQLogin.BackColor = Color.FromArgb(0, 77, 111);
      this.tbIQLogin.BorderStyle = BorderStyle.None;
      this.tbIQLogin.ForeColor = Color.White;
      this.tbIQLogin.Location = new Point(103, 190);
      this.tbIQLogin.Name = "tbIQLogin";
      this.tbIQLogin.Size = new Size(110, 13);
      this.tbIQLogin.TabIndex = 112;
      this.lIQPass.AutoSize = true;
      this.lIQPass.Font = new Font("Arial", 9f);
      this.lIQPass.ForeColor = SystemColors.ButtonHighlight;
      this.lIQPass.Location = new Point(36, 213);
      this.lIQPass.Name = "lIQPass";
      this.lIQPass.Size = new Size(63, 15);
      this.lIQPass.TabIndex = 111;
      this.lIQPass.Text = "Password";
      this.lIQLogin.AutoSize = true;
      this.lIQLogin.Font = new Font("Arial", 9f);
      this.lIQLogin.ForeColor = SystemColors.ButtonHighlight;
      this.lIQLogin.Location = new Point(51, 189);
      this.lIQLogin.Name = "lIQLogin";
      this.lIQLogin.Size = new Size(49, 15);
      this.lIQLogin.TabIndex = 110;
      this.lIQLogin.Text = "User ID";
      this.panel13.BackColor = Color.Transparent;
      this.panel13.BackgroundImage = (Image) Resources.iq1;
      this.panel13.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel13.Location = new Point(73, 11);
      this.panel13.Name = "panel13";
      this.panel13.Size = new Size(105, 105);
      this.panel13.TabIndex = 109;
      this.btIqInstruments.BackColor = Color.Transparent;
      this.btIqInstruments.BackgroundImage = (Image) componentResourceManager.GetObject("btIqInstruments.BackgroundImage");
      this.btIqInstruments.BackgroundImageLayout = ImageLayout.Stretch;
      this.btIqInstruments.Cursor = Cursors.Hand;
      this.btIqInstruments.FlatAppearance.BorderSize = 0;
      this.btIqInstruments.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.btIqInstruments.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.btIqInstruments.FlatStyle = FlatStyle.Flat;
      this.btIqInstruments.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.btIqInstruments.ForeColor = Color.White;
      this.btIqInstruments.Location = new Point(37, 263);
      this.btIqInstruments.Name = "btIqInstruments";
      this.btIqInstruments.Size = new Size(181, 42);
      this.btIqInstruments.TabIndex = 103;
      this.btIqInstruments.Text = "Instruments";
      this.btIqInstruments.UseVisualStyleBackColor = false;
      this.btIqInstruments.Click += new EventHandler(this.btIqInstruments_Click);
      this.pTWS.BackColor = Color.Transparent;
      this.pTWS.Controls.Add((Control) this.panel10);
      this.pTWS.Controls.Add((Control) this.twsInstruments);
      this.pTWS.Controls.Add((Control) this.label10);
      this.pTWS.Location = new Point(231, 181);
      this.pTWS.Name = "pTWS";
      this.pTWS.Size = new Size(271, 323);
      this.pTWS.TabIndex = 113;
      this.panel10.BackColor = Color.Transparent;
      this.panel10.BackgroundImage = (Image) Resources.tws3;
      this.panel10.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel10.Location = new Point(73, 11);
      this.panel10.Name = "panel10";
      this.panel10.Size = new Size(105, 105);
      this.panel10.TabIndex = 109;
      this.twsInstruments.BackColor = Color.Transparent;
      this.twsInstruments.BackgroundImage = (Image) componentResourceManager.GetObject("twsInstruments.BackgroundImage");
      this.twsInstruments.BackgroundImageLayout = ImageLayout.Stretch;
      this.twsInstruments.Cursor = Cursors.Hand;
      this.twsInstruments.FlatAppearance.BorderSize = 0;
      this.twsInstruments.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.twsInstruments.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.twsInstruments.FlatStyle = FlatStyle.Flat;
      this.twsInstruments.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.twsInstruments.ForeColor = Color.White;
      this.twsInstruments.Location = new Point(37, 263);
      this.twsInstruments.Name = "twsInstruments";
      this.twsInstruments.Size = new Size(181, 42);
      this.twsInstruments.TabIndex = 103;
      this.twsInstruments.Text = "Instruments";
      this.twsInstruments.UseVisualStyleBackColor = false;
      this.twsInstruments.Click += new EventHandler(this.twsInstruments_Click);
      this.twsInstruments.MouseLeave += new EventHandler(this.twsInstruments_MouseLeave);
      this.twsInstruments.MouseMove += new MouseEventHandler(this.twsInstruments_MouseMove);
      this.label10.AutoSize = true;
      this.label10.BackColor = Color.Transparent;
      this.label10.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label10.ForeColor = Color.White;
      this.label10.Location = new Point(40, 185);
      this.label10.Name = "label10";
      this.label10.Size = new Size(187, 30);
      this.label10.TabIndex = 102;
      this.label10.Text = "Please open Trader Workstation \r\n         (TWS) trading platform";
      this.panel3.BackColor = Color.Transparent;
      this.panel3.BackgroundImage = (Image) Resources.karta2016;
      this.panel3.BackgroundImageLayout = ImageLayout.Center;
      this.panel3.Controls.Add((Control) this.panel16);
      this.panel3.Location = new Point(84, 146);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(527, 379);
      this.panel3.TabIndex = 99;
      this.panel3.Paint += new PaintEventHandler(this.panel3_Paint);
      this.panel16.BackColor = Color.Transparent;
      this.panel16.Controls.Add((Control) this.linkLabel9);
      this.panel16.Controls.Add((Control) this.linkLabel8);
      this.panel16.Controls.Add((Control) this.linkLabel7);
      this.panel16.Controls.Add((Control) this.linkLabel6);
      this.panel16.Controls.Add((Control) this.linkLabel5);
      this.panel16.Controls.Add((Control) this.linkLabel4);
      this.panel16.Controls.Add((Control) this.linkLabel3);
      this.panel16.Controls.Add((Control) this.linkLabel2);
      this.panel16.Location = new Point(9, 145);
      this.panel16.Name = "panel16";
      this.panel16.Size = new Size(495, 138);
      this.panel16.TabIndex = 98;
      this.linkLabel9.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel9.AutoSize = true;
      this.linkLabel9.Cursor = Cursors.Hand;
      this.linkLabel9.Font = new Font("Arial", 9f);
      this.linkLabel9.LinkColor = Color.White;
      this.linkLabel9.Location = new Point(247, 53);
      this.linkLabel9.Name = "linkLabel9";
      this.linkLabel9.Size = new Size(89, 15);
      this.linkLabel9.TabIndex = 7;
      this.linkLabel9.TabStop = true;
      this.linkLabel9.Text = "westernpips.ru";
      this.linkLabel9.VisitedLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel9.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel9_LinkClicked);
      this.linkLabel9.MouseLeave += new EventHandler(this.linkLabel9_MouseLeave);
      this.linkLabel9.MouseMove += new MouseEventHandler(this.linkLabel9_MouseMove);
      this.linkLabel8.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel8.AutoSize = true;
      this.linkLabel8.Cursor = Cursors.Hand;
      this.linkLabel8.Font = new Font("Arial", 9f);
      this.linkLabel8.LinkColor = Color.White;
      this.linkLabel8.Location = new Point(370, 99);
      this.linkLabel8.Name = "linkLabel8";
      this.linkLabel8.Size = new Size(91, 15);
      this.linkLabel8.TabIndex = 6;
      this.linkLabel8.TabStop = true;
      this.linkLabel8.Text = "westernpips.cn";
      this.linkLabel8.VisitedLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel8.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel8_LinkClicked);
      this.linkLabel8.MouseLeave += new EventHandler(this.linkLabel8_MouseLeave);
      this.linkLabel8.MouseMove += new MouseEventHandler(this.linkLabel8_MouseMove);
      this.linkLabel7.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel7.AutoSize = true;
      this.linkLabel7.Cursor = Cursors.Hand;
      this.linkLabel7.Font = new Font("Arial", 9f);
      this.linkLabel7.LinkColor = Color.White;
      this.linkLabel7.Location = new Point(333, 77);
      this.linkLabel7.Name = "linkLabel7";
      this.linkLabel7.Size = new Size(110, 15);
      this.linkLabel7.TabIndex = 5;
      this.linkLabel7.TabStop = true;
      this.linkLabel7.Text = "westernpips.name";
      this.linkLabel7.VisitedLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel7.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel7_LinkClicked);
      this.linkLabel7.MouseLeave += new EventHandler(this.linkLabel7_MouseLeave);
      this.linkLabel7.MouseMove += new MouseEventHandler(this.linkLabel7_MouseMove);
      this.linkLabel6.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel6.AutoSize = true;
      this.linkLabel6.Cursor = Cursors.Hand;
      this.linkLabel6.Font = new Font("Arial", 9f);
      this.linkLabel6.LinkColor = Color.White;
      this.linkLabel6.Location = new Point(342, 56);
      this.linkLabel6.Name = "linkLabel6";
      this.linkLabel6.Size = new Size(112, 15);
      this.linkLabel6.TabIndex = 4;
      this.linkLabel6.TabStop = true;
      this.linkLabel6.Text = "westernpips.center";
      this.linkLabel6.VisitedLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel6.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel6_LinkClicked);
      this.linkLabel6.MouseLeave += new EventHandler(this.linkLabel6_MouseLeave);
      this.linkLabel6.MouseMove += new MouseEventHandler(this.linkLabel6_MouseMove);
      this.linkLabel5.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel5.AutoSize = true;
      this.linkLabel5.Cursor = Cursors.Hand;
      this.linkLabel5.Font = new Font("Arial", 9f);
      this.linkLabel5.LinkColor = Color.White;
      this.linkLabel5.Location = new Point(232, 98);
      this.linkLabel5.Name = "linkLabel5";
      this.linkLabel5.Size = new Size(92, 15);
      this.linkLabel5.TabIndex = 3;
      this.linkLabel5.TabStop = true;
      this.linkLabel5.Text = "westernpips.eu";
      this.linkLabel5.VisitedLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel5.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
      this.linkLabel5.MouseLeave += new EventHandler(this.linkLabel5_MouseLeave);
      this.linkLabel5.MouseMove += new MouseEventHandler(this.linkLabel5_MouseMove);
      this.linkLabel4.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel4.AutoSize = true;
      this.linkLabel4.Cursor = Cursors.Hand;
      this.linkLabel4.Font = new Font("Arial", 9f);
      this.linkLabel4.LinkColor = Color.White;
      this.linkLabel4.Location = new Point(144, 48);
      this.linkLabel4.Name = "linkLabel4";
      this.linkLabel4.Size = new Size(102, 15);
      this.linkLabel4.TabIndex = 2;
      this.linkLabel4.TabStop = true;
      this.linkLabel4.Text = "westernpips.com";
      this.linkLabel4.VisitedLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel4.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
      this.linkLabel4.MouseLeave += new EventHandler(this.linkLabel4_MouseLeave);
      this.linkLabel4.MouseMove += new MouseEventHandler(this.linkLabel4_MouseMove);
      this.linkLabel3.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel3.AutoSize = true;
      this.linkLabel3.Cursor = Cursors.Hand;
      this.linkLabel3.Font = new Font("Arial", 9f);
      this.linkLabel3.LinkColor = Color.White;
      this.linkLabel3.Location = new Point(15, 107);
      this.linkLabel3.Name = "linkLabel3";
      this.linkLabel3.Size = new Size(92, 15);
      this.linkLabel3.TabIndex = 1;
      this.linkLabel3.TabStop = true;
      this.linkLabel3.Text = "westernpips.us";
      this.linkLabel3.VisitedLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel3.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
      this.linkLabel3.MouseLeave += new EventHandler(this.linkLabel3_MouseLeave);
      this.linkLabel3.MouseMove += new MouseEventHandler(this.linkLabel3_MouseMove);
      this.linkLabel2.ActiveLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel2.AutoSize = true;
      this.linkLabel2.Cursor = Cursors.Hand;
      this.linkLabel2.Font = new Font("Arial", 9f);
      this.linkLabel2.LinkColor = Color.White;
      this.linkLabel2.Location = new Point(36, 59);
      this.linkLabel2.Name = "linkLabel2";
      this.linkLabel2.Size = new Size(96, 15);
      this.linkLabel2.TabIndex = 0;
      this.linkLabel2.TabStop = true;
      this.linkLabel2.Text = "westernpips.pro";
      this.linkLabel2.VisitedLinkColor = Color.FromArgb(73, 138, 243);
      this.linkLabel2.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
      this.linkLabel2.MouseLeave += new EventHandler(this.linkLabel2_MouseLeave);
      this.linkLabel2.MouseMove += new MouseEventHandler(this.linkLabel2_MouseMove);
      this.pCQG.BackColor = Color.Transparent;
      this.pCQG.Controls.Add((Control) this.panel7);
      this.pCQG.Controls.Add((Control) this.bCQGInstruments);
      this.pCQG.Controls.Add((Control) this.label17);
      this.pCQG.Location = new Point(231, 182);
      this.pCQG.Name = "pCQG";
      this.pCQG.Size = new Size(271, 323);
      this.pCQG.TabIndex = 88;
      this.panel7.BackColor = Color.Transparent;
      this.panel7.BackgroundImage = (Image) Resources.cqg2016;
      this.panel7.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel7.Location = new Point(73, 11);
      this.panel7.Name = "panel7";
      this.panel7.Size = new Size(105, 105);
      this.panel7.TabIndex = 101;
      this.bCQGInstruments.BackColor = Color.Transparent;
      this.bCQGInstruments.BackgroundImage = (Image) componentResourceManager.GetObject("bCQGInstruments.BackgroundImage");
      this.bCQGInstruments.BackgroundImageLayout = ImageLayout.Stretch;
      this.bCQGInstruments.Cursor = Cursors.Hand;
      this.bCQGInstruments.FlatAppearance.BorderSize = 0;
      this.bCQGInstruments.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.bCQGInstruments.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.bCQGInstruments.FlatStyle = FlatStyle.Flat;
      this.bCQGInstruments.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.bCQGInstruments.ForeColor = Color.White;
      this.bCQGInstruments.Location = new Point(37, 263);
      this.bCQGInstruments.Name = "bCQGInstruments";
      this.bCQGInstruments.Size = new Size(181, 42);
      this.bCQGInstruments.TabIndex = 100;
      this.bCQGInstruments.Text = "Instruments";
      this.bCQGInstruments.UseVisualStyleBackColor = false;
      this.bCQGInstruments.Click += new EventHandler(this.bCQGInstruments_Click);
      this.bCQGInstruments.MouseLeave += new EventHandler(this.bCQGInstruments_MouseLeave);
      this.bCQGInstruments.MouseMove += new MouseEventHandler(this.bCQGInstruments_MouseMove);
      this.label17.AutoSize = true;
      this.label17.BackColor = Color.Transparent;
      this.label17.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label17.ForeColor = Color.White;
      this.label17.Location = new Point(64, 185);
      this.label17.Name = "label17";
      this.label17.Size = new Size((int) sbyte.MaxValue, 30);
      this.label17.TabIndex = 99;
      this.label17.Text = "Please open CQG FX \r\n     trading platform";
      this.pRithmic.BackColor = Color.Transparent;
      this.pRithmic.Controls.Add((Control) this.cbRithmicFeed);
      this.pRithmic.Controls.Add((Control) this.panel9);
      this.pRithmic.Controls.Add((Control) this.bRInstrumetns);
      this.pRithmic.Controls.Add((Control) this.rbEurope);
      this.pRithmic.Controls.Add((Control) this.rbUSA);
      this.pRithmic.Controls.Add((Control) this.rbDemo);
      this.pRithmic.Controls.Add((Control) this.tbRitmicsPass);
      this.pRithmic.Controls.Add((Control) this.tbRitmicsUser);
      this.pRithmic.Controls.Add((Control) this.label15);
      this.pRithmic.Controls.Add((Control) this.label14);
      this.pRithmic.Location = new Point(231, 182);
      this.pRithmic.Name = "pRithmic";
      this.pRithmic.Size = new Size(271, 323);
      this.pRithmic.TabIndex = 84;
      this.cbRithmicFeed.AutoSize = true;
      this.cbRithmicFeed.ForeColor = Color.White;
      this.cbRithmicFeed.Location = new Point(106, 237);
      this.cbRithmicFeed.Name = "cbRithmicFeed";
      this.cbRithmicFeed.Size = new Size(106, 17);
      this.cbRithmicFeed.TabIndex = 109;
      this.cbRithmicFeed.Text = "Use Server Feed";
      this.cbRithmicFeed.UseVisualStyleBackColor = true;
      this.cbRithmicFeed.CheckedChanged += new EventHandler(this.cbRithmicFeed_CheckedChanged);
      this.panel9.BackColor = Color.Transparent;
      this.panel9.BackgroundImage = (Image) Resources.rithmic2016fff;
      this.panel9.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel9.Location = new Point(73, 11);
      this.panel9.Name = "panel9";
      this.panel9.Size = new Size(105, 105);
      this.panel9.TabIndex = 108;
      this.bRInstrumetns.BackColor = Color.Transparent;
      this.bRInstrumetns.BackgroundImage = (Image) componentResourceManager.GetObject("bRInstrumetns.BackgroundImage");
      this.bRInstrumetns.BackgroundImageLayout = ImageLayout.Stretch;
      this.bRInstrumetns.Cursor = Cursors.Hand;
      this.bRInstrumetns.FlatAppearance.BorderSize = 0;
      this.bRInstrumetns.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.bRInstrumetns.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.bRInstrumetns.FlatStyle = FlatStyle.Flat;
      this.bRInstrumetns.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.bRInstrumetns.ForeColor = Color.White;
      this.bRInstrumetns.Location = new Point(37, 263);
      this.bRInstrumetns.Name = "bRInstrumetns";
      this.bRInstrumetns.Size = new Size(181, 42);
      this.bRInstrumetns.TabIndex = 107;
      this.bRInstrumetns.Text = "Instruments";
      this.bRInstrumetns.UseVisualStyleBackColor = false;
      this.bRInstrumetns.Click += new EventHandler(this.bRInstrumetns_Click);
      this.bRInstrumetns.MouseLeave += new EventHandler(this.bRInstrumetns_MouseLeave);
      this.bRInstrumetns.MouseMove += new MouseEventHandler(this.bRInstrumetns_MouseMove);
      this.rbEurope.AutoSize = true;
      this.rbEurope.ForeColor = Color.White;
      this.rbEurope.Location = new Point(160, 206);
      this.rbEurope.Name = "rbEurope";
      this.rbEurope.Size = new Size(59, 17);
      this.rbEurope.TabIndex = 106;
      this.rbEurope.TabStop = true;
      this.rbEurope.Text = "Europe";
      this.rbEurope.UseVisualStyleBackColor = true;
      this.rbEurope.CheckedChanged += new EventHandler(this.rbEurope_CheckedChanged);
      this.rbUSA.AutoSize = true;
      this.rbUSA.ForeColor = Color.White;
      this.rbUSA.Location = new Point(105, 206);
      this.rbUSA.Name = "rbUSA";
      this.rbUSA.Size = new Size(47, 17);
      this.rbUSA.TabIndex = 105;
      this.rbUSA.TabStop = true;
      this.rbUSA.Text = "USA";
      this.rbUSA.UseVisualStyleBackColor = true;
      this.rbUSA.CheckedChanged += new EventHandler(this.rbUSA_CheckedChanged);
      this.rbDemo.AutoSize = true;
      this.rbDemo.ForeColor = Color.White;
      this.rbDemo.Location = new Point(41, 206);
      this.rbDemo.Name = "rbDemo";
      this.rbDemo.Size = new Size(53, 17);
      this.rbDemo.TabIndex = 104;
      this.rbDemo.TabStop = true;
      this.rbDemo.Text = "Demo";
      this.rbDemo.UseVisualStyleBackColor = true;
      this.rbDemo.CheckedChanged += new EventHandler(this.rbDemo_CheckedChanged);
      this.tbRitmicsPass.BackColor = Color.FromArgb(0, 77, 111);
      this.tbRitmicsPass.BorderStyle = BorderStyle.None;
      this.tbRitmicsPass.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.tbRitmicsPass.ForeColor = Color.White;
      this.tbRitmicsPass.Location = new Point(106, 176);
      this.tbRitmicsPass.Name = "tbRitmicsPass";
      this.tbRitmicsPass.Size = new Size(110, 13);
      this.tbRitmicsPass.TabIndex = 91;
      this.tbRitmicsPass.UseSystemPasswordChar = true;
      this.tbRitmicsUser.BackColor = Color.FromArgb(0, 77, 111);
      this.tbRitmicsUser.BorderStyle = BorderStyle.None;
      this.tbRitmicsUser.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.tbRitmicsUser.ForeColor = Color.White;
      this.tbRitmicsUser.Location = new Point(106, 151);
      this.tbRitmicsUser.Name = "tbRitmicsUser";
      this.tbRitmicsUser.Size = new Size(110, 13);
      this.tbRitmicsUser.TabIndex = 89;
      this.label15.AutoSize = true;
      this.label15.BackColor = Color.Transparent;
      this.label15.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label15.ForeColor = Color.White;
      this.label15.Location = new Point(37, 176);
      this.label15.Name = "label15";
      this.label15.Size = new Size(63, 15);
      this.label15.TabIndex = 87;
      this.label15.Text = "Password";
      this.label14.AutoSize = true;
      this.label14.BackColor = Color.Transparent;
      this.label14.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label14.ForeColor = Color.White;
      this.label14.Location = new Point(37, 151);
      this.label14.Name = "label14";
      this.label14.Size = new Size(66, 15);
      this.label14.TabIndex = 85;
      this.label14.Text = "Username";
      this.pSaxo.BackColor = Color.Transparent;
      this.pSaxo.Controls.Add((Control) this.label9);
      this.pSaxo.Controls.Add((Control) this.tbSaxoTimeOut);
      this.pSaxo.Controls.Add((Control) this.panel11);
      this.pSaxo.Controls.Add((Control) this.bSInstrumetns);
      this.pSaxo.Controls.Add((Control) this.label18);
      this.pSaxo.Location = new Point(231, 182);
      this.pSaxo.Name = "pSaxo";
      this.pSaxo.Size = new Size(271, 323);
      this.pSaxo.TabIndex = 87;
      this.pSaxo.Paint += new PaintEventHandler(this.panel13_Paint);
      this.label9.AutoSize = true;
      this.label9.Font = new Font("Arial", 9f);
      this.label9.ForeColor = Color.White;
      this.label9.Location = new Point(64, 228);
      this.label9.Name = "label9";
      this.label9.Size = new Size(93, 15);
      this.label9.TabIndex = 111;
      this.label9.Text = "Frequency (ms)";
      this.tbSaxoTimeOut.BackColor = Color.FromArgb(0, 77, 111);
      this.tbSaxoTimeOut.BorderStyle = BorderStyle.None;
      this.tbSaxoTimeOut.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.tbSaxoTimeOut.ForeColor = Color.White;
      this.tbSaxoTimeOut.Location = new Point(149, 230);
      this.tbSaxoTimeOut.Name = "tbSaxoTimeOut";
      this.tbSaxoTimeOut.Size = new Size(50, 13);
      this.tbSaxoTimeOut.TabIndex = 110;
      this.tbSaxoTimeOut.KeyPress += new KeyPressEventHandler(this.tbSaxoTimeOut_KeyPress);
      this.panel11.BackColor = Color.Transparent;
      this.panel11.BackgroundImage = (Image) Resources.saxo2016ff;
      this.panel11.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel11.Location = new Point(73, 11);
      this.panel11.Name = "panel11";
      this.panel11.Size = new Size(105, 105);
      this.panel11.TabIndex = 109;
      this.bSInstrumetns.BackColor = Color.Transparent;
      this.bSInstrumetns.BackgroundImage = (Image) componentResourceManager.GetObject("bSInstrumetns.BackgroundImage");
      this.bSInstrumetns.BackgroundImageLayout = ImageLayout.Stretch;
      this.bSInstrumetns.Cursor = Cursors.Hand;
      this.bSInstrumetns.FlatAppearance.BorderSize = 0;
      this.bSInstrumetns.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.bSInstrumetns.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.bSInstrumetns.FlatStyle = FlatStyle.Flat;
      this.bSInstrumetns.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.bSInstrumetns.ForeColor = Color.White;
      this.bSInstrumetns.Location = new Point(37, 263);
      this.bSInstrumetns.Name = "bSInstrumetns";
      this.bSInstrumetns.Size = new Size(181, 42);
      this.bSInstrumetns.TabIndex = 103;
      this.bSInstrumetns.Text = "Instruments";
      this.bSInstrumetns.UseVisualStyleBackColor = false;
      this.bSInstrumetns.Click += new EventHandler(this.bSInstrumetns_Click);
      this.bSInstrumetns.MouseLeave += new EventHandler(this.bSInstrumetns_MouseLeave);
      this.bSInstrumetns.MouseMove += new MouseEventHandler(this.bSInstrumetns_MouseMove);
      this.label18.AutoSize = true;
      this.label18.BackColor = Color.Transparent;
      this.label18.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label18.ForeColor = Color.White;
      this.label18.Location = new Point(64, 185);
      this.label18.Name = "label18";
      this.label18.Size = new Size(146, 30);
      this.label18.TabIndex = 102;
      this.label18.Text = "Please open SaxoTrader \r\n        trading platform";
      this.pLmax.BackColor = Color.Transparent;
      this.pLmax.Controls.Add((Control) this.cbFixLmax);
      this.pLmax.Controls.Add((Control) this.cbServerFeed);
      this.pLmax.Controls.Add((Control) this.panel12);
      this.pLmax.Controls.Add((Control) this.bLmaxInstruments);
      this.pLmax.Controls.Add((Control) this.cbLmax);
      this.pLmax.Controls.Add((Control) this.LmaxPass);
      this.pLmax.Controls.Add((Control) this.LmaxUser);
      this.pLmax.Controls.Add((Control) this.label5);
      this.pLmax.Controls.Add((Control) this.label4);
      this.pLmax.Location = new Point(231, 182);
      this.pLmax.Name = "pLmax";
      this.pLmax.Size = new Size(271, 323);
      this.pLmax.TabIndex = 86;
      this.cbFixLmax.AutoSize = true;
      this.cbFixLmax.ForeColor = Color.White;
      this.cbFixLmax.Location = new Point(106, 238);
      this.cbFixLmax.Name = "cbFixLmax";
      this.cbFixLmax.Size = new Size(60, 17);
      this.cbFixLmax.TabIndex = 112;
      this.cbFixLmax.Text = "FIX 4.4";
      this.cbFixLmax.UseVisualStyleBackColor = true;
      this.cbFixLmax.CheckedChanged += new EventHandler(this.cbFixLmax_CheckedChanged);
      this.cbServerFeed.AutoSize = true;
      this.cbServerFeed.ForeColor = Color.White;
      this.cbServerFeed.Location = new Point(106, 218);
      this.cbServerFeed.Name = "cbServerFeed";
      this.cbServerFeed.Size = new Size(106, 17);
      this.cbServerFeed.TabIndex = 111;
      this.cbServerFeed.Text = "Use Server Feed";
      this.cbServerFeed.UseVisualStyleBackColor = true;
      this.cbServerFeed.CheckedChanged += new EventHandler(this.cbServerFeed_CheckedChanged);
      this.panel12.BackColor = Color.Transparent;
      this.panel12.BackgroundImage = (Image) Resources.lmax2016f;
      this.panel12.BackgroundImageLayout = ImageLayout.Stretch;
      this.panel12.Location = new Point(73, 11);
      this.panel12.Name = "panel12";
      this.panel12.Size = new Size(105, 105);
      this.panel12.TabIndex = 110;
      this.bLmaxInstruments.BackColor = Color.Transparent;
      this.bLmaxInstruments.BackgroundImage = (Image) componentResourceManager.GetObject("bLmaxInstruments.BackgroundImage");
      this.bLmaxInstruments.BackgroundImageLayout = ImageLayout.Stretch;
      this.bLmaxInstruments.Cursor = Cursors.Hand;
      this.bLmaxInstruments.FlatAppearance.BorderSize = 0;
      this.bLmaxInstruments.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.bLmaxInstruments.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.bLmaxInstruments.FlatStyle = FlatStyle.Flat;
      this.bLmaxInstruments.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.bLmaxInstruments.ForeColor = Color.White;
      this.bLmaxInstruments.Location = new Point(37, 263);
      this.bLmaxInstruments.Name = "bLmaxInstruments";
      this.bLmaxInstruments.Size = new Size(181, 42);
      this.bLmaxInstruments.TabIndex = 105;
      this.bLmaxInstruments.Text = "Instruments";
      this.bLmaxInstruments.UseVisualStyleBackColor = false;
      this.bLmaxInstruments.Click += new EventHandler(this.bLmaxInstruments_Click);
      this.bLmaxInstruments.MouseLeave += new EventHandler(this.bLmaxInstruments_MouseLeave);
      this.bLmaxInstruments.MouseMove += new MouseEventHandler(this.bLmaxInstruments_MouseMove);
      this.cbLmax.AutoSize = true;
      this.cbLmax.ForeColor = Color.White;
      this.cbLmax.Location = new Point(106, 198);
      this.cbLmax.Name = "cbLmax";
      this.cbLmax.Size = new Size(97, 17);
      this.cbLmax.TabIndex = 104;
      this.cbLmax.Text = "Demo Account";
      this.cbLmax.UseVisualStyleBackColor = true;
      this.cbLmax.CheckedChanged += new EventHandler(this.cbLmax_CheckedChanged);
      this.LmaxPass.BackColor = Color.FromArgb(0, 77, 111);
      this.LmaxPass.BorderStyle = BorderStyle.None;
      this.LmaxPass.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.LmaxPass.ForeColor = Color.White;
      this.LmaxPass.Location = new Point(106, 176);
      this.LmaxPass.Name = "LmaxPass";
      this.LmaxPass.Size = new Size(110, 13);
      this.LmaxPass.TabIndex = 103;
      this.LmaxPass.UseSystemPasswordChar = true;
      this.LmaxUser.BackColor = Color.FromArgb(0, 77, 111);
      this.LmaxUser.BorderStyle = BorderStyle.None;
      this.LmaxUser.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.LmaxUser.ForeColor = Color.White;
      this.LmaxUser.Location = new Point(106, 151);
      this.LmaxUser.Name = "LmaxUser";
      this.LmaxUser.Size = new Size(110, 13);
      this.LmaxUser.TabIndex = 101;
      this.LmaxUser.TextChanged += new EventHandler(this.LmaxUser_TextChanged);
      this.label5.AutoSize = true;
      this.label5.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label5.ForeColor = Color.White;
      this.label5.Location = new Point(37, 176);
      this.label5.Name = "label5";
      this.label5.Size = new Size(63, 15);
      this.label5.TabIndex = 100;
      this.label5.Text = "Password";
      this.label4.AutoSize = true;
      this.label4.Font = new Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label4.ForeColor = Color.White;
      this.label4.Location = new Point(37, 151);
      this.label4.Name = "label4";
      this.label4.Size = new Size(66, 15);
      this.label4.TabIndex = 99;
      this.label4.Text = "Username";
      this.panelmulti.BackColor = Color.Transparent;
      this.panelmulti.Controls.Add((Control) this.btIQFeed);
      this.panelmulti.Controls.Add((Control) this.label1);
      this.panelmulti.Controls.Add((Control) this.btTWSStart);
      this.panelmulti.Location = new Point(95, 294);
      this.panelmulti.Name = "panelmulti";
      this.panelmulti.Size = new Size(502, 211);
      this.panelmulti.TabIndex = 115;
      this.btIQFeed.BackColor = Color.Transparent;
      this.btIQFeed.BackgroundImage = (Image) Resources.button1x;
      this.btIQFeed.BackgroundImageLayout = ImageLayout.Stretch;
      this.btIQFeed.Cursor = Cursors.Hand;
      this.btIQFeed.FlatAppearance.BorderSize = 0;
      this.btIQFeed.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.btIQFeed.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.btIQFeed.FlatStyle = FlatStyle.Flat;
      this.btIQFeed.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.btIQFeed.ForeColor = Color.White;
      this.btIQFeed.Location = new Point(182, 150);
      this.btIQFeed.Name = "btIQFeed";
      this.btIQFeed.Size = new Size(157, 34);
      this.btIQFeed.TabIndex = 114;
      this.btIQFeed.Text = "IQ Feed";
      this.btIQFeed.UseVisualStyleBackColor = false;
      this.btIQFeed.Click += new EventHandler(this.btIQFeed_Click);
      this.btIQFeed.MouseLeave += new EventHandler(this.button1_MouseLeave);
      this.btIQFeed.MouseMove += new MouseEventHandler(this.button1_MouseMove);
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.label1.ForeColor = Color.White;
      this.label1.Location = new Point(100, 61);
      this.label1.Name = "label1";
      this.label1.Size = new Size(320, 18);
      this.label1.TabIndex = 113;
      this.label1.Text = "Please select broker from avalable connections";
      this.btTWSStart.BackColor = Color.Transparent;
      this.btTWSStart.BackgroundImage = (Image) Resources.button1x;
      this.btTWSStart.BackgroundImageLayout = ImageLayout.Stretch;
      this.btTWSStart.Cursor = Cursors.Hand;
      this.btTWSStart.FlatAppearance.BorderSize = 0;
      this.btTWSStart.FlatAppearance.MouseDownBackColor = Color.Transparent;
      this.btTWSStart.FlatAppearance.MouseOverBackColor = Color.Transparent;
      this.btTWSStart.FlatStyle = FlatStyle.Flat;
      this.btTWSStart.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.btTWSStart.ForeColor = Color.White;
      this.btTWSStart.Location = new Point(182, 99);
      this.btTWSStart.Name = "btTWSStart";
      this.btTWSStart.Size = new Size(157, 34);
      this.btTWSStart.TabIndex = 112;
      this.btTWSStart.Text = "Interactive Brokers";
      this.btTWSStart.UseVisualStyleBackColor = false;
      this.btTWSStart.Click += new EventHandler(this.btTWSStart_Click);
      this.btTWSStart.MouseLeave += new EventHandler(this.btTWSStart_MouseLeave);
      this.btTWSStart.MouseMove += new MouseEventHandler(this.btTWSStart_MouseMove);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.White;
      this.ClientSize = new Size(703, 725);
      this.Controls.Add((Control) this.panel5);
      this.Controls.Add((Control) this.panel2);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.IsMdiContainer = true;
      this.Name = nameof (MainWindow);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Trade Monitor";
      this.TransparencyKey = Color.FromArgb(224, 224, 224);
      this.FormClosing += new FormClosingEventHandler(this.TradeMonitor_FormClosing);
      this.Load += new EventHandler(this.TradeMonitor_Load);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      ((ISupportInitialize) this.pictureBox2).EndInit();
      this.pIQFeed.ResumeLayout(false);
      this.pIQFeed.PerformLayout();
      this.pTWS.ResumeLayout(false);
      this.pTWS.PerformLayout();
      this.panel3.ResumeLayout(false);
      this.panel16.ResumeLayout(false);
      this.panel16.PerformLayout();
      this.pCQG.ResumeLayout(false);
      this.pCQG.PerformLayout();
      this.pRithmic.ResumeLayout(false);
      this.pRithmic.PerformLayout();
      this.pSaxo.ResumeLayout(false);
      this.pSaxo.PerformLayout();
      this.pLmax.ResumeLayout(false);
      this.pLmax.PerformLayout();
      this.panelmulti.ResumeLayout(false);
      this.panelmulti.PerformLayout();
      this.ResumeLayout(false);
    }

    private enum startMmode
    {
      CQG,
      Rithmics,
      Saxo,
      Lmax,
      TWS,
      IQFeed,
    }
  }
}
