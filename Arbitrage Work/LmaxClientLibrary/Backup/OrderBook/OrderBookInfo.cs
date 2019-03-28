// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.OrderBook.OrderBookInfo
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;

namespace Com.Lmax.Api.OrderBook
{
  public class OrderBookInfo
  {
    private readonly Decimal _priceIncrement;
    private readonly Decimal _quantityIncrement;
    private readonly Decimal _volatilityBandPercentage;

    public OrderBookInfo(Decimal priceIncrement, Decimal quantityIncrement, Decimal volatilityBandPercentage)
    {
      this._priceIncrement = priceIncrement;
      this._quantityIncrement = quantityIncrement;
      this._volatilityBandPercentage = volatilityBandPercentage;
    }

    public Decimal PriceIncrement
    {
      get
      {
        return this._priceIncrement;
      }
    }

    public Decimal QuantityIncrement
    {
      get
      {
        return this._quantityIncrement;
      }
    }

    public Decimal VolatilityBandPercentage
    {
      get
      {
        return this._volatilityBandPercentage;
      }
    }

    public bool Equals(OrderBookInfo other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return other._priceIncrement == this._priceIncrement && other._quantityIncrement == this._quantityIncrement && other._volatilityBandPercentage == this._volatilityBandPercentage;
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (OrderBookInfo))
        return false;
      return this.Equals((OrderBookInfo) obj);
    }

    public override int GetHashCode()
    {
      return (this._priceIncrement.GetHashCode() * 397 ^ this._quantityIncrement.GetHashCode()) * 397 ^ this._volatilityBandPercentage.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format("PriceIncrement: {0}, QuantityIncrement: {1}, VolatilityBandPercentage: {2}", (object) this._priceIncrement, (object) this._quantityIncrement, (object) this._volatilityBandPercentage);
    }
  }
}
