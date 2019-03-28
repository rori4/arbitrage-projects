// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Events.PositionBuilder
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Position;
using System;

namespace Com.Lmax.Api.Internal.Events
{
  internal class PositionBuilder
  {
    private long _accountId;
    private long _instrumentId;
    private Decimal _shortUnfilledCost;
    private Decimal _longUnfilledCost;
    private Decimal _openQuantity;
    private Decimal _openCost;
    private Decimal _cumlativeCost;

    public PositionBuilder AccountId(long accountId)
    {
      this._accountId = accountId;
      return this;
    }

    public PositionBuilder InstrumentId(long instrumentId)
    {
      this._instrumentId = instrumentId;
      return this;
    }

    public PositionBuilder ShortUnfilledCost(Decimal shortUnfilledCost)
    {
      this._shortUnfilledCost = shortUnfilledCost;
      return this;
    }

    public PositionBuilder LongUnfilledCost(Decimal longUnfilledCost)
    {
      this._longUnfilledCost = longUnfilledCost;
      return this;
    }

    public PositionBuilder OpenQuantity(Decimal openQuantity)
    {
      this._openQuantity = openQuantity;
      return this;
    }

    public PositionBuilder OpenCost(Decimal openCost)
    {
      this._openCost = openCost;
      return this;
    }

    public PositionBuilder CumulativeCost(Decimal cumulativeCost)
    {
      this._cumlativeCost = cumulativeCost;
      return this;
    }

    public PositionEvent NewInstance()
    {
      return new PositionEvent(this._accountId, this._instrumentId, this._shortUnfilledCost, this._longUnfilledCost, this._openQuantity, this._cumlativeCost, this._openCost);
    }
  }
}
