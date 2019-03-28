// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Order.Execution
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;

namespace Com.Lmax.Api.Order
{
  public class Execution
  {
    private readonly long _exceutionId;
    private readonly Decimal _price;
    private readonly Decimal _quantity;
    private readonly Com.Lmax.Api.Order.Order _order;
    private readonly Decimal _cancelledQuantity;

    public Execution(long exceutionId, Decimal price, Decimal quantity, Com.Lmax.Api.Order.Order order, Decimal cancelledQuantity)
    {
      this._exceutionId = exceutionId;
      this._price = price;
      this._quantity = quantity;
      this._order = order;
      this._cancelledQuantity = cancelledQuantity;
    }

    public long ExecutionId
    {
      get
      {
        return this._exceutionId;
      }
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

    public Com.Lmax.Api.Order.Order Order
    {
      get
      {
        return this._order;
      }
    }

    public Decimal CancelledQuantity
    {
      get
      {
        return this._cancelledQuantity;
      }
    }

    public bool Equals(Execution other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return other._exceutionId == this._exceutionId && other._price == this._price && (other._quantity == this._quantity && object.Equals((object) other._order, (object) this._order)) && other._cancelledQuantity == this._cancelledQuantity;
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (Execution))
        return false;
      return this.Equals((Execution) obj);
    }

    public override int GetHashCode()
    {
      return (((this._exceutionId.GetHashCode() * 397 ^ this._price.GetHashCode()) * 397 ^ this._quantity.GetHashCode()) * 397 ^ (this._order != (Com.Lmax.Api.Order.Order) null ? this._order.GetHashCode() : 0)) * 397 ^ this._cancelledQuantity.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format("Execution{{ExceutionId: {0}, Price: {1}, Quantity: {2}, Order: {3}, CancelledQuantity: {4}}}", (object) this._exceutionId, (object) this._price, (object) this._quantity, (object) this._order, (object) this._cancelledQuantity);
    }
  }
}
