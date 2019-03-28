// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.OrderBook.Instrument
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

namespace Com.Lmax.Api.OrderBook
{
  public class Instrument
  {
    private readonly long _id;
    private readonly string _name;
    private readonly UnderlyingInfo _underlying;
    private readonly CalendarInfo _calendar;
    private readonly RiskInfo _risk;
    private readonly OrderBookInfo _orderBook;
    private readonly ContractInfo _contract;
    private readonly CommercialInfo _commercial;

    public Instrument(long id, string name, UnderlyingInfo underlying, CalendarInfo calendar, RiskInfo risk, OrderBookInfo orderBook, ContractInfo contract, CommercialInfo commercial)
    {
      this._id = id;
      this._name = name;
      this._underlying = underlying;
      this._calendar = calendar;
      this._risk = risk;
      this._orderBook = orderBook;
      this._contract = contract;
      this._commercial = commercial;
    }

    public long Id
    {
      get
      {
        return this._id;
      }
    }

    public string Name
    {
      get
      {
        return this._name;
      }
    }

    public UnderlyingInfo Underlying
    {
      get
      {
        return this._underlying;
      }
    }

    public CalendarInfo Calendar
    {
      get
      {
        return this._calendar;
      }
    }

    public RiskInfo Risk
    {
      get
      {
        return this._risk;
      }
    }

    public OrderBookInfo OrderBook
    {
      get
      {
        return this._orderBook;
      }
    }

    public ContractInfo Contract
    {
      get
      {
        return this._contract;
      }
    }

    public CommercialInfo Commercial
    {
      get
      {
        return this._commercial;
      }
    }

    public bool Equals(Instrument other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return other._id == this._id && object.Equals((object) other._name, (object) this._name) && (object.Equals((object) other._underlying, (object) this._underlying) && object.Equals((object) other._calendar, (object) this._calendar)) && (object.Equals((object) other._risk, (object) this._risk) && object.Equals((object) other._orderBook, (object) this._orderBook) && object.Equals((object) other._contract, (object) this._contract)) && object.Equals((object) other._commercial, (object) this._commercial);
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (Instrument))
        return false;
      return this.Equals((Instrument) obj);
    }

    public override int GetHashCode()
    {
      return ((((((this._id.GetHashCode() * 397 ^ (this._name != null ? this._name.GetHashCode() : 0)) * 397 ^ (this._underlying != null ? this._underlying.GetHashCode() : 0)) * 397 ^ (this._calendar != null ? this._calendar.GetHashCode() : 0)) * 397 ^ (this._risk != null ? this._risk.GetHashCode() : 0)) * 397 ^ (this._orderBook != null ? this._orderBook.GetHashCode() : 0)) * 397 ^ (this._contract != null ? this._contract.GetHashCode() : 0)) * 397 ^ (this._commercial != null ? this._commercial.GetHashCode() : 0);
    }

    public override string ToString()
    {
      return string.Format("Id: {0}, Name: {1}, Underlying: {2}, Calendar: {3}, Risk: {4}, OrderBook: {5}, Contract: {6}, Commercial: {7}", (object) this._id, (object) this._name, (object) this._underlying, (object) this._calendar, (object) this._risk, (object) this._orderBook, (object) this._contract, (object) this._commercial);
    }
  }
}
