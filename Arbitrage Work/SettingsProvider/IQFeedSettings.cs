// Decompiled with JetBrains decompiler
// Type: SettingsProvider.IQFeedSettings
// Assembly: SettingsProvider, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 05C1E7FD-6DD9-4012-9105-50C0A7D91CF0
// Assembly location: F:\Arbitrage Cracks\TradeMonitor\lib\SettingsProvider.dll

using System.Xml.Serialization;

namespace SettingsProvider
{
  [XmlRoot("IQFeed", IsNullable = false, Namespace = "http://www.westernpips.com")]
  public class IQFeedSettings
  {
    public string Product { get; set; }

    public string Version { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public int ProviderId { get; set; }
  }
}
