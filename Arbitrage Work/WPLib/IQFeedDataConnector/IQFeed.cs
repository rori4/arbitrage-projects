// Decompiled with JetBrains decompiler
// Type: IQFeedDataConnector.IQFeed
// Assembly: WPLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A67F71FE-CC9D-4C7E-B402-72B871993086
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\WPLib.dll

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using WPBase;

namespace IQFeedDataConnector
{
  public class IQFeed : DataConnectorBase
  {
    private static bool m_bRegistered = true;
    private byte[] m_szAdminSocketBuffer = new byte[8096];
    private byte[] m_szLevel1SocketBuffer = new byte[8096];
    private string m_sAdminIncompleteRecord = "";
    private string m_sLevel1IncompleteRecord = "";
    private bool m_bAdminNeedBeginReceive = true;
    private bool m_bLevel1NeedBeginReceive = true;
    private string product = "SERGEY_BEREZHNOV_12216";
    private string version = "5.2";
    private string login = "";
    private string password = "";
    private AsyncCallback m_pfnAdminCallback;
    private AsyncCallback m_pfnLevel1Callback;
    private Socket m_sockAdmin;
    private Socket m_sockLevel1;

    public override bool start(SessionParameters _params)
    {
      bool flag = base.start(_params);
      this.product = _params.connectionParameters[0];
      this.version = _params.connectionParameters[1];
      this.login = _params.connectionParameters[2];
      this.password = _params.connectionParameters[3];
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("-product {0} ", (object) this.product);
      stringBuilder.AppendFormat("-version {0} ", (object) this.version);
      stringBuilder.AppendFormat("-login {0} ", (object) this.login);
      stringBuilder.AppendFormat("-password {0} ", (object) this.password);
      stringBuilder.Append("-savelogininfo ");
      stringBuilder.Append("-autoconnect");
      Process.Start("IQConnect.exe", stringBuilder.ToString());
      Thread.Sleep(8000);
      this.m_sockAdmin = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      this.m_sockLevel1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      try
      {
        this.m_sockAdmin.Connect((EndPoint) this.getConnection("Admin"));
        this.m_sockLevel1.Connect((EndPoint) this.getConnection("Level1"));
        this.WaitForData("Level1");
        this.SendRequestToIQFeed(string.Format("S,SET PROTOCOL,{0}\r\n", (object) this.version));
        foreach (InstrumentInfo instrument in this.Instruments)
          this.SendRequestToIQFeed(string.Format("w{0}\r\n", (object) instrument.ID));
      }
      catch (Exception ex)
      {
        if (this.DataWriter != null)
          this.DataWriter.writeException(ex);
      }
      return flag;
    }

    public override bool stop()
    {
      bool flag = true;
      try
      {
        if (this.m_sockAdmin != null)
        {
          this.m_sockAdmin.Shutdown(SocketShutdown.Receive);
          this.m_sockAdmin.Disconnect(false);
          this.m_sockAdmin.Close();
        }
        if (this.m_sockLevel1 != null)
        {
          this.m_sockLevel1.Shutdown(SocketShutdown.Receive);
          this.m_sockLevel1.Disconnect(false);
          this.m_sockLevel1.Close();
        }
      }
      catch
      {
      }
      if (flag)
        return base.stop();
      return false;
    }

    public void SendRequestToIQFeed(string sCommand)
    {
      byte[] numArray = new byte[sCommand.Length];
      byte[] bytes = Encoding.ASCII.GetBytes(sCommand);
      int length = bytes.Length;
      try
      {
        if (this.m_sockLevel1.Send(bytes, length, SocketFlags.None) != length)
          Console.WriteLine(string.Format("Error Sending Request:\r\n{0}", (object) sCommand.TrimEnd("\r\n".ToCharArray())));
        else
          Console.WriteLine(string.Format("Request Sent Successfully:\r\n{0}", (object) sCommand.TrimEnd("\r\n".ToCharArray())));
      }
      catch (SocketException ex)
      {
        Console.WriteLine(string.Format("Socket Error Sending Request:\r\n{0}\r\n{1}", (object) sCommand.TrimEnd("\r\n".ToCharArray()), (object) ex.Message));
      }
    }

    public IPEndPoint getConnection(string portName)
    {
      return new IPEndPoint(IPAddress.Parse("127.0.0.1"), IQFeed.GetIQFeedPort(portName));
    }

    public static int GetIQFeedPort(string sPort)
    {
      int num = 0;
      RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\DTN\\IQFeed\\Startup");
      if (registryKey != null)
      {
        string str = "";
        if (!(sPort == "Level1"))
        {
          if (!(sPort == "Lookup"))
          {
            if (!(sPort == "Level2"))
            {
              if (!(sPort == "Admin"))
              {
                if (sPort == "Derivative")
                  str = registryKey.GetValue("DerivativePort", (object) "9400").ToString();
              }
              else
                str = registryKey.GetValue("AdminPort", (object) "9300").ToString();
            }
            else
              str = registryKey.GetValue("Level2Port", (object) "9200").ToString();
          }
          else
            str = registryKey.GetValue("LookupPort", (object) "9100").ToString();
        }
        else
          str = registryKey.GetValue("Level1Port", (object) "5009").ToString();
        num = Convert.ToInt32(str);
      }
      return num;
    }

    public void OnReceive(IAsyncResult asyn)
    {
      try
      {
        if (asyn.AsyncState.ToString().Equals("Admin"))
        {
          int count = this.m_sockAdmin.EndReceive(asyn);
          this.m_bAdminNeedBeginReceive = true;
          string str1 = this.m_sAdminIncompleteRecord + Encoding.ASCII.GetString(this.m_szAdminSocketBuffer, 0, count);
          this.m_sAdminIncompleteRecord = "";
          while (str1.Length > 0)
          {
            int length1 = str1.IndexOf("\n");
            if (length1 == -1)
            {
              this.m_sAdminIncompleteRecord = str1;
              str1 = "";
            }
            else
            {
              string str2 = str1.Substring(0, length1);
              if (str2.StartsWith("S,STATS,"))
              {
                if (!IQFeed.m_bRegistered)
                {
                  string s = "S,REGISTER CLIENT APP," + this.product + "," + this.version + "\r\n";
                  byte[] numArray = new byte[s.Length];
                  byte[] bytes = Encoding.ASCII.GetBytes(s);
                  int length2 = bytes.Length;
                  this.m_sockAdmin.Send(bytes, length2, SocketFlags.None);
                  IQFeed.m_bRegistered = true;
                }
              }
              else if (str2.StartsWith("S,REGISTER CLIENT APP COMPLETED"))
              {
                string s = "" + "S,CONNECT\r\n";
                byte[] numArray = new byte[s.Length];
                byte[] bytes = Encoding.ASCII.GetBytes(s);
                int length2 = bytes.Length;
                this.m_sockAdmin.Send(bytes, length2, SocketFlags.None);
                IQFeed.m_bRegistered = true;
              }
              str1 = str1.Substring(str2.Length + 1);
            }
          }
          this.WaitForData("Admin");
        }
        else
        {
          if (!asyn.AsyncState.ToString().Equals("Level1"))
            return;
          int count = this.m_sockLevel1.EndReceive(asyn);
          this.m_bLevel1NeedBeginReceive = true;
          string str1 = this.m_sLevel1IncompleteRecord + Encoding.ASCII.GetString(this.m_szLevel1SocketBuffer, 0, count);
          this.m_sLevel1IncompleteRecord = "";
          while (str1.Length > 0)
          {
            int length = str1.IndexOf("\n");
            if (length > 0)
            {
              string str2 = str1.Substring(0, length);
              string[] strArray = str2.Split(',');
              if (strArray != null && strArray[0] == "Q")
              {
                this.currentTradeInfo.TickTimeUtc = DateTime.UtcNow;
                this.DataWriter.write(new TradeInfo()
                {
                  Intrument = "IQFeed!!" + strArray[1],
                  Ask = Decimal.Parse(strArray[9].Replace('.', ',')),
                  Bid = Decimal.Parse(strArray[7].Replace('.', ','))
                });
              }
              str1 = str1.Substring(str2.Length + 1);
            }
            else
            {
              this.m_sLevel1IncompleteRecord = str1;
              str1 = "";
            }
          }
          this.WaitForData("Level1");
        }
      }
      catch (Exception ex)
      {
        if (this.DataWriter == null)
          return;
        this.DataWriter.writeException(ex);
      }
    }

    public void WaitForData(string sSocketName)
    {
      try
      {
        if (sSocketName.Equals("Admin"))
        {
          if (this.m_pfnAdminCallback == null)
            this.m_pfnAdminCallback = new AsyncCallback(this.OnReceive);
          if (!this.m_bAdminNeedBeginReceive)
            return;
          this.m_bAdminNeedBeginReceive = false;
          this.m_sockAdmin.BeginReceive(this.m_szAdminSocketBuffer, 0, this.m_szAdminSocketBuffer.Length, SocketFlags.None, this.m_pfnAdminCallback, (object) sSocketName);
        }
        else
        {
          if (!sSocketName.Equals("Level1"))
            return;
          if (this.m_pfnLevel1Callback == null)
            this.m_pfnLevel1Callback = new AsyncCallback(this.OnReceive);
          if (!this.m_bLevel1NeedBeginReceive)
            return;
          this.m_bLevel1NeedBeginReceive = false;
          this.m_sockLevel1.BeginReceive(this.m_szLevel1SocketBuffer, 0, this.m_szLevel1SocketBuffer.Length, SocketFlags.None, this.m_pfnLevel1Callback, (object) sSocketName);
        }
      }
      catch (Exception ex)
      {
        if (this.DataWriter == null)
          return;
        this.DataWriter.writeException(ex);
      }
    }
  }
}
