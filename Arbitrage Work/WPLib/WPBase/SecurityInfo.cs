// Decompiled with JetBrains decompiler
// Type: WPBase.SecurityInfo
// Assembly: WPLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A67F71FE-CC9D-4C7E-B402-72B871993086
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\WPLib.dll

using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace WPBase
{
  public class SecurityInfo
  {
    public static byte[] getSignature()
    {
      byte[] buffer = SecurityInfo.calcSecret();
      SHA1 shA1 = SHA1.Create();
      shA1.Initialize();
      return shA1.ComputeHash(buffer);
    }

    public static byte[] calcSecret()
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
  }
}
