// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Order.Order
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;

namespace Com.Lmax.Api.Order
{
  public class Order : IEquatable<Com.Lmax.Api.Order.Order>
  {
    private readonly string _instructionId;
    private readonly string _orderId;
    private readonly long _instrumentId;
    private readonly long _accountId;
    private readonly Decimal? _price;
    private readonly Decimal? _stopLossOffset;
    private readonly Decimal? _stopProfitOffset;
    private readonly Decimal? _stopReferencePrice;
    private readonly Decimal _quantity;
    private readonly Decimal _filledQuantity;
    private readonly Decimal _cancelledQuantity;
    private readonly OrderType _orderType;
    private readonly TimeInForce _timeInForce;
    private readonly Decimal _commission;

    public Order(string instructionId, string orderId, long instrumentId, long accountId, Decimal? price, Decimal quantity, Decimal filledQuantity, Decimal cancelledQuantity, OrderType orderType, TimeInForce timeInForce, Decimal? stopLossOffset, Decimal? stopProfitOffset, Decimal? stopReferencePrice, Decimal commission)
    {
      this._instructionId = instructionId;
      this._orderId = orderId;
      this._instrumentId = instrumentId;
      this._accountId = accountId;
      this._price = price;
      this._quantity = quantity;
      this._filledQuantity = filledQuantity;
      this._cancelledQuantity = cancelledQuantity;
      this._orderType = orderType;
      this._timeInForce = timeInForce;
      this._stopReferencePrice = stopReferencePrice;
      this._stopProfitOffset = stopProfitOffset;
      this._stopLossOffset = stopLossOffset;
      this._commission = commission;
    }

    public string InstructionId
    {
      get
      {
        return this._instructionId;
      }
    }

    public string OrderId
    {
      get
      {
        return this._orderId;
      }
    }

    public long InstrumentId
    {
      get
      {
        return this._instrumentId;
      }
    }

    public long AccountId
    {
      get
      {
        return this._accountId;
      }
    }

    public OrderType OrderType
    {
      get
      {
        return this._orderType;
      }
    }

    public Decimal Quantity
    {
      get
      {
        return this._quantity;
      }
    }

    public Decimal FilledQuantity
    {
      get
      {
        return this._filledQuantity;
      }
    }

    public Decimal? LimitPrice
    {
      get
      {
        return this._orderType == OrderType.LIMIT ? this._price : new Decimal?();
      }
    }

    public Decimal? StopPrice
    {
      get
      {
        return this._orderType == OrderType.STOP_ORDER ? this._price : new Decimal?();
      }
    }

    public Decimal CancelledQuantity
    {
      get
      {
        return this._cancelledQuantity;
      }
    }

    public Decimal? StopLossOffset
    {
      get
      {
        return this._stopLossOffset;
      }
    }

    public Decimal? StopProfitOffset
    {
      get
      {
        return this._stopProfitOffset;
      }
    }

    public Decimal? StopReferencePrice
    {
      get
      {
        return this._stopReferencePrice;
      }
    }

    public Decimal Commission
    {
      get
      {
        return this._commission;
      }
    }

    public TimeInForce TimeInForce
    {
      get
      {
        return this._timeInForce;
      }
    }

    public bool Equals(Com.Lmax.Api.Order.Order other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return other._instructionId == this._instructionId && object.Equals((object) other._orderId, (object) this._orderId) && (other._instrumentId == this._instrumentId && other._accountId == this._accountId) && other._price.Equals((object) this._price) && (other._stopLossOffset.Equals((object) this._stopLossOffset) && other._stopProfitOffset.Equals((object) this._stopProfitOffset)) && (other._stopReferencePrice.Equals((object) this._stopReferencePrice) && other._quantity == this._quantity && (other._filledQuantity == this._filledQuantity && other._cancelledQuantity == this._cancelledQuantity) && (other._commission == this._commission && other._timeInForce == this._timeInForce)) && object.Equals((object) other._orderType, (object) this._orderType);
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (Com.Lmax.Api.Order.Order))
        return false;
      return this.Equals((Com.Lmax.Api.Order.Order) obj);
    }

    public override int GetHashCode()
    {
      return (this._instructionId.GetHashCode() * 397 ^ (this._orderId != null ? this._orderId.GetHashCode() : 0)) * 397 ^ this._instrumentId.GetHashCode();
    }

    public static bool operator ==(Com.Lmax.Api.Order.Order left, Com.Lmax.Api.Order.Order right)
    {
      return object.Equals((object) left, (object) right);
    }

    public static bool operator !=(Com.Lmax.Api.Order.Order left, Com.Lmax.Api.Order.Order right)
    {
      return !object.Equals((object) left, (object) right);
    }

    public override string ToString()
    {
      return string.Format("Order{{InstructionId: {0}, OrderId: {1}, InstrumentId: {2}, AccountId: {3}, Price: {4}, StopLossOffset: {5}, StopProfitOffset: {6}, StopReferencePrice: {7}, Quantity: {8}, FilledQuantity: {9}, CancelledQuantity: {10}, OrderType: {11}, Commission: {12}}}", (object) this._instructionId, (object) this._orderId, (object) this._instrumentId, (object) this._accountId, (object) this._price, (object) this._stopLossOffset, (object) this._stopProfitOffset, (object) this._stopReferencePrice, (object) this._quantity, (object) this._filledQuantity, (object) this._cancelledQuantity, (object) this._orderType, (object) this._commission);
    }
  }
}
