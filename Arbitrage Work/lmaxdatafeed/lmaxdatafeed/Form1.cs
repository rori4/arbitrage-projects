// Decompiled with JetBrains decompiler
// Type: lmaxdatafeed.Form1
// Assembly: lmaxdatafeed, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DFEBD8BE-A1D3-43D8-B547-6EC80FD649BE
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\lmaxdatafeed.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace lmaxdatafeed
{
  public class Form1 : Form
  {
    public string username = string.Empty;
    public string serialnumber = string.Empty;
    public List<string> InstrumentIDs = new List<string>();
    public List<string> BusNames = new List<string>();
    public List<int> BusConnectors = new List<int>();
    public List<long> Timers = new List<long>();
    private int e = 60;
    private int f = 180;
    private b_сlass a;
    public string login;
    public string password;
    public string url;
    public string type;
    private bool b;
    private bool c;
    private bool d;
    public bool ShowQuotes;
    private IContainer g;
    private Button h;
    private Button i;
    private DataGridView j;
    private ListBox k;
    private Button l;
    private System.Windows.Forms.Timer m;
    private DataGridViewTextBoxColumn n;
    private DataGridViewTextBoxColumn o;
    private DataGridViewTextBoxColumn p;
    private DataGridViewTextBoxColumn q;
    private DataGridViewTextBoxColumn r;
    private DataGridViewTextBoxColumn s;

    [DllImport("ipcbus.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern int CreateBusConnector();

    [DllImport("ipcbus.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern void DestroyBusConnector(int BusConnectorID);

    [DllImport("ipcbus.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern bool CreateBus(int BusConnectorID, [MarshalAs(UnmanagedType.LPStr)] string BusName);

    [DllImport("ipcbus.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern bool OpenBus(int BusConnectorID, [MarshalAs(UnmanagedType.LPStr)] string BusName);

    [DllImport("ipcbus.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern bool Write(int BusConnectorID, double Buf1, double Buf2);

    [DllImport("ipcbus.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern byte GetConnectionsCount(int BusConnectorID);

    [DllImport("ipcbus.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static extern string GetConnectionName(int BusConnectorID, byte ConnectionNumber);

    public static byte[] GetBytes(string str)
    {
      byte[] numArray = new byte[str.Length * 2];
      Buffer.BlockCopy((Array) str.ToCharArray(), 0, (Array) numArray, 0, numArray.Length);
      return numArray;
    }

    public static string GetString(byte[] bytes)
    {
      char[] chArray = new char[bytes.Length / 2];
      Buffer.BlockCopy((Array) bytes, 0, (Array) chArray, 0, bytes.Length);
      return new string(chArray);
    }

    public bool CheckSN()
    {
      Crc32 crc32 = new Crc32();
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string empty4 = string.Empty;
      string empty5 = string.Empty;
      string empty6 = string.Empty;
      string empty7 = string.Empty;
      string empty8 = string.Empty;
      string empty9 = string.Empty;
      string empty10 = string.Empty;
      string empty11 = string.Empty;
      string empty12 = string.Empty;
      string empty13 = string.Empty;
      if (string.IsNullOrEmpty(this.username))
      {
        int num = (int) MessageBox.Show("Please enter your name in the field \"USER\" in the config file.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      if (string.IsNullOrEmpty(this.serialnumber))
      {
        int num = (int) MessageBox.Show("Please enter your serial number in the field \"SERIAL_NUMBER\" in the config file.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      bool flag;
      try
      {
        foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor").Get())
          empty1 = managementBaseObject["Name"].ToString();
        foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard").Get())
          empty2 = managementBaseObject["SerialNumber"].ToString();
        foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS").Get())
          empty3 = managementBaseObject["SerialNumber"].ToString();
        foreach (byte num in crc32.ComputeHash(Form1.GetBytes(empty1)))
          empty4 += num.ToString("x2").ToUpper();
        foreach (byte num in crc32.ComputeHash(Form1.GetBytes(empty2)))
          empty5 += num.ToString("x2").ToUpper();
        foreach (byte num in crc32.ComputeHash(Form1.GetBytes(empty3)))
          empty6 += num.ToString("x2").ToUpper();
        foreach (byte num in crc32.ComputeHash(Form1.GetBytes(this.username)))
          empty7 += num.ToString("x2").ToUpper();
        foreach (byte num in crc32.ComputeHash(Form1.GetBytes(empty4 + empty5 + empty6)))
          empty9 += num.ToString("x2").ToUpper();
        foreach (byte num in crc32.ComputeHash(Form1.GetBytes(empty7 + empty9)))
          empty10 += num.ToString("x2").ToUpper();
        foreach (byte num in crc32.ComputeHash(Form1.GetBytes(empty10)))
          empty11 += num.ToString("x2").ToUpper();
        foreach (byte num in crc32.ComputeHash(Form1.GetBytes(empty11)))
          empty12 += num.ToString("x2").ToUpper();
        flag = string.Compare(this.serialnumber, empty10 + empty11 + empty12) == 0;
      }
      catch (ManagementException ex)
      {
        int num = (int) MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        flag = false;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("ERROR: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        flag = false;
      }
      return flag;
    }

    public Form1()
    {
      this.a21();
      this.WriteLog("LMAX Data Feed Connector version: 0.1.5");
      this.j.Rows.Clear();
      StreamReader streamReader = new StreamReader("lmaxdatafeed.cfg");
      while (!streamReader.EndOfStream)
      {
        string source = streamReader.ReadLine();
        string str1 = "USER=";
        if (source.Contains(str1))
        {
          this.username = source.Substring(str1.Length);
        }
        else
        {
          string str2 = "SERIAL_NUMBER=";
          if (source.Contains(str2))
          {
            this.serialnumber = source.Substring(str2.Length);
          }
          else
          {
            string str3 = "LMAX_LOGIN=";
            if (source.Contains(str3))
            {
              this.login = source.Substring(str3.Length);
            }
            else
            {
              string str4 = "LMAX_PASSW=";
              if (source.Contains(str4))
              {
                this.password = source.Substring(str4.Length);
              }
              else
              {
                string str5 = "LMAX_URL=";
                if (source.Contains(str5))
                {
                  this.url = source.Substring(str5.Length);
                }
                else
                {
                  string str6 = "LMAX_TYPE=";
                  if (source.Contains(str6))
                  {
                    this.type = source.Substring(str6.Length);
                  }
                  else
                  {
                    string str7 = "WARNING_SIGNAL_INTERVAL=";
                    if (source.Contains(str7))
                    {
                      this.e = Convert.ToInt32(source.Substring(str7.Length));
                    }
                    else
                    {
                      string str8 = "ERROR_SIGNAL_INTERVAL=";
                      if (source.Contains(str8))
                      {
                        this.f = Convert.ToInt32(source.Substring(str8.Length));
                      }
                      else
                      {
                        string str9 = "RECONNECT_ON_ERROR=";
                        if (source.Contains(str9))
                        {
                          if (source.Substring(str9.Length).Contains("true"))
                            this.b = true;
                        }
                        else
                        {
                          string str10 = "FULL_RESTART_ON_ERROR=";
                          if (source.Contains(str10))
                          {
                            if (source.Substring(str10.Length).Contains("true"))
                              this.c = true;
                          }
                          else
                          {
                            string str11 = "SHOW_QUOTES=";
                            if (source.Contains(str11))
                            {
                              if (source.Substring(str11.Length).Contains("true"))
                                this.ShowQuotes = true;
                            }
                            else
                            {
                              string str12 = "AUTOSTART=";
                              if (source.Contains(str12))
                              {
                                if (source.Substring(str12.Length).Contains("true"))
                                  this.d = true;
                              }
                              else
                              {
                                string str13 = "LMAX_INSTRUMENTS";
                                if (source.Contains(str13) && streamReader.ReadLine() == "{" && !streamReader.EndOfStream)
                                {
                                  while (source != "}" && !streamReader.EndOfStream)
                                  {
                                    source = streamReader.ReadLine();
                                    if (source.Contains<char>(' '))
                                    {
                                      string str14 = source.Remove(source.IndexOf(' '), source.Length - source.IndexOf(' '));
                                      this.InstrumentIDs.Add(str14);
                                      string str15 = source.Remove(0, source.IndexOf(' ')).Replace("  ", string.Empty).Trim().Replace(" ", string.Empty);
                                      this.BusNames.Add(str15);
                                      this.j.Rows.Add((object) str14, (object) str15, (object) "", (object) "", (object) "");
                                    }
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      streamReader.Close();
      for (int index = 0; index < this.BusNames.Count<string>(); ++index)
      {
        int busConnector = Form1.CreateBusConnector();
        if (busConnector != 0)
        {
          this.BusConnectors.Add(busConnector);
          this.WriteLog("New BusConnector created (id: " + (object) busConnector + ")");
        }
        else
          this.WriteLog("ERROR: unable to create new BusConnector...");
        if (!Form1.CreateBus(busConnector, this.BusNames[index]))
        {
          this.WriteLog("ERROR: unable to create bus \"" + this.BusNames[index] + "\"...");
          if (!Form1.OpenBus(busConnector, this.BusNames[index]))
            this.WriteLog("ERROR: unable to open bus...");
          else
            this.WriteLog("Bus \"" + this.BusNames[index] + "\" opened...");
        }
        else
          this.WriteLog("Bus \"" + this.BusNames[index] + "\" created...");
      }
      if (this.c && !this.d)
      {
        this.WriteLog("WARNING! Parameter \"FULL_RESTART_ON_ERROR\" set to \"true\", but \"AUTOSTART\" set to \"false\" - switching \"FULL_RESTART_ON_ERROR\" to \"false\"...");
        this.c = false;
      }
      if (!this.d)
        return;
      this.WriteLog("Parameter \"AUTOSTART\" set to \"true\", starting...");
      if (this.c)
      {
        this.WriteLog("Parameter \"FULL_RESTART_ON_ERROR\" set to \"true\", sleeping for 2 seconds...");
        Thread.Sleep(2000);
      }
      this.cmm();
    }

    private void cmm()
    {
      if (!this.ShowQuotes)
        this.WriteLog("SHOW_QUOTES set to \"false\", quotes will not be displayed...");
      this.a = new b_сlass(this);
      this.Timers.Clear();
      for (int index = 0; index < this.BusNames.Count<string>(); ++index)
        this.Timers.Add(DateTime.Now.Ticks / 10000000L);
      this.m.Enabled = true;
      this.a.a1(this.url, this.login, this.password);
      this.h.Enabled = false;
      this.l.Enabled = true;
    }

    private void b_m()
    {
      this.a.k.Abort();
      this.a = (b_сlass) null;
      this.h.Enabled = true;
      this.l.Enabled = false;
      this.SetStatus(false);
      this.m.Enabled = false;
    }

    private void cmm(object A_0, EventArgs A_1)
    {
      this.cmm();
    }

    private void b_m(object A_0, EventArgs A_1)
    {
      this.b_m();
    }

    public void WriteLog(string msg)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Form1.ci ci = new Form1.ci();
      // ISSUE: reference to a compiler-generated field
      ci.c = this;
      // ISSUE: reference to a compiler-generated field
      ci.a = 0;
      // ISSUE: reference to a compiler-generated field
      ci.b = DateTime.Now.ToString() + " - " + msg;
      if (this.k.InvokeRequired)
      {
        // ISSUE: reference to a compiler-generated method
        this.k.BeginInvoke((Delegate) new Action(ci.d));
      }
      else
      {
        if (this.k.Items.Count > 200)
          this.k.Items.Clear();
        // ISSUE: reference to a compiler-generated field
        this.k.Items.Add((object) ci.b);
        this.k.SelectedIndex = this.k.Items.Count - 1;
        this.k.SelectedIndex = -1;
      }
    }

    public void SendDataToBus(int BusConnectorID, double Ask, double Bid)
    {
      Form1.Write(this.BusConnectors[BusConnectorID], Ask, Bid);
      this.Timers[BusConnectorID] = DateTime.Now.Ticks / 10000000L;
    }

    public void SetDataGridValue(int Row, int Col, string value)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Form1.a1 a1 = new Form1.a1();
      // ISSUE: reference to a compiler-generated field
      a1.b = Row;
      // ISSUE: reference to a compiler-generated field
      a1.c = Col;
      // ISSUE: reference to a compiler-generated field
      a1.d = value;
      // ISSUE: reference to a compiler-generated field
      a1.a = this;
      if (this.j.InvokeRequired)
      {
        // ISSUE: reference to a compiler-generated method
        this.j.BeginInvoke((Delegate) new Action(a1.e));
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        this.j.Rows[a1.b].Cells[a1.c].Value = (object) a1.d;
        // ISSUE: reference to a compiler-generated field
        if (a1.c != 5)
          return;
        // ISSUE: reference to a compiler-generated field
        if (a1.d == "WARNING")
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.j.Rows[a1.b].Cells[a1.c].Style.BackColor = Color.FromArgb(244, 104, 11);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.j.Rows[a1.b].Cells[a1.c].Style.ForeColor = Color.White;
        }
        // ISSUE: reference to a compiler-generated field
        if (a1.d == "ERROR")
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.j.Rows[a1.b].Cells[a1.c].Style.BackColor = Color.Red;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.j.Rows[a1.b].Cells[a1.c].Style.ForeColor = Color.White;
        }
        // ISSUE: reference to a compiler-generated field
        if (a1.d == "OK")
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.j.Rows[a1.b].Cells[a1.c].Style.BackColor = Color.White;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.j.Rows[a1.b].Cells[a1.c].Style.ForeColor = Color.Black;
        }
      }
    }

    public void SetStatus(bool Connected)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Form1.bi bi = new Form1.bi();
      // ISSUE: reference to a compiler-generated field
      bi.b = Connected;
      // ISSUE: reference to a compiler-generated field
      bi.a = this;
      if (this.i.InvokeRequired)
      {
        // ISSUE: reference to a compiler-generated method
        this.i.BeginInvoke((Delegate) new Action(bi.c));
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (bi.b)
          this.i.BackColor = Color.Lime;
        else
          this.i.BackColor = Color.Red;
      }
    }

    private void a21(object A_0, EventArgs A_1)
    {
      for (int Row1 = 0; Row1 < this.Timers.Count<long>(); ++Row1)
      {
        DateTime now = DateTime.Now;
        int int32 = Convert.ToInt32(now.Ticks / 10000000L - this.Timers[Row1]);
        if (this.ShowQuotes)
        {
          int Row2 = Row1;
          int Col = 4;
          now = DateTime.Now;
          string str = Convert.ToString(now.Ticks / 10000000L - this.Timers[Row1]);
          this.SetDataGridValue(Row2, Col, str);
          if (int32 < this.e)
            this.SetDataGridValue(Row1, 5, "OK");
          if (int32 > this.e && int32 < this.f)
            this.SetDataGridValue(Row1, 5, "WARNING");
        }
        if (int32 >= this.f)
        {
          this.SetDataGridValue(Row1, 5, "ERROR");
          if (this.c && int32 >= this.f + 3)
          {
            this.m.Enabled = false;
            this.WriteLog("ERROR: no quotes on " + this.BusNames[Row1] + " over the last " + (object) this.f + " seconds...");
            this.WriteLog("ERROR: \"FullRestartOnError\" set to \"true\" - sleeping 2 seconds before full restart..");
            Process process = new Process();
            process.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            process.StartInfo.FileName = Application.ExecutablePath;
            this.WriteLog("Directory: " + AppDomain.CurrentDomain.BaseDirectory);
            this.WriteLog("App FileName: " + Application.ExecutablePath);
            Thread.Sleep(2000);
            process.Start();
            Application.Exit();
            break;
          }
          if (this.b && int32 >= this.f + 3)
          {
            this.WriteLog("ERROR: no quotes on " + this.BusNames[Row1] + " over the last " + (object) this.f + " seconds...");
            this.WriteLog("ERROR: \"ReconnectOnError\" set to \"true\" - sleeping 2 seconds before restart..");
            Thread.Sleep(2000);
            this.b_m();
            this.cmm();
          }
        }
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.g != null)
        this.g.Dispose();
      base.Dispose(disposing);
    }

    private void a21()
    {
      this.g = (IContainer) new Container();
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form1));
      this.h = new Button();
      this.i = new Button();
      this.j = new DataGridView();
      this.n = new DataGridViewTextBoxColumn();
      this.o = new DataGridViewTextBoxColumn();
      this.p = new DataGridViewTextBoxColumn();
      this.q = new DataGridViewTextBoxColumn();
      this.r = new DataGridViewTextBoxColumn();
      this.s = new DataGridViewTextBoxColumn();
      this.k = new ListBox();
      this.l = new Button();
      this.m = new System.Windows.Forms.Timer(this.g);
      ((ISupportInitialize) this.j).BeginInit();
      this.SuspendLayout();
      this.h.Location = new Point(12, 12);
      this.h.Name = "button1";
      this.h.Size = new Size(75, 23);
      this.h.TabIndex = 0;
      this.h.Text = "Start";
      this.h.UseVisualStyleBackColor = true;
      this.h.Click += new EventHandler(this.cmm);
      this.i.BackColor = Color.Red;
      this.i.Enabled = false;
      this.i.ForeColor = Color.Red;
      this.i.Location = new Point(185, 12);
      this.i.Name = "button2";
      this.i.Size = new Size(23, 23);
      this.i.TabIndex = 7;
      this.i.UseVisualStyleBackColor = false;
      this.j.AllowUserToAddRows = false;
      this.j.AllowUserToDeleteRows = false;
      this.j.AllowUserToResizeRows = false;
      gridViewCellStyle1.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.j.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.j.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.j.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.j.Columns.AddRange((DataGridViewColumn) this.n, (DataGridViewColumn) this.o, (DataGridViewColumn) this.p, (DataGridViewColumn) this.q, (DataGridViewColumn) this.r, (DataGridViewColumn) this.s);
      this.j.Location = new Point(12, 41);
      this.j.Name = "dataGridView1";
      this.j.RowHeadersVisible = false;
      this.j.Size = new Size(643, 177);
      this.j.TabIndex = 15;
      gridViewCellStyle2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      gridViewCellStyle2.NullValue = (object) null;
      this.n.DefaultCellStyle = gridViewCellStyle2;
      this.n.FillWeight = 93.08083f;
      this.n.HeaderText = "Instrument ID";
      this.n.MinimumWidth = 6;
      this.n.Name = "Id";
      this.n.ReadOnly = true;
      this.o.FillWeight = 77.56735f;
      this.o.HeaderText = "Bus name";
      this.o.MinimumWidth = 10;
      this.o.Name = "Bus";
      this.o.ReadOnly = true;
      this.p.FillWeight = 77.56735f;
      this.p.HeaderText = "Ask";
      this.p.MinimumWidth = 10;
      this.p.Name = "Ask";
      this.p.ReadOnly = true;
      this.q.FillWeight = 77.56735f;
      this.q.HeaderText = "Bid";
      this.q.MinimumWidth = 10;
      this.q.Name = "Bid";
      this.q.ReadOnly = true;
      this.r.FillWeight = 40f;
      this.r.HeaderText = "Seconds";
      this.r.MinimumWidth = 10;
      this.r.Name = "Seconds";
      this.s.FillWeight = 50f;
      this.s.HeaderText = "Status";
      this.s.Name = "Status";
      this.k.FormattingEnabled = true;
      this.k.HorizontalScrollbar = true;
      this.k.Location = new Point(13, 224);
      this.k.Name = "listBox1";
      this.k.ScrollAlwaysVisible = true;
      this.k.Size = new Size(643, 238);
      this.k.TabIndex = 16;
      this.l.Enabled = false;
      this.l.Location = new Point(104, 12);
      this.l.Name = "button3";
      this.l.Size = new Size(75, 23);
      this.l.TabIndex = 17;
      this.l.Text = "Stop";
      this.l.UseVisualStyleBackColor = true;
      this.l.Click += new EventHandler(this.b_m);
      this.m.Tick += new EventHandler(this.a21);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(668, 468);
      this.Controls.Add((Control) this.l);
      this.Controls.Add((Control) this.k);
      this.Controls.Add((Control) this.j);
      this.Controls.Add((Control) this.i);
      this.Controls.Add((Control) this.h);
      this.FormBorderStyle = FormBorderStyle.Fixed3D;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.Name = nameof (Form1);
      this.Text = "LMAX Data Feed Connector";
      ((ISupportInitialize) this.j).EndInit();
      this.ResumeLayout(false);
    }
  }
}
