// Decompiled with JetBrains decompiler
// Type: SettingsProvider.MarketDataRecord
// Assembly: SettingsProvider, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 05C1E7FD-6DD9-4012-9105-50C0A7D91CF0
// Assembly location: F:\Arbitrage Cracks\TradeMonitor\lib\SettingsProvider.dll

using System;

namespace SettingsProvider
{
  public class MarketDataRecord
  {
    private string _id = "";
    private string _symbol = "";
    private Decimal? _bid;
    private Decimal? _ask;
    private Decimal? _bidQty;
    private Decimal? _askQty;

    public string Symbol
    {
      get
      {
        return this._symbol;
      }
      set
      {
        this._symbol = value;
      }
    }

    public string Id
    {
      get
      {
        return this._id;
      }
      set
      {
        this._id = value;
      }
    }

    public Decimal? Bid
    {
      get
      {
        return this._bid;
      }
      set
      {
        this._bid = value;
      }
    }

    public Decimal? Ask
    {
      get
      {
        return this._ask;
      }
      set
      {
        this._ask = value;
      }
    }

    public Decimal? BidQty
    {
      get
      {
        return this._bidQty;
      }
      set
      {
        this._bidQty = value;
      }
    }

    public Decimal? AskQty
    {
      get
      {
        return this._askQty;
      }
      set
      {
        this._askQty = value;
      }
    }

    public MarketDataRecord(string id, string symbol, Decimal? bid = null, Decimal? ask = null, Decimal? bidQty = null, Decimal? askQty = null)
    {
      this._id = id;
      this._symbol = symbol;
      this._bid = bid;
      this._ask = ask;
      this._bidQty = bidQty;
      this._askQty = askQty;
    }
  }
}
