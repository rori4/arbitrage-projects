// Decompiled with JetBrains decompiler
// Type: WPBase.TradeInfo
// Assembly: WPLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A67F71FE-CC9D-4C7E-B402-72B871993086
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\WPLib.dll

using System;

namespace WPBase
{
  public class TradeInfo
  {
    public string Intrument { get; set; }

    public Decimal Bid { get; set; }

    public Decimal Ask { get; set; }

    public Decimal Volume { get; set; }

    public DateTime TickTimeUtc { get; set; }
  }
}
