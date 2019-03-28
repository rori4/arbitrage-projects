// Decompiled with JetBrains decompiler
// Type: SettingsProvider.Security
// Assembly: SettingsProvider, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 05C1E7FD-6DD9-4012-9105-50C0A7D91CF0
// Assembly location: F:\Arbitrage Cracks\TradeMonitor\lib\SettingsProvider.dll

using SettingsProvider.WesternPipes;
using System.Collections.Generic;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace SettingsProvider
{
  public class Security
  {
    public bool connectToServer()
    {
      bool flag = false;
      LicenseServiceClient licenseServiceClient = new LicenseServiceClient();
      if (licenseServiceClient.checkUser(new Trader()
      {
        Account = Settings.LocalSetting.User,
        Signature = Settings.LocalSetting.Config
      }) == 0)
        flag = true;
      licenseServiceClient.Close();
      return true;
    }

    public static int requestRegistration()
    {
      try
      {
        return new LicenseServiceClient().sendRequest(new Trader()
        {
          Account = Settings.LocalSetting.User,
          Signature = Settings.LocalSetting.Config
        });
      }
      catch
      {
        return -5;
      }
    }

    private string GetMacAddress()
    {
      string str = "";
      foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
      {
        if (networkInterface.OperationalStatus == OperationalStatus.Up)
        {
          str += networkInterface.GetPhysicalAddress().ToString();
          break;
        }
      }
      return str;
    }

    public static byte[] CalcHash(string user)
    {
      byte[] buffer = SettingsProvider.Security.calcSecret(user);
      SHA1CryptoServiceProvider cryptoServiceProvider = new SHA1CryptoServiceProvider();
      cryptoServiceProvider.Initialize();
      return cryptoServiceProvider.ComputeHash(buffer);
    }

    public static byte[] calcSecret(string user)
    {
      string s = "??";
      ManagementObjectSearcher managementObjectSearcher1 = new ManagementObjectSearcher("select * from Win32_Processor");
      if (managementObjectSearcher1 != null)
      {
        foreach (ManagementObject managementObject in managementObjectSearcher1.Get())
          s += (string) managementObject["ProcessorId"];
      }
      ManagementObjectSearcher managementObjectSearcher2 = new ManagementObjectSearcher("select * from Win32_BaseBoard");
      if (managementObjectSearcher2 != null)
      {
        foreach (ManagementObject managementObject in managementObjectSearcher2.Get())
          s += (string) managementObject["SerialNumber"];
      }
      return Encoding.ASCII.GetBytes(s);
    }

    public static bool checkLicense(string Account, string SKU)
    {
      LicenseServiceClient licenseServiceClient1 = new LicenseServiceClient();
      LicenseServiceClient licenseServiceClient2 = licenseServiceClient1;
      Trader _traderData = new Trader();
      _traderData.Account = Account;
      _traderData.Signature = SettingsProvider.Security.CalcHash(Account);
      string SKU1 = SKU;
      int num = licenseServiceClient2.checkLicense(_traderData, SKU1) == 0 ? 1 : 0;
      licenseServiceClient1.Close();
      return num == 0;
    }

    public static ICollection<InstrumentsContract> getInstuments(string Account, string SKU, string Product = "")
    {
      LicenseServiceClient licenseServiceClient1 = new LicenseServiceClient();
      LicenseServiceClient licenseServiceClient2 = licenseServiceClient1;
      Trader _traderData = new Trader();
      _traderData.Account = Account;
      _traderData.Signature = SettingsProvider.Security.CalcHash(Account);
      string SKU1 = SKU;
      string ProviderName = Product;
      InstrumentsContract[] instumentsSku = licenseServiceClient2.getInstumentsSKU(_traderData, SKU1, ProviderName);
      licenseServiceClient1.Close();
      return (ICollection<InstrumentsContract>) instumentsSku;
    }
  }
}
