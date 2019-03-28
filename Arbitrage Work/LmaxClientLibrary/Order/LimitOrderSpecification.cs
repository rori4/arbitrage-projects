// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Order.LimitOrderSpecification
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;

namespace Com.Lmax.Api.Order
{
  public class LimitOrderSpecification : OrderSpecification
  {
    private Decimal _price;

    public LimitOrderSpecification(string instructionId, long instrumentId, Decimal price, Decimal quantity, TimeInForce timeInForce, Decimal? stopLossPriceOffset, Decimal? stopProfitPriceOffset)
      : base(instructionId, instrumentId, timeInForce, quantity, stopLossPriceOffset, stopProfitPriceOffset)
    {
      this.Price = price;
    }

    public LimitOrderSpecification(string instructionId, long instrumentId, Decimal price, Decimal quantity, TimeInForce timeInForce)
      : this(instructionId, instrumentId, price, quantity, timeInForce, new Decimal?(), new Decimal?())
    {
    }

    protected override Decimal? GetPrice()
    {
      return new Decimal?(this._price);
    }

    public Decimal Price
    {
      get
      {
        return this._price;
      }
      set
      {
        this._price = value;
      }
    }
  }
}
