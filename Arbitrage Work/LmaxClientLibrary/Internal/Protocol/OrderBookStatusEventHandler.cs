// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.OrderBookStatusEventHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.OrderBook;
using System;

namespace Com.Lmax.Api.Internal.Protocol
{
  public class OrderBookStatusEventHandler : DefaultHandler
  {
    private const string OrderBookId = "id";
    private const string Status = "status";

    public OrderBookStatusEventHandler()
      : base("orderBookStatus")
    {
      this.AddHandler("id");
      this.AddHandler("status");
    }

    public override void EndElement(string endElement)
    {
      if (this.OrderBookStatusChanged == null || !(this.ElementName == endElement))
        return;
      long longValue;
      this.TryGetValue("id", out longValue);
      OrderBookStatus status = (OrderBookStatus) Enum.Parse(typeof (OrderBookStatus), this.GetStringValue("status"));
      this.OrderBookStatusChanged(new OrderBookStatusEvent(longValue, status));
    }

    public event OnOrderBookStatusEvent OrderBookStatusChanged;
  }
}
