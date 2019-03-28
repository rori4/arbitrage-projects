// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Order.StopOrderSpecification
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal.Xml;
using System;

namespace Com.Lmax.Api.Order
{
  public class StopOrderSpecification : OrderSpecification
  {
    private Decimal _stopPrice;

    public StopOrderSpecification(string instructionId, long instrumentId, Decimal stopPrice, Decimal quantity, TimeInForce timeInForce, Decimal? stopLossPriceOffset, Decimal? stopProfitPriceOffset)
      : base(instructionId, instrumentId, timeInForce, quantity, stopLossPriceOffset, stopProfitPriceOffset)
    {
      this._stopPrice = stopPrice;
    }

    public StopOrderSpecification(string instructionId, long instrumentId, Decimal stopPrice, Decimal quantity, TimeInForce timeInForce)
      : this(instructionId, instrumentId, stopPrice, quantity, timeInForce, new Decimal?(), new Decimal?())
    {
    }

    public override void WriteTo(IStructuredWriter writer)
    {
      writer.StartElement("req").StartElement("body").StartElement("order").ValueOrNone("instrumentId", new long?(this.InstrumentId)).ValueOrNone("instructionId", this.InstructionId).ValueOrNone("stopPrice", new Decimal?(this.StopPrice)).ValueOrNone("quantity", new Decimal?(this.Quantity)).ValueOrNone("timeInForce", Enum.GetName(this.TimeInForce.GetType(), (object) this.TimeInForce)).ValueOrNone("stopLossOffset", this.StopLossPriceOffset).ValueOrNone("stopProfitOffset", this.StopProfitPriceOffset).EndElement("order").EndElement("body").EndElement("req");
    }

    protected override Decimal? GetPrice()
    {
      return new Decimal?();
    }

    public Decimal StopPrice
    {
      get
      {
        return this._stopPrice;
      }
      set
      {
        this._stopPrice = value;
      }
    }
  }
}
