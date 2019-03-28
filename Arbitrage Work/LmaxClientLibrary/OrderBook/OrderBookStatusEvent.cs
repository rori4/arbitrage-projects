// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.OrderBook.OrderBookStatusEvent
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

namespace Com.Lmax.Api.OrderBook
{
  public sealed class OrderBookStatusEvent
  {
    private readonly long _instrumentId;
    private readonly OrderBookStatus _status;

    public OrderBookStatusEvent(long instrumentId, OrderBookStatus status)
    {
      this._instrumentId = instrumentId;
      this._status = status;
    }

    public long InstrumentId
    {
      get
      {
        return this._instrumentId;
      }
    }

    public OrderBookStatus Status
    {
      get
      {
        return this._status;
      }
    }

    public bool Equals(OrderBookStatusEvent other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return other._instrumentId == this._instrumentId && other._status == this._status;
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (OrderBookStatusEvent))
        return false;
      return this.Equals((OrderBookStatusEvent) obj);
    }

    public override int GetHashCode()
    {
      return this._instrumentId.GetHashCode() * 397 ^ this._status.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format("InstrumentId: {0}, Status: {1}", (object) this._instrumentId, (object) this._status);
    }
  }
}
