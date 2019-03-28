// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.OrderBook.PricePoint
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;

namespace Com.Lmax.Api.OrderBook
{
  public struct PricePoint
  {
    private readonly Decimal _price;
    private readonly Decimal _quantity;

    public PricePoint(Decimal quantity, Decimal price)
    {
      this._price = price;
      this._quantity = quantity;
    }

    public Decimal Price
    {
      get
      {
        return this._price;
      }
    }

    public Decimal Quantity
    {
      get
      {
        return this._quantity;
      }
    }

    public bool Equals(PricePoint other)
    {
      return other._price == this._price && other._quantity == this._quantity;
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj) || obj.GetType() != typeof (PricePoint))
        return false;
      return this.Equals((PricePoint) obj);
    }

    public override int GetHashCode()
    {
      Decimal num1 = this._price;
      int num2 = num1.GetHashCode() * 397;
      num1 = this._quantity;
      int hashCode = num1.GetHashCode();
      return num2 ^ hashCode;
    }

    public override string ToString()
    {
      return string.Format("Price: {0}, Quantity: {1}", (object) this._price, (object) this._quantity);
    }
  }
}
