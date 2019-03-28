// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.PositionEventHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal.Events;
using System;

namespace Com.Lmax.Api.Internal.Protocol
{
  public class PositionEventHandler : DefaultHandler
  {
    private const string RootNode = "position";
    private const string AccountId = "accountId";
    private const string InstrumentId = "instrumentId";
    private const string ShortUnfilledCost = "shortUnfilledCost";
    private const string LongUnfilledCost = "longUnfilledCost";
    private const string OpenQuantity = "openQuantity";
    private const string CumulativeCost = "cumulativeCost";
    private const string OpenCost = "openCost";

    public event OnPositionEvent PositionEventListener;

    public PositionEventHandler()
      : base("position")
    {
      this.AddHandler("accountId");
      this.AddHandler("instrumentId");
      this.AddHandler("shortUnfilledCost");
      this.AddHandler("longUnfilledCost");
      this.AddHandler("openQuantity");
      this.AddHandler("openCost");
      this.AddHandler("cumulativeCost");
    }

    public override void EndElement(string endElement)
    {
      if (this.PositionEventListener == null || !"position".Equals(endElement))
        return;
      long longValue1;
      this.TryGetValue("accountId", out longValue1);
      long longValue2;
      this.TryGetValue("instrumentId", out longValue2);
      Decimal dec1;
      this.TryGetValue("openQuantity", out dec1);
      Decimal dec2;
      this.TryGetValue("openCost", out dec2);
      Decimal dec3;
      this.TryGetValue("cumulativeCost", out dec3);
      PositionBuilder positionBuilder = new PositionBuilder();
      positionBuilder.AccountId(longValue1).InstrumentId(longValue2).OpenQuantity(dec1).OpenCost(dec2).CumulativeCost(dec3);
      Decimal dec4;
      if (this.TryGetValue("shortUnfilledCost", out dec4))
        positionBuilder.ShortUnfilledCost(dec4);
      else
        positionBuilder.ShortUnfilledCost(new Decimal(0));
      Decimal dec5;
      if (this.TryGetValue("longUnfilledCost", out dec5))
        positionBuilder.LongUnfilledCost(dec5);
      else
        positionBuilder.LongUnfilledCost(new Decimal(0));
      this.PositionEventListener(positionBuilder.NewInstance());
    }
  }
}
