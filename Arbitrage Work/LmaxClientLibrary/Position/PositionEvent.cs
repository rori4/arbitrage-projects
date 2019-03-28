// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Position.PositionEvent
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;

namespace Com.Lmax.Api.Position
{
  public class PositionEvent : IEquatable<PositionEvent>
  {
    private readonly long _accountId;
    private readonly long _instrumentId;
    private readonly Decimal _shortUnfilledCost;
    private readonly Decimal _longUnfilledCost;
    private readonly Decimal _openQuantity;
    private readonly Decimal _cumulativeCost;
    private readonly Decimal _openCost;

    public PositionEvent(long accountId, long instrumentId, Decimal shortUnfilledCost, Decimal longUnfilledCost, Decimal openQuantity, Decimal cumulativeCost, Decimal openCost)
    {
      this._accountId = accountId;
      this._openCost = openCost;
      this._cumulativeCost = cumulativeCost;
      this._openQuantity = openQuantity;
      this._longUnfilledCost = longUnfilledCost;
      this._shortUnfilledCost = shortUnfilledCost;
      this._instrumentId = instrumentId;
    }

    public long AccountId
    {
      get
      {
        return this._accountId;
      }
    }

    public long InstrumentId
    {
      get
      {
        return this._instrumentId;
      }
    }

    public Decimal ShortUnfilledCost
    {
      get
      {
        return this._shortUnfilledCost;
      }
    }

    public Decimal LongUnfilledCost
    {
      get
      {
        return this._longUnfilledCost;
      }
    }

    public Decimal OpenQuantity
    {
      get
      {
        return this._openQuantity;
      }
    }

    public Decimal CumulativeCost
    {
      get
      {
        return this._cumulativeCost;
      }
    }

    public Decimal OpenCost
    {
      get
      {
        return this._openCost;
      }
    }

    public override string ToString()
    {
      return string.Format("AccountId: {0}, InstrumentId: {1}, ShortUnfilledCost: {2}, LongUnfilledCost: {3}, OpenQuantity: {4}, CumulativeCost: {5}, OpenCost: {6}", (object) this._accountId, (object) this._instrumentId, (object) this._shortUnfilledCost, (object) this._longUnfilledCost, (object) this._openQuantity, (object) this._cumulativeCost, (object) this._openCost);
    }

    public bool Equals(PositionEvent other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return other._accountId == this._accountId && other._instrumentId == this._instrumentId && (other._shortUnfilledCost == this._shortUnfilledCost && other._longUnfilledCost == this._longUnfilledCost) && (other._openQuantity == this._openQuantity && other._cumulativeCost == this._cumulativeCost) && other._openCost == this._openCost;
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (PositionEvent))
        return false;
      return this.Equals((PositionEvent) obj);
    }

    public override int GetHashCode()
    {
      return (((((this._accountId.GetHashCode() * 397 ^ this._instrumentId.GetHashCode()) * 397 ^ this._shortUnfilledCost.GetHashCode()) * 397 ^ this._longUnfilledCost.GetHashCode()) * 397 ^ this._openQuantity.GetHashCode()) * 397 ^ this._cumulativeCost.GetHashCode()) * 397 ^ this._openCost.GetHashCode();
    }

    public static bool operator ==(PositionEvent left, PositionEvent right)
    {
      return object.Equals((object) left, (object) right);
    }

    public static bool operator !=(PositionEvent left, PositionEvent right)
    {
      return !object.Equals((object) left, (object) right);
    }
  }
}
