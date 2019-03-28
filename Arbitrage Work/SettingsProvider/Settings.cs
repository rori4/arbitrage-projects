// Decompiled with JetBrains decompiler
// Type: SettingsProvider.Settings
// Assembly: SettingsProvider, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 05C1E7FD-6DD9-4012-9105-50C0A7D91CF0
// Assembly location: F:\Arbitrage Cracks\TradeMonitor\lib\SettingsProvider.dll

using SettingsProvider.WesternPipes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SettingsProvider
{
  public class Settings
  {
    public static Settings LocalSetting = new Settings();
    public List<Settings.rithmicSubscription> rithmicSubscriptions;

    public static bool SetStyle(Control c, ControlStyles Style, bool value)
    {
      bool flag = false;
      MethodInfo method = typeof (Control).GetMethod(nameof (SetStyle), BindingFlags.Instance | BindingFlags.NonPublic);
      if (method != (MethodInfo) null && c != null)
      {
        method.Invoke((object) c, new object[2]
        {
          (object) Style,
          (object) value
        });
        flag = true;
      }
      return flag;
    }

    public string lamxProxyUrl { get; set; }

    public string configFileNameLmax { get; set; }

    public string configFileName { get; set; }

    public bool testConnection { get; set; }

    public string RUser { get; set; }

    public string RPass { get; set; }

    public string RExch { get; set; }

    public string User { get; set; }

    public string Port { get; set; }

    public int SaxoTimeout { get; set; }

    public Settings.LmaxSettings Lmax { get; set; }

    public byte[] Config { get; set; }

    public string ServerUrl { get; set; }

    public string FixUser { get; set; }

    public string FixPassword { get; set; }

    public bool FixDemo { get; set; }

    public string MdConnectionPoint { get; set; }

    public string TsConnectionPoint { get; set; }

    public int RithmicMode { get; set; }

    public int LmaxMode { get; set; }

    public Dictionary<int, Settings.LmaxValues> LmaxInstruments { get; set; }

    public List<string> RithimicInstruments { get; set; }

    public List<Settings.IQFeedInstruments> IQInstruments { get; set; }

    public List<Settings.CQGInstrument> CQGInstruments { get; set; }

    public List<Settings.SaxoValues> SaxoInstruments { get; set; }

    public Settings.RithmicSetting rithmicSettings { get; set; }

    public List<Settings.TWSSubscription> TWSInstruments { get; set; }

    public IQFeedSettings IQFeedParams { get; set; }

    public void GetInstruments()
    {
      if (this.CQGInstruments != null)
        this.CQGInstruments.Clear();
      if (this.SaxoInstruments != null)
        this.SaxoInstruments.Clear();
      if (this.LmaxInstruments != null)
        this.LmaxInstruments.Clear();
      if (this.rithmicSubscriptions != null)
        this.rithmicSubscriptions.Clear();
      if (this.TWSInstruments != null)
        this.TWSInstruments.Clear();
      if (this.IQInstruments != null)
        this.IQInstruments.Clear();
      if (!new Security().connectToServer())
        return;
      LicenseServiceClient licenseServiceClient = new LicenseServiceClient();
      List<ProviderContract> list1 = ((IEnumerable<ProviderContract>) licenseServiceClient.getProviders(new Trader()
      {
        Account = this.User,
        Signature = this.Config
      })).ToList<ProviderContract>();
      List<InstrumentsContract> list2 = ((IEnumerable<InstrumentsContract>) licenseServiceClient.getInstuments(new Trader()
      {
        Account = this.User,
        Signature = this.Config
      })).ToList<InstrumentsContract>();
      licenseServiceClient.Close();
      if (list1 != null && list2 != null)
      {
        foreach (InstrumentsContract instrumentsContract in list2)
        {
          InstrumentsContract p = instrumentsContract;
          IEnumerable<ProviderContract> source = list1.Where<ProviderContract>((Func<ProviderContract, bool>) (pr => pr.Id == p.ProviderId));
          if (source != null && source.Count<ProviderContract>() == 1)
          {
            string name = source.First<ProviderContract>().Name;
            if (!(name == "Lmax"))
            {
              if (!(name == "CQG"))
              {
                if (!(name == "Saxo"))
                {
                  if (!(name == "Rithmic"))
                  {
                    if (!(name == "TWS"))
                    {
                      if (name == "IQFeed")
                        this.IQInstruments.Add(new Settings.IQFeedInstruments()
                        {
                          Id = p.Id,
                          Symbol = p.DisplayId,
                          Name = p.Description,
                          Enabled = p.Enabled
                        });
                    }
                    else
                    {
                      string[] strArray = p.DisplayId.Split('%');
                      this.TWSInstruments.Add(new Settings.TWSSubscription()
                      {
                        InstumentId = p.Id,
                        Symbol = strArray[0],
                        Currency = strArray[1],
                        SecType = p.Parametr1,
                        Exchange = p.Parametr2,
                        Enabled = p.Enabled
                      });
                    }
                  }
                  else
                    this.rithmicSubscriptions.Add(new Settings.rithmicSubscription()
                    {
                      InstumentId = p.Id,
                      symbol = p.DisplayId,
                      Description = p.Description,
                      exchange = p.Parametr1,
                      Enabled = p.Enabled
                    });
                }
                else
                  this.SaxoInstruments.Add(new Settings.SaxoValues()
                  {
                    InstumentId = p.Id,
                    InstrumentType = p.DisplayId,
                    Name = p.Description,
                    Enabled = p.Enabled
                  });
              }
              else
                this.CQGInstruments.Add(new Settings.CQGInstrument()
                {
                  InstumentId = p.Id,
                  Instrument = p.DisplayId,
                  Enabled = p.Enabled
                });
            }
            else
            {
              int result = 0;
              if (int.TryParse(p.DisplayId, out result) && !this.LmaxInstruments.ContainsKey(result))
                this.LmaxInstruments.Add(result, new Settings.LmaxValues()
                {
                  InstrumentId = p.Id,
                  Name = p.Description,
                  Enabled = p.Enabled
                });
            }
          }
        }
      }
      Settings.LocalSetting.rithmicSubscriptions = this.rithmicSubscriptions;
    }

    public void SaveRithmicInstruments()
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load("rithmic.xml");
      XmlElement documentElement = xmlDocument.DocumentElement;
      documentElement.InnerXml = "";
      foreach (Settings.rithmicSubscription rithmicSubscription in this.rithmicSubscriptions)
      {
        string exchange = rithmicSubscription.exchange;
        string symbol = rithmicSubscription.symbol;
        XmlElement xmlElement = xmlDocument.GetElementById(exchange);
        if (xmlElement == null)
        {
          xmlElement = xmlDocument.CreateElement("exchange");
          xmlElement.SetAttribute("id", exchange);
          documentElement.AppendChild((XmlNode) xmlElement);
        }
        XmlElement element = xmlDocument.CreateElement("symbol");
        element.InnerText = symbol;
        xmlElement.AppendChild((XmlNode) element);
      }
      xmlDocument.Save("rithmic.xml");
    }

    public void initLamxSettings()
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(this.configFileNameLmax);
      XmlElement documentElement = xmlDocument.DocumentElement;
      string s = "1000";
      string innerText1 = documentElement.GetElementsByTagName("LmaxUrl").Item(0).InnerText;
      string innerText2 = documentElement.GetElementsByTagName("LmaxUser").Item(0).InnerText;
      string innerText3 = documentElement.GetElementsByTagName("LmaxPassword").Item(0).InnerText;
      bool flag = bool.Parse(documentElement.GetElementsByTagName("UseServerFeed").Item(0).InnerText);
      this.Lmax = new Settings.LmaxSettings()
      {
        Url = innerText1 ?? "",
        User = innerText2 ?? "",
        Password = innerText3 ?? "",
        Timeout = s == null || !(s != "") ? 5000 : int.Parse(s),
        UseServerFeed = flag
      };
    }

    public void initRithmicSettings()
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(this.configFileName);
      XmlElement documentElement = xmlDocument.DocumentElement;
      Settings.LocalSetting.MdConnectionPoint = documentElement.GetElementsByTagName("MdConnectionPoint").Item(0).InnerText;
      Settings.LocalSetting.TsConnectionPoint = documentElement.GetElementsByTagName("TsConnectionPoint").Item(0).InnerText;
      string innerText1 = documentElement.GetElementsByTagName("RithmicsUser").Item(0).InnerText;
      string innerText2 = documentElement.GetElementsByTagName("RithmicsPass").Item(0).InnerText;
      Settings.LocalSetting.RUser = innerText1;
      Settings.LocalSetting.RPass = innerText2;
      Settings.LocalSetting.RExch = this.RExch;
      Settings.RithmicSetting rithmicSetting = new Settings.RithmicSetting()
      {
        amdcnctPt = documentElement.GetElementsByTagName("AmdCnctPt").Item(0).InnerText,
        appName = documentElement.GetElementsByTagName("AppName").Item(0).InnerText,
        appVersion = documentElement.GetElementsByTagName("AppVersion").Item(0).InnerText,
        certFile = documentElement.GetElementsByTagName("CertFile").Item(0).InnerText,
        dnsServerAddr = documentElement.GetElementsByTagName("DnsServerAddr").Item(0).InnerText,
        domianName = documentElement.GetElementsByTagName("DomainName").Item(0).InnerText,
        LicSrvrAddr = documentElement.GetElementsByTagName("LicSrvrAddr").Item(0).InnerText,
        LocBrokAddr = documentElement.GetElementsByTagName("LocBrokAddr").Item(0).InnerText,
        LoggerAddr = documentElement.GetElementsByTagName("LoggerAddr").Item(0).InnerText,
        LogFilePath = documentElement.GetElementsByTagName("LogFilePath").Item(0).InnerText,
        UseServerFeed = Convert.ToBoolean(documentElement.GetElementsByTagName("UseServerFeed").Item(0).InnerText)
      };
      rithmicSetting.amdcnctPt = documentElement.GetElementsByTagName("AmdCnctPt").Item(0).InnerText;
      Settings.LocalSetting.rithmicSettings = rithmicSetting;
    }

    public void sendLogin()
    {
      LicenseServiceClient licenseServiceClient = new LicenseServiceClient();
      licenseServiceClient.sendLogin(new Trader()
      {
        Account = this.User,
        Signature = this.Config
      });
      licenseServiceClient.Close();
    }

    public void initLocalSettings()
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load("UserConfig.xml");
      XmlElement documentElement = xmlDocument.DocumentElement;
      documentElement.GetElementsByTagName("User").Item(0);
      this.User = documentElement.GetElementsByTagName("User").Item(0).InnerText ?? "";
      this.Port = documentElement.GetElementsByTagName("Port").Item(0).InnerText ?? "";
      string innerText = documentElement.GetElementsByTagName("ServiceURL").Item(0).InnerText;
      int result1;
      this.SaxoTimeout = int.TryParse(documentElement.GetElementsByTagName("SaxoTimeout").Item(0).InnerText, out result1) ? result1 : 2;
      int result2;
      this.LmaxMode = int.TryParse(documentElement.GetElementsByTagName("LmaxMode").Item(0).InnerText, out result2) ? result2 : 0;
      int result3;
      this.RithmicMode = int.TryParse(documentElement.GetElementsByTagName("RithmicMode").Item(0).InnerText, out result3) ? result3 : 1;
      if (Settings.LocalSetting.SaxoInstruments == null)
        Settings.LocalSetting.SaxoInstruments = new List<Settings.SaxoValues>();
      if (Settings.LocalSetting.CQGInstruments == null)
        Settings.LocalSetting.CQGInstruments = new List<Settings.CQGInstrument>();
      if (this.TWSInstruments == null)
        this.TWSInstruments = new List<Settings.TWSSubscription>();
      if (this.IQInstruments == null)
        this.IQInstruments = new List<Settings.IQFeedInstruments>();
      this.initFixSettings();
      this.setModeLmax(this.LmaxMode);
      this.setMode(this.RithmicMode);
      this.Config = Security.CalcHash(this.User);
      if (Settings.LocalSetting.LmaxInstruments == null)
        Settings.LocalSetting.LmaxInstruments = new Dictionary<int, Settings.LmaxValues>();
      if (this.rithmicSubscriptions == null)
        this.rithmicSubscriptions = new List<Settings.rithmicSubscription>();
      this.loadIQFeedParams();
    }

    public void initFixSettings()
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load("FixData.xml");
      XmlElement documentElement = xmlDocument.DocumentElement;
      try
      {
        this.FixUser = documentElement.GetElementsByTagName("FixUser").Item(0).InnerText;
        this.FixPassword = documentElement.GetElementsByTagName("FixPassword").Item(0).InnerText;
        this.FixDemo = Convert.ToBoolean(documentElement.GetElementsByTagName("FixDemo").Item(0).InnerText);
      }
      catch
      {
      }
    }

    public void storeLmaxSettings()
    {
      if (this.Lmax.User == null || this.Lmax.User == "")
        return;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(this.configFileNameLmax);
      XmlElement documentElement1 = xmlDocument.DocumentElement;
      XmlElement documentElement2 = xmlDocument.DocumentElement;
      documentElement2.GetElementsByTagName("LmaxUrl").Item(0).InnerText = this.Lmax.Url;
      documentElement2.GetElementsByTagName("LmaxUser").Item(0).InnerText = this.Lmax.User;
      documentElement2.GetElementsByTagName("LmaxPassword").Item(0).InnerText = this.Lmax.Password;
      documentElement2.GetElementsByTagName("UseServerFeed").Item(0).InnerText = this.Lmax.UseServerFeed.ToString();
      xmlDocument.Save(this.configFileNameLmax);
    }

    public void storeRithmicSettings()
    {
      if (this.RUser == null || this.RUser == "")
        return;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(this.configFileName);
      XmlElement documentElement = xmlDocument.DocumentElement;
      documentElement.GetElementsByTagName("RithmicsUser").Item(0).InnerText = this.RUser;
      documentElement.GetElementsByTagName("RithmicsPass").Item(0).InnerText = this.RPass;
      documentElement.GetElementsByTagName("AmdCnctPt").Item(0).InnerText = this.rithmicSettings.amdcnctPt;
      documentElement.GetElementsByTagName("AppName").Item(0).InnerText = this.rithmicSettings.appName;
      documentElement.GetElementsByTagName("AppVersion").Item(0).InnerText = this.rithmicSettings.appVersion;
      documentElement.GetElementsByTagName("CertFile").Item(0).InnerText = this.rithmicSettings.certFile;
      documentElement.GetElementsByTagName("DnsServerAddr").Item(0).InnerText = this.rithmicSettings.dnsServerAddr;
      documentElement.GetElementsByTagName("DomainName").Item(0).InnerText = this.rithmicSettings.domianName;
      documentElement.GetElementsByTagName("LicSrvrAddr").Item(0).InnerText = this.rithmicSettings.LicSrvrAddr;
      documentElement.GetElementsByTagName("LocBrokAddr").Item(0).InnerText = this.rithmicSettings.LocBrokAddr;
      documentElement.GetElementsByTagName("LoggerAddr").Item(0).InnerText = this.rithmicSettings.LoggerAddr;
      documentElement.GetElementsByTagName("LogFilePath").Item(0).InnerText = this.rithmicSettings.LogFilePath;
      documentElement.GetElementsByTagName("UseServerFeed").Item(0).InnerText = this.rithmicSettings.UseServerFeed.ToString();
      xmlDocument.Save(this.configFileName);
    }

    public void storeFixSettings()
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load("FixData.xml");
      XmlElement documentElement = xmlDocument.DocumentElement;
      try
      {
        documentElement.GetElementsByTagName("FixUser").Item(0).InnerText = this.FixUser;
        documentElement.GetElementsByTagName("FixPassword").Item(0).InnerText = this.FixPassword;
        documentElement.GetElementsByTagName("FixDemo").Item(0).InnerText = this.FixDemo.ToString();
        xmlDocument.Save("FixData.xml");
        string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string path = this.FixDemo ? Path.Combine(directoryName, "Configs\\lmaxdemoconfig.cfg") : Path.Combine(directoryName, "Configs\\lmaxconfig.cfg");
        string str = System.IO.File.ReadAllText(path);
        int startIndex = str.IndexOf("SenderCompID");
        int num1 = str.IndexOf("\n", startIndex);
        string oldValue = str.Substring(startIndex, num1 - startIndex + 1);
        int num2 = oldValue.IndexOf("=");
        string newValue = oldValue.Substring(0, num2 + 1) + this.FixUser + "\r\n";
        System.IO.File.WriteAllText(path, str.Replace(oldValue, newValue));
      }
      catch
      {
      }
    }

    public void useRiyhmicFeed(bool set)
    {
      this.rithmicSettings.UseServerFeed = set;
    }

    public void storeSettings()
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load("UserConfig.xml");
      XmlElement documentElement = xmlDocument.DocumentElement;
      documentElement.GetElementsByTagName("User").Item(0).InnerText = this.User;
      documentElement.GetElementsByTagName("Port").Item(0).InnerText = this.Port;
      documentElement.GetElementsByTagName("RithmicMode").Item(0).InnerText = this.RithmicMode.ToString();
      documentElement.GetElementsByTagName("LmaxMode").Item(0).InnerText = this.LmaxMode.ToString();
      documentElement.GetElementsByTagName("SaxoTimeout").Item(0).InnerText = (this.SaxoTimeout != 0 ? this.SaxoTimeout : 10).ToString();
      xmlDocument.Save("UserConfig.xml");
      this.storeLmaxSettings();
      this.storeRithmicSettings();
      this.saveIQFeedParams();
      this.Config = Security.CalcHash(this.User);
    }

    public Settings()
    {
      this.configFileName = "TradeConfigReal.xml";
      this.configFileNameLmax = "LmaxSettings.xml";
      this.IQFeedParams = new IQFeedSettings();
    }

    public void setModeLmax(int mode = 0)
    {
      this.LmaxMode = mode;
      switch (mode)
      {
        case 0:
        case 1:
          this.configFileNameLmax = mode != 0 ? "LmaxSettingsDemo.xml" : "LmaxSettings.xml";
          this.initLamxSettings();
          break;
        case 2:
          this.FixDemo = false;
          this.storeFixSettings();
          this.storeSettings();
          break;
        case 3:
          this.FixDemo = true;
          this.storeFixSettings();
          this.storeSettings();
          break;
      }
    }

    public void setMode(int mode = 0)
    {
      this.RithmicMode = mode;
      switch (mode)
      {
        case 1:
          this.configFileName = "TradeConfigReal.xml";
          break;
        case 2:
          this.configFileName = "TraderConfigEuro.xml";
          break;
        default:
          this.configFileName = "TradeConfig.xml";
          break;
      }
      this.initRithmicSettings();
    }

    public void getProxySettings()
    {
      byte[] lmaxSettings = new LicenseServiceClient().getLmaxSettings(new Trader()
      {
        Account = this.User,
        Signature = this.Config
      });
      if (lmaxSettings == null)
        return;
      string[] strArray = Encoding.ASCII.GetString(lmaxSettings).Split('!');
      this.lamxProxyUrl = strArray[0];
      Settings.LmaxSettings lmax = Settings.LocalSetting.Lmax;
      lmax.User = strArray[1];
      lmax.Password = strArray[2];
      Settings.LocalSetting.Lmax = lmax;
    }

    public void getProxySettingsRithmic()
    {
      byte[] rithmicSettings = new LicenseServiceClient().getRithmicSettings(new Trader()
      {
        Account = this.User,
        Signature = this.Config
      });
      if (rithmicSettings == null)
        return;
      string[] strArray = Encoding.ASCII.GetString(rithmicSettings).Split('!');
      this.lamxProxyUrl = strArray[0];
      this.RUser = strArray[1];
      this.RPass = strArray[2];
      WebProxy webProxy = new WebProxy(this.lamxProxyUrl, 9798);
      webProxy.BypassList = new List<string>((IEnumerable<string>) webProxy.BypassList)
      {
        this.lamxProxyUrl
      }.ToArray();
    }

    public string addNewInstrument(string _InstrumentId, string _Name, string _Parametr1, int _Provider, string _Parametr2 = "")
    {
      bool flag = true;
      string str1 = "";
      if (_InstrumentId == "")
      {
        str1 += "Instrument id must be defined\n";
        flag = false;
      }
      if (_Parametr1 == "" && _Provider == 3)
      {
        str1 += "Exchange must be defined";
        flag = false;
      }
      if (_Name == "")
      {
        str1 += "Name  must be defined";
        flag = false;
      }
      if (!flag)
        return str1;
      string str2 = "";
      LicenseServiceClient licenseServiceClient = new LicenseServiceClient();
      int num = licenseServiceClient.addInstrument(new InstrumentsContract()
      {
        DisplayId = _InstrumentId,
        Description = _Name,
        Parametr1 = _Parametr1,
        Parametr2 = _Parametr2,
        ProviderId = _Provider
      });
      licenseServiceClient.Close();
      switch (num)
      {
        case 0:
          str2 = "Instument already under revision";
          break;
        case 1:
          str2 = "Your request has been added";
          break;
      }
      return str2;
    }

    private void saveIQFeedParams()
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (IQFeedSettings));
      TextWriter textWriter1 = (TextWriter) new StreamWriter("IQFeed.xml");
      TextWriter textWriter2 = textWriter1;
      IQFeedSettings iqFeedParams = this.IQFeedParams;
      xmlSerializer.Serialize(textWriter2, (object) iqFeedParams);
      textWriter1.Close();
    }

    private void loadIQFeedParams()
    {
      if (!System.IO.File.Exists("IQFeed.xml"))
      {
        System.IO.File.Create("IQFeed.xml").Close();
        this.IQFeedParams.ProviderId = 6;
        this.IQFeedParams.Version = "5.2";
        this.IQFeedParams.Product = "SERGEY_BEREZHNOV_12216";
        this.IQFeedParams.Login = "";
        this.IQFeedParams.Password = "";
        this.saveIQFeedParams();
      }
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (IQFeedSettings));
      TextReader textReader = (TextReader) new StreamReader("IQFeed.xml");
      this.IQFeedParams = (IQFeedSettings) xmlSerializer.Deserialize(textReader);
      textReader.Close();
    }

    public struct LmaxSettings
    {
      public string Url;
      public string User;
      public string Password;
      public int Timeout;
      public bool UseServerFeed;
    }

    public struct LmaxValues
    {
      public long InstrumentId;
      public string Name;
      public bool Enabled;
    }

    public struct RithmicSetting
    {
      public string amdcnctPt;
      public string appName;
      public string appVersion;
      public string certFile;
      public string dnsServerAddr;
      public string domianName;
      public string LicSrvrAddr;
      public string LocBrokAddr;
      public string LoggerAddr;
      public string LogFilePath;
      public bool UseServerFeed;
    }

    public struct SaxoValues
    {
      public long InstumentId;
      public string InstrumentType;
      public string Name;
      public bool Enabled;
    }

    public struct rithmicSubscription
    {
      public long InstumentId;
      public string exchange;
      public string symbol;
      public string Description;
      public bool Enabled;
    }

    public struct CQGInstrument
    {
      public long InstumentId;
      public string Instrument;
      public bool Enabled;
    }

    public struct TWSSubscription
    {
      public long InstumentId;
      public string Symbol;
      public string Currency;
      public string Exchange;
      public string SecType;
      public bool Enabled;
    }

    public struct IQFeedInstruments
    {
      public long Id;
      public string Symbol;
      public string Name;
      public bool Enabled;
    }
  }
}
