// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Order.OrderSpecification
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal.Xml;
using System;

namespace Com.Lmax.Api.Order
{
  public abstract class OrderSpecification : IOrderSpecification, IRequest
  {
    private string _instructionId;
    private long _instrumentId;
    private Decimal _quantity;
    private Decimal? _stopLossPriceOffset;
    private Decimal? _stopProfitPriceOffset;
    private TimeInForce _timeInForce;

    protected OrderSpecification(string instructionId, long instrumentId, TimeInForce timeInForce, Decimal quantity, Decimal? stopLossPriceOffset, Decimal? stopProfitPriceOffset)
    {
      this._instructionId = instructionId;
      this._instrumentId = instrumentId;
      this._timeInForce = timeInForce;
      this._quantity = quantity;
      this._stopLossPriceOffset = stopLossPriceOffset;
      this._stopProfitPriceOffset = stopProfitPriceOffset;
    }

    public string InstructionId
    {
      get
      {
        return this._instructionId;
      }
      set
      {
        this._instructionId = value;
      }
    }

    public long InstrumentId
    {
      get
      {
        return this._instrumentId;
      }
      set
      {
        this._instrumentId = value;
      }
    }

    public Decimal Quantity
    {
      get
      {
        return this._quantity;
      }
      set
      {
        this._quantity = value;
      }
    }

    public Decimal? StopLossPriceOffset
    {
      get
      {
        return this._stopLossPriceOffset;
      }
      set
      {
        this._stopLossPriceOffset = value;
      }
    }

    public Decimal? StopProfitPriceOffset
    {
      get
      {
        return this._stopProfitPriceOffset;
      }
      set
      {
        this._stopProfitPriceOffset = value;
      }
    }

    public TimeInForce TimeInForce
    {
      get
      {
        return this._timeInForce;
      }
      set
      {
        this._timeInForce = value;
      }
    }

    public string Uri
    {
      get
      {
        return "/secure/trade/placeOrder";
      }
    }

    public virtual void WriteTo(IStructuredWriter writer)
    {
      writer.StartElement("req").StartElement("body").StartElement("order").ValueOrNone("instrumentId", new long?(this._instrumentId)).ValueOrNone("instructionId", this._instructionId).ValueOrNone("price", this.GetPrice()).ValueOrNone("quantity", new Decimal?(this._quantity)).ValueOrNone("timeInForce", Enum.GetName(this.TimeInForce.GetType(), (object) this._timeInForce)).ValueOrNone("stopLossOffset", this._stopLossPriceOffset).ValueOrNone("stopProfitOffset", this._stopProfitPriceOffset).EndElement("order").EndElement("body").EndElement("req");
    }

    protected abstract Decimal? GetPrice();
  }
}
