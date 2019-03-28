// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Events.OrderBuilder
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Order;
using System;
using System.Collections.Generic;

namespace Com.Lmax.Api.Internal.Events
{
  public class OrderBuilder
  {
    private readonly IDictionary<string, OrderType> _orderTypes = (IDictionary<string, OrderType>) new Dictionary<string, OrderType>();
    private TimeInForce _timeInForce = TimeInForce.Unknown;
    private readonly IDictionary<string, TimeInForce> _timeInForceTypes = (IDictionary<string, TimeInForce>) new Dictionary<string, TimeInForce>();
    private long _accountId;
    private Decimal _cancelledQuantity;
    private Decimal _filledQuantity;
    private string _instructionId;
    private long _instrumentId;
    private string _orderId;
    private OrderType _orderType;
    private Decimal? _price;
    private Decimal _quantity;
    private Decimal? _stopReferencePrice;
    private Decimal? _stopProfitOffset;
    private Decimal? _stopLossOffset;
    private Decimal _commission;

    public OrderBuilder()
    {
      this._orderTypes.Add("STOP_COMPOUND_PRICE_LIMIT", OrderType.LIMIT);
      this._orderTypes.Add("PRICE_LIMIT", OrderType.LIMIT);
      this._orderTypes.Add("STOP_COMPOUND_MARKET", OrderType.MARKET);
      this._orderTypes.Add("MARKET_ORDER", OrderType.MARKET);
      this._orderTypes.Add("STOP_LOSS_ORDER", OrderType.STOP_LOSS_MARKET_ORDER);
      this._orderTypes.Add("STOP_PROFIT_ORDER", OrderType.STOP_PROFIT_LIMIT_ORDER);
      this._orderTypes.Add("STOP_ORDER", OrderType.STOP_ORDER);
      this._timeInForceTypes.Add("GoodTilCancelled", TimeInForce.GoodTilCancelled);
      this._timeInForceTypes.Add("GoodForDay", TimeInForce.GoodForDay);
      this._timeInForceTypes.Add("ImmediateOrCancel", TimeInForce.ImmediateOrCancel);
      this._timeInForceTypes.Add("FillOrKill", TimeInForce.FillOrKill);
    }

    public Com.Lmax.Api.Order.Order NewInstance()
    {
      return new Com.Lmax.Api.Order.Order(this._instructionId, this._orderId, this._instrumentId, this._accountId, this._price, this._quantity, this._filledQuantity, this._cancelledQuantity, this._orderType, this._timeInForce, this._stopLossOffset, this._stopProfitOffset, this._stopReferencePrice, this._commission);
    }

    public OrderBuilder InstructionId(string anInstructionId)
    {
      this._instructionId = anInstructionId;
      return this;
    }

    public OrderBuilder OrderId(string anOrderId)
    {
      this._orderId = anOrderId;
      return this;
    }

    public OrderBuilder InstrumentId(long anInstrumentId)
    {
      this._instrumentId = anInstrumentId;
      return this;
    }

    public OrderBuilder AccountId(long anAccountId)
    {
      this._accountId = anAccountId;
      return this;
    }

    public OrderBuilder Quantity(Decimal aQuantity)
    {
      this._quantity = aQuantity;
      return this;
    }

    public OrderBuilder FilledQuantity(Decimal aFilledQuantity)
    {
      this._filledQuantity = aFilledQuantity;
      return this;
    }

    public OrderBuilder Price(Decimal aPrice)
    {
      this._price = new Decimal?(aPrice);
      return this;
    }

    public OrderBuilder OrderType(OrderType anOrderType)
    {
      this._orderType = anOrderType;
      return this;
    }

    public OrderBuilder StopReferencePrice(Decimal stopReferencePrice)
    {
      this._stopReferencePrice = new Decimal?(stopReferencePrice);
      return this;
    }

    public OrderBuilder StopProfitOffset(Decimal stopProfitOffset)
    {
      this._stopProfitOffset = new Decimal?(stopProfitOffset);
      return this;
    }

    public OrderBuilder StopLossOffset(Decimal stopLossOffset)
    {
      this._stopLossOffset = new Decimal?(stopLossOffset);
      return this;
    }

    public OrderBuilder Commission(Decimal commission)
    {
      this._commission = commission;
      return this;
    }

    public OrderBuilder TimeInForce(TimeInForce timeInForce)
    {
      this._timeInForce = timeInForce;
      return this;
    }

    public OrderBuilder OrderType(string messageOrderType)
    {
      OrderType anOrderType;
      if (this._orderTypes.TryGetValue(messageOrderType, out anOrderType))
      {
        this.OrderType(anOrderType);
      }
      else
      {
        try
        {
          this.OrderType((OrderType) Enum.Parse(typeof (OrderType), messageOrderType));
        }
        catch (ArgumentException ex)
        {
          this.OrderType(OrderType.UNKNOWN);
        }
      }
      return this;
    }

    public OrderBuilder CancelledQuantity(Decimal aCancelledQuantity)
    {
      this._cancelledQuantity = aCancelledQuantity;
      return this;
    }

    public OrderBuilder TimeInForce(string messageTimeInForce)
    {
      try
      {
        this.TimeInForce((TimeInForce) Enum.Parse(typeof (TimeInForce), messageTimeInForce));
      }
      catch (ArgumentException ex)
      {
        this.TimeInForce(TimeInForce.Unknown);
      }
      return this;
    }
  }
}
