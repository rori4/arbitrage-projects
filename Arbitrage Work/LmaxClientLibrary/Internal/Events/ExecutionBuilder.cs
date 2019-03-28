// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Events.ExecutionBuilder
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Order;
using System;

namespace Com.Lmax.Api.Internal.Events
{
  public class ExecutionBuilder
  {
    private long _executionId;
    private Decimal _price;
    private Decimal _quantity;
    private Decimal _cancelledQuantity;
    private Com.Lmax.Api.Order.Order _order;

    public ExecutionBuilder ExecutionId(long executionId)
    {
      this._executionId = executionId;
      return this;
    }

    public ExecutionBuilder Price(Decimal price)
    {
      this._price = price;
      return this;
    }

    public ExecutionBuilder Quantity(Decimal quantity)
    {
      this._quantity = quantity;
      return this;
    }

    public ExecutionBuilder CancelledQuantity(Decimal cancelledQuantity)
    {
      this._cancelledQuantity = cancelledQuantity;
      return this;
    }

    public ExecutionBuilder Order(Com.Lmax.Api.Order.Order order)
    {
      this._order = order;
      return this;
    }

    public Execution NewInstance()
    {
      return new Execution(this._executionId, this._price, this._quantity, this._order, this._cancelledQuantity);
    }
  }
}
