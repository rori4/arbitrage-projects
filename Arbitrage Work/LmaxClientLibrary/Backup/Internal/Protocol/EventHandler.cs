// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.EventHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

namespace Com.Lmax.Api.Internal.Protocol
{
  public class EventHandler : Handler
  {
    private readonly OrderBookEventHandler _orderBookEventHandler;

    public EventHandler(OrderBookEventHandler orderBookEventHandler)
    {
      this._orderBookEventHandler = orderBookEventHandler;
    }

    public override Handler GetHandler(string qName)
    {
      if (qName == this._orderBookEventHandler.ElementName)
        return (Handler) this._orderBookEventHandler;
      return (Handler) this;
    }
  }
}
