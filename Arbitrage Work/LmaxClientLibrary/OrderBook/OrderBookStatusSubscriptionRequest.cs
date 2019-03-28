﻿// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.OrderBook.OrderBookStatusSubscriptionRequest
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal.Xml;

namespace Com.Lmax.Api.OrderBook
{
  public class OrderBookStatusSubscriptionRequest : SubscriptionRequest
  {
    private readonly long _instrumentId;

    public OrderBookStatusSubscriptionRequest(long instrumentId)
    {
      this._instrumentId = instrumentId;
    }

    protected override void WriteSubscriptionBodyTo(IStructuredWriter writer)
    {
      writer.ValueOrEmpty("orderBookStatus", new long?(this._instrumentId));
    }
  }
}
